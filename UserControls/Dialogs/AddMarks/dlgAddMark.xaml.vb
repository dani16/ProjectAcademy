Public Class dlgAddMark
    Private _studentID As Integer
    Private _groupID As Integer
    Private _listGroups As List(Of clsGroup)
    Private _mark As New clsMark

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Sub New(ByVal studentID As Integer, ByVal groupID As Integer)
        Me._studentID = studentID
        Me._groupID = groupID
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

    Public Property GroupID() As Integer
        Get
            Return _groupID
        End Get
        Set(ByVal value As Integer)
            _groupID = value
        End Set
    End Property

    ''' <summary>
    ''' Method that loads the necessary element to add a mark
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub dlgAddMark_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Dim student As clsStudent = Application.oStudentManager.getStudent(StudentID)
        Dim currentDate As Date = Date.Now

        'Student name
        txtName.Text = student.Name & " " & student.Surname

        'Group
        _listGroups = Application.oGroupManager.getStudentGroups(StudentID)
        cmbBxGroup.ItemsSource = _listGroups
        cmbBxGroup.SelectedValue = GroupID

        'Date Mark
        _mark.DateMark = Date.Now
        txtDateMark.DataContext = _mark
    End Sub

    ''' <summary>
    ''' Method that cancels the creation of a new Mark
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub btnCancelNewStudent_Click(sender As Object, e As RoutedEventArgs) Handles btnCancelAddMark.Click
        'Close Window
        Dim parentWindow As Window = Window.GetWindow(Me)
        parentWindow.Close()
    End Sub

    ''' <summary>
    ''' Method that creates a new Mark
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub btnAddMark_Click(sender As Object, e As RoutedEventArgs) Handles btnAddMark.Click
        Dim numReg As Integer
        Dim validation As Boolean = True

        'Create new event
        With _mark
            'Valid Group
            If cmbBxGroup.SelectedValue Is Nothing Then
                validation = False
                'cmbBxGroup.BorderBrush = Brushes.Red
                lblErrorGroup.Visibility = Windows.Visibility.Visible
            Else
                .GroupID = cmbBxGroup.SelectedValue
                cmbBxGroup.ClearValue(TextBox.BorderBrushProperty)
                lblErrorGroup.Visibility = Windows.Visibility.Collapsed
            End If

            'Get StudentID
            .StudentID = _studentID

            'Get DateMark
            .DateMark = Date.Now

            'Get Marks
            .Listening = txtListening.NumericValue
            .Speaking = txtListening.NumericValue
            .Reading = txtReading.NumericValue
            .Writing = txtWriting.NumericValue
            .Exam = txtExamn.NumericValue
            .Overall = txtOverall.NumericValue
        End With

        'Insert event        
        If validation Then
            numReg = Application.oMarkManager.insertMark(_mark)

            If numReg > 0 Then
                'Close Window
                Dim parentWindow As Window = Window.GetWindow(Me)
                parentWindow.Close()

                'Show messageBox confirm
                Dim messageBoxResult As MessageBoxResult
                Dim messageBoxOk As New msgBxOk(CType(FindResource("message_box.create_mark"), String), CType(FindResource("message_box.create_mark_success"), String))
                messageBoxResult = messageBoxOk.ShowMessageBox()
            Else
                'Show messageBox Error
                Dim messageBoxResult2 As MessageBoxResult
                Dim messageBoxOk2 As New msgBxOk(CType(FindResource("message_box.error_title"), String), CType(FindResource("message_box.error_message"), String))
                messageBoxResult2 = messageBoxOk2.ShowMessageBox()
            End If
        End If
    End Sub
End Class
