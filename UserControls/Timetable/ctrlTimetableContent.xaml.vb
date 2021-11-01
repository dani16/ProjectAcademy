Imports System.Windows

Public Class ctrlTimetableContent
    Private _groupID As Integer
    Private _teacherID As Integer
    Private _timeDay As Integer  '0: AM or 1: PM 
    Private _size As Integer '0: Normal or 1: Small(For GroupsTab)
    Private _showTeacherOtherGroups As Boolean

    Public Sub New()
        Me._size = 0
        Me._showTeacherOtherGroups = True
        InitializeComponent()
    End Sub

    Public Sub New(ByVal groupID As Integer, ByVal teacherID As Integer, ByVal timeDay As Integer)
        Me._groupID = groupID
        Me._teacherID = teacherID
        Me._timeDay = timeDay
        Me._size = 0
        Me._showTeacherOtherGroups = True
        InitializeComponent()
    End Sub

    Public Property GroupID() As Integer
        Get
            Return _groupID
        End Get
        Set(ByVal value As Integer)
            _groupID = value
        End Set
    End Property

    Public Property TeacherID() As Integer
        Get
            Return _teacherID
        End Get
        Set(ByVal value As Integer)
            _teacherID = value
        End Set
    End Property

    Public Property TimeDay() As Integer
        Get
            Return _timeDay
        End Get
        Set(ByVal value As Integer)
            _timeDay = value
        End Set
    End Property

    Public Property Size() As Integer
        Get
            Return _size
        End Get
        Set(ByVal value As Integer)
            _size = value
        End Set
    End Property

    Public Property ShowTeacherOtherGroups() As Boolean
        Get
            Return _showTeacherOtherGroups
        End Get
        Set(ByVal value As Boolean)
            _showTeacherOtherGroups = value
        End Set
    End Property

    ''' <summary>
    ''' Method that load the calendar control days
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub ctrlTimetable_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Call buildTimetable()
    End Sub

    ''' <summary>
    ''' Method that bulids the timetable
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub buildTimetable()
        'Clear the grid of all child controls
        gridTimetable.Children.Clear()

        'Add rows
        Call addRowsToTimetable()

        'Set time of day
        Dim hourAux As TimeSpan

        If _timeDay = 0 Then
            hourAux = New TimeSpan(0, 8, 0, 0, 0) '"8:00:00 AM"
        ElseIf _timeDay = 1 Then
            hourAux = New TimeSpan(0, 15, 0, 0, 0) '"15:00:00 PM"
        End If

        For i As Integer = 0 To 13 'Number of hour rows
            Dim weekTimetableControl As New ctrlTimetableWeek()

            'Add new ctrlTimetableWeek to a row gridTimetable
            Grid.SetRow(weekTimetableControl, i)
            gridTimetable.Children.Add(weekTimetableControl)

            'Set Time class to the firs column
            Dim timetableItem As New ctrlTimetableItem()
            Grid.SetColumn(timetableItem, 0)
            Dim dateAux As Date = hourAux.ToString
            weekTimetableControl.timeClass.Text = Format(dateAux, "HH:mm")

            'Days of the week      
            For j As Integer = 1 To 5
                'Get Day of the week
                Dim dayOfWeek As String = Nothing
                Select Case (j)
                    Case 1
                        dayOfWeek = "Mo"
                    Case 2
                        dayOfWeek = "Tu"
                    Case 3
                        dayOfWeek = "We"
                    Case 4
                        dayOfWeek = "Th"
                    Case 5
                        dayOfWeek = "Fr"
                End Select

                'Load each weekrow with a ctrlTimetableItemr
                timetableItem = New ctrlTimetableItem()
                timetableItem.Tag = dayOfWeek & "," & hourAux.ToString
                AddHandler timetableItem.MouseDoubleClick, AddressOf TimetableItem_DoubleClick

                'Get clsTimetableClass of this hour
                Dim listClassItem As New clsTimetable

                If ShowTeacherOtherGroups Then
                    listClassItem = Application.oTimetableManager.getTimetableClassTeacher(dayOfWeek, hourAux, TeacherID)
                Else
                    listClassItem = Application.oTimetableManager.getTimetableClassGroup(dayOfWeek, hourAux, GroupID)
                End If

                If Not listClassItem Is Nothing Then
                    If Size = 1 Then
                        Dim timetableClass As New ctrlTimetableClassSmall()
                        timetableClass.Tag = listClassItem.TimetableID
                        timetableClass.DataContext = listClassItem
                        If GroupID > 0 And Application.oTimetableManager.getTimetableClassGroup(dayOfWeek, hourAux, GroupID) Is Nothing Then
                            timetableClass.Opacity = 0.5
                        End If

                        AddHandler timetableClass.MouseDoubleClick, AddressOf TimetableClass_DoubleClick
                        timetableItem.stckPnlClass.Children.Add(timetableClass)
                    Else
                        Dim timetableClass As New ctrlTimetableClass()
                        timetableClass.Tag = listClassItem.TimetableID
                        timetableClass.DataContext = listClassItem
                        If GroupID > 0 And Application.oTimetableManager.getTimetableClassGroup(dayOfWeek, hourAux, GroupID) Is Nothing Then
                            timetableClass.Opacity = 0.5
                        End If

                        AddHandler timetableClass.MouseDoubleClick, AddressOf TimetableClass_DoubleClick
                        timetableItem.stckPnlClass.Children.Add(timetableClass)
                    End If
                End If

                Grid.SetColumn(timetableItem, j)
                weekTimetableControl.gridWeekRow.Children.Add(timetableItem)
            Next

            'Add hour
            hourAux += TimeSpan.FromMinutes(30)
        Next
    End Sub

    ''' <summary>
    ''' Method that loads the rows to the timetable
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub addRowsToTimetable()
        'Clear calendar rows
        gridTimetable.RowDefinitions.Clear()

        For i As Integer = 0 To 13 'Number hours
            Dim rowDef = New RowDefinition()
            gridTimetable.RowDefinitions.Add(rowDef)
        Next
    End Sub

    ''' <summary>
    ''' Method that updates the timetable
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub updateTimetable()
        buildTimetable()
    End Sub

    ''' <summary>
    ''' Method that load the Timetable of the morning
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object SelectionChangedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub rdBtnMorning_Checked(sender As Object, e As RoutedEventArgs) Handles rdBtnMorning.Checked
        TimeDay = 0
        Call updateTimetable()
    End Sub

    ''' <summary>
    ''' Method that load the Timetable of the afternoon
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object SelectionChangedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub rdBtnAfternoon_Checked(sender As Object, e As RoutedEventArgs) Handles rdBtnAfternoon.Checked
        TimeDay = 1
        Call updateTimetable()
    End Sub

