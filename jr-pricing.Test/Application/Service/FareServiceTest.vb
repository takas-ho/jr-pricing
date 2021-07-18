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

        Public Function CalculateHalf(fare As Integer) As Integer
            Return CInt(Math.Floor(fare / 2 / 10) * 10)
        End Function

        Public Function ReduceBy10Percent(fare As Integer) As Integer
            Return ReduceByPercent(fare, percent:=10)
        End Function

        Public Function ReduceByPercent(fare As Integer, percent As Integer) As Integer
            Return CInt(Math.Floor(fare * (100 - percent) / 100 / 10) * 10)
        End Function

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
        End Sub

        <Test()>
        Public Sub 大人2_小人2()
            Dim attempt As Attempt = AttemptFactory.大人2_小人2_通常期_新大阪_指定席_ひかり_片道()
            Dim result As Amount = fareService.AmountFor(attempt)
            Dim destination As Destination = Destination.新大阪
            Dim adultFare As Integer = fareTable.GetFare(destination) + surchargeTable.GetSurcharge(destination)
            Dim childFare As Integer = CalculateHalf(fareTable.GetFare(destination)) + CalculateHalf(surchargeTable.GetSurcharge(destination))
            Dim expected As New Amount(adultFare * 2 + childFare * 2)
            Assert.That(result, [Is].EqualTo(expected))
        End Sub

        <Test()>
        Public Sub 片道600km以上の往復チケットだと_運賃が1割引き()
            Dim attempt As Attempt = AttemptFactory.大人1_通常期_姫路_指定席_ひかり_往復()
            Dim actual As Amount = fareService.AmountFor(attempt)
            Dim destination As Destination = Destination.姫路
            Dim adultFare As Integer = ReduceBy10Percent(fareTable.GetFare(destination)) + surchargeTable.GetSurcharge(destination)
            Dim expected As New Amount(adultFare * 2)
            Assert.That(actual, [Is].EqualTo(expected))
        End Sub

        <TestCase("2019/12/20", 15, "その他は15%引き")>
        <TestCase("2019/12/21", 10, "年末年始(12/21-1/10)は1割引き")>
        <TestCase("2019/12/31", 10, "年末年始(12/21-1/10)は1割引き")>
        <TestCase("2019/1/1", 10, "年末年始(12/21-1/10)は1割引き")>
        <TestCase("2019/1/10", 10, "年末年始(12/21-1/10)は1割引き")>
        <TestCase("2019/1/11", 15, "その他は15%引き")>
        Public Sub _8人_で団体割引(departureDate As String, discountPercent As Integer, message As String)
            Dim attempt As Attempt = AttemptFactory.大人8_新大阪_指定席_ひかり_片道(departureDate)
            Dim actual As Amount = fareService.AmountFor(attempt)
            Dim destination As Destination = Destination.新大阪
            Dim adultFare As Integer = ReduceByPercent(fareTable.GetFare(destination), percent:=discountPercent) + ReduceByPercent(surchargeTable.GetSurcharge(destination), percent:=discountPercent)
            Dim expected As New Amount(adultFare * 8)
            Assert.That(actual, [Is].EqualTo(expected))
        End Sub

        <TestCase(30, 30)>
        <TestCase(31, 30)>
        <TestCase(32, 31)>
        <TestCase(50, 49)>
        <TestCase(51, 49)>
        <TestCase(52, 50)>
        Public Sub 大人n_で団体割引_合計人数に応じて_無料の人員アリ(adultCount As Integer, personCount As Integer)
            Dim attempt As Attempt = AttemptFactory.大人_通常期_新大阪_指定席_ひかり_片道(adultCount)
            Dim actual As Amount = fareService.AmountFor(attempt)
            Dim destination As Destination = Destination.新大阪
            Dim adultFare As Integer = ReduceBy10Percent(fareTable.GetFare(destination)) + ReduceBy10Percent(surchargeTable.GetSurcharge(destination))
            Dim expected As New Amount(adultFare * personCount)
            Assert.That(actual, [Is].EqualTo(expected))
        End Sub

    End Class
End Namespace