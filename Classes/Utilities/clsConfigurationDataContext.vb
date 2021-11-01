Public Class clsConfigurationDataContext
    ' CLASS: clsConfigurationDataContext
    '
    ' PROPERTIES:
    '   _userDataContext: clsUser, basic. Searchable/modifiable.
    '   _configuration: clsConfiguration, basic. Searchable/modifiable.
    '   _preferences: clsPreferences, basic. Searchable/modifiable.
    '
    ' METHODS: 
    '   
#Region "Attributes"
    Private _userDataContext As clsUserDataContext
    Private _configuration As clsConfiguration
    Private _preferences As clsPreferences
#End Region

#Region "Constructors"
    'By Default
    Public Sub New()
        Me._userDataContext = Nothing
        Me._configuration = Nothing
    End Sub

    'With parameters
    Public Sub New(ByVal userDataContext As clsUserDataContext, ByVal configuration As clsConfiguration)
        Me._userDataContext = userDataContext
        Me._configuration = configuration
    End Sub
#End Region

#Region "Gets y Sets"
    Public Property UserDataContext() As clsUserDataContext
        Get
            Return _userDataContext
        End Get
        Set(ByVal value As clsUserDataContext)
            _userDataContext = value
        End Set
    End Property

    Public Property Configuration() As clsConfiguration
        Get
            Return _configuration
        End Get
        Set(ByVal value As clsConfiguration)
            _configuration = value
        End Set
    End Property
#End Region
End Class
