Imports System.Runtime.CompilerServices
Imports Jr.Pricing.Model.Bill

Namespace Model.Specification
    ''' <summary>
    ''' 大人・子供区分
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum AdultType
        大人
        小人
    End Enum
    Public Module AdultTypeExtension
        Private ReadOnly map As New Dictionary(Of AdultType, Func(Of Amount, Amount)) From {
            {AdultType.大人, Function(fare) fare},
            {AdultType.小人, Function(fare) fare.CalculateHalf()}}
        ''' <summary>
        ''' 振る舞いを返す
        ''' </summary>
        ''' <param name="aType">区分</param>
        ''' <returns>振る舞い</returns>
        ''' <remarks></remarks>
        <Extension()> Public Function CalculateFare(aType As AdultType, fare As Amount) As Amount
            Return map(aType).Invoke(fare)
        End Function
    End Module

End Namespace