Public Class ctrlTimetableTab
    Private _timeOfDay As Integer '0:Morning 1:Afternoon 2:All day
    Private _groupID As Integer
    Private _teacherID As Integer
    Private _listGroups As List(Of clsGroup)
    Private _listTeachers As List(Of clsTeacher)

    ''' <summary>
    ''' Method that get all the Teachers from the database when the control is loaded
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub ctrlTimetableTab_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        'Get all the Teachers from the database
        _listTeachers = Application.oTeacherManager.getAllTeachers()
        cmbBxSearchTeacher.ItemsSource = _listTeachers
        cmbBxSearchTeacher.SelectedIndex = 0

        'Load Timetable
        Dim timeOfDay As Integer = Application.oTimetableManager.getTimeOfDayGroup(_groupID)
        If timeOfDay = 0 Then
            _timeOfDay = 0
        ElseIf timeOfDay = 1 Then
            _timeOfDay = 1
        End If
        gridTimetableControl.TeacherID = _teacherID
        gridTimetableControl.GroupID = _groupID
        gridTimetableControl.updateTimetable()
    End Sub

    ''' <summary>
    ''' Method that search for specific Timetable of the Groups of the teacher selected
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object SelectionChangedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub cmbBxSearchTeacher_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbBxSearchTeacher.SelectionChanged
        _teacherID = cmbBxSearchTeacher.SelectedValue
        _listGroups = Application.oGroupManager.getAllGroupsSearch(Nothing, _teacherID)
        cmbBxSearchGroup.ItemsSource = _listGroups

        gridTimetableControl.TeacherID = _teacherID
        gridTimetableControl.GroupID = _groupID
        gridTimetableControl.updateTimetable()
    End Sub

    ''' <summary>
    ''' Method that search for specific Timetable of a Groups selected
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object SelectionChangedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub cmbBxSearchGroup_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbBxSearchGroup.SelectionChanged
        _groupID = cmbBxSearchGroup.SelectedValue

        'Load Timetable
        Dim timeOfDay As Integer = Application.oTimetableManager.getTimeOfDayGroup(_groupID)
        If timeOfDay = 0 Then
            _timeOfDay = 0
        ElseIf timeOfDay = 1 Then
            _timeOfDay = 1
        End If
        gridTimetableControl.TeacherID = _teacherID
        gridTimetableControl.GroupID = _groupID
        gridTimetableControl.updateTimetable()
    End Sub

    ''' <summary>
    ''' Method that removes a timetable of a Group
    ''' </summary>
    ''' <param name="timetableID"></param>
    ''' <remarks></remarks>
    Private Sub gridTimetableControl_TimetableClass_DoubleClicked(timetableID As Integer) Handles gridTimetableControl.TimetableClass_DoubleClicked
        Dim numReg As Integer

        If Not cmbBxSearchGroup.SelectedValue Is Nothing Then
            'Remove hour
            Dim timetable As clsTimetable = Application.oTimetableManager.getTimetableClassByID(timetableID)

            'Only can remove hour if it is the group we are editing
            If timetable.GroupID = _groupID Then
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
        Else
            Dim messageBoxResult As MessageBoxResult
            Dim messageBoxOk As New msgBxOk(CType(FindResource("message_box.timetable_change_hour"), String), CType(FindResource("message_box.timetable_change_hour_error"), String))
            messageBoxResult = messageBoxOk.ShowMessageBox()
        End If
    End Sub

    ''' <summary>
    ''' Method that adds a timetable to a Group
    ''' </summary>
    ''' <param name="day">An String</param>
    ''' <param name="hour">An object Timespan</param>
    ''' <remarks></remarks>
    Private Sub gridTimetableControl_TimetableItem_DoubleClicked(day As String, hour As TimeSpan) Handles gridTimetableControl.TimetableItem_DoubleClicked
        Dim numReg As Integer

        If Not cmbBxSearchGroup.SelectedValue Is Nothing Then
            'Add hour
            Dim timetable As New clsTimetable
            With timetable
                .Day = day
                .Hour = hour
                .GroupID = _groupID
            End With

            'If there is no other class with that hour
            numReg = Application.oTimetableManager.insertTimetable(timetable)
        Else
            Dim messageBoxResult As MessageBoxResult
            Dim messageBoxOk As New msgBxOk(CType(FindResource("message_box.timetable_change_hour"), String), CType(FindResource("message_box.timetable_change_hour_error"), String))
            messageBoxResult = messageBoxOk.ShowMessageBox()
        End If
    End Sub
End Class
