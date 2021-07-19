Imports Jr.Pricing.Model.Bill
Imports Jr.Pricing.Model.Discount

Namespace Model
    Public Interface ITicket
        ''' <summary>
        ''' 運賃を計算する
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function CalculateFare() As Amount

        ''' <summary>
        ''' 割引ルールを設定する
        ''' </summary>
        ''' <param name="discounts">割引ルール[]</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Function SetDiscounts(discounts As TicketDiscounts) As ITicket
    End Interface
End Namespace