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

        Public Function CalculateHalf() As Amount
            Return New Amount(CInt(Math.Floor(Value / 2 / 10) * 10))
        End Function

        ''' <summary>
        ''' 率(%)で割引する
        ''' </summary>
        ''' <param name="percent"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ReduceByPercent(percent As Integer) As Amount
            Return New Amount(CInt(Math.Floor(Value * (100 - percent) / 100 / 10) * 10))
        End Function
    End Class
End Namespace