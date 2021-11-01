Imports System.Data.SqlClient

Public Class clsInscriptionManager

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
    ''' Method that checks if a person bolongs to a group
    ''' </summary>
    ''' <param name="personID">An Integer</param>
    ''' <param name="groupID">An Integer</param>
    ''' <returns>Return TRUE if the person belongs to the group or FALSE if is not.</returns>
    ''' <remarks></remarks>
    Public Function isPersonOnGroup(ByVal personID As Integer, ByVal groupID As Integer) As Boolean
        Dim connection As New SqlConnection
        Dim dataReader As SqlDataReader
        Dim sql As String
        Dim command As New SqlCommand
        Dim result As Boolean = False

        Try
            'Conect to the dataBase
            connection = con.getConnection

            'Create object Command
            sql = " SELECT * FROM Inscription AS I " & _
                    " JOIN Student AS S ON I.ID_Student = S.ID_Student " & _
                    " WHERE S.ID_Person = @PersonID AND I.ID_Group = @GroupID "

            command = New SqlCommand(sql, connection)
            If personID = 0 Then
                command.Parameters.AddWithValue("@PersonID", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@PersonID", personID)
            End If
            If groupID = 0 Then
                command.Parameters.AddWithValue("@GroupID", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@GroupID", groupID)
            End If

            'Get DataReader
            dataReader = command.ExecuteReader()

            'Get Groups
            If dataReader.HasRows() Then
                result = True
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
    ''' Method that inserts a new Student on a Group into the database
    ''' </summary>
    ''' <param name="studentID">An Integer</param>
    ''' <param name="groupID">An Integer</param>
    ''' <returns>Returns an integer with the number of rows afected.</returns>
    ''' <remarks></remarks>
    Public Function studentInscription(ByVal studentID As Integer, ByVal groupID As Integer) As Integer
        Dim connection As New SqlConnection
        Dim sql As String
        Dim command As New SqlCommand
        Dim numReg As Integer
        Dim group As New clsGroup

        Try
            'Connect to the database
            connection = con.getConnection

            'Insert User
            sql = "INSERT INTO Inscription (ID_Student,ID_Group,dateInscription) " _
                & "VALUES(@StudentID,@GroupID,@DateInscription)"

            command = New SqlCommand(sql, connection)

            group = Application.oGroupManager.getGroup(groupID)
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

            Dim dateInscription As Date = Date.Today
            command.Parameters.AddWithValue("@DateInscription", dateInscription)

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
    ''' Method that removes a Student from a Group
    ''' </summary>
    ''' <param name="studentID">An Integer</param>
    ''' <param name="groupID">An Integer</param>
    ''' <returns>Returns an integer with the number of rows afected.</returns>
    ''' <remarks></remarks>
    Public Function removeStudent(ByVal studentID As Integer, ByVal groupID As Integer) As Integer
        Dim connection As New SqlConnection
        Dim sql As String
        Dim command As New SqlCommand
        Dim numReg As Integer
        Try
            'Connect to the database
            connection = con.getConnection

            'Insert User
            sql = "DELETE FROM Inscription WHERE ID_Student = @StudentID AND ID_Group = @GroupID "

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
    ''' Method that checks if an Student has already an Inscription in a group
    ''' </summary>
    ''' <param name="studentID">An Integer</param>
    ''' <param name="groupID">An Integer</param>
    ''' <returns>Returns TRUE if the student is already inscripted in the group or FALSE if is not.</returns>
    ''' <remarks></remarks>
    Public Function checkStudentInscription(ByVal studentID As Integer, ByVal groupID As Integer) As Boolean
        Dim connection As New SqlConnection
        Dim dataReader As SqlDataReader
        Dim sql As String
        Dim command As New SqlCommand
        Dim result As Boolean = False
        Try
            'Connect to the database
            connection = con.getConnection

            'Insert User
            sql = "SELECT * FROM Inscription WHERE ID_Student = @StudentID AND ID_Group = @GroupID "

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

            'Execute 
            dataReader = command.ExecuteReader()

            'Get Inscription
            If dataReader.HasRows Then
                dataReader.Read()
                result = True
            End If

            ''Close dataReader
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
#End Region
End Class
