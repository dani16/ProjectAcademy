' CLASS: clsNotification
'
' PROPERTIES:
'   _notificationID: integer, basic. Searchable.
'   _subject: string, basic. Searchable/modifiable.
'   _detail: string, basic. Searchable/modifiable.
'   _eventCalendarID: integer, basic. Searchable/modifiable.
'   _teacherID: integer, basic. Searchable/modifiable.
'
' METHODS: 
'   

Public Class clsNotification

#Region "Attributes"
    Private _notificationID As Integer
    Private _subject As String
    Private _detail As String
    Private _eventCalendarID As Integer
    Private _teacherID As Integer
#End Region

#Region "Constructors"
    'By Default
    Public Sub New()
        Me._notificationID = 0
        Me._subject = Nothing
        Me._detail = Nothing
        Me._eventCalendarID = 0
        Me._teacherID = 0
    End Sub

    'With parameters
    Public Sub New(ByVal notificationID As Integer, ByVal subject As String, ByVal detail As String, ByVal eventCalendarID As Integer, ByVal teacherID As Integer)
        Me._notificationID = 0
        Me._subject = Nothing
        Me._detail = Nothing
        Me._eventCalendarID = 0
        Me._teacherID = 0
    End Sub
#End Region

#Region "Gets y Sets"
    Public Property NotificationID() As Integer
        Get
            Return _notificationID
        End Get
        Set(value As Integer)
            _notificationID = value
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

    Public Property EventCalendarID() As Integer
        Get
            Return _eventCalendarID
        End Get
        Set(value As Integer)
            _eventCalendarID = value
        End Set
    End Property

    Public Property TeacherID() As Integer
        Get
            Return _teacherID
        End Get
        Set(value As Integer)
            _teacherID = value
        End Set
    End Property
#End Region
End Class

