Imports Jr.Pricing.Fw
Imports Jr.Pricing.Model.Bill
Imports Jr.Pricing.Model.Discount
Imports Jr.Pricing.Model.Specification

Namespace Model
    ''' <summary>
    ''' 購入希望
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Attempt : Inherits ValueObject
        Private ReadOnly adult As Integer
        Private ReadOnly child As Integer

        Private ReadOnly departureDate As DepartureDate
        Private ReadOnly destination As Destination

        Private ReadOnly seatType As SeatType
        Private ReadOnly trainType As TrainType
        Private ReadOnly ticketSellerType As TicketSellerType

        Public Sub New(adult As Integer, child As Integer, departureDate As DepartureDate, destination As Destination, seatType As SeatType, trainType As TrainType, ticketSellerType As TicketSellerType)
            Me.adult = adult
            Me.child = child
            Me.departureDate = departureDate
            Me.destination = destination
            Me.seatType = seatType
            Me.trainType = trainType
            Me.ticketSellerType = ticketSellerType
        End Sub

        Protected Overrides Function GetAtomicValues() As IEnumerable(Of Object)
            Return New Object() {adult, child, departureDate, destination, seatType, trainType, ticketSellerType}
        End Function

        Public ReadOnly Property [To]() As Destination
            Get
                Return destination
            End Get
        End Property

        Public Overrides Function ToString() As String
            Return String.Format("大人={1}人{0}" _
                                 & "子供={2}人{0}" _
                                 & "出発日={3}{0}" _
                                 & "目的地={4}{0}" _
                                 & "座席区分={5}{0}" _
                                 & "列車種類={6}{0}" _
                                 & "片道/往復={7}", vbCrLf, adult, child, departureDate, destination, seatType, trainType, ticketSellerType)
        End Function

        Private Function ToBasicFare(fare As Integer) As BasicFare
            Return New BasicFare(destination, New Amount(fare))
        End Function

        Private Function ToExpressFare(fare As Integer, additionalFare As Integer) As ExpressFare
            Return New ExpressFare(destination, seatType, New Amount(fare), trainType.Calculate(New Amount(additionalFare)))
        End Function

        Private Function ToBasicTicket(fare As Integer, distance As Integer) As Tickets
            Dim results As New Tickets
            results = results.AddRange(Enumerable.Range(0, ticketSellerType.CalculateTickets(adult)) _
                                       .Select(Function(i) New BasicTicket(ToBasicFare(fare), AdultType.大人, departureDate)).Cast(Of ITicket))
            results = results.AddRange(Enumerable.Range(0, ticketSellerType.CalculateTickets(child)) _
                                       .Select(Function(i) New BasicTicket(ToBasicFare(fare), AdultType.小人, departureDate)).Cast(Of ITicket))
            results = results.SetDiscounts(BuildDiscounts(distance, TicketType.乗車券))
            Return results
        End Function

        Private Function ToExpressTicket(expressFare As Integer, additionalFare As Integer, distance As Integer) As Tickets
            Dim results As New Tickets
            results = results.AddRange(Enumerable.Range(0, ticketSellerType.CalculateTickets(adult)) _
                                       .Select(Function(i) New ExpressTicket(ToExpressFare(expressFare, additionalFare), AdultType.大人, departureDate)).Cast(Of ITicket))
            results = results.AddRange(Enumerable.Range(0, ticketSellerType.CalculateTickets(child)) _
                                       .Select(Function(i) New ExpressTicket(ToExpressFare(expressFare, additionalFare), AdultType.小人, departureDate)).Cast(Of ITicket))
            results = results.SetDiscounts(BuildDiscounts(distance, TicketType.特急券))
            Return results
        End Function

        Private Function BuildDiscounts(distance As Integer, ticketType As TicketType) As Discounts
            Return New Discounts({New RoundTripDiscount(distance, ticketType, ticketSellerType),
                                  New GroupDiscountForTicket(adult + child, departureDate)})
        End Function

        Public Function ToTickets(fare As Integer, expressFare As Integer, additionalFare As Integer, distance As Integer) As Tickets
            Dim basics As Tickets = ToBasicTicket(fare, distance)
            Dim expresses As Tickets = ToExpressTicket(expressFare, additionalFare, distance)
            Return basics.AddRange(Enumerable.Range(0, expresses.Count).Select(Function(i) expresses(i)))
        End Function

    End Class
End Namespace