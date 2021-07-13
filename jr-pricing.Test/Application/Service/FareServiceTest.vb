Imports Jr.Pricing.Model
Imports Jr.Pricing.Model.Bill
Imports Jr.Pricing.Model.Rules
Imports Jr.Pricing.Model.Specification
Imports NUnit.Framework

Namespace Application.Service
    Public Class FareServiceTest

        Private Shared fareService As FareService

        Private Shared fareTable As FareTable
        Private Shared surchargeTable As SurchargeTable
        Private Shared additionalSurchargeTable As AdditionalSurchargeTable
        Private Shared distanceTable As DistanceTable

        <TestFixtureSetUp()>
        Public Shared Sub SetUpOnce()
            fareTable = New FareTable
            surchargeTable = New SurchargeTable
            additionalSurchargeTable = New AdditionalSurchargeTable
            distanceTable = New DistanceTable

            fareService = New FareService(fareTable, surchargeTable, additionalSurchargeTable, distanceTable)
        End Sub

        <Test()>
        Public Sub 基本()
            Dim attempt As Attempt = AttemptFactory.大人1_通常期_新大阪_指定席_ひかり_片道()
            Dim result As Amount = fareService.AmountFor(attempt)
            Dim destination As Destination = Destination.新大阪
            Dim expected As New Amount(fareTable.GetFare(destination) + surchargeTable.GetSurcharge(destination))
            Assert.That(result, [Is].EqualTo(expected))
        End Sub

        <Test()>
        Public Sub 自由席は割引()
            Dim attempt As Attempt = AttemptFactory.大人1_通常期_新大阪_自由席_ひかり_片道()
            Dim result As Amount = fareService.AmountFor(attempt)
            Dim destination As Destination = Destination.新大阪
            Dim expected As New Amount(fareTable.GetFare(destination) + surchargeTable.GetSurcharge(destination) - 530)
            Assert.That(result, [Is].EqualTo(expected))
        End Sub

        <Test()>
        Public Sub のぞみは割増し()
            Dim attempt As Attempt = AttemptFactory.大人1_通常期_新大阪_指定席_のぞみ_片道()
            Dim result As Amount = fareService.AmountFor(attempt)
            Dim destination As Destination = Destination.新大阪
            Dim expected As New Amount(fareTable.GetFare(destination) + surchargeTable.GetSurcharge(destination) + additionalSurchargeTable.GetAdditionalSurcharge(destination))
            Assert.That(result, [Is].EqualTo(expected))
        End Sub

    End Class
End Namespace