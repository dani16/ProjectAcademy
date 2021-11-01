Public Class clsNullDateToDatePickerConverter
    Implements System.Windows.Data.IValueConverter

    'CLASS clsNullDateToDatePickerConverter

    '   This class contains the necessary methods to bind Null Dates to the property SelectedDate from the DatePicker controls.
    '   Without this converter, the value of the property SelectedDate will be automatically '01/01/0001'

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
            result = String.Empty
        Else
            result = dateToConvert
        End If

        Return result
    End Function

    ''' <summary>
    ''' Methods that converts back the value the DatePicker from the above Convert method
    ''' </summary>
    ''' <param name="value">An Object</param>
    ''' <param name="targetType">An object Type</param>
    ''' <param name="parameter">An Object</param>
    ''' <param name="culture">An object Globalization.CultureInfo</param>
    ''' <returns>Returns 'M' if the user select the Male radioButton of 'F' if the Female one is selected</returns>
    ''' <remarks></remarks>
    Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
        Dim result As String = ""
        Dim dateToConvert As Date
        Dim valueString As String = CType(value, String)

        Try
            If valueString.Length = 10 Then
                dateToConvert = CType(value, Date)

                If CType(value, Date) = Nothing Then
                    result = ""
                Else
                    result = value
                End If
            Else
                result = value
            End If
        Catch ex As Exception
        End Try

        Return result
    End Function
End Class
