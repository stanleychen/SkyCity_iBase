Imports System.Text
Public Class DataHelper

#Region "AppendUnmatchColumn"
    Public Shared Sub AppendUnmatchColumn(ByRef builder As StringBuilder, ByVal caption As String, ByVal value As String)
        If Not String.IsNullOrEmpty(value) AndAlso Not String.IsNullOrEmpty(value.Trim()) AndAlso value.Trim() <> "0" Then
            builder.Append(caption + ": " + value)
            builder.Append(System.Environment.NewLine)
        End If
    End Sub
    Public Shared Sub AppendUnmatchColumn(ByRef builder As StringBuilder, ByVal caption As String, ByVal value As Decimal)
        builder.Append(caption + ": " + value.ToString())
        builder.Append(System.Environment.NewLine)
    End Sub
#End Region

#Region "GetTime"
    Public Shared Function GetTime(ByVal value As Object) As String
        Dim tempString As String

        If value Is Nothing Then
            tempString = ""
        Else
            Dim tempDate As System.DateTime
            tempDate = CDate(value)
            tempString = tempDate.ToShortTimeString()
        End If

        Return tempString
    End Function
#End Region

#Region "GetString"
    Public Shared Function GetString(ByVal bytes As Byte()) As String
        Return System.Text.Encoding.Default.GetString(bytes)
    End Function

    Public Shared Function GetString(ByVal value As Decimal, ByVal length As Integer) As String
        Dim format As String = String.Empty
        For i As Integer = 0 To length
            format += "0"
        Next
        Return value.ToString(format)
    End Function
#End Region

End Class

