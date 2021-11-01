Public Class clsConfigurationVisibilityConverter
    Implements System.Windows.Data.IMultiValueConverter

    'CLASS clsConfigurationVisibleOrHiddenConverter
    '   This class contains the necessary methods to bind the boolean variables from the clsConfiguration and clsUser classes
    '   with the property visible of the controls of the Application.
    '   It is a MultiValueConverter that checks:
    '       1-If the User is an Administrator, in which case, the control will always be shown
    '       2-If the control is available for the user, which means that if the permission is activated on the Configuration Settings

    ''' <summary>
    ''' Method that converts the boolean variables from the Configuration class to the property Visibility
    ''' </summary>
    ''' <param name="values">A list of Objects</param>
    ''' <param name="targetType">An object Type</param>
    ''' <param name="parameter">An Object</param>
    ''' <param name="culture">An object Globalization.CultureInfo</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Convert(values() As Object, targetType As Type, parameter As Object, culture As Globalization.CultureInfo) As Object Implements IMultiValueConverter.Convert
        Dim result As Object

        If values.Length <= 1 Then
            'Admin Controls, only Administrator will see this controls
            If Not parameter Is Nothing Then
                If values(0) And parameter.Equals("Admin") Then
                    result = Visibility.Visible
                Else
                    result = Visibility.Collapsed
                End If
            Else
                'Login Window, we only ask for permissions (Admin won't see unavailable controls)
                If Not values(0) Then
                    result = Visibility.Collapsed
                Else
                    result = Visibility.Visible
                End If
            End If
        Else
            'Users Control, users will see if the permissions is set to True
            If Not values(0) And Not values(1) Then
                result = Visibility.Collapsed
            Else
                result = Visibility.Visible
            End If
        End If

        Return result
    End Function

    ''' <summary>
    ''' Not implemented method
    ''' </summary>
    ''' <param name="value">An Object</param>
    ''' <param name="targetTypes">An object Type</param>
    ''' <param name="parameter">An Object</param>
    ''' <param name="culture">An object Globalization.CultureInfo</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function ConvertBack(value As Object, targetTypes() As Type, parameter As Object, culture As Globalization.CultureInfo) As Object() Implements IMultiValueConverter.ConvertBack
        Throw New NotImplementedException()
    End Function
End Class
