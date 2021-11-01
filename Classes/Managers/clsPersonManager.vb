Imports System.Data.SqlClient
Imports System.IO

Public Class clsPersonManager
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
    ''' Method that returns a Person from the database
    ''' </summary>
    ''' <pre>None</pre>
    ''' <param name="personID">An Integer</param>
    ''' <returns>An object clsPerson</returns>
    ''' <pos>Returns the user.</pos>
    ''' <remarks></remarks>
    Public Function getPerson(PersonID As Integer) As clsPerson
        Dim connection As New SqlConnection
        Dim dataReader As SqlDataReader
        Dim sql As String
        Dim command As New SqlCommand
        Dim person As New clsPerson

        Try
            'Conect to the dataBase
            connection = con.getConnection

            'Create object Command
            sql = "SELECT * FROM Person " & _
                " WHERE ID_Person=@PersonID "
            command = New SqlCommand(sql, connection)
            If PersonID = 0 Then
                command.Parameters.AddWithValue("@PersonID", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@PersonID", PersonID)
            End If

            'Get DataReader
            dataReader = command.ExecuteReader()

            'Get Student
            If dataReader.HasRows Then
                dataReader.Read()
                With person
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
            Else
                person = Nothing
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

        Return person
    End Function

    ''' <summary>
    ''' Method that updates the Student pesonal information from the database
    ''' </summary>
    ''' <param name="student">An object clsStudent</param>
    ''' <returns>Returns an integer with the number of rows afected.</returns>
    ''' <remarks></remarks>
    Public Function updatePerson(ByVal student As clsStudent) As Integer
        Dim connection As New SqlConnection
        Dim numReg As Integer
        Dim sql As String
        Dim command As New SqlCommand
        Dim memStream As New MemoryStream()
        Dim encoder As New JpegBitmapEncoder()

        Try
            'Conect to the dataBase
            connection = con.getConnection

            'Create object Command
            sql = "UPDATE Person SET name = @Name " _
                                    & ", surname = @Surname " _
                                    & ", DNI = @DNI " _
                                    & ", birthDate = @BirthDate " _
                                    & ", sex = @Sex " _
                                    & ", photo = @Photo " _
                                    & ", [address] = @Address " _
                                    & ", city = @City " _
                                    & ", postalCode = @PostalCode " _
                                    & ", telephone = @Telephone " _
                                    & ", email = @Email " _
                                    & " WHERE ID_Person = @PersonID"

            command = New SqlCommand(sql, connection)

            If student.Name Is "" Or student.Name Is Nothing Then
                command.Parameters.AddWithValue("@Name", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@Name", student.Name)
            End If

            If student.Surname Is "" Or student.Surname Is Nothing Then
                command.Parameters.AddWithValue("@Surname", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@Surname", student.Surname)
            End If

            If student.DNI Is "" Or student.DNI Is Nothing Then
                command.Parameters.AddWithValue("@DNI", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@DNI", student.DNI)
            End If

            If student.BirthDate = Nothing Then
                command.Parameters.AddWithValue("@BirthDate", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@BirthDate", student.BirthDate)
            End If

            If student.Sex = Nothing Then
                command.Parameters.AddWithValue("@Sex", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@Sex", student.Sex)
            End If

            If student.Photo Is Nothing Then
                command.Parameters.AddWithValue("@Photo", DBNull.Value)
            Else
                command.Parameters.Add("@Photo", System.Data.SqlDbType.Image)
                encoder.Frames.Add(BitmapFrame.Create(student.Photo.Source))
                encoder.Save(memStream)
                command.Parameters("@Photo").Value = memStream.GetBuffer()
            End If

            If student.Address Is "" Or student.Address Is Nothing Then
                command.Parameters.AddWithValue("@Address", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@Address", student.Address)
            End If

            If student.City Is "" Or student.City Is Nothing Then
                command.Parameters.AddWithValue("@City", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@City", student.City)
            End If

            If student.PostalCode Is "" Or student.PostalCode Is Nothing Then
                command.Parameters.AddWithValue("@PostalCode", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@PostalCode", student.PostalCode)
            End If

            If student.Telephone Is "" Or student.Telephone Is Nothing Then
                command.Parameters.AddWithValue("@Telephone", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@Telephone", student.Telephone)
            End If

            If student.Email Is "" Or student.Email Is Nothing Then
                command.Parameters.AddWithValue("@Email", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@Email", student.Email)
            End If

            If student.PersonID = 0 Then
                command.Parameters.AddWithValue("@PersonID", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@PersonID", student.PersonID)
            End If

            'Get dataReader
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
    ''' Method that updates the Teacher pesonal information from the database
    ''' </summary>
    ''' <param name="teacher">An object clsTeacher</param>
    ''' <returns>Returns an integer with the number of rows afected.</returns>
    ''' <remarks></remarks>
    Public Function updatePerson(ByVal teacher As clsTeacher) As Integer
        Dim connection As New SqlConnection
        Dim numReg As Integer
        Dim sql As String
        Dim command As New SqlCommand
        Dim memStream As New MemoryStream()
        Dim encoder As New JpegBitmapEncoder()

        Try
            'Conect to the dataBase
            connection = con.getConnection

            'Create object Command
            sql = "UPDATE Person SET name = @Name " _
                                    & ", surname = @Surname " _
                                    & ", DNI = @DNI " _
                                    & ", birthDate = @BirthDate " _
                                    & ", sex = @Sex " _
                                    & ", photo = @Photo " _
                                    & ", [address] = @Address " _
                                    & ", city = @City " _
                                    & ", postalCode = @PostalCode " _
                                    & ", telephone = @Telephone " _
                                    & ", email = @Email " _
                                    & " WHERE ID_Person = @PersonID"

            command = New SqlCommand(sql, connection)
            If teacher.Name Is "" Or teacher.Name Is Nothing Then
                command.Parameters.AddWithValue("@Name", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@Name", teacher.Name)
            End If

            If teacher.Surname Is "" Or teacher.Surname Is Nothing Then
                command.Parameters.AddWithValue("@Surname", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@Surname", teacher.Surname)
            End If

            If teacher.DNI Is "" Or teacher.DNI Is Nothing Then
                command.Parameters.AddWithValue("@DNI", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@DNI", teacher.DNI)
            End If

            If teacher.BirthDate = Nothing Then
                command.Parameters.AddWithValue("@BirthDate", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@BirthDate", teacher.BirthDate)
            End If

            If teacher.Sex = Nothing Then
                command.Parameters.AddWithValue("@Sex", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@Sex", teacher.Sex)
            End If

            If teacher.Photo Is Nothing Then
                command.Parameters.AddWithValue("@Photo", DBNull.Value)
            Else
                command.Parameters.Add("@Photo", System.Data.SqlDbType.Image)
                encoder.Frames.Add(BitmapFrame.Create(teacher.Photo.Source))
                encoder.Save(memStream)
                command.Parameters("@Photo").Value = memStream.GetBuffer()
            End If

            If teacher.Address Is "" Or teacher.Address Is Nothing Then
                command.Parameters.AddWithValue("@Address", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@Address", teacher.Address)
            End If

            If teacher.City Is "" Or teacher.City Is Nothing Then
                command.Parameters.AddWithValue("@City", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@City", teacher.City)
            End If

            If teacher.PostalCode Is "" Or teacher.PostalCode Is Nothing Then
                command.Parameters.AddWithValue("@PostalCode", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@PostalCode", teacher.PostalCode)
            End If

            If teacher.Telephone Is "" Or teacher.Telephone Is Nothing Then
                command.Parameters.AddWithValue("@Telephone", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@Telephone", teacher.Telephone)
            End If

            If teacher.Email Is "" Or teacher.Email Is Nothing Then
                command.Parameters.AddWithValue("@Email", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@Email", teacher.Email)
            End If

            If teacher.PersonID = 0 Then
                command.Parameters.AddWithValue("@PersonID", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@PersonID", teacher.PersonID)
            End If

            'Get dataReader
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
    ''' Method that inserts a new Person of a Student into the database
    ''' </summary>
    ''' <param name="student">An object clsStudent</param>
    ''' <returns>Returns an integer with the number of rows afected.</returns>
    ''' <remarks></remarks>
    Public Function insertPerson(ByVal student As clsStudent) As Integer
        Dim connection As New SqlConnection
        Dim sql As String
        Dim command As New SqlCommand
        Dim newPersonID As New Integer
        Dim numReg As Integer
        Dim memStream As New MemoryStream()
        Dim encoder As New JpegBitmapEncoder()

        Try
            'Conect to the dataBase
            connection = con.getConnection

            'Insert Person
            sql = "INSERT INTO Person (name,surname,DNI,sex,photo,birthDate,[address],city,postalCode,telephone,email) " _
                & "VALUES(@Name,@Surname,@DNI,@Sex,@Photo,@BirthDate,@Address,@City,@PostalCode,@Telephone,@Email)"

            command = New SqlCommand(sql, connection)
            If student.Name Is "" Or student.Name Is Nothing Then
                command.Parameters.AddWithValue("@Name", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@Name", student.Name)
            End If

            If student.Surname Is "" Or student.Surname Is Nothing Then
                command.Parameters.AddWithValue("@Surname", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@Surname", student.Surname)
            End If

            If student.DNI Is "" Or student.DNI Is Nothing Then
                command.Parameters.AddWithValue("@DNI", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@DNI", student.DNI)
            End If

            If student.BirthDate = Nothing Then
                command.Parameters.AddWithValue("@BirthDate", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@BirthDate", student.BirthDate)
            End If

            If student.Sex = Nothing Then
                command.Parameters.AddWithValue("@Sex", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@Sex", student.Sex)
            End If

            If student.Photo Is Nothing Then
                command.Parameters.AddWithValue("@Photo", DBNull.Value)
            Else
                command.Parameters.Add("@Photo", System.Data.SqlDbType.Image)
                encoder.Frames.Add(BitmapFrame.Create(student.Photo.Source))
                encoder.Save(memStream)
                command.Parameters("@Photo").Value = memStream.GetBuffer()
            End If

            If student.Address Is "" Or student.Address Is Nothing Then
                command.Parameters.AddWithValue("@Address", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@Address", student.Address)
            End If

            If student.City Is "" Or student.City Is Nothing Then
                command.Parameters.AddWithValue("@City", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@City", student.City)
            End If

            If student.PostalCode Is "" Or student.PostalCode Is Nothing Then
                command.Parameters.AddWithValue("@PostalCode", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@PostalCode", student.PostalCode)
            End If

            If student.Telephone Is "" Or student.Telephone Is Nothing Then
                command.Parameters.AddWithValue("@Telephone", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@Telephone", student.Telephone)
            End If

            If student.Email Is "" Or student.Email Is Nothing Then
                command.Parameters.AddWithValue("@Email", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@Email", student.Email)
            End If

            'Execute insert User
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
    ''' Method that inserts a new Person of a Teacher into the database
    ''' </summary>
    ''' <param name="teacher">An object clsTeacher</param>
    ''' <returns>Returns an integer with the number of rows afected.</returns>
    ''' <remarks></remarks>
    Public Function insertPerson(ByVal teacher As clsTeacher) As Integer
        Dim connection As New SqlConnection
        Dim sql As String
        Dim command As New SqlCommand
        Dim newPersonID As New Integer
        Dim numReg As Integer
        Dim memStream As New MemoryStream()
        Dim encoder As New JpegBitmapEncoder()

        Try
            'Conect to the dataBase
            connection = con.getConnection

            'Insert Person
            sql = "INSERT INTO Person (name,surname,DNI,sex,photo,birthDate,[address],city,postalCode,telephone,email) " _
                & "VALUES(@Name,@Surname,@DNI,@Sex,@Photo,@BirthDate,@Address,@City,@PostalCode,@Telephone,@Email)"

            command = New SqlCommand(sql, connection)
            If teacher.Name Is "" Or teacher.Name Is Nothing Then
                command.Parameters.AddWithValue("@Name", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@Name", teacher.Name)
            End If

            If teacher.Surname Is "" Or teacher.Surname Is Nothing Then
                command.Parameters.AddWithValue("@Surname", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@Surname", teacher.Surname)
            End If

            If teacher.DNI Is "" Or teacher.DNI Is Nothing Then
                command.Parameters.AddWithValue("@DNI", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@DNI", teacher.DNI)
            End If

            If teacher.BirthDate = Nothing Then
                command.Parameters.AddWithValue("@BirthDate", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@BirthDate", teacher.BirthDate)
            End If

            If teacher.Sex = Nothing Then
                command.Parameters.AddWithValue("@Sex", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@Sex", teacher.Sex)
            End If

            If teacher.Photo Is Nothing Then
                command.Parameters.AddWithValue("@Photo", DBNull.Value)
            Else
                command.Parameters.Add("@Photo", System.Data.SqlDbType.Image)
                encoder.Frames.Add(BitmapFrame.Create(teacher.Photo.Source))
                encoder.Save(memStream)
                command.Parameters("@Photo").Value = memStream.GetBuffer()
            End If

            If teacher.Address Is "" Or teacher.Address Is Nothing Then
                command.Parameters.AddWithValue("@Address", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@Address", teacher.Address)
            End If

            If teacher.City Is "" Or teacher.City Is Nothing Then
                command.Parameters.AddWithValue("@City", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@City", teacher.City)
            End If

            If teacher.PostalCode Is "" Or teacher.PostalCode Is Nothing Then
                command.Parameters.AddWithValue("@PostalCode", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@PostalCode", teacher.PostalCode)
            End If

            If teacher.Telephone Is "" Or teacher.Telephone Is Nothing Then
                command.Parameters.AddWithValue("@Telephone", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@Telephone", teacher.Telephone)
            End If

            If teacher.Email Is "" Or teacher.Email Is Nothing Then
                command.Parameters.AddWithValue("@Email", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@Email", teacher.Email)
            End If

            'Execute insert User
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
    ''' Method that gets the last PersonID from the table 'Person'
    ''' </summary>
    ''' <returns>Returns the last Person ID inserted</returns>
    ''' <remarks></remarks>
    Public Function getLastPersonID() As Integer
        Dim connection As New SqlConnection
        Dim dataReader As SqlDataReader
        Dim sql As String
        Dim command As New SqlCommand
        Dim result As Integer

        Try
            'Conect to the dataBase
            connection = con.getConnection

            'Create object Command
            sql = "SELECT TOP(1) ID_Person FROM Person Order by ID_Person DESC "
            command = New SqlCommand(sql, connection)

            'Get DataReader
            dataReader = command.ExecuteReader()

            'Get Product
            If dataReader.HasRows Then
                dataReader.Read()
                result = dataReader("ID_Person")
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

        Return result
    End Function

    ''' <summary>
    ''' Method that check if a DNI exists.
    ''' </summary>
    ''' <param name="DNI">A String</param>
    ''' <returns>Return TRUE if the DNI exists of FALSE if is not.</returns>
    ''' <remarks></remarks>
    Public Function existDNI(ByVal DNI As String) As Boolean
        Dim connection As New SqlConnection
        Dim dataReader As SqlDataReader
        Dim sql As String
        Dim command As New SqlCommand
        Dim exist As Integer = False

        Try
            'Conect to the dataBase
            connection = con.getConnection

            'Create object Command
            sql = "SELECT * FROM Person WHERE DNI=@DNI "
            command = New SqlCommand(sql, connection)
            If DNI Is "" Then
                command.Parameters.AddWithValue("@DNI", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@DNI", DNI)
            End If

            'Get DataReader
            dataReader = command.ExecuteReader()

            'Get DNI
            If dataReader.HasRows Then
                dataReader.Read()
                exist = True
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

        Return exist
    End Function

    ''' <summary>
    ''' Method that returns a list with all the deleted People from the database
    ''' </summary>
    ''' <returns>A list of Student</returns>
    ''' <remarks></remarks>
    Public Function getAllDeletedPeople() As List(Of clsPerson)
        Dim listPeople As New List(Of clsPerson)
        Dim connection As New SqlConnection
        Dim dataReader As SqlDataReader
        Dim sql As String
        Dim command As New SqlCommand

        Try
            'Conect to the dataBase
            connection = con.getConnection

            'Create object Command
            sql = "SELECT * FROM Deleted_Person " & _
                    " ORDER BY name,surname "

            command = New SqlCommand(sql, connection)

            'Get DataReader
            dataReader = command.ExecuteReader()

            'Get Students
            While dataReader.Read()
                Dim person As New clsPerson
                With person
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

                listPeople.Add(person)
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

        Return listPeople
    End Function

    ''' <summary>
    ''' Method that returns a list with all the deleted People that matches with 
    ''' the search from the database
    ''' </summary>
    ''' <param name="search">A String</param>
    ''' <returns>A list of Student</returns>
    ''' <remarks></remarks>
    Public Function getAllDeletedPeople(ByVal search As String) As List(Of clsPerson)
        Dim listPeople As New List(Of clsPerson)
        Dim connection As New SqlConnection
        Dim dataReader As SqlDataReader
        Dim sql As String
        Dim command As New SqlCommand

        Try
            'Conect to the dataBase
            connection = con.getConnection

            'Create object Command
            sql = "SELECT * FROM Deleted_Person AS P " & _
                " WHERE P.name LIKE @Search OR P.surname LIKE @Search OR " & _
                        " P.name + ' ' + P.surname LIKE @Search " & _
                " ORDER BY name,surname "

            command = New SqlCommand(sql, connection)
            command.Parameters.Add("@Search", System.Data.SqlDbType.NVarChar)
            command.Parameters("@Search").Value = "%" & search & "%"

            'Get DataReader
            dataReader = command.ExecuteReader()

            'Get Students
            While dataReader.Read()
                Dim person As New clsPerson
                With person
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

                listPeople.Add(person)
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

        Return listPeople
    End Function
#End Region
End Class
