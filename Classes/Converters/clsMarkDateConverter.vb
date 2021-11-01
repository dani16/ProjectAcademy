Public Class clsMarkDateConverter
    Implements System.Windows.Data.IValueConverter

    'CLASS clsMarkDateConverter
    '   This class contains the necessary methods to bind the date of a Mark
    '   to format that displays the Term and the Year.
    ''' <summary>
    ''' Method that converts that allows to get the name of a group using its ID
    ''' </summary>
    ''' <param name="value">A list of Objects</param>
    ''' <param name="targetType">An object Type</param>
    ''' <param name="parameter">An Object</param>
    ''' <param name="culture">An object Globalization.CultureInfo</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As Globalization.CultureInfo) As Object Implements IValueConverter.Convert
        Dim result As String = Nothing
        Dim mark As New clsMark
        mark.DateMark = value

        Select Case (mark.getTerm())
            Case 0
                result = CType(Application.Current.FindResource("calendar.first_term"), String)
            Case 1
                result = CType(Application.Current.FindResource("calendar.second_term"), String)
            Case 2
                result = CType(Application.Current.FindResource("calendar.third_term"), String)
            Case 3
                result = CType(Application.Current.FindResource("calendar.summer"), String)
        End Select

        Return result & " (" & mark.DateMark.Year & ")"
    End Function

    ''' <summary>
    ''' Not implemented method
    ''' </summary>
    ''' <param name="value">An Object</param>
    ''' <param name="targetType">An object Type</param>
    ''' <param name="parameter">An Object</param>
    ''' <param name="culture">An object Globalization.CultureInfo</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
        Throw New NotImplementedException()
    End Function
End Class
