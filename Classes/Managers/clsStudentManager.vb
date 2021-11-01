Imports System.Data.SqlClient
Imports System.IO

Public Class clsStudentManager
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
    ''' Method that returns an Student from the database
    ''' </summary>
    ''' <pre>None</pre>
    ''' <param name="studentID">An Integer</param>
    ''' <returns>An object clsStudent</returns>
    ''' <pos>Returns the user.</pos>
    ''' <remarks></remarks>
    Public Function getStudent(studentID As Integer) As clsStudent
        Dim connection As New SqlConnection
        Dim dataReader As SqlDataReader
        Dim sql As String
        Dim command As New SqlCommand
        Dim student As New clsStudent

        Try
            'Conect to the dataBase
            connection = con.getConnection

            'Create object Command
            sql = "SELECT * FROM Student AS S " & _
                " JOIN Person AS P ON S.ID_Person = P.ID_Person WHERE ID_Student=@StudentID "
            command = New SqlCommand(sql, connection)
            If studentID = 0 Then
                command.Parameters.AddWithValue("@StudentID", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@StudentID", studentID)
            End If

            'Get DataReader
            dataReader = command.ExecuteReader()

            'Get Student
            If dataReader.HasRows Then
                dataReader.Read()
                With student
                    If Not IsDBNull(dataReader("ID_Student")) Then
                        .StudentID = dataReader("ID_Student")
                    Else
                        .StudentID = Nothing
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

                    If Not IsDBNull(dataReader("situation")) Then
                        .Situation = dataReader("situation")
                    Else
                        .Situation = Nothing
                    End If

                    If Not IsDBNull(dataReader("englishLevel")) Then
                        .EnglishLevel = dataReader("englishLevel")
                    Else
                        .EnglishLevel = Nothing
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
                student = Nothing
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

        Return student
    End Function

    ''' <summary>
    ''' Method that returns a list with all the Students from the database
    ''' </summary>
    ''' <returns>A list of Student</returns>
    ''' <remarks></remarks>
    Public Function getAllStudents() As List(Of clsStudent)
        Dim listStudents As New List(Of clsStudent)
        Dim connection As New SqlConnection
        Dim dataReader As SqlDataReader
        Dim sql As String
        Dim command As New SqlCommand

        Try
            'Conect to the dataBase
            connection = con.getConnection

            'Create object Command
            sql = "SELECT * FROM Student AS S " & _
                    " JOIN Person AS P ON S.ID_Person = P.ID_Person " & _
                    " ORDER BY P.name, P.surname "

            command = New SqlCommand(sql, connection)

            'Get DataReader
            dataReader = command.ExecuteReader()

            'Get Students
            While dataReader.Read()
                Dim student As New clsStudent
                With student
                    If Not IsDBNull(dataReader("ID_Student")) Then
                        .StudentID = dataReader("ID_Student")
                    Else
                        .StudentID = Nothing
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

                    If Not IsDBNull(dataReader("situation")) Then
                        .Situation = dataReader("situation")
                    Else
                        .Situation = Nothing
                    End If

                    If Not IsDBNull(dataReader("englishLevel")) Then
                        .EnglishLevel = dataReader("englishLevel")
                    Else
                        .EnglishLevel = Nothing
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

                listStudents.Add(student)
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

        Return listStudents
    End Function

    ''' <summary>
    ''' Method that returns a list with all the Students that do not belong 
    ''' to a group from the database
    ''' </summary>
    ''' <param name="groupID">An Integer</param>
    ''' <param name="search">A String</param>
    ''' <returns>A list of Student</returns>
    ''' <remarks></remarks>
    Public Function getNotGroupStudents(ByVal groupID As Integer, ByVal search As String) As List(Of clsStudent)
        Dim listStudents As New List(Of clsStudent)
        Dim connection As New SqlConnection
        Dim dataReader As SqlDataReader
        Dim sql As String
        Dim command As New SqlCommand

        Try
            'Conect to the dataBase
            connection = con.getConnection

            'Create object Command
            sql = "SELECT S.*, P.* FROM Student AS S " & _
                    " JOIN Person AS P ON S.ID_Person = P.ID_Person " & _
                    " WHERE P.name LIKE @Search OR P.surname LIKE @Search OR " & _
                                " P.name + ' ' + P.surname LIKE @Search " & _
                    " EXCEPT SELECT S.*, P.* FROM Student AS S " & _
                                " JOIN Person AS P ON S.ID_Person = P.ID_Person " & _
                                " JOIN Inscription AS I ON S.ID_Student = I.ID_Student " & _
                                " WHERE I.ID_Group = @GroupID " & _
                                " ORDER BY P.name, P.surname "

            command = New SqlCommand(sql, connection)
            command.Parameters.Add("@GroupID", System.Data.SqlDbType.Int)
            command.Parameters("@GroupID").Value = groupID
            command.Parameters.Add("@Search", System.Data.SqlDbType.NVarChar)
            command.Parameters("@Search").Value = "%" & search & "%"

            'Get DataReader
            dataReader = command.ExecuteReader()

            'Get Students
            While dataReader.Read()
                Dim student As New clsStudent
                With student
                    If Not IsDBNull(dataReader("ID_Student")) Then
                        .StudentID = dataReader("ID_Student")
                    Else
                        .StudentID = Nothing
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

                    If Not IsDBNull(dataReader("situation")) Then
                        .Situation = dataReader("situation")
                    Else
                        .Situation = Nothing
                    End If

                    If Not IsDBNull(dataReader("englishLevel")) Then
                        .EnglishLevel = dataReader("englishLevel")
                    Else
                        .EnglishLevel = Nothing
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

                listStudents.Add(student)
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

        Return listStudents
    End Function

    ''' <summary>
    ''' Method that returns a list with all the Students that are 
    ''' can be Teachers from the database
    ''' </summary>
    ''' <param name="search">A String</param>
    ''' <returns>A list of Student</returns>
    ''' <remarks></remarks>
    Public Function getNotTeacherStudents(ByVal search As String) As List(Of clsStudent)
        Dim listStudents As New List(Of clsStudent)
        Dim connection As New SqlConnection
        Dim dataReader As SqlDataReader
        Dim sql As String
        Dim command As New SqlCommand

        Try
            'Conect to the dataBase
            connection = con.getConnection

            'Create object Command
            sql = "SELECT P.*, S.* FROM Student AS S " & _
                    " JOIN Person AS P ON S.ID_Person = P.ID_Person " & _
                    " WHERE P.name LIKE @Search OR P.surname LIKE @Search OR " & _
                                " P.name + ' ' + P.surname LIKE @Search " & _
                    " EXCEPT SELECT P.*, S.* FROM Student AS S " & _
                                " JOIN Person AS P ON S.ID_Person = P.ID_Person " & _
                                " JOIN Teacher AS T ON P.ID_Person = T.ID_Person " & _
                                " ORDER BY P.name, P.surname "

            command = New SqlCommand(sql, connection)
            command.Parameters.Add("@Search", System.Data.SqlDbType.NVarChar)
            command.Parameters("@Search").Value = "%" & search & "%"

            'Get DataReader
            dataReader = command.ExecuteReader()

            'Get Students
            While dataReader.Read()
                Dim student As New clsStudent
                With student
                    If Not IsDBNull(dataReader("ID_Student")) Then
                        .StudentID = dataReader("ID_Student")
                    Else
                        .StudentID = Nothing
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

                    If Not IsDBNull(dataReader("situation")) Then
                        .Situation = dataReader("situation")
                    Else
                        .Situation = Nothing
                    End If

                    If Not IsDBNull(dataReader("englishLevel")) Then
                        .EnglishLevel = dataReader("englishLevel")
                    Else
                        .EnglishLevel = Nothing
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

                listStudents.Add(student)
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

        Return listStudents
    End Function

    ''' <summary>
    ''' Method that returns a list with all the Students of a Group from the database
    ''' </summary>
    ''' <param name="groupID">An Integer</param>
    ''' <returns>A list of Students</returns>
    ''' <remarks></remarks>
    Public Function getGroupStudents(ByVal groupID As Integer) As List(Of clsStudent)
        Dim listStudents As New List(Of clsStudent)
        Dim connection As New SqlConnection
        Dim dataReader As SqlDataReader
        Dim sql As String
        Dim command As New SqlCommand

        Try
            'Conect to the dataBase
            connection = con.getConnection

            'Create object Command
            sql = "SELECT * FROM Student AS S " & _
                " JOIN Person AS P ON S.ID_Person = P.ID_Person " & _
                " JOIN Inscription AS I ON S.ID_Student = I.ID_Student " & _
                " WHERE I.ID_Group = @GroupID " & _
                " ORDER BY P.name, P.surname "

            command = New SqlCommand(sql, connection)
            If groupID = 0 Then
                command.Parameters.AddWithValue("@GroupID", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@GroupID", groupID)
            End If

            'Get DataReader
            dataReader = command.ExecuteReader()

            'Get Students
            While dataReader.Read()
                Dim student As New clsStudent
                With student
                    If Not IsDBNull(dataReader("ID_Student")) Then
                        .StudentID = dataReader("ID_Student")
                    Else
                        .StudentID = Nothing
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

                    If Not IsDBNull(dataReader("situation")) Then
                        .Situation = dataReader("situation")
                    Else
                        .Situation = Nothing
                    End If

                    If Not IsDBNull(dataReader("englishLevel")) Then
                        .EnglishLevel = dataReader("englishLevel")
                    Else
                        .EnglishLevel = Nothing
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

                listStudents.Add(student)
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

        Return listStudents
    End Function

    ''' <summary>
    ''' Method that returns a list with all the Students that match the search from the database
    ''' </summary>
    ''' <param name="search">A String</param>
    ''' <returns>A list of Student</returns>
    ''' <remarks></remarks>
    Public Function getAllStudents(ByVal search As String) As List(Of clsStudent)
        Dim listStudents As New List(Of clsStudent)
        Dim connection As New SqlConnection
        Dim dataReader As SqlDataReader
        Dim sql As String
        Dim command As New SqlCommand

        Try
            'Conect to the dataBase
            connection = con.getConnection

            'Create object Command
            sql = "SELECT * FROM Student AS S " & _
                " JOIN Person AS P ON S.ID_Person = P.ID_Person " & _
                " WHERE P.name LIKE @Search OR P.surname LIKE @Search OR " & _
                        " P.name + ' ' + surname LIKE @Search " & _
                " ORDER BY P.name, P.surname "

            command = New SqlCommand(sql, connection)
            command.Parameters.Add("@Search", System.Data.SqlDbType.NVarChar)
            command.Parameters("@Search").Value = "%" & search & "%"

            'Get DataReader
            dataReader = command.ExecuteReader()

            'Get Students
            While dataReader.Read()
                Dim student As New clsStudent
                With student
                    If Not IsDBNull(dataReader("ID_Student")) Then
                        .StudentID = dataReader("ID_Student")
                    Else
                        .StudentID = Nothing
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

                    If Not IsDBNull(dataReader("situation")) Then
                        .Situation = dataReader("situation")
                    Else
                        .Situation = Nothing
                    End If

                    If Not IsDBNull(dataReader("englishLevel")) Then
                        .EnglishLevel = dataReader("englishLevel")
                    Else
                        .EnglishLevel = Nothing
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

                listStudents.Add(student)
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

        Return listStudents
    End Function

    ''' <summary>
    ''' Method that updates a Student from the database
    ''' </summary>
    ''' <param name="student">An object clsStudent</param>
    ''' <returns>Returns an integer with the number of rows afected.</returns>
    ''' <remarks></remarks>
    Public Function updateStudent(ByVal student As clsStudent) As Integer
        Dim connection As New SqlConnection
        Dim numReg As Integer
        Dim sql As String
        Dim command As New SqlCommand
        Dim memStream As New MemoryStream()
        Dim encoder As New JpegBitmapEncoder()

        'Conect to the dataBase
        connection = con.getConnection

        'Create object Command
        sql = "UPDATE Student SET situation = @Situation " _
                                & ", englishlevel = @EnglishLevel " _
                                & " WHERE ID_Student = @StudentID"

        command = New SqlCommand(sql, connection)
        If student.Situation Is "" Or student.Situation Is Nothing Then
            command.Parameters.AddWithValue("@Situation", DBNull.Value)
        Else
            command.Parameters.AddWithValue("@Situation", student.Situation)
        End If

        If student.EnglishLevel Is "" Or student.EnglishLevel Is Nothing Then
            command.Parameters.AddWithValue("@EnglishLevel", DBNull.Value)
        Else
            command.Parameters.AddWithValue("@EnglishLevel", student.EnglishLevel)
        End If

        If student.StudentID = 0 Then
            command.Parameters.AddWithValue("@StudentID", DBNull.Value)
        Else
            command.Parameters.AddWithValue("@StudentID", student.StudentID)
        End If

        'Get dataReader
        numReg = command.ExecuteNonQuery()

        'Disconnect from the database
        con.closeConnection(connection)

        Return numReg
    End Function

    ''' <summary>
    ''' Method that inserts a new Student into the database
    ''' </summary>
    ''' <param name="student">An object clsStudent</param>
    ''' <returns>Returns an integer with the number of rows afected.</returns>
    ''' <remarks></remarks>
    Public Function insertStudent(ByVal student As clsStudent) As Integer
        Dim connection As New SqlConnection
        Dim sql As String
        Dim command As New SqlCommand
        Dim newPersonID As New Integer
        Dim numReg As Integer

        'Conect to the dataBase
        connection = con.getConnection

        'Insert User
        sql = "INSERT INTO Student (situation,ID_Person,englishLevel) " _
            & "VALUES(@Situation,@PersonID,@EnglishLevel)"

        command = New SqlCommand(sql, connection)
        If student.Situation Is "" Or student.Situation Is Nothing Then
            command.Parameters.AddWithValue("@Situation", DBNull.Value)
        Else
            command.Parameters.AddWithValue("@Situation", student.Situation)
        End If

        If student.PersonID = 0 Then
            command.Parameters.AddWithValue("@PersonID", DBNull.Value)
        Else
            command.Parameters.AddWithValue("@PersonID", student.PersonID)
        End If

        If student.EnglishLevel Is "" Or student.EnglishLevel Is Nothing Then
            command.Parameters.AddWithValue("@EnglishLevel", DBNull.Value)
        Else
            command.Parameters.AddWithValue("@EnglishLevel", student.EnglishLevel)
        End If

        'Execute insert Student
        numReg = command.ExecuteNonQuery()

        'Disconnect from the database
        con.closeConnection(connection)

        Return numReg
    End Function

    ''' <summary>
    ''' Method that deletes a Student from the database
    ''' </summary>
    ''' <param name="studentID">An Integer</param>
    ''' <returns></returns>
    ''' <remarks>Returns an integer with the number of rows afected.</remarks>
    Public Function deleteStudent(ByVal studentID As Integer) As Integer
        Dim connection As New SqlConnection
        Dim numReg As Integer
        Dim sql As String
        Dim command As New SqlCommand

        Try
            'Conect to the dataBase
            connection = con.getConnection

            'Create object Command
            sql = "DELETE FROM Student WHERE ID_Student = @StudentID "

            command = New SqlCommand(sql, connection)
            If studentID = 0 Then
                command.Parameters.AddWithValue("@StudentID", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@StudentID", studentID)
            End If

            'Execute delete of the student
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
