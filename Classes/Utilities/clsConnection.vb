Imports System.Data.SqlClient

' CLASS: clsConnection

' PROPERTIES
'   _database: string, basic. Searchable/modifiable.
'   _user: string, basic. Searchable/modifiablee.
'   _pass: string, basic. Searchable/modifiablee.
'
' MÉTODOS
'   Function getConnection() As SqlConnection
'       This method opens a connection with the database. Throws the following excepctions: SqlExcepion, InvalidOperationException y Exception.
'   
'   Sub closeConnection(ByRef connection As SqlConnection)
'       This method closes a connection from the database. Throws the following excepctions: SqlExcepion, InvalidOperationException y Exception.
'
'   

Public Class clsConnection
#Region "Attributes"
    'Private _connection As New SqlConnection
    Private _databaseName As String
    Private _user As String
    Private _pass As String
#End Region

#Region "Constructors"
    Public Sub New()
        Me._databaseName = "EnglishAcademyDB"
        Me._user = "usuario"
        Me._pass = "pass"
    End Sub

    Public Sub New(ByVal databaseName As String, ByVal user As String, ByVal pass As String)
        Me._databaseName = databaseName
        Me._user = user
        Me._pass = pass
    End Sub
#End Region

#Region "Properties"
    '_databaseName String
    Public Property databaseName As String
        Get
            Return _databaseName
        End Get
        Set(ByVal value As String)
            If databaseName <> "" Then
                Me._databaseName = databaseName
            Else
                Throw New Exception("DatabaseName can't be empty.")
            End If
        End Set
    End Property

    '_user String
    Public Property user As String
        Get
            Return _user
        End Get
        Set(ByVal value As String)
            If user <> "" Then
                Me._user = user
            Else
                Throw New Exception("User can't be empty.")
            End If
        End Set
    End Property

    '_pass String
    Public Property pass As String
        Get
            Return _pass
        End Get
        Set(ByVal value As String)
            If pass <> "" Then
                Me._pass = pass
            Else
                Throw New Exception("Pass can't be empty.")
            End If
        End Set
    End Property

#End Region

#Region "Methods"
    ''' <summary>
    ''' Method that creates a connection to the database
    ''' </summary>
    ''' <pre>None</pre>
    ''' <pos>The connection is created</pos>
    ''' <remarks></remarks>
    Public Function getConnection() As SqlConnection
        Dim connection As New SqlConnection()

        Try
            'Create string for the connection
            connection.ConnectionString = "server=(local);" & "database=" & _databaseName & ";uid=" & _user & ";pwd=" & _pass & ";"

            'Open Connection
            connection.Open()

        Catch oExcep As SqlException
            Throw oExcep
        Catch invEx As InvalidOperationException
            Throw invEx
        Catch ex As Exception
            Throw ex
        End Try

        Return connection
    End Function

    ''' <summary>
    ''' Method that close the connection to the database
    ''' </summary>
    ''' <pre>None</pre>
    ''' <pos>The connection is closed</pos>
    ''' <param name="connection">The input parameter is the connection to close.</param>
    ''' <remarks></remarks>
    Public Sub closeConnection(ByRef connection As SqlConnection)
        Try
            'Close Connection
            connection.Close()

        Catch oExcep As SqlException
            Throw oExcep
        Catch invEx As InvalidOperationException
            Throw invEx
        Catch ex As Exception
            Throw ex
        End Try
    End Sub

#End Region

End Class
