Public Class dlgAddNotification
    Private _teacherID As Integer

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Sub New(ByVal teacherID)
        Me._teacherID = teacherID
        InitializeComponent()
    End Sub

    Public Property TeacherID() As Integer
        Get
            Return _teacherID
        End Get
        Set(ByVal value As Integer)
            _teacherID = value
        End Set
    End Property

    ''' <summary>
    ''' Method that creates a new Event
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub btnCreateNewNotification_Click(sender As Object, e As RoutedEventArgs) Handles btnCreateNewNotification.Click
        Dim numReg As Integer
        Dim notification As New clsNotification
        Dim validation As Boolean = True

        'Create new notification
        With notification
            'Valid Subject
            If Not Application.oValidator.validateEmptyString(txtSubject.Text) Then
                txtSubject.BorderBrush = Brushes.Red
                lblErrorSubject.Visibility = Windows.Visibility.Visible
                validation = False
            Else
                .Subject = txtSubject.Text
                txtSubject.ClearValue(TextBox.BorderBrushProperty)
                lblErrorSubject.Visibility = Windows.Visibility.Collapsed
            End If            '

            'Get Detail
            .Detail = txtDetail.Text

            'Get Type
            .TeacherID = TeacherID
        End With

        'Insert notification        
        If validation Then
            numReg = Application.oNotificationManager.insertNotification(notification)

            If numReg > 0 Then
                'Close Window
                Dim parentWindow As Window = Window.GetWindow(Me)
                parentWindow.Close()

                'Show messageBox confirm
                Dim messageBoxResult As MessageBoxResult
                Dim messageBoxOk As New msgBxOk(CType(FindResource("message_box.create_notification"), String), CType(FindResource("message_box.create_notification_success"), String))
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
    ''' Method that cancels the creation of a new Notification
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub btnCancelNewNotification_Click(sender As Object, e As RoutedEventArgs) Handles btnCancelNewNotification.Click
        'Close Window
        Dim parentWindow As Window = Window.GetWindow(Me)
        parentWindow.Close()
    End Sub
End Class
