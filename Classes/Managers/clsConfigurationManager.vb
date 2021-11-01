Imports System.Data.SqlClient

Public Class clsConfigurationManager
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
    ''' Method that gets the configuration values from the database
    ''' </summary>
    ''' <pre>None</pre>
    ''' <returns>An object clsConfiguration</returns>
    ''' <pos>Returns the </pos>
    ''' <remarks></remarks>
    Public Function getConfiguration() As clsConfiguration
        Dim configuration As New clsConfiguration
        Dim connection As New SqlConnection
        Dim dataReader As SqlDataReader
        Dim sql As String
        Dim command As New SqlCommand

        Try
            'Connect to the dataBase
            connection = con.getConnection

            'Create object Command
            sql = "SELECT * FROM Configuration"
            command = New SqlCommand(sql, connection)

            ''Get DataReader
            dataReader = command.ExecuteReader()

            'Get Configuration
            If dataReader.HasRows Then
                dataReader.Read()
                With configuration
                    'General
                    .ShowHome = dataReader("showHome")
                    .ShowStudents = dataReader("showStudents")
                    .ShowGroups = dataReader("showGroups")
                    .ShowTeachers = dataReader("showTeachers")
                    .ShowAssessment = dataReader("showAssessment")
                    .ShowMarks = dataReader("showMarks")
                    .ShowCalendar = dataReader("showCalendar")
                    .ShowTimetable = dataReader("showTimetable")
                    .ShowPayments = dataReader("showPayments")

                    'Permissions
                    .AllowNewStudents = dataReader("allowNewStudents")
                    .AllowEditStudents = dataReader("allowEditStudents")
                    .AllowDeleteStudents = dataReader("allowDeleteStudents")
                    .AllowNewGroups = dataReader("allowNewGroups")
                    .AllowEditGroups = dataReader("allowEditGroups")
                    .AllowFinishGroups = dataReader("allowFinishGroups")
                    .AllowNewTeachers = dataReader("allowNewTeachers")
                    .AllowEditTeachers = dataReader("allowEditTeachers")
                    .AllowDeleteTeachers = dataReader("allowDeleteTeachers")
                    .AllowNewAssessment = dataReader("allowNewAssessment")
                    .AllowEditAssessment = dataReader("allowEditAssessment")
                    .AllowDeleteAssessment = dataReader("allowDeleteAssessment")
                    .AllowNewMarks = dataReader("allowNewMarks")
                    .AllowEditMarks = dataReader("allowEditMarks")
                    .AllowDeleteMarks = dataReader("allowDeleteMarks")
                    .AllowNewCalendar = dataReader("allowNewCalendarEvent")
                    .AllowEditCalendar = dataReader("allowEditCalendarEvent")
                    .AllowDeleteCalendar = dataReader("allowDeleteCalendarEvent")
                    .AllowEditTimetable = dataReader("allowEditTimetable")
                    .AllowEditPayments = dataReader("allowEditPayments")

                    'Others
                    .AllowEditPersonalData = dataReader("allowEditPersonalData")
                    .AllowChangePassword = dataReader("allowChangePassword")
                End With
            Else
                configuration = Nothing
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

        Return configuration
    End Function

    ''' <summary>
    ''' Method that updates the application Configuration
    ''' </summary>
    ''' <param name="configuration">An Object Configuration</param>
    ''' <returns>Returns an integer with the number of rows afected.</returns>
    ''' <remarks></remarks>
    Public Function saveConfigurationChanges(ByVal configuration As clsConfiguration) As Integer
        Dim connection As New SqlConnection
        Dim numReg As Integer
        Dim sql As String
        Dim command As New SqlCommand

        'Conect to the dataBase
        connection = con.getConnection

        'Create object Command
        sql = "UPDATE Configuration SET showHome = @ShowHome " _
                                & ", showStudents = @ShowStudents " _
                                & ", showGroups = @ShowGroups " _
                                & ", showTeachers = @ShowTeachers " _
                                & ", showAssessment = @ShowAssessment " _
                                & ", showMarks = @ShowMarks " _
                                & ", showCalendar = @ShowCalendar " _
                                & ", showTimetable = @ShowTimetable " _
                                & ", showPayments = @ShowPayments " _
                                & ", allowNewStudents = @AllowNewStudents " _
                                & ", allowEditStudents = @AllowEditStudents " _
                                & ", allowDeleteStudents = @AllowDeleteStudents " _
                                & ", allowNewGroups = @AllowNewGroups " _
                                & ", allowEditGroups = @AllowEditGroups " _
                                & ", allowFinishGroups = @AllowFinishGroups " _
                                & ", allowNewTeachers = @AllowNewTeachers " _
                                & ", allowEditTeachers = @AllowEditTeachers " _
                                & ", allowDeleteTeachers = @AllowDeleteTeachers " _
                                & ", allowNewAssessment = @AllowNewAssessment " _
                                & ", allowEditAssessment = @AllowEditAssessment " _
                                & ", allowDeleteAssessment = @AllowDeleteAssessment " _
                                & ", allowNewMarks = @AllowNewMarks " _
                                & ", allowEditMarks = @AllowEditMarks " _
                                & ", allowDeleteMarks = @AllowDeleteMarks " _
                                & ", allowNewCalendarEvent = @AllowNewCalendarEvent " _
                                & ", allowEditCalendarEvent = @AllowEditCalendarEvent " _
                                & ", allowDeleteCalendarEvent = @AllowDeleteCalendarEvent " _
                                & ", allowEditTimetable = @AllowEditTimetable " _
                                & ", allowEditPayments = @AllowEditPayments " _
                                & ", allowEditPersonalData = @AllowEditPersonalData " _
                                & ", allowChangePassword = @AllowChangePassword " _
                                & " WHERE ID_Configuration = @ID "

        command = New SqlCommand(sql, connection)
        command.Parameters.Add("@ShowHome", System.Data.SqlDbType.Bit)
        command.Parameters("@ShowHome").Value = configuration.ShowHome

        command.Parameters.Add("@ShowStudents", System.Data.SqlDbType.Bit)
        command.Parameters("@ShowStudents").Value = configuration.ShowStudents

        command.Parameters.Add("@ShowGroups", System.Data.SqlDbType.Bit)
        command.Parameters("@ShowGroups").Value = configuration.ShowGroups

        command.Parameters.Add("@ShowTeachers", System.Data.SqlDbType.Bit)
        command.Parameters("@ShowTeachers").Value = configuration.ShowTeachers

        command.Parameters.Add("@ShowAssessment", System.Data.SqlDbType.Bit)
        command.Parameters("@ShowAssessment").Value = configuration.ShowAssessment

        command.Parameters.Add("@ShowMarks", System.Data.SqlDbType.Bit)
        command.Parameters("@ShowMarks").Value = configuration.ShowMarks

        command.Parameters.Add("@ShowCalendar", System.Data.SqlDbType.Bit)
        command.Parameters("@ShowCalendar").Value = configuration.ShowCalendar

        command.Parameters.Add("@ShowTimetable", System.Data.SqlDbType.Bit)
        command.Parameters("@ShowTimetable").Value = configuration.ShowTimetable

        command.Parameters.Add("@ShowPayments", System.Data.SqlDbType.Bit)
        command.Parameters("@ShowPayments").Value = configuration.ShowPayments

        command.Parameters.Add("@AllowNewStudents", System.Data.SqlDbType.Bit)
        command.Parameters("@AllowNewStudents").Value = configuration.AllowNewStudents

        command.Parameters.Add("@AllowEditStudents", System.Data.SqlDbType.Bit)
        command.Parameters("@AllowEditStudents").Value = configuration.AllowEditStudents

        command.Parameters.Add("@AllowDeleteStudents", System.Data.SqlDbType.Bit)
        command.Parameters("@AllowDeleteStudents").Value = configuration.AllowDeleteStudents

        command.Parameters.Add("@AllowNewGroups", System.Data.SqlDbType.Bit)
        command.Parameters("@AllowNewGroups").Value = configuration.AllowNewGroups

        command.Parameters.Add("@AllowEditGroups", System.Data.SqlDbType.Bit)
        command.Parameters("@AllowEditGroups").Value = configuration.AllowEditGroups

        command.Parameters.Add("@AllowFinishGroups", System.Data.SqlDbType.Bit)
        command.Parameters("@AllowFinishGroups").Value = configuration.AllowFinishGroups

        command.Parameters.Add("@AllowNewTeachers", System.Data.SqlDbType.Bit)
        command.Parameters("@AllowNewTeachers").Value = configuration.AllowNewTeachers

        command.Parameters.Add("@AllowEditTeachers", System.Data.SqlDbType.Bit)
        command.Parameters("@AllowEditTeachers").Value = configuration.AllowEditTeachers

        command.Parameters.Add("@AllowDeleteTeachers", System.Data.SqlDbType.Bit)
        command.Parameters("@AllowDeleteTeachers").Value = configuration.AllowDeleteTeachers

        command.Parameters.Add("@AllowNewAssessment", System.Data.SqlDbType.Bit)
        command.Parameters("@AllowNewAssessment").Value = configuration.AllowNewAssessment

        command.Parameters.Add("@AllowEditAssessment", System.Data.SqlDbType.Bit)
        command.Parameters("@AllowEditAssessment").Value = configuration.AllowEditAssessment

        command.Parameters.Add("@AllowDeleteAssessment", System.Data.SqlDbType.Bit)
        command.Parameters("@AllowDeleteAssessment").Value = configuration.AllowDeleteAssessment

        command.Parameters.Add("@AllowNewMarks", System.Data.SqlDbType.Bit)
        command.Parameters("@AllowNewMarks").Value = configuration.AllowNewMarks

        command.Parameters.Add("@AllowEditMarks", System.Data.SqlDbType.Bit)
        command.Parameters("@AllowEditMarks").Value = configuration.AllowEditMarks

        command.Parameters.Add("@AllowDeleteMarks", System.Data.SqlDbType.Bit)
        command.Parameters("@AllowDeleteMarks").Value = configuration.AllowDeleteMarks

        command.Parameters.Add("@AllowNewCalendarEvent", System.Data.SqlDbType.Bit)
        command.Parameters("@AllowNewCalendarEvent").Value = configuration.AllowNewCalendar

        command.Parameters.Add("@AllowEditCalendarEvent", System.Data.SqlDbType.Bit)
        command.Parameters("@AllowEditCalendarEvent").Value = configuration.AllowEditCalendar

        command.Parameters.Add("@AllowDeleteCalendarEvent", System.Data.SqlDbType.Bit)
        command.Parameters("@AllowDeleteCalendarEvent").Value = configuration.AllowDeleteCalendar

        command.Parameters.Add("@AllowEditTimetable", System.Data.SqlDbType.Bit)
        command.Parameters("@AllowEditTimetable").Value = configuration.AllowEditTimetable

        command.Parameters.Add("@AllowEditPayments", System.Data.SqlDbType.Bit)
        command.Parameters("@AllowEditPayments").Value = configuration.AllowEditPayments

        command.Parameters.Add("@AllowEditPersonalData", System.Data.SqlDbType.Bit)
        command.Parameters("@AllowEditPersonalData").Value = configuration.AllowEditPersonalData

        command.Parameters.Add("@AllowChangePassword", System.Data.SqlDbType.Bit)
        command.Parameters("@AllowChangePassword").Value = configuration.AllowChangePassword

        command.Parameters.Add("@ID", System.Data.SqlDbType.Bit)
        command.Parameters("@ID").Value = 1

        'Get dataReader
        numReg = command.ExecuteNonQuery()

        'Disconnect from the database
        con.closeConnection(connection)

        Return numReg
    End Function
#End Region
End Class
