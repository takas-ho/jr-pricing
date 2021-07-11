package example.domain.model.specification;

import java.util.Objects;

import example.domain.model.bill.Amount;

/**
 * 基本運賃
 */
public class BasicFare {

    Destination destination;
    public final Amount fare;

    public BasicFare(Destination destination, Amount fare) {
        this.destination = destination;
        this.fare = fare;
    }

    public Amount calculate() {
        return this.fare;
    }

    @Override
    public String toString() {
        return this.destination + "行き 料金" + fare.toString() + "";
    }

    @Override
    public boolean equals(Object other) {
        return isEqual((BasicFare) other);
    }

    private boolean isEqual(BasicFare otherFare) {
        return this.destination == otherFare.destination && this.fare.equals(otherFare.fare);
    }

    @Override
    public int hashCode() {
        return Objects.hash(destination);
    }

}
