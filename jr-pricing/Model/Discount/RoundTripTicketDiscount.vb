Imports Jr.Pricing.Model.Bill
Imports Jr.Pricing.Model.Specification

Namespace Model.Discount
    ''' <summary>
    ''' 往復割引（片道600km以上）
    ''' </summary>
    ''' <remarks></remarks>
    Public Class RoundTripTicketDiscount : Implements ITicketDiscount

        Private ReadOnly distance As Integer
        Private ReadOnly ticketType As TicketType
        Private ReadOnly ticketSellerType As TicketSellerType

        Public Sub New(distance As Integer, ticketType As TicketType, ticketSellerType As TicketSellerType)
            Me.distance = distance
            Me.ticketType = ticketType
            Me.ticketSellerType = ticketSellerType
        End Sub

        Public Function CalculateIfNecessary(amount As Amount) As Amount Implements ITicketDiscount.CalculateIfNecessary
            If ticketType = ticketType.乗車券 AndAlso 600 < distance AndAlso ticketSellerType = ticketSellerType.往復 Then
                Return amount.ReduceByPercent(10)
            End If
            Return amount
        End Function
    End Class
End Namespace