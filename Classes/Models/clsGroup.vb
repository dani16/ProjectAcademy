' CLASS: clsGroup
'
' PROPERTIES:
'   _groupID: integer, basic. Searchable.
'   _englishLevel: string, basic. Searchable/modifiable.
'   _description: string, basic. Searchable/modifiable.
'   _dateStarting: date, basic. Searchable/modifiable.
'   _dateFinish: date, basic. Searchable/modifiable.
'   _feeInscription: double, basic. Searchable/modifiable.
'   _feeMonthly: double, basic. Searchable/modifiable.
'   _teacherID: integer, basic. Searchable/modifiable.
'   _students: List of Students, basic.Searchable/modifiable.
'
' METHODS: 
'

Public Class clsGroup

#Region "Attributes"
    Private _groupID As Integer
    Private _englishLevel As String
    Private _description As String
    Private _dateStarting As Date
    Private _dateFinish As Date
    Private _feeInscription As Double
    Private _feeMonthly As Double
    Private _teacherID As Integer
    Private _students As List(Of clsStudent)
#End Region

#Region "Constructors"
    'By Default
    Public Sub New()
        Me._groupID = 0
        Me._englishLevel = Nothing
        Me._description = Nothing
        Me._dateStarting = Nothing
        Me._dateFinish = Nothing
        Me._feeInscription = 0
        Me._feeMonthly = 0
        Me._teacherID = 0
        Me._students = Nothing
    End Sub

    'With parameters
    Public Sub New(ByVal groupID As Integer, ByVal englishLevel As String, ByVal description As String,
                   ByVal dateStarting As Date, ByVal dateFinish As Date, ByVal feeInscription As Double, ByVal feeMonthly As Double,
                   ByVal teacherID As Integer, ByVal students As List(Of clsStudent))
        Me._groupID = groupID
        Me._englishLevel = englishLevel
        Me._description = description
        Me._dateStarting = dateStarting
        Me._dateFinish = dateFinish
        Me._feeInscription = feeInscription
        Me._feeInscription = feeMonthly
        Me._teacherID = teacherID
        Me._students = students
    End Sub
#End Region

#Region "Gets y Sets"
    Public Property GroupID() As Integer
        Get
            Return _groupID
        End Get
        Set(value As Integer)
            _groupID = value
        End Set
    End Property

    Public Property EnglishLevel() As String
        Get
            Return _englishLevel
        End Get
        Set(ByVal value As String)
            _englishLevel = value
        End Set
    End Property

    Public Property Description() As String
        Get
            Return _description
        End Get
        Set(ByVal value As String)
            _description = value
        End Set
    End Property

    Public Property DateStarting() As Date
        Get
            Return _dateStarting
        End Get
        Set(ByVal value As Date)
            _dateStarting = value
        End Set
    End Property

    Public Property DateFinish() As Date
        Get
            Return _dateFinish
        End Get
        Set(ByVal value As Date)
            _dateFinish = value
        End Set
    End Property

    Public Property FeeInscription() As Double
        Get
            Return _feeInscription
        End Get
        Set(ByVal value As Double)
            _feeInscription = value
        End Set
    End Property

    Public Property FeeMonthly() As Double
        Get
            Return _feeMonthly
        End Get
        Set(ByVal value As Double)
            _feeMonthly = value
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

    Public Property Students() As List(Of clsStudent)
        Get
            Return _students
        End Get
        Set(ByVal value As List(Of clsStudent))
            _students = value
        End Set
    End Property
#End Region

#Region "Methods"
    Public Function getStudent(ByVal DNI As String) As clsStudent
        Dim result As New clsStudent

        For Each student As clsStudent In Students
            If student.DNI = DNI Then
                result = student
            End If
        Next

        Return result
    End Function

    Public Sub addStudent(ByVal student As clsStudent)
        Students.Add(student)
    End Sub

    Public Sub addStudents(ByVal students As List(Of clsStudent))
        _students.AddRange(students)
    End Sub

    Public Sub removeStudent(ByVal student As clsStudent)
        Students.Remove(student)
    End Sub

    Public Sub removeAllStudents()
        Students.Clear()
    End Sub

    Public Sub sortByNameSurname()
        Students.Sort()
    End Sub
#End Region
End Class
