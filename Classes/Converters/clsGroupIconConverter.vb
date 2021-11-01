Public Class clsGroupIconConverter
    Implements System.Windows.Data.IValueConverter

    'CLASS clsGroupIconConverter
    '   This class contains the necessary methods to bind the variable source of images
    '   with the the differents group images depending on the different english level of the Application.

    ''' <summary>
    ''' Method that converts the Group type property to the Image.Source property
    ''' </summary>
    ''' <param name="value">A list of Objects</param>
    ''' <param name="targetType">An object Type</param>
    ''' <param name="parameter">An Object</param>
    ''' <param name="culture">An object Globalization.CultureInfo</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As Globalization.CultureInfo) As Object Implements IValueConverter.Convert
        Dim result As New Image

        Select Case value
            Case "Infants"
                result.Source = CType(Application.Current.FindResource("imgInfants"), ImageSource)
            Case "Primary"
                result.Source = CType(Application.Current.FindResource("imgPrimary"), ImageSource)
            Case "Secondary"
                result.Source = CType(Application.Current.FindResource("imgSecondary"), ImageSource)
            Case "A1"
                result.Source = CType(Application.Current.FindResource("imgA1"), ImageSource)
            Case "A2"
                result.Source = CType(Application.Current.FindResource("imgA2"), ImageSource)
            Case "B1"
                result.Source = CType(Application.Current.FindResource("imgB1"), ImageSource)
            Case "B2"
                result.Source = CType(Application.Current.FindResource("imgB2"), ImageSource)
            Case "C1"
                result.Source = CType(Application.Current.FindResource("imgC1"), ImageSource)
            Case "C2"
                result.Source = CType(Application.Current.FindResource("imgC2"), ImageSource)
            Case Else
                result.Source = CType(Application.Current.FindResource("imgDefault"), ImageSource)
        End Select

        Return result.Source
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
