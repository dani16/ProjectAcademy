Public Class ctrlAdvancedSettingsTab
    ''' <summary>
    ''' Method that saves the changes on the advanced configuration in the database
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnSaveChangesAdvancedSettings_Click(sender As Object, e As RoutedEventArgs)
        Dim numReg As Integer
        Dim configuration As New clsConfiguration

        With configuration
            'Configuration
            .ShowHome = chckBxShowHome.IsChecked
            .ShowStudents = chckBxShowStudents.IsChecked
            .ShowGroups = chckBxShowGroups.IsChecked
            .ShowTeachers = chckBxShowTeachers.IsChecked
            .ShowAssessment = chckBxShowAssessment.IsChecked
            .ShowMarks = chckBxShowMarks.IsChecked
            .ShowCalendar = chckBxShowCalendar.IsChecked
            .ShowTimetable = chckBxShowTimetable.IsChecked
            .ShowPayments = chckBxShowPayment.IsChecked

            'Permissions
            .AllowNewStudents = chckBxAllowNewStudents.IsChecked
            .AllowEditStudents = chckBxAllowEditStudents.IsChecked
            .AllowDeleteStudents = chckBxAllowDeleteStudents.IsChecked
            .AllowNewGroups = chckBxAllowNewGroups.IsChecked
            .AllowEditGroups = chckBxAllowEditGroups.IsChecked
            .AllowFinishGroups = chckBxAllowDeleteGroups.IsChecked
            .AllowNewTeachers = chckBxAllowNewTeachers.IsChecked
            .AllowEditTeachers = chckBxAllowEditTeachers.IsChecked
            .AllowDeleteTeachers = chckBxAllowDeleteTeachers.IsChecked
            .AllowNewAssessment = chckBxAllowNewAssessment.IsChecked
            .AllowEditAssessment = chckBxAllowEditAssessment.IsChecked
            .AllowDeleteAssessment = chckBxAllowDeleteAssessment.IsChecked
            .AllowNewMarks = chckBxAllowNewMarks.IsChecked
            .AllowEditMarks = chckBxAllowEditMarks.IsChecked
            .AllowDeleteMarks = chckBxAllowDeleteMarks.IsChecked
            .AllowNewCalendar = chckBxAllowNewCalendar.IsChecked
            .AllowEditCalendar = chckBxAllowEditCalendar.IsChecked
            .AllowDeleteCalendar = chckBxAllowDeleteCalendar.IsChecked
            .AllowEditTimetable = chckBxAllowEditTimetable.IsChecked
            .AllowEditPayments = chckBxAllowEditPayments.IsChecked

            'Others
            .AllowChangePassword = chckBxAllowChangePassword.IsChecked
            .AllowEditPersonalData = chckBxAllowEditPersonalData.IsChecked
        End With

        numReg = Application.oConfigurationManager.saveConfigurationChanges(configuration)

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
