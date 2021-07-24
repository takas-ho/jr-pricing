Namespace Fw
    ''' <summary>
    ''' 型に関するユーティリティクラス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class TypeUtil

        ''' <summary>
        ''' Nullable(Of T) かを返す
        ''' </summary>
        ''' <param name="aType">判定するType</param>
        ''' <returns>結果</returns>
        ''' <remarks></remarks>
        Public Shared Function IsTypeNullable(ByVal aType As Type) As Boolean
            Return aType.Name.StartsWith("Nullable`")
        End Function

        ''' <summary>
        ''' 型を返す. Nullable(Of T) なら T の型を返す.
        ''' </summary>
        ''' <param name="aType">判定する Type</param>
        ''' <returns>型 (Nullable以外)</returns>
        ''' <remarks></remarks>
        Public Shared Function GetTypeIfNullable(ByVal aType As Type) As Type
            If IsTypeNullable(aType) Then
                Return aType.GetGenericArguments(0)
            End If
            Return aType
        End Function

    End Class
End Namespace
