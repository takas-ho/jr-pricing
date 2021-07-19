Imports Jr.Pricing.Fw
Imports Jr.Pricing.Model.Bill
Imports Jr.Pricing.Model.Discount
Imports Jr.Pricing.Model.Specification

Namespace Model
    ''' <summary>
    ''' 乗車券
    ''' </summary>
    ''' <remarks></remarks>
    Public Class BasicTicket : Inherits ValueObject : Implements ITicket
        Private ReadOnly basicFare As BasicFare
        Private ReadOnly adultType As AdultType
        Private ReadOnly [date] As DepartureDate
        Private ReadOnly discounts As TicketDiscounts

        Public Sub New(basicFare As BasicFare, adultType As AdultType, [date] As DepartureDate)
            Me.new(basicFare, adultType, [date], Nothing)
        End Sub
        Public Sub New(basicFare As BasicFare, adultType As AdultType, [date] As DepartureDate, discounts As TicketDiscounts)
            Me.basicFare = basicFare
            Me.adultType = adultType
            Me.date = [date]
            Me.discounts = If(discounts, New TicketDiscounts)
        End Sub

        Public Function CalculateFare() As Amount Implements ITicket.CalculateFare
            Return discounts.Calculate(Me.adultType.CalculateFare(Me.basicFare.Calculate()))
        End Function

        ''' <summary>
        ''' 割引ルールを設定する
        ''' </summary>
        ''' <param name="discounts">割引ルール[]</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function SetDiscounts(discounts As TicketDiscounts) As ITicket Implements ITicket.SetDiscounts
            Return New BasicTicket(basicFare, adultType, [date], discounts)
        End Function

        Protected Overrides Function GetAtomicValues() As IEnumerable(Of Object)
            Return New Object() {basicFare, adultType, [date]}
        End Function
    End Class
End Namespace