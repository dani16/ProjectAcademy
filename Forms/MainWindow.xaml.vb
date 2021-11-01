Class MainWindow
    ''' <summary>
    ''' Method that is execute when the application is loaded
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub MainWindow_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Dim listDefaulterPayments As List(Of clsPayment)
        Dim listEvents As List(Of clsCalendarEvent)
        Dim numReg As Integer

        'Generate Monthly payments of every inscripted student
        Application.oPaymentManager.generateMonthlyPayments()

        'Generate new Notifications
        'Clear notifications user
        numReg = Application.oNotificationManager.clearNotifications(Me.DataContext.UserDataContext.Teacher.TeacherID)

        'Payments Notifications
        listDefaulterPayments = Application.oPaymentManager.getDefaulterPayments()
        If listDefaulterPayments.Count > 0 Then
            Dim notification As New clsNotification
            Dim defaulters As String = Nothing

            notification.Subject = listDefaulterPayments.Count & " " & CType(FindResource("notification.defaulters"), String)

            For Each defaulter In listDefaulterPayments
                defaulters &= defaulter.Name & vbCrLf & "  " & " (" & defaulter.PaymentDescription & ")" & vbCr & vbCr
            Next

            notification.Detail = defaulters

            Application.oNotificationManager.insertNotification(notification)
        End If

        'Events notifications
        Dim dataContext As clsConfigurationDataContext = CType(Me.DataContext, clsConfigurationDataContext)
        listEvents = Application.oCalendarEventManager.getCalendarEventsToNotify(Me.DataContext.UserDataContext.Teacher.TeacherID, Me.DataContext.UserDataContext.Preferences)

        For Each eventCalendar In listEvents
            Dim notification As New clsNotification
            Dim type As String = Nothing

            Select Case eventCalendar.Type
                Case 0
                    type = CType(FindResource("label.exam"), String)
                Case 1
                    type = CType(FindResource("label.test"), String)
                Case 2
                    type = CType(FindResource("label.holiday"), String)
                Case 3
                    type = CType(FindResource("label.other"), String)
            End Select

            notification.Subject = type & " Event: " & eventCalendar.Subject
            notification.Detail = CType(FindResource("label.date_starting"), String) & ": " & eventCalendar.StartDate & vbCr &
                                    CType(FindResource("label.date_finish"), String) & ": " & eventCalendar.StartDate
            notification.EventCalendarID = eventCalendar.CalendarEventID
            notification.TeacherID = eventCalendar.TeacherID

            Application.oNotificationManager.insertNotification(notification)
        Next
    End Sub

    ''' <summary>
    ''' Method that ask for closing when the user close the application
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub MainWindow_Closing(sender As Object, e As ComponentModel.CancelEventArgs) Handles Me.Closing
        'Detect if the user is closing the window, and not loging out
        If Not Application.Current.Windows.Count >= 2 Then
            Dim messageBoxResult As MessageBoxResult
            Dim messageBoxYesNo As New msgBxYesNo(CType(FindResource("message_box.exit_app"), String), CType(FindResource("message_box.exit_app_question"), String))
            messageBoxResult = messageBoxYesNo.ShowMessageBox()

            If messageBoxResult = messageBoxResult.No Then
                'Cancel closing Application
                e.Cancel = True
            End If
        End If
    End Sub
End Class
