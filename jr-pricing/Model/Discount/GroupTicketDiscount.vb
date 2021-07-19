Imports Jr.Pricing.Model.Bill
Imports Jr.Pricing.Model.Specification

Namespace Model.Discount
    ''' <summary>
    ''' 団体割引（チケット向け）
    ''' </summary>
    ''' <remarks></remarks>
    Public Class GroupTicketDiscount : Implements ITicketDiscount
        Private ReadOnly persons As Integer
        Private ReadOnly departureDate As DepartureDate

        Public Sub New(persons As Integer, departureDate As DepartureDate)
            Me.persons = persons
            Me.departureDate = departureDate
        End Sub

        Public Function CalculateIfNecessary(amount As Amount) As Amount Implements ITicketDiscount.CalculateIfNecessary
            If persons < 8 Then
                Return amount
            End If
            Dim year As Integer = departureDate.GetYear()
            If New DepartureDateRange(New DepartureDate(year, 12, 21), New DepartureDate(year, 12, 31)).Contains(departureDate) _
               OrElse New DepartureDateRange(New DepartureDate(year, 1, 1), New DepartureDate(year, 1, 10)).Contains(departureDate) Then
                Return amount.ReduceByPercent(10)
            End If
            Return amount.ReduceByPercent(15)
        End Function
    End Class
End Namespace