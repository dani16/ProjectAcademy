Public Class ctrlLanguageSettingsTab
    ''' <summary>
    ''' Method that 
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub ctrlLanguageSettingsTab_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        txtLanguageSelected.Text = ""
    End Sub

    ''' <summary>
    ''' Method that changes the language of the application to Spanish
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub btnSpanishLanguage_Click(sender As Object, e As RoutedEventArgs) Handles btnSpanishLanguage.Click
        Application.SelectCulture("es-ES")
        txtLanguageSelected.Text = CType(FindResource("label.language_change"), String) & " " & CType(FindResource("label.language_spanish"), String)
    End Sub

    ''' <summary>
    ''' Method that changes the language of the application to English
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub btnEnglishLanguage_Click(sender As Object, e As RoutedEventArgs) Handles btnEnglishLanguage.Click
        Application.SelectCulture("en-US")
        txtLanguageSelected.Text = CType(FindResource("label.language_change"), String) & " " & CType(FindResource("label.language_english"), String)
    End Sub
End Class
