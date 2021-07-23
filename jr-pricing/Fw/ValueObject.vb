Namespace Fw
    ''' <summary>
    ''' 値オブジェクトの基底クラス
    ''' </summary>
    ''' <remarks>当クラス実装のときは、Immutableクラスにしなければいけない</remarks>
    ''' <see>https://docs.microsoft.com/ja-jp/dotnet/standard/microservices-architecture/microservice-ddd-cqrs-patterns/implement-value-objects</see>
    Public MustInherit Class ValueObject

        ''' <summary>
        ''' 同値判定に含めるメンバー変数値を列挙して返す
        ''' </summary>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Protected MustOverride Function GetAtomicValues() As IEnumerable(Of Object)

        Protected Shared Function EqualsOperator(left As ValueObject, right As ValueObject) As Boolean
            If ReferenceEquals(left, Nothing) Xor ReferenceEquals(right, Nothing) Then
                Return False
            End If
            Return ReferenceEquals(left, Nothing) OrElse left.Equals(right)
        End Function

        Protected Shared Function NotEqualsOperator(left As ValueObject, right As ValueObject) As Boolean
            Return Not EqualsOperator(left, right)
        End Function

        Public Overrides Function Equals(obj As Object) As Boolean
            If obj Is Nothing OrElse Not (obj.GetType Is Me.GetType) Then
                Return False
            End If
            Dim other As ValueObject = DirectCast(obj, ValueObject)
            Dim thisValues As IEnumerator(Of Object) = GetAtomicValues.GetEnumerator
            Dim otherValues As IEnumerator(Of Object) = other.GetAtomicValues.GetEnumerator
            While thisValues.MoveNext AndAlso otherValues.MoveNext
                If ReferenceEquals(thisValues.Current, Nothing) Xor ReferenceEquals(otherValues.Current, Nothing) Then
                    Return False
                End If
                If thisValues.Current IsNot Nothing AndAlso Not thisValues.Current.Equals(otherValues.Current) Then
                    Return False
                End If
            End While
            Return Not thisValues.MoveNext AndAlso Not otherValues.MoveNext
        End Function

        Public Overrides Function GetHashCode() As Integer
            Return EzUtil.MakeKey(GetAtomicValues.Select(Function(x) If(x, 0)).ToArray).GetHashCode
        End Function

    End Class
End Namespace