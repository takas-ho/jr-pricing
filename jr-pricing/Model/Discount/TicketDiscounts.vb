Imports Jr.Pricing.Fw
Imports Jr.Pricing.Model.Bill

Namespace Model.Discount
    Public Class TicketDiscounts : Inherits CollectionObject(Of ITicketDiscount)

        Public Sub New()
        End Sub

        Public Sub New(src As CollectionObject(Of ITicketDiscount))
            MyBase.New(src)
        End Sub

        Public Sub New(initialList As IEnumerable(Of ITicketDiscount))
            MyBase.New(initialList)
        End Sub

        ''' <summary>
        ''' すべての割引ルールで計算をする
        ''' </summary>
        ''' <param name="amount"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Calculate(amount As Amount) As Amount
            Return InternalList.Aggregate(amount, Function(current, aDiscount) aDiscount.CalculateIfNecessary(current)).Floor10()
        End Function

    End Class
End Namespace