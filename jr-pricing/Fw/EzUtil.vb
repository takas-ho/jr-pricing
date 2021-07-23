Imports System.Text

Namespace Fw
    ''' <summary>
    ''' 簡素な共通処理を集めたユーティリティクラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class EzUtil

        ''' <summary>
        ''' 指定キーの組み合わせで一意になる値を作成する
        ''' </summary>
        ''' <param name="keys">キー()</param>
        ''' <returns>一意のキー</returns>
        ''' <remarks></remarks>
        Public Shared Function MakeKey(ByVal ParamArray keys As Object()) As String
            Const SEPARATOR As String = ";'"":"
            Const SEPARATOR_LENGTH As Integer = 4
            'Return Join(keys, SEPARATOR)
            ' パフォーマンスチューニング
            ' 165922回呼び出しにて404.08(ms)が165.922(ms)に。
            If keys Is Nothing OrElse keys.Length = 0 Then
                Return Nothing
            End If
            Dim result As New StringBuilder
            For Each key As Object In keys
                result.Append(key).Append(SEPARATOR)
            Next
            result.Length -= SEPARATOR_LENGTH
            Return result.ToString
        End Function

        Private Delegate Function CompareToCallback(Of T)(ByVal x As T, ByVal y As T) As Integer
        ''' <summary>
        ''' IComparer(Of T)実装用に、Null値だったら最後にSortする値を返す
        ''' </summary>
        ''' <param name="x">値x</param>
        ''' <param name="y">値y</param>
        ''' <returns>x &lt; y なら -1, x &gt; y なら 1, 等しければ 0</returns>
        ''' <remarks></remarks>
        Private Shared Function PerformCompareNullsLast(Of T As Structure)(ByVal callback As CompareToCallback(Of T), ByVal x As T?, ByVal y As T?) As Integer
            Dim result As Integer? = CompareObjectNullsLast(x, y)
            If result.HasValue Then
                Return result.Value
            End If
            Return callback(x.Value, y.Value)
        End Function

        ''' <summary>
        ''' IComparer(Of T)実装用に、Null値だったら最後に降順Sortする値を返す
        ''' </summary>
        ''' <param name="x">値x</param>
        ''' <param name="y">値y</param>
        ''' <returns>x &lt; y なら 1, x &gt; y なら -1, 等しければ 0</returns>
        ''' <remarks></remarks>
        Private Shared Function PerformCompareDescNullsLast(Of T As Structure)(ByVal callback As CompareToCallback(Of T), ByVal x As T?, ByVal y As T?) As Integer
            Dim result As Integer? = CompareObjectNullsLast(x, y)
            If result.HasValue Then
                Return result.Value
            End If
            Return callback(y.Value, x.Value)
        End Function

        ''' <summary>
        ''' IComparer(Of T)実装用に、Null値だったら最後にSortする値を返す
        ''' </summary>
        ''' <param name="x">値x</param>
        ''' <param name="y">値y</param>
        ''' <returns>x &lt; y なら -1, x &gt; y なら 1, 等しければ 0</returns>
        ''' <remarks></remarks>
        Public Shared Function CompareNullsLast(ByVal x As Decimal?, ByVal y As Decimal?) As Integer
            Return PerformCompareNullsLast(Of Decimal)(Function(x2, y2) x2.CompareTo(y2), x, y)
        End Function

        ''' <summary>
        ''' IComparer(Of T)実装用に、Null値だったら最後に降順Sortする値を返す
        ''' </summary>
        ''' <param name="x">値x</param>
        ''' <param name="y">値y</param>
        ''' <returns>x &lt; y なら 1, x &gt; y なら -1, 等しければ 0</returns>
        ''' <remarks></remarks>
        Public Shared Function CompareDescNullsLast(ByVal x As Decimal?, ByVal y As Decimal?) As Integer
            Return PerformCompareDescNullsLast(Of Decimal)(Function(x2, y2) x2.CompareTo(y2), x, y)
        End Function

        ''' <summary>
        ''' IComparer(Of T)実装用に、Null値だったら最後にSortする値を返す
        ''' </summary>
        ''' <param name="x">値x</param>
        ''' <param name="y">値y</param>
        ''' <returns>x &lt; y なら -1, x &gt; y なら 1, 等しければ 0</returns>
        ''' <remarks></remarks>
        Public Shared Function CompareNullsLast(ByVal x As Long?, ByVal y As Long?) As Integer
            Return PerformCompareNullsLast(Of Long)(Function(x2, y2) x2.CompareTo(y2), x, y)
        End Function

        ''' <summary>
        ''' IComparer(Of T)実装用に、Null値だったら最後に降順Sortする値を返す
        ''' </summary>
        ''' <param name="x">値x</param>
        ''' <param name="y">値y</param>
        ''' <returns>x &lt; y なら 1, x &gt; y なら -1, 等しければ 0</returns>
        ''' <remarks></remarks>
        Public Shared Function CompareDescNullsLast(ByVal x As Long?, ByVal y As Long?) As Integer
            Return PerformCompareDescNullsLast(Of Long)(Function(x2, y2) x2.CompareTo(y2), x, y)
        End Function

        ''' <summary>
        ''' IComparer(Of T)実装用に、Null値だったら最後にSortする値を返す
        ''' </summary>
        ''' <param name="x">値x</param>
        ''' <param name="y">値y</param>
        ''' <returns>x &lt; y なら -1, x &gt; y なら 1, 等しければ 0</returns>
        ''' <remarks></remarks>
        Public Shared Function CompareNullsLast(ByVal x As Integer?, ByVal y As Integer?) As Integer
            Return PerformCompareNullsLast(Of Integer)(Function(x2, y2) x2.CompareTo(y2), x, y)
        End Function

        ''' <summary>
        ''' IComparer(Of T)実装用に、Null値だったら最後に降順Sortする値を返す
        ''' </summary>
        ''' <param name="x">値x</param>
        ''' <param name="y">値y</param>
        ''' <returns>x &lt; y なら 1, x &gt; y なら -1, 等しければ 0</returns>
        ''' <remarks></remarks>
        Public Shared Function CompareDescNullsLast(ByVal x As Integer?, ByVal y As Integer?) As Integer
            Return PerformCompareDescNullsLast(Of Integer)(Function(x2, y2) x2.CompareTo(y2), x, y)
        End Function

        ''' <summary>
        ''' IComparer(Of T)実装用に、Null値だったら最後にSortする値を返す
        ''' </summary>
        ''' <param name="x">値x</param>
        ''' <param name="y">値y</param>
        ''' <returns>Null値だったら最後にSortする値. それ以外は、Nothing</returns>
        ''' <remarks></remarks>
        Public Shared Function CompareObjectNullsLast(ByVal x As Object, ByVal y As Object) As Integer?
            If x Is Nothing AndAlso y Is Nothing Then
                Return 0
            ElseIf x Is Nothing Then
                Return 1
            ElseIf y Is Nothing Then
                Return -1
            End If
            Return Nothing
        End Function

        ''' <summary>
        ''' パラメータがnullで無い事を保証する(nullの場合、例外を投げる)
        ''' </summary>
        ''' <param name="parameter">引数</param>
        ''' <param name="name">引数名</param>
        ''' <remarks></remarks>
        Public Shared Sub AssertParameterIsNotNull(ByVal parameter As Object, ByVal name As String)
            If parameter Is Nothing Then
                Throw New ArgumentNullException(name)
            End If
        End Sub

        ''' <summary>
        ''' パラメータがemptyで無い事を保証する(emptyの場合、例外を投げる)
        ''' </summary>
        ''' <param name="parameter">引数</param>
        ''' <param name="name">引数名</param>
        ''' <remarks></remarks>
        Public Shared Sub AssertParameterIsNotEmpty(ByVal parameter As Object, ByVal name As String)
            AssertParameterIsNotNull(parameter, name)
            If TypeOf parameter Is String Then
                If StringUtil.IsNotEmpty(parameter) Then
                    Return
                End If
            ElseIf TypeOf parameter Is IEnumerable Then
                Dim values As IEnumerable = DirectCast(parameter, IEnumerable)
                If CollectionUtil.IsNotEmpty(values) Then
                    Return
                End If
            ElseIf TypeOf parameter Is ICollectionObject Then
                Dim values As ICollectionObject = DirectCast(parameter, ICollectionObject)
                If CollectionUtil.IsNotEmpty(values) Then
                    Return
                End If
            ElseIf TypeOf parameter Is PrimitiveValueObject Then
                AssertParameterIsNotEmpty(DirectCast(parameter, PrimitiveValueObject).Value, name)
                Return
            Else
                Throw New NotSupportedException("String型、配列、またはコレクションにのみ対応")
            End If
            Throw New ArgumentException("emptyです.", name)
        End Sub

        ''' <summary>
        ''' 真かを返す
        ''' </summary>
        ''' <param name="value">null許容のboolean値</param>
        ''' <returns>判定結果</returns>
        ''' <remarks></remarks>
        Public Shared Function IsTrue(ByVal value As Boolean?) As Boolean
            If value Is Nothing Then
                Return False
            End If
            Return value.Value
        End Function

        ''' <summary>
        ''' 真かを返す
        ''' </summary>
        ''' <param name="value">null許容のboolean値</param>
        ''' <returns>判定結果</returns>
        ''' <remarks></remarks>
        Public Shared Function IsTrue(ByVal value As Object) As Boolean
            If value Is Nothing Then
                Return False
            End If
            If TypeOf value Is Boolean Then
                Return DirectCast(value, Boolean)
            End If
            If IsNumeric(value) Then
                Return CInt(value) <> 0
            End If
            Dim str As String = value.ToString.ToLower
            Return "true".Equals(str)
        End Function

        ''' <summary>
        ''' コンソール出力
        ''' </summary>
        ''' <param name="message">出力メッセージ</param>
        ''' <param name="args">埋め込み引数</param>
        ''' <remarks></remarks>
        Public Shared Sub logDebug(ByVal message As String, ByVal ParamArray args As Object())
            Const TIME_FORMAT As String = "HH:mm:ss,fff"
            Const SPACE As String = " "
            Dim sb As New StringBuilder
            sb.Append(DateTime.Now.ToString(TIME_FORMAT))
            sb.Append(SPACE)
            If 0 < args.Length Then
                sb.AppendFormat(message, args)
            Else
                sb.Append(message)
            End If
            Debug.Print(sb.ToString)
        End Sub

        ''' <summary>
        ''' Empty値、またはNull値か、を返す
        ''' </summary>
        ''' <param name="str">判定する文字列</param>
        ''' <returns>Empty値かNull値の場合、true</returns>
        ''' <remarks></remarks>
        Public Shared Function IsEmpty(ByVal str As String) As Boolean
            Return StringUtil.IsEmpty(str)
        End Function

        ''' <summary>
        ''' Empty値以外でかつ、Null値以外か、を返す
        ''' </summary>
        ''' <param name="str">判定する文字列</param>
        ''' <returns>Empty値以外でかつ、Null値以外の場合、true</returns>
        ''' <remarks></remarks>
        Public Shared Function IsNotEmpty(ByVal str As String) As Boolean
            Return StringUtil.IsNotEmpty(str)
        End Function

        ''' <summary>
        ''' 値を持つコレクションか？を返す
        ''' </summary>
        ''' <param name="collection">コレクション</param>
        ''' <returns>判定結果</returns>
        ''' <remarks></remarks>
        Public Shared Function IsNotEmpty(ByVal collection As IEnumerable) As Boolean
            Return CollectionUtil.IsNotEmpty(collection)
        End Function

        ''' <summary>
        ''' 値を持たないコレクションか？を返す
        ''' </summary>
        ''' <param name="collection">コレクション</param>
        ''' <returns>判定結果</returns>
        ''' <remarks></remarks>
        Public Shared Function IsEmpty(ByVal collection As IEnumerable) As Boolean
            Return CollectionUtil.IsEmpty(collection)
        End Function

        ''' <summary>
        ''' 値を持つコレクションか？を返す
        ''' </summary>
        ''' <param name="collection">コレクション</param>
        ''' <returns>判定結果</returns>
        ''' <remarks></remarks>
        Public Shared Function IsNotEmpty(ByVal collection As ICollectionObject) As Boolean
            Return CollectionUtil.IsNotEmpty(collection)
        End Function

        ''' <summary>
        ''' 値を持たないコレクションか？を返す
        ''' </summary>
        ''' <param name="collection">コレクション</param>
        ''' <returns>判定結果</returns>
        ''' <remarks></remarks>
        Public Shared Function IsEmpty(ByVal collection As ICollectionObject) As Boolean
            Return CollectionUtil.IsEmpty(collection)
        End Function

        ''' <summary>
        ''' 値があるか？を返す
        ''' </summary>
        ''' <param name="value">PrimitiveValueObject</param>
        ''' <returns>判定結果</returns>
        ''' <remarks></remarks>
        Public Shared Function IsNotEmpty(Of T)(ByVal value As PrimitiveValueObject(Of T)) As Boolean
            Return Not IsEmpty(Of T)(value)
        End Function

        ''' <summary>
        ''' 値がない、もしくは初期値か？を返す
        ''' </summary>
        ''' <param name="value">PrimitiveValueObject</param>
        ''' <returns>判定結果</returns>
        ''' <remarks></remarks>
        Public Shared Function IsEmpty(Of T)(ByVal value As PrimitiveValueObject(Of T)) As Boolean
            If value Is Nothing OrElse DirectCast(value, PrimitiveValueObject).Value Is Nothing Then
                Return True
            End If
            Return value.IsEmpty
        End Function

        ''' <summary>
        ''' 値があるか？を返す
        ''' </summary>
        ''' <param name="value">コレクション</param>
        ''' <returns>判定結果</returns>
        ''' <remarks></remarks>
        Public Shared Function IsNotEmpty(Of T As Structure)(ByVal value As Nullable(Of T)) As Boolean
            Return Not IsEmpty(Of T)(value)
        End Function

        ''' <summary>
        ''' 値がない、もしくは初期値か？を返す
        ''' </summary>
        ''' <param name="value">コレクション</param>
        ''' <returns>判定結果</returns>
        ''' <remarks></remarks>
        Public Shared Function IsEmpty(Of T As Structure)(ByVal value As Nullable(Of T)) As Boolean
            Return Not value.HasValue OrElse value.Value.Equals(Activator.CreateInstance(GetType(T)))
        End Function

        ''' <summary>比較処理</summary>
        Public Delegate Function DelegateJudgeCompare() As Integer
        ''' <summary>
        ''' 比較処理を順次実行し結果を適切に返す
        ''' </summary>
        ''' <param name="compareCallbacks">比較処理[]</param>
        ''' <returns>ソート判定に必要な値</returns>
        ''' <remarks></remarks>
        Public Shared Function JudgeCompare(ByVal ParamArray compareCallbacks As DelegateJudgeCompare()) As Integer
            Return (From compareCallback In compareCallbacks Select compareCallback.Invoke()).FirstOrDefault(Function(result) result <> 0)
        End Function
        ''' <summary>
        ''' 比較処理を順次実行し結果を適切に返す
        ''' </summary>
        ''' <param name="compareCallbacks">比較処理[]</param>
        ''' <returns>ソート判定に必要な値</returns>
        ''' <remarks></remarks>
        Public Shared Function CombineCompare(ByVal ParamArray compareCallbacks As DelegateJudgeCompare()) As Integer
            Return JudgeCompare(compareCallbacks)
        End Function

    End Class
End Namespace
