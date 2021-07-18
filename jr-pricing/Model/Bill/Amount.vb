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
            Return ReduceByPercent(50)
        End Function

        ''' <summary>
        ''' 10円未満を切り捨てる
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function Floor10() As Amount
            Return New Amount(CInt(Math.Floor(Value / 10)) * 10)
        End Function

        ''' <summary>
        ''' 率(%)で割引する
        ''' </summary>
        ''' <param name="percent"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function ReduceByPercent(percent As Integer) As Amount
            Return MultiplyAndDivide(multiplier:=100 - percent, divisor:=100)
        End Function

        ''' <summary>
        ''' 乗算して除算する
        ''' </summary>
        ''' <param name="multiplier"></param>
        ''' <param name="divisor"></param>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Function MultiplyAndDivide(multiplier As Integer, divisor As Integer) As Amount
            Return New Amount(CInt(Math.Floor(Value * multiplier / divisor))).Floor10()
        End Function

    End Class
End Namespace