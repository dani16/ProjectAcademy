Imports System.Data.SqlClient
Imports System.IO

Public Class clsTeacherManager
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
    ''' Method that returns an Teacher from the database
    ''' </summary>
    ''' <pre>None</pre>
    ''' <param name="teacherID">An Integer</param>
    ''' <returns>An object clsTeacher</returns>
    ''' <pos>Returns the Teacher.</pos>
    ''' <remarks></remarks>
    Public Function getTeacher(teacherID As Integer) As clsTeacher
        Dim connection As New SqlConnection
        Dim dataReader As SqlDataReader
        Dim sql As String
        Dim command As New SqlCommand
        Dim teacher As New clsTeacher

        Try
            'Conect to the dataBase
            connection = con.getConnection

            'Create object Command
            sql = "SELECT * FROM Teacher AS T " & _
                " JOIN Person AS P ON T.ID_Person = P.ID_Person WHERE ID_Teacher=@TeacherID "
            command = New SqlCommand(sql, connection)
            command.Parameters.Add("@TeacherId", System.Data.SqlDbType.NVarChar)
            command.Parameters("@TeacherId").Value = teacherID

            'Get DataReader
            dataReader = command.ExecuteReader()

            'Get Teacher
            If dataReader.HasRows Then
                dataReader.Read()
                With teacher
                    If Not IsDBNull(dataReader("ID_Teacher")) Then
                        .TeacherID = dataReader("ID_Teacher")
                    Else
                        .TeacherID = Nothing
                    End If

                    If Not IsDBNull(dataReader("name")) Then
                        .Name = dataReader("name")
                    Else
                        .Name = Nothing
                    End If

                    If Not IsDBNull(dataReader("surname")) Then
                        .Surname = dataReader("surname")
                    Else
                        .Surname = Nothing
                    End If

                    If Not IsDBNull(dataReader("DNI")) Then
                        .DNI = dataReader("DNI")
                    Else
                        .DNI = Nothing
                    End If

                    If Not IsDBNull(dataReader("sex")) Then
                        .Sex = dataReader("sex")
                    Else
                        .Sex = Nothing
                    End If

                    If Not IsDBNull(dataReader("birthDate")) Then
                        .BirthDate = dataReader("birthDate")
                    Else
                        .BirthDate = Nothing
                    End If

                    If Not IsDBNull(dataReader("address")) Then
                        .Address = dataReader("address")
                    Else
                        .Address = Nothing
                    End If

                    If Not IsDBNull(dataReader("city")) Then
                        .City = dataReader("city")
                    Else
                        .City = Nothing
                    End If

                    If Not IsDBNull(dataReader("postalCode")) Then
                        .PostalCode = dataReader("postalCode")
                    Else
                        .PostalCode = Nothing
                    End If

                    If Not IsDBNull(dataReader("telephone")) Then
                        .Telephone = dataReader("telephone")
                    Else
                        .Telephone = Nothing
                    End If

                    If Not IsDBNull(dataReader("email")) Then
                        .Email = dataReader("email")
                    Else
                        .Email = Nothing
                    End If

                    If Not IsDBNull(dataReader("ID_Person")) Then
                        .PersonID = dataReader("ID_Person")
                    Else
                        .PersonID = Nothing
                    End If

                    'Get Image
                    If Not IsDBNull(dataReader("photo")) Then
                        Dim image As Byte() = New Byte() {}
                        image = CType(dataReader("photo"), Byte())
                        Dim bitmap As New BitmapImage()
                        Dim stream As New MemoryStream(image)
                        Dim getImage As New Image

                        bitmap.BeginInit()
                        bitmap.CacheOption = BitmapCacheOption.OnLoad
                        bitmap.StreamSource = stream
                        bitmap.EndInit()

                        getImage.Source = bitmap
                        .Photo = getImage
                    Else
                        .Photo = Nothing
                    End If
                End With
            Else
                teacher = Nothing
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

        Return teacher
    End Function

    ''' <summary>
    ''' Method that returns a list with all the Teachers from the database
    ''' </summary>
    ''' <returns>A list of Teachers</returns>
    ''' <remarks></remarks>
    Public Function getAllTeachers() As List(Of clsTeacher)
        Dim listTeachers As New List(Of clsTeacher)
        Dim connection As New SqlConnection
        Dim dataReader As SqlDataReader
        Dim sql As String
        Dim command As New SqlCommand

        Try
            'Conect to the dataBase
            connection = con.getConnection

            'Create object Command
            sql = "SELECT * FROM Teacher AS T " & _
                " JOIN Person AS P ON T.ID_Person = P.ID_Person " & _
                " ORDER BY P.name, P.surname "

            command = New SqlCommand(sql, connection)

            'Get DataReader
            dataReader = command.ExecuteReader()

            'Get Product
            While dataReader.Read()
                Dim teacher As New clsTeacher
                With teacher
                    If Not IsDBNull(dataReader("ID_Teacher")) Then
                        .TeacherID = dataReader("ID_Teacher")
                    Else
                        .TeacherID = Nothing
                    End If

                    If Not IsDBNull(dataReader("name")) Then
                        .Name = dataReader("name")
                    Else
                        .Name = Nothing
                    End If

                    If Not IsDBNull(dataReader("surname")) Then
                        .Surname = dataReader("surname")
                    Else
                        .Surname = Nothing
                    End If

                    If Not IsDBNull(dataReader("DNI")) Then
                        .DNI = dataReader("DNI")
                    Else
                        .DNI = Nothing
                    End If

                    If Not IsDBNull(dataReader("sex")) Then
                        .Sex = dataReader("sex")
                    Else
                        .Sex = Nothing
                    End If

                    If Not IsDBNull(dataReader("birthDate")) Then
                        .BirthDate = dataReader("birthDate")
                    Else
                        .BirthDate = Nothing
                    End If

                    If Not IsDBNull(dataReader("address")) Then
                        .Address = dataReader("address")
                    Else
                        .Address = Nothing
                    End If

                    If Not IsDBNull(dataReader("city")) Then
                        .City = dataReader("city")
                    Else
                        .City = Nothing
                    End If

                    If Not IsDBNull(dataReader("postalCode")) Then
                        .PostalCode = dataReader("postalCode")
                    Else
                        .PostalCode = Nothing
                    End If

                    If Not IsDBNull(dataReader("telephone")) Then
                        .Telephone = dataReader("telephone")
                    Else
                        .Telephone = Nothing
                    End If

                    If Not IsDBNull(dataReader("email")) Then
                        .Email = dataReader("email")
                    Else
                        .Email = Nothing
                    End If

                    If Not IsDBNull(dataReader("ID_Person")) Then
                        .PersonID = dataReader("ID_Person")
                    Else
                        .PersonID = Nothing
                    End If

                    'Get Image
                    If Not IsDBNull(dataReader("photo")) Then
                        Dim image As Byte() = New Byte() {}
                        image = CType(dataReader("photo"), Byte())
                        Dim bitmap As New BitmapImage()
                        Dim stream As New MemoryStream(image)
                        Dim getImage As New Image

                        bitmap.BeginInit()
                        bitmap.CacheOption = BitmapCacheOption.OnLoad
                        bitmap.StreamSource = stream
                        bitmap.EndInit()

                        getImage.Source = bitmap
                        .Photo = getImage
                    Else
                        .Photo = Nothing
                    End If
                End With

                listTeachers.Add(teacher)
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

        Return listTeachers
    End Function

    ''' <summary>
    ''' Method that returns a list of Teachers that have not user account from the database
    ''' </summary>
    ''' <returns>A list of Teachers</returns>
    ''' <remarks></remarks>
    Public Function getNotUserTeachers() As List(Of clsTeacher)
        Dim listTeachers As New List(Of clsTeacher)
        Dim connection As New SqlConnection
        Dim dataReader As SqlDataReader
        Dim sql As String
        Dim command As New SqlCommand

        Try
            'Conect to the dataBase
            connection = con.getConnection

            'Create object Command
            sql = "SELECT * FROM Teacher AS T " & _
                    " JOIN Person AS P ON T.ID_Person = P.ID_Person " & _
                    " EXCEPT SELECT T.*, P.* FROM [User] AS U " & _
                        " JOIN Teacher AS T ON U.ID_Teacher = T.ID_Teacher " & _
                        " JOIN Person AS P ON T.ID_Person = P.ID_Person " & _
                        " ORDER BY P.name, P.surname "

            command = New SqlCommand(sql, connection)

            'Get DataReader
            dataReader = command.ExecuteReader()

            'Get Product
            While dataReader.Read()
                Dim teacher As New clsTeacher
                With teacher
                    If Not IsDBNull(dataReader("ID_Teacher")) Then
                        .TeacherID = dataReader("ID_Teacher")
                    Else
                        .TeacherID = Nothing
                    End If

                    If Not IsDBNull(dataReader("name")) Then
                        .Name = dataReader("name")
                    Else
                        .Name = Nothing
                    End If

                    If Not IsDBNull(dataReader("surname")) Then
                        .Surname = dataReader("surname")
                    Else
                        .Surname = Nothing
                    End If

                    If Not IsDBNull(dataReader("DNI")) Then
                        .DNI = dataReader("DNI")
                    Else
                        .DNI = Nothing
                    End If

                    If Not IsDBNull(dataReader("sex")) Then
                        .Sex = dataReader("sex")
                    Else
                        .Sex = Nothing
                    End If

                    If Not IsDBNull(dataReader("birthDate")) Then
                        .BirthDate = dataReader("birthDate")
                    Else
                        .BirthDate = Nothing
                    End If

                    If Not IsDBNull(dataReader("address")) Then
                        .Address = dataReader("address")
                    Else
                        .Address = Nothing
                    End If

                    If Not IsDBNull(dataReader("city")) Then
                        .City = dataReader("city")
                    Else
                        .City = Nothing
                    End If

                    If Not IsDBNull(dataReader("postalCode")) Then
                        .PostalCode = dataReader("postalCode")
                    Else
                        .PostalCode = Nothing
                    End If

                    If Not IsDBNull(dataReader("telephone")) Then
                        .Telephone = dataReader("telephone")
                    Else
                        .Telephone = Nothing
                    End If

                    If Not IsDBNull(dataReader("email")) Then
                        .Email = dataReader("email")
                    Else
                        .Email = Nothing
                    End If

                    If Not IsDBNull(dataReader("ID_Person")) Then
                        .PersonID = dataReader("ID_Person")
                    Else
                        .PersonID = Nothing
                    End If

                    'Get Image
                    If Not IsDBNull(dataReader("photo")) Then
                        Dim image As Byte() = New Byte() {}
                        image = CType(dataReader("photo"), Byte())
                        Dim bitmap As New BitmapImage()
                        Dim stream As New MemoryStream(image)
                        Dim getImage As New Image

                        bitmap.BeginInit()
                        bitmap.CacheOption = BitmapCacheOption.OnLoad
                        bitmap.StreamSource = stream
                        bitmap.EndInit()

                        getImage.Source = bitmap
                        .Photo = getImage
                    Else
                        .Photo = Nothing
                    End If
                End With

                listTeachers.Add(teacher)
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

        Return listTeachers
    End Function

    ''' <summary>
    ''' Method that returns a list of Teachers that have not user account from the database
    ''' </summary>
    ''' <param name="search">A String</param>
    ''' <returns>A list of Teachers</returns>
    ''' <remarks></remarks>
    Public Function getNotUserTeachers(ByVal search As String) As List(Of clsTeacher)
        Dim listTeachers As New List(Of clsTeacher)
        Dim connection As New SqlConnection
        Dim dataReader As SqlDataReader
        Dim sql As String
        Dim command As New SqlCommand

        Try
            'Conect to the dataBase
            connection = con.getConnection

            'Create object Command
            sql = "SELECT * FROM Teacher AS T " & _
                " JOIN Person AS P ON T.ID_Person = P.ID_Person " & _
                " WHERE P.name LIKE @Search or P.surname LIKE @Search OR " & _
                " P.name + ' ' + surname LIKE @Search " & _
                " EXCEPT SELECT T.*,P.* FROM Teacher AS T " & _
                    " JOIN Person AS P ON T.ID_Person = P.ID_Person " & _
                    " JOIN [User] AS U ON T.ID_Teacher = U.ID_Teacher " & _
                    " ORDER BY P.name, P.surname "

            command = New SqlCommand(sql, connection)
            command.Parameters.Add("@Search", System.Data.SqlDbType.NVarChar)
            command.Parameters("@Search").Value = "%" & search & "%"

            'Get DataReader
            dataReader = command.ExecuteReader()

            'Get Product
            While dataReader.Read()
                Dim teacher As New clsTeacher
                With teacher
                    If Not IsDBNull(dataReader("ID_Teacher")) Then
                        .TeacherID = dataReader("ID_Teacher")
                    Else
                        .TeacherID = Nothing
                    End If

                    If Not IsDBNull(dataReader("name")) Then
                        .Name = dataReader("name")
                    Else
                        .Name = Nothing
                    End If

                    If Not IsDBNull(dataReader("surname")) Then
                        .Surname = dataReader("surname")
                    Else
                        .Surname = Nothing
                    End If

                    If Not IsDBNull(dataReader("DNI")) Then
                        .DNI = dataReader("DNI")
                    Else
                        .DNI = Nothing
                    End If

                    If Not IsDBNull(dataReader("sex")) Then
                        .Sex = dataReader("sex")
                    Else
                        .Sex = Nothing
                    End If

                    If Not IsDBNull(dataReader("birthDate")) Then
                        .BirthDate = dataReader("birthDate")
                    Else
                        .BirthDate = Nothing
                    End If

                    If Not IsDBNull(dataReader("address")) Then
                        .Address = dataReader("address")
                    Else
                        .Address = Nothing
                    End If

                    If Not IsDBNull(dataReader("city")) Then
                        .City = dataReader("city")
                    Else
                        .City = Nothing
                    End If

                    If Not IsDBNull(dataReader("postalCode")) Then
                        .PostalCode = dataReader("postalCode")
                    Else
                        .PostalCode = Nothing
                    End If

                    If Not IsDBNull(dataReader("telephone")) Then
                        .Telephone = dataReader("telephone")
                    Else
                        .Telephone = Nothing
                    End If

                    If Not IsDBNull(dataReader("email")) Then
                        .Email = dataReader("email")
                    Else
                        .Email = Nothing
                    End If

                    If Not IsDBNull(dataReader("ID_Person")) Then
                        .PersonID = dataReader("ID_Person")
                    Else
                        .PersonID = Nothing
                    End If

                    'Get Image
                    If Not IsDBNull(dataReader("photo")) Then
                        Dim image As Byte() = New Byte() {}
                        image = CType(dataReader("photo"), Byte())
                        Dim bitmap As New BitmapImage()
                        Dim stream As New MemoryStream(image)
                        Dim getImage As New Image

                        bitmap.BeginInit()
                        bitmap.CacheOption = BitmapCacheOption.OnLoad
                        bitmap.StreamSource = stream
                        bitmap.EndInit()

                        getImage.Source = bitmap
                        .Photo = getImage
                    Else
                        .Photo = Nothing
                    End If
                End With

                listTeachers.Add(teacher)
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

        Return listTeachers
    End Function

    ''' <summary>
    ''' Method that returns a list of Person with all the Teachers from the database
    ''' </summary>
    ''' <param name="search">A String</param>
    ''' <returns>A list of Teachers</returns>
    ''' <remarks></remarks>
    Public Function getAllTeachers(ByVal search As String) As List(Of clsTeacher)
        Dim listTeachers As New List(Of clsTeacher)
        Dim connection As New SqlConnection
        Dim dataReader As SqlDataReader
        Dim sql As String
        Dim command As New SqlCommand

        Try
            'Conect to the dataBase
            connection = con.getConnection

            'Create object Command
            sql = "SELECT * FROM Teacher AS T " & _
                " JOIN Person AS P ON T.ID_Person = P.ID_Person " & _
                " WHERE P.name LIKE @Search or P.surname LIKE @Search OR " & _
                " P.name + ' ' + surname LIKE @Search " & _
                " ORDER BY P.name, P.surname "

            command = New SqlCommand(sql, connection)
            command.Parameters.Add("@Search", System.Data.SqlDbType.NVarChar)
            command.Parameters("@Search").Value = "%" & search & "%"

            'Get DataReader
            dataReader = command.ExecuteReader()

            'Get Product
            While dataReader.Read()
                Dim teacher As New clsTeacher
                With teacher
                    If Not IsDBNull(dataReader("ID_Teacher")) Then
                        .TeacherID = dataReader("ID_Teacher")
                    Else
                        .TeacherID = Nothing
                    End If

                    If Not IsDBNull(dataReader("name")) Then
                        .Name = dataReader("name")
                    Else
                        .Name = Nothing
                    End If

                    If Not IsDBNull(dataReader("surname")) Then
                        .Surname = dataReader("surname")
                    Else
                        .Surname = Nothing
                    End If

                    If Not IsDBNull(dataReader("DNI")) Then
                        .DNI = dataReader("DNI")
                    Else
                        .DNI = Nothing
                    End If

                    If Not IsDBNull(dataReader("sex")) Then
                        .Sex = dataReader("sex")
                    Else
                        .Sex = Nothing
                    End If

                    If Not IsDBNull(dataReader("birthDate")) Then
                        .BirthDate = dataReader("birthDate")
                    Else
                        .BirthDate = Nothing
                    End If

                    If Not IsDBNull(dataReader("address")) Then
                        .Address = dataReader("address")
                    Else
                        .Address = Nothing
                    End If

                    If Not IsDBNull(dataReader("city")) Then
                        .City = dataReader("city")
                    Else
                        .City = Nothing
                    End If

                    If Not IsDBNull(dataReader("postalCode")) Then
                        .PostalCode = dataReader("postalCode")
                    Else
                        .PostalCode = Nothing
                    End If

                    If Not IsDBNull(dataReader("telephone")) Then
                        .Telephone = dataReader("telephone")
                    Else
                        .Telephone = Nothing
                    End If

                    If Not IsDBNull(dataReader("email")) Then
                        .Email = dataReader("email")
                    Else
                        .Email = Nothing
                    End If

                    If Not IsDBNull(dataReader("ID_Person")) Then
                        .PersonID = dataReader("ID_Person")
                    Else
                        .PersonID = Nothing
                    End If

                    'Get Image
                    If Not IsDBNull(dataReader("photo")) Then
                        Dim image As Byte() = New Byte() {}
                        image = CType(dataReader("photo"), Byte())
                        Dim bitmap As New BitmapImage()
                        Dim stream As New MemoryStream(image)
                        Dim getImage As New Image

                        bitmap.BeginInit()
                        bitmap.CacheOption = BitmapCacheOption.OnLoad
                        bitmap.StreamSource = stream
                        bitmap.EndInit()

                        getImage.Source = bitmap
                        .Photo = getImage
                    Else
                        .Photo = Nothing
                    End If
                End With

                listTeachers.Add(teacher)
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

        Return listTeachers
    End Function

    ''' <summary>
    ''' Method that returns a list with all the Person of the Teachers that are no Students,
    ''' and match the search from the database
    ''' </summary>
    ''' <returns>A list of Teachers</returns>
    ''' <remarks></remarks>
    Public Function getNotStudentTeachers(ByVal search As String) As List(Of clsTeacher)
        Dim listTeachers As New List(Of clsTeacher)
        Dim connection As New SqlConnection
        Dim dataReader As SqlDataReader
        Dim sql As String
        Dim command As New SqlCommand

        Try
            'Conect to the dataBase
            connection = con.getConnection

            'Create object Command
            sql = "SELECT P.*, T.* FROM Teacher AS T " & _
                " JOIN Person AS P ON T.ID_Person = P.ID_Person " & _
                " WHERE P.name LIKE @Search or P.surname LIKE @Search OR " & _
                " P.name + ' ' + surname LIKE @Search " & _
                " EXCEPT SELECT P.*, T.* FROM Teacher AS T " & _
                    " JOIN Person AS P ON T.ID_Person = P.ID_Person " & _
                    " JOIN Student AS S ON P.ID_Person = S.ID_Person  " & _
                    " ORDER BY P.name, P.surname "

            command = New SqlCommand(sql, connection)
            command.Parameters.Add("@Search", System.Data.SqlDbType.NVarChar)
            command.Parameters("@Search").Value = "%" & search & "%"

            'Get DataReader
            dataReader = command.ExecuteReader()

            'Get Product
            While dataReader.Read()
                Dim teacher As New clsTeacher
                With teacher
                    If Not IsDBNull(dataReader("ID_Person")) Then
                        .PersonID = dataReader("ID_Person")
                    Else
                        .PersonID = Nothing
                    End If

                    If Not IsDBNull(dataReader("name")) Then
                        .Name = dataReader("name")
                    Else
                        .Name = Nothing
                    End If

                    If Not IsDBNull(dataReader("surname")) Then
                        .Surname = dataReader("surname")
                    Else
                        .Surname = Nothing
                    End If

                    If Not IsDBNull(dataReader("DNI")) Then
                        .DNI = dataReader("DNI")
                    Else
                        .DNI = Nothing
                    End If

                    If Not IsDBNull(dataReader("sex")) Then
                        .Sex = dataReader("sex")
                    Else
                        .Sex = Nothing
                    End If

                    If Not IsDBNull(dataReader("birthDate")) Then
                        .BirthDate = dataReader("birthDate")
                    Else
                        .BirthDate = Nothing
                    End If

                    If Not IsDBNull(dataReader("address")) Then
                        .Address = dataReader("address")
                    Else
                        .Address = Nothing
                    End If

                    If Not IsDBNull(dataReader("city")) Then
                        .City = dataReader("city")
                    Else
                        .City = Nothing
                    End If

                    If Not IsDBNull(dataReader("postalCode")) Then
                        .PostalCode = dataReader("postalCode")
                    Else
                        .PostalCode = Nothing
                    End If

                    If Not IsDBNull(dataReader("telephone")) Then
                        .Telephone = dataReader("telephone")
                    Else
                        .Telephone = Nothing
                    End If

                    If Not IsDBNull(dataReader("email")) Then
                        .Email = dataReader("email")
                    Else
                        .Email = Nothing
                    End If

                    'Get Image
                    If Not IsDBNull(dataReader("photo")) Then
                        Dim image As Byte() = New Byte() {}
                        image = CType(dataReader("photo"), Byte())
                        Dim bitmap As New BitmapImage()
                        Dim stream As New MemoryStream(image)
                        Dim getImage As New Image

                        bitmap.BeginInit()
                        bitmap.CacheOption = BitmapCacheOption.OnLoad
                        bitmap.StreamSource = stream
                        bitmap.EndInit()

                        getImage.Source = bitmap
                        .Photo = getImage
                    Else
                        .Photo = Nothing
                    End If
                End With

                listTeachers.Add(teacher)
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

        Return listTeachers
    End Function


    ''' <summary>
    ''' Method that inserts a new Teacher into the database
    ''' </summary>
    ''' <param name="teacher">An object clsTeacher</param>
    ''' <returns>Returns an integer with the number of rows afected.</returns>
    ''' <remarks></remarks>
    Public Function insertTeacher(ByVal teacher As clsTeacher) As Integer
        Dim connection As New SqlConnection
        Dim sql As String
        Dim command As New SqlCommand
        Dim newPersonID As New Integer
        Dim numReg As Integer

        'Conect to the dataBase
        connection = con.getConnection

        'Insert User
        sql = "INSERT INTO Teacher (ID_Person) " _
            & "VALUES(@PersonID)"

        command = New SqlCommand(sql, connection)
        If teacher.PersonID = 0 Then
            command.Parameters.AddWithValue("@PersonID", DBNull.Value)
        Else
            command.Parameters.AddWithValue("@PersonID", teacher.PersonID)
        End If

        'Execute insert User
        numReg = command.ExecuteNonQuery()

        'Disconnect from the database
        con.closeConnection(connection)

        Return numReg
    End Function

    ''' <summary>
    ''' Method that deletes a Teacher from the database
    ''' </summary>
    ''' <param name="teacherID">An Integer</param>
    ''' <returns></returns>
    ''' <remarks>Returns an integer with the number of rows afected.</remarks>
    Public Function deleteTeacher(ByVal teacherID As Integer) As Integer
        Dim connection As New SqlConnection
        Dim numReg As Integer
        Dim sql As String
        Dim command As New SqlCommand

        Try
            'Conect to the dataBase
            connection = con.getConnection

            'Create object Command
            sql = "DELETE FROM Teacher WHERE ID_Teacher = @TeacherID "

            command = New SqlCommand(sql, connection)
            If teacherID = 0 Then
                command.Parameters.AddWithValue("@TeacherID", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@TeacherID", teacherID)
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
#End Region
End Class
