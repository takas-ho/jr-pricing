Imports Jr.Pricing.Fw
Imports Jr.Pricing.Model.Bill
Imports Jr.Pricing.Model.Specification

Namespace Model
    ''' <summary>
    ''' 特急券
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ExpressTicket : Inherits ValueObject
        Private ReadOnly expressFare As ExpressFare
        Private ReadOnly adultType As AdultType
        Private ReadOnly [date] As DepartureDate

        Public Sub New(expressFare As ExpressFare, adultType As AdultType, [date] As DepartureDate)
            Me.expressFare = ExpressFare
            Me.adultType = adultType
            Me.date = [date]
        End Sub

        Public Function CalculateFare() As Amount
            Return Me.adultType.CalculateFare(Me.expressFare.Calculate())
        End Function

        Protected Overrides Function GetAtomicValues() As IEnumerable(Of Object)
            Return New Object() {expressFare, adultType, [date]}
        End Function
    End Class
End Namespace