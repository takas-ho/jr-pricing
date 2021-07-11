package example.application.service;

import example.domain.model.attempt.Attempt;
import example.domain.model.specification.*;

class AttemptFactory {

    static Attempt 大人1_通常期_新大阪_指定席_ひかり_片道() {
        return new Attempt(
                1, 0,
                new DepartureDate("2019-12-24"),
                Destination.新大阪,
                SeatType.指定席,
                TrainType.ひかり,
                TicketType.片道
        );
    }

    static Attempt 大人1_通常期_新大阪_自由席_ひかり_片道() {
        return new Attempt(
                1, 0,
                new DepartureDate("2019-12-24"),
                Destination.新大阪,
                SeatType.自由席,
                TrainType.ひかり,
                TicketType.片道
        );
    }

    public static Attempt 大人1_通常期_新大阪_指定席_のぞみ_片道() {
        return new Attempt(
                1, 0,
                new DepartureDate("2019-12-24"),
                Destination.新大阪,
                SeatType.指定席,
                TrainType.のぞみ,
                TicketType.片道
        );
    }

    public static Attempt 大人1_通常期_姫路_自由席_のぞみ_片道() {
        return new Attempt(
                1, 0,
                new DepartureDate("2019-12-24"),
                Destination.姫路,
                SeatType.自由席,
                TrainType.のぞみ,
                TicketType.片道
        );
    }

    public static Attempt 子供1_通常期_新大阪_指定席_ひかり_片道() {
        return new Attempt(
                0, 1,
                new DepartureDate("2019-12-24"),
                Destination.新大阪,
                SeatType.指定席,
                TrainType.ひかり,
                TicketType.片道
        );
    }
}
