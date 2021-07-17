Imports Jr.Pricing.Fw

Namespace Model.Specification
    Public Class DepartureDateRange : Inherits ValueObject

        Private ReadOnly start As DepartureDate
        Private ReadOnly [end] As DepartureDate

        Public Sub New(start As DepartureDate, [end] As DepartureDate)
            Me.start = start
            Me.end = [end]
        End Sub

        Public Function Contains([date] As DepartureDate) As Boolean
            Return start.CompareTo([date]) <= 0 AndAlso [date].CompareTo([end]) <= 0
        End Function

        Protected Overrides Function GetAtomicValues() As IEnumerable(Of Object)
            Return New Object() {start, [end]}
        End Function

    End Class
End Namespace