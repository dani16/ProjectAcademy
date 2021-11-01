Imports System.Windows

Public Class ctrlCalendarContent
    Private _teacherID As Integer
    Private _displayStartDate As Date = Date.Now.AddDays(-1 * (Date.Now.Day - 1))

    Public Sub New()
        Me._teacherID = 0
        InitializeComponent()
    End Sub

    Public Sub New(ByVal teacherID As Integer)
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
    ''' Method that load the calendar control days
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub ctrlCalendar_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Call buildCalendar()
    End Sub

    ''' <summary>
    ''' Method that bulids the calendar
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub buildCalendar()
        Dim daysInMonth As Integer = DateTime.DaysInMonth(_displayStartDate.Year, _displayStartDate.Month)
        Dim firstWeekDayMonth As Integer = CInt(System.Enum.ToObject(GetType(System.DayOfWeek), _displayStartDate.DayOfWeek))
        Dim weekCount As Integer = 0
        Dim weekCalendarControl As New ctrlCalendarWeek()

        'Clear the grid of all child controls
        gridMonthCalendar.Children.Clear()
        Windows.NameScope.SetNameScope(Me, New Windows.NameScope())

        'Add rows
        Call addRowsToCalendar(daysInMonth, firstWeekDayMonth)

        'Set txtCurrentDate to current Date
        txtCurrentDate.Text = MonthName(_displayStartDate.Month) & " " & _displayStartDate.Year

        For i As Integer = 1 To daysInMonth
            Dim dateAux As Date = New Date(_displayStartDate.Year, _displayStartDate.Month, i)

            'Check if it is a new week
            If i <> 1 And dateAux.DayOfWeek = DayOfWeek.Monday Then
                'Add existing ctrlCalendarWeek to the gridMonthCalendar
                Grid.SetRow(weekCalendarControl, weekCount)
                gridMonthCalendar.Children.Add(weekCalendarControl)

                'Create take a new weekrowcontrol
                weekCalendarControl = New ctrlCalendarWeek()
                weekCount += 1
            End If

            'Load each weekrow with a ctrlCalendarItem whose label is set to day number
            Dim calendarItem As New ctrlCalendarItem()
            calendarItem.txtNumberDay.Text = i.ToString
            calendarItem.Tag = i
            AddHandler calendarItem.MouseDoubleClick, AddressOf CalendarItem_DoubleClick

            'Customize calendar item for today:
            If (New Date(_displayStartDate.Year, _displayStartDate.Month, i)) = Date.Today Then
                calendarItem.brdCalendarItem.BorderThickness = New Thickness(4)
                calendarItem.brdCalendarItem.BorderBrush = Brushes.Blue
                calendarItem.brdCalendarItemHeader.Background = Brushes.AliceBlue
                calendarItem.txtNumberDay.Foreground = Brushes.Black
                calendarItem.stckPnlEvents.Background = Brushes.Wheat
            End If

            'Insert Calendar Event
            Dim listEvents As List(Of clsCalendarEvent) = Application.oCalendarEventManager.getCalendarEventByDate(TeacherID, dateAux)

            For Each item As clsCalendarEvent In listEvents
                Dim calendarEvent As New ctrlCalendarEvent()
                calendarEvent.Name = "Evnt" & item.CalendarEventID.ToString()
                calendarEvent.Tag = item.CalendarEventID
                calendarEvent.DataContext = item
                AddHandler calendarEvent.MouseDoubleClick, AddressOf CalendarEvent_DoubleClick
                calendarItem.stckPnlEvents.Children.Add(calendarEvent)
            Next

            'Add Calendar Item into a Column
            Dim columnDay As Integer = dateAux.DayOfWeek

            Select Case (columnDay)
                Case 1
                    columnDay = 0
                Case 2
                    columnDay = 1
                Case 3
                    columnDay = 2
                Case 4
                    columnDay = 3
                Case 5
                    columnDay = 4
                Case 6
                    columnDay = 5
                Case 0
                    columnDay = 6
            End Select

            Grid.SetColumn(calendarItem, columnDay)
            weekCalendarControl.gridWeekRow.Children.Add(calendarItem)
        Next

        'Add last week of the month to the gridMonthCalendar
        Grid.SetRow(weekCalendarControl, weekCount)
        gridMonthCalendar.Children.Add(weekCalendarControl)
    End Sub

    ''' <summary>
    ''' Method that loads the rows to the calendar
    ''' </summary>
    ''' <param name="days">An Integer</param>
    ''' <param name="firstWeekDayMonth">An Integer</param>
    ''' <remarks></remarks>
    Private Sub addRowsToCalendar(ByVal days As Integer, ByVal firstWeekDayMonth As Integer)
        'Clear calendar rows
        gridMonthCalendar.RowDefinitions.Clear()

        Dim endOffSetDays As Integer = 7 - (CInt(System.Enum.ToObject(GetType(System.DayOfWeek), _displayStartDate.AddDays(days - 1).DayOfWeek)) + 1)

        'Creates the necessary row for the month (4 or 5 weeks)
        For i As Integer = 1 To CInt((days + firstWeekDayMonth + endOffSetDays) / 7)
            Dim rowDef = New RowDefinition()
            gridMonthCalendar.RowDefinitions.Add(rowDef)
        Next
    End Sub

    ''' <summary>
    ''' Method that updates the month of the calendar
    ''' </summary>
    ''' <param name="MonthsToAdd"></param>
    ''' <remarks></remarks>
    Private Sub updateMonth(ByVal monthsToAdd As Integer)
        _displayStartDate = _displayStartDate.AddMonths(monthsToAdd)
        buildCalendar()
    End Sub

    ''' <summary>
    ''' Method that changes to a previous month on the calendar
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object MouseButtonEventArgs</param>
    ''' <remarks></remarks>
    Private Sub imgMonthGoPrev_MouseLeftButtonUp(sender As Object, e As MouseButtonEventArgs)
        updateMonth(-1)
    End Sub

    ''' <summary>
    ''' Method that changes to a next month on the calendar
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object MouseButtonEventArgs</param>
    ''' <remarks></remarks>
    Private Sub imgMonthGoNext_MouseLeftButtonUp(sender As Object, e As MouseButtonEventArgs)
        updateMonth(1)
    End Sub

    ''' <summary>
    ''' Method that creates a new event
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub btnAddEvent_Click(sender As Object, e As RoutedEventArgs) Handles btnAddEvent.Click
        Dim addEventDialog As New dlgAddEvent(Date.Today, TeacherID)
        addEventDialog.ShowDialog()

        'Refresh calendar
        buildCalendar()
    End Sub

