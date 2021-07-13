Imports Jr.Pricing.Fw
Imports Jr.Pricing.Model.Bill
Imports Jr.Pricing.Model.Specification

Namespace Model
    ''' <summary>
    ''' 特急料金
    ''' </summary>
    ''' <remarks></remarks>
    Public Class ExpressFare : Inherits ValueObject

        Private ReadOnly destination As Destination
        Private ReadOnly seatType As SeatType
        Private ReadOnly trainType As TrainType
        Private ReadOnly fare As Amount
        Private ReadOnly additionalFare As Amount

        Public Sub New(destination As Destination, seatType As SeatType, fare As Amount, additionalFare As Amount)
            Me.destination = destination
            Me.seatType = seatType
            Me.fare = fare
            Me.additionalFare = additionalFare
            Me.trainType = trainType.ValueOf(additionalFare)
        End Sub

        Protected Overrides Function GetAtomicValues() As IEnumerable(Of Object)
            Return New Object() {destination, seatType, trainType, fare, additionalFare}
        End Function

        Public Function Calculate() As Amount
            Return Me.seatType.Calculate(Me.fare.Add(additionalFare))
        End Function

    End Class
End Namespace