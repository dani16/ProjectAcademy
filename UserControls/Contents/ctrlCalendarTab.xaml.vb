Public Class ctrlCalendarTab
    ''' <summary>
    ''' Method that load the calendar with the event of a teacher
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub ctrlCalendarTab_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        calendarControl.TeacherID = txtTeacherID.Text
    End Sub

    ''' <summary>
    ''' Method that shows the information of an Event
    ''' </summary>
    ''' <param name="calendarEventID">An Integer</param>
    ''' <remarks></remarks>
    Private Sub calendarControl_CalendarEvent_DoubleClicked(ByVal calendarEventID As Integer) Handles calendarControl.CalendarEvent_DoubleClicked
        Dim viewEventDialog As New dlgViewEvent(calendarEventID)
        viewEventDialog.ShowDialog()
    End Sub

    ''' <summary>
    ''' Method that creates a new Event
    ''' </summary>
    ''' <param name="dateItem">An object Date</param>
    ''' <remarks></remarks>
    Private Sub calendarControl_CalendarItem_DoubleClicked(dateItem As Date) Handles calendarControl.CalendarItem_DoubleClicked
        Dim addEventDialog As New dlgAddEvent(dateItem, txtTeacherID.Text)
        addEventDialog.ShowDialog()
    End Sub
End Class
