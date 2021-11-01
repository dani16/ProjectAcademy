' CLASS: clsConfiguration
'
' PROPERTIES:
'   /* General Configuration */    '   
'   /* Restrictions */
'   _ConfigurationID: integer, basic. Searchable.
'   showHome			boolean, basic. Searchable/modifiable
'   showStudents		boolean, basic. Searchable/modifiable
'   showGroups			boolean, basic. Searchable/modifiable
'   showTeachers		boolean, basic. Searchable/modifiable
'   showAssessment		boolean, basic. Searchable/modifiable
'   showMarks			boolean, basic. Searchable/modifiable
'   showCalendar		boolean, basic. Searchable/modifiable
'   showTimetable		boolean, basic. Searchable/modifiable
'   showPayments		boolean, basic. Searchable/modifiable
'
'   /* Permissions */
'   --Students	
'   allowNewStudents	boolean, basic. Searchable/modifiable
'   allowEditStudents	boolean, basic. Searchable/modifiable
'   allowDeleteStudents  boolean, basic. Searchable/modifiable
'
'   --Groups
'   allowNewGroups		boolean, basic. Searchable/modifiable
'   allowEditGroups	    boolean, basic. Searchable/modifiable
'   allowFinishGroups	boolean, basic. Searchable/modifiable
'
'   --Teachers
'   allowNewTeachers	    boolean, basic. Searchable/modifiable
'   allowEditTeachers	    boolean, basic. Searchable/modifiable
'   allowDeleteTeachers     boolean, basic. Searchable/modifiable
'   allowTeacherStudents    boolean, basic. Searchable/modifiable
'
'   --Assessment
'   allowNewAssessment	    boolean, basic. Searchable/modifiable
'   allowEditAssessment	    boolean, basic. Searchable/modifiable
'   allowDeleteAssessment   boolean, basic. Searchable/modifiable
'
'   --Marks
'   allowNewMarks		boolean, basic. Searchable/modifiable
'   allowEditMarks		boolean, basic. Searchable/modifiable
'   allowDeleteMarks	boolean, basic. Searchable/modifiable
'
'   --Calendar
'   allowNewCalendar	boolean, basic. Searchable/modifiable
'   allowEditCalendar	boolean, basic. Searchable/modifiable
'   allowDeleteCalendar boolean, basic. Searchable/modifiable
'
'   --Timetable
'   allowEditTimetable	    boolean, basic. Searchable/modifiable
'
'   --Payments
'   allowNewPayments	    boolean, basic. Searchable/modifiable
'   allowEditPayments	    boolean, basic. Searchable/modifiable
'   allowDeletePayments     boolean, basic. Searchable/modifiable
'
'   /* Others */
'   --Users
'   allowNewUsers		        boolean, basic. Searchable/modifiable
'   allowEditPersonalData	    boolean, basic. Searchable/modifiable 
'   allowChangePassword	        boolean, basic. Searchable/modifiable
' METHODS: 
'   
'

Public Class clsConfiguration
#Region "Attributes"
    'General
    Private _ConfigurationID As Integer
    Private _showHome As Boolean
    Private _showStudents As Boolean
    Private _showGroups As Boolean
    Private _showTeachers As Boolean
    Private _showAssessment As Boolean
    Private _showMarks As Boolean
    Private _showCalendar As Boolean
    Private _showTimetable As Boolean
    Private _showPayments As Boolean
    Private _allowNewStudents As Boolean
    Private _allowEditStudents As Boolean
    Private _allowDeleteStudents As Boolean
    Private _allowNewGroups As Boolean
    Private _allowEditGroups As Boolean
    Private _allowFinishGroups As Boolean
    Private _allowNewTeachers As Boolean
    Private _allowEditTeachers As Boolean
    Private _allowDeleteTeachers As Boolean
    Private _allowTeacherStudents As Boolean
    Private _allowNewAssessment As Boolean
    Private _allowEditAssessment As Boolean
    Private _allowDeleteAssessment As Boolean
    Private _allowNewMarks As Boolean
    Private _allowEditMarks As Boolean
    Private _allowDeleteMarks As Boolean
    Private _allowNewCalendar As Boolean
    Private _allowEditCalendar As Boolean
    Private _allowDeleteCalendar As Boolean
    Private _allowEditTimetable As Boolean
    'Private _allowNewPayments As Boolean
    Private _allowEditPayments As Boolean
    'Private _allowDeletePayments As Boolean
    Private _allowNewUsers As Boolean
    Private _allowEditPersonalData As Boolean
    Private _allowChangePassword As Boolean
