Public Class ctrlCalendarEvent
    ''' <summary>
    ''' Method that sets the color of the event border
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlCalendarEvent_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        If CType(txtType.Text, Integer) = 0 Then
            brdCalendarEvent.Style = CType(FindResource("styleCalendarExamnEvent"), Style)
        ElseIf CType(txtType.Text, Integer) = 1 Then
            brdCalendarEvent.Style = CType(FindResource("styleCalendarTestEvent"), Style)
        ElseIf CType(txtType.Text, Integer) = 2 Then
            brdCalendarEvent.Style = CType(FindResource("styleCalendarHolidayEvent"), Style)
        Else
            brdCalendarEvent.Style = CType(FindResource("styleCalendarOtherEvent"), Style)
        End If
    End Sub
End Class
