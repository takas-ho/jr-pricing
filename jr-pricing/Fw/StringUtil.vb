Imports System.Text

Namespace Fw
    ''' <summary>
    ''' 文字列操作のユーティリティ
    ''' </summary>
    ''' <remarks></remarks>
    Public Class StringUtil

        ''' <summary>
        ''' Empty値、またはNull値か、を返す
        ''' </summary>
        ''' <param name="str">判定する文字列</param>
        ''' <returns>Empty値かNull値の場合、true</returns>
        ''' <remarks></remarks>
        Public Shared Function IsEmpty(ByVal str As Object) As Boolean
            Return str Is Nothing OrElse If(TypeOf str Is PrimitiveValueObject,
                                        IsEmpty(DirectCast(str, PrimitiveValueObject).Value),
                                        str.ToString.Trim.Length = 0)
        End Function

        ''' <summary>
        ''' Empty値以外でかつ、Null値以外か、を返す
        ''' </summary>
        ''' <param name="str">判定する文字列</param>
        ''' <returns>Empty値以外でかつ、Null値以外の場合、true</returns>
        ''' <remarks></remarks>
        Public Shared Function IsNotEmpty(ByVal str As Object) As Boolean
            Return Not IsEmpty(str)
        End Function

        ''' <summary>
        ''' 数値をInteger型にして返す
        ''' </summary>
        ''' <param name="number">数値</param>
        ''' <returns>Integer型の値</returns>
        ''' <remarks></remarks>
        Public Shared Function ToInteger(ByVal number As Object) As Integer?
            Return ToInteger(number, Nothing)
        End Function

        ''' <summary>
        ''' 数値をInteger型にして返す
        ''' </summary>
        ''' <param name="number">数値</param>
        ''' <param name="valueIfNull">null置き換え値</param>
        ''' <returns>Integer型の値</returns>
        ''' <remarks></remarks>
        Public Shared Function ToInteger(ByVal number As Object, ByVal valueIfNull As Integer?) As Integer?
            Return ToInteger(StringUtil.ToString(number), valueIfNull)
        End Function

        ''' <summary>
        ''' 数値文字列をInteger型にして返す
        ''' </summary>
        ''' <param name="number">数値文字列</param>
        ''' <returns>Integer型の値</returns>
        ''' <remarks></remarks>
        Public Shared Function ToInteger(ByVal number As String) As Integer?
            Return ToInteger(number, Nothing)
        End Function

        ''' <summary>
        ''' 数値文字列をInteger型にして返す
        ''' </summary>
        ''' <param name="number">数値文字列</param>
        ''' <param name="valueIfNull">null置き換え値</param>
        ''' <returns>Integer型の値</returns>
        ''' <remarks></remarks>
        Public Shared Function ToInteger(ByVal number As String, ByVal valueIfNull As Integer?) As Integer?
            If Not IsNumeric(number) Then
                Return valueIfNull
            End If
            Return CInt(number)
        End Function

        ''' <summary>
        ''' Objectを文字列にして返す
        ''' </summary>
        ''' <param name="obj">数値</param>
        ''' <returns>文字列</returns>
        ''' <remarks>NullならNullのまま返す。Convert.ToString() はNullの時、0 が返る</remarks>
        Public Overloads Shared Function ToString(ByVal obj As Object) As String
            If obj Is Nothing Then
                Return Nothing
            End If
            If TypeOf obj Is DateTime OrElse TypeOf obj Is DateTime? Then
                Return ToDateTimeString(DirectCast(obj, DateTime?))
            End If
            Return obj.ToString
        End Function

        ''' <summary>
        ''' ローカルPC コントロールパネル|地域と言語 の日付書式に左右されない日時文字列にする
        ''' </summary>
        ''' <param name="aDate">日時</param>
        ''' <returns>yyyy/mm/dd h24:mm:ss</returns>
        ''' <remarks>日時値`2010/01/02 03:04:05`をToStringすると、XP="2010/01/02 3:04:05" Win7="10/01/02 3:04:05"</remarks>
        Public Shared Function ToDateTimeString(ByVal aDate As DateTime?) As String
            If Not aDate.HasValue Then
                Return Nothing
            End If
            Dim dateValue As Date = aDate.Value
            If dateValue.Year <= 1 Then
                Return dateValue.ToString("H:mm:ss")
            End If
            Return dateValue.ToString("yyyy/MM/dd H:mm:ss")
        End Function

        ''' <summary>
        ''' NULL値だったら空文字変換 Null Value Logic
        ''' </summary>
        ''' <param name="obj">判定object</param>
        ''' <returns>対応した文字列</returns>
        ''' <remarks>PrimitiveValueObject型を返したい場合は、`EzUtil#Nvl`を利用すべき</remarks>
        Public Shared Function Nvl(ByVal obj As Object) As String
            Return Nvl(obj, String.Empty)
        End Function

        ''' <summary>
        ''' NULL値だったら変換 Null Value Logic
        ''' </summary>
        ''' <param name="obj">判定object</param>
        ''' <param name="nullVal">置き換え文字列</param>
        ''' <returns>対応した文字列</returns>
        ''' <remarks>PrimitiveValueObject型を返したい場合は、`EzUtil#Nvl`を利用すべき</remarks>
        Public Shared Function Nvl(ByVal obj As Object, ByVal nullVal As String) As String
            If obj Is Nothing Then
                Return nullVal
            End If
            If TypeOf obj Is PrimitiveValueObject _
           AndAlso DirectCast(obj, PrimitiveValueObject).Value Is Nothing Then
                Return nullVal
            End If
            Return ToString(obj)
        End Function

        ''' <summary>
        ''' NULL値or空文字だったら変換  Empty Value Logic
        ''' </summary>
        ''' <param name="str">判定文字列</param>
        ''' <param name="emptyVal">置き換え文字列</param>
        ''' <returns>対応した文字列</returns>
        ''' <remarks></remarks>
        Public Shared Function Evl(ByVal str As Object, ByVal emptyVal As String) As String
            If IsEmpty(str) Then
                Return emptyVal
            End If
            Return ToString(str)
        End Function

        ''' <summary>
        ''' 保持している値がNULL値or空文字の値オブジェクトだったら変換  Empty Value Logic
        ''' </summary>
        ''' <param name="value">判定値オブジェクト</param>
        ''' <param name="emptyValue">置き換え値オブジェクト</param>
        ''' <returns>対応した値オブジェクト</returns>
        ''' <remarks></remarks>
        Public Shared Function Evl(Of T As PrimitiveValueObject)(ByVal value As T, ByVal emptyValue As T) As T
            If IsEmpty(value) Then
                Return emptyValue
            End If
            Return value
        End Function

        ''' <summary>
        ''' 値を整形する
        ''' </summary>
        ''' <param name="formatString">整形書式</param>
        ''' <param name="value">値</param>
        ''' <returns>整形した値</returns>
        ''' <remarks></remarks>
        Public Shared Function Format(ByVal formatString As String, ByVal value As Object) As String
            Dim sb As New StringBuilder
            Return String.Format(sb.Append("{0:").Append(formatString).Append("}").ToString, value)
        End Function


        ''' <summary>
        ''' IComparer(Of T)実装用に、Null値だったら最後にSortする値を返す
        ''' </summary>
        ''' <param name="x">値x</param>
        ''' <param name="y">値y</param>
        ''' <returns>x &lt; y なら -1, x &gt; y なら 1, 等しければ 0</returns>
        ''' <remarks></remarks>
        Public Shared Function CompareNullsLast(ByVal x As Object, ByVal y As Object) As Integer
            Return CompareNullsLast(ToString(x), ToString(y))
        End Function

        ''' <summary>
        ''' IComparer(Of T)実装用に、Null値だったら最後に降順Sortする値を返す
        ''' </summary>
        ''' <param name="x">値x</param>
        ''' <param name="y">値y</param>
        ''' <returns>x &lt; y なら -1, x &gt; y なら 1, 等しければ 0</returns>
        ''' <remarks></remarks>
        Public Shared Function CompareDescNullsLast(ByVal x As Object, ByVal y As Object) As Integer
            Return CompareDescNullsLast(ToString(x), ToString(y))
        End Function

        ''' <summary>
        ''' IComparer(Of T)実装用に、Null値だったら最後にSortする値を返す
        ''' </summary>
        ''' <param name="x">値x</param>
        ''' <param name="y">値y</param>
        ''' <returns>x &lt; y なら -1, x &gt; y なら 1, 等しければ 0</returns>
        ''' <remarks></remarks>
        Public Shared Function CompareNullsLast(ByVal x As String, ByVal y As String) As Integer
            Dim result As Integer? = EzUtil.CompareObjectNullsLast(x, y)
            If result.HasValue Then
                Return result.Value
            End If
            Return x.CompareTo(y)
        End Function

        ''' <summary>
        ''' IComparer(Of T)実装用に、Null値だったら最後に降順Sortする値を返す
        ''' </summary>
        ''' <param name="x">値x</param>
        ''' <param name="y">値y</param>
        ''' <returns>x &lt; y なら -1, x &gt; y なら 1, 等しければ 0</returns>
        ''' <remarks></remarks>
        Public Shared Function CompareDescNullsLast(ByVal x As String, ByVal y As String) As Integer
            Dim result As Integer? = EzUtil.CompareObjectNullsLast(x, y)
            If result.HasValue Then
                Return result.Value
            End If
            Return y.CompareTo(x)
        End Function

    End Class
End Namespace
