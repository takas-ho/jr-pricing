Imports System.Runtime.CompilerServices
Imports Jr.Pricing.Model.Bill

Namespace Model.Specification
    ''' <summary>
    ''' 座席区分
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SeatType
        指定席
        自由席
    End Enum
    Public Module SeatTypeExtension
        Private ReadOnly additionalFareByType As New Dictionary(Of SeatType, Func(Of DepartureDate, Amount)) _
            From {{SeatType.指定席, Function(aDate)
                                     Dim year As Integer = aDate.GetYear()
                                     If New DepartureDateRange(New DepartureDate(year, 12, 25), New DepartureDate(year, 12, 31)).Contains(aDate) _
                                        OrElse New DepartureDateRange(New DepartureDate(year, 1, 1), New DepartureDate(year, 1, 10)).Contains(aDate) Then
                                         Return New Amount(200)
                                     ElseIf New DepartureDateRange(New DepartureDate(year, 1, 16), New DepartureDate(year, 1, 30)).Contains(aDate) Then
                                         Return New Amount(-200)
                                     End If
                                     Return New Amount(0)
                                 End Function},
                  {SeatType.自由席, Function(aDate) New Amount(-530)}}
        ''' <summary>
        ''' 振る舞いを返す
        ''' </summary>
        ''' <param name="aType">区分</param>
        ''' <returns>振る舞い</returns>
        ''' <remarks></remarks>
        <Extension()> Public Function Calculate(aType As SeatType, amount As Amount, departureDate As DepartureDate) As Amount
            Return amount.Add(additionalFareByType(aType).Invoke(departureDate))
        End Function
    End Module
End Namespace