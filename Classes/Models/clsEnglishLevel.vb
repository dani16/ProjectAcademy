' CLASS: clsEnglishLevel
'
' PROPERTIES:
'   _englishLevel: string, basic. Searchable.
'   _description: string, basic. Searchable/modifiable. 
'
' METHODS: 
'   
'

Public Class clsEnglishLevel

#Region "Attributes"
    Private _englishLevel As String
    Private _description As String
#End Region

#Region "Constructors"

    'By Default
    Public Sub New()
        Me._englishLevel = Nothing
        Me._description = Nothing
    End Sub

    'With parameters
    Public Sub New(ByVal englishLevel As String, ByVal description As String)
        Me._englishLevel = englishLevel
        Me._description = description
    End Sub
#End Region

#Region "Gets y Sets"
    Public Property EnglishLevel() As String
        Get
            Return _englishLevel
        End Get
        Set(value As String)
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
#End Region
End Class
