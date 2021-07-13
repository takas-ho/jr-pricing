Imports System.Runtime.CompilerServices
Imports Jr.Pricing.Model.Bill

Namespace Model.Specification
    ''' <summary>
    ''' 列車種類
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum TrainType
        のぞみ
        ひかり
    End Enum
    Public Module TrainTypeExtension
        Private ReadOnly ひかりSurcharge As New Amount(0)
        ''' <summary>
        ''' 振る舞いを返す
        ''' </summary>
        ''' <param name="aType">区分</param>
        ''' <returns>振る舞い</returns>
        ''' <remarks></remarks>
        <Extension()> Public Function Calculate(aType As TrainType, additionalSurcharge As Amount) As Amount
            Return If(aType = TrainType.ひかり, ひかりSurcharge, additionalSurcharge)
        End Function

        <Extension()> Public Function ValueOf(aType As TrainType, additionalSurcharge As Amount) As TrainType
            Return If(ひかりSurcharge.Equals(additionalSurcharge), TrainType.ひかり, TrainType.のぞみ)
        End Function
    End Module
End Namespace