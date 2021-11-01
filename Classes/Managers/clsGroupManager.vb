Imports System.Data.SqlClient
Imports System.IO

Public Class clsGroupManager

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
    ''' Method that returns an Group from the database
    ''' </summary>
    ''' <pre>None</pre>
    ''' <param name="groupID">An Integer</param>
    ''' <returns>An object clsStudent</returns>
    ''' <pos>Returns the user.</pos>
    ''' <remarks></remarks>
    Public Function getGroup(groupID As Integer) As clsGroup
        Dim connection As New SqlConnection
        Dim dataReader As SqlDataReader
        Dim sql As String
        Dim command As New SqlCommand
        Dim group As New clsGroup

        Try
            'Conect to the dataBase
            connection = con.getConnection

            'Create object Command
            sql = "SELECT * FROM [Group] AS G " & _
                    " WHERE G.ID_Group = @GroupID "

            command = New SqlCommand(sql, connection)
            If groupID = 0 Then
                command.Parameters.AddWithValue("@GroupID", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@GroupID", groupID)
            End If

            'Get DataReader
            dataReader = command.ExecuteReader()

            'Get Group
            If dataReader.HasRows Then
                dataReader.Read()
                With group
                    If Not IsDBNull(dataReader("ID_Group")) Then
                        .GroupID = dataReader("ID_Group")
                    Else
                        .GroupID = Nothing
                    End If

                    If Not IsDBNull(dataReader("englishLevel")) Then
                        .EnglishLevel = dataReader("englishLevel")
                    Else
                        .EnglishLevel = Nothing
                    End If

                    If Not IsDBNull(dataReader("description")) Then
                        .Description = dataReader("description")
                    Else
                        .Description = Nothing
                    End If

                    If Not IsDBNull(dataReader("dateStarting")) Then
                        .DateStarting = dataReader("dateStarting")
                    Else
                        .DateStarting = Nothing
                    End If

                    If Not IsDBNull(dataReader("dateFinish")) Then
                        .DateFinish = dataReader("dateFinish")
                    Else
                        .DateFinish = Nothing
                    End If

                    If Not IsDBNull(dataReader("feeInscription")) Then
                        .FeeInscription = dataReader("feeInscription")
                    Else
                        .FeeInscription = Nothing
                    End If

                    If Not IsDBNull(dataReader("feeMonthly")) Then
                        .FeeMonthly = dataReader("feeMonthly")
                    Else
                        .FeeMonthly = Nothing
                    End If

                    If Not IsDBNull(dataReader("ID_Teacher")) Then
                        .TeacherID = dataReader("ID_Teacher")
                    Else
                        .TeacherID = Nothing
                    End If

                    'Get Students
                    .Students = Application.oStudentManager.getGroupStudents(.GroupID)
                End With
            Else
                group = Nothing
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

        Return group
    End Function

    ''' <summary>
    ''' Method that returns a list with all the Groups from the database
    ''' </summary>
    ''' <returns>A list of Student</returns>
    ''' <remarks></remarks>
    Public Function getAllGroups() As List(Of clsGroup)
        Dim listGroups As New List(Of clsGroup)
        Dim connection As New SqlConnection
        Dim dataReader As SqlDataReader
        Dim sql As String
        Dim command As New SqlCommand

        Try
            'Conect to the dataBase
            connection = con.getConnection

            'Create object Command
            sql = "SELECT * FROM [Group] AS G ORDER BY dateStarting DESC "

            command = New SqlCommand(sql, connection)

            'Get DataReader
            dataReader = command.ExecuteReader()

            'Get Groups
            While dataReader.Read()
                Dim group As New clsGroup
                With group
                    If Not IsDBNull(dataReader("ID_Group")) Then
                        .GroupID = dataReader("ID_Group")
                    Else
                        .GroupID = Nothing
                    End If

                    If Not IsDBNull(dataReader("englishLevel")) Then
                        .EnglishLevel = dataReader("englishLevel")
                    Else
                        .EnglishLevel = Nothing
                    End If

                    If Not IsDBNull(dataReader("description")) Then
                        .Description = dataReader("description")
                    Else
                        .Description = Nothing
                    End If

                    If Not IsDBNull(dataReader("dateStarting")) Then
                        .DateStarting = dataReader("dateStarting")
                    Else
                        .DateStarting = Nothing
                    End If

                    If Not IsDBNull(dataReader("dateFinish")) Then
                        .DateFinish = dataReader("dateFinish")
                    Else
                        .DateFinish = Nothing
                    End If

                    If Not IsDBNull(dataReader("feeInscription")) Then
                        .FeeInscription = dataReader("feeInscription")
                    Else
                        .FeeInscription = Nothing
                    End If

                    If Not IsDBNull(dataReader("feeMonthly")) Then
                        .FeeMonthly = dataReader("feeMonthly")
                    Else
                        .FeeMonthly = Nothing
                    End If

                    If Not IsDBNull(dataReader("ID_Teacher")) Then
                        .TeacherID = dataReader("ID_Teacher")
                    Else
                        .TeacherID = Nothing
                    End If

                    'Add Students
                    .Students = Application.oStudentManager.getGroupStudents(.GroupID)
                End With

                listGroups.Add(group)
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

        Return listGroups
    End Function

    ''' <summary>
    ''' Method that returns a list with all the Groups that matches with 
    ''' the search from the database
    ''' </summary>
    ''' <param name="englishLevel">A String</param>
    ''' <param name="teacherID">An Integer</param>
    ''' <returns>A list of Student</returns>
    ''' <remarks></remarks>
    Public Function getAllGroupsSearch(ByVal englishLevel As String, ByVal teacherID As Integer) As List(Of clsGroup)
        Dim listGroups As New List(Of clsGroup)
        Dim connection As New SqlConnection
        Dim dataReader As SqlDataReader
        Dim sql As String
        Dim command As New SqlCommand

        Try
            'Conect to the dataBase
            connection = con.getConnection

            If Not englishLevel Is Nothing And Not teacherID = 0 Then
                sql = "SELECT * FROM [Group] " & _
                    " WHERE englishLevel = @EnglishLevel And ID_Teacher = @TeacherID AND dateFinish IS NULL " & _
                    " ORDER BY dateStarting DESC "
            ElseIf englishLevel Is Nothing Then
                sql = "SELECT * FROM [Group] " & _
                   " WHERE ID_Teacher = @TeacherID AND dateFinish IS NULL " & _
                   " ORDER BY dateStarting DESC  "
            Else
                sql = "SELECT * FROM [Group] " & _
                   " WHERE englishLevel = @EnglishLevel AND dateFinish IS NULL " & _
                   " ORDER BY dateStarting DESC "
            End If

            'Create object Command
            command = New SqlCommand(sql, connection)
            If englishLevel Is "" Or englishLevel Is Nothing Then
                command.Parameters.AddWithValue("@EnglishLevel", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@EnglishLevel", englishLevel)
            End If

            If teacherID = 0 Then
                command.Parameters.AddWithValue("@TeacherID", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@TeacherID", teacherID)
            End If

            'Get DataReader
            dataReader = command.ExecuteReader()

            'Get Groups
            While dataReader.Read()
                Dim group As New clsGroup
                With group
                    If Not IsDBNull(dataReader("ID_Group")) Then
                        .GroupID = dataReader("ID_Group")
                    Else
                        .GroupID = Nothing
                    End If

                    If Not IsDBNull(dataReader("englishLevel")) Then
                        .EnglishLevel = dataReader("englishLevel")
                    Else
                        .EnglishLevel = Nothing
                    End If

                    If Not IsDBNull(dataReader("description")) Then
                        .Description = dataReader("description")
                    Else
                        .Description = Nothing
                    End If

                    If Not IsDBNull(dataReader("dateStarting")) Then
                        .DateStarting = dataReader("dateStarting")
                    Else
                        .DateStarting = Nothing
                    End If

                    If Not IsDBNull(dataReader("dateFinish")) Then
                        .DateFinish = dataReader("dateFinish")
                    Else
                        .DateFinish = Nothing
                    End If

                    If Not IsDBNull(dataReader("feeInscription")) Then
                        .FeeInscription = dataReader("feeInscription")
                    Else
                        .FeeInscription = Nothing
                    End If

                    If Not IsDBNull(dataReader("feeMonthly")) Then
                        .FeeMonthly = dataReader("feeMonthly")
                    Else
                        .FeeMonthly = Nothing
                    End If

                    If Not IsDBNull(dataReader("ID_Teacher")) Then
                        .TeacherID = dataReader("ID_Teacher")
                    Else
                        .TeacherID = Nothing
                    End If

                    'Add Students
                    .Students = Application.oStudentManager.getAllStudents(group.GroupID)
                End With

                listGroups.Add(group)
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

        Return listGroups
    End Function

    ''' <summary>
    ''' Method that returns a list with all the Groups that have not finished yet from the database
    ''' </summary>
    ''' <returns>A list of Student</returns>
    ''' <remarks></remarks>
    Public Function getActiveGroups() As List(Of clsGroup)
        Dim listGroups As New List(Of clsGroup)
        Dim connection As New SqlConnection
        Dim dataReader As SqlDataReader
        Dim sql As String
        Dim command As New SqlCommand

        Try
            'Conect to the dataBase
            connection = con.getConnection

            'Create object Command
            sql = "SELECT * FROM [Group] AS G " & _
                    " WHERE dateFinish IS NULL " & _
                    " ORDER BY dateStarting DESC "

            command = New SqlCommand(sql, connection)

            'Get DataReader
            dataReader = command.ExecuteReader()

            'Get Groups
            While dataReader.Read()
                Dim group As New clsGroup
                With group
                    If Not IsDBNull(dataReader("ID_Group")) Then
                        .GroupID = dataReader("ID_Group")
                    Else
                        .GroupID = Nothing
                    End If

                    If Not IsDBNull(dataReader("englishLevel")) Then
                        .EnglishLevel = dataReader("englishLevel")
                    Else
                        .EnglishLevel = Nothing
                    End If

                    If Not IsDBNull(dataReader("description")) Then
                        .Description = dataReader("description")
                    Else
                        .Description = Nothing
                    End If

                    If Not IsDBNull(dataReader("dateStarting")) Then
                        .DateStarting = dataReader("dateStarting")
                    Else
                        .DateStarting = Nothing
                    End If

                    If Not IsDBNull(dataReader("dateFinish")) Then
                        .DateFinish = dataReader("dateFinish")
                    Else
                        .DateFinish = Nothing
                    End If

                    If Not IsDBNull(dataReader("feeInscription")) Then
                        .FeeInscription = dataReader("feeInscription")
                    Else
                        .FeeInscription = Nothing
                    End If

                    If Not IsDBNull(dataReader("feeMonthly")) Then
                        .FeeMonthly = dataReader("feeMonthly")
                    Else
                        .FeeMonthly = Nothing
                    End If

                    If Not IsDBNull(dataReader("ID_Teacher")) Then
                        .TeacherID = dataReader("ID_Teacher")
                    Else
                        .TeacherID = Nothing
                    End If

                    'Add Students
                    .Students = Application.oStudentManager.getGroupStudents(.GroupID)
                End With

                listGroups.Add(group)
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

        Return listGroups
    End Function

    ''' <summary>
    ''' Method that returns a list with all the Groups that have no teacher asignated from the database
    ''' </summary>
    ''' <returns>A list of Student</returns>
    ''' <remarks></remarks>
    Public Function getGroupsWithNoTeacher() As List(Of clsGroup)
        Dim listGroups As New List(Of clsGroup)
        Dim connection As New SqlConnection
        Dim dataReader As SqlDataReader
        Dim sql As String
        Dim command As New SqlCommand

        Try
            'Conect to the dataBase
            connection = con.getConnection

            'Create object Command
            sql = "SELECT * FROM [Group] AS G " & _
                    " WHERE ID_Teacher IS NULL AND dateFinish IS NULL " & _
                    " ORDER BY dateStarting DESC "

            command = New SqlCommand(sql, connection)

            'Get DataReader
            dataReader = command.ExecuteReader()

            'Get Groups
            While dataReader.Read()
                Dim group As New clsGroup
                With group
                    If Not IsDBNull(dataReader("ID_Group")) Then
                        .GroupID = dataReader("ID_Group")
                    Else
                        .GroupID = Nothing
                    End If

                    If Not IsDBNull(dataReader("englishLevel")) Then
                        .EnglishLevel = dataReader("englishLevel")
                    Else
                        .EnglishLevel = Nothing
                    End If

                    If Not IsDBNull(dataReader("description")) Then
                        .Description = dataReader("description")
                    Else
                        .Description = Nothing
                    End If

                    If Not IsDBNull(dataReader("dateStarting")) Then
                        .DateStarting = dataReader("dateStarting")
                    Else
                        .DateStarting = Nothing
                    End If

                    If Not IsDBNull(dataReader("dateFinish")) Then
                        .DateFinish = dataReader("dateFinish")
                    Else
                        .DateFinish = Nothing
                    End If

                    If Not IsDBNull(dataReader("feeInscription")) Then
                        .FeeInscription = dataReader("feeInscription")
                    Else
                        .FeeInscription = Nothing
                    End If

                    If Not IsDBNull(dataReader("feeMonthly")) Then
                        .FeeMonthly = dataReader("feeMonthly")
                    Else
                        .FeeMonthly = Nothing
                    End If

                    If Not IsDBNull(dataReader("ID_Teacher")) Then
                        .TeacherID = dataReader("ID_Teacher")
                    Else
                        .TeacherID = Nothing
                    End If

                    'Add Students
                    .Students = Application.oStudentManager.getGroupStudents(.GroupID)
                End With

                listGroups.Add(group)
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

        Return listGroups
    End Function

    ''' <summary>
    ''' Method that returns a list with all the Groups that matches with 
    ''' the search and has no Teacher asignated from the database
    ''' </summary>
    ''' <param name="englishLevel">A String</param>
    ''' <returns>A list of Student</returns>
    ''' <remarks></remarks>
    Public Function getGroupsWithNoTeacher(ByVal englishLevel As String) As List(Of clsGroup)
        Dim listGroups As New List(Of clsGroup)
        Dim connection As New SqlConnection
        Dim dataReader As SqlDataReader
        Dim sql As String
        Dim command As New SqlCommand

        Try
            'Conect to the dataBase
            connection = con.getConnection

            'Create object Command
            sql = "SELECT * FROM [Group] " & _
                   " WHERE englishLevel = @EnglishLevel AND ID_Teacher IS NULL " & _
                   " ORDER BY dateStarting DESC "
            command = New SqlCommand(sql, connection)

            If englishLevel Is "" Or englishLevel Is Nothing Then
                command.Parameters.AddWithValue("@EnglishLevel", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@EnglishLevel", englishLevel)
            End If

            'Get DataReader
            dataReader = command.ExecuteReader()

            'Get Groups
            While dataReader.Read()
                Dim group As New clsGroup
                With group
                    If Not IsDBNull(dataReader("ID_Group")) Then
                        .GroupID = dataReader("ID_Group")
                    Else
                        .GroupID = Nothing
                    End If

                    If Not IsDBNull(dataReader("englishLevel")) Then
                        .EnglishLevel = dataReader("englishLevel")
                    Else
                        .EnglishLevel = Nothing
                    End If

                    If Not IsDBNull(dataReader("description")) Then
                        .Description = dataReader("description")
                    Else
                        .Description = Nothing
                    End If

                    If Not IsDBNull(dataReader("dateStarting")) Then
                        .DateStarting = dataReader("dateStarting")
                    Else
                        .DateStarting = Nothing
                    End If

                    If Not IsDBNull(dataReader("dateFinish")) Then
                        .DateFinish = dataReader("dateFinish")
                    Else
                        .DateFinish = Nothing
                    End If

                    If Not IsDBNull(dataReader("feeInscription")) Then
                        .FeeInscription = dataReader("feeInscription")
                    Else
                        .FeeInscription = Nothing
                    End If

                    If Not IsDBNull(dataReader("feeMonthly")) Then
                        .FeeMonthly = dataReader("feeMonthly")
                    Else
                        .FeeMonthly = Nothing
                    End If

                    If Not IsDBNull(dataReader("ID_Teacher")) Then
                        .TeacherID = dataReader("ID_Teacher")
                    Else
                        .TeacherID = Nothing
                    End If

                    'Add Students
                    .Students = Application.oStudentManager.getAllStudents(group.GroupID)
                End With

                listGroups.Add(group)
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

        Return listGroups
    End Function

    ''' <summary>
    ''' Method that returns a list with all the Groups of a Student
    ''' </summary>
    ''' <param name="studentID">A String</param>
    ''' <returns>A list of Student</returns>
    ''' <remarks></remarks>
    Public Function getStudentGroups(ByVal studentID As Integer) As List(Of clsGroup)
        Dim listGroups As New List(Of clsGroup)
        Dim connection As New SqlConnection
        Dim dataReader As SqlDataReader
        Dim sql As String
        Dim command As New SqlCommand

        Try
            'Conect to the dataBase
            connection = con.getConnection

            'Create object Command
            sql = "SELECT * FROM [Group] AS G " & _
                   " JOIN Inscription AS I ON G.ID_Group = I.ID_Group " & _
                   " WHERE I.ID_Student = @StudentID " & _
                   " ORDER BY G.dateStarting DESC "
            command = New SqlCommand(sql, connection)

            If studentID = 0 Then
                command.Parameters.AddWithValue("@StudentID", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@StudentID", studentID)
            End If

            'Get DataReader
            dataReader = command.ExecuteReader()

            'Get Groups
            While dataReader.Read()
                Dim group As New clsGroup
                With group
                    If Not IsDBNull(dataReader("ID_Group")) Then
                        .GroupID = dataReader("ID_Group")
                    Else
                        .GroupID = Nothing
                    End If

                    If Not IsDBNull(dataReader("englishLevel")) Then
                        .EnglishLevel = dataReader("englishLevel")
                    Else
                        .EnglishLevel = Nothing
                    End If

                    If Not IsDBNull(dataReader("description")) Then
                        .Description = dataReader("description")
                    Else
                        .Description = Nothing
                    End If

                    If Not IsDBNull(dataReader("dateStarting")) Then
                        .DateStarting = dataReader("dateStarting")
                    Else
                        .DateStarting = Nothing
                    End If

                    If Not IsDBNull(dataReader("dateFinish")) Then
                        .DateFinish = dataReader("dateFinish")
                    Else
                        .DateFinish = Nothing
                    End If

                    If Not IsDBNull(dataReader("feeInscription")) Then
                        .FeeInscription = dataReader("feeInscription")
                    Else
                        .FeeInscription = Nothing
                    End If

                    If Not IsDBNull(dataReader("feeMonthly")) Then
                        .FeeMonthly = dataReader("feeMonthly")
                    Else
                        .FeeMonthly = Nothing
                    End If

                    If Not IsDBNull(dataReader("ID_Teacher")) Then
                        .TeacherID = dataReader("ID_Teacher")
                    Else
                        .TeacherID = Nothing
                    End If

                    'Add Students
                    .Students = Application.oStudentManager.getAllStudents(group.GroupID)
                End With

                listGroups.Add(group)
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

        Return listGroups
    End Function

    ''' <summary>
    ''' Method that returns a list with all the Groups that are not related to a student from the database
    ''' </summary>
    ''' <returns>A list of Groups</returns>
    ''' <remarks></remarks>
    Public Function getGroupsForStudent(ByVal studentID As Integer) As List(Of clsGroup)
        Dim listGroups As New List(Of clsGroup)
        Dim connection As New SqlConnection
        Dim dataReader As SqlDataReader
        Dim sql As String
        Dim command As New SqlCommand

        Try
            'Conect to the dataBase
            connection = con.getConnection

            'Create object Command
            sql = " SELECT * FROM [Group] WHERE dateFinish IS NULL " & _
                    "EXCEPT SELECT G.* FROM [Group] AS G " & _
                    " JOIN Inscription AS I ON G.ID_Group = I.ID_Group " & _
                    " WHERE I.ID_Student = @StudentID AND G.dateFinish IS NULL " & _
                    " ORDER BY dateStarting DESC "

            command = New SqlCommand(sql, connection)
            If studentID = 0 Then
                command.Parameters.AddWithValue("@StudentID", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@StudentID", studentID)
            End If

            'Get DataReader
            dataReader = command.ExecuteReader()

            'Get Groups
            While dataReader.Read()
                Dim group As New clsGroup
                With group
                    If Not IsDBNull(dataReader("ID_Group")) Then
                        .GroupID = dataReader("ID_Group")
                    Else
                        .GroupID = Nothing
                    End If

                    If Not IsDBNull(dataReader("englishLevel")) Then
                        .EnglishLevel = dataReader("englishLevel")
                    Else
                        .EnglishLevel = Nothing
                    End If

                    If Not IsDBNull(dataReader("description")) Then
                        .Description = dataReader("description")
                    Else
                        .Description = Nothing
                    End If

                    If Not IsDBNull(dataReader("dateStarting")) Then
                        .DateStarting = dataReader("dateStarting")
                    Else
                        .DateStarting = Nothing
                    End If

                    If Not IsDBNull(dataReader("dateFinish")) Then
                        .DateFinish = dataReader("dateFinish")
                    Else
                        .DateFinish = Nothing
                    End If

                    If Not IsDBNull(dataReader("feeInscription")) Then
                        .FeeInscription = dataReader("feeInscription")
                    Else
                        .FeeInscription = Nothing
                    End If

                    If Not IsDBNull(dataReader("feeMonthly")) Then
                        .FeeMonthly = dataReader("feeMonthly")
                    Else
                        .FeeMonthly = Nothing
                    End If

                    If Not IsDBNull(dataReader("ID_Teacher")) Then
                        .TeacherID = dataReader("ID_Teacher")
                    Else
                        .TeacherID = Nothing
                    End If

                    'Add Students
                    .Students = Application.oStudentManager.getGroupStudents(.GroupID)
                End With

                listGroups.Add(group)
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

        Return listGroups
    End Function

    ''' <summary>
    ''' Method that updates a Group from the database
    ''' </summary>
    ''' <param name="group">An object clsGroup</param>
    ''' <returns>Returns an integer with the number of rows afected.</returns>
    ''' <remarks></remarks>
    Public Function updateGroup(ByVal group As clsGroup) As Integer
        Dim connection As New SqlConnection
        Dim numReg As Integer
        Dim sql As String
        Dim command As New SqlCommand

        Try
            'Conect to the dataBase
            connection = con.getConnection

            'Create object Command
            sql = "UPDATE [Group] SET englishLevel = @EnglishLevel " _
                                    & ", description = @Description " _
                                    & ", feeInscription = @FeeInscription " _
                                    & ", feeMonthly = @FeeMonthly " _
                                    & ", ID_Teacher = @TeacherID" _
                                    & " WHERE ID_Group = @GroupID"

            command = New SqlCommand(sql, connection)
            If group.EnglishLevel Is "" Or group.EnglishLevel Is Nothing Then
                command.Parameters.AddWithValue("@EnglishLevel", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@EnglishLevel", group.EnglishLevel)
            End If

            If group.Description Is "" Or group.Description Is Nothing Then
                command.Parameters.AddWithValue("@Description", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@Description", group.Description)
            End If

            If group.FeeInscription = 0 Then
                command.Parameters.AddWithValue("@FeeInscription", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@FeeInscription", group.FeeInscription)
            End If

            If group.FeeMonthly = 0 Then
                command.Parameters.AddWithValue("@FeeMonthly", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@FeeMonthly", group.FeeMonthly)
            End If

            If group.TeacherID = 0 Then
                command.Parameters.AddWithValue("@TeacherID", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@TeacherID", group.TeacherID)
            End If

            If group.GroupID = 0 Then
                command.Parameters.AddWithValue("@GroupID", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@GroupID", group.GroupID)
            End If

            'Update Students
            Dim students As New List(Of clsStudent)
            students = group.Students
            For Each student As clsStudent In students
                'Check if the student is already inscripted
                If Not Application.oInscriptionManager.checkStudentInscription(student.StudentID, group.GroupID) Then
                    Application.oInscriptionManager.studentInscription(student.StudentID, group.GroupID)
                End If
            Next

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
    ''' Method that finish a Group, setting a value to dateFinish
    ''' </summary>
    ''' <param name="groupID">An Integer</param>
    ''' <returns>Returns an integer with the number of rows afected.</returns>
    ''' <remarks></remarks>
    Public Function finishGroup(ByVal groupID As Integer) As Integer
        Dim connection As New SqlConnection
        Dim numReg As Integer
        Dim sql As String
        Dim command As New SqlCommand

        Try
            'Conect to the dataBase
            connection = con.getConnection

            'Create object Command
            sql = "UPDATE [Group] SET dateFinish = @DateFinish " & _
                    " WHERE ID_Group = @GroupID"

            command = New SqlCommand(sql, connection)
            If groupID = 0 Then
                command.Parameters.AddWithValue("@GroupID", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@GroupID", groupID)
            End If

            Dim dateFinish As Date = Date.Today
            command.Parameters.AddWithValue("@DateFinish", dateFinish)

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
    ''' Method that inserts a new Group into the database
    ''' </summary>
    ''' <param name="group">An Object clsGroup</param>
    ''' <returns>Returns an integer with the number of rows afected.</returns>
    ''' <remarks></remarks>
    Public Function insertGroup(ByVal group As clsGroup) As Integer
        Dim connection As New SqlConnection
        Dim sql As String
        Dim command As New SqlCommand
        Dim numReg As Integer

        Try
            'Connect to the database
            connection = con.getConnection

            'Insert User
            sql = "INSERT INTO [Group] (englishLevel,[description],dateStarting,feeInscription,feeMonthly,ID_Teacher) " _
                & "VALUES(@EnglishLevel,@Description,@DateStarting,@FeeInscription,@FeeMonthly,@TeacherID)"

            command = New SqlCommand(sql, connection)
            Dim dateInscription As Date = Date.Today
            command.Parameters.AddWithValue("@EnglishLevel", group.EnglishLevel)

            If group.Description Is "" Or group.Description Is Nothing Then
                command.Parameters.AddWithValue("@Description", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@Description", group.Description)
            End If

            Dim dateStarting As Date = Date.Today
            command.Parameters.AddWithValue("@DateStarting", dateStarting)

            If group.FeeInscription = 0 Then
                command.Parameters.AddWithValue("@FeeInscription", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@FeeInscription", group.FeeInscription)
            End If

            If group.FeeMonthly = 0 Then
                command.Parameters.AddWithValue("@FeeMonthly", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@FeeMonthly", group.FeeMonthly)
            End If

            If group.TeacherID = 0 Then
                command.Parameters.AddWithValue("@TeacherID", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@TeacherID", group.TeacherID)
            End If

            'Execute 
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
