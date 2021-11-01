Public Class ctrlContentTabControl
    ''' <summary>
    ''' Method that changes from the HomeTab to the Notification selected
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub HomeTabItem_Click(sender As Object, e As RoutedEventArgs)
        Dim originalSource As Control = e.OriginalSource

        'Button view Group
        If originalSource.Name.Equals("btnViewNotification") Then
            'Button view Notification
            Dim listBoxItem As ListBoxItem = originalSource.TemplatedParent
            Dim notification As clsNotification = CType(listBoxItem.DataContext, clsNotification)

            If Not notification.EventCalendarID = 0 Then
                tbCtrlMain.SelectedValue = tabCalendar
            Else
                Dim payments As List(Of clsPayment) = Application.oPaymentManager.getDefaulterPayments()
                tbCtrlMain.SelectedValue = tabPayments
                paymentsTabItem.dataGridPayments.ItemsSource = payments
                paymentsTabItem.cmbBxYear.SelectedValue = Nothing
                paymentsTabItem.cmbBxMonth.SelectedValue = Nothing
            End If
        End If
    End Sub

    ''' <summary>
    ''' Method that changes from the StudentTab to the Group Tab
    ''' with the selected Group from the list Student Groups
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub StudentsTabItem_Click(sender As Object, e As RoutedEventArgs)
        Dim originalSource As Control = e.OriginalSource

        'Button view Group
        If originalSource.Name.Equals("btnViewGroup") Then
            Dim listBoxItem As ListBoxItem = originalSource.TemplatedParent
            Dim group As clsGroup = CType(listBoxItem.DataContext, clsGroup)
            tbCtrlMain.SelectedValue = tabGroups
            groupTabItem.gridGroupInformation.DataContext = group
        ElseIf originalSource.Name.Equals("btnViewMark") Then
            'Button view Marks
            Dim listBoxItem As ListBoxItem = originalSource.TemplatedParent
            Dim mark As clsMark = CType(listBoxItem.DataContext, clsMark)
            Dim student As clsStudent = Application.oStudentManager.getStudent(mark.StudentID)
            tbCtrlMain.SelectedValue = tabMarks
            markTabItem.gridStudentMarks.DataContext = student
            markTabItem.gridTableMarks.DataContext = mark
            markTabItem.gridDateMark.DataContext = mark
        End If
    End Sub

    ''' <summary>
    ''' Method that changes from the GroupTab to the Students Tab
    ''' with the selected Student from the list Group Students.
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub GroupTabItem_Click(sender As Object, e As RoutedEventArgs)
        Dim originalSource As Control = e.OriginalSource

        If originalSource.Name.Equals("btnViewStudent") Then
            Dim listBoxItem As ListBoxItem = originalSource.TemplatedParent
            Dim student As clsStudent = CType(listBoxItem.DataContext, clsStudent)
            tbCtrlMain.SelectedValue = tabStudents
            studentTabItem.gridStudentInformation.DataContext = student
        End If
    End Sub

    ''' <summary>
    ''' Method that changes from the TeacherTab to the Group Tab
    ''' with the selected Group from the list Student Groups
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub teacherTabItem_Click(sender As Object, e As RoutedEventArgs)
        Dim originalSource As Control = e.OriginalSource

        If originalSource.Name.Equals("btnViewGroup") Then
            Dim listBoxItem As ListBoxItem = originalSource.TemplatedParent
            Dim group As clsGroup = CType(listBoxItem.DataContext, clsGroup)
            tbCtrlMain.SelectedValue = tabGroups
            groupTabItem.gridGroupInformation.DataContext = group
        End If
    End Sub
End Class
