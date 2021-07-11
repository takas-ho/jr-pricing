package example.application.service;

import example.domain.model.attempt.Attempt;
import example.domain.model.bill.Amount;
import example.domain.model.rules.AdditionalSurchargeTable;
import example.domain.model.rules.DistanceTable;
import example.domain.model.rules.FareTable;
import example.domain.model.rules.SurchargeTable;
import example.domain.model.specification.BasicFare;
import example.domain.model.specification.BasicTicket;
import example.domain.model.specification.Destination;
import example.domain.model.specification.ExpressFare;
import example.domain.model.specification.ExpressTicket;

import org.springframework.stereotype.Service;

/**
 * 料金計算サービス
 */
@Service
public class FareService {

    FareTable fareTable;
    SurchargeTable surchargeTable;
    AdditionalSurchargeTable additionalSurchargeTable;
    DistanceTable distanceTable;

    public FareService(FareTable fareTable, SurchargeTable surchargeTable, AdditionalSurchargeTable additionalSurchargeTable, DistanceTable distanceTable) {
        this.fareTable = fareTable;
        this.surchargeTable = surchargeTable;
        this.additionalSurchargeTable = additionalSurchargeTable;
        this.distanceTable = distanceTable;
    }

    public Amount amountFor(Attempt attempt) {
        Destination to = attempt.to();
        BasicTicket basic = attempt.toBasicTicket(fareTable.fare(to));
        ExpressTicket express  = attempt.toExpressTicket(surchargeTable.surcharge(to), additionalSurchargeTable.surcharge(to));
        Amount fare = basic.calculate().add(express.calculate());
        return fare;
    }
}
