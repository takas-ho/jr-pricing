Imports Jr.Pricing.Model.Bill
Imports Jr.Pricing.Model.Specification

Namespace Model.Discount
    Public Class ExpressTicketDiscount : Implements ITicketDiscount
        Private ReadOnly ticketType As TicketType
        Private ReadOnly seatType As SeatType
        Private ReadOnly departureDate As DepartureDate

        Public Sub New(ticketType As TicketType, seatType As SeatType, departureDate As DepartureDate)
            Me.ticketType = ticketType
            Me.seatType = seatType
            Me.departureDate = departureDate
        End Sub

        Public Function CalculateIfNecessary(amount As Amount) As Amount Implements ITicketDiscount.CalculateIfNecessary
            If ticketType = ticketType.特急券 AndAlso seatType = seatType.指定席 Then
                Dim year As Integer = departureDate.GetYear()
                If New DepartureDateRange(New DepartureDate(New Date(year, 12, 25)), New DepartureDate(New Date(year, 12, 31))).Contains(departureDate) _
                   OrElse New DepartureDateRange(New DepartureDate(New Date(year, 1, 1)), New DepartureDate(New Date(year, 1, 10))).Contains(departureDate) Then
                    Return amount.Add(New Amount(200))
                ElseIf New DepartureDateRange(New DepartureDate(New Date(year, 1, 16)), New DepartureDate(New Date(year, 1, 30))).Contains(departureDate) Then
                    Return amount.Add(New Amount(-200))
                End If
            End If
            Return amount
        End Function
    End Class
End Namespace