Public Class ctrlUsersSettings
    Dim _listUserDataContext As List(Of clsUserDataContext)

    ''' <summary>
    ''' Method that get all the User from the database when the control is loaded
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub ctrlUsersSettingsTab_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        'Get all the Users from the database
        _listUserDataContext = Application.oUserDataContextManager.getAllUserDataContexts()
        lstBxUsers.ItemsSource = _listUserDataContext
    End Sub

    ''' <summary>
    ''' Method that opens a new dlgNewUser Window
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub btnNewUser_Click(sender As Object, e As RoutedEventArgs) Handles btnNewUser.Click
        Dim isNewUserWindowOpen As Boolean = False

        'If there is no dlgNewUser Window, create and show a new one.
        If Not isNewUserWindowOpen Then
            Dim newUserDialog As New dlgNewUser()
            newUserDialog.ShowDialog()
        End If

        'Refresh list in case of there are new students
        refreshUserList()
    End Sub

    ''' <summary>
    ''' Method that refresh the User ListBox
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub refreshUserList()
        _listUserDataContext = Application.oUserDataContextManager.getAllUserDataContexts
        lstBxUsers.ItemsSource = _listUserDataContext
    End Sub

    ''' <summary>
    ''' Method that is executed when the user press a button from the
    ''' Users List
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub lstBxUsers_Click(sender As Object, e As RoutedEventArgs)
        Dim numReg As Integer
        Dim originalSource As Control = e.OriginalSource
        Dim listBoxItem As ListBoxItem = originalSource.TemplatedParent
        Dim button As Button = CType(e.OriginalSource, Button)
        Dim userDataContext As clsUserDataContext = CType(listBoxItem.DataContext, clsUserDataContext)
        Dim messageBoxResult As MessageBoxResult
        Dim messageBoxYesNo As New msgBxYesNo()


        'If the press the button Remove User
        If button.Name.Equals("btnDeleteUser") Then
            messageBoxYesNo = New msgBxYesNo(CType(FindResource("message_box.remove_user"), String), CType(FindResource("message_box.remove_user_question"), String) & "  " & userDataContext.User.Username & "?")
            messageBoxResult = messageBoxYesNo.ShowMessageBox()

            'Delete User
            If messageBoxResult = messageBoxResult.Yes Then
                numReg = Application.oUserManager.deleteUser(userDataContext.User.UserID)
            End If
        ElseIf button.Name.Equals("btnSetAdmin") Then
            messageBoxYesNo = New msgBxYesNo(CType(FindResource("message_box.set_admin"), String), CType(FindResource("message_box.set_admin_question"), String) & "  " & userDataContext.User.Username & "?")
            messageBoxResult = messageBoxYesNo.ShowMessageBox()

            'Set Administrator
            If messageBoxResult = messageBoxResult.Yes Then
                numReg = Application.oUserManager.setAdministrator(userDataContext.User.UserID)
            End If
        ElseIf button.Name.Equals("btnUnsetAdmin") Then
            messageBoxYesNo = New msgBxYesNo(CType(FindResource("message_box.unset_admin"), String), CType(FindResource("message_box.unset_admin_question"), String) & "  " & userDataContext.User.Username & "?")
            messageBoxResult = messageBoxYesNo.ShowMessageBox()

            'Set Administrator
            If messageBoxResult = messageBoxResult.Yes Then
                numReg = Application.oUserManager.unsetAdministrator(userDataContext.User.UserID)
            End If
        End If

        'Refresh list Students
        refreshUserList()
    End Sub
End Class
