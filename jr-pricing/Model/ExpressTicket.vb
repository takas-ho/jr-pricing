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
    Public Class ExpressTickets : Inherits CollectionObject(Of ExpressTicket)

        Public Sub New()
        End Sub

        Public Sub New(src As CollectionObject(Of ExpressTicket))
            MyBase.New(src)
        End Sub

        Public Sub New(initialList As IEnumerable(Of ExpressTicket))
            MyBase.New(initialList)
        End Sub

        Public Overloads Function Add(item As ExpressTicket) As ExpressTickets
            Return MyBase.Add(Of ExpressTickets)(item)
        End Function

        Public Overloads Function AddRange(items As IEnumerable(Of ExpressTicket)) As ExpressTickets
            Return MyBase.AddRange(Of ExpressTickets)(items)
        End Function

        Public Function CalculateFare() As Amount
            Dim result As New Amount(0)
            InternalList.ForEach(Sub(item) result = result.Add(item.CalculateFare()))
            Return result
        End Function
    End Class
End Namespace