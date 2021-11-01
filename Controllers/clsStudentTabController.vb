Imports System.ComponentModel

Public Class clsStudentTabController
    Implements INotifyPropertyChanged

#Region "PropertyChanged"
    ' Declare the event
    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    ' Create the OnPropertyChanged method to raise the event
    Protected Sub OnPropertyChanged(ByVal name As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(name))
    End Sub
#End Region

#Region "Commands"
    'Add Students

    'Remove Students

    'Add Group to Student
#End Region

#Region "Properties"
    Private _configuration As clsConfiguration = Application.oConfigurationManager.getConfiguration()
    Private _userDataContext As clsUserDataContext = Application.oUserDataContextManager.getUser("admin")
    Private _listEnglishLevels As List(Of clsEnglishLevel) = Application.oEnglishLevelManager.getEnglishLevels()
    Private _listStudents As List(Of clsStudent) = Application.oStudentManager.getAllStudents()
    Private _student As clsStudent = _listStudents(0)
    Private _listGroups As List(Of clsGroup) = Application.oGroupManager.getStudentGroups(_student.StudentID)
    Private _listMarks As List(Of clsMark) = Application.oMarkManager.getStudentMarks(_student.StudentID)

    Public Property Configuration() As clsConfiguration
        Get
            Return _configuration
        End Get
        Set(value As clsConfiguration)
            _configuration = value
        End Set
    End Property

    Public Property UserDataContext() As clsUserDataContext
        Get
            Return _userDataContext
        End Get
        Set(value As clsUserDataContext)
            _userDataContext = value
        End Set
    End Property

    Public Property ListEnglishLevels() As List(Of clsEnglishLevel)
        Get
            Return _listEnglishLevels
        End Get
        Set(value As List(Of clsEnglishLevel))
            _listEnglishLevels = value
        End Set
    End Property

    Public Property Student() As clsStudent
        Get
            Return _student
        End Get
        Set(value As clsStudent)
            _student = value
        End Set
    End Property

    Public Property ListStudents() As List(Of clsStudent)
        Get
            Return _listStudents
        End Get
        Set(value As List(Of clsStudent))
            _listStudents = value
        End Set
    End Property

    Public Property ListGroups() As List(Of clsGroup)
        Get
            Return _listGroups
        End Get
        Set(value As List(Of clsGroup))
            _listGroups = value
        End Set
    End Property

    Public Property ListMark() As List(Of clsMark)
        Get
            Return _listMarks
        End Get
        Set(value As List(Of clsMark))
            _listMarks = value
        End Set
    End Property
#End Region
End Class
