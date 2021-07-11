package example.domain.model.specification;

import example.domain.model.bill.Amount;

/**
 * 大人・小人区分
 */
public enum AdultType {
    大人(false),
    小人(true);

    private boolean discountsHalf;
    private AdultType(boolean discountsHalf) {
        this.discountsHalf = discountsHalf;
    }
    public Amount calculate(Amount amount) {
        return discountsHalf ? amount.calcurateToHalf() : amount;
    }
}
