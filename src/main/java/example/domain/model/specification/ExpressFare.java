package example.domain.model.specification;

import java.util.Objects;

import example.domain.model.bill.Amount;

/**
 * 特急料金
 */
public class ExpressFare {

    Destination destination;
    SeatType seatType;
    Amount fare;

    public ExpressFare(Destination destination, SeatType seatType, Amount fare) {
        this.destination = destination;
        this.seatType = seatType;
        this.fare = fare;
    }

    public Amount calculate() {
        return this.seatType.calculate(this.fare);
    }

    @Override
    public String toString() {
        return this.destination + "行き " + seatType.toString() + " 料金" + fare.toString() + "";
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
