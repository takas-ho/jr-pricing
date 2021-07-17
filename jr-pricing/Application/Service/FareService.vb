﻿Imports Jr.Pricing.Model
Imports Jr.Pricing.Model.Bill
Imports Jr.Pricing.Model.Rules
Imports Jr.Pricing.Model.Specification

Namespace Application.Service
    ''' <summary>
    ''' 料金計算サービス
    ''' </summary>
    ''' <remarks></remarks>
    Public Class FareService

        Private ReadOnly fareTable As FareTable
        Private ReadOnly surchargeTable As SurchargeTable
        Private ReadOnly additionalSurchargeTable As AdditionalSurchargeTable
        Private ReadOnly distanceTable As DistanceTable

        Public Sub New(fareTable As FareTable, surchargeTable As SurchargeTable, additionalSurchargeTable As AdditionalSurchargeTable, distanceTable As DistanceTable)
            Me.fareTable = fareTable
            Me.surchargeTable = surchargeTable
            Me.additionalSurchargeTable = additionalSurchargeTable
            Me.distanceTable = distanceTable
        End Sub

        Public Function AmountFor(attempt As Attempt) As Amount
            Dim [to] As Destination = attempt.To()
            Dim distance As Integer = distanceTable.GetDistance([to])
            Dim tickets As Tickets = attempt.ToTickets(fareTable.GetFare([to]), surchargeTable.GetSurcharge([to]), additionalSurchargeTable.GetAdditionalSurcharge([to]), distance)
            Dim fare As Amount = tickets.TotalFare()
            Return fare
        End Function

    End Class
End Namespace