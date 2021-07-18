Imports Jr.Pricing.Model.Specification

Namespace Model.Rules
    ''' <summary>
    ''' 運賃テーブル
    ''' </summary>
    ''' <remarks></remarks>
    Public Class FareTable

        Private ReadOnly map As New Dictionary(Of Destination, Integer) From {
            {Destination.新大阪, 8910},
            {Destination.姫路, 10020}
        }

        Public Function GetFare(destination As Destination) As Integer
            Return map(destination)
        End Function

    End Class
End Namespace