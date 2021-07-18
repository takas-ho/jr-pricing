Imports Jr.Pricing.Fw
Imports Jr.Pricing.Model.Bill
Imports Jr.Pricing.Model.Discount

Namespace Model

    Public Class Tickets : Inherits CollectionObject(Of ITicket)

        Private summaryDiscounts As SummaryDiscounts

        Public Sub New()
        End Sub

        Public Sub New(src As Tickets)
            MyBase.New(src)
            Me.summaryDiscounts = src.summaryDiscounts
        End Sub

        Public Sub New(initialList As IEnumerable(Of ITicket))
            MyBase.New(initialList)
        End Sub

        Public Overloads Function Add(item As ITicket) As Tickets
            Return MyBase.Add(Of Tickets)(item)
        End Function

        Public Overloads Function AddRange(items As IEnumerable(Of ITicket)) As Tickets
            Return MyBase.AddRange(Of Tickets)(items)
        End Function

        ''' <summary>
        ''' 運賃を合計する
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function TotalFare() As Amount
            Dim result As New Amount(0)
            InternalList.ForEach(Sub(item) result = result.Add(item.CalculateFare()))
            result = summaryDiscounts.Calculate(result)
            Return result
        End Function

        ''' <summary>
        ''' すべての乗車券に割引ルールを設定する
        ''' </summary>
        ''' <param name="discounts">割引ルール[]</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function SetTicketDiscounts(discounts As TicketDiscounts) As Tickets
            Dim results As Tickets = Clone(Of Tickets)()
            results.InternalList.Clear()
            For Each ticket As ITicket In InternalList
                results.InternalList.Add(ticket.SetDiscounts(discounts))
            Next
            Return results
        End Function

        ''' <summary>
        ''' 合計金額の割引ルールを設定する
        ''' </summary>
        ''' <param name="discounts">割引ルール[]</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function SetSummaryDiscounts(discounts As SummaryDiscounts) As Tickets
            Dim results As Tickets = Clone(Of Tickets)()
            results.summaryDiscounts = discounts
            Return results
        End Function

    End Class
End Namespace