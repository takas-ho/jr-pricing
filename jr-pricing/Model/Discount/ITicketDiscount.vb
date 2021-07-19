Imports Jr.Pricing.Model.Bill

Namespace Model.Discount
    ''' <summary>
    ''' チケットの割引
    ''' </summary>
    ''' <remarks></remarks>
    Public Interface ITicketDiscount
        ''' <summary>
        ''' 割引計算をする
        ''' </summary>
        ''' <param name="amount"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function CalculateIfNecessary(amount As Amount) As Amount
    End Interface
End Namespace