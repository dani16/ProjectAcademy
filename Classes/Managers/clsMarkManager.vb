Imports System.Data.SqlClient

Public Class clsMarkManager

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
    ''' Method that returns a Mark from the database
    ''' </summary>
    ''' <pre>None</pre>
    ''' <param name="studentID">An Integer</param>
    ''' <param name="groupID">An Integer</param>
    ''' <returns>An object clsMark</returns>
    ''' <pos>Returns the clsMark.</pos>
    ''' <remarks></remarks>
    Public Function getMark(ByVal studentID As Integer, groupID As Integer, ByVal dateMark As Date) As clsMark
        Dim connection As New SqlConnection
        Dim dataReader As SqlDataReader
        Dim sql As String
        Dim command As New SqlCommand
        Dim mark As New clsMark

        Try
            'Conect to the dataBase
            connection = con.getConnection

            'Create object Command
            sql = "SELECT * FROM Mark " & _
                    " WHERE ID_Student = @StudentID AND ID_Group = @GroupID AND dateMark = @DateMark "

            command = New SqlCommand(sql, connection)
            If studentID = 0 Then
                command.Parameters.AddWithValue("@StudentID", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@StudentID", studentID)
            End If
            If groupID = 0 Then
                command.Parameters.AddWithValue("@GroupID", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@GroupID", groupID)
            End If
            If dateMark = Nothing Then
                command.Parameters.AddWithValue("@DateMark", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@DateMark", dateMark)
            End If

            'Get DataReader
            dataReader = command.ExecuteReader()

            'Get Mark
            If dataReader.HasRows Then
                dataReader.Read()
                With mark
                    If Not IsDBNull(dataReader("ID_Student")) Then
                        .StudentID = dataReader("ID_Student")
                    Else
                        .StudentID = Nothing
                    End If

                    If Not IsDBNull(dataReader("ID_Group")) Then
                        .GroupID = dataReader("ID_Group")
                    Else
                        .GroupID = Nothing
                    End If

                    If Not IsDBNull(dataReader("listening")) Then
                        .Listening = dataReader("listening")
                    Else
                        .Listening = Nothing
                    End If

                    If Not IsDBNull(dataReader("speaking")) Then
                        .Speaking = dataReader("speaking")
                    Else
                        .Speaking = Nothing
                    End If

                    If Not IsDBNull(dataReader("reading")) Then
                        .Reading = dataReader("reading")
                    Else
                        .Reading = Nothing
                    End If

                    If Not IsDBNull(dataReader("writing")) Then
                        .Writing = dataReader("writing")
                    Else
                        .Writing = Nothing
                    End If

                    If Not IsDBNull(dataReader("exam")) Then
                        .Exam = dataReader("exam")
                    Else
                        .Exam = Nothing
                    End If

                    If Not IsDBNull(dataReader("overall")) Then
                        .Overall = dataReader("overall")
                    Else
                        .Overall = Nothing
                    End If

                    If Not IsDBNull(dataReader("dateMark")) Then
                        .DateMark = dataReader("dateMark")
                    Else
                        .DateMark = Nothing
                    End If

                End With
            Else
                mark = Nothing
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

        Return mark
    End Function

    ''' <summary>
    ''' Method that returns a list with all the Marks from the database
    ''' </summary>
    ''' <returns>A list of Student</returns>
    ''' <remarks></remarks>
    Public Function getAllMarks() As List(Of clsMark)
        Dim listMarks As New List(Of clsMark)
        Dim connection As New SqlConnection
        Dim dataReader As SqlDataReader
        Dim sql As String
        Dim command As New SqlCommand

        Try
            'Conect to the dataBase
            connection = con.getConnection

            'Create object Command
            sql = "SELECT * FROM Mark ORDER BY dateMark DESC "

            command = New SqlCommand(sql, connection)

            'Get DataReader
            dataReader = command.ExecuteReader()

            'Get Groups
            While dataReader.Read()
                Dim mark As New clsMark
                With mark
                    If Not IsDBNull(dataReader("ID_Student")) Then
                        .StudentID = dataReader("ID_Student")
                    Else
                        .StudentID = Nothing
                    End If

                    If Not IsDBNull(dataReader("ID_Group")) Then
                        .GroupID = dataReader("ID_Group")
                    Else
                        .GroupID = Nothing
                    End If

                    If Not IsDBNull(dataReader("listening")) Then
                        .Listening = dataReader("listening")
                    Else
                        .Listening = Nothing
                    End If

                    If Not IsDBNull(dataReader("speaking")) Then
                        .Speaking = dataReader("speaking")
                    Else
                        .Speaking = Nothing
                    End If

                    If Not IsDBNull(dataReader("reading")) Then
                        .Reading = dataReader("reading")
                    Else
                        .Reading = Nothing
                    End If

                    If Not IsDBNull(dataReader("writing")) Then
                        .Writing = dataReader("writing")
                    Else
                        .Writing = Nothing
                    End If

                    If Not IsDBNull(dataReader("exam")) Then
                        .Exam = dataReader("exam")
                    Else
                        .Exam = Nothing
                    End If

                    If Not IsDBNull(dataReader("overall")) Then
                        .Overall = dataReader("overall")
                    Else
                        .Overall = Nothing
                    End If

                    If Not IsDBNull(dataReader("dateMark")) Then
                        .DateMark = dataReader("dateMark")
                    Else
                        .DateMark = Nothing
                    End If
                End With

                listMarks.Add(mark)
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

        Return listMarks
    End Function

    ''' <summary>
    ''' Method that returns a list with the Marks of a Student with the search from the database
    ''' </summary>
    ''' <param name="studentID">An Integer</param>
    ''' <returns>A list of clsMark</returns>
    ''' <remarks></remarks>
    Public Function getStudentMarks(ByVal studentID As Integer) As List(Of clsMark)
        Dim listMarks As New List(Of clsMark)
        Dim connection As New SqlConnection
        Dim dataReader As SqlDataReader
        Dim sql As String
        Dim command As New SqlCommand

        Try
            'Conect to the dataBase
            connection = con.getConnection

            'Create object Command
            sql = "SELECT * FROM Mark " & _
                    " WHERE ID_Student = @StudentID " & _
                    " ORDER BY dateMark DESC "
            command = New SqlCommand(sql, connection)

            If studentID = 0 Then
                command.Parameters.AddWithValue("@StudentID", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@StudentID", studentID)
            End If

            'Get DataReader
            dataReader = command.ExecuteReader()

            'Get Marks
            While dataReader.Read()
                Dim mark As New clsMark
                With mark
                    If Not IsDBNull(dataReader("ID_Student")) Then
                        .StudentID = dataReader("ID_Student")
                    Else
                        .StudentID = Nothing
                    End If

                    If Not IsDBNull(dataReader("ID_Group")) Then
                        .GroupID = dataReader("ID_Group")
                    Else
                        .GroupID = Nothing
                    End If

                    If Not IsDBNull(dataReader("listening")) Then
                        .Listening = dataReader("listening")
                    Else
                        .Listening = Nothing
                    End If

                    If Not IsDBNull(dataReader("speaking")) Then
                        .Speaking = dataReader("speaking")
                    Else
                        .Speaking = Nothing
                    End If

                    If Not IsDBNull(dataReader("reading")) Then
                        .Reading = dataReader("reading")
                    Else
                        .Reading = Nothing
                    End If

                    If Not IsDBNull(dataReader("writing")) Then
                        .Writing = dataReader("writing")
                    Else
                        .Writing = Nothing
                    End If

                    If Not IsDBNull(dataReader("exam")) Then
                        .Exam = dataReader("exam")
                    Else
                        .Exam = Nothing
                    End If

                    If Not IsDBNull(dataReader("overall")) Then
                        .Overall = dataReader("overall")
                    Else
                        .Overall = Nothing
                    End If

                    If Not IsDBNull(dataReader("dateMark")) Then
                        .DateMark = dataReader("dateMark")
                    Else
                        .DateMark = Nothing
                    End If
                End With

                listMarks.Add(mark)
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

        Return listMarks
    End Function

    ''' <summary>
    ''' Method that returns a list with the Marks of a Student with the search from the database
    ''' </summary>
    ''' <param name="studentID">An Integer</param>
    ''' <param name="groupID">An Integer</param>
    ''' <returns>A list of clsMark</returns>
    ''' <remarks></remarks>
    Public Function getGroupStudentMarks(ByVal studentID As Integer, ByVal groupID As Integer) As List(Of clsMark)
        Dim listMarks As New List(Of clsMark)
        Dim connection As New SqlConnection
        Dim dataReader As SqlDataReader
        Dim sql As String
        Dim command As New SqlCommand

        Try
            'Conect to the dataBase
            connection = con.getConnection

            'Create object Command
            sql = "SELECT * FROM Mark " & _
                    " WHERE ID_Student = @StudentID AND ID_Group = @GroupID " & _
                    " ORDER BY dateMark DESC "
            command = New SqlCommand(sql, connection)

            If studentID = 0 Then
                command.Parameters.AddWithValue("@StudentID", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@StudentID", studentID)
            End If

            If groupID = 0 Then
                command.Parameters.AddWithValue("@GroupID", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@GroupID", groupID)
            End If

            'Get DataReader
            dataReader = command.ExecuteReader()

            'Get Marks
            While dataReader.Read()
                Dim mark As New clsMark
                With mark
                    If Not IsDBNull(dataReader("ID_Student")) Then
                        .StudentID = dataReader("ID_Student")
                    Else
                        .StudentID = Nothing
                    End If

                    If Not IsDBNull(dataReader("ID_Group")) Then
                        .GroupID = dataReader("ID_Group")
                    Else
                        .GroupID = Nothing
                    End If

                    If Not IsDBNull(dataReader("listening")) Then
                        .Listening = dataReader("listening")
                    Else
                        .Listening = Nothing
                    End If

                    If Not IsDBNull(dataReader("speaking")) Then
                        .Speaking = dataReader("speaking")
                    Else
                        .Speaking = Nothing
                    End If

                    If Not IsDBNull(dataReader("reading")) Then
                        .Reading = dataReader("reading")
                    Else
                        .Reading = Nothing
                    End If

                    If Not IsDBNull(dataReader("writing")) Then
                        .Writing = dataReader("writing")
                    Else
                        .Writing = Nothing
                    End If

                    If Not IsDBNull(dataReader("exam")) Then
                        .Exam = dataReader("exam")
                    Else
                        .Exam = Nothing
                    End If

                    If Not IsDBNull(dataReader("overall")) Then
                        .Overall = dataReader("overall")
                    Else
                        .Overall = Nothing
                    End If

                    If Not IsDBNull(dataReader("dateMark")) Then
                        .DateMark = dataReader("dateMark")
                    Else
                        .DateMark = Nothing
                    End If
                End With

                listMarks.Add(mark)
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

        Return listMarks
    End Function

    ''' <summary>
    ''' Method that updates a Mark from the database
    ''' </summary>
    ''' <param name="mark">An object clsMark</param>
    ''' <returns>Returns an integer with the number of rows afected.</returns>
    ''' <remarks></remarks>
    Public Function updateMark(ByVal mark As clsMark) As Integer
        Dim connection As New SqlConnection
        Dim numReg As Integer
        Dim sql As String
        Dim command As New SqlCommand

        Try
            'Conect to the dataBase
            connection = con.getConnection

            'Create object Command
            sql = "UPDATE Mark SET listening = @Listening " _
                                    & ", speaking = @Speaking " _
                                    & ", reading = @Reading " _
                                    & ", writing = @Writing " _
                                    & ", exam = @Exam " _
                                    & ", overall = @Overall " _
                                    & " WHERE ID_Student = @StudentID AND ID_Group = @GroupID AND dateMark = @DateMark "

            command = New SqlCommand(sql, connection)
            command.Parameters.AddWithValue("@Listening", mark.Listening)
            command.Parameters.AddWithValue("@Speaking", mark.Speaking)
            command.Parameters.AddWithValue("@Reading", mark.Reading)
            command.Parameters.AddWithValue("@Writing", mark.Writing)
            command.Parameters.AddWithValue("@Exam", mark.Exam)
            command.Parameters.AddWithValue("@Overall", mark.Overall)

            If mark.DateMark = Nothing Then
                command.Parameters.AddWithValue("@DateMark", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@DateMark", mark.DateMark)
            End If

            If mark.StudentID = 0 Then
                command.Parameters.AddWithValue("@StudentID", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@StudentID", mark.StudentID)
            End If

            If mark.GroupID = 0 Then
                command.Parameters.AddWithValue("@GroupID", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@GroupID", mark.GroupID)
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
    ''' Method that inserts a new Mark into the database
    ''' </summary>
    ''' <param name="mark">An Object clsMark</param>
    ''' <returns>Returns an integer with the number of rows afected.</returns>
    ''' <remarks></remarks>
    Public Function insertMark(ByVal mark As clsMark) As Integer
        Dim connection As New SqlConnection
        Dim sql As String
        Dim command As New SqlCommand
        Dim numReg As Integer

        Try
            'Connect to the database
            connection = con.getConnection

            'Insert User
            sql = "INSERT INTO Mark(ID_Student,ID_Group,listening,speaking,reading,writing,exam,overall,dateMark) " _
                & "VALUES(@StudentID,@GroupID,@Listening,@Speaking,@Reading,@Writing,@Exam,@Overall,@DateMark)"

            command = New SqlCommand(sql, connection)

            command.Parameters.AddWithValue("@Listening", mark.Listening)
            command.Parameters.AddWithValue("@Speaking", mark.Speaking)
            command.Parameters.AddWithValue("@Reading", mark.Reading)
            command.Parameters.AddWithValue("@Writing", mark.Writing)
            command.Parameters.AddWithValue("@Exam", mark.Exam)
            command.Parameters.AddWithValue("@Overall", mark.Overall)

            If mark.DateMark = Nothing Then
                command.Parameters.AddWithValue("@DateMark", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@DateMark", mark.DateMark)
            End If

            If mark.StudentID = 0 Then
                command.Parameters.AddWithValue("@StudentID", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@StudentID", mark.StudentID)
            End If

            If mark.GroupID = 0 Then
                command.Parameters.AddWithValue("@GroupID", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@GroupID", mark.GroupID)
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

    ''' <summary>
    ''' Method that deletes a Mark from a Student
    ''' </summary>
    ''' <param name="studentID">An Integer</param>
    ''' <param name="groupID">An Integer</param>
    ''' <returns>Returns an integer with the number of rows afected.</returns>
    ''' <remarks></remarks>
    Public Function deleteMark(ByVal studentID As Integer, ByVal groupID As Integer, ByVal dateMark As Date) As Integer
        Dim connection As New SqlConnection
        Dim sql As String
        Dim command As New SqlCommand
        Dim numReg As Integer
        Try
            'Connect to the database
            connection = con.getConnection

            'Insert User
            sql = "DELETE FROM Mark WHERE ID_Student = @StudentID AND ID_Group = @GroupID AND dateMark = @DateMark "

            command = New SqlCommand(sql, connection)
            If studentID = 0 Then
                command.Parameters.AddWithValue("@StudentID", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@StudentID", studentID)
            End If

            If groupID = 0 Then
                command.Parameters.AddWithValue("@GroupID", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@GroupID", groupID)
            End If

            If dateMark = Nothing Then
                command.Parameters.AddWithValue("@DateMark", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@DateMark", dateMark)
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
