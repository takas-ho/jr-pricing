Imports Jr.Pricing.Fw

Namespace Model.Bill
    ''' <summary>
    ''' 金額
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Amount : Inherits PrimitiveValueObject(Of Integer)

        Public Sub New(value As Integer)
            MyBase.New(value)
        End Sub

        Public Function Add(other As Amount) As Amount
            Return New Amount(Value + other.Value)
        End Function

        Public Overrides Function ToString() As String
            Return String.Format("{0:C}円", Value)
        End Function

    End Class
End Namespace