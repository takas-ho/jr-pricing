package example.domain.model.specification;

import example.domain.model.bill.Amount;

/**
 * 列車種類
 */
public enum TrainType {
    のぞみ(true),
    ひかり(false);

    private boolean isApply;
    private TrainType(boolean isApply) {
        this.isApply = isApply;
    }

    public Amount apply(Amount surcharge) {
        return isApply ? surcharge : new Amount(0);
    }

    public static TrainType valueOf(Amount surcharge) {
        return surcharge.equals(new Amount(0)) ? ひかり : のぞみ;
    }
}
