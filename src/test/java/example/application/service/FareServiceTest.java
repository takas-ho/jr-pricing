package example.application.service;

import example.domain.model.attempt.Attempt;
import example.domain.model.bill.Amount;
import example.domain.model.rules.DistanceTable;
import example.domain.model.rules.FareTable;
import example.domain.model.rules.SurchargeTable;
import example.domain.model.specification.Destination;
import org.junit.jupiter.api.BeforeAll;
import org.junit.jupiter.api.Test;
import static org.junit.jupiter.api.Assertions.*;

class FareServiceTest {

    static private FareService fareService;

    static private FareTable fareTable;
    static private SurchargeTable surchargeTable;
    static private DistanceTable distanceTable;

    @BeforeAll
    static void setUp() {
        fareTable = new FareTable();
        surchargeTable = new SurchargeTable();
        distanceTable = new DistanceTable();

        fareService = new FareService(fareTable, surchargeTable, distanceTable);
    }

    @Test
    void 基本() {
        Attempt attempt = AttemptFactory.大人1_通常期_新大阪_指定席_ひかり_片道();
        Amount result = fareService.amountFor(attempt);
        Destination destination = Destination.新大阪;
        Amount expected = new Amount(fareTable.fare(destination) + surchargeTable.surcharge(destination));
        assertEquals(expected, result);
    }

    @Test
    void 自由席は_割引() {
        Attempt attempt = AttemptFactory.大人1_通常期_新大阪_自由席_ひかり_片道();
        Amount result = fareService.amountFor(attempt);
        Destination destination = Destination.新大阪;
        Amount expected = new Amount(fareTable.fare(destination) + surchargeTable.surcharge(destination) - 530);
        assertEquals(expected, result);
    }
}