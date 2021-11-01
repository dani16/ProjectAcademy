Public Class ctrlHomeTab
    Private _listNotifications

    ''' <summary>
    ''' Method that get all the notifications of a teacher from the database when the control is loaded
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub ctrlHomeTab_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        _listNotifications = Application.oNotificationManager.getTeacherNotifications(txtTeacherID.Text)
        lstBxNotifications.ItemsSource = _listNotifications
    End Sub

    ''' <summary>
    ''' Method that creates a new Notification
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub btnNewNotification_Click(sender As Object, e As RoutedEventArgs) Handles btnNewNotification.Click
        Dim addNotificationDialog As New dlgAddNotification(txtTeacherID.Text)
        addNotificationDialog.ShowDialog()

        'Refresh list in case of there are new notification
        refreshNotificationList()
    End Sub

    ''' <summary>
    ''' Method that refresh the Notification ListBox
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub refreshNotificationList()
        _listNotifications = Application.oNotificationManager.getTeacherNotifications(txtTeacherID.Text)
        lstBxNotifications.ItemsSource = _listNotifications
    End Sub


    Private Sub lstBxNotifications_Click(sender As Object, e As RoutedEventArgs)
        Dim numReg As Integer
        Dim originalSource As Control = e.OriginalSource
        Dim listBoxItem As ListBoxItem = originalSource.TemplatedParent
        Dim button As Button = CType(e.OriginalSource, Button)
        Dim notification As clsNotification = CType(listBoxItem.DataContext, clsNotification)

        'If the pressede button is the Remove Notification
        If button.Name.Equals("btnDeleteNotification") Then
            Dim messageBoxResult As MessageBoxResult
            Dim messageBoxYesNo As New msgBxYesNo(CType(FindResource("message_box.delete_notification"), String), CType(FindResource("message_box.delete_notification_question"), String))

            messageBoxResult = messageBoxYesNo.ShowMessageBox()

            'Delete Notification
            If messageBoxResult = Windows.MessageBoxResult.Yes Then
                numReg = Application.oNotificationManager.deleteNotification(notification.NotificationID)

                If numReg > 0 Then
                    'Refresh lists
                    refreshNotificationList()
                Else
                    'Show messageBox Error
                    Dim messageBoxResult2 As MessageBoxResult
                    Dim messageBoxOk2 As New msgBxOk(CType(FindResource("message_box.error_title"), String), CType(FindResource("message_box.error_message"), String))
                    messageBoxResult2 = messageBoxOk2.ShowMessageBox()
                End If
            End If
        End If
    End Sub
End Class
