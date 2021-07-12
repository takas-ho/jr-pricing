Imports Jr.Pricing.Fw

Namespace Model.Bill
    ''' <summary>
    ''' 料金計算書
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Bill : Inherits ValueObject

        Private ReadOnly attempt As Attempt
        Private ReadOnly amount As Amount

        Public Sub New(attempt As Attempt, amount As Amount)
            Me.attempt = attempt
            Me.amount = amount
        End Sub

        Protected Overrides Function GetAtomicValues() As IEnumerable(Of Object)
            Return New Object() {attempt, amount}
        End Function

        Public Overrides Function ToString() As String
            Return attempt.ToString() & vbCrLf & amount.ToString()
        End Function

    End Class
End Namespace