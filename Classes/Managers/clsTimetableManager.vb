Imports System.Data.SqlClient

Public Class clsTimetableManager
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
    ''' Method that returns the morning or afternoon timetable from the database
    ''' </summary>
    ''' <pre>None</pre>
    ''' <param name="timetableID">An Integer</param>
    ''' <returns>A list of clsTimetable</returns>
    ''' <pos>Returns the clsTimetable.</pos>
    ''' <remarks></remarks>
    Public Function getTimetableClassByID(ByVal timetableID As Integer) As clsTimetable
        Dim connection As New SqlConnection
        Dim dataReader As SqlDataReader
        Dim sql As String
        Dim command As New SqlCommand
        Dim timetableClass As New clsTimetable

        Try
            'Conect to the dataBase
            connection = con.getConnection

            'Create object Command
            sql = "SELECT * FROM Timetable " & _
                    " WHERE ID_Timetable = @TimetableID "

            command = New SqlCommand(sql, connection)
            If timetableID = 0 Then
                command.Parameters.AddWithValue("@TimetableID", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@TimetableID", timetableID)
            End If

            'Get DataReader
            dataReader = command.ExecuteReader()

            'Get Timetable
            If dataReader.HasRows Then
                dataReader.Read()

                With timetableClass
                    If Not IsDBNull(dataReader("ID_Timetable")) Then
                        .TimetableID = dataReader("ID_Timetable")
                    Else
                        .TimetableID = Nothing
                    End If

                    If Not IsDBNull(dataReader("day")) Then
                        .Day = dataReader("day")
                    Else
                        .Day = Nothing
                    End If

                    If Not IsDBNull(dataReader("hour")) Then
                        .Hour = dataReader("hour")
                    Else
                        .Hour = Nothing
                    End If

                    If Not IsDBNull(dataReader("ID_Group")) Then
                        .GroupID = dataReader("ID_Group")
                    Else
                        .GroupID = Nothing
                    End If
                End With
            Else
                timetableClass = Nothing
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

        Return timetableClass
    End Function

    ''' <summary>
    ''' Method that returns the morning or afternoon timetable from the database
    ''' </summary>
    ''' <pre>None</pre>
    ''' <param name="timeDay">A String</param>
    ''' <returns>A list of clsTimetable</returns>
    ''' <pos>Returns the user.</pos>
    ''' <remarks></remarks>
    Public Function getTimetable(ByVal timeDay As Integer) As List(Of clsTimetable)
        Dim connection As New SqlConnection
        Dim dataReader As SqlDataReader
        Dim sql As String
        Dim command As New SqlCommand
        Dim listTimetable As New List(Of clsTimetable)

        Try
            'Conect to the dataBase
            connection = con.getConnection

            'Create object Command
            sql = "SELECT * FROM Timetable "

            If timeDay = 1 Then
                sql &= " WHERE [hour] < '15:30' "
            Else
                sql &= " WHERE [hour] > '15:00' "
            End If

            command = New SqlCommand(sql, connection)

            'Get DataReader
            dataReader = command.ExecuteReader()

            'Get Timetable
            While dataReader.Read()
                Dim timetable As New clsTimetable

                With timetable
                    If Not IsDBNull(dataReader("ID_Timetable")) Then
                        .TimetableID = dataReader("ID_Timetable")
                    Else
                        .TimetableID = Nothing
                    End If

                    If Not IsDBNull(dataReader("day")) Then
                        .Day = dataReader("day")
                    Else
                        .Day = Nothing
                    End If

                    If Not IsDBNull(dataReader("hour")) Then
                        .Hour = dataReader("hour")
                    Else
                        .Hour = Nothing
                    End If

                    If Not IsDBNull(dataReader("ID_Group")) Then
                        .GroupID = dataReader("ID_Group")
                    Else
                        .GroupID = Nothing
                    End If
                End With

                listTimetable.Add(timetable)
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

        Return listTimetable
    End Function

    ''' <summary>
    ''' Method that returns a timetable class of a teacher from the database
    ''' </summary>
    ''' <pre>None</pre>
    ''' <param name="day">A String</param>
    ''' <param name="hour">An object Timespan</param>
    ''' <returns>A list of clsTimetable</returns>
    ''' <pos>Returns the user.</pos>
    ''' <remarks></remarks>
    Public Function getTimetableClassTeacher(ByVal day As String, ByVal hour As TimeSpan, ByVal teacherID As Integer) As clsTimetable
        Dim connection As New SqlConnection
        Dim dataReader As SqlDataReader
        Dim sql As String
        Dim command As New SqlCommand
        Dim timetableClass As New clsTimetable

        Try
            'Conect to the dataBase
            connection = con.getConnection

            'Create object Command
            sql = "SELECT * FROM Timetable AS T " & _
                    " JOIN [Group] AS G ON T.ID_Group = G.ID_Group " & _
                    " WHERE T.[day] = @Day AND T.[hour] = @Hour AND G.ID_Teacher = @TeacherID "

            command = New SqlCommand(sql, connection)
            If day Is Nothing Then
                command.Parameters.AddWithValue("@Day", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@Day", day)
            End If

            If hour = Nothing Then
                command.Parameters.AddWithValue("@Hour", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@Hour", hour)
            End If
            If teacherID = 0 Then
                command.Parameters.AddWithValue("@TeacherID", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@TeacherID", teacherID)
            End If

            'Get DataReader
            dataReader = command.ExecuteReader()

            'Get Timetable
            If dataReader.HasRows Then
                dataReader.Read()

                With timetableClass
                    If Not IsDBNull(dataReader("ID_Timetable")) Then
                        .TimetableID = dataReader("ID_Timetable")
                    Else
                        .TimetableID = Nothing
                    End If

                    If Not IsDBNull(dataReader("day")) Then
                        .Day = dataReader("day")
                    Else
                        .Day = Nothing
                    End If

                    If Not IsDBNull(dataReader("hour")) Then
                        .Hour = dataReader("hour")
                    Else
                        .Hour = Nothing
                    End If

                    If Not IsDBNull(dataReader("ID_Group")) Then
                        .GroupID = dataReader("ID_Group")
                    Else
                        .GroupID = Nothing
                    End If
                End With
            Else
                timetableClass = Nothing
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

        Return timetableClass
    End Function

    ''' <summary>
    ''' Method that returns a timetable class of a group from the database
    ''' </summary>
    ''' <pre>None</pre>
    ''' <param name="day">A String</param>
    ''' <param name="hour">An object Timespan</param>
    ''' <returns>A list of clsTimetable</returns>
    ''' <pos>Returns the user.</pos>
    ''' <remarks></remarks>
    Public Function getTimetableClassGroup(ByVal day As String, ByVal hour As TimeSpan, ByVal groupID As Integer) As clsTimetable
        Dim connection As New SqlConnection
        Dim dataReader As SqlDataReader
        Dim sql As String
        Dim command As New SqlCommand
        Dim timetableClass As New clsTimetable

        Try
            'Conect to the dataBase
            connection = con.getConnection

            'Create object Command
            sql = "SELECT * FROM Timetable " & _
                    " WHERE [day] = @Day AND [hour] = @Hour AND ID_Group = @GroupID "

            command = New SqlCommand(sql, connection)
            If day Is Nothing Then
                command.Parameters.AddWithValue("@Day", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@Day", day)
            End If

            If hour = Nothing Then
                command.Parameters.AddWithValue("@Hour", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@Hour", hour)
            End If
            If groupID = 0 Then
                command.Parameters.AddWithValue("@GroupID", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@GroupID", groupID)
            End If

            'Get DataReader
            dataReader = command.ExecuteReader()

            'Get Timetable
            If dataReader.HasRows Then
                dataReader.Read()

                With timetableClass
                    If Not IsDBNull(dataReader("ID_Timetable")) Then
                        .TimetableID = dataReader("ID_Timetable")
                    Else
                        .TimetableID = Nothing
                    End If

                    If Not IsDBNull(dataReader("day")) Then
                        .Day = dataReader("day")
                    Else
                        .Day = Nothing
                    End If

                    If Not IsDBNull(dataReader("hour")) Then
                        .Hour = dataReader("hour")
                    Else
                        .Hour = Nothing
                    End If

                    If Not IsDBNull(dataReader("ID_Group")) Then
                        .GroupID = dataReader("ID_Group")
                    Else
                        .GroupID = Nothing
                    End If
                End With
            Else
                timetableClass = Nothing
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

        Return timetableClass
    End Function

    ''' <summary>
    ''' Method that returns a timetable class of a group from the database
    ''' </summary>
    ''' <pre>None</pre>
    ''' <param name="groupID">An Integer</param>
    ''' <returns>A list of clsTimetable</returns>
    ''' <pos>Returns 0 if the time of day of the group is the Morning or all day, or 1 if it is the Afternoon</pos>
    ''' <remarks></remarks>
    Public Function getTimeOfDayGroup(ByVal groupID As Integer) As Integer
        Dim connection As New SqlConnection
        Dim dataReader As SqlDataReader
        Dim sql As String
        Dim command As New SqlCommand
        Dim timeOfDay As Integer = 1

        Try
            'Conect to the dataBase
            connection = con.getConnection

            'Create object Command
            sql = "SELECT * FROM Timetable " & _
                    " WHERE ID_Group = @GroupID "

            command = New SqlCommand(sql, connection)
            If groupID = 0 Then
                command.Parameters.AddWithValue("@GroupID", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@GroupID", groupID)
            End If

            'Get DataReader
            dataReader = command.ExecuteReader()

            'Get Timetable
            If dataReader.HasRows Then
                While dataReader.Read() And timeOfDay = 1
                    Dim timetableClass As New clsTimetable
                    With timetableClass
                        If Not IsDBNull(dataReader("hour")) Then
                            .Hour = dataReader("hour")
                        Else
                            .Hour = Nothing
                        End If

                        If Not .Hour = Nothing Then
                            If .Hour < New TimeSpan(0, 15, 0, 0, 0) Then
                                timeOfDay = 0
                            End If
                        End If
                    End With
                End While
            Else
                timeOfDay = 0
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

        Return timeOfDay
    End Function

    ''' <summary>
    ''' Method that inserts a new Timetable into the database
    ''' </summary>
    ''' <param name="timetable">An object clsStudent</param>
    ''' <returns>Returns an integer with the number of rows afected.</returns>
    ''' <remarks></remarks>
    Public Function insertTimetable(ByVal timetable As clsTimetable) As Integer
        Dim connection As New SqlConnection
        Dim sql As String
        Dim command As New SqlCommand
        Dim newPersonID As New Integer
        Dim numReg As Integer

        'Conect to the dataBase
        connection = con.getConnection

        'Insert Timetable
        sql = "INSERT INTO Timetable ([day],[hour],ID_Group) " _
            & "VALUES(@Day,@Hour,@GroupID)"

        command = New SqlCommand(sql, connection)
        If timetable.Day Is "" Or timetable.Day Is Nothing Then
            command.Parameters.AddWithValue("@Day", DBNull.Value)
        Else
            command.Parameters.AddWithValue("@Day", timetable.Day)
        End If

        If timetable.Hour = Nothing Then
            command.Parameters.AddWithValue("@Hour", DBNull.Value)
        Else
            command.Parameters.AddWithValue("@Hour", timetable.Hour)
        End If

        If timetable.GroupID = 0 Then
            command.Parameters.AddWithValue("@GroupID", DBNull.Value)
        Else
            command.Parameters.AddWithValue("@GroupID", timetable.GroupID)
        End If

        'Execute insert Timetable
        numReg = command.ExecuteNonQuery()

        'Disconnect from the database
        con.closeConnection(connection)

        Return numReg
    End Function

    ''' <summary>
    ''' Method that deletes a Timetable from the database
    ''' </summary>
    ''' <param name="timetableID">An Integer</param>
    ''' <returns></returns>
    ''' <remarks>Returns an integer with the number of rows afected.</remarks>
    Public Function deleteTimetable(ByVal timetableID As Integer) As Integer
        Dim connection As New SqlConnection
        Dim numReg As Integer
        Dim sql As String
        Dim command As New SqlCommand

        Try
            'Conect to the dataBase
            connection = con.getConnection

            'Create object Command
            sql = "DELETE FROM Timetable WHERE ID_Timetable = @TimetableID "

            command = New SqlCommand(sql, connection)
            If timetableID = 0 Then
                command.Parameters.AddWithValue("@TimetableID", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@TimetableID", timetableID)
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

    ''' <summary>
    ''' Method that deletes a Timetable of a Group from the database
    ''' </summary>
    ''' <param name="groupID">An Integer</param>
    ''' <returns></returns>
    ''' <remarks>Returns an integer with the number of rows afected.</remarks>
    Public Function deleteTimetableGroup(ByVal groupID As Integer) As Integer
        Dim connection As New SqlConnection
        Dim numReg As Integer
        Dim sql As String
        Dim command As New SqlCommand

        Try
            'Conect to the dataBase
            connection = con.getConnection

            'Create object Command
            sql = "DELETE FROM Timetable WHERE ID_Group = @GroupID "

            command = New SqlCommand(sql, connection)
            If groupID = 0 Then
                command.Parameters.AddWithValue("@GroupID", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@GroupID", groupID)
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
