Imports Jr.Pricing.Model.Bill

Namespace Model.Discount
    Public Class GroupSummaryDiscount : Implements ISummaryDiscount

        Private ReadOnly personCount As Integer
        Public Sub New(personCount As Integer)
            Me.personCount = personCount
        End Sub

        Public Function CalculateIfNecessary(amount As Amount) As Amount Implements ISummaryDiscount.CalculateIfNecessary
            If personCount <= 30 Then
                Return amount
            End If
            Return amount.MultiplyAndDivide(personCount - CInt(Math.Ceiling(personCount / 50)), personCount)
        End Function

    End Class
End Namespace