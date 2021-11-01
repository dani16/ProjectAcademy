Public Class dlgAddStudentToGroup
    Private _groupID As Integer
    Private _student As clsStudent
    Private _listStudents As List(Of clsStudent)

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Sub New(ByVal groupID As Integer)
        Me._groupID = groupID
        InitializeComponent()
    End Sub

    Public Property GroupID() As Integer
        Get
            Return _groupID
        End Get
        Set(ByVal value As Integer)
            _groupID = value
        End Set
    End Property

    ''' <summary>
    ''' Method that set the necessary elements when the window is loaded
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub dlgAddStudentToGroup_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Dim curApp As Application = Application.Current
        Dim mainWindow As Window = curApp.MainWindow

        'Set position of dialog on the center of the screen
        Me.Left = mainWindow.Left + (mainWindow.Width - Me.ActualWidth) / 2
        Me.Top = mainWindow.Top + (mainWindow.Height - Me.ActualHeight) / 2

    End Sub

    ''' <summary>
    ''' Method that search for Studnets when the user write on the searcher textBox
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub txtBxSearchStudent_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtBxSearchStudent.TextChanged
        txtMessageSearch.Visibility = Windows.Visibility.Hidden

        If txtBxSearchStudent.Text.Length >= 3 Then
            'Get deleted people from the database
            _listStudents = Application.oStudentManager.getNotGroupStudents(GroupID, txtBxSearchStudent.Text)
            lstBxStudents.ItemsSource = _listStudents

            'Show message: Not results found
            If _listStudents.Count = 0 Then
                txtMessageSearch.Visibility = Windows.Visibility.Visible
            End If
        Else
            lstBxStudents.ItemsSource = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Method that  when a student is selected from the listBox
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub lstBxStudents_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles lstBxStudents.SelectionChanged
        'When you change the tab and you return to the Student tab the Selected Index is -1.
        If Me.lstBxStudents.SelectedIndex >= 0 Then
            _student = _listStudents.Item(Me.lstBxStudents.SelectedIndex)
        End If

        Me.gridAddStudent.DataContext = _student
    End Sub

    ''' <summary>
    ''' Method that cancels the addition of a student to a group
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub btnCancelAddStudent_Click(sender As Object, e As RoutedEventArgs) Handles btnCancelAddStudent.Click
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
    Private Sub btnAddStudent_Click(sender As Object, e As RoutedEventArgs) Handles btnAddStudent.Click
        Dim numReg As Integer
        Dim validation As Boolean = True

        If Not _student Is Nothing Then
            'Check if the student to add is not the current Teacher of the group
            If Application.oGroupManager.getGroup(_groupID).TeacherID <> 0 Then
                If Application.oTeacherManager.getTeacher(Application.oGroupManager.getGroup(_groupID).TeacherID).PersonID = _student.PersonID Then
                    validation = False
                End If
            End If

            If validation Then
                numReg = Application.oInscriptionManager.studentInscription(_student.StudentID, GroupID)

                If numReg > 0 Then
                    'Close Window
                    Dim parentWindow As Window = Window.GetWindow(Me)
                    parentWindow.Close()

                    'Show messageBox confirm
                    Dim messageBoxResult As MessageBoxResult
                    Dim messageBoxOk As New msgBxOk(CType(FindResource("message_box.add_student_to_group"), String), CType(FindResource("message_box.student_success_group"), String))
                    messageBoxResult = messageBoxOk.ShowMessageBox()
                Else
                    'Show messageBox Error
                    Dim messageBoxResult As MessageBoxResult
                    Dim messageBoxOk As New msgBxOk(CType(FindResource("message_box.error_title"), String), CType(FindResource("message_box.error_message"), String))
                    messageBoxResult = messageBoxOk.ShowMessageBox()
                End If
            Else
                'Show messageBox Error
                Dim messageBoxResult As MessageBoxResult
                Dim messageBoxOk As New msgBxOk(CType(FindResource("message_box.error_add_student"), String), CType(FindResource("message_box.error_add_student_message"), String))
                messageBoxResult = messageBoxOk.ShowMessageBox()
            End If
        End If
    End Sub
End Class
