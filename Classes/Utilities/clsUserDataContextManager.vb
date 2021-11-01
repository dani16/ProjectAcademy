Imports System.Data.SqlClient
Imports System.IO

Public Class clsUserDataContextManager
#Region "Attributes"
    Private con As New clsConnection
#End Region

#Region "Constructors"
    Public Sub New()
        con = New clsConnection()
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' Method that returns an UserDataContext from the database
    ''' </summary>
    ''' <pre>None</pre>
    ''' <param name="username">A string </param>
    ''' <returns>An object clsUser</returns>
    ''' <pos>Returns the user.</pos>
    ''' <remarks></remarks>
    Public Function getUser(username As String) As clsUserDataContext
        Dim connection As New SqlConnection
        Dim dataReader As SqlDataReader
        Dim sql As String
        Dim command As New SqlCommand
        Dim userDataContext As New clsUserDataContext

        Try
            'Conect to the dataBase
            connection = con.getConnection

            'Create object Command
            sql = "SELECT * FROM [User] AS U" & _
                    " JOIN Teacher AS T ON U.ID_Teacher = T.ID_Teacher " & _
                    " JOIN Person AS P ON T.ID_Person = P.ID_Person " & _
                    " WHERE username=@Username "

            command = New SqlCommand(sql, connection)
            command.Parameters.Add("@Username", System.Data.SqlDbType.NVarChar)
            command.Parameters("@Username").Value = username

            'Get DataReader
            dataReader = command.ExecuteReader()

            'Get User
            If dataReader.HasRows Then
                dataReader.Read()

                'Get User
                Dim user As clsUser = Application.oUserManager.getUser(username)

                'Get Teacher
                Dim teacher As clsTeacher = Application.oTeacherManager.getTeacher(user.TeacherID)

                'Get Preferences
                Dim preferences As clsPreferences = Application.oPreferencesManager.getPreferences(user.UserID)

                'Create object UserDataContext
                userDataContext = New clsUserDataContext(user, teacher, preferences)
            Else
                userDataContext = Nothing
            End If

            'Close dataReader
            dataReader.Close()

        Catch oExcep As SqlException
            Throw oExcep
        Catch invEx As InvalidOperationException
            Throw invEx
        Catch ex As Exception
            Throw ex
        Finally
            'Disconnect from the database
            con.closeConnection(connection)
        End Try

        Return userDataContext
    End Function


    ''' <summary>
    ''' Method that returns a list with all the UserDataContexts from the database
    ''' </summary>
    ''' <returns>A list of Users</returns>
    ''' <remarks></remarks>
    Public Function getAllUserDataContexts() As List(Of clsUserDataContext)
        Dim listUserDataContexts As New List(Of clsUserDataContext)
        Dim connection As New SqlConnection
        Dim dataReader As SqlDataReader
        Dim sql As String
        Dim command As New SqlCommand

        Try
            'Conect to the dataBase
            connection = con.getConnection

            'Create object Command
            sql = "SELECT * FROM [User] AS U " & _
                    " JOIN Teacher AS T ON U.ID_Teacher = T.ID_Teacher " & _
                    " JOIN Person AS P ON T.ID_Person = P.ID_Person "

            command = New SqlCommand(sql, connection)

            'Get DataReader
            dataReader = command.ExecuteReader()

            'Get Students
            While dataReader.Read()
                Dim userDataContext As clsUserDataContext

                'Get User
                Dim user As clsUser = Application.oUserManager.getUser(dataReader("username"))

                'Get Teacher
                Dim teacher As clsTeacher = Application.oTeacherManager.getTeacher(user.TeacherID)

                'Get Preferences
                Dim preferences As clsPreferences = Application.oPreferencesManager.getPreferences(user.UserID)

                'Create object UserDataContext
                userDataContext = New clsUserDataContext(user, teacher, preferences)

                listUserDataContexts.Add(userDataContext)
            End While

            'Close dataReader
            dataReader.Close()

        Catch oExcep As SqlException
            Throw oExcep
        Catch invEx As InvalidOperationException
            Throw invEx
        Catch ex As Exception
            Throw ex
        Finally
            'Disconnect from the database
            con.closeConnection(connection)
        End Try

        Return listUserDataContexts
    End Function
#End Region
End Class
