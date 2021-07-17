Imports Jr.Pricing.Fw

Namespace Model.Specification
    ''' <summary>
    ''' 出発日
    ''' </summary>
    ''' <remarks></remarks>
    Public Class DepartureDate : Inherits PrimitiveValueObject(Of DateTime)

        Public Sub New(year As Integer, month As Integer, day As Integer)
            Me.New(New DateTime(year, month, day))
        End Sub

        Public Sub New(value As String)
            Me.New(CDate(value))
        End Sub

        Public Sub New(value As DateTime)
            MyBase.New(value)
        End Sub

        Public Function GetYear() As Integer
            Return Value.Year
        End Function

    End Class
End Namespace