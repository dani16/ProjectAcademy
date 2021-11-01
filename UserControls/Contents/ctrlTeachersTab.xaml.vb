Imports Microsoft.Win32

Public Class ctrlTeachersTab
    Dim _listTeachers As List(Of clsTeacher)
    Dim _listGroups As List(Of clsGroup)
    Dim _teacher As New clsTeacher

    ''' <summary>
    ''' Method that get all the Teachers from the database when the control is loaded
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub ctrlStudentsTab_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        'Default Status Buttons
        btnSaveChangesTeachers.IsEnabled = False
        btnEditTeacher.Visibility = Windows.Visibility.Visible
        btnCancelEditTeacher.Visibility = Windows.Visibility.Collapsed
        btnDeleteTeacher.IsEnabled = True

        If TypeOf (gridTeacherInformation.DataContext) Is clsStudent Then 'When the application is loaded the gridStudentInformation dataContext is an object clsConfigurationDataContext
            'Clear the grid from unsaved changes
            'Or In case of the dataContext it is set from another tab (Ex: View student on TabGroups) sets the Student selected from another tab
            _teacher = Application.oTeacherManager.getTeacher(CType(gridTeacherInformation.DataContext, clsTeacher).TeacherID)
        End If

        'Get all the Teachers from the database
        _listTeachers = Application.oTeacherManager.getAllTeachers()
        lstBxTeachers.ItemsSource = _listTeachers

        'Set First Teacher on the Grid
        If _listTeachers.Count > 0 Then
            'Me.gridTeacherInformation.DataContext = _listTeachers(0)
            _listGroups = Application.oGroupManager.getAllGroupsSearch(Nothing, _teacher.TeacherID)
            lstBxGroups.ItemsSource = _listGroups
        Else
            Me.gridTeacherInformation.IsEnabled = False
            btnEditTeacher.IsEnabled = False
            btnDeleteTeacher.IsEnabled = False
        End If

        'Set DataPicker DisplayDateEnd
        txtBirthDate.DisplayDateEnd = Date.Today
    End Sub

    ''' <summary>
    ''' Method that  when a Teacher is selected from the listBox
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub lstBxTeachers_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles lstBxTeachers.SelectionChanged
        'When you change the tab and you return to the Student tab the Selected Index is -1.
        If Me.lstBxTeachers.SelectedIndex >= 0 Then
            _teacher = _listTeachers.Item(Me.lstBxTeachers.SelectedIndex)

            'Load Groups
            _listGroups = Application.oGroupManager.getAllGroupsSearch(Nothing, _teacher.TeacherID)
            lstBxGroups.ItemsSource = _listGroups
        End If

        btnSaveChangesTeachers.IsEnabled = False
        btnEditTeacher.Visibility = Windows.Visibility.Visible
        btnCancelEditTeacher.Visibility = Windows.Visibility.Collapsed
        btnDeleteTeacher.IsEnabled = True

        Me.gridTeacherInformation.DataContext = _teacher
    End Sub

    ''' <summary>
    ''' Method that search for an specific Teacher when the user write on the searcher textBox
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub txtBxSearchTeacher_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtBxSearchTeacher.TextChanged
        txtMessageSearch.Visibility = Windows.Visibility.Hidden

        'Starts searching the the use introduces at least 3 characters
        If txtBxSearchTeacher.Text.Length >= 3 Then
            _listTeachers = Application.oTeacherManager.getAllTeachers(txtBxSearchTeacher.Text)

            'Show message: Not results found
            If _listTeachers.Count = 0 Then
                txtMessageSearch.Visibility = Windows.Visibility.Visible
            End If
        Else
            _listTeachers = Application.oTeacherManager.getAllTeachers()
        End If

        lstBxTeachers.ItemsSource = _listTeachers
    End Sub

    ''' <summary>
    ''' Method that opens a new dlgNewTeacher Window
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub btnNewTeacher_Click(sender As Object, e As RoutedEventArgs) Handles btnNewTeacher.Click
        Dim newTeacherDialog As New dlgNewTeacher()
        newTeacherDialog.ShowDialog()

        'Refresh list in case of there are new teachers
        refreshTeacherList()
    End Sub

    ''' <summary>
    ''' Method that changes the image of an Teacher
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub btnChangeImageTeacher_Click(sender As Object, e As RoutedEventArgs) Handles btnChangeImageTeacher.Click
        Dim openFileDialog As New OpenFileDialog()
        Dim numReg As New Integer

        'The user search on his computer for a new Photo
        openFileDialog.ShowDialog()
        openFileDialog.Title = CType(FindResource("dialog.photo_title"), String)
        'openFileDialog.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files(*.png)|*.png|JPG"
        openFileDialog.DefaultExt = ".jpeg"

        'Set New Photo to the Teacher Object
        If Not openFileDialog.FileName Is "" Then
            imgTeacherPhoto.Source = New BitmapImage(New Uri(openFileDialog.FileName))
        End If
    End Sub

    ''' <summary>
    ''' Method that deletes the photo of a Teacher
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub btnDeleteImageStudent_Click(sender As Object, e As RoutedEventArgs) Handles btnDeleteImageTeacher.Click
        _teacher.Photo = Nothing
        imgTeacherPhoto.Source = Nothing
    End Sub

    ''' <summary>
    ''' Method that deletes a Teacher
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub btnDeleteTeacher_Click(sender As Object, e As RoutedEventArgs) Handles btnDeleteTeacher.Click
        Dim numReg As Integer
        Dim messageBoxResult As MessageBoxResult
        Dim messageBoxYesNo As New msgBxYesNo(CType(FindResource("message_box.delete_teacher"), String), CType(FindResource("message_box.delete_teacher_question"), String) & " " & txtName.Text & " " & txtSurname.Text & "?")
        messageBoxResult = messageBoxYesNo.ShowMessageBox()

        If messageBoxResult = messageBoxResult.Yes Then
            numReg = Application.oTeacherManager.deleteTeacher(txtTeacherID.Text)

            'Refresh Students
            refreshTeacherList()

            'Clear Grid 
            If _listTeachers.Count > 0 Then
                Me.gridTeacherInformation.DataContext = _listTeachers(0)
            Else
                Me.gridTeacherInformation.DataContext = Nothing
            End If

            'Show messageBox confirm
            Dim messageBoxOk As New msgBxOk(CType(FindResource("message_box.delete_teacher"), String), CType(FindResource("message_box.delete_teacher_success"), String))
            messageBoxResult = messageBoxOk.ShowMessageBox()
        End If
    End Sub

    ''' <summary>
    ''' Method that allow a user to edit the information of a Teacher
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub btnEditTeacher_Click(sender As Object, e As RoutedEventArgs) Handles btnEditTeacher.Click
        btnSaveChangesTeachers.IsEnabled = True
        btnEditTeacher.Visibility = Windows.Visibility.Collapsed
        btnCancelEditTeacher.Visibility = Windows.Visibility.Visible
        btnDeleteTeacher.IsEnabled = False
    End Sub

    ''' <summary>
    ''' Method that cancels the edition of a student
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub btnCancelEditStudent_Click(sender As Object, e As RoutedEventArgs) Handles btnCancelEditTeacher.Click
        btnSaveChangesTeachers.IsEnabled = False
        btnEditTeacher.Visibility = Windows.Visibility.Visible
        btnCancelEditTeacher.Visibility = Windows.Visibility.Collapsed
        btnDeleteTeacher.IsEnabled = True

        _teacher = Application.oTeacherManager.getTeacher(_teacher.TeacherID)
        gridTeacherInformation.DataContext = _teacher
    End Sub

    ''' <summary>
    ''' Method that save the changes made on a Teacher
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub btnSaveChangesTeachers_Click(sender As Object, e As RoutedEventArgs) Handles btnSaveChangesTeachers.Click
        Dim numReg As Integer
        Dim validation As Boolean = True
        Dim sex As New Char

        'Valid Name
        If Not Application.oValidator.validateEmptyString(txtName.Text) Then
            txtName.BorderBrush = Brushes.Red
            lblErrorName.Visibility = Windows.Visibility.Visible
            validation = False
        Else
            _teacher.Name = txtName.Text
            txtDNI.ClearValue(TextBox.BorderBrushProperty)
            lblErrorName.Visibility = Windows.Visibility.Collapsed
        End If

        'Valid Surname
        If Not Application.oValidator.validateEmptyString(txtSurname.Text) Then
            txtSurname.BorderBrush = Brushes.Red
            lblErrorSurname.Visibility = Windows.Visibility.Visible
            validation = False
        Else
            _teacher.Surname = txtSurname.Text
            txtSurname.ClearValue(TextBox.BorderBrushProperty)
            lblErrorSurname.Visibility = Windows.Visibility.Collapsed
        End If

        'Validate DNI
        lblErrorDNI.Text = ""
        If Not Application.oValidator.validateEmptyString(txtDNI.Text) Then
            txtDNI.BorderBrush = Brushes.Red
            lblErrorDNI.Visibility = Windows.Visibility.Visible
            lblErrorDNI.Text = CType(FindResource("error.DNI_empty"), String)
            validation = False
        ElseIf Not Application.oValidator.validateDNI(txtDNI.Text) Then
            txtDNI.BorderBrush = Brushes.Red
            lblErrorDNI.Visibility = Windows.Visibility.Visible
            lblErrorDNI.Text = CType(FindResource("error.DNI_invalid"), String)
            validation = False
        ElseIf Not Application.oValidator.validateExistDNI(txtDNI.Text) And Not txtDNI.Text = DNI.Text Then
            txtDNI.BorderBrush = Brushes.Red
            lblErrorDNI.Visibility = Windows.Visibility.Visible
            lblErrorDNI.Text = CType(FindResource("error.DNI_existing"), String)
            validation = False
        Else
            _teacher.DNI = txtDNI.Text
            txtDNI.ClearValue(TextBox.BorderBrushProperty)
            lblErrorDNI.Visibility = Windows.Visibility.Collapsed
        End If

        'Validate Date
        If txtBirthDate.SelectedDate Is Nothing Then
            _teacher.BirthDate = Nothing
            txtBirthDate.ClearValue(TextBox.BorderBrushProperty)
            lblErrorDateBirth.Visibility = Windows.Visibility.Collapsed
        Else
            If Not IsDate(txtBirthDate.SelectedDate) Or txtBirthDate.SelectedDate.Value > Date.Today Or txtBirthDate.SelectedDate.Value.Year < 1754 Then
                txtBirthDate.BorderBrush = Brushes.Red
                lblErrorDateBirth.Visibility = Windows.Visibility.Visible
                txtBirthDate.SelectedDate = Nothing
                validation = False
            Else
                _teacher.BirthDate = txtBirthDate.SelectedDate
                txtBirthDate.BorderBrush = Brushes.White
                lblErrorDateBirth.Visibility = Windows.Visibility.Collapsed
            End If
        End If

        'Validate Telephone
        If Application.oValidator.validateEmptyString(txtTelephone.Text) And Not Application.oValidator.validateTelephone(txtTelephone.Text) Then
            txtTelephone.BorderBrush = Brushes.Red
            lblErrorTelephone.Visibility = Windows.Visibility.Visible
            validation = False
        Else
            _teacher.Telephone = txtTelephone.Text
            txtTelephone.ClearValue(TextBox.BorderBrushProperty)
            lblErrorTelephone.Visibility = Windows.Visibility.Collapsed
        End If

        'Validate Email
        If Application.oValidator.validateEmptyString(txtEmail.Text) And Not Application.oValidator.validateEmail(txtEmail.Text) Then
            txtEmail.BorderBrush = Brushes.Red
            lblErrorEmail.Visibility = Windows.Visibility.Visible
            validation = False
        Else
            _teacher.Email = txtEmail.Text
            txtEmail.ClearValue(TextBox.BorderBrushProperty)
            lblErrorEmail.Visibility = Windows.Visibility.Collapsed
        End If

        If validation Then
            'Get sex
            If (rdBtnSexMale.IsChecked) Then
                sex = "M"
            Else
                sex = "F"
            End If

            _teacher.Sex = sex

            'If any photo has been attached
            If _teacher.Photo Is Nothing Then
                If sex = "M" Then
                    imgTeacherPhoto.Source = CType(FindResource("imgMan"), ImageSource)
                Else
                    imgTeacherPhoto.Source = CType(FindResource("imgWomen"), ImageSource)
                End If
            End If

            'Set photo
            _teacher.Photo = imgTeacherPhoto

            _teacher.Address = txtAddress.Text
            _teacher.City = txtCity.Text
            _teacher.PostalCode = txtPostalCode.Text

            'Save Student
            numReg = Application.oPersonManager.updatePerson(_teacher)
            numReg = Application.oPersonManager.updatePerson(_teacher)

            'Change access to buttons and textboxs
            btnSaveChangesTeachers.IsEnabled = False
            btnEditTeacher.IsEnabled = True

            'Show messageBox confirm
            Dim messageBoxResult As MessageBoxResult
            Dim messageBoxOk As New msgBxOk(CType(FindResource("message_box.save_changes"), String), CType(FindResource("message_box.save_changes_success"), String))
            messageBoxResult = messageBoxOk.ShowMessageBox()

            'Refresh list in case of there are new teachers
            refreshTeacherList()
        End If
    End Sub

    ''' <summary>
    ''' Method that refresh the Teacher ListBox
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub refreshTeacherList()
        _listTeachers = Application.oTeacherManager.getAllTeachers()
        lstBxTeachers.ItemsSource = _listTeachers

        If _listTeachers.Count > 0 Then
            Me.gridTeacherInformation.IsEnabled = True
            btnEditTeacher.IsEnabled = True
            btnDeleteTeacher.IsEnabled = True
        Else
            Me.gridTeacherInformation.IsEnabled = False
            btnEditTeacher.IsEnabled = False
            btnDeleteTeacher.IsEnabled = False
        End If
    End Sub

    ''' <summary>
    ''' Method that refresh the Group ListBox
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub refreshGroupList()
        _listGroups = Application.oGroupManager.getAllGroupsSearch(Nothing, txtTeacherID.Text)
        lstBxGroups.ItemsSource = _listGroups
    End Sub

    ''' <summary>
    ''' Method that opens a new dlgAddGroupToTeacher Window
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub btnAddGroup_Click(sender As Object, e As RoutedEventArgs) Handles btnAddGroup.Click
        Dim addGroupToTeacherDialog As New dlgAddGroupToTeacher(txtTeacherID.Text)
        addGroupToTeacherDialog.ShowDialog()

        'Refresh list in case of there are new groups
        refreshGroupList()
    End Sub

    ''' <summary>
    ''' Method that is executed when the user press a button from the
    ''' Group List
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub listGroupsClick(sender As Object, e As RoutedEventArgs)
        Dim numReg As Integer
        Dim originalSource As Control = e.OriginalSource
        Dim listBoxItem As ListBoxItem = originalSource.TemplatedParent
        Dim button As Button = CType(e.OriginalSource, Button)
        Dim group As clsGroup = CType(listBoxItem.DataContext, clsGroup)

        'If the pressed button is the Remove Group
        If button.Name.Equals("btnRemoveGroup") Then
            Dim messageBoxResult As MessageBoxResult
            Dim messageBoxWithCheckBox As New msgBxYesNo(CType(FindResource("message_box.remove_group"), String), CType(FindResource("message_box.remove_group_question"), String))

            messageBoxResult = messageBoxWithCheckBox.ShowMessageBox()

            'Delete Group
            If messageBoxResult = Windows.MessageBoxResult.Yes Then
                group.TeacherID = Nothing
                numReg = Application.oGroupManager.updateGroup(group)

                If numReg > 0 Then
                    'Refresh list Students
                    refreshTeacherList()
                    refreshGroupList()
                Else
                    'Show messageBox Error
                    Dim messageBoxResult2 As MessageBoxResult
                    Dim messageBoxOk2 As New msgBxOk(CType(FindResource("message_box.error_title"), String), CType(FindResource("message_box.error_message"), String))
                    messageBoxResult2 = messageBoxOk2.ShowMessageBox()
                End If
            End If
        End If
    End Sub
End Class

