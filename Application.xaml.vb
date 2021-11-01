Imports System.Globalization
Imports System.Threading

Class Application
    'SHARED PROPERTIES
    'ConfigurationDataContext Application
    Private Shared Property _configurationDataContext As clsConfigurationDataContext

    Public Shared Property ConfigurationDataContextProperty As clsConfigurationDataContext
        Get
            Return _configurationDataContext
        End Get
        Set(value As clsConfigurationDataContext)
            _configurationDataContext = value
        End Set
    End Property

    'CLASS MANAGERS
    'EnglishLevel Manager
    Private Shared _englishLevelManager As New clsEnglishLevelManager

    Public Shared ReadOnly Property oEnglishLevelManager() As clsEnglishLevelManager
        Get
            Return _englishLevelManager
        End Get
    End Property

    'Configuration Manager
    Private Shared _configurationManager As New clsConfigurationManager

    Public Shared ReadOnly Property oConfigurationManager() As clsConfigurationManager
        Get
            Return _configurationManager
        End Get
    End Property

    'Preferences Manager
    Private Shared _preferencesManager As New clsPreferencesManager

    Public Shared ReadOnly Property oPreferencesManager() As clsPreferencesManager
        Get
            Return _preferencesManager
        End Get
    End Property

    'Validator
    Private Shared _validator As New clsValidator

    Public Shared ReadOnly Property oValidator() As clsValidator
        Get
            Return _validator
        End Get
    End Property

    'Person Manager
    Private Shared _personManager As New clsPersonManager

    Public Shared ReadOnly Property oPersonManager() As clsPersonManager
        Get
            Return _personManager
        End Get
    End Property

    'User Manager
    Private Shared _userManager As New clsUserManager

    Public Shared ReadOnly Property oUserManager() As clsUserManager
        Get
            Return _userManager
        End Get
    End Property

    'UserDataContext Manager
    Private Shared _userDataContextManager As New clsUserDataContextManager

    Public Shared ReadOnly Property oUserDataContextManager() As clsUserDataContextManager
        Get
            Return _userDataContextManager
        End Get
    End Property

    'Student Manager
    Private Shared _studentManager As New clsStudentManager

    Public Shared ReadOnly Property oStudentManager() As clsStudentManager
        Get
            Return _studentManager
        End Get
    End Property

    'Teacher Manager
    Private Shared _teacherManager As New clsTeacherManager

    Public Shared ReadOnly Property oTeacherManager() As clsTeacherManager
        Get
            Return _teacherManager
        End Get
    End Property

    'Group Manager
    Private Shared _groupManager As New clsGroupManager

    Public Shared ReadOnly Property oGroupManager() As clsGroupManager
        Get
            Return _groupManager
        End Get
    End Property

    'Inscription Manager
    Private Shared _inscriptionManager As New clsInscriptionManager

    Public Shared ReadOnly Property oInscriptionManager() As clsInscriptionManager
        Get
            Return _inscriptionManager
        End Get
    End Property

    'Mark Manager
    Private Shared _markManager As New clsMarkManager

    Public Shared ReadOnly Property oMarkManager() As clsMarkManager
        Get
            Return _markManager
        End Get
    End Property

    'Payment Manager
    Private Shared _paymentManager As New clsPaymentManager

    Public Shared ReadOnly Property oPaymentManager() As clsPaymentManager
        Get
            Return _paymentManager
        End Get
    End Property

    'CalendarEvent Manager
    Private Shared _calendarEventManager As New clsCalendarEventManager

    Public Shared ReadOnly Property oCalendarEventManager() As clsCalendarEventManager
        Get
            Return _calendarEventManager
        End Get
    End Property

    'Timetable Manager
    Private Shared _timetableManager As New clsTimetableManager

    Public Shared ReadOnly Property oTimetableManager() As clsTimetableManager
        Get
            Return _timetableManager
        End Get
    End Property

    'Notification Manager
    Private Shared _notificationManager As New clsNotificationManager

    Public Shared ReadOnly Property oNotificationManager() As clsNotificationManager
        Get
            Return _notificationManager
        End Get
    End Property

    ''' <summary>
    ''' Method that changes the language of the application
    ''' </summary>
    ''' <param name="culture">An String</param>
    ''' <remarks></remarks>
    Public Shared Sub SelectCulture(ByVal culture As String)
        'List all our resources      
        Dim dictionaryList As List(Of ResourceDictionary) = New List(Of ResourceDictionary)
        Dim resourceDictionary As New ResourceDictionary

        For Each dictionary As ResourceDictionary In Application.Current.Resources.MergedDictionaries
            dictionaryList.Add(dictionary)
        Next

        'We want our specific culture      
        Dim requestedCulture As String = String.Format("Dictionaries/dictionaryLanguages.{0}.xaml", culture)
        resourceDictionary = dictionaryList.FirstOrDefault(Function(d) d.Source.OriginalString.Equals(requestedCulture))

        If resourceDictionary Is Nothing Then
            'If not found, we select our default language      
            requestedCulture = "Dictionaries/dictionaryLanguages.xaml"
            resourceDictionary = dictionaryList.FirstOrDefault(Function(d) d.Source.OriginalString.Equals(requestedCulture))
        End If

        'If we have the requested resource, remove it from the list and place at the end.\      
        'Then this language will be our string table to use.      
        If Not resourceDictionary Is Nothing Then
            Application.Current.Resources.MergedDictionaries.Remove(resourceDictionary)
            Application.Current.Resources.MergedDictionaries.Add(resourceDictionary)
        End If

        'Inform the threads of the new culture      
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(culture)
        Thread.CurrentThread.CurrentUICulture = New CultureInfo(culture)
    End Sub
End Class
