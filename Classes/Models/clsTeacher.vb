' CLASS: clsTeacher
'
' PROPERTIES:
'   _TeacherID: integer, basic. Searchable.
'
' METHODS: 
'   
'
Public Class clsTeacher
    Inherits clsPerson

#Region "Attributes"
    Private _teacherID As Integer
#End Region

#Region "Constructors"
    'By Default
    Public Sub New()
        Me._teacherID = 0
    End Sub

    'With parameters
    Public Sub New(ByVal teacherID As Integer)
        Me._teacherID = 0
    End Sub
#End Region

#Region "Gets y Sets"
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
