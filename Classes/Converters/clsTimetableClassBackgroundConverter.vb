Public Class clsTimetableClassBackgroundConverter
    Implements System.Windows.Data.IValueConverter

    'CLASS clsTimetableClassItemBackground

    '   This class contains the necessary methods to bind the Group Level with
    '   the property Background from the Border of the control ctrlTimetableClass.

    ''' <summary>
    ''' Method that converts the Background property from the Border from the control ctrlTimetableClass with an object clsTimetable.
    ''' </summary>
    ''' <param name="value">An Object</param>
    ''' <param name="targetType">An object Type</param>
    ''' <param name="parameter">An Object</param>
    ''' <param name="culture">An object Globalization.CultureInfo</param>
    ''' <returns>Returns a String with the information we are asking for.</returns>
    ''' <remarks></remarks>
    Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As Globalization.CultureInfo) As Object Implements IValueConverter.Convert
        Dim result As Brush = Nothing
        Dim group As clsGroup = Application.oGroupManager.getGroup(CType(value, Integer))

        If Not group Is Nothing Then
            Select Case group.EnglishLevel
                Case "Infants"
                    result = Brushes.LightBlue
                Case "Primary"
                    result = Brushes.GreenYellow
                Case "Secondary"
                    result = Brushes.Pink
                Case "A1"
                    result = Brushes.Red
                Case "A2"
                    result = Brushes.YellowGreen
                Case "B1"
                    result = Brushes.CornflowerBlue
                Case "B2"
                    result = Brushes.Goldenrod
                Case "C1"
                    result = Brushes.MediumOrchid
                Case "C2"
                    result = Brushes.Yellow
                Case Else
                    result = Brushes.Transparent
            End Select
        End If

        Return result
    End Function

    ''' <summary>
    ''' Not implemented method.
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

