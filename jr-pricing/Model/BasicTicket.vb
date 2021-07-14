Imports Jr.Pricing.Fw
Imports Jr.Pricing.Model.Bill
Imports Jr.Pricing.Model.Specification

Namespace Model
    ''' <summary>
    ''' 乗車券
    ''' </summary>
    ''' <remarks></remarks>
    Public Class BasicTicket : Inherits ValueObject
        Private ReadOnly basicFare As BasicFare
        Private ReadOnly adultType As AdultType
        Private ReadOnly [date] As DepartureDate

        Public Sub New(basicFare As BasicFare, adultType As AdultType, [date] As DepartureDate)
            Me.basicFare = basicFare
            Me.adultType = adultType
            Me.date = [date]
        End Sub

        Public Function CalculateFare() As Amount
            Return Me.adultType.CalculateFare(Me.basicFare.Calculate())
        End Function

        Protected Overrides Function GetAtomicValues() As IEnumerable(Of Object)
            Return New Object() {basicFare, adultType, [date]}
        End Function
    End Class
End Namespace