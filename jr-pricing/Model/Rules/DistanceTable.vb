Imports Jr.Pricing.Model.Specification

Namespace Model.Rules
    ''' <summary>
    ''' 営業キロテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Public Class DistanceTable

        Private ReadOnly map As New Dictionary(Of Destination, Integer) From {
            {Destination.新大阪, 553},
            {Destination.姫路, 644}
        }

        Public Function GetDistance(destination As Destination) As Integer
            Return map(destination)
        End Function

    End Class
End Namespace