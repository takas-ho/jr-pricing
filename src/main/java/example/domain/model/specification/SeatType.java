package example.domain.model.specification;

import example.domain.model.bill.Amount;

/**
 * 座席区分
 */
public enum SeatType {
    指定席( 0),
    自由席( -530);

    private int additional;
    private SeatType(int additional) {
        this.additional = additional;
    }
    public Amount calculate(Amount amount) {
        return amount.add(new Amount(additional));
    }
}
