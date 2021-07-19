Imports Jr.Pricing.Model.Bill

Namespace Model.Discount
    Public Interface IDiscount
        ''' <summary>
        ''' 割引計算をする
        ''' </summary>
        ''' <param name="amount"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function CalculateIfNecessary(amount As Amount) As Amount
    End Interface
End Namespace