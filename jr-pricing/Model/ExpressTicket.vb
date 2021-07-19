Imports Jr.Pricing.Fw
Imports Jr.Pricing.Model.Bill
Imports Jr.Pricing.Model.Discount
Imports Jr.Pricing.Model.Specification

Namespace Model
    ''' <summary>
    ''' 特急券
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ExpressTicket : Inherits ValueObject : Implements ITicket
        Private ReadOnly expressFare As ExpressFare
        Private ReadOnly adultType As AdultType
        Private ReadOnly [date] As DepartureDate
        Private ReadOnly discounts As TicketDiscounts

        Public Sub New(expressFare As ExpressFare, adultType As AdultType, [date] As DepartureDate)
            Me.new(expressFare, adultType, [date], Nothing)
        End Sub
        Public Sub New(expressFare As ExpressFare, adultType As AdultType, [date] As DepartureDate, discounts As TicketDiscounts)
            Me.expressFare = expressFare
            Me.adultType = adultType
            Me.date = [date]
            Me.discounts = If(discounts, New TicketDiscounts)
        End Sub

        Public Function CalculateFare() As Amount Implements ITicket.CalculateFare
            Return discounts.Calculate(Me.adultType.CalculateFare(Me.expressFare.Calculate()))
        End Function

        ''' <summary>
        ''' 割引ルールを設定する
        ''' </summary>
        ''' <param name="discounts">割引ルール[]</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function SetDiscounts(discounts As TicketDiscounts) As ITicket Implements ITicket.SetDiscounts
            Return New ExpressTicket(expressFare, adultType, [date], discounts)
        End Function

        Protected Overrides Function GetAtomicValues() As IEnumerable(Of Object)
            Return New Object() {expressFare, adultType, [date]}
        End Function
    End Class
End Namespace