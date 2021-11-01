' CLASS: clsMark
'
' PROPERTIES:
'   _studentID: integer, basic. Searchable/modifiable.
'   _groupID: integer, basic. Searchable/modifiable.
'   _listening: double, basic. Searchable/modifiable.
'   _speaking: double, basic. Searchable/modifiable.
'   _reading: double, basic. Searchable/modifiable.
'   _writing: double, basic. Searchable/modifiable.
'   _exam: double, basic. Searchable/modifiable.
'   _overall: double, basic. Searchable/modifiable.
'   _dateMark: date, basic. Searchable/modifiable.
'
' METHODS: 
'   getTerm(): Method that gets the Term from the date
'   getOverall(): Method that gets the global mark of a Mark
'
Public Class clsMark

#Region "Attributes"
    Private _studentID As Integer
    Private _groupID As Integer
    Private _listening As Double
    Private _speaking As Double
    Private _reading As Double
    Private _writing As Double
    Private _exam As Double
    Private _overall As Double
    Private _dateMark As Date
#End Region

#Region "Constructors"
    'By Default
    Public Sub New()
        Me._studentID = 0
        Me._groupID = 0
        Me._listening = 0
        Me._speaking = 0
        Me._reading = 0
        Me._writing = 0
        Me._exam = 0
        Me._overall = 0
        Me._dateMark = Nothing
    End Sub

    'With parameters
    Public Sub New(ByVal studentID As Integer, ByVal groupID As Integer, ByVal listening As Double, ByVal speaking As Double,
                   ByVal reading As Double, ByVal writing As Double, ByVal exam As Double, ByVal overall As Double,
                   ByVal dateMark As Date)
        Me._studentID = studentID
        Me._groupID = groupID
        Me._listening = listening
        Me._speaking = speaking
        Me._reading = reading
        Me._writing = writing
        Me._exam = exam
        Me._overall = overall
        Me._dateMark = dateMark
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

    Public Property GroupID() As Integer
        Get
            Return _groupID
        End Get
        Set(value As Integer)
            _groupID = value
        End Set
    End Property

    Public Property Listening() As Double
        Get
            Return _listening
        End Get
        Set(ByVal value As Double)
            _listening = value
        End Set
    End Property

    Public Property Speaking() As Double
        Get
            Return _speaking
        End Get
        Set(ByVal value As Double)
            _speaking = value
        End Set
    End Property

    Public Property Reading() As Double
        Get
            Return _reading
        End Get
        Set(ByVal value As Double)
            _reading = value
        End Set
    End Property

    Public Property Writing() As Double
        Get
            Return _writing
        End Get
        Set(ByVal value As Double)
            _writing = value
        End Set
    End Property

    Public Property Exam() As Double
        Get
            Return _exam
        End Get
        Set(ByVal value As Double)
            _exam = value
        End Set
    End Property

    Public Property Overall() As Double
        Get
            Return _overall
        End Get
        Set(ByVal value As Double)
            _overall = value
        End Set
    End Property

    Public Property DateMark() As Date
        Get
            Return _dateMark
        End Get
        Set(ByVal value As Date)
            _dateMark = value
        End Set
    End Property
#End Region

#Region "Methods"
    ''' <summary>
    ''' Method that get the term of a Mark from the Date
    ''' </summary>
    ''' <returns>An Integer</returns>
    ''' <remarks></remarks>
    Public Function getTerm() As Integer
        Dim term As String

        If DateMark.Month >= 10 And DateMark.Month <= 12 Then
            term = 0
        ElseIf DateMark.Month >= 1 And DateMark.Month <= 3 Then
            term = 1
        ElseIf DateMark.Month >= 4 And DateMark.Month <= 6 Then
            term = 2
        Else
            term = 3
        End If

        Return term
    End Function

    ''' <summary>
    ''' Method that calculates the overall of all the marks
    ''' </summary>
    ''' <returns>A double</returns>
    ''' <remarks></remarks>
    Public Function getOverall() As Double
        Dim overall As Double

        overall = (((Listening + Speaking + Reading + Writing) * 0.4) + (Exam * 0.6)) / 5

        Return overall
    End Function
#End Region
End Class
