Public Class dlgNewGroup
    ''' <summary>
    ''' Method that set the necessary elements when the window is loaded
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub dlgNewGroup_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Dim curApp As Application = Application.Current
        Dim mainWindow As Window = curApp.MainWindow

        'Set position of dialog on the center of the screen
        Me.Left = mainWindow.Left + (mainWindow.Width - Me.ActualWidth) / 2
        Me.Top = mainWindow.Top + (mainWindow.Height - Me.ActualHeight) / 2
    End Sub
End Class
