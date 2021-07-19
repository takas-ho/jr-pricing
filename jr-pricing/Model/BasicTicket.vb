Imports Jr.Pricing.Fw
Imports Jr.Pricing.Model.Bill
Imports Jr.Pricing.Model.Discount
Imports Jr.Pricing.Model.Specification

Namespace Model
    ''' <summary>
    ''' 乗車券
    ''' </summary>
    ''' <remarks></remarks>
    Public Class BasicTicket : Inherits ValueObject
        Private ReadOnly basicFare As BasicFare
        Private ReadOnly adultType As AdultType
        Private ReadOnly [date] As DepartureDate
        Private ReadOnly discounts As Discounts

        Public Sub New(basicFare As BasicFare, adultType As AdultType, [date] As DepartureDate)
            Me.new(basicFare, adultType, [date], Nothing)
        End Sub
        Public Sub New(basicFare As BasicFare, adultType As AdultType, [date] As DepartureDate, discounts As Discounts)
            Me.basicFare = basicFare
            Me.adultType = adultType
            Me.date = [date]
            Me.discounts = If(discounts, New Discounts)
        End Sub

        Public Function CalculateFare() As Amount
            Return discounts.Calculate(Me.adultType.CalculateFare(Me.basicFare.Calculate()))
        End Function

        ''' <summary>
        ''' 割引ルールを設定する
        ''' </summary>
        ''' <param name="discounts">割引ルール[]</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function SetDiscounts(discounts As Discounts) As BasicTicket
            Return New BasicTicket(basicFare, adultType, [date], discounts)
        End Function

        Protected Overrides Function GetAtomicValues() As IEnumerable(Of Object)
            Return New Object() {basicFare, adultType, [date]}
        End Function
    End Class
    Public Class BasicTickets : Inherits CollectionObject(Of BasicTicket)

        Public Sub New()
        End Sub

        Public Sub New(src As CollectionObject(Of BasicTicket))
            MyBase.New(src)
        End Sub

        Public Sub New(initialList As IEnumerable(Of BasicTicket))
            MyBase.New(initialList)
        End Sub

        Public Overloads Function Add(item As BasicTicket) As BasicTickets
            Return MyBase.Add(Of BasicTickets)(item)
        End Function

        Public Overloads Function AddRange(items As IEnumerable(Of BasicTicket)) As BasicTickets
            Return MyBase.AddRange(Of BasicTickets)(items)
        End Function

        Public Function CalculateFare() As Amount
            Dim result As New Amount(0)
            InternalList.ForEach(Sub(item) result = result.Add(item.CalculateFare()))
            Return result
        End Function

        ''' <summary>
        ''' すべての乗車券に割引ルールを設定する
        ''' </summary>
        ''' <param name="discounts">割引ルール[]</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function SetDiscounts(discounts As Discounts) As BasicTickets
            Dim results As BasicTickets = DirectCast(Clone(), BasicTickets)
            results.InternalList.Clear()
            For Each ticket As BasicTicket In InternalList
                results.InternalList.Add(ticket.SetDiscounts(discounts))
            Next
            Return results
        End Function
    End Class
End Namespace