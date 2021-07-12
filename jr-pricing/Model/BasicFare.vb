Imports Jr.Pricing.Fw
Imports Jr.Pricing.Model.Bill
Imports Jr.Pricing.Model.Specification

Namespace Model
    ''' <summary>
    ''' 基本運賃
    ''' </summary>
    ''' <remarks></remarks>
    Public Class BasicFare : Inherits ValueObject

        Private ReadOnly destination As Destination
        Private ReadOnly fare As Amount

        Public Sub New(destination As Destination, fare As Amount)
            Me.destination = destination
            Me.fare = fare
        End Sub

        Protected Overrides Function GetAtomicValues() As IEnumerable(Of Object)
            Return New Object() {destination, fare}
        End Function

        Public Function Calculate() As Amount
            Return fare
        End Function

    End Class
End Namespace