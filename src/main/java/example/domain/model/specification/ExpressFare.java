package example.domain.model.specification;

import java.util.Objects;

import example.domain.model.bill.Amount;

/**
 * 特急料金
 */
public class ExpressFare {

    Destination destination;
    SeatType seatType;
    TrainType trainType;
    Amount fare;
    Amount additionalFare;

    public ExpressFare(Destination destination, SeatType seatType, Amount fare, Amount additionalFare) {
        this.destination = destination;
        this.seatType = seatType;
        this.fare = fare;
        this.additionalFare = additionalFare;
        this.trainType = TrainType.valueOf(additionalFare);
    }

    public Amount calculate() {
        return this.seatType.calculate(this.fare.add(additionalFare));
    }

    @Override
    public String toString() {
        return this.destination + "行き " + seatType.toString() + " " + trainType.toString() + " 料金" + fare.toString() + "";
    }

    @Override
    public boolean equals(Object other) {
        return isEqual((ExpressFare) other);
    }

    private boolean isEqual(ExpressFare otherFare) {
        return this.destination == otherFare.destination && this.seatType == otherFare.seatType && this.fare.equals(otherFare.fare);
    }

    @Override
    public int hashCode() {
        return Objects.hash(destination) + Objects.hash(seatType);
    }
}
