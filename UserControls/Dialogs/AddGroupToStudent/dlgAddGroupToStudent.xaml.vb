Public Class dlgAddGroupToStudent
    Private _studentID As Integer
    Private _group As clsGroup
    Private _listGroups As List(Of clsGroup)

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Sub New(ByVal studentID As Integer)
        Me._studentID = studentID
        InitializeComponent()
    End Sub

    Public Property StudentID() As Integer
        Get
            Return _studentID
        End Get
        Set(ByVal value As Integer)
            _studentID = value
        End Set
    End Property

    ''' <summary>
    ''' Method that set the necessary elements when the window is loaded
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub dlgAddGroupToTeacher_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Dim curApp As Application = Application.Current
        Dim mainWindow As Window = curApp.MainWindow
        Dim englishLevel As List(Of clsEnglishLevel)

        'Set position of dialog on the center of the screen
        Me.Left = mainWindow.Left + (mainWindow.Width - Me.ActualWidth) / 2
        Me.Top = mainWindow.Top + (mainWindow.Height - Me.ActualHeight) / 2

        'Load English Level into comboBox
        englishLevel = Application.oEnglishLevelManager.getEnglishLevels()
        cmbBxSearchEnglishLevel.ItemsSource = englishLevel

        'Load Groups
        _listGroups = Application.oGroupManager.getGroupsForStudent(_studentID)

        If _listGroups.Count > 0 Then
            lstBxGroups.ItemsSource = _listGroups
            txtMessageSearch.Visibility = Windows.Visibility.Collapsed
        Else
            txtMessageSearch.Visibility = Windows.Visibility.Visible
        End If
    End Sub

    ''' <summary>
    ''' Method that search for specific Groups
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object SelectionChangedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub cmbBxSearchEnglishLevel_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbBxSearchEnglishLevel.SelectionChanged
        txtMessageSearch.Visibility = Windows.Visibility.Hidden

        If Not cmbBxSearchEnglishLevel.SelectedValue Is Nothing Then
            _listGroups = Application.oGroupManager.getGroupsWithNoTeacher(cmbBxSearchEnglishLevel.SelectedValue)

        Else
            _listGroups = Application.oGroupManager.getGroupsWithNoTeacher()
        End If

        lstBxGroups.ItemsSource = _listGroups

        'Show message: Not results found
        If _listGroups.Count = 0 Then
            txtMessageSearch.Visibility = Windows.Visibility.Visible
        End If
    End Sub

    ''' <summary>
    ''' Method that cleans the English level filter
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub btnCleanFilterEnglishLevel_Click(sender As Object, e As RoutedEventArgs) Handles btnCleanFilterEnglishLevel.Click
        Dim englishLevel As String = ""
        txtMessageSearch.Visibility = Windows.Visibility.Hidden

        'Clear cmbBxSearchEnglishLevel
        cmbBxSearchEnglishLevel.SelectedValue = Nothing

        _listGroups = Application.oGroupManager.getGroupsWithNoTeacher()
        lstBxGroups.ItemsSource = _listGroups
    End Sub

    ''' <summary>
    ''' Method that loads a group to the create grid when a group is selected from the listBox
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub lstBxGroups_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles lstBxGroups.SelectionChanged
        'When you change the tab and you return to the Student tab the Selected Index is -1.
        If Me.lstBxGroups.SelectedIndex >= 0 Then
            _group = _listGroups.Item(Me.lstBxGroups.SelectedIndex)
        End If

        Me.gridAddGroup.DataContext = _group

        'Load timetable
        Dim timeOfDay As Integer = Application.oTimetableManager.getTimeOfDayGroup(_group.GroupID)
        If timeOfDay = 0 Then
            gridTimetable.TimeDay = 0
        ElseIf timeOfDay = 1 Then
            gridTimetable.TimeDay = 1
        End If
        gridTimetable.TeacherID = _group.TeacherID
        gridTimetable.GroupID = _group.GroupID
        gridTimetable.Size = 1 'Small
        gridTimetable.ShowTeacherOtherGroups = False
        gridTimetable.updateTimetable()
    End Sub

    ''' <summary>
    ''' Method that cancels the addition of a student to a group
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub btnCancelAddGroup_Click(sender As Object, e As RoutedEventArgs) Handles btnCancelAddGroup.Click
        'Close Window
        Dim parentWindow As Window = Window.GetWindow(Me)
        parentWindow.Close()
    End Sub

    ''' <summary>
    ''' Method that add a student to a group
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub btnAddGroup_Click(sender As Object, e As RoutedEventArgs) Handles btnAddGroup.Click
        Dim numReg As Integer
        Dim student As New clsStudent
        Dim validation As Boolean = True

        'Add Student
        student = Application.oStudentManager.getStudent(_studentID)
        _group.addStudent(student)

        'Check if the student to add is not the current Teacher of the group
        If _group.TeacherID <> 0 Then
            If Application.oTeacherManager.getTeacher(_group.TeacherID).PersonID = student.PersonID Then
                validation = False
            End If
        End If

        'Check if the student to add is not the current Teacher of the group
        If validation Then
            numReg = Application.oGroupManager.updateGroup(_group)

            If numReg > 0 Then
                'Close Window
                Dim parentWindow As Window = Window.GetWindow(Me)
                parentWindow.Close()

                'Show messageBox confirm
                Dim messageBoxResult As MessageBoxResult
                Dim messageBoxOk As New msgBxOk(CType(FindResource("message_box.add_group_to_student"), String), CType(FindResource("message_box.group_success_student"), String))
                messageBoxResult = messageBoxOk.ShowMessageBox()
            Else
                'Show messageBox Error
                Dim messageBoxResult2 As MessageBoxResult
                Dim messageBoxOk2 As New msgBxOk(CType(FindResource("message_box.error_title"), String), CType(FindResource("message_box.error_message"), String))
                messageBoxResult2 = messageBoxOk2.ShowMessageBox()
            End If
        Else
            'Show messageBox Error
            Dim messageBoxResult As MessageBoxResult
            Dim messageBoxOk As New msgBxOk(CType(FindResource("message_box.error_add_student"), String), CType(FindResource("message_box.error_add_student_message"), String))
            messageBoxResult = messageBoxOk.ShowMessageBox()
        End If
    End Sub
End Class
