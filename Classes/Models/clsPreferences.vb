' CLASS: clsPreferences
'
' PROPERTIES:
'   _PreferenceID                   integer, basic. Searchable.
'   _userID                         integer, basic. Searchable/modifiable.
'   _languageDefault                integer, basic. Searchable/modifiable.
'   _activateEventNotifications     boolean, basic. Searchable/modifiable.
'   _activatePaymentsNotifications  boolean, basic. Searchable/modifiable.
'   _daysNotifyEvents               integer, basic. Searchable/modifiable.
'   _daysNotifyExam                 integer, basic. Searchable/modifiable.
'   _daysNotifyTest                 integer, basic. Searchable/modifiable.
'   _dayNotifyHoliday               integer, basic. Searchable/modifiable.
'   _dayNotifyOthers                integer, basic. Searchable/modifiable.
'   _dayNotifyPayments              integer, basic. Searchable/modifiable.
'
' METHODS: 
'   
'
Public Class clsPreferences
#Region "Attributes"
    'General
    Private _PreferenceID As Integer
    Private _userID As Integer
    Private _languageDefault As Integer
    Private _activateEventNotifications As Boolean
    Private _activatePaymentsNotifications As Boolean
    Private _daysNotifyEvents As Integer
    Private _daysNotifyExam As Integer
    Private _daysNotifyTest As Integer
    Private _dayNotifyHoliday As Integer
    Private _dayNotifyOthers As Integer
    Private _dayNotifyPayments As Integer
#End Region

#Region "Constructors"
    'By Default
    Public Sub New()
        'General
        Me._PreferenceID = 0
        Me._userID = 0
        Me._languageDefault = 0
        Me._activateEventNotifications = Nothing
        Me._activatePaymentsNotifications = Nothing
        Me._daysNotifyEvents = 0
        Me._daysNotifyExam = 0
        Me._daysNotifyTest = 0
        Me._dayNotifyHoliday = 0
        Me._dayNotifyOthers = 0
        Me._dayNotifyPayments = 0
    End Sub
#End Region

#Region "Gets y Sets"
    Public ReadOnly Property PreferencesID() As Integer
        Get
            Return _PreferenceID
        End Get
    End Property

    Public Property UserID() As Integer
        Get
            Return _userID
        End Get
        Set(ByVal value As Integer)
            _userID = value
        End Set
    End Property

    Public Property LanguageDefault() As Integer
        Get
            Return _languageDefault
        End Get
        Set(ByVal value As Integer)
            _languageDefault = value
        End Set
    End Property

    Public Property ActivateEventNotifications() As Boolean
        Get
            Return _activateEventNotifications
        End Get
        Set(ByVal value As Boolean)
            _activateEventNotifications = value
        End Set
    End Property

    Public Property ActivatePaymentsNotifications() As Boolean
        Get
            Return _activatePaymentsNotifications
        End Get
        Set(ByVal value As Boolean)
            _activatePaymentsNotifications = value
        End Set
    End Property

    Public Property DaysNotifyEvents() As Integer
        Get
            Return _daysNotifyEvents
        End Get
        Set(ByVal value As Integer)
            _daysNotifyEvents = value
        End Set
    End Property

    Public Property DaysNotifyExam() As Integer
        Get
            Return _daysNotifyExam
        End Get
        Set(ByVal value As Integer)
            _daysNotifyExam = value
        End Set
    End Property

    Public Property DaysNotifyTest() As Integer
        Get
            Return _daysNotifyTest
        End Get
        Set(ByVal value As Integer)
            _daysNotifyTest = value
        End Set
    End Property

    Public Property DaysNotifyHoliday() As Integer
        Get
            Return _dayNotifyHoliday
        End Get
        Set(ByVal value As Integer)
            _dayNotifyHoliday = value
        End Set
    End Property

    Public Property DaysNotifyOthers() As Integer
        Get
            Return _dayNotifyOthers
        End Get
        Set(ByVal value As Integer)
            _dayNotifyOthers = value
        End Set
    End Property

    Public Property DaysNotifyPayments() As Integer
        Get
            Return _dayNotifyPayments
        End Get
        Set(ByVal value As Integer)
            _dayNotifyPayments = value
        End Set
    End Property
#End Region
End Class
