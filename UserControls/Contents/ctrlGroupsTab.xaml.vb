Public Class ctrlGroupsTab
    Dim _listStudents As List(Of clsStudent)
    Dim _listGroups As List(Of clsGroup)
    Dim _group As New clsGroup

    ''' <summary>
    ''' Method that get all the groups from the database when the control is loaded
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub ctrlGroupsTab_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Dim englishLevel As List(Of clsEnglishLevel)

        'Default Status Buttons
        btnSaveChangesGroups.IsEnabled = False
        btnEditGroup.Visibility = Windows.Visibility.Visible
        btnCancelEditGroup.Visibility = Windows.Visibility.Collapsed
        cmbBxEnglishLevel.IsEnabled = False
        gridTimetable.IsEnabled = False

        'Load English Level into comboBox
        englishLevel = Application.oEnglishLevelManager.getEnglishLevels()
        cmbBxEnglishLevel.ItemsSource = englishLevel
        cmbBxSearchEnglishLevel.ItemsSource = englishLevel

        'Load Teacher into comboBox
        cmbBxSearchTeacher.ItemsSource = Application.oTeacherManager.getAllTeachers()
        cmbBxTeacherGroup.ItemsSource = Application.oTeacherManager.getAllTeachers()

        If TypeOf (gridGroupInformation.DataContext) Is clsGroup Then 'When the application is loaded the gridStudentInformation dataContext is an object clsConfigurationDataContext
            'Clear the grid from unsaved changes
            'Or In case of the dataContext it is set from another tab (Ex: View group on TabStudents) sets the Group selected from another tab
            _group = Application.oGroupManager.getGroup(CType(gridGroupInformation.DataContext, clsGroup).GroupID)
        End If

        'Get all the groups from the database
        _listGroups = Application.oGroupManager.getAllGroups()
        lstBxGroups.ItemsSource = _listGroups
        gridGroupInformation.DataContext = _group

        If _listGroups.Count > 0 Then
            'Do not show button to finish Group
            If Not _group.DateFinish = Nothing Then
                txtFinishDate.Visibility = Windows.Visibility.Visible
                btnFinishGroup.Visibility = Windows.Visibility.Collapsed
            Else
                txtFinishDate.Visibility = Windows.Visibility.Collapsed
                btnFinishGroup.Visibility = Windows.Visibility.Visible
            End If

            'Load Timetable
            Dim timeOfDay As Integer = Application.oTimetableManager.getTimeOfDayGroup(_group.GroupID)
            If timeOfDay = 0 Then
                gridTimetable.TimeDay = 0
            ElseIf timeOfDay = 1 Then
                gridTimetable.TimeDay = 1
            End If

            gridTimetable.Size = 1 'Small
            gridTimetable.TeacherID = _group.TeacherID
            gridTimetable.GroupID = _group.GroupID
            gridTimetable.updateTimetable()

            'Load Students
            refreshStudentList()
        Else
            Me.gridGroupInformation.IsEnabled = False
            btnEditGroup.IsEnabled = False
            lstBxGroups.DataContext = Nothing
            lstBxStudentGroup.ItemsSource = Nothing
            cmbBxEnglishLevel.IsEnabled = False
            gridListStudents.IsEnabled = False
        End If
    End Sub

    ''' <summary>
    ''' Method that  when a student is selected from the listBox
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub lstBxGroups_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles lstBxGroups.SelectionChanged
        'When you change the tab and you return to the Student tab the Selected Index is -1.
        If Me.lstBxGroups.SelectedIndex >= 0 Then
            _group = _listGroups.Item(Me.lstBxGroups.SelectedIndex)
        End If

        btnSaveChangesGroups.IsEnabled = False
        btnEditGroup.Visibility = Windows.Visibility.Visible
        btnCancelEditGroup.Visibility = Windows.Visibility.Collapsed
        cmbBxEnglishLevel.IsEnabled = False

        If Not _group Is Nothing Then
            Me.gridGroupInformation.DataContext = _group

            'Load Timetable
            Dim timeOfDay As Integer = Application.oTimetableManager.getTimeOfDayGroup(_group.GroupID)
            If timeOfDay = 0 Then
                gridTimetable.TimeDay = 0
            ElseIf timeOfDay = 1 Then
                gridTimetable.TimeDay = 1
            End If
            gridTimetable.TeacherID = _group.TeacherID
            gridTimetable.GroupID = _group.GroupID
            gridTimetable.updateTimetable()

            'Do not show button to finish Group
            If Not _group.DateFinish = Nothing Then
                txtFinishDate.Visibility = Windows.Visibility.Visible
                btnFinishGroup.Visibility = Windows.Visibility.Collapsed
            Else
                txtFinishDate.Visibility = Windows.Visibility.Collapsed
                btnFinishGroup.Visibility = Windows.Visibility.Visible
            End If
        End If
    End Sub

    ''' <summary>
    ''' Method that opens a new dlgNewGroup Window
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub btnNewGroup_Click(sender As Object, e As RoutedEventArgs) Handles btnNewGroup.Click
        Dim newGroupDialog As New dlgNewGroup()
        newGroupDialog.ShowDialog()

        'Refresh list in case of there are new groups
        refreshGroupList()
    End Sub

    ''' <summary>
    ''' Method that sets the finish Group date
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub btnFinishGroup_Click(sender As Object, e As RoutedEventArgs) Handles btnFinishGroup.Click
        Dim numReg As Integer
        Dim messageBoxResult As MessageBoxResult
        Dim messageBoxYesNo As New msgBxYesNo(CType(FindResource("message_box.finish_group"), String), CType(FindResource("message_box.finish_group_question"), String))
        messageBoxResult = messageBoxYesNo.ShowMessageBox()

        If messageBoxResult = messageBoxResult.Yes Then
            numReg = Application.oGroupManager.finishGroup(_group.GroupID)

            If numReg > 0 Then
                'Show messageBox confirm
                Dim messageBoxOk As New msgBxOk(CType(FindResource("message_box.finish_group"), String), CType(FindResource("message_box.finish_group_success"), String))
                messageBoxOk.ShowMessageBox()

                'Do not show button to finish Group
                txtFinishDate.Visibility = Windows.Visibility.Visible
                btnFinishGroup.Visibility = Windows.Visibility.Collapsed

                'Refresh Group
                _group = Application.oGroupManager.getGroup(txtGroupID.Text)
                gridGroupInformation.DataContext = _group
                refreshGroupList()
            Else
                'Show messageBox Error
                Dim messageBoxResult2 As MessageBoxResult
                Dim messageBoxOk2 As New msgBxOk(CType(FindResource("message_box.error_title"), String), CType(FindResource("message_box.error_message"), String))
                messageBoxResult2 = messageBoxOk2.ShowMessageBox()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Method that allow a user to edit the information of a Group
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub btnEditGroup_Click(sender As Object, e As RoutedEventArgs) Handles btnEditGroup.Click
        btnSaveChangesGroups.IsEnabled = True
        btnEditGroup.Visibility = Windows.Visibility.Collapsed
        btnCancelEditGroup.Visibility = Windows.Visibility.Visible
        cmbBxEnglishLevel.IsEnabled = True
        cmbBxTeacherGroup.IsEnabled = True
        gridTimetable.IsEnabled = True
    End Sub

    ''' <summary>
    ''' Method that cancels the edition of a student
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub btnCancelEditStudent_Click(sender As Object, e As RoutedEventArgs) Handles btnCancelEditGroup.Click
        btnSaveChangesGroups.IsEnabled = False
        btnEditGroup.Visibility = Windows.Visibility.Visible
        btnCancelEditGroup.Visibility = Windows.Visibility.Collapsed
        cmbBxEnglishLevel.IsEnabled = False
        gridTimetable.IsEnabled = False

        _group = Application.oGroupManager.getGroup(_group.GroupID)
        gridGroupInformation.DataContext = _group
    End Sub

    ''' <summary>
    ''' Method that save the changes made on a Group
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub btnSaveChangesStudents_Click(sender As Object, e As RoutedEventArgs) Handles btnSaveChangesGroups.Click
        Dim numReg As Integer
        Dim aux1() As String = txtFeeInscription.Text.Split(" ")
        Dim aux2() As String = txtFeeMonthly.Text.Split(" ")
        Dim validation As Boolean = True
        lblErrorFeeInscription.Visibility = Windows.Visibility.Collapsed
        lblErrorFeeMonthly.Visibility = Windows.Visibility.Collapsed

        'Inscription fee
        If txtFeeInscription.Text = "" Then
            _group.FeeInscription = 0
        Else
            If Double.TryParse(aux1(0), New Double) Then
                If aux1(0) < 0 Then
                    validation = False
                    lblErrorFeeInscription.Visibility = Windows.Visibility.Visible
                End If
            Else
                validation = False
                lblErrorFeeInscription.Visibility = Windows.Visibility.Visible
            End If
        End If

        'Monthly fee
        If txtFeeMonthly.Text = "" Then
            _group.FeeMonthly = 0
        Else
            If Double.TryParse(aux2(0), New Double) Then
                If aux2(0) < 0 Then
                    validation = False
                    lblErrorFeeMonthly.Visibility = Windows.Visibility.Visible
                End If
            Else
                validation = False
                lblErrorFeeMonthly.Visibility = Windows.Visibility.Visible
            End If
        End If

        If validation Then
            'Save Group
            numReg = Application.oGroupManager.updateGroup(_group)

            If numReg > 0 Then
                'Show messageBox confirm
                Dim messageBoxResult As MessageBoxResult
                Dim messageBoxOk As New msgBxOk(CType(FindResource("message_box.save_changes"), String), CType(FindResource("message_box.save_changes_success"), String))
                messageBoxResult = messageBoxOk.ShowMessageBox()

                'Refresh Listgroup
                refreshGroupList()
            Else
                'Show messageBox Error
                Dim messageBoxResult2 As MessageBoxResult
                Dim messageBoxOk2 As New msgBxOk(CType(FindResource("message_box.error_title"), String), CType(FindResource("message_box.error_message"), String))
                messageBoxResult2 = messageBoxOk2.ShowMessageBox()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Method that refresh the Group ListBox
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub refreshGroupList()
        _listGroups = Application.oGroupManager.getAllGroups()
        lstBxGroups.ItemsSource = _listGroups

        If _listGroups.Count > 0 Then
            Me.gridGroupInformation.IsEnabled = True
            gridListStudents.IsEnabled = True
            btnEditGroup.IsEnabled = True
        Else
            Me.gridGroupInformation.IsEnabled = False
            gridListStudents.IsEnabled = False
            btnSaveChangesGroups.IsEnabled = False
            btnEditGroup.IsEnabled = False
        End If

        btnSaveChangesGroups.IsEnabled = False
        btnEditGroup.Visibility = Windows.Visibility.Visible
        btnCancelEditGroup.Visibility = Windows.Visibility.Collapsed
        cmbBxEnglishLevel.IsEnabled = False
        cmbBxTeacherGroup.IsEnabled = False
        gridTimetable.IsEnabled = False

        If Not _group Is Nothing Then
            'Load Timetable
            Dim timeOfDay As Integer = Application.oTimetableManager.getTimeOfDayGroup(_group.GroupID)
            If timeOfDay = 0 Then
                gridTimetable.TimeDay = 0
            ElseIf timeOfDay = 1 Then
                gridTimetable.TimeDay = 1
            End If
            gridTimetable.TeacherID = _group.TeacherID
            gridTimetable.GroupID = _group.GroupID
            gridTimetable.updateTimetable()
        End If
    End Sub

    ''' <summary>
    ''' Method that refresh the Students ListBox
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub refreshStudentList()
        _group = Application.oGroupManager.getGroup(_group.GroupID)
        Me.gridGroupInformation.DataContext = _group
    End Sub

    ''' <summary>
    ''' Method that search for specific Groups
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object SelectionChangedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub cmbBxSearchEnglishLevel_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbBxSearchEnglishLevel.SelectionChanged
        Dim teacherID As Integer
        Dim englishLevel As String = ""
        txtMessageSearch.Visibility = Windows.Visibility.Hidden

        If Not cmbBxSearchEnglishLevel.SelectedValue Is Nothing Or Not cmbBxSearchTeacher.SelectedValue Is Nothing Then
            If cmbBxSearchTeacher.SelectedValue Is Nothing Then
                teacherID = 0
            Else
                teacherID = cmbBxSearchTeacher.SelectedValue + 1
            End If

            If cmbBxSearchEnglishLevel.SelectedValue Is Nothing Then
                englishLevel = Nothing
            Else
                englishLevel = cmbBxSearchEnglishLevel.SelectedValue
            End If

            _listGroups = Application.oGroupManager.getAllGroupsSearch(englishLevel, teacherID)

            'Show message: Not results found
            If _listGroups.Count = 0 Then
                txtMessageSearch.Visibility = Windows.Visibility.Visible
            End If

            lstBxGroups.ItemsSource = _listGroups
        Else
            _listGroups = Application.oGroupManager.getAllGroups()
            lstBxGroups.ItemsSource = _listGroups
        End If
    End Sub

    ''' <summary>
    ''' Method that search for specific Groups
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object SelectionChangedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub cmbBxSearchTeacher_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbBxSearchTeacher.SelectionChanged
        Dim teacherID As Integer
        Dim englishLevel As String = ""
        txtMessageSearch.Visibility = Windows.Visibility.Hidden

        If Not cmbBxSearchEnglishLevel.SelectedValue Is Nothing Or Not cmbBxSearchTeacher.SelectedValue Is Nothing Then
            If cmbBxSearchTeacher.SelectedValue Is Nothing Then
                teacherID = 0
            Else
                teacherID = cmbBxSearchTeacher.SelectedValue
            End If

            If cmbBxSearchEnglishLevel.SelectedValue Is Nothing Then
                englishLevel = Nothing
            Else
                englishLevel = cmbBxSearchEnglishLevel.SelectedValue
            End If

            _listGroups = Application.oGroupManager.getAllGroupsSearch(englishLevel, teacherID)

            'Show message: Not results found
            If _listGroups.Count = 0 Then
                txtMessageSearch.Visibility = Windows.Visibility.Visible
            End If

            lstBxGroups.ItemsSource = _listGroups
        Else
            _listGroups = Application.oGroupManager.getAllGroups()
            lstBxGroups.ItemsSource = _listGroups
        End If
    End Sub

    ''' <summary>
    ''' Method that cleans the English level filter
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub btnCleanFilterEnglishLevel_Click(sender As Object, e As RoutedEventArgs) Handles btnCleanFilterEnglishLevel.Click
        Dim teacherID As Integer
        Dim englishLevel As String = ""
        txtMessageSearch.Visibility = Windows.Visibility.Hidden

        'Clear cmbBxSearchEnglishLevel
        cmbBxSearchEnglishLevel.SelectedValue = Nothing

        If Not cmbBxSearchEnglishLevel.SelectedValue Is Nothing Or Not cmbBxSearchTeacher.SelectedValue Is Nothing Then
            If cmbBxSearchTeacher.SelectedValue Is Nothing Then
                teacherID = 0
            End If

            If cmbBxSearchEnglishLevel.SelectedValue Is Nothing Then
                englishLevel = Nothing
            End If

            _listGroups = Application.oGroupManager.getAllGroupsSearch(englishLevel, teacherID)

            'Show message: Not results found
            If _listGroups.Count = 0 Then
                txtMessageSearch.Visibility = Windows.Visibility.Visible
            End If

            lstBxGroups.ItemsSource = _listGroups
        Else
            _listGroups = Application.oGroupManager.getAllGroups()
            lstBxGroups.ItemsSource = _listGroups
        End If
    End Sub

    ''' <summary>
    ''' Method that cleans the Teacher filter
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub btnCleanFilterTeacher_Click(sender As Object, e As RoutedEventArgs) Handles btnCleanFilterTeacher.Click
        Dim teacherID As Integer
        Dim englishLevel As String = ""
        txtMessageSearch.Visibility = Windows.Visibility.Hidden

        'Clear cmbBxSearchTeacher
        cmbBxSearchTeacher.SelectedValue = Nothing

        If Not cmbBxSearchEnglishLevel.SelectedValue Is Nothing Or Not cmbBxSearchTeacher.SelectedValue Is Nothing Then
            If cmbBxSearchTeacher.SelectedValue Is Nothing Then
                teacherID = 0
            End If

            If cmbBxSearchEnglishLevel.SelectedValue Is Nothing Then
                englishLevel = Nothing
            End If

            _listGroups = Application.oGroupManager.getAllGroupsSearch(englishLevel, teacherID)

            'Show message: Not results found
            If _listGroups.Count = 0 Then
                txtMessageSearch.Visibility = Windows.Visibility.Visible
            End If

            lstBxGroups.ItemsSource = _listGroups
        Else
            _listGroups = Application.oGroupManager.getAllGroups()
            lstBxGroups.ItemsSource = _listGroups
        End If
    End Sub

    ''' <summary>
    ''' Method that opens a new dlgAddStudentToGroup Window
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub btnAddStudent_Click(sender As Object, e As RoutedEventArgs) Handles btnAddStudent.Click
        Dim addStudentToGroupDialog As New dlgAddStudentToGroup(_group.GroupID)
        addStudentToGroupDialog.ShowDialog()

        'Refresh list in case of there are new groups
        refreshStudentList()
    End Sub

    ''' <summary>
    ''' Method that is executed when the user press a button from the
    ''' Students List
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub listGroupStudentsClick(sender As Object, e As RoutedEventArgs)
        Dim numReg As Integer
        Dim originalSource As Control = e.OriginalSource
        Dim listBoxItem As ListBoxItem = originalSource.TemplatedParent
        Dim button As Button = CType(e.OriginalSource, Button)
        Dim student As clsStudent = CType(listBoxItem.DataContext, clsStudent)

        'If the press the button Remove Student
        If button.Name.Equals("btnRemoveStudent") Then
            Dim messageBoxResult As MessageBoxResult
            Dim messageBoxWithCheckBox As New msgBxYesNoWithCheckBox(CType(FindResource("message_box.remove_student"), String), CType(FindResource("message_box.remove_student_question"), String) & " " & student.Name & " " & student.Surname & "?", False, CType(FindResource("message_box.remove_payments_student"), String))

            messageBoxResult = messageBoxWithCheckBox.ShowMessageBox()

            'Delete Student
            If messageBoxResult = Windows.MessageBoxResult.Yes Then
                numReg = Application.oInscriptionManager.removeStudent(student.StudentID, txtGroupID.Text)

                If numReg > 0 Then
                    'Remove payments if checkbox is True
                    If messageBoxWithCheckBox.CheckBox = True Then
                        numReg = Application.oPaymentManager.deleteStudentPayment(student.StudentID, _group.GroupID)
                    End If

                    'Refresh list Students
                    refreshStudentList()
                End If

                If numReg <= 0 Then
                    'Show messageBox Error
                    Dim messageBoxResult2 As MessageBoxResult
                    Dim messageBoxOk2 As New msgBxOk(CType(FindResource("message_box.error_title"), String), CType(FindResource("message_box.error_message"), String))
                    messageBoxResult2 = messageBoxOk2.ShowMessageBox()
                End If
            End If


        End If
    End Sub

    ''' <summary>
    ''' Method that removes a timetable of a Group
    ''' </summary>
    ''' <param name="timetableID"></param>
    ''' <remarks></remarks>
    Private Sub gridTimetableControl_TimetableClass_DoubleClicked(timetableID As Integer) Handles gridTimetable.TimetableClass_DoubleClicked
        Dim numReg As Integer

        'Remove hour
        Dim timetable As clsTimetable = Application.oTimetableManager.getTimetableClassByID(timetableID)

        'Only can remove hour if it is the group we are editing
        If timetable.GroupID = _group.GroupID Then
            numReg = Application.oTimetableManager.deleteTimetable(timetableID)

            If numReg < 0 Then
                'Show messageBox Error
                Dim messageBoxResult2 As MessageBoxResult
                Dim messageBoxOk2 As New msgBxOk(CType(FindResource("message_box.error_title"), String), CType(FindResource("message_box.error_message"), String))
                messageBoxResult2 = messageBoxOk2.ShowMessageBox()
            End If
        Else
            Dim messageBoxResult As MessageBoxResult
            Dim messageBoxOk As New msgBxOk(CType(FindResource("message_box.timetable_change_hour"), String), CType(FindResource("message_box.timetable_change_hour_exists"), String))
            messageBoxResult = messageBoxOk.ShowMessageBox()
        End If
    End Sub

    ''' <summary>
    ''' Method that adds a timetable to a Group
    ''' </summary>
    ''' <param name="day">An String</param>
    ''' <param name="hour">An object Timespan</param>
    ''' <remarks></remarks>
    Private Sub gridTimetableControl_TimetableItem_DoubleClicked(day As String, hour As TimeSpan) Handles gridTimetable.TimetableItem_DoubleClicked
        Dim numReg As Integer

        'Add hour
        Dim timetable As New clsTimetable
        With timetable
            .Day = day
            .Hour = hour
            .GroupID = _group.GroupID
        End With

        'If there is no other class with that hour
        numReg = Application.oTimetableManager.insertTimetable(timetable)

        If numReg < 0 Then
            'Show messageBox Error
            Dim messageBoxResult2 As MessageBoxResult
            Dim messageBoxOk2 As New msgBxOk(CType(FindResource("message_box.error_title"), String), CType(FindResource("message_box.error_message"), String))
            messageBoxResult2 = messageBoxOk2.ShowMessageBox()
        End If
    End Sub
End Class
