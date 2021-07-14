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

        <Test()>
        Public Sub 姫路まで_のぞみで自由席()
            Dim attempt As Attempt = AttemptFactory.大人1_通常期_姫路_自由席_のぞみ_片道()
            Dim result As Amount = fareService.AmountFor(attempt)
            Dim destination As Destination = Destination.姫路
            Dim expected As New Amount(fareTable.GetFare(destination) + surchargeTable.GetSurcharge(destination) + additionalSurchargeTable.GetAdditionalSurcharge(destination) - 530)
            Assert.That(result, [Is].EqualTo(expected))
        End Sub

        <Test()>
        Public Sub 小人は半額()
            Dim attempt As Attempt = AttemptFactory.小人1_通常期_新大阪_指定席_ひかり_片道()
            Dim result As Amount = fareService.AmountFor(attempt)
            Dim destination As Destination = Destination.新大阪
            Dim half = Function(fare As Integer) CInt(Math.Floor(fare / 2 / 10) * 10)
            Dim expected As New Amount(half(fareTable.GetFare(destination)) + half(surchargeTable.GetSurcharge(destination)))
            Assert.That(result, [Is].EqualTo(expected))
        End Sub

    End Class
End Namespace