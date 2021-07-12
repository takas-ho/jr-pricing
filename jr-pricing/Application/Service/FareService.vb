Imports Jr.Pricing.Model
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
        Private ReadOnly distanceTable As DistanceTable

        Public Sub New(fareTable As FareTable, surchargeTable As SurchargeTable, distanceTable As DistanceTable)
            Me.fareTable = fareTable
            Me.surchargeTable = surchargeTable
            Me.distanceTable = distanceTable
        End Sub

        Public Function AmountFor(attempt As Attempt) As Amount
            Dim [to] As Destination = attempt.To()
            Dim basic As BasicFare = attempt.ToBasicFare(fareTable.GetFare([to]))
            Dim express As ExpressFare = attempt.ToExpressFare(surchargeTable.GetSurcharge([to]))
            Dim fare As Amount = basic.Calculate().Add(express.Calculate())
            Return fare
        End Function

    End Class
End Namespace