#End Region

#Region "Constructors"
    'By Default
    Public Sub New()
        'General
        Me._ConfigurationID = 0
        Me._showHome = Nothing
        Me._showStudents = Nothing
        Me._showGroups = Nothing
        Me._showTeachers = Nothing
        Me._showAssessment = Nothing
        Me._showMarks = Nothing
        Me._showCalendar = Nothing
        Me._showTimetable = Nothing
        Me._showPayments = Nothing
        Me._allowNewStudents = Nothing
        Me._allowEditStudents = Nothing
        Me._allowDeleteStudents = Nothing
        Me._allowNewGroups = Nothing
        Me._allowEditGroups = Nothing
        Me._allowFinishGroups = Nothing
        Me._allowNewTeachers = Nothing
        Me._allowEditTeachers = Nothing
        Me._allowDeleteTeachers = Nothing
        Me._allowTeacherStudents = Nothing
        Me._allowNewAssessment = Nothing
        Me._allowEditAssessment = Nothing
        Me._allowDeleteAssessment = Nothing
        Me._allowNewMarks = Nothing
        Me._allowEditMarks = Nothing
        Me._allowDeleteMarks = Nothing
        Me._allowNewCalendar = Nothing
        Me._allowEditCalendar = Nothing
        Me._allowDeleteCalendar = Nothing
        Me._allowEditTimetable = Nothing
        'Me._allowNewPayments = Nothing
        Me._allowEditPayments = Nothing
        'Me._allowDeletePayments = Nothing
        Me._allowNewUsers = Nothing
        Me._allowEditPersonalData = Nothing
        Me._allowChangePassword = Nothing
    End Sub
#End Region

