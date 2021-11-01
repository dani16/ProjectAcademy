Imports System.Data.SqlClient

Public Class clsCalendarEventManager
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
    ''' Method that returns a Calendar event from the database
    ''' </summary>
    ''' <pre>None</pre>
    ''' <param name="calendarEventID">An Integer</param>
    ''' <returns>An object clsEnglishLevel</returns>
    ''' <pos>Returns the object clsCalendarEvent.</pos>
    ''' <remarks></remarks>
    Public Function getCalendarEvent(ByVal calendarEventID As Integer) As clsCalendarEvent
        Dim connection As New SqlConnection
        Dim dataReader As SqlDataReader
        Dim sql As String
        Dim command As New SqlCommand
        Dim calendarEvent As New clsCalendarEvent

        Try
            'Conect to the dataBase
            connection = con.getConnection

            'Create object Command
            sql = "SELECT * FROM CalendarEvent " & _
                " WHERE ID_CalendarEvent = @CalendarEventID "
            command = New SqlCommand(sql, connection)
            If calendarEventID = 0 Then
                command.Parameters.AddWithValue("@CalendarEventID", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@CalendarEventID", calendarEventID)
            End If

            'Get DataReader
            dataReader = command.ExecuteReader()

            'Get CalendarEvent
            If dataReader.HasRows Then
                dataReader.Read()
                With calendarEvent
                    If Not IsDBNull(dataReader("ID_CalendarEvent")) Then
                        .CalendarEventID = dataReader("ID_CalendarEvent")
                    Else
                        .CalendarEventID = Nothing
                    End If

                    If Not IsDBNull(dataReader("startDate")) Then
                        .StartDate = dataReader("startDate")
                    Else
                        .StartDate = Nothing
                    End If

                    If Not IsDBNull(dataReader("finishDate")) Then
                        .FinishDate = dataReader("finishDate")
                    Else
                        .FinishDate = Nothing
                    End If

                    If Not IsDBNull(dataReader("type")) Then
                        .Type = dataReader("type")
                    Else
                        .Type = Nothing
                    End If

                    If Not IsDBNull(dataReader("subject")) Then
                        .Subject = dataReader("subject")
                    Else
                        .Subject = Nothing
                    End If

                    If Not IsDBNull(dataReader("detail")) Then
                        .Detail = dataReader("detail")
                    Else
                        .Detail = Nothing
                    End If
                End With
            Else
                calendarEvent = Nothing
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

        Return calendarEvent
    End Function

    ''' <summary>
    ''' Method that returns all the Caldendar Events of a day from the database.
    ''' </summary>
    ''' <pre>None</pre>
    ''' <param name="teacherID">An Integer</param>
    ''' <param name="eventDate">An object Date</param>
    ''' <returns>An object clsEnglishLevel</returns>
    ''' <pos>Returns the object clsCalendarEvent.</pos>
    ''' <remarks></remarks>
    Public Function getCalendarEventByDate(ByVal teacherID As Integer, ByVal eventDate As Date) As List(Of clsCalendarEvent)
        Dim connection As New SqlConnection
        Dim dataReader As SqlDataReader
        Dim sql As String
        Dim command As New SqlCommand
        Dim listCalendarEvents As New List(Of clsCalendarEvent)

        Try
            'Conect to the dataBase
            connection = con.getConnection

            'Create object Command
            sql = "SELECT * FROM CalendarEvent " & _
                " WHERE @Date BETWEEN startDate AND finishDate AND ID_Teacher = @TeacherID "
            command = New SqlCommand(sql, connection)
            If teacherID = 0 Then
                command.Parameters.AddWithValue("@TeacherID", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@TeacherID", teacherID)
            End If

            If eventDate = Nothing Then
                command.Parameters.AddWithValue("@Date", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@Date", eventDate)
            End If

            'Get DataReader
            dataReader = command.ExecuteReader()

            'Get CalendarEvent
            While dataReader.Read()
                Dim calendarEvent As New clsCalendarEvent

                With calendarEvent
                    If Not IsDBNull(dataReader("ID_CalendarEvent")) Then
                        .CalendarEventID = dataReader("ID_CalendarEvent")
                    Else
                        .CalendarEventID = Nothing
                    End If

                    If Not IsDBNull(dataReader("startDate")) Then
                        .StartDate = dataReader("startDate")
                    Else
                        .StartDate = Nothing
                    End If

                    If Not IsDBNull(dataReader("finishDate")) Then
                        .FinishDate = dataReader("finishDate")
                    Else
                        .FinishDate = Nothing
                    End If

                    If Not IsDBNull(dataReader("type")) Then
                        .Type = dataReader("type")
                    Else
                        .Type = Nothing
                    End If

                    If Not IsDBNull(dataReader("subject")) Then
                        .Subject = dataReader("subject")
                    Else
                        .Subject = Nothing
                    End If

                    If Not IsDBNull(dataReader("detail")) Then
                        .Detail = dataReader("detail")
                    Else
                        .Detail = Nothing
                    End If
                End With

                listCalendarEvents.Add(calendarEvent)
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

        Return listCalendarEvents
    End Function

    ''' <summary>
    ''' Method that return all the Caldendar Events from the database.
    ''' </summary>
    ''' <returns>A list of clsCalendarEvent</returns>
    ''' <remarks></remarks>
    Public Function getCalendarEventsToNotify(ByVal teacherID As Integer, ByVal preferences As clsPreferences) As List(Of clsCalendarEvent)
        Dim listCalendarEvents As New List(Of clsCalendarEvent)
        Dim connection As New SqlConnection
        Dim dataReader As SqlDataReader
        Dim sql As String
        Dim command As New SqlCommand

        Try
            'Conect to the dataBase
            connection = con.getConnection

            'Create object Command
            sql = "SELECT * FROM CalendarEvent AS C " & _
                    " JOIN [User] AS U ON C.ID_Teacher = U.ID_Teacher " & _
                    " JOIN Preferences AS P ON U.ID_User = P.ID_User " & _
                    " WHERE C.ID_Teacher = @TeacherID And " & _
                    " GETDATE() BETWEEN DATEADD(DAY,-@DayToNotify,C.startDate) AND C.finishDate "

            command = New SqlCommand(sql, connection)
            If teacherID = 0 Then
                command.Parameters.AddWithValue("@TeacherID", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@TeacherID", teacherID)
            End If

            If preferences Is Nothing Then
                command.Parameters.AddWithValue("@DayToNotify", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@DayToNotify", preferences.DaysNotifyEvents)
            End If

            'Get DataReader
            dataReader = command.ExecuteReader()

            'Get CalendarEvent
            While dataReader.Read()
                Dim calendarEvent As New clsCalendarEvent

                With calendarEvent
                    If Not IsDBNull(dataReader("ID_CalendarEvent")) Then
                        .CalendarEventID = dataReader("ID_CalendarEvent")
                    Else
                        .CalendarEventID = Nothing
                    End If

                    If Not IsDBNull(dataReader("startDate")) Then
                        .StartDate = dataReader("startDate")
                    Else
                        .StartDate = Nothing
                    End If

                    If Not IsDBNull(dataReader("finishDate")) Then
                        .FinishDate = dataReader("finishDate")
                    Else
                        .FinishDate = Nothing
                    End If

                    If Not IsDBNull(dataReader("type")) Then
                        .Type = dataReader("type")
                    Else
                        .Type = Nothing
                    End If

                    If Not IsDBNull(dataReader("subject")) Then
                        .Subject = dataReader("subject")
                    Else
                        .Subject = Nothing
                    End If

                    If Not IsDBNull(dataReader("detail")) Then
                        .Detail = dataReader("detail")
                    Else
                        .Detail = Nothing
                    End If

                    If Not IsDBNull(dataReader("ID_Teacher")) Then
                        .TeacherID = dataReader("ID_Teacher")
                    Else
                        .TeacherID = Nothing
                    End If
                End With

                listCalendarEvents.Add(calendarEvent)
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

        Return listCalendarEvents
    End Function

    ''' <summary>
    ''' Method that return all the Caldendar Events from the database.
    ''' </summary>
    ''' <returns>A list of clsCalendarEvent</returns>
    ''' <remarks></remarks>
    Public Function getCalendarEvents() As List(Of clsCalendarEvent)
        Dim listCalendarEvents As New List(Of clsCalendarEvent)
        Dim connection As New SqlConnection
        Dim dataReader As SqlDataReader
        Dim sql As String
        Dim command As New SqlCommand

        Try
            'Conect to the dataBase
            connection = con.getConnection

            'Create object Command
            sql = "SELECT * FROM CalendarEvent "

            command = New SqlCommand(sql, connection)

            'Get DataReader
            dataReader = command.ExecuteReader()

            'Get CalendarEvent
            While dataReader.Read()
                Dim calendarEvent As New clsCalendarEvent

                With calendarEvent
                    If Not IsDBNull(dataReader("ID_CalendarEvent")) Then
                        .CalendarEventID = dataReader("ID_CalendarEvent")
                    Else
                        .CalendarEventID = Nothing
                    End If

                    If Not IsDBNull(dataReader("startDate")) Then
                        .StartDate = dataReader("startDate")
                    Else
                        .StartDate = Nothing
                    End If

                    If Not IsDBNull(dataReader("finishDate")) Then
                        .FinishDate = dataReader("finishDate")
                    Else
                        .FinishDate = Nothing
                    End If

                    If Not IsDBNull(dataReader("type")) Then
                        .Type = dataReader("type")
                    Else
                        .Type = Nothing
                    End If

                    If Not IsDBNull(dataReader("subject")) Then
                        .Subject = dataReader("subject")
                    Else
                        .Subject = Nothing
                    End If

                    If Not IsDBNull(dataReader("detail")) Then
                        .Detail = dataReader("detail")
                    Else
                        .Detail = Nothing
                    End If
                End With

                listCalendarEvents.Add(calendarEvent)
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

        Return listCalendarEvents
    End Function

    ''' <summary>
    ''' Method that inserts a new Event into the database
    ''' </summary>
    ''' <param name="eventCalendar">An object clsCalendarEvent</param>
    ''' <returns>Returns an integer with the number of rows afected.</returns>
    ''' <remarks></remarks>
    Public Function insertEvent(ByVal eventCalendar As clsCalendarEvent) As Integer
        Dim connection As New SqlConnection
        Dim sql As String
        Dim command As New SqlCommand
        Dim newPersonID As New Integer
        Dim numReg As Integer

        'Conect to the dataBase
        connection = con.getConnection

        'Insert User
        sql = "INSERT INTO CalendarEvent (startDate,finishDate,type,subject,detail,ID_Teacher) " _
            & "VALUES(@StartDate,@FinishDate,@Type,@Subject,@Detail,@TeacherID)"

        command = New SqlCommand(sql, connection)
        If eventCalendar.StartDate = Nothing Then
            command.Parameters.AddWithValue("@StartDate", DBNull.Value)
        Else
            command.Parameters.AddWithValue("@StartDate", eventCalendar.StartDate)
        End If

        If eventCalendar.FinishDate = Nothing Then
            command.Parameters.AddWithValue("@FinishDate", DBNull.Value)
        Else
            command.Parameters.AddWithValue("@FinishDate", eventCalendar.FinishDate)
        End If

        command.Parameters.AddWithValue("@Type", eventCalendar.Type)

        If eventCalendar.Subject Is "" Or eventCalendar.Subject Is Nothing Then
            command.Parameters.AddWithValue("@Subject", DBNull.Value)
        Else
            command.Parameters.AddWithValue("@Subject", eventCalendar.Subject)
        End If

        If eventCalendar.Detail Is "" Or eventCalendar.Detail Is Nothing Then
            command.Parameters.AddWithValue("@Detail", DBNull.Value)
        Else
            command.Parameters.AddWithValue("@Detail", eventCalendar.Detail)
        End If

        If eventCalendar.TeacherID = 0 Then
            command.Parameters.AddWithValue("@TeacherID", DBNull.Value)
        Else
            command.Parameters.AddWithValue("@TeacherID", eventCalendar.TeacherID)
        End If

        'Execute insert User
        numReg = command.ExecuteNonQuery()

        'Disconnect from the database
        con.closeConnection(connection)

        Return numReg
    End Function

    ''' <summary>
    ''' Method that updates an Event from the database
    ''' </summary>
    ''' <param name="eventCalendar">An object clsCalendarEvent</param>
    ''' <returns>Returns an integer with the number of rows afected.</returns>
    ''' <remarks></remarks>
    Public Function updateEvent(ByVal eventCalendar As clsCalendarEvent) As Integer
        Dim connection As New SqlConnection
        Dim sql As String
        Dim command As New SqlCommand
        Dim newPersonID As New Integer
        Dim numReg As Integer

        'Conect to the dataBase
        connection = con.getConnection

        'Insert User
        sql = "UPDATE CalendarEvent " & _
                " SET startDate = @StartDate, " & _
                " finishDate = @FinishDate, " & _
                " type = @Type, " & _
                " subject = @Subject, " & _
                " detail = @Detail " & _
                " WHERE ID_CalendarEvent = @CalendarEventID "

        command = New SqlCommand(sql, connection)
        If eventCalendar.CalendarEventID = 0 Then
            command.Parameters.AddWithValue("@CalendarEventID", DBNull.Value)
        Else
            command.Parameters.AddWithValue("@CalendarEventID", eventCalendar.CalendarEventID)
        End If

        If eventCalendar.StartDate = Nothing Then
            command.Parameters.AddWithValue("@StartDate", DBNull.Value)
        Else
            command.Parameters.AddWithValue("@StartDate", eventCalendar.StartDate)
        End If

        If eventCalendar.FinishDate = Nothing Then
            command.Parameters.AddWithValue("@FinishDate", DBNull.Value)
        Else
            command.Parameters.AddWithValue("@FinishDate", eventCalendar.FinishDate)
        End If

        If eventCalendar.Type = 0 Then
            command.Parameters.AddWithValue("@Type", DBNull.Value)
        Else
            command.Parameters.AddWithValue("@Type", eventCalendar.Type)
        End If

        If eventCalendar.Subject Is "" Or eventCalendar.Subject Is Nothing Then
            command.Parameters.AddWithValue("@Subject", DBNull.Value)
        Else
            command.Parameters.AddWithValue("@Subject", eventCalendar.Subject)
        End If

        If eventCalendar.Detail Is "" Or eventCalendar.Detail Is Nothing Then
            command.Parameters.AddWithValue("@Detail", DBNull.Value)
        Else
            command.Parameters.AddWithValue("@Detail", eventCalendar.Detail)
        End If

        'Execute insert User
        numReg = command.ExecuteNonQuery()

        'Disconnect from the database
        con.closeConnection(connection)

        Return numReg
    End Function

    ''' <summary>
    ''' Method that deletes an Event from the database
    ''' </summary>
    ''' <param name="eventID">An Integer</param>
    ''' <returns></returns>
    ''' <remarks>Returns an integer with the number of rows afected.</remarks>
    Public Function deleteEvent(ByVal eventID As Integer) As Integer
        Dim connection As New SqlConnection
        Dim numReg As Integer
        Dim sql As String
        Dim command As New SqlCommand

        Try
            'Conect to the dataBase
            connection = con.getConnection

            'Create object Command
            sql = "DELETE FROM CalendarEvent WHERE ID_CalendarEvent = @EventID "

            command = New SqlCommand(sql, connection)
            If eventID = 0 Then
                command.Parameters.AddWithValue("@EventID", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@EventID", eventID)
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
