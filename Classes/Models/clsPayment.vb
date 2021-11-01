' CLASS: clsPayment
'
' PROPERTIES:
'   _studentID: integer, basic. Searchable/modifiable.
'   _name: String, basic. Searchable/modifiable.
'   _DNI: String, basic. Searchable/modifiable.
'   _groupID: integer, basic. Searchable/modifiable.
'   _groupName: String, basic. Searchable/modifiable.
'   _datePayment: Date, basic. Searchable/modifiable.
'   _paymentType: character, basic. Searchable/modifiable.
'   _paymentDescription: String, basic. Searchable/modifiable.
'   _amount: double, basic. Searchable/modifiable.
'   _status: boolean, basic. Searchable/modifiable.
'
' METHODS: 
'   paymentTypeToString(): Method that returns a String with the paymentType of an object

Public Class clsPayment

#Region "Attributes"
    Private _studentID As Integer
    Private _name As String
    Private _DNI As String
    Private _groupID As Integer
    Private _groupName As String
    Private _datePayment As Date
    Private _paymentType As Char
    Private _paymentDescription As String
    Private _amount As Double
    Private _status As Boolean
#End Region

#Region "Constructors"
    'By Default
    Public Sub New()
        Me._studentID = 0
        Me._name = Nothing
        Me._DNI = Nothing
        Me._groupID = 0
        Me._groupName = Nothing
        Me._datePayment = Nothing
        Me._paymentType = Nothing
        Me._paymentDescription = Nothing
        Me._amount = Nothing
        Me._status = False
    End Sub

    'With parameters
    Public Sub New(ByVal studentID As Integer, ByVal name As String, ByVal DNI As String, ByVal groupID As Integer,
                   ByVal groupName As String, ByVal datePayment As Date, ByVal paymentType As Char,
                   ByVal paymentDescription As String, ByVal amount As Double, ByVal status As Boolean,
                   ByVal students As List(Of clsStudent))
        Me._studentID = studentID
        Me._name = name
        Me._DNI = DNI
        Me._groupID = groupID
        Me._groupName = groupName
        Me._datePayment = datePayment
        Me._paymentType = paymentType
        Me._paymentDescription = paymentDescription
        Me._amount = amount
        Me._status = status
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

    Public Property Name() As String
        Get
            Return _name
        End Get
        Set(value As String)
            _name = value
        End Set
    End Property

    Public Property DNI() As String
        Get
            Return _DNI
        End Get
        Set(value As String)
            _DNI = value
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

    Public Property GroupName() As String
        Get
            Return _groupName
        End Get
        Set(value As String)
            _groupName = value
        End Set
    End Property

    Public Property DatePayment() As Date
        Get
            Return _datePayment
        End Get
        Set(ByVal value As Date)
            _datePayment = value
        End Set
    End Property

    Public Property PaymentType() As Char
        Get
            Return _paymentType
        End Get
        Set(ByVal value As Char)
            _paymentType = value
        End Set
    End Property

    Public Property PaymentDescription() As String
        Get
            Return _paymentDescription
        End Get
        Set(value As String)
            _paymentDescription = value
        End Set
    End Property

    Public Property Amount() As Double
        Get
            Return _amount
        End Get
        Set(ByVal value As Double)
            _amount = value
        End Set
    End Property

    Public Property Status() As Boolean
        Get
            Return _status
        End Get
        Set(ByVal value As Boolean)
            _status = value
        End Set
    End Property
#End Region

#Region "Methods"
    ''' <summary>
    ''' Method that returns a String with the paymentType of an object
    ''' clsPayment
    ''' </summary>
    ''' <returns>A String</returns>
    ''' <remarks></remarks>
    Public Function paymentTypeToString() As String
        Dim result As String = Nothing
        If PaymentType = "I" Then
            result = "Student Inscription"
        ElseIf PaymentType = "M" Then
            result = "Monthly payment ( " & MonthName(DatePayment.Month) & " " & DatePayment.Year & " )"
        ElseIf PaymentType = "O" Then
            result = "Others"
        End If
        Return result
    End Function
#End Region
End Class
