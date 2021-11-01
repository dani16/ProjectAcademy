' CLASS: clsCalendarEvent
'
' PROPERTIES:
'   _timetableID: integer, basic. Searchable.
'   _groupID: integer, basic. Searchable.
'   _day: String, basic. Searchable/modifiable.
'   _hour: Timespan, basic. Searchable/modifiable.
'
' METHODS: 
'   
'
Public Class clsTimetable

#Region "Attributes"
    Private _timetableID As Integer
    Private _groupID As Integer
    Private _day As String
    Private _hour As TimeSpan
#End Region

#Region "Constructors"
    'By Default
    Public Sub New()
        Me._timetableID = 0
        Me._groupID = 0
        Me._day = Nothing
        Me._hour = Nothing
    End Sub

    'With parameters
    Public Sub New(ByVal timetableID As Integer, ByVal groupID As Integer, ByVal day As String, ByVal hour As TimeSpan)
        Me._timetableID = timetableID
        Me._groupID = groupID
        Me._day = day
        Me._hour = hour
    End Sub
#End Region

#Region "Gets y Sets"
    Public Property TimetableID() As Integer
        Get
            Return _timetableID
        End Get
        Set(value As Integer)
            _timetableID = value
        End Set
    End Property

    Public Property GroupID() As Integer
        Get
            Return _groupID
        End Get
        Set(value As Integer)
            _groupID = value
        End Set
    End Property

    Public Property Day() As String
        Get
            Return _day
        End Get
        Set(ByVal value As String)
            _day = value
        End Set
    End Property

    Public Property Hour() As TimeSpan
        Get
            Return _hour
        End Get
        Set(ByVal value As TimeSpan)
            _hour = value
        End Set
    End Property
#End Region
End Class