#Region "Events"
    ''' <summary>
    ''' Event that is execute when the user clicks twice on a Calendar Event
    ''' </summary>
    ''' <param name="calendarEventID">An Integer</param>
    ''' <remarks></remarks>
    Public Event CalendarEvent_DoubleClicked(ByVal calendarEventID As Integer)

    Private Sub CalendarEvent_DoubleClick(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
        If e.Source.GetType Is GetType(ctrlCalendarEvent) Then
            If CType(e.Source, ctrlCalendarEvent).Tag IsNot Nothing Then
                'Raise event
                RaiseEvent CalendarEvent_DoubleClicked(CInt(CType(e.Source, ctrlCalendarEvent).Tag))
                Call buildCalendar()
            End If
            e.Handled = True
        End If
    End Sub

    ''' <summary>
    ''' Event that is execute when the user clicks twice on a Calendar Item
    ''' </summary>
    ''' <param name="dateItem">An object Date</param>
    ''' <remarks></remarks>
    Public Event CalendarItem_DoubleClicked(ByVal dateItem As Date)

    Private Sub CalendarItem_DoubleClick(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
        If e.Source.GetType Is GetType(ctrlCalendarItem) AndAlso FindVisualAncestor(GetType(ctrlCalendarEvent), e.OriginalSource) Is Nothing Then
            Dim dateNewEvent As Date = New Date(_displayStartDate.Year, _displayStartDate.Month, CInt(CType(e.Source, ctrlCalendarItem).Tag))
            RaiseEvent CalendarItem_DoubleClicked(dateNewEvent)
            Call buildCalendar()
            e.Handled = True
        End If
    End Sub

    Public Shared Function FindVisualAncestor( _
                ByVal ancestorType As System.Type, _
                ByVal visual As Media.Visual) As FrameworkElement

        While (visual IsNot Nothing AndAlso Not ancestorType.IsInstanceOfType(visual))
            visual = DirectCast(Media.VisualTreeHelper.GetParent(visual), Media.Visual)
        End While
        Return CType(visual, FrameworkElement)
    End Function
#End Region
End Class