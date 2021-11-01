' CLASS: clsUserDataContext
'
' PROPERTIES:
'   _user: clsUser, basic. Searchable/modifiable.
'   _teacher: clsTeacher, basic. Searchable/modifiable.
'   _preferences: clsPreferences, basic. Searchable/modifiable.
'
' METHODS: 
'   
'
Public Class clsUserDataContext
#Region "Attributes"
    Private _user As clsUser
    Private _teacher As clsTeacher
    Private _preferences As clsPreferences
#End Region

#Region "Constructors"
    'By Default
    Public Sub New()
        Me._user = Nothing
        Me._teacher = Nothing
        Me._preferences = Nothing
    End Sub

    'With parameters
    Public Sub New(ByVal user As clsUser, ByVal teacher As clsTeacher, ByVal preferences As clsPreferences)
        Me._user = user
        Me._teacher = teacher
        Me._preferences = preferences
    End Sub
#End Region

#Region "Gets y Sets"
    Public Property User() As clsUser
        Get
            Return _user
        End Get
        Set(ByVal value As clsUser)
            _user = value
        End Set
    End Property

    Public Property Teacher() As clsTeacher
        Get
            Return _teacher
        End Get
        Set(ByVal value As clsTeacher)
            _teacher = value
        End Set
    End Property

    Public Property Preferences() As clsPreferences
        Get
            Return _preferences
        End Get
        Set(ByVal value As clsPreferences)
            _preferences = value
        End Set
    End Property
#End Region

End Class