#Region "Gets y Sets"
    Public ReadOnly Property ConfigurationID() As Integer
        Get
            Return _ConfigurationID
        End Get
    End Property

    Public Property ShowHome() As Boolean
        Get
            Return _showHome
        End Get
        Set(ByVal value As Boolean)
            _showHome = value
        End Set
    End Property

    Public Property ShowStudents() As Boolean
        Get
            Return _showStudents
        End Get
        Set(ByVal value As Boolean)
            _showStudents = value
        End Set
    End Property

    Public Property ShowGroups() As Boolean
        Get
            Return _showGroups
        End Get
        Set(ByVal value As Boolean)
            _showGroups = value
        End Set
    End Property

    Public Property ShowTeachers() As Boolean
        Get
            Return _showTeachers
        End Get
        Set(ByVal value As Boolean)
            _showTeachers = value
        End Set
    End Property

    Public Property ShowAssessment() As Boolean
        Get
            Return _showAssessment
        End Get
        Set(ByVal value As Boolean)
            _showAssessment = value
        End Set
    End Property

    Public Property ShowMarks() As Boolean
        Get
            Return _showMarks
        End Get
        Set(ByVal value As Boolean)
            _showMarks = value
        End Set
    End Property

    Public Property ShowCalendar() As Boolean
        Get
            Return _showCalendar
        End Get
        Set(ByVal value As Boolean)
            _showCalendar = value
        End Set
    End Property

    Public Property ShowTimetable() As Boolean
        Get
            Return _showTimetable
        End Get
        Set(ByVal value As Boolean)
            _showTimetable = value
        End Set
    End Property

    Public Property ShowPayments() As Boolean
        Get
            Return _showPayments
        End Get
        Set(ByVal value As Boolean)
            _showPayments = value
        End Set
    End Property

    Public Property AllowNewStudents() As Boolean
        Get
            Return _allowNewStudents
        End Get
        Set(ByVal value As Boolean)
            _allowNewStudents = value
        End Set
    End Property

    Public Property AllowEditStudents() As Boolean
        Get
            Return _allowEditStudents
        End Get
        Set(ByVal value As Boolean)
            _allowEditStudents = value
        End Set
    End Property

    Public Property AllowDeleteStudents() As Boolean
        Get
            Return _allowDeleteStudents
        End Get
        Set(ByVal value As Boolean)
            _allowDeleteStudents = value
        End Set
    End Property

    Public Property AllowNewGroups() As Boolean
        Get
            Return _allowNewGroups
        End Get
        Set(ByVal value As Boolean)
            _allowNewGroups = value
        End Set
    End Property

    Public Property AllowEditGroups() As Boolean
        Get
            Return _allowEditGroups
        End Get
        Set(ByVal value As Boolean)
            _allowEditGroups = value
        End Set
    End Property

    Public Property AllowFinishGroups() As Boolean
        Get
            Return _allowFinishGroups
        End Get
        Set(ByVal value As Boolean)
            _allowFinishGroups = value
        End Set
    End Property

    Public Property AllowNewTeachers() As Boolean
        Get
            Return _allowNewTeachers
        End Get
        Set(ByVal value As Boolean)
            _allowNewTeachers = value
        End Set
    End Property

    Public Property AllowEditTeachers() As Boolean
        Get
            Return _allowEditTeachers
        End Get
        Set(ByVal value As Boolean)
            _allowEditTeachers = value
        End Set
    End Property

    Public Property AllowDeleteTeachers() As Boolean
        Get
            Return _allowDeleteTeachers
        End Get
        Set(ByVal value As Boolean)
            _allowDeleteTeachers = value
        End Set
    End Property

    Public Property AllowNewAssessment() As Boolean
        Get
            Return _allowNewAssessment
        End Get
        Set(ByVal value As Boolean)
            _allowNewAssessment = value
        End Set
    End Property

    Public Property AllowEditAssessment() As Boolean
        Get
            Return _allowEditAssessment
        End Get
        Set(ByVal value As Boolean)
            _allowEditAssessment = value
        End Set
    End Property

    Public Property AllowDeleteAssessment() As Boolean
        Get
            Return _allowDeleteAssessment
        End Get
        Set(ByVal value As Boolean)
            _allowDeleteAssessment = value
        End Set
    End Property

    Public Property AllowNewCalendar() As Boolean
        Get
            Return _allowNewCalendar
        End Get
        Set(ByVal value As Boolean)
            _allowNewCalendar = value
        End Set
    End Property

    Public Property AllowEditCalendar() As Boolean
        Get
            Return _allowEditCalendar
        End Get
        Set(ByVal value As Boolean)
            _allowEditCalendar = value
        End Set
    End Property

    Public Property AllowDeleteCalendar() As Boolean
        Get
            Return _allowDeleteCalendar
        End Get
        Set(ByVal value As Boolean)
            _allowDeleteCalendar = value
        End Set
    End Property

    Public Property AllowEditTimetable() As Boolean
        Get
            Return _allowEditTimetable
        End Get
        Set(ByVal value As Boolean)
            _allowEditTimetable = value
        End Set
    End Property

    Public Property AllowEditPayments() As Boolean
        Get
            Return _allowEditPayments
        End Get
        Set(ByVal value As Boolean)
            _allowEditPayments = value
        End Set
    End Property

    Public Property AllowNewMarks() As Boolean
        Get
            Return _allowNewMarks
        End Get
        Set(ByVal value As Boolean)
            _allowNewMarks = value
        End Set
    End Property

    Public Property AllowEditMarks() As Boolean
        Get
            Return _allowEditMarks
        End Get
        Set(ByVal value As Boolean)
            _allowEditMarks = value
        End Set
    End Property

    Public Property AllowDeleteMarks() As Boolean
        Get
            Return _allowDeleteMarks
        End Get
        Set(ByVal value As Boolean)
            _allowDeleteMarks = value
        End Set
    End Property

    Public Property AllowNewUsers() As Boolean
        Get
            Return _allowNewUsers
        End Get
        Set(ByVal value As Boolean)
            _allowNewUsers = value
        End Set
    End Property

    Public Property AllowEditPersonalData() As Boolean
        Get
            Return _allowEditPersonalData
        End Get
        Set(ByVal value As Boolean)
            _allowEditPersonalData = value
        End Set
    End Property

    Public Property AllowChangePassword() As Boolean
        Get
            Return _allowChangePassword
        End Get
        Set(ByVal value As Boolean)
            _allowChangePassword = value
        End Set
    End Property
#End Region
End Class
