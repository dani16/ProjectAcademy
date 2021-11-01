' CLASS: clsStudent
'
' PROPERTIES:
'   _StudentID: integer, basic. Searchable.
'   _situation: string, basic. Searchable/modifiable.
'   _englishLevel: string, basic. Searchable/modifiable.
'
' METHODS: 
'   
'
Public Class clsStudent
    Inherits clsPerson

#Region "Attributes"
    Private _studentID As Integer
    Private _situation As String
    Private _englishLevel As String
#End Region

#Region "Constructors"
    'By Default
    Public Sub New()
        Me._studentID = 0
        Me._situation = Nothing
        Me._englishLevel = Nothing
    End Sub

    'With parameters
    Public Sub New(ByVal studentID As Integer, ByVal situation As String, ByVal englishLevel As String)
        Me._studentID = 0
        Me._situation = Nothing
        Me._englishLevel = Nothing
    End Sub
#End Region

#Region "Gets y Sets"
    Public Property StudentID() As Integer
        Get
            Return _studentID
        End Get
        Set(value As Integer)
            _studentID = value
        End Set
    End Property

    Public Property Situation() As String
        Get
            Return _situation
        End Get
        Set(ByVal value As String)
            _situation = value
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
#End Region
End Class
