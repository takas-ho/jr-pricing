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
    Public Class BasicTickets : Inherits CollectionObject(Of BasicTicket)

        Public Sub New()
        End Sub

        Public Sub New(src As CollectionObject(Of BasicTicket))
            MyBase.New(src)
        End Sub

        Public Sub New(initialList As IEnumerable(Of BasicTicket))
            MyBase.New(initialList)
        End Sub

        Public Overloads Function Add(item As BasicTicket) As BasicTickets
            Return MyBase.Add(Of BasicTickets)(item)
        End Function

        Public Overloads Function AddRange(items As IEnumerable(Of BasicTicket)) As BasicTickets
            Return MyBase.AddRange(Of BasicTickets)(items)
        End Function

        Public Function CalculateFare() As Amount
            Dim result As New Amount(0)
            InternalList.ForEach(Sub(item) result = result.Add(item.CalculateFare()))
            Return result
        End Function
    End Class
End Namespace