#Region "Events"
    ''' <summary>
    ''' Event that is execute when the user clicks twice on a Timetable Class
    ''' </summary>
    ''' <param name="timetableID">An Integer</param>
    ''' <remarks></remarks>
    Public Event TimetableClass_DoubleClicked(ByVal timetableID As Integer)

    Private Sub TimetableClass_DoubleClick(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
        If e.Source.GetType Is GetType(ctrlTimetableClass) Then
            If CType(e.Source, ctrlTimetableClass).Tag IsNot Nothing Then
                'Raise event
                RaiseEvent TimetableClass_DoubleClicked(CInt(CType(e.Source, ctrlTimetableClass).Tag))
                Call buildTimetable()
            End If
            e.Handled = True
        ElseIf e.Source.GetType Is GetType(ctrlTimetableClassSmall) Then
            If CType(e.Source, ctrlTimetableClassSmall).Tag IsNot Nothing Then
                'Raise event
                RaiseEvent TimetableClass_DoubleClicked(CInt(CType(e.Source, ctrlTimetableClassSmall).Tag))
                Call buildTimetable()
            End If
            e.Handled = True
        End If
    End Sub

    ''' <summary>
    ''' Event that is execute when the user clicks twice on a Timetable Item
    ''' </summary>
    ''' <param name="day">An String</param>
    ''' <param name="hour">An object DateTime</param>
    ''' <remarks></remarks>
    Public Event TimetableItem_DoubleClicked(ByVal day As String, ByVal hour As TimeSpan)

    Private Sub TimetableItem_DoubleClick(ByVal sender As Object, ByVal e As MouseButtonEventArgs)
        If e.Source.GetType Is GetType(ctrlTimetableItem) AndAlso FindVisualAncestor(GetType(ctrlTimetableClass), e.OriginalSource) Is Nothing And FindVisualAncestor(GetType(ctrlTimetableClassSmall), e.OriginalSource) Is Nothing Then
            Dim timetableDayHour As String = CType(CType(e.Source, ctrlTimetableItem).Tag, String)
            Dim aux As String() = timetableDayHour.Split(",")
            Dim hours As String() = aux(1).Split(":")
            RaiseEvent TimetableItem_DoubleClicked(aux(0), New TimeSpan(0, hours(0), hours(1), 0, 0))
            Call buildTimetable()
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