Public Class dlgViewEvent
    Private _eventID As Integer

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Sub New(ByVal eventID As Integer)
        Me._eventID = eventID
        InitializeComponent()
    End Sub

    Public Property EventID() As Integer
        Get
            Return _eventID
        End Get
        Set(ByVal value As Integer)
            _eventID = value
        End Set
    End Property

    ''' <summary>
    ''' Method that load the event type into the combobox
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub dlgEditEvent_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Dim eventCalendar As clsCalendarEvent = Application.oCalendarEventManager.getCalendarEvent(EventID)

        'By default the gridEventInformation cannot be edit
        gridEventInformation.DataContext = eventCalendar
        gridEventInformation.IsEnabled = False

        'Set comboBox value Type
        cmbBoxType.SelectedIndex = eventCalendar.Type
    End Sub

    ''' <summary>
    ''' Method that updates an Event
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub btnUpdateEvent_Click(sender As Object, e As RoutedEventArgs) Handles btnSaveEvent.Click
        Dim numReg As Integer
        Dim eventCalendar As New clsCalendarEvent
        Dim validation As Boolean = True

        'Update event
        With eventCalendar
            'Get CalendarEventID
            .CalendarEventID = EventID

            'Get Type
            .Type = cmbBoxType.SelectedIndex

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
            If txtStartingDate.SelectedDate Is Nothing Or Not IsDate(txtStartingDate.SelectedDate) Or txtStartingDate.SelectedDate.Value.Year < 1754 Then
                txtStartingDate.BorderBrush = Brushes.Red
                lblErrorStartingDate.Visibility = Windows.Visibility.Visible
                txtStartingDate.SelectedDate = Nothing
                validation = False
            Else
                .StartDate = txtStartingDate.SelectedDate
                txtStartingDate.BorderBrush = Brushes.White
                lblErrorStartingDate.Visibility = Windows.Visibility.Collapsed
            End If

            'Validate FinishDate
            If txtFinishDate.SelectedDate Is Nothing Or Not IsDate(txtStartingDate.SelectedDate) Or txtStartingDate.SelectedDate.Value.Year < 1754 Then
                txtFinishDate.BorderBrush = Brushes.Red
                lblErrorFinishDate.Visibility = Windows.Visibility.Visible
                txtFinishDate.SelectedDate = Nothing
                validation = False
            Else
                .FinishDate = txtStartingDate.SelectedDate
                txtFinishDate.BorderBrush = Brushes.White
                lblErrorFinishDate.Visibility = Windows.Visibility.Collapsed
            End If

            'Get Detail
            .Detail = txtDetail.Text
        End With

        'Insert event        
        If validation Then
            numReg = Application.oCalendarEventManager.updateEvent(eventCalendar)

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
    ''' Method that deletes a Event
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub btnDeleteEvent_Click(sender As Object, e As RoutedEventArgs) Handles btnDeleteEvent.Click
        Dim numReg As Integer
        Dim messageBoxResult As MessageBoxResult
        Dim messageBoxYesNo As New msgBxYesNo(CType(FindResource("message_box.delete_event"), String), CType(FindResource("message_box.delete_event_question"), String))
        messageBoxResult = messageBoxYesNo.ShowMessageBox()

        If messageBoxResult = messageBoxResult.Yes Then
            'Close Dialog
            exitDialog()

            numReg = Application.oCalendarEventManager.deleteEvent(EventID)

            If numReg > 0 Then
                'Show messageBox confirm
                Dim messageBoxResult2 As MessageBoxResult
                Dim messageBoxOk As New msgBxOk(CType(FindResource("message_box.delete_event"), String), CType(FindResource("message_box.delete_event_success"), String))
                messageBoxResult2 = messageBoxOk.ShowMessageBox()
            Else
                'Show messageBox Error
                Dim messageBoxResult2 As MessageBoxResult
                Dim messageBoxOk2 As New msgBxOk(CType(FindResource("message_box.error_title"), String), CType(FindResource("message_box.error_message"), String))
                messageBoxResult2 = messageBoxOk2.ShowMessageBox()
            End If
        End If
    End Sub

    ''' <summary>
    ''' Method that enables the user to edit the event
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub btnEditEvent_Click(sender As Object, e As RoutedEventArgs) Handles btnEditEvent.Click
        gridEventInformation.IsEnabled = True
        stckPnlViewButtons.Visibility = Windows.Visibility.Collapsed
        stckPnlEditButtons.Visibility = Windows.Visibility.Visible
    End Sub

    ''' <summary>
    ''' Method that cancels the creation of a new USer
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub btnCancelEvent_Click(sender As Object, e As RoutedEventArgs) Handles btnCancelEvent.Click
        'Clear changes
        gridEventInformation.DataContext = Application.oCalendarEventManager.getCalendarEvent(EventID)

        gridEventInformation.IsEnabled = False
        stckPnlViewButtons.Visibility = Windows.Visibility.Visible
        stckPnlEditButtons.Visibility = Windows.Visibility.Collapsed
    End Sub

    ''' <summary>
    ''' Method that exits the window
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub btnExit_Click(sender As Object, e As RoutedEventArgs) Handles btnExit.Click
        exitDialog()
    End Sub

    ''' <summary>
    ''' Method that close the window
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub exitDialog()
        'Close Window
        Dim parentWindow As Window = Window.GetWindow(Me)
        parentWindow.Close()
    End Sub

End Class
