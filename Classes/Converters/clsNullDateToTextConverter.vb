Public Class clsNullDateToTextConverter
    Implements System.Windows.Data.IValueConverter

    'CLASS clsNullDateToTextConverter

    '   This class contains the necessary methods to bind Null Dates to the property Text from the controls.
    '   Without this converter, the value of the property Text will be automatically '01/01/0001'

    ''' <summary>
    ''' Method that converts a null Date value into an empty String.
    ''' </summary>
    ''' <param name="value">An Object</param>
    ''' <param name="targetType">An object Type</param>
    ''' <param name="parameter">An Object</param>
    ''' <param name="culture">An object Globalization.CultureInfo</param>
    ''' <returns>Returns an empty String if the Date has a null value.</returns>
    ''' <remarks></remarks>
    Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As Globalization.CultureInfo) As Object Implements IValueConverter.Convert
        Dim result As String
        Dim dateToConvert As Date = CType(value, Date)

        If dateToConvert = Nothing Then
            result = "In Progress"
        Else
            result = dateToConvert
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
