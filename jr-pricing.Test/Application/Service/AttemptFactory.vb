﻿Imports Jr.Pricing.Model
Imports Jr.Pricing.Model.Specification

Namespace Application.Service
    Public Class AttemptFactory

        Public Shared Function 大人1_通常期_新大阪_指定席_ひかり_片道() As Attempt
            Return New Attempt(adult:=1, child:=0,
                               departureDate:=New DepartureDate("2019/12/24"),
                               destination:=Destination.新大阪,
                               seatType:=SeatType.指定席,
                               trainType:=TrainType.ひかり,
                               ticketType:=TicketType.片道)
        End Function

        Public Shared Function 大人1_通常期_新大阪_自由席_ひかり_片道() As Attempt
            Return New Attempt(adult:=1, child:=0,
                               departureDate:=New DepartureDate("2019/12/24"),
                               destination:=Destination.新大阪,
                               seatType:=SeatType.自由席,
                               trainType:=TrainType.ひかり,
                               ticketType:=TicketType.片道)
        End Function

        Public Shared Function 大人1_通常期_新大阪_指定席_のぞみ_片道() As Attempt
            Return New Attempt(adult:=1, child:=0,
                               departureDate:=New DepartureDate("2019/12/24"),
                               destination:=Destination.新大阪,
                               seatType:=SeatType.指定席,
                               trainType:=TrainType.のぞみ,
                               ticketType:=TicketType.片道)
        End Function

        Public Shared Function 大人1_通常期_姫路_自由席_のぞみ_片道() As Attempt
            Return New Attempt(adult:=1, child:=0,
                               departureDate:=New DepartureDate("2019/12/24"),
                               destination:=Destination.姫路,
                               seatType:=SeatType.自由席,
                               trainType:=TrainType.のぞみ,
                               ticketType:=TicketType.片道)
        End Function

        Public Shared Function 小人1_通常期_新大阪_指定席_ひかり_片道() As Attempt
            Return New Attempt(adult:=0, child:=1,
                               departureDate:=New DepartureDate("2019/12/24"),
                               destination:=Destination.新大阪,
                               seatType:=SeatType.指定席,
                               trainType:=TrainType.ひかり,
                               ticketType:=TicketType.片道)
        End Function

        Public Shared Function 大人2_小人2_通常期_新大阪_指定席_ひかり_片道() As Attempt
            Return New Attempt(adult:=2, child:=2,
                               departureDate:=New DepartureDate("2019/12/24"),
                               destination:=Destination.新大阪,
                               seatType:=SeatType.指定席,
                               trainType:=TrainType.ひかり,
                               ticketType:=TicketType.片道)
        End Function
    End Class
End Namespace