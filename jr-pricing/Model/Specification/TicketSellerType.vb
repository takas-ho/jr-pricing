Imports System.Runtime.CompilerServices

Namespace Model.Specification
    ''' <summary>
    ''' 片道/往復
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum TicketSellerType
        片道
        往復
    End Enum
    Public Module TicketSellerTypeExtension
        Private ReadOnly additionalFareByType As New Dictionary(Of TicketSellerType, Func(Of Integer, Integer)) _
            From {{TicketSellerType.片道, Function(personCount) personCount},
                  {TicketSellerType.往復, Function(personCount) personCount * 2}}
        ''' <summary>
        ''' チケット枚数を算出する
        ''' </summary>
        ''' <param name="aType"></param>
        ''' <param name="personCount">人数</param>
        ''' <returns>枚数</returns>
        ''' <remarks></remarks>
        <Extension()> Public Function CalculateTickets(aType As TicketSellerType, personCount As Integer) As Integer
            Return additionalFareByType(aType)(personCount)
        End Function
    End Module
End Namespace