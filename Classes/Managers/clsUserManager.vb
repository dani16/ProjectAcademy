Imports System.Data.SqlClient
Imports System.IO

Public Class clsUserManager
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
    ''' Method that validates the access of an User to the application
    ''' </summary>
    ''' <pre>None</pre>
    ''' <param name="username">A string</param>
    ''' <param name="password">A string</param>
    ''' <returns>A boolean</returns>
    ''' <pos>Returns TRUE if the user is valid o FALSE if is not.</pos>
    ''' <remarks></remarks>
    Public Function validateUser(username As String, password As String) As Boolean
        Dim validate As Boolean = False
        Dim connection As New SqlConnection
        Dim dataReader As SqlDataReader
        Dim sql As String
        Dim command As New SqlCommand

        Try
            'Connect to the dataBase
            connection = con.getConnection

            'Create object Command
            sql = "SELECT * FROM [User] WHERE username=@Username AND password = @Password"
            command = New SqlCommand(sql, connection)
            If username Is "" Or username Is Nothing Then
                command.Parameters.AddWithValue("@Username", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@Username", username)
            End If

            If password Is "" Or password Is Nothing Then
                command.Parameters.AddWithValue("@Password", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@Password", password)
            End If

            'Get DataReader
            dataReader = command.ExecuteReader()

            'Get User
            If dataReader.HasRows Then
                dataReader.Read()
                validate = True
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

        Return validate
    End Function

    ''' <summary>
    ''' Method that returns an User from the database
    ''' </summary>
    ''' <pre>None</pre>
    ''' <param name="username">A string </param>
    ''' <returns>An object clsUser</returns>
    ''' <pos>Returns the user.</pos>
    ''' <remarks></remarks>
    Public Function getUser(username As String) As clsUser
        Dim connection As New SqlConnection
        Dim dataReader As SqlDataReader
        Dim sql As String
        Dim command As New SqlCommand
        Dim user As New clsUser

        Try
            'Conect to the dataBase
            connection = con.getConnection

            'Create object Command
            sql = "SELECT * FROM [User] " & _
                    " WHERE username=@Username "
            '" JOIN Person AS P ON U.ID_Person = P.ID_Person WHERE username=@Username "
            command = New SqlCommand(sql, connection)
            command.Parameters.Add("@Username", System.Data.SqlDbType.NVarChar)
            command.Parameters("@Username").Value = username

            'Get DataReader
            dataReader = command.ExecuteReader()

            'Get User
            If dataReader.HasRows Then
                dataReader.Read()
                With user
                    If Not IsDBNull(dataReader("ID_User")) Then
                        .UserID = dataReader("ID_User")
                    Else
                        .UserID = Nothing
                    End If

                    If Not IsDBNull(dataReader("username")) Then
                        .Username = dataReader("username")
                    Else
                        .Username = Nothing
                    End If

                    If Not IsDBNull(dataReader("password")) Then
                        .Password = dataReader("password")
                    Else
                        .Password = Nothing
                    End If

                    If Not IsDBNull(dataReader("type")) Then
                        .Type = dataReader("type")
                    Else
                        .Type = Nothing
                    End If

                    If Not IsDBNull(dataReader("ID_Teacher")) Then
                        .TeacherID = dataReader("ID_Teacher")
                    Else
                        .TeacherID = Nothing
                    End If
                End With
            Else
                user = Nothing
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

        Return user
    End Function


    ''' <summary>
    ''' Method that returns a list with all the Users from the database
    ''' </summary>
    ''' <returns>A list of Users</returns>
    ''' <remarks></remarks>
    Public Function getAllUsers() As List(Of clsUser)
        Dim listUsers As New List(Of clsUser)
        Dim connection As New SqlConnection
        Dim dataReader As SqlDataReader
        Dim sql As String
        Dim command As New SqlCommand

        Try
            'Conect to the dataBase
            connection = con.getConnection

            'Create object Command
            sql = "SELECT * FROM [User] "

            command = New SqlCommand(sql, connection)

            'Get DataReader
            dataReader = command.ExecuteReader()

            'Get Students
            While dataReader.Read()
                Dim user As New clsUser
                With user
                    If Not IsDBNull(dataReader("ID_User")) Then
                        .UserID = dataReader("ID_User")
                    Else
                        .UserID = Nothing
                    End If

                    If Not IsDBNull(dataReader("username")) Then
                        .Username = dataReader("username")
                    Else
                        .Username = Nothing
                    End If

                    If Not IsDBNull(dataReader("type")) Then
                        .Type = dataReader("type")
                    Else
                        .Type = Nothing
                    End If

                    If Not IsDBNull(dataReader("ID_Teacher")) Then
                        .TeacherID = dataReader("ID_Teacher")
                    Else
                        .TeacherID = Nothing
                    End If

                End With

                listUsers.Add(user)
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

        Return listUsers
    End Function

    ''' <summary>
    ''' Method that inserts a new User into the database
    ''' </summary>
    ''' <param name="user">An object clsUser</param>
    ''' <returns>Returns an integer with the number of rows afected.</returns>
    ''' <remarks></remarks>
    Public Function insertUser(ByVal user As clsUser) As Integer
        Dim connection As New SqlConnection
        Dim sql As String
        Dim command As New SqlCommand
        Dim newPersonID As New Integer
        Dim numReg As Integer

        'Conect to the dataBase
        connection = con.getConnection

        'Insert User
        sql = "INSERT INTO [User] (Username,[password],[type],ID_Teacher) " _
            & "VALUES(@Username, '1234', 0, @TeacherID)"

        command = New SqlCommand(sql, connection)
        If user.Username Is "" Or user.Username Is Nothing Then
            command.Parameters.AddWithValue("@Username", DBNull.Value)
        Else
            command.Parameters.AddWithValue("@Username", user.Username)
        End If

        If user.TeacherID = 0 Then
            command.Parameters.AddWithValue("@TeacherID", DBNull.Value)
        Else
            command.Parameters.AddWithValue("@TeacherID", user.TeacherID)
        End If

        'Execute insert User
        numReg = command.ExecuteNonQuery()

        'Disconnect from the database
        con.closeConnection(connection)

        Return numReg
    End Function

    ''' <summary>
    ''' Method that deletes a User from the database
    ''' </summary>
    ''' <param name="userID">An Integer</param>
    ''' <returns></returns>
    ''' <remarks>Returns an integer with the number of rows afected.</remarks>
    Public Function deleteUser(ByVal userID As Integer) As Integer
        Dim connection As New SqlConnection
        Dim numReg As Integer
        Dim sql As String
        Dim command As New SqlCommand

        Try
            'Conect to the dataBase
            connection = con.getConnection

            'Create object Command
            sql = "DELETE FROM [User] WHERE ID_User = @UserID "

            command = New SqlCommand(sql, connection)
            If userID = 0 Then
                command.Parameters.AddWithValue("@UserID", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@UserID", userID)
            End If
            'Execute deletion of the product
            numReg = command.ExecuteNonQuery()

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

        Return numReg
    End Function

    ''' <summary>
    ''' Method that changes the Username of an User
    ''' </summary>
    ''' <param name="teacherID">An Integer</param>
    ''' <param name="newUsername">A String</param>
    ''' <returns>Returns an integer with the number of rows afected.</returns>
    ''' <remarks></remarks>
    Public Function changeUsername(ByVal teacherID As Integer, ByVal newUsername As String) As Integer
        Dim numReg As Integer
        Dim connection As New SqlConnection
        Dim sql As String
        Dim command As New SqlCommand

        'Conect to the dataBase
        connection = con.getConnection

        'Create object Command
        sql = "UPDATE [User] SET username = @Username " & _
                                " WHERE ID_Teacher = @IDTeacher "

        command = New SqlCommand(sql, connection)
        command.Parameters.AddWithValue("@Username", newUsername)
        command.Parameters.AddWithValue("@IDTeacher", teacherID)

        'Get dataReader
        numReg = command.ExecuteNonQuery()

        'Disconnect from the database
        con.closeConnection(connection)

        Return numReg
    End Function

    ''' <summary>
    ''' Method that changes the password of an User
    ''' </summary>
    ''' <param name="username">A String</param>
    ''' <param name="currentPassword">A String</param>
    ''' <param name="newPassword">A String</param>
    ''' <returns>Returns an integer with the number of rows afected.</returns>
    ''' <remarks></remarks>
    Public Function changePassword(ByVal username As String, ByVal currentPassword As String, ByVal newPassword As String) As Boolean
        Dim numReg As Integer
        Dim connection As New SqlConnection
        Dim sql As String
        Dim command As New SqlCommand

        'Conect to the dataBase
        connection = con.getConnection

        'Create object Command
        sql = "UPDATE [User] SET password = @NewPassword " & _
                                " WHERE username = @Username and password = @CurrentPassword "

        command = New SqlCommand(sql, connection)
        command.Parameters.AddWithValue("@Username", username)
        command.Parameters.AddWithValue("@CurrentPassword", currentPassword)
        command.Parameters.AddWithValue("@NewPassword", newPassword)

        'Get dataReader
        numReg = command.ExecuteNonQuery()

        'Disconnect from the database
        con.closeConnection(connection)

        Return numReg
    End Function

    ''' <summary>
    ''' Method that sets an Administrator User 
    ''' </summary>
    ''' <param name="userID">A Integer</param>
    ''' <returns>Returns an integer with the number of rows afected.</returns>
    ''' <remarks></remarks>
    Public Function setAdministrator(ByVal userID As Integer) As Integer
        Dim numReg As Integer
        Dim connection As New SqlConnection
        Dim sql As String
        Dim command As New SqlCommand

        'Conect to the dataBase
        connection = con.getConnection

        'Create object Command
        sql = "UPDATE [User] SET [type] = 1 " & _
                " WHERE ID_User = @UserID "

        command = New SqlCommand(sql, connection)
        command.Parameters.AddWithValue("@UserID", userID)

        'Get dataReader
        numReg = command.ExecuteNonQuery()

        'Disconnect from the database
        con.closeConnection(connection)

        Return numReg
    End Function

    ''' <summary>
    ''' Method that unsets an Administrator User 
    ''' </summary>
    ''' <param name="userID">A Integer</param>
    ''' <returns>Returns an integer with the number of rows afected.</returns>
    ''' <remarks></remarks>
    Public Function unsetAdministrator(ByVal userID As Integer) As Integer
        Dim numReg As Integer
        Dim connection As New SqlConnection
        Dim sql As String
        Dim command As New SqlCommand

        'Conect to the dataBase
        connection = con.getConnection

        'Create object Command
        sql = "UPDATE [User] SET [type] = 0 " & _
                " WHERE ID_User = @UserID "

        command = New SqlCommand(sql, connection)
        command.Parameters.AddWithValue("@UserID", userID)

        'Get dataReader
        numReg = command.ExecuteNonQuery()

        'Disconnect from the database
        con.closeConnection(connection)

        Return numReg
    End Function
#End Region
End Class
