Public Class ctrlMarksTab
    Dim _listStudents As List(Of clsStudent)
    Dim _listGroups As List(Of clsGroup)
    Dim _listMarks As List(Of clsMark)
    Dim _student As clsStudent
    Dim _mark As clsMark

    ''' <summary>
    ''' Method that get all the student from the database when the control is loaded
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub ctrlMarksTab_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        'Default Status Buttons
        btnSaveChangesMarks.IsEnabled = False
        btnEditMark.IsEnabled = True
        btnEditMark.Visibility = Windows.Visibility.Visible
        btnCancelEditMark.Visibility = Windows.Visibility.Collapsed

        If TypeOf (gridMarkInformation.DataContext) Is clsStudent Then 'When the application is loaded the gridMarkInformation dataContext is an object clsConfigurationDataContext
            'Clear the grid from unsaved changes
            'Or In case of the dataContext it is set from another tab (Ex: View student on TabGroups) sets the Student selected from another tab
            If Not gridTableMarks.DataContext Is Nothing Then
                _student = Application.oStudentManager.getStudent(CType(gridTableMarks.DataContext, clsMark).StudentID)
                _mark = Application.oMarkManager.getMark(CType(gridTableMarks.DataContext, clsMark).StudentID, CType(gridTableMarks.DataContext, clsMark).GroupID, CType(gridTableMarks.DataContext, clsMark).DateMark)
            End If
        End If

        'Get all the students from the database
        _listStudents = Application.oStudentManager.getAllStudents()
        lstBxStudents.ItemsSource = _listStudents
        Me.gridMarkInformation.DataContext = _student

        If _listStudents.Count > 0 Then
            'Load groups
            _listGroups = Application.oGroupManager.getStudentGroups(_student.StudentID)
            cmbBxGroup.ItemsSource = _listGroups

            If Not _mark Is Nothing Then
                cmbBxGroup.SelectedIndex = 0
            End If

            If _listGroups.Count > 0 Then
                If Not _mark Is Nothing Then
                    cmbBxGroup.SelectedValue = _mark.GroupID

                    'Load Marks
                    _listMarks = Application.oMarkManager.getGroupStudentMarks(_student.StudentID, cmbBxGroup.SelectedValue)
                    lstBxMarks.ItemsSource = _listMarks

                    If _listMarks.Count > 0 Then
                        Me.gridTableMarks.DataContext = _mark
                        cmbBxYear.SelectedValue = DateTime.Today.Year
                        cmbBxMonth.SelectedIndex = _mark.getTerm()
                        btnEditMark.IsEnabled = True
                    Else
                        btnEditMark.IsEnabled = False
                    End If
                End If
            Else
                btnEditMark.IsEnabled = False
            End If
        Else
            cmbBxGroup.ItemsSource = Nothing
            Me.gridMarkInformation.IsEnabled = False
            btnEditMark.IsEnabled = False
            lstBxMarks.ItemsSource = Nothing
            gridStudentMarks.IsEnabled = False
        End If
    End Sub

    ''' <summary>
    ''' Method that load a student when a student is selected from the listBox
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub lstBxStudents_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles lstBxStudents.SelectionChanged
        'Default Status Buttons
        btnSaveChangesMarks.IsEnabled = False
        btnEditMark.Visibility = Windows.Visibility.Visible
        btnCancelEditMark.Visibility = Windows.Visibility.Collapsed

        'When you change the tab and you return to the Student tab the Selected Index is -1.
        If Me.lstBxStudents.SelectedIndex >= 0 Then

            _student = _listStudents.Item(Me.lstBxStudents.SelectedIndex)

            'Load Groups
            _listGroups = Application.oGroupManager.getStudentGroups(_student.StudentID)
            cmbBxGroup.ItemsSource = _listGroups
            cmbBxGroup.SelectedIndex = 0

            If _listGroups.Count > 0 Then
                gridStudentMarks.IsEnabled = True

                'Load Marks
                _listMarks = Application.oMarkManager.getGroupStudentMarks(_student.StudentID, cmbBxGroup.SelectedValue)
                lstBxMarks.ItemsSource = _listMarks

                If _listMarks.Count > 0 Then
                    btnEditMark.IsEnabled = True

                    _mark = _listMarks(0)
                    Me.gridMarkInformation.DataContext = _student
                    Me.gridTableMarks.DataContext = _mark
                Else
                    btnEditMark.IsEnabled = False
                    Me.gridMarkInformation.DataContext = _student
                    Me.gridTableMarks.DataContext = Nothing
                End If
            Else
                btnEditMark.IsEnabled = False
                lstBxMarks.ItemsSource = Nothing
                gridStudentMarks.IsEnabled = False
            End If
        End If
    End Sub

    ''' <summary>
    ''' Method that search for an specific Student when the user write on the searcher textBox
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub txtBxSearchStudent_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtBxSearchStudent.TextChanged
        txtMessageSearch.Visibility = Windows.Visibility.Hidden

        'Starts searching the use introduces at least 3 characters
        If txtBxSearchStudent.Text.Length >= 3 Then
            _listStudents = Application.oStudentManager.getAllStudents(txtBxSearchStudent.Text)

            'Show message: Not results found
            If _listStudents.Count = 0 Then
                txtMessageSearch.Visibility = Windows.Visibility.Visible
            End If
        Else
            _listStudents = Application.oStudentManager.getAllStudents()
        End If

        lstBxStudents.ItemsSource = _listStudents
    End Sub

    ''' <summary>
    ''' Method that load the mark selected into the main grid
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub lstBxMarks_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles lstBxMarks.SelectionChanged
        'When you change the tab and you return to the Student tab the Selected Index is -1.
        If Me.lstBxStudents.SelectedIndex >= 0 And Me.lstBxMarks.SelectedIndex >= 0 Then
            _mark = _listMarks.Item(Me.lstBxMarks.SelectedIndex)
        End If

        btnSaveChangesMarks.IsEnabled = False
        btnEditMark.Visibility = Windows.Visibility.Visible
        btnCancelEditMark.Visibility = Windows.Visibility.Collapsed

        Me.gridTableMarks.DataContext = _mark
    End Sub

    ''' <summary>
    ''' Method that load all the marks of a Student from a group
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub cmbBxGroup_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbBxGroup.SelectionChanged
        If Not _student Is Nothing Then
            _listMarks = Application.oMarkManager.getGroupStudentMarks(_student.StudentID, cmbBxGroup.SelectedValue)
            lstBxMarks.ItemsSource = _listMarks

            If _listMarks.Count > 0 Then
                _mark = _listMarks(0)
                Me.gridTableMarks.DataContext = _mark

                'Load Years into comboBox
                cmbBxYear.ItemsSource = Enumerable.Range(1950, DateTime.Today.Year).ToList()
                cmbBxYear.SelectedValue = DateTime.Today.Year
                cmbBxMonth.SelectedIndex = _mark.getTerm()

                btnEditMark.IsEnabled = True
            Else
                btnEditMark.IsEnabled = False
                Me.gridTableMarks.DataContext = Nothing
            End If
        End If
    End Sub

    ''' <summary>
    ''' Method that adds a new mark
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub btnAddMark_Click(sender As Object, e As RoutedEventArgs) Handles btnAddMark.Click
        Dim addMarkDialog As New dlgAddMark(_student.StudentID, cmbBxGroup.SelectedValue)
        addMarkDialog.ShowDialog()

        'Refresh list in case of there are new marks
        refreshMarkList()
    End Sub

    ''' <summary>
    ''' Method that allow a user to edit the information of a Student
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub btnEditStudent_Click(sender As Object, e As RoutedEventArgs) Handles btnEditMark.Click
        btnSaveChangesMarks.IsEnabled = True
        btnEditMark.Visibility = Windows.Visibility.Collapsed
        btnCancelEditMark.Visibility = Windows.Visibility.Visible
    End Sub

    ''' <summary>
    ''' Method that cancels the edition of a student
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub btnCancelEditStudent_Click(sender As Object, e As RoutedEventArgs) Handles btnCancelEditMark.Click
        btnSaveChangesMarks.IsEnabled = False
        btnEditMark.Visibility = Windows.Visibility.Visible
        btnCancelEditMark.Visibility = Windows.Visibility.Collapsed

        'Remove changes
        gridTableMarks.DataContext = _mark
        cmbBxYear.SelectedValue = DateTime.Today.Year
        cmbBxMonth.SelectedIndex = _mark.getTerm()
    End Sub

    ''' <summary>
    ''' Method that save the changes made on a Mark
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub btnSaveChangesMarks_Click(sender As Object, e As RoutedEventArgs) Handles btnSaveChangesMarks.Click
        Dim numReg As Integer

        numReg = Application.oMarkManager.updateMark(_mark)

        If numReg > 0 Then
            'Show messageBox confirm
            Dim messageBoxResult As MessageBoxResult
            Dim messageBoxOk As New msgBxOk(CType(FindResource("message_box.save_changes"), String), CType(FindResource("message_box.save_changes_success"), String))
            messageBoxResult = messageBoxOk.ShowMessageBox()

            'Refresh student list
            btnSaveChangesMarks.IsEnabled = False
            btnEditMark.Visibility = Windows.Visibility.Visible
            btnCancelEditMark.Visibility = Windows.Visibility.Collapsed
        Else
            'Show messageBox Error
            Dim messageBoxResult2 As MessageBoxResult
            Dim messageBoxOk2 As New msgBxOk(CType(FindResource("message_box.error_title"), String), CType(FindResource("message_box.error_message"), String))
            messageBoxResult2 = messageBoxOk2.ShowMessageBox()
        End If
    End Sub

    ''' <summary>
    ''' Method that is executed when the user press a button from the
    ''' Marks List
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub listMarksClick(sender As Object, e As RoutedEventArgs)
        Dim numReg As Integer
        Dim originalSource As Control = e.OriginalSource
        Dim listBoxItem As ListBoxItem = originalSource.TemplatedParent
        Dim button As Button = CType(e.OriginalSource, Button)
        Dim mark As clsMark = CType(listBoxItem.DataContext, clsMark)

        'If the pressede button is the Remove Student
        If button.Name.Equals("btnRemoveMark") Then
            Dim messageBoxResult As MessageBoxResult
            Dim messageBoxYesNo As New msgBxYesNo(CType(FindResource("message_box.delete_mark"), String), CType(FindResource("message_box.delete_mark_question"), String))

            messageBoxResult = messageBoxYesNo.ShowMessageBox()

            'Delete Mark
            If messageBoxResult = Windows.MessageBoxResult.Yes Then
                numReg = Application.oMarkManager.deleteMark(mark.StudentID, mark.GroupID, mark.DateMark)

                If numReg > 0 Then
                    'Refresh lists
                    refreshMarkList()
                Else
                    'Show messageBox Error
                    Dim messageBoxResult2 As MessageBoxResult
                    Dim messageBoxOk2 As New msgBxOk(CType(FindResource("message_box.error_title"), String), CType(FindResource("message_box.error_message"), String))
                    messageBoxResult2 = messageBoxOk2.ShowMessageBox()
                End If
            End If
        ElseIf button.Name.Equals("btnPrintMark") Then
            'Generate Receipt
            Dim generateMark As New clsGenerateMark(_student, CInt(txtTeacherID.Text), _mark)
            generateMark.generateReceipt()
        End If
    End Sub

    ''' <summary>
    ''' Method that refresh the Marks ListBox
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub refreshMarkList()
        _listMarks = Application.oMarkManager.getGroupStudentMarks(_student.StudentID, cmbBxGroup.SelectedValue)
        lstBxMarks.ItemsSource = _listMarks

        If _listMarks.Count > 0 Then
            btnEditMark.IsEnabled = True
        Else
            btnEditMark.IsEnabled = False
        End If
    End Sub
End Class
