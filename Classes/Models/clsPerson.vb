' CLASS: clsPerson
'
' PROPERTIES:
'   _PersonID: integer, basic. Searchable.
'   _name: string, basic. Searchable/modifiable.
'   _surname: string, basic. Searchable/modifiable.
'   _DNI: string, basic. Searchable/modifiable.
'   _photo: image, basic. Searchable/modifiable.
'   _sex: char, basic. Searchable/modifiable.
'   _birthDate: date, basic. Searchable/modifiable.
'   _address: string, basic. Searchable/modifiable.
'   _city: string, basic. Searchable/modifiable.
'   _postalCode: string, basic. Searchable/modifiable.
'   _telephone: string, basic. Searchable/modifiable.
'   _email: string,  basic. Searchable/modifiable.
'
' METHODS: 
'   
'
Public Class clsPerson

#Region "Attributes"
    Private _personID As Integer
    Private _name As String
    Private _surname As String
    Private _DNI As String
    Private _photo As Image
    Private _sex As Char
    Private _birthDate As Date
    Private _address As String
    Private _city As String
    Private _postalCode As String
    Private _telephone As String
    Private _email As String
#End Region

#Region "Constructors"

    'By Default
    Public Sub New()
        Me._personID = 0
        Me._name = Nothing
        Me._surname = Nothing
        Me._DNI = Nothing
        Me._photo = Nothing
        Me._sex = Nothing
        Me._birthDate = Nothing
        Me._address = Nothing
        Me._city = Nothing
        Me._postalCode = Nothing
        Me._telephone = Nothing
        Me._email = Nothing
    End Sub

    'With parameters
    Public Sub New(ByVal personID As Integer, ByVal name As String, ByVal surname As String, ByVal DNI As String,
                   ByVal photo As Image, ByVal sex As Char, ByVal birthDate As Date, ByVal address As String, ByVal city As String,
                   ByVal postalCode As String, ByVal telephone As String, ByVal email As String)
        Me._personID = personID
        Me._name = name
        Me._surname = surname
        Me._DNI = DNI
        Me._photo = photo
        Me._sex = sex
        Me._birthDate = birthDate
        Me._address = address
        Me._city = city
        Me._postalCode = postalCode
        Me._telephone = telephone
        Me._email = email
    End Sub
#End Region

#Region "Gets y Sets"
    Public Property PersonID() As Integer
        Get
            Return _personID
        End Get
        Set(value As Integer)
            _personID = value
        End Set
    End Property

    Public Property Name() As String
        Get
            Return _name
        End Get
        Set(ByVal value As String)
            _name = value
        End Set
    End Property

    Public Property Surname() As String
        Get
            Return _surname
        End Get
        Set(ByVal value As String)
            _surname = value
        End Set
    End Property

    Public Property DNI() As String
        Get
            Return _DNI
        End Get
        Set(ByVal value As String)
            _DNI = value
        End Set
    End Property

    Public Property Photo() As Image
        Get
            Return _photo
        End Get
        Set(ByVal value As Image)
            _photo = value
        End Set
    End Property

    Public Property Sex() As Char
        Get
            Return _sex
        End Get
        Set(ByVal value As Char)
            _sex = value
        End Set
    End Property

    Public Property BirthDate() As Date
        Get
            Return _birthDate
        End Get
        Set(ByVal value As Date)
            _birthDate = value
        End Set
    End Property

    Public Property Address() As String
        Get
            Return _address
        End Get
        Set(ByVal value As String)
            _address = value
        End Set
    End Property

    Public Property City() As String
        Get
            Return _city
        End Get
        Set(ByVal value As String)
            _city = value
        End Set
    End Property

    Public Property PostalCode() As String
        Get
            Return _postalCode
        End Get
        Set(ByVal value As String)
            _postalCode = value
        End Set
    End Property

    Public Property Telephone() As String
        Get
            Return _telephone
        End Get
        Set(ByVal value As String)
            _telephone = value
        End Set
    End Property

    Public Property Email() As String
        Get
            Return _email
        End Get
        Set(ByVal value As String)
            _email = value
        End Set
    End Property

#End Region

End Class

