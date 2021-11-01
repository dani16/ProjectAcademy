Public Class clsGroupNameConverter
    Implements System.Windows.Data.IValueConverter

    'CLASS clsGroupNameConverter
    '   This class contains the necessary methods to bind the ID of a group
    '   to a name of a group.
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
        Dim result As String
        Dim group As clsGroup = Application.oGroupManager.getGroup(value)

        result = group.EnglishLevel & " " & group.Description

        Return result
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
