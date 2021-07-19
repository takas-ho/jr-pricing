Imports Jr.Pricing.Fw
Imports Jr.Pricing.Model.Bill
Imports Jr.Pricing.Model.Discount
Imports Jr.Pricing.Model.Specification

Namespace Model
    ''' <summary>
    ''' 特急券
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ExpressTicket : Inherits ValueObject
        Private ReadOnly expressFare As ExpressFare
        Private ReadOnly adultType As AdultType
        Private ReadOnly [date] As DepartureDate
        Private ReadOnly discounts As Discounts

        Public Sub New(expressFare As ExpressFare, adultType As AdultType, [date] As DepartureDate)
            Me.new(expressFare, adultType, [date], Nothing)
        End Sub
        Public Sub New(expressFare As ExpressFare, adultType As AdultType, [date] As DepartureDate, discounts As Discounts)
            Me.expressFare = expressFare
            Me.adultType = adultType
            Me.date = [date]
            Me.discounts = If(discounts, New Discounts)
        End Sub

        Public Function CalculateFare() As Amount
            Return discounts.Calculate(Me.adultType.CalculateFare(Me.expressFare.Calculate()))
        End Function

        ''' <summary>
        ''' 割引ルールを設定する
        ''' </summary>
        ''' <param name="discounts">割引ルール[]</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function SetDiscounts(discounts As Discounts) As ExpressTicket
            Return New ExpressTicket(expressFare, adultType, [date], discounts)
        End Function

        Protected Overrides Function GetAtomicValues() As IEnumerable(Of Object)
            Return New Object() {expressFare, adultType, [date]}
        End Function
    End Class
    Public Class ExpressTickets : Inherits CollectionObject(Of ExpressTicket)

        Public Sub New()
        End Sub

        Public Sub New(src As CollectionObject(Of ExpressTicket))
            MyBase.New(src)
        End Sub

        Public Sub New(initialList As IEnumerable(Of ExpressTicket))
            MyBase.New(initialList)
        End Sub

        Public Overloads Function Add(item As ExpressTicket) As ExpressTickets
            Return MyBase.Add(Of ExpressTickets)(item)
        End Function

        Public Overloads Function AddRange(items As IEnumerable(Of ExpressTicket)) As ExpressTickets
            Return MyBase.AddRange(Of ExpressTickets)(items)
        End Function

        Public Function CalculateFare() As Amount
            Dim result As New Amount(0)
            InternalList.ForEach(Sub(item) result = result.Add(item.CalculateFare()))
            Return result
        End Function

        ''' <summary>
        ''' すべての特急券に割引ルールを設定する
        ''' </summary>
        ''' <param name="discounts">割引ルール[]</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function SetDiscounts(discounts As Discounts) As ExpressTickets
            Dim results As ExpressTickets = DirectCast(Clone(), ExpressTickets)
            results.InternalList.Clear()
            For Each ticket As ExpressTicket In InternalList
                results.InternalList.Add(ticket.SetDiscounts(discounts))
            Next
            Return results
        End Function
    End Class
End Namespace