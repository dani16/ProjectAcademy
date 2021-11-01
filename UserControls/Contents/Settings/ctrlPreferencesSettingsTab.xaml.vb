Public Class ctrlPreferencesSettingsTab
    ''' <summary>
    ''' Method that automatically changes the language of the application
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub cmbBxDefaultLanguage_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbBxDefaultLanguage.SelectionChanged
        Dim culture As String
        If cmbBxDefaultLanguage.SelectedIndex = 0 Then
            culture = "en-US"
        Else
            culture = "es-ES"
        End If

        Application.SelectCulture(culture)
    End Sub

    ''' <summary>
    ''' Method that saves the preferences changes in the database
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub btnSaveChangesPreferences_Click(sender As Object, e As RoutedEventArgs) Handles btnSaveChangesPreferences.Click
        Dim numReg As Integer
        Dim preferences As New clsPreferences

        With preferences
            .LanguageDefault = cmbBxDefaultLanguage.SelectedIndex
            .ActivateEventNotifications = chckBxActivateEventNotifications.IsChecked
            .ActivatePaymentsNotifications = chckBxActivatePaymentsNotifications.IsChecked

            If txtDayNotificationEvent.Text = "" Then
                .DaysNotifyEvents = 0
            Else
                .DaysNotifyEvents = txtDayNotificationEvent.Text
            End If

            If txtDayNotificationExam.Text = "" Then
                .DaysNotifyExam = 0
            Else
                .DaysNotifyExam = txtDayNotificationExam.Text
            End If

            If txtDayNotificationTest.Text = "" Then
                .DaysNotifyTest = 0
            Else
                .DaysNotifyTest = txtDayNotificationTest.Text
            End If

            If txtDayNotificationHoliday.Text = "" Then
                .DaysNotifyHoliday = 0
            Else
                .DaysNotifyHoliday = txtDayNotificationHoliday.Text
            End If

            If txtDayNotificationOther.Text = "" Then
                .DaysNotifyOthers = 0
            Else
                .DaysNotifyOthers = txtDayNotificationOther.Text
            End If

            If txtDayNoticationPayment.Text = "" Then
                .DaysNotifyPayments = 0
            Else
                .DaysNotifyPayments = txtDayNoticationPayment.Text
            End If

            .UserID = txtUserID.Text
        End With

        numReg = Application.oPreferencesManager.savePreferencesChanges(preferences)

        If numReg > 0 Then
            Dim messageBoxResult As MessageBoxResult
            Dim messageBoxOk As New msgBxOk(CType(FindResource("message_box.save_changes"), String), CType(FindResource("message_box.save_changes_success"), String))
            messageBoxResult = messageBoxOk.ShowMessageBox()
        Else
            Dim messageBoxResult2 As MessageBoxResult
            Dim messageBoxOk2 As New msgBxOk(CType(FindResource("message_box.error_title"), String), CType(FindResource("message_box.error_message"), String))
            messageBoxResult2 = messageBoxOk2.ShowMessageBox()
        End If
    End Sub
End Class
