Imports System.Data.SqlClient

Public Class clsNotificationManager

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
    ''' Method that returns a notification from the database
    ''' </summary>
    ''' <pre>None</pre>
    ''' <param name="notificationID">An Integer</param>
    ''' <returns>An object clsNotification</returns>
    ''' <pos>Returns the object clsNotification.</pos>
    ''' <remarks></remarks>
    Public Function getNotification(ByVal notificationID As Integer) As clsNotification
        Dim connection As New SqlConnection
        Dim dataReader As SqlDataReader
        Dim sql As String
        Dim command As New SqlCommand
        Dim notification As New clsNotification

        Try
            'Conect to the dataBase
            connection = con.getConnection

            'Create object Command
            sql = "SELECT * FROM Notification " & _
                " WHERE ID_Notification = @NotificationID "
            command = New SqlCommand(sql, connection)
            If notificationID = 0 Then
                command.Parameters.AddWithValue("@CalendarEventID", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@CalendarEventID", notificationID)
            End If

            'Get DataReader
            dataReader = command.ExecuteReader()

            'Get CalendarEvent
            If dataReader.HasRows Then
                dataReader.Read()
                With notification
                    If Not IsDBNull(dataReader("ID_Notification")) Then
                        .NotificationID = dataReader("ID_Notification")
                    Else
                        .NotificationID = Nothing
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

                    If Not IsDBNull(dataReader("ID_EventCalendar")) Then
                        .EventCalendarID = dataReader("ID_EventCalendar")
                    Else
                        .EventCalendarID = Nothing
                    End If

                    If Not IsDBNull(dataReader("ID_Teacher")) Then
                        .TeacherID = dataReader("ID_Teacher")
                    Else
                        .TeacherID = Nothing
                    End If
                End With
            Else
                notification = Nothing
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

        Return notification
    End Function

    ''' <summary>
    ''' Method that returns all the notifications of a User from the database.
    ''' </summary>
    ''' <pre>None</pre>
    ''' <param name="teacherID">An Integer</param>
    ''' <returns>An object clsNotification</returns>
    ''' <pos>Returns the object clsNotification.</pos>
    ''' <remarks></remarks>
    Public Function getTeacherNotifications(ByVal teacherID As Integer) As List(Of clsNotification)
        Dim connection As New SqlConnection
        Dim dataReader As SqlDataReader
        Dim sql As String
        Dim command As New SqlCommand
        Dim userNotifications As New List(Of clsNotification)

        Try
            'Conect to the dataBase
            connection = con.getConnection

            'Create object Command
            sql = "SELECT * FROM Notification " & _
                " WHERE ID_Teacher = @TeacherID OR ID_Teacher IS NULL "
            command = New SqlCommand(sql, connection)
            If teacherID = 0 Then
                command.Parameters.AddWithValue("@TeacherID", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@TeacherID", teacherID)
            End If

            'Get DataReader
            dataReader = command.ExecuteReader()

            'Get CalendarEvent
            While dataReader.Read()
                Dim notification As New clsNotification

                With notification
                    If Not IsDBNull(dataReader("ID_Notification")) Then
                        .NotificationID = dataReader("ID_Notification")
                    Else
                        .NotificationID = Nothing
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

                    If Not IsDBNull(dataReader("ID_EventCalendar")) Then
                        .EventCalendarID = dataReader("ID_EventCalendar")
                    Else
                        .EventCalendarID = Nothing
                    End If

                    If Not IsDBNull(dataReader("ID_Teacher")) Then
                        .TeacherID = dataReader("ID_Teacher")
                    Else
                        .TeacherID = Nothing
                    End If
                End With

                userNotifications.Add(notification)
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

        Return userNotifications
    End Function

    ''' <summary>
    ''' Method that inserts a new Notification into the database
    ''' </summary>
    ''' <param name="notification">An object clsNotification</param>
    ''' <returns>Returns an integer with the number of rows afected.</returns>
    ''' <remarks></remarks>
    Public Function insertNotification(ByVal notification As clsNotification) As Integer
        Dim connection As New SqlConnection
        Dim sql As String
        Dim command As New SqlCommand
        Dim newPersonID As New Integer
        Dim numReg As Integer

        Try
            'Conect to the dataBase
            connection = con.getConnection

            'Insert User
            sql = "INSERT INTO Notification (subject,detail,ID_EventCalendar,ID_Teacher) " _
                & "VALUES(@Subject,@Detail,@EventCalendarID,@TeacherID)"

            command = New SqlCommand(sql, connection)
            If notification.Subject Is "" Or notification.Subject Is Nothing Then
                command.Parameters.AddWithValue("@Subject", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@Subject", notification.Subject)
            End If

            If notification.Detail Is "" Or notification.Detail Is Nothing Then
                command.Parameters.AddWithValue("@Detail", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@Detail", notification.Detail)
            End If

            If notification.EventCalendarID = 0 Then
                command.Parameters.AddWithValue("@EventCalendarID", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@EventCalendarID", notification.EventCalendarID)
            End If

            If notification.TeacherID = 0 Then
                command.Parameters.AddWithValue("@TeacherID", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@TeacherID", notification.TeacherID)
            End If

            'Execute insert User
            numReg = command.ExecuteNonQuery()

            'Disconnect from the database
            con.closeConnection(connection)
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
    ''' Method that updates an Event from the database
    ''' </summary>
    ''' <param name="notification">An object clsNotification</param>
    ''' <returns>Returns an integer with the number of rows afected.</returns>
    ''' <remarks></remarks>
    Public Function updateNotification(ByVal notification As clsNotification) As Integer
        Dim connection As New SqlConnection
        Dim sql As String
        Dim command As New SqlCommand
        Dim newPersonID As New Integer
        Dim numReg As Integer

        'Conect to the dataBase
        connection = con.getConnection

        'Insert User
        sql = "UPDATE Notification " & _
                " SET subject = @Subject, " & _
                " detail = @Detail " & _
                " ID_EventCalendar = @EventCalendarID " & _
                " ID_Teacher = @TeacherID " & _
                " WHERE ID_Notification = @NotificationID "

        command = New SqlCommand(sql, connection)
        If notification.NotificationID = 0 Then
            command.Parameters.AddWithValue("@NotificationID", DBNull.Value)
        Else
            command.Parameters.AddWithValue("@NotificationID", notification.NotificationID)
        End If

        If notification.Subject Is "" Or notification.Subject Is Nothing Then
            command.Parameters.AddWithValue("@Subject", DBNull.Value)
        Else
            command.Parameters.AddWithValue("@Subject", notification.Subject)
        End If

        If notification.Detail Is "" Or notification.Detail Is Nothing Then
            command.Parameters.AddWithValue("@Detail", DBNull.Value)
        Else
            command.Parameters.AddWithValue("@Detail", notification.Detail)
        End If

        If notification.EventCalendarID = 0 Then
            command.Parameters.AddWithValue("@EventCalendarID", DBNull.Value)
        Else
            command.Parameters.AddWithValue("@EventCalendarID", notification.EventCalendarID)
        End If

        If notification.TeacherID = 0 Then
            command.Parameters.AddWithValue("@TeacherID", DBNull.Value)
        Else
            command.Parameters.AddWithValue("@TeacherID", notification.TeacherID)
        End If

        'Execute insert User
        numReg = command.ExecuteNonQuery()

        'Disconnect from the database
        con.closeConnection(connection)

        Return numReg
    End Function

    ''' <summary>
    ''' Method that deletes an Notification from the database
    ''' </summary>
    ''' <param name="notificationID">An Integer</param>
    ''' <returns></returns>
    ''' <remarks>Returns an integer with the number of rows afected.</remarks>
    Public Function deleteNotification(ByVal notificationID As Integer) As Integer
        Dim connection As New SqlConnection
        Dim numReg As Integer
        Dim sql As String
        Dim command As New SqlCommand

        Try
            'Conect to the dataBase
            connection = con.getConnection

            'Create object Command
            sql = "DELETE FROM Notification WHERE ID_Notification = @NotificationID "

            command = New SqlCommand(sql, connection)
            If notificationID = 0 Then
                command.Parameters.AddWithValue("@NotificationID", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@NotificationID", notificationID)
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
    ''' Method that deletes all the Notification from the database
    ''' </summary>
    ''' <param name="teacherID">An Integer</param>
    ''' <returns>Returns an integer with the number of rows afected.</returns>
    ''' <remarks></remarks>
    Public Function clearNotifications(ByVal teacherID As Integer) As Integer
        Dim connection As New SqlConnection
        Dim numReg As Integer
        Dim sql As String
        Dim command As New SqlCommand

        Try
            'Conect to the dataBase
            connection = con.getConnection

            'Create object Command
            sql = "DELETE FROM [Notification] WHERE ID_Teacher = @TeacherID "

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