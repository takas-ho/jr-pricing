package example.domain.model.attempt;

import example.domain.model.specification.*;

/**
 * 購入希望
 */
public class Attempt {
    int adult;
    int child;

    DepartureDate departureDate;
    Destination destination;

    SeatType seatType;
    TrainType trainType;
    TicketType ticketType;

    public Attempt(int adult, int child, DepartureDate departureDate, Destination destination, SeatType seatType, TrainType trainType, TicketType ticketType) {
        this.adult = adult;
        this.child = child;
        this.departureDate = departureDate;
        this.destination = destination;
        this.seatType = seatType;
        this.trainType = trainType;
        this.ticketType = ticketType;
    }

    public Destination to() {
        return destination;
    }

    @Override
    public String toString() {
        return  "大人=" + adult + "人" +
                "\n子供=" + child + "人" +
                "\n出発日=" + departureDate +
                "\n目的地=" + destination +
                "\n座席区分=" + seatType +
                "\n列車種類=" + trainType +
                "\n片道/往復=" + ticketType
                ;
    }

    private BasicFare toBasicFare(int fare) {
        return new BasicFare(destination, new example.domain.model.bill.Amount(fare));
    }

    private ExpressFare toExpressFare(int fare, int additional) {
        return new ExpressFare(destination, seatType, new example.domain.model.bill.Amount(fare), 
                                trainType.apply(new example.domain.model.bill.Amount(additional)));
    }

    public BasicTicket toBasicTicket(int fare) {
        return new BasicTicket(toBasicFare(fare), 0 < adult ? AdultType.大人 : AdultType.小人, departureDate);
    }

    public ExpressTicket toExpressTicket(int fare, int additional) {
        return new ExpressTicket(toExpressFare(fare, additional), 0 < adult ? AdultType.大人 : AdultType.小人, departureDate);
    }
}