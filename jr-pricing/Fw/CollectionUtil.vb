Namespace Fw
    ''' <summary>
    ''' コレクション用のユーティリティクラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class CollectionUtil
        ''' <summary>値の一意キーを作成する</summary>
        ''' <typeparam name="T">値の型</typeparam>
        ''' <param name="obj">値</param>
        ''' <returns>一意キー</returns>
        ''' <remarks></remarks>
        Public Delegate Function MakeKeyCallback(Of T)(ByVal obj As T) As Object

        ''' <summary>値の一意キーを作成する</summary>
        ''' <typeparam name="T">値の型</typeparam>
        ''' <param name="obj">値</param>
        ''' <returns>一意キー</returns>
        ''' <remarks></remarks>
        Public Delegate Function MakeKeyCallback(Of T, TResult)(ByVal obj As T) As TResult

        ''' <summary>
        ''' 値を持つコレクションか？を返す
        ''' </summary>
        ''' <param name="collection">コレクション</param>
        ''' <returns>判定結果</returns>
        ''' <remarks></remarks>
        Public Shared Function IsNotEmpty(ByVal collection As IEnumerable) As Boolean
            If collection Is Nothing Then
                Return False
            End If
            Return collection.Cast(Of Object)().Any()
        End Function

        ''' <summary>
        ''' 値を持つコレクションか？を返す
        ''' </summary>
        ''' <param name="collection">コレクション</param>
        ''' <returns>判定結果</returns>
        ''' <remarks></remarks>
        Public Shared Function IsNotEmpty(ByVal collection As ICollectionObject) As Boolean
            If collection Is Nothing Then
                Return False
            End If
            Return 0 < collection.Count
        End Function

        ''' <summary>
        ''' 値を持たないコレクションか？を返す
        ''' </summary>
        ''' <param name="collection">コレクション</param>
        ''' <returns>判定結果</returns>
        ''' <remarks></remarks>
        Public Shared Function IsEmpty(ByVal collection As IEnumerable) As Boolean
            Return Not IsNotEmpty(collection)
        End Function

        ''' <summary>
        ''' 値を持たないコレクションか？を返す
        ''' </summary>
        ''' <param name="collection">コレクション</param>
        ''' <returns>判定結果</returns>
        ''' <remarks></remarks>
        Public Shared Function IsEmpty(ByVal collection As ICollectionObject) As Boolean
            Return Not IsNotEmpty(collection)
        End Function

        ''' <summary>
        ''' 特定のキーごとの一覧にして返す
        ''' </summary>
        ''' <typeparam name="T">コレクションの型</typeparam>
        ''' <param name="vos">コレクション値[]</param>
        ''' <param name="aMakeKey">特定のキーを作成 ※ラムダ式</param>
        ''' <returns>キーごとの一覧</returns>
        ''' <remarks></remarks>
        Public Shared Function MakeVosByKey(Of T, TResult)(ByVal vos As IEnumerable(Of T), ByVal aMakeKey As MakeKeyCallback(Of T, TResult)) As Dictionary(Of TResult, List(Of T))
            Dim result As New Dictionary(Of TResult, List(Of T))
            For Each vo As T In vos
                Dim key As TResult = aMakeKey(vo)
                If Not result.ContainsKey(key) Then
                    result.Add(key, New List(Of T))
                End If
                result(key).Add(vo)
            Next
            Return result
        End Function

        ''' <summary>
        ''' 特定のキーごとにして返す ※同一キーの値は、先勝ち
        ''' </summary>
        ''' <typeparam name="T">コレクションの型</typeparam>
        ''' <param name="vos">コレクション値[]</param>
        ''' <param name="aMakeKey">特定のキーを作成 ※ラムダ式</param>
        ''' <returns>キーごとの値</returns>
        ''' <remarks></remarks>
        Public Shared Function MakeVoByKey(Of T, TResult)(ByVal vos As IEnumerable(Of T), ByVal aMakeKey As MakeKeyCallback(Of T, TResult)) As Dictionary(Of TResult, T)
            Dim result As New Dictionary(Of TResult, T)
            For Each vo As T In vos
                Dim key As TResult = aMakeKey(vo)
                If result.ContainsKey(key) Then
                    Continue For
                End If
                result.Add(key, vo)
            Next
            Return result
        End Function

    End Class
End Namespace
