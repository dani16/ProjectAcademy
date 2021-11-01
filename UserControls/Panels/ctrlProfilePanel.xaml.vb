Public Class ctrlProfilePanel
    ''' <summary>
    ''' Method that logs out a user from the application
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">an object RoutedEventArgs</param>
    ''' <remarks>The current application Window will be closed, and the login Window will appear.</remarks>
    Private Sub btnSignOut_Click(sender As Object, e As RoutedEventArgs)
        'Ask User if he really wants to logout
        Dim messageBoxResult As MessageBoxResult
        Dim messageBoxYesNo As New msgBxYesNo(CType(FindResource("message_box.logout"), String), CType(FindResource("message_box.logout_question"), String))
        messageBoxResult = messageBoxYesNo.ShowMessageBox()

        If messageBoxResult = messageBoxResult.Yes Then
            'Change Window
            Dim loginWindow As New LoginWindow()
            Dim parentWindow As Window = Window.GetWindow(Me)
            parentWindow.Close()
            loginWindow.Show()
        End If
    End Sub
End Class
