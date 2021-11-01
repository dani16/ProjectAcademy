Imports Microsoft.Win32

Public Class ctrlStudentsTab
    Private _controllerStudent As clsStudentTabController

    Public Sub New()
        InitializeComponent()
        _controllerStudent = New clsStudentTabController()
        Me.DataContext = _controllerStudent
    End Sub

    Private Sub ctrlStudentsTab_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        'Set DataPicker DisplayDateEnd
        txtBirthDate.DisplayDateEnd = Date.Today
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlStudentsTab_DataContextChanged(sender As Object, e As DependencyPropertyChangedEventArgs) Handles Me.DataContextChanged
        If Not lstBxStudents.ItemsSource Is Nothing Then
            'Default Status Buttons
            btnSaveChangesStudents.IsEnabled = False
            btnEditStudent.Visibility = Windows.Visibility.Visible
            btnCancelEditStudent.Visibility = Windows.Visibility.Collapsed
            btnDeleteStudent.IsEnabled = True
            cmbBxEnglishLevel.IsEnabled = False
        Else
            btnSaveChangesStudents.IsEnabled = False
            btnEditStudent.Visibility = Windows.Visibility.Visible
            btnEditStudent.IsEnabled = False
            btnCancelEditStudent.Visibility = Windows.Visibility.Collapsed
            btnDeleteStudent.IsEnabled = False
            cmbBxEnglishLevel.IsEnabled = False
        End If
    End Sub

    'Dim _listStudents As List(Of clsStudent)
    'Dim _listGroups As List(Of clsGroup)
    'Dim _listMarks As List(Of clsMark)
    'Dim _student As New clsStudent

    ' ''' <summary>
    ' ''' Method that get all the student from the database when the control is loaded
    ' ''' </summary>
    ' ''' <param name="sender">An Object</param>
    ' ''' <param name="e">An object RoutedEventArgs</param>
    ' ''' <remarks></remarks>
    'Private Sub ctrlStudentsTab_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
    '    Dim englishLevel As List(Of clsEnglishLevel)

    '    'Load English Level into comboBox
    '    englishLevel = Application.oEnglishLevelManager.getEnglishLevels()
    '    cmbBxEnglishLevel.ItemsSource = englishLevel

    '    'Set DataPicker DisplayDateEnd
    '    txtBirthDate.DisplayDateEnd = Date.Today

    '    'Default Status Buttons
    '    btnSaveChangesStudents.IsEnabled = False
    '    btnEditStudent.Visibility = Windows.Visibility.Visible
    '    btnCancelEditStudent.Visibility = Windows.Visibility.Collapsed
    '    btnDeleteStudent.IsEnabled = True
    '    cmbBxEnglishLevel.IsEnabled = False

    '    If TypeOf (gridStudentInformation.DataContext) Is clsStudent Then 'When the application is loaded the gridStudentInformation dataContext is an object clsConfigurationDataContext
    '        'Clear the grid from unsaved changes
    '        'Or In case of the dataContext it is set from another tab (Ex: View student on TabGroups) sets the Student selected from another tab
    '        _student = Application.oStudentManager.getStudent(CType(gridStudentInformation.DataContext, clsStudent).StudentID)
    '    End If

    '    'Get all the students from the database
    '    _listStudents = Application.oStudentManager.getAllStudents()
    '    lstBxStudents.ItemsSource = _listStudents
    '    gridStudentInformation.DataContext = _student

    '    If _listStudents.Count > 0 Then
    '        'Load Groups
    '        _listGroups = Application.oGroupManager.getStudentGroups(_student.StudentID)
    '        lstBxGroups.ItemsSource = _listGroups

    '        'Load Marks
    '        _listMarks = Application.oMarkManager.getStudentMarks(_student.StudentID)
    '        lstBxMarks.ItemsSource = _listMarks
    '    Else
    '        Me.gridStudentInformation.IsEnabled = False
    '        btnEditStudent.IsEnabled = False
    '        btnDeleteStudent.IsEnabled = False
    '        lstBxGroups.ItemsSource = Nothing
    '        gridStudentGroups.IsEnabled = False
    '    End If
    'End Sub

    ' ''' <summary>
    ' ''' Method that load a student when a student is selected from the listBox
    ' ''' </summary>
    ' ''' <param name="sender">An Object</param>
    ' ''' <param name="e">An object RoutedEventArgs</param>
    ' ''' <remarks></remarks>
    'Private Sub lstBxStudents_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles lstBxStudents.SelectionChanged
    '    'When you change the tab and you return to the Student tab the Selected Index is -1.
    '    If Me.lstBxStudents.SelectedIndex >= 0 Then
    '        _student = _listStudents.Item(Me.lstBxStudents.SelectedIndex)

    '        'Load Groups
    '        _listGroups = Application.oGroupManager.getStudentGroups(_student.StudentID)
    '        lstBxGroups.ItemsSource = _listGroups

    '        'Load Marks
    '        _listMarks = Application.oMarkManager.getStudentMarks(_student.StudentID)
    '        lstBxMarks.ItemsSource = _listMarks
    '    End If

    '    btnSaveChangesStudents.IsEnabled = False
    '    btnEditStudent.Visibility = Windows.Visibility.Visible
    '    btnCancelEditStudent.Visibility = Windows.Visibility.Collapsed
    '    btnDeleteStudent.IsEnabled = True
    '    cmbBxEnglishLevel.IsEnabled = False

    '    Me.gridStudentInformation.DataContext = _student
    'End Sub

    ' ''' <summary>
    ' ''' Method that search for an specific Student when the user write on the searcher textBox
    ' ''' </summary>
    ' ''' <param name="sender">An Object</param>
    ' ''' <param name="e">An object RoutedEventArgs</param>
    ' ''' <remarks></remarks>
    'Private Sub txtBxSearchStudent_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtBxSearchStudent.TextChanged
    '    txtMessageSearch.Visibility = Windows.Visibility.Hidden

    '    'Starts searching the use introduces at least 3 characters
    '    If txtBxSearchStudent.Text.Length >= 3 Then
    '        _listStudents = Application.oStudentManager.getAllStudents(txtBxSearchStudent.Text)

    '        'Show message: Not results found
    '        If _listStudents.Count = 0 Then
    '            txtMessageSearch.Visibility = Windows.Visibility.Visible
    '        End If
    '    Else
    '        _listStudents = Application.oStudentManager.getAllStudents()
    '    End If

    '    lstBxStudents.ItemsSource = _listStudents
    'End Sub

    ' ''' <summary>
    ' ''' Method that opens a new dlgNewStudent Window
    ' ''' </summary>
    ' ''' <param name="sender">An Object</param>
    ' ''' <param name="e">An object RoutedEventArgs</param>
    ' ''' <remarks></remarks>
    'Private Sub btnNewStudent_Click(sender As Object, e As RoutedEventArgs) Handles btnNewStudent.Click
    '    Dim newStudentDialog As New dlgNewStudent()
    '    newStudentDialog.ShowDialog()

    '    'Refresh list in case of there are new students
    '    refreshStudentList()
    '    gridStudentInformation.DataContext = _student
    'End Sub

    ' ''' <summary>
    ' ''' Method that changes the image of an Student
    ' ''' </summary>
    ' ''' <param name="sender">An Object</param>
    ' ''' <param name="e">An object RoutedEventArgs</param>
    ' ''' <remarks></remarks>
    'Private Sub btnChangeImageStudent_Click(sender As Object, e As RoutedEventArgs) Handles btnChangeImageStudent.Click
    '    Dim openFileDialog As New OpenFileDialog()
    '    Dim numReg As New Integer

    '    'The user search on his computer for a new Photo
    '    openFileDialog.ShowDialog()
    '    openFileDialog.Title = CType(FindResource("dialog.photo_title"), String)
    '    'openFileDialog.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files(*.png)|*.png|JPG"
    '    openFileDialog.DefaultExt = ".jpeg"

    '    'Set New Photo to the Student Object
    '    If Not openFileDialog.FileName Is "" Then
    '        imgStudentPhoto.Source = New BitmapImage(New Uri(openFileDialog.FileName))
    '    End If
    'End Sub

    ' ''' <summary>
    ' ''' Method that deletes the photo of a Student
    ' ''' </summary>
    ' ''' <param name="sender">An Object</param>
    ' ''' <param name="e">An object RoutedEventArgs</param>
    ' ''' <remarks></remarks>
    'Private Sub btnDeleteImageStudent_Click(sender As Object, e As RoutedEventArgs) Handles btnDeleteImageStudent.Click
    '    imgStudentPhoto.Source = Nothing
    'End Sub

    ' ''' <summary>
    ' ''' Method that deletes a Student
    ' ''' </summary>
    ' ''' <param name="sender">An Object</param>
    ' ''' <param name="e">An object RoutedEventArgs</param>
    ' ''' <remarks></remarks>
    'Private Sub btnDeleteStudent_Click(sender As Object, e As RoutedEventArgs) Handles btnDeleteStudent.Click
    '    Dim numReg As Integer
    '    Dim messageBoxResult As MessageBoxResult
    '    Dim messageBoxYesNo As New msgBxYesNo(CType(FindResource("message_box.delete_student"), String), CType(FindResource("message_box.delete_student_question"), String) & " " & txtName.Text & " " & txtSurname.Text & "?")
    '    messageBoxResult = messageBoxYesNo.ShowMessageBox()

    '    If messageBoxResult = messageBoxResult.Yes Then
    '        numReg = Application.oStudentManager.deleteStudent(txtStudentID.Text)

    '        If numReg > 0 Then
    '            'Refresh Students
    '            refreshStudentList()

    '            'Show messageBox confirm
    '            Dim messageBoxResult2 As MessageBoxResult
    '            Dim messageBoxOk As New msgBxOk(CType(FindResource("message_box.delete_student"), String), CType(FindResource("message_box.delete_student_success"), String))
    '            messageBoxResult2 = messageBoxOk.ShowMessageBox()
    '        End If
    '    End If
    'End Sub

    ' ''' <summary>
    ' ''' Method that allow a user to edit the information of a Student
    ' ''' </summary>
    ' ''' <param name="sender">An Object</param>
    ' ''' <param name="e">An object RoutedEventArgs</param>
    ' ''' <remarks></remarks>
    'Private Sub btnEditStudent_Click(sender As Object, e As RoutedEventArgs) Handles btnEditStudent.Click
    '    btnSaveChangesStudents.IsEnabled = True
    '    btnEditStudent.Visibility = Windows.Visibility.Collapsed
    '    btnCancelEditStudent.Visibility = Windows.Visibility.Visible
    '    btnDeleteStudent.IsEnabled = False
    '    cmbBxEnglishLevel.IsEnabled = True
    'End Sub

    ' ''' <summary>
    ' ''' Method that cancels the edition of a student
    ' ''' </summary>
    ' ''' <param name="sender">An Object</param>
    ' ''' <param name="e">An object RoutedEventArgs</param>
    ' ''' <remarks></remarks>
    'Private Sub btnCancelEditStudent_Click(sender As Object, e As RoutedEventArgs) Handles btnCancelEditStudent.Click
    '    btnSaveChangesStudents.IsEnabled = False
    '    btnEditStudent.Visibility = Windows.Visibility.Visible
    '    btnCancelEditStudent.Visibility = Windows.Visibility.Collapsed
    '    btnDeleteStudent.IsEnabled = True
    '    cmbBxEnglishLevel.IsEnabled = False

    '    _student = Application.oStudentManager.getStudent(_student.StudentID)
    '    gridStudentInformation.DataContext = _student
    'End Sub

    ' ''' <summary>
    ' ''' Method that save the changes made on a Student
    ' ''' </summary>
    ' ''' <param name="sender">An Object</param>
    ' ''' <param name="e">An object RoutedEventArgs</param>
    ' ''' <remarks></remarks>
    'Private Sub btnSaveChangesStudents_Click(sender As Object, e As RoutedEventArgs) Handles btnSaveChangesStudents.Click
    '    Dim numReg As Integer
    '    Dim validation As Boolean = True
    '    Dim age As Integer
    '    Dim sex As New Char

    '    'Valid Name
    '    If Not Application.oValidator.validateEmptyString(txtName.Text) Then
    '        txtName.BorderBrush = Brushes.Red
    '        lblErrorName.Visibility = Windows.Visibility.Visible
    '        validation = False
    '    Else
    '        _student.Name = txtName.Text
    '        txtDNI.ClearValue(TextBox.BorderBrushProperty)
    '        lblErrorName.Visibility = Windows.Visibility.Collapsed
    '    End If

    '    'Valid Surname
    '    If Not Application.oValidator.validateEmptyString(txtSurname.Text) Then
    '        txtSurname.BorderBrush = Brushes.Red
    '        lblErrorSurname.Visibility = Windows.Visibility.Visible
    '        validation = False
    '    Else
    '        _student.Surname = txtSurname.Text
    '        txtSurname.ClearValue(TextBox.BorderBrushProperty)
    '        lblErrorSurname.Visibility = Windows.Visibility.Collapsed
    '    End If

    '    'Validate DNI
    '    lblErrorDNI.Text = ""
    '    If Not Application.oValidator.validateEmptyString(txtDNI.Text) Then
    '        txtDNI.BorderBrush = Brushes.Red
    '        lblErrorDNI.Visibility = Windows.Visibility.Visible
    '        lblErrorDNI.Text = CType(FindResource("error.DNI_empty"), String)
    '        validation = False
    '    ElseIf Not Application.oValidator.validateDNI(txtDNI.Text) Then
    '        txtDNI.BorderBrush = Brushes.Red
    '        lblErrorDNI.Visibility = Windows.Visibility.Visible
    '        lblErrorDNI.Text = CType(FindResource("error.DNI_invalid"), String)
    '        validation = False
    '    ElseIf Not Application.oValidator.validateExistDNI(txtDNI.Text) And Not txtDNI.Text = DNI.Text Then
    '        txtDNI.BorderBrush = Brushes.Red
    '        lblErrorDNI.Visibility = Windows.Visibility.Visible
    '        lblErrorDNI.Text = CType(FindResource("error.DNI_existing"), String)
    '        validation = False
    '    Else
    '        _student.DNI = txtDNI.Text
    '        txtDNI.ClearValue(TextBox.BorderBrushProperty)
    '        lblErrorDNI.Visibility = Windows.Visibility.Collapsed
    '    End If

    '    'Validate Date
    '    If txtBirthDate.SelectedDate Is Nothing Then
    '        _student.BirthDate = Nothing
    '        txtBirthDate.ClearValue(TextBox.BorderBrushProperty)
    '        lblErrorDateBirth.Visibility = Windows.Visibility.Collapsed
    '    Else
    '        If Not IsDate(txtBirthDate.SelectedDate) Or txtBirthDate.SelectedDate.Value > Date.Today Or txtBirthDate.SelectedDate.Value.Year < 1754 Then
    '            txtBirthDate.BorderBrush = Brushes.Red
    '            lblErrorDateBirth.Visibility = Windows.Visibility.Visible
    '            txtBirthDate.SelectedDate = Nothing
    '            validation = False
    '        Else
    '            _student.BirthDate = txtBirthDate.SelectedDate
    '            txtBirthDate.BorderBrush = Brushes.White
    '            lblErrorDateBirth.Visibility = Windows.Visibility.Collapsed
    '        End If
    '    End If

    '    'Validate Telephone
    '    If Application.oValidator.validateEmptyString(txtTelephone.Text) And Not Application.oValidator.validateTelephone(txtTelephone.Text) Then
    '        txtTelephone.BorderBrush = Brushes.Red
    '        lblErrorTelephone.Visibility = Windows.Visibility.Visible
    '        validation = False
    '    Else
    '        _student.Telephone = txtTelephone.Text
    '        txtTelephone.ClearValue(TextBox.BorderBrushProperty)
    '        lblErrorTelephone.Visibility = Windows.Visibility.Collapsed
    '    End If

    '    'Validate Email
    '    If Application.oValidator.validateEmptyString(txtEmail.Text) And Not Application.oValidator.validateEmail(txtEmail.Text) Then
    '        txtEmail.BorderBrush = Brushes.Red
    '        lblErrorEmail.Visibility = Windows.Visibility.Visible
    '        validation = False
    '    Else
    '        _student.Email = txtEmail.Text
    '        txtEmail.ClearValue(TextBox.BorderBrushProperty)
    '        lblErrorEmail.Visibility = Windows.Visibility.Collapsed
    '    End If

    '    If validation Then
    '        'Get sex
    '        If (rdBtnSexMale.IsChecked) Then
    '            sex = "M"
    '        Else
    '            sex = "F"
    '        End If

    '        _student.Situation = txtSituation.Text
    '        '_student.EnglishLevel = txtEnglishLevel.Text
    '        _student.EnglishLevel = cmbBxEnglishLevel.SelectedValue
    '        _student.Sex = sex

    '        'Get Age
    '        If Not txtBirthDate.SelectedDate Is Nothing Then
    '            age = Date.Today.Year - txtBirthDate.SelectedDate.Value.Year
    '            If txtBirthDate.SelectedDate.Value > Date.Today.AddYears(-age) Then
    '                age -= 1
    '            End If
    '        Else
    '            age = 18
    '        End If

    '        'If any photo has been attached
    '        If imgStudentPhoto.Source Is Nothing Then
    '            If sex = "M" Then
    '                If age >= 18 Then
    '                    imgStudentPhoto.Source = CType(FindResource("imgMan"), ImageSource)
    '                Else
    '                    imgStudentPhoto.Source = CType(FindResource("imgBoy"), ImageSource)
    '                End If
    '            Else
    '                If sex = "F" Then
    '                    If age >= 18 Then
    '                        imgStudentPhoto.Source = CType(FindResource("imgWomen"), ImageSource)
    '                    Else
    '                        imgStudentPhoto.Source = CType(FindResource("imgGirl"), ImageSource)
    '                    End If
    '                End If
    '            End If
    '        End If

    '        'Set photo
    '        _student.Photo = imgStudentPhoto

    '        _student.Address = txtAddress.Text
    '        _student.City = txtCity.Text
    '        _student.PostalCode = txtPostalCode.Text

    '        'Save Student
    '        numReg = Application.oStudentManager.updateStudent(_student)
    '        If numReg > 0 Then
    '            numReg = Application.oPersonManager.updatePerson(_student)

    '            If numReg > 0 Then
    '                'Change access to buttons and textboxs
    '                btnSaveChangesStudents.IsEnabled = False
    '                btnEditStudent.Visibility = Windows.Visibility.Visible
    '                btnCancelEditStudent.Visibility = Windows.Visibility.Collapsed
    '                btnDeleteStudent.IsEnabled = True
    '                'txtEnglishLevel.Visibility = Windows.Visibility.Visible
    '                'cmbBxEnglishLevel.Visibility = Windows.Visibility.Collapsed

    '                'Show messageBox confirm
    '                Dim messageBoxResult As MessageBoxResult
    '                Dim messageBoxOk As New msgBxOk(CType(FindResource("message_box.save_changes"), String), CType(FindResource("message_box.save_changes_success"), String))
    '                messageBoxResult = messageBoxOk.ShowMessageBox()

    '                'Refresh student list
    '                refreshStudentList()
    '            End If
    '        End If

    '        If numReg <= 0 Then
    '            'Show messageBox Error
    '            Dim messageBoxResult2 As MessageBoxResult
    '            Dim messageBoxOk2 As New msgBxOk(CType(FindResource("message_box.error_title"), String), CType(FindResource("message_box.error_message"), String))
    '            messageBoxResult2 = messageBoxOk2.ShowMessageBox()
    '        End If
    '    End If
    'End Sub

    ' ''' <summary>
    ' ''' Method that refresh the Student ListBox
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Private Sub refreshStudentList()
    '    _listStudents = Application.oStudentManager.getAllStudents()
    '    lstBxStudents.ItemsSource = _listStudents

    '    If _listStudents.Count > 0 Then
    '        Me.gridStudentInformation.IsEnabled = True
    '        gridStudentGroups.IsEnabled = True
    '        btnEditStudent.IsEnabled = True
    '        btnDeleteStudent.IsEnabled = True
    '    Else
    '        Me.gridStudentInformation.IsEnabled = False
    '        lstBxGroups.DataContext = Nothing
    '        gridStudentGroups.IsEnabled = False
    '        btnEditStudent.IsEnabled = False
    '        btnDeleteStudent.IsEnabled = False
    '        _student = Nothing
    '        Me.gridStudentInformation.DataContext = Nothing
    '        Me.lstBxGroups.ItemsSource = Nothing
    '    End If

    '    btnSaveChangesStudents.IsEnabled = False
    '    btnEditStudent.Visibility = Windows.Visibility.Visible
    '    btnCancelEditStudent.Visibility = Windows.Visibility.Collapsed
    '    cmbBxEnglishLevel.IsEnabled = False
    'End Sub

    ' ''' <summary>
    ' ''' Method that refresh the Group ListBox
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Private Sub refreshGroupList()
    '    _listGroups = Application.oGroupManager.getStudentGroups(txtStudentID.Text)
    '    lstBxGroups.ItemsSource = _listGroups
    'End Sub

    ' ''' <summary>
    ' ''' Method that refresh the Group ListBox
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Private Sub refreshMarkList()
    '    _listMarks = Application.oMarkManager.getStudentMarks(txtStudentID.Text)
    '    lstBxMarks.ItemsSource = _listMarks
    'End Sub

    ' ''' <summary>
    ' ''' Method that opens a new dlgAddGroupToStudent Window
    ' ''' </summary>
    ' ''' <param name="sender">An Object</param>
    ' ''' <param name="e">An object RoutedEventArgs</param>
    ' ''' <remarks></remarks>
    'Private Sub btnAddGroup_Click(sender As Object, e As RoutedEventArgs) Handles btnAddGroup.Click
    '    Dim addGroupToStudentDialog As New dlgAddGroupToStudent(txtStudentID.Text)
    '    addGroupToStudentDialog.ShowDialog()

    '    'Refresh list in case of there are new groups
    '    refreshGroupList()
    'End Sub

    ' ''' <summary>
    ' ''' Method that opens a new dlgAddMark Window
    ' ''' </summary>
    ' ''' <param name="sender">An Object</param>
    ' ''' <param name="e">An object RoutedEventArgs</param>
    ' ''' <remarks></remarks>
    'Private Sub btnAddMark_Click(sender As Object, e As RoutedEventArgs) Handles btnAddMark.Click
    '    Dim addMarkDialog As New dlgAddMark(txtStudentID.Text, Nothing)
    '    addMarkDialog.ShowDialog()

    '    'Refresh list in case of there are new marks
    '    refreshMarkList()
    'End Sub

    ' ''' <summary>
    ' ''' Method that is executed when the user press a button from the
    ' ''' Groups List
    ' ''' </summary>
    ' ''' <param name="sender">An Object</param>
    ' ''' <param name="e">An object RoutedEventArgs</param>
    ' ''' <remarks></remarks>
    'Private Sub listGroupsClick(sender As Object, e As RoutedEventArgs)
    '    Dim numReg As Integer
    '    Dim originalSource As Control = e.OriginalSource
    '    Dim listBoxItem As ListBoxItem = originalSource.TemplatedParent
    '    Dim button As Button = CType(e.OriginalSource, Button)
    '    Dim group As clsGroup = CType(listBoxItem.DataContext, clsGroup)

    '    'If the pressede button is the Remove Student
    '    If button.Name.Equals("btnRemoveGroup") Then
    '        Dim messageBoxResult As MessageBoxResult
    '        Dim messageBoxWithCheckBox As New msgBxYesNoWithCheckBox(CType(FindResource("message_box.remove_group"), String), CType(FindResource("message_box.remove_group_question"), String), False, CType(FindResource("message_box.remove_payments_student"), String))

    '        messageBoxResult = messageBoxWithCheckBox.ShowMessageBox()

    '        'Delete Group
    '        If messageBoxResult = Windows.MessageBoxResult.Yes Then
    '            numReg = Application.oInscriptionManager.removeStudent(_student.StudentID, group.GroupID)

    '            If numReg > 0 Then
    '                'Remove payments if checkbox is True
    '                If messageBoxWithCheckBox.CheckBox = True Then
    '                    numReg = Application.oPaymentManager.deleteStudentPayment(_student.StudentID, group.GroupID)
    '                End If

    '                'Refresh list Students
    '                refreshStudentList()
    '                refreshGroupList()
    '            End If

    '            If numReg <= 0 Then
    '                'Show messageBox Error
    '                Dim messageBoxResult2 As MessageBoxResult
    '                Dim messageBoxOk2 As New msgBxOk(CType(FindResource("message_box.error_title"), String), CType(FindResource("message_box.error_message"), String))
    '                messageBoxResult2 = messageBoxOk2.ShowMessageBox()
    '            End If
    '        End If
    '    End If
    'End Sub

    ' ''' <summary>
    ' ''' Method that is executed when the user press a button from the
    ' ''' Marks List
    ' ''' </summary>
    ' ''' <param name="sender">An Object</param>
    ' ''' <param name="e">An object RoutedEventArgs</param>
    ' ''' <remarks></remarks>
    'Private Sub listMarksClick(sender As Object, e As RoutedEventArgs)
    '    Dim numReg As Integer
    '    Dim originalSource As Control = e.OriginalSource
    '    Dim listBoxItem As ListBoxItem = originalSource.TemplatedParent
    '    Dim button As Button = CType(e.OriginalSource, Button)
    '    Dim mark As clsMark = CType(listBoxItem.DataContext, clsMark)

    '    'If the pressede button is the Remove Student
    '    If button.Name.Equals("btnRemoveMark") Then
    '        Dim messageBoxResult As MessageBoxResult
    '        Dim messageBoxYesNo As New msgBxYesNo(CType(FindResource("message_box.delete_mark"), String), CType(FindResource("message_box.delete_mark_question"), String))

    '        messageBoxResult = messageBoxYesNo.ShowMessageBox()

    '        'Delete Mark
    '        If messageBoxResult = Windows.MessageBoxResult.Yes Then
    '            numReg = Application.oMarkManager.deleteMark(mark.StudentID, mark.GroupID, mark.DateMark)

    '            If numReg > 0 Then
    '                'Refresh lists
    '                refreshStudentList()
    '                refreshGroupList()
    '                refreshMarkList()
    '            Else
    '                'Show messageBox Error
    '                Dim messageBoxResult2 As MessageBoxResult
    '                Dim messageBoxOk2 As New msgBxOk(CType(FindResource("message_box.error_title"), String), CType(FindResource("message_box.error_message"), String))
    '                messageBoxResult2 = messageBoxOk2.ShowMessageBox()
    '            End If
    '        End If
    '    End If
    'End Sub     
End Class
