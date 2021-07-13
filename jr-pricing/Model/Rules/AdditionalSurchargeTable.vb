Imports Jr.Pricing.Model.Specification

Namespace Model.Rules
    ''' <summary>
    ''' のぞみ割り増し特急券テーブル
    ''' </summary>
    ''' <remarks></remarks>
    Public Class AdditionalSurchargeTable

        Private ReadOnly map As New Dictionary(Of Destination, Integer) From {
            {Destination.新大阪, 320},
            {Destination.姫路, 530}
        }

        Public Function GetAdditionalSurcharge(destination As Destination) As Integer
            Return map(destination)
        End Function

    End Class
End Namespace
