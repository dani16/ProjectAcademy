Public Class dlgNewUser
    Dim _listTeacher As List(Of clsTeacher)
    Dim _teacher As clsTeacher

    ''' <summary>
    ''' Method that loads the teachers into the listbox
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub dlgNewUser_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        _listTeacher = Application.oTeacherManager.getNotUserTeachers()
        lstBxTeachers.ItemsSource = _listTeacher
    End Sub

    ''' <summary>
    ''' Method that search for Teacher when the user write on the searcher textBox
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub txtBxSearchTeacher_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtBxSearchTeacher.TextChanged
        txtMessageSearch.Visibility = Windows.Visibility.Hidden

        If txtBxSearchTeacher.Text.Length >= 3 Then
            'Get teacher from the database that have not an user account
            _listTeacher = Application.oTeacherManager.getNotUserTeachers(txtBxSearchTeacher.Text)
            lstBxTeachers.ItemsSource = _listTeacher

            'Show message: Not results found
            If _listTeacher.Count = 0 Then
                txtMessageSearch.Visibility = Windows.Visibility.Visible
            End If
        Else
            lstBxTeachers.ItemsSource = Application.oTeacherManager.getNotUserTeachers()
        End If
    End Sub

    ''' <summary>
    ''' Method that load the student selected on the grid
    '''  from the listBox
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub lstBxStudents_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles lstBxTeachers.SelectionChanged
        'When you change the tab and you return to the Student tab the Selected Index is -1.
        If Me.lstBxTeachers.SelectedIndex >= 0 Then
            _teacher = _listTeacher.Item(Me.lstBxTeachers.SelectedIndex)
        End If

        Me.gridNewTeacher.DataContext = _teacher
    End Sub

    ''' <summary>
    ''' Method that creates a new User
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub btnCreateNewStudent_Click(sender As Object, e As RoutedEventArgs) Handles btnCreateNewUser.Click
        Dim numReg As Integer
        Dim user As New clsUser
        Dim level As New clsEnglishLevel

        'Create new user
        With user
            .Username = _teacher.DNI
            .TeacherID = _teacher.TeacherID
        End With

        'Insert user        
        numReg = Application.oUserManager.insertUser(user)

        If numReg > 0 Then
            'Close Window
            Dim parentWindow As Window = Window.GetWindow(Me)
            parentWindow.Close()

            'Show messageBox confirm
            Dim messageBoxResult As MessageBoxResult
            Dim messageBoxOk As New msgBxOk(CType(FindResource("message_box.create_user"), String), CType(FindResource("message_box.create_user_success"), String))
            messageBoxResult = messageBoxOk.ShowMessageBox()
        Else
            'Show messageBox Error
            Dim messageBoxResult2 As MessageBoxResult
            Dim messageBoxOk2 As New msgBxOk(CType(FindResource("message_box.error_title"), String), CType(FindResource("message_box.error_message"), String))
            messageBoxResult2 = messageBoxOk2.ShowMessageBox()
        End If
    End Sub

    ''' <summary>
    ''' Method that cancels the creation of a new USer
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub btnCancelNewStudent_Click(sender As Object, e As RoutedEventArgs) Handles btnCancelNewUser.Click
        'Close Window
        Dim parentWindow As Window = Window.GetWindow(Me)
        parentWindow.Close()
    End Sub
End Class
