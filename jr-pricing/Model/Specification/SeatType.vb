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
        Private ReadOnly additionalFareByType As New Dictionary(Of SeatType, Amount) _
            From {{SeatType.指定席, New Amount(0)},
                  {SeatType.自由席, New Amount(-530)}}
        ''' <summary>
        ''' 振る舞いを返す
        ''' </summary>
        ''' <param name="aType">区分</param>
        ''' <returns>振る舞い</returns>
        ''' <remarks></remarks>
        <Extension()> Public Function Calculate(aType As SeatType, amount As Amount) As Amount
            Return amount.Add(additionalFareByType(aType))
        End Function
    End Module
End Namespace