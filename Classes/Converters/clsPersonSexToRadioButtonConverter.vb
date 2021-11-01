Public Class clsPersonSexToRadioButtonConverter
    Implements System.Windows.Data.IValueConverter

    'CLASS clsPersonSexToRadioButtonConverter

    '   This class contains the necessary methods to bind the Char variable 'sex('M' or 'F')' from the clsPerson class with
    '   the property IsChecked of the radio Button control.

    ''' <summary>
    ''' Method that converts the Char property 'sex('M' or 'F')' from an object clsPerson to a boolean.
    ''' </summary>
    ''' <param name="value">An Object</param>
    ''' <param name="targetType">An object Type</param>
    ''' <param name="parameter">An Object</param>
    ''' <param name="culture">An object Globalization.CultureInfo</param>
    ''' <returns>Returns TRUE if the sex of the radioButton selected matches with the sex of the current User or the Application.</returns>
    ''' <remarks></remarks>
    Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As Globalization.CultureInfo) As Object Implements IValueConverter.Convert
        Dim result As Boolean = False

        If (parameter.Equals("Male") And value = "M") Or (parameter.Equals("Female") And value = "F") Then
            result = True
        End If

        Return result
    End Function

    ''' <summary>
    ''' Methods that converts back the value of the radio button from the above Convert method
    ''' </summary>
    ''' <param name="value">An Object</param>
    ''' <param name="targetType">An object Type</param>
    ''' <param name="parameter">An Object</param>
    ''' <param name="culture">An object Globalization.CultureInfo</param>
    ''' <returns>Returns 'M' if the user select the Male radioButton of 'F' if the Female one is selected</returns>
    ''' <remarks></remarks>
    Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
        Dim result As Char

        If parameter.Equals("Male") Then
            result = "M"
        Else
            result = "F"
        End If

        Return result
    End Function
End Class
