' CLASS: clsUser
'
' PROPERTIES:
'   _userID: integer, basic. Searchable.
'   _username: string, basic. Searchable/modifiable.
'   _password: string, basic. Searchable/modifiable.'   
'   _type: boolean, basic. Searchable/modifiable.   TRUE:Admin, FALSE:Teacher user
'   _teacherID: integer, basic. Searchable/modifiable.
'
' METHODS: 
'   
'

Public Class clsUser

#Region "Attributes"
    Private _userID As Integer
    Private _username As String
    Private _password As String
    Private _type As Boolean
    Private _teacherID As Integer
#End Region

#Region "Constructors"
    'By Default
    Public Sub New()
        Me._userID = 0
        Me._username = Nothing
        Me._password = Nothing
        Me._type = "0"
        Me._teacherID = 0
    End Sub

    'With parameters
    Public Sub New(ByVal userID As Integer, ByVal username As String, ByVal password As String,
                   ByVal type As Boolean, ByVal status As Boolean, ByVal teacherID As Integer)
        Me._userID = userID
        Me._username = username
        Me._password = password
        Me._type = type
        Me._teacherID = teacherID
    End Sub
#End Region

#Region "Gets y Sets"

    Public Property UserID() As Integer
        Get
            Return _userID
        End Get
        Set(ByVal value As Integer)
            _userID = value
        End Set
    End Property

    Public Property Username() As String
        Get
            Return _username
        End Get
        Set(ByVal value As String)
            _username = value
        End Set
    End Property

    Public Property Password() As String
        Get
            Return _password
        End Get
        Set(ByVal value As String)
            _password = value
        End Set
    End Property

    Public Property Type() As Boolean
        Get
            Return _type
        End Get
        Set(ByVal value As Boolean)
            _type = value
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
