package example.domain.model.specification;

import example.domain.model.bill.Amount;

/**
 * 特急券
 */
public class ExpressTicket {

    ExpressFare expressFare;
    AdultType adultType;
    DepartureDate date;
    
    public ExpressTicket(ExpressFare expressFare, AdultType adultType, DepartureDate date) {
        this.expressFare = expressFare;
        this.adultType = adultType;
        this.date = date;
    }

    public Amount calculate() {
        return this.adultType.calculate(this.expressFare.calculate());
    }

    @Override
    public String toString() {
        return this.expressFare.destination + "行き " + adultType.toString() + " 料金" + calculate().toString() + "";
    }

}
