Public Class ctrlLogin
    ''' <summary>
    ''' Methods that lets a user login into the Application
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub btnLogin_Click(sender As Object, e As RoutedEventArgs)
        lblError.Visibility = Windows.Visibility.Collapsed
        Dim user As New clsUser
        Dim teacher As New clsTeacher
        Dim userDataContext As New clsUserDataContext
        Dim configuration As New clsConfiguration
        Dim preferences As New clsPreferences
        Dim configurationDataContext As New clsConfigurationDataContext

        'Valid user
        If Application.oUserManager.validateUser(txtInsertName.Text, txtInsertPass.Password) Then
            userDataContext = Application.oUserDataContextManager.getUser(txtInsertName.Text)
            configuration = Application.oConfigurationManager.getConfiguration()
            configurationDataContext = New clsConfigurationDataContext(userDataContext, configuration)

            'Change Window
            Dim mainWindow As New MainWindow()
            Dim parentWindow As Window = Window.GetWindow(Me)
            mainWindow.Show()
            mainWindow.DataContext = configurationDataContext
            parentWindow.Close()
        Else
            'Invalid User
            lblError.Visibility = Windows.Visibility.Visible
        End If
    End Sub
End Class
