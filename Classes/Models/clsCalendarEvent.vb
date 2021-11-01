' CLASS: clsCalendarEvent
'
' PROPERTIES:
'   _calendarEventID: integer, basic. Searchable.
'   _startDate: Date, basic. Searchable/modifiable.
'   _finishDate: Date, basic. Searchable/modifiable.
'   _type: character, basic. Searchable/modifiable.     1:Examn, 2:Test, 3:Holiday, 4:Other
'   _subject: string, basic. Searchable/modifiable.
'   _detail: string, basic. Searchable/modifiable.
'   _teacherID: integer, basic. Searchable/modifiable.
'
' METHODS: 
'   
'
Public Class clsCalendarEvent

#Region "Attributes"
    Private _calendarEventID As Integer
    Private _startDate As Date
    Private _finishDate As Date
    Private _type As Integer
    Private _subject As String
    Private _detail As String
    Private _teacherID As Integer
#End Region

#Region "Constructors"
    'By Default
    Public Sub New()
        Me._calendarEventID = 0
        Me._startDate = Nothing
        Me._finishDate = Nothing
        Me._type = 0
        Me._subject = Nothing
        Me._detail = Nothing
        Me._teacherID = 0
    End Sub

    'With parameters
    Public Sub New(ByVal calendarEventID As Integer, ByVal startDate As Date, ByVal finishDate As Date,
                   ByVal type As Integer, ByVal subject As String, ByVal detail As String, ByVal teacherID As Integer)
        Me._calendarEventID = calendarEventID
        Me._startDate = startDate
        Me._finishDate = finishDate
        Me._type = type
        Me._subject = subject
        Me._detail = detail
        Me._teacherID = teacherID
    End Sub
#End Region

#Region "Gets y Sets"
    Public Property CalendarEventID() As Integer
        Get
            Return _calendarEventID
        End Get
        Set(value As Integer)
            _calendarEventID = value
        End Set
    End Property

    Public Property StartDate() As Date
        Get
            Return _startDate
        End Get
        Set(ByVal value As Date)
            _startDate = value
        End Set
    End Property

    Public Property FinishDate() As Date
        Get
            Return _finishDate
        End Get
        Set(ByVal value As Date)
            _finishDate = value
        End Set
    End Property

    Public Property Type() As Integer
        Get
            Return _type
        End Get
        Set(ByVal value As Integer)
            _type = value
        End Set
    End Property

    Public Property Subject() As String
        Get
            Return _subject
        End Get
        Set(ByVal value As String)
            _subject = value
        End Set
    End Property

    Public Property Detail() As String
        Get
            Return _detail
        End Get
        Set(ByVal value As String)
            _detail = value
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
#End Region
End Class
