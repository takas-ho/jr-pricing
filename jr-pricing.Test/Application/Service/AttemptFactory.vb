Imports Jr.Pricing.Model
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

    End Class
End Namespace