﻿Imports Jr.Pricing.Fw
Imports Jr.Pricing.Model.Bill
Imports Jr.Pricing.Model.Specification

Namespace Model
    ''' <summary>
    ''' 購入希望
    ''' </summary>
    ''' <remarks></remarks>
    Public Class Attempt : Inherits ValueObject
        Private ReadOnly adult As Integer
        Private ReadOnly child As Integer

        Private ReadOnly departureDate As DepartureDate
        Private ReadOnly destination As Destination

        Private ReadOnly seatType As SeatType
        Private ReadOnly trainType As TrainType
        Private ReadOnly ticketType As TicketType

        Public Sub New(adult As Integer, child As Integer, departureDate As DepartureDate, destination As Destination, seatType As SeatType, trainType As TrainType, ticketType As TicketType)
            Me.adult = adult
            Me.child = child
            Me.departureDate = departureDate
            Me.destination = destination
            Me.seatType = seatType
            Me.trainType = trainType
            Me.ticketType = ticketType
        End Sub

        Protected Overrides Function GetAtomicValues() As IEnumerable(Of Object)
            Return New Object() {adult, child, departureDate, destination, seatType, trainType, ticketType}
        End Function

        Public ReadOnly Property [To]() As Destination
            Get
                Return destination
            End Get
        End Property

        Public Overrides Function ToString() As String
            Return String.Format("大人={1}人{0}" _
                                 & "子供={2}人{0}" _
                                 & "出発日={3}{0}" _
                                 & "目的地={4}{0}" _
                                 & "座席区分={5}{0}" _
                                 & "列車種類={6}{0}" _
                                 & "片道/往復={7}", vbCrLf, adult, child, departureDate, destination, seatType, trainType, ticketType)
        End Function

        Private Function ToBasicFare(fare As Integer) As BasicFare
            Return New BasicFare(destination, New Amount(fare))
        End Function

        Private Function ToExpressFare(fare As Integer, additionalFare As Integer) As ExpressFare
            Return New ExpressFare(destination, seatType, New Amount(fare), trainType.Calculate(New Amount(additionalFare)))
        End Function

        Public Function ToBasicTicket(fare As Integer) As BasicTickets
            Dim results As New BasicTickets
            results = results.AddRange(Enumerable.Range(0, adult) _
                                       .Select(Function(i) New BasicTicket(ToBasicFare(fare), AdultType.大人, departureDate)))
            results = results.AddRange(Enumerable.Range(0, child) _
                                       .Select(Function(i) New BasicTicket(ToBasicFare(fare), AdultType.小人, departureDate)))
            Return results
        End Function

        Public Function ToExpressTicket(fare As Integer, additionalFare As Integer) As ExpressTickets
            Dim results As New ExpressTickets
            results = results.AddRange(Enumerable.Range(0, adult) _
                                       .Select(Function(i) New ExpressTicket(ToExpressFare(fare, additionalFare), AdultType.大人, departureDate)))
            results = results.AddRange(Enumerable.Range(0, child) _
                                       .Select(Function(i) New ExpressTicket(ToExpressFare(fare, additionalFare), AdultType.小人, departureDate)))
            Return results
        End Function
    End Class
End Namespace