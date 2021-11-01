Public Class dlgAddEvent
    Private _eventDate As Date
    Private _teacherID As Integer

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Sub New(ByVal eventDate As Date, ByVal teacherID As Integer)
        Me._eventDate = eventDate
        Me._teacherID = teacherID
        InitializeComponent()
    End Sub

    Public Property EventDate() As Date
        Get
            Return _eventDate
        End Get
        Set(ByVal value As Date)
            _eventDate = value
        End Set
    End Property

    Public Property TeacherID() As Integer
        Get
            Return _teacherID
        End Get
        Set(ByVal value As Integer)
            _teacherID = value
        End Set
    End Property

    ''' <summary>
    ''' Method that loads the default dates.
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub dlgAddEvent_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Dim currentDate As Date = Date.Today

        txtStartingDate.Text = EventDate
        txtFinishDate.Text = EventDate
    End Sub

    ''' <summary>
    ''' Method that creates a new Event
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub btnCreateNewEvent_Click(sender As Object, e As RoutedEventArgs) Handles btnCreateNewEvent.Click
        Dim numReg As Integer
        Dim eventCalendar As New clsCalendarEvent
        Dim validation As Boolean = True

        'Create new event
        With eventCalendar
            'Valid Subject
            If Not Application.oValidator.validateEmptyString(txtSubject.Text) Then
                txtSubject.BorderBrush = Brushes.Red
                lblErrorSubject.Visibility = Windows.Visibility.Visible
                validation = False
            Else
                .Subject = txtSubject.Text
                txtSubject.ClearValue(TextBox.BorderBrushProperty)
                lblErrorSubject.Visibility = Windows.Visibility.Collapsed
            End If

            'Validate StartingDate
            If txtStartingDate.SelectedDate Is Nothing Then
                Dim dateStarting As Date = Date.Today
                .StartDate = dateStarting
                txtStartingDate.ClearValue(TextBox.BorderBrushProperty)
                lblErrorStartingDate.Visibility = Windows.Visibility.Collapsed
            Else
                If Not IsDate(txtStartingDate.SelectedDate) Or txtStartingDate.SelectedDate.Value.Year < 1754 Then
                    txtStartingDate.BorderBrush = Brushes.Red
                    lblErrorStartingDate.Visibility = Windows.Visibility.Visible
                    txtStartingDate.SelectedDate = Nothing
                    validation = False
                Else
                    .StartDate = txtStartingDate.SelectedDate
                    txtStartingDate.BorderBrush = Brushes.White
                    lblErrorStartingDate.Visibility = Windows.Visibility.Collapsed
                End If
            End If

            'Validate FinishDate
            If txtFinishDate.SelectedDate Is Nothing Then
                Dim finishStarting As Date = Date.Today
                .FinishDate = finishStarting
                txtFinishDate.ClearValue(TextBox.BorderBrushProperty)
                lblErrorFinishDate.Visibility = Windows.Visibility.Collapsed
            Else
                If Not IsDate(txtStartingDate.SelectedDate) Or txtStartingDate.SelectedDate.Value.Year < 1754 Then
                    txtFinishDate.BorderBrush = Brushes.Red
                    lblErrorFinishDate.Visibility = Windows.Visibility.Visible
                    txtFinishDate.SelectedDate = Nothing
                    validation = False
                Else
                    .FinishDate = txtFinishDate.SelectedDate
                    txtFinishDate.BorderBrush = Brushes.White
                    lblErrorFinishDate.Visibility = Windows.Visibility.Collapsed
                End If
            End If

            'Get Detail
            .Detail = txtDetail.Text

            'Get Type
            .Type = cmbBoxType.SelectedIndex

            'Get TeacherID
            .TeacherID = TeacherID
        End With

        'Insert event        
        If validation Then
            numReg = Application.oCalendarEventManager.insertEvent(eventCalendar)

            If numReg > 0 Then
                'Close Window
                Dim parentWindow As Window = Window.GetWindow(Me)
                parentWindow.Close()

                'Show messageBox confirm
                Dim messageBoxResult As MessageBoxResult
                Dim messageBoxOk As New msgBxOk(CType(FindResource("message_box.create_event"), String), CType(FindResource("message_box.create_event_success"), String))
                messageBoxResult = messageBoxOk.ShowMessageBox()
            Else
                'Show messageBox Error
                Dim messageBoxResult2 As MessageBoxResult
                Dim messageBoxOk2 As New msgBxOk(CType(FindResource("message_box.error_title"), String), CType(FindResource("message_box.error_message"), String))
                messageBoxResult2 = messageBoxOk2.ShowMessageBox()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Method that cancels the creation of a new USer
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub btnCancelNewStudent_Click(sender As Object, e As RoutedEventArgs) Handles btnCancelNewEvent.Click
        'Close Window
        Dim parentWindow As Window = Window.GetWindow(Me)
        parentWindow.Close()
    End Sub

    ''' <summary>
    ''' Method that check that the starting date is previous to the finsihDate
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub txtStartingDate_SelectedDateChanged(sender As Object, e As SelectionChangedEventArgs) Handles txtStartingDate.SelectedDateChanged
        If Not txtStartingDate.SelectedDate Is Nothing Then
            txtFinishDate.SelectedDate = txtStartingDate.SelectedDate
        Else
            txtStartingDate.SelectedDate = txtFinishDate.SelectedDate
        End If
    End Sub

    ''' <summary>
    ''' Method that check that the starting date is previous to the finsihDate
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub txtFinishDate_SelectedDateChanged(sender As Object, e As SelectionChangedEventArgs) Handles txtFinishDate.SelectedDateChanged
        If Not txtFinishDate.SelectedDate Is Nothing Then
            If txtStartingDate.SelectedDate > txtFinishDate.SelectedDate Then
                txtFinishDate.SelectedDate = txtStartingDate.SelectedDate
            ElseIf txtStartingDate.SelectedDate Is Nothing Then
                txtStartingDate.SelectedDate = txtFinishDate.SelectedDate
            End If
        Else
            txtFinishDate.SelectedDate = txtStartingDate.SelectedDate
        End If
    End Sub
End Class
