package example.domain.model.specification;

import example.domain.model.bill.Amount;

/**
 * 乗車券
 */
public class BasicTicket {

    BasicFare basicFare;
    AdultType adultType;
    DepartureDate date;
    
    public BasicTicket(BasicFare basicFare, AdultType adultType, DepartureDate date) {
        this.basicFare = basicFare;
        this.adultType = adultType;
        this.date = date;
    }

    public Amount calculate() {
        return this.adultType.calculate(this.basicFare.calculate());
    }

    @Override
    public String toString() {
        return this.basicFare.destination + "行き " + adultType.toString() + " 料金" + calculate().toString() + "";
    }

}
