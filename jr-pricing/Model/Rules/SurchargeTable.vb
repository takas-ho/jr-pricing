Imports Jr.Pricing.Model.Specification

Namespace Model.Rules
    ''' <summary>
    ''' ひかり 指定席特急券テーブル
    ''' </summary>
    ''' <remarks></remarks>
    Public Class SurchargeTable

        Private ReadOnly map As New Dictionary(Of Destination, Integer) From {
            {Destination.新大阪, 5490},
            {Destination.姫路, 5920}
        }

        Public Function GetSurcharge(destination As Destination) As Integer
            Return map(destination)
        End Function

    End Class
End Namespace