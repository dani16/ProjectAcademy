Imports System.Data.SqlClient

Public Class clsPreferencesManager

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
    ''' Method that gets the preferences values from the database
    ''' </summary>
    ''' <param name="userID">An Integer</param>
    ''' <pre>None</pre>
    ''' <returns>An object clsPreference</returns>
    ''' <pos>Returns the object clsPreference of a Teacher</pos>
    ''' <remarks></remarks>
    Public Function getPreferences(ByVal userID As Integer) As clsPreferences
        Dim preferences As New clsPreferences
        Dim connection As New SqlConnection
        Dim dataReader As SqlDataReader
        Dim sql As String
        Dim command As New SqlCommand

        Try
            'Connect to the dataBase
            connection = con.getConnection

            'Create object Command
            sql = "SELECT * FROM Preferences WHERE ID_User = @UserID "
            command = New SqlCommand(sql, connection)

            If userID = 0 Then
                command.Parameters.AddWithValue("@UserID", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@UserID", userID)
            End If

            ''Get DataReader
            dataReader = command.ExecuteReader()

            'Get Configuration
            If dataReader.HasRows Then
                dataReader.Read()
                With preferences
                    .LanguageDefault = dataReader("languageDefault")
                    .ActivateEventNotifications = dataReader("activateEventNotifications")
                    .ActivatePaymentsNotifications = dataReader("activatePaymentsNotifications")
                    .DaysNotifyEvents = dataReader("daysNotifyEvents")
                    .DaysNotifyExam = dataReader("daysNotifyExam")
                    .DaysNotifyTest = dataReader("daysNotifyTest")
                    .DaysNotifyHoliday = dataReader("dayNotifyHoliday")
                    .DaysNotifyOthers = dataReader("dayNotifyOthers")
                    .DaysNotifyPayments = dataReader("daysNotifyPayments")
                End With
            Else
                preferences = Nothing
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

        Return preferences
    End Function

    ''' <summary>
    ''' Method that updates a user Preferences
    ''' </summary>
    ''' <param name="preferences">An Object clsPreferences</param>
    ''' <returns>Returns an integer with the number of rows afected.</returns>
    ''' <remarks></remarks>
    Public Function savePreferencesChanges(ByVal preferences As clsPreferences) As Integer
        Dim connection As New SqlConnection
        Dim numReg As Integer
        Dim sql As String
        Dim command As New SqlCommand

        'Conect to the dataBase
        connection = con.getConnection

        'Create object Command
        sql = "UPDATE Preferences SET languageDefault = @LanguageDefault " _
                                & ", activateEventNotifications = @ActivateEventNotifications " _
                                & ", activatePaymentsNotifications = @ActivatePaymentsNotifications " _
                                & ", daysNotifyEvents = @DaysNotifyEvents " _
                                & ", daysNotifyExam = @DaysNotifyExam " _
                                & ", daysNotifyTest = @DaysNotifyTest " _
                                & ", dayNotifyHoliday = @DayNotifyHoliday " _
                                & ", dayNotifyOthers = @DayNotifyOthers " _
                                & ", dayNotifyPaymets = @DayNotifyPayments " _
                                & " WHERE ID_User = @UserID "

        command = New SqlCommand(sql, connection)
        If preferences.UserID = 0 Then
            command.Parameters.AddWithValue("@UserID", DBNull.Value)
        Else
            command.Parameters.AddWithValue("@UserID", preferences.UserID)
        End If

        command.Parameters.AddWithValue("@LanguageDefault", preferences.LanguageDefault)
        command.Parameters.AddWithValue("@ActivateEventNotifications", preferences.ActivateEventNotifications)
        command.Parameters.AddWithValue("@ActivatePaymentsNotifications", preferences.ActivatePaymentsNotifications)
        command.Parameters.AddWithValue("@DaysNotifyEvents", preferences.DaysNotifyEvents)
        command.Parameters.AddWithValue("@DaysNotifyExam", preferences.DaysNotifyExam)
        command.Parameters.AddWithValue("@DaysNotifyTest", preferences.DaysNotifyTest)
        command.Parameters.AddWithValue("@DayNotifyHoliday", preferences.DaysNotifyHoliday)
        command.Parameters.AddWithValue("@DayNotifyOthers", preferences.DaysNotifyOthers)
        command.Parameters.AddWithValue("@DaysNotifyPayments", preferences.DaysNotifyPayments)

        'Get dataReader
        numReg = command.ExecuteNonQuery()

        'Disconnect from the database
        con.closeConnection(connection)

        Return numReg
    End Function
#End Region
End Class
