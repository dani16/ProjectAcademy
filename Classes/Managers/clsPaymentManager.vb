Imports System.Data.SqlClient
Imports System.Data

Public Class clsPaymentManager

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
    ''' Method that returns a list with all the payment of 
    ''' a Student from the database
    ''' </summary>
    ''' <pre>None</pre>
    ''' <param name="studentID">An Integer</param>
    ''' <returns>An object clsStudent</returns>
    ''' <pos>Returns the user.</pos>
    ''' <remarks></remarks>
    Public Function getStudentPayment(ByVal studentID As Integer) As List(Of clsPayment)
        Dim connection As New SqlConnection
        Dim dataReader As SqlDataReader
        Dim sql As String
        Dim command As New SqlCommand
        Dim listPayments As New List(Of clsPayment)

        Try
            'Conect to the dataBase
            connection = con.getConnection

            'Create object Command
            sql = "SELECT * FROM Payment " & _
                    " WHERE ID_Student = @StudentID " & _
                    " ORDER BY datePayment DESC  "
            command = New SqlCommand(sql, connection)
            If studentID = 0 Then
                command.Parameters.AddWithValue("@StudentID", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@StudentID", studentID)
            End If

            'Get DataReader
            dataReader = command.ExecuteReader()

            'Get Payments
            While dataReader.Read()
                Dim payment As New clsPayment
                With payment
                    If Not IsDBNull(dataReader("ID_Student")) Then
                        .StudentID = dataReader("ID_Student")
                    Else
                        .StudentID = Nothing
                    End If

                    If Not IsDBNull(dataReader("name")) And Not IsDBNull(dataReader("surname")) Then
                        .Name = dataReader("name") & " " & dataReader("surname")
                    Else
                        .Name = Nothing
                    End If

                    If Not IsDBNull(dataReader("DNI")) Then
                        .DNI = dataReader("DNI")
                    Else
                        .DNI = Nothing
                    End If

                    If Not IsDBNull(dataReader("ID_Group")) Then
                        .GroupID = dataReader("ID_Group")
                    Else
                        .GroupID = Nothing
                    End If

                    If Not IsDBNull(dataReader("description")) Then
                        .GroupName = dataReader("description")
                    Else
                        .GroupName = Nothing
                    End If

                    If Not IsDBNull(dataReader("datePayment")) Then
                        .DatePayment = dataReader("datePayment")
                    Else
                        .DatePayment = Nothing
                    End If

                    If Not IsDBNull(dataReader("paymentType")) Then
                        .PaymentType = dataReader("paymentType")
                    Else
                        .PaymentType = Nothing
                    End If

                    .PaymentDescription = payment.paymentTypeToString()

                    If Not IsDBNull(dataReader("amount")) Then
                        .Amount = dataReader("amount")
                    Else
                        .Amount = Nothing
                    End If

                    If Not IsDBNull(dataReader("status")) Then
                        .Status = dataReader("status")
                    Else
                        .Status = Nothing
                    End If
                End With

                listPayments.Add(payment)
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

        Return listPayments
    End Function

    ''' <summary>
    ''' Method that returns a list with all the Payments from the database
    ''' </summary>
    ''' <returns>A list of Student</returns>
    ''' <remarks></remarks>
    Public Function getAllPayments() As List(Of clsPayment)
        Dim listPayments As New List(Of clsPayment)
        Dim connection As New SqlConnection
        Dim dataReader As SqlDataReader
        Dim sql As String
        Dim command As New SqlCommand

        Try
            'Conect to the dataBase
            connection = con.getConnection

            'Create object Command
            sql = "SELECT * FROM V_Payments ORDER BY datePayment DESC "

            command = New SqlCommand(sql, connection)

            'Get DataReader
            dataReader = command.ExecuteReader()

            'Get Payments
            While dataReader.Read()
                Dim payment As New clsPayment
                With payment
                    If Not IsDBNull(dataReader("ID_Student")) Then
                        .StudentID = dataReader("ID_Student")
                    Else
                        .StudentID = Nothing
                    End If

                    If Not IsDBNull(dataReader("name")) And Not IsDBNull(dataReader("surname")) Then
                        .Name = dataReader("name") & " " & dataReader("surname")
                    Else
                        .Name = Nothing
                    End If

                    If Not IsDBNull(dataReader("DNI")) Then
                        .DNI = dataReader("DNI")
                    Else
                        .DNI = Nothing
                    End If

                    If Not IsDBNull(dataReader("ID_Group")) Then
                        .GroupID = dataReader("ID_Group")
                    Else
                        .GroupID = Nothing
                    End If

                    If Not IsDBNull(dataReader("description")) Then
                        .GroupName = dataReader("description")
                    Else
                        .GroupName = Nothing
                    End If

                    If Not IsDBNull(dataReader("datePayment")) Then
                        .DatePayment = dataReader("datePayment")
                    Else
                        .DatePayment = Nothing
                    End If

                    If Not IsDBNull(dataReader("paymentType")) Then
                        .PaymentType = dataReader("paymentType")
                    Else
                        .PaymentType = Nothing
                    End If

                    .PaymentDescription = payment.paymentTypeToString()

                    If Not IsDBNull(dataReader("amount")) Then
                        .Amount = dataReader("amount")
                    Else
                        .Amount = Nothing
                    End If

                    If Not IsDBNull(dataReader("status")) Then
                        .Status = dataReader("status")
                    Else
                        .Status = Nothing
                    End If
                End With

                listPayments.Add(payment)
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

        Return listPayments
    End Function

    ''' <summary>
    ''' Method that returns a list with all the Payments on a specific date from the database
    ''' </summary>
    ''' <param name="year">An Integer</param>
    ''' <param name="month">An Integer</param>
    ''' <returns>A list of Student</returns>
    ''' <remarks></remarks>
    Public Function getAllPayments(ByVal year As Integer, ByVal month As Integer) As List(Of clsPayment)
        Dim listPayments As New List(Of clsPayment)
        Dim connection As New SqlConnection
        Dim dataReader As SqlDataReader
        Dim sql As String = Nothing
        Dim command As New SqlCommand

        Try
            'Conect to the dataBase
            connection = con.getConnection

            If Not year = 0 And Not month = 0 Then
                'Create object Command
                sql = "SELECT * FROM V_Payments " & _
                        " WHERE YEAR(datePayment) AND MONTH(datePayment) " & _
                        " ORDER BY datePayment DESC "
            ElseIf Not year = 0 And month = 0 Then
                sql = "SELECT * FROM V_Payments " & _
                        " WHERE YEAR(datePayment) " & _
                        " ORDER BY datePayment DESC "
            ElseIf year = 0 And Not month = 0 Then
                sql = "SELECT * FROM V_Payments " & _
                        " WHERE MONTH(datePayment) " & _
                        " ORDER BY datePayment DESC "
            End If

            command = New SqlCommand(sql, connection)

            'Get DataReader
            dataReader = command.ExecuteReader()

            'Get Payments
            While dataReader.Read()
                Dim payment As New clsPayment
                With payment
                    If Not IsDBNull(dataReader("ID_Student")) Then
                        .StudentID = dataReader("ID_Student")
                    Else
                        .StudentID = Nothing
                    End If

                    If Not IsDBNull(dataReader("name")) And Not IsDBNull(dataReader("surname")) Then
                        .Name = dataReader("name") & " " & dataReader("surname")
                    Else
                        .Name = Nothing
                    End If

                    If Not IsDBNull(dataReader("DNI")) Then
                        .DNI = dataReader("DNI")
                    Else
                        .DNI = Nothing
                    End If

                    If Not IsDBNull(dataReader("ID_Group")) Then
                        .GroupID = dataReader("ID_Group")
                    Else
                        .GroupID = Nothing
                    End If

                    If Not IsDBNull(dataReader("description")) Then
                        .GroupName = dataReader("description")
                    Else
                        .GroupName = Nothing
                    End If

                    If Not IsDBNull(dataReader("datePayment")) Then
                        .DatePayment = dataReader("datePayment")
                    Else
                        .DatePayment = Nothing
                    End If

                    If Not IsDBNull(dataReader("paymentType")) Then
                        .PaymentType = dataReader("paymentType")
                    Else
                        .PaymentType = Nothing
                    End If

                    .PaymentDescription = payment.paymentTypeToString()

                    If Not IsDBNull(dataReader("amount")) Then
                        .Amount = dataReader("amount")
                    Else
                        .Amount = Nothing
                    End If

                    If Not IsDBNull(dataReader("status")) Then
                        .Status = dataReader("status")
                    Else
                        .Status = Nothing
                    End If
                End With

                listPayments.Add(payment)
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

        Return listPayments
    End Function

    ''' <summary>
    ''' Method that returns a list with all the Payments that match the search from the database
    ''' </summary>
    ''' <param name="studentID">An Integer</param>
    ''' <param name="year">An Integer</param>
    ''' <param name="month">An Integer</param>
    ''' <param name="status">A Boolean</param>
    ''' <returns>A list of Student</returns>
    ''' <remarks></remarks>
    Public Function getAllPayments(ByVal studentID As Integer, ByVal year As Integer, ByVal month As Integer, ByVal status As Boolean) As List(Of clsPayment)
        Dim listPayments As New List(Of clsPayment)
        Dim connection As New SqlConnection
        Dim dataReader As SqlDataReader
        Dim sql As String = Nothing
        Dim command As New SqlCommand

        Try
            'Connect to the dataBase
            connection = con.getConnection

            sql = "SELECT * FROM V_Payments WHERE "

            If status = True Then
                sql &= " [status] = 0 "
            Else
                sql &= " ([status] = 0 OR [status] = 1) "
            End If

            If Not studentID = 0 Or Not year = 0 Or Not month = 0 Then
                If Not studentID = 0 Then
                    sql &= " AND ID_Student = @StudentID "
                End If

                If Not year = 0 And Not month = 0 Then
                    sql &= " AND YEAR(datePayment) = @Year AND MONTH(datePayment) = @Month "
                ElseIf Not year = 0 And month = 0 Then
                    sql &= " AND YEAR(datePayment) = @Year "
                ElseIf year = 0 And Not month = 0 Then
                    sql &= " AND MONTH(datePayment) = @Month "
                End If
            End If

            sql &= " ORDER BY datePayment DESC "

            command = New SqlCommand(sql, connection)
            command.Parameters.AddWithValue("@StudentID", studentID)
            command.Parameters.AddWithValue("@Year", year)
            command.Parameters.AddWithValue("@Month", month)

            'Get DataReader
            dataReader = command.ExecuteReader()

            'Get Payments
            While dataReader.Read()
                Dim payment As New clsPayment
                With payment
                    If Not IsDBNull(dataReader("ID_Student")) Then
                        .StudentID = dataReader("ID_Student")
                    Else
                        .StudentID = Nothing
                    End If

                    If Not IsDBNull(dataReader("name")) And Not IsDBNull(dataReader("surname")) Then
                        .Name = dataReader("name") & " " & dataReader("surname")
                    Else
                        .Name = Nothing
                    End If

                    If Not IsDBNull(dataReader("DNI")) Then
                        .DNI = dataReader("DNI")
                    Else
                        .DNI = Nothing
                    End If

                    If Not IsDBNull(dataReader("ID_Group")) Then
                        .GroupID = dataReader("ID_Group")
                    Else
                        .GroupID = Nothing
                    End If

                    If Not IsDBNull(dataReader("description")) Then
                        .GroupName = dataReader("description")
                    Else
                        .GroupName = Nothing
                    End If

                    If Not IsDBNull(dataReader("datePayment")) Then
                        .DatePayment = dataReader("datePayment")
                    Else
                        .DatePayment = Nothing
                    End If

                    If Not IsDBNull(dataReader("paymentType")) Then
                        .PaymentType = dataReader("paymentType")
                    Else
                        .PaymentType = Nothing
                    End If

                    .PaymentDescription = payment.paymentTypeToString()

                    If Not IsDBNull(dataReader("amount")) Then
                        .Amount = dataReader("amount")
                    Else
                        .Amount = Nothing
                    End If

                    If Not IsDBNull(dataReader("status")) Then
                        .Status = dataReader("status")
                    Else
                        .Status = Nothing
                    End If
                End With

                listPayments.Add(payment)
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

        Return listPayments
    End Function

    ''' <summary>
    ''' Method that returns a list with all the Payments of a student from the database
    ''' </summary>
    ''' <param name="studentID">An Integer</param>
    ''' <returns>A list of Student</returns>
    ''' <remarks></remarks>
    Public Function getAllPayments(ByVal studentID As Integer) As List(Of clsPayment)
        Dim listPayments As New List(Of clsPayment)
        Dim connection As New SqlConnection
        Dim dataReader As SqlDataReader
        Dim sql As String
        Dim command As New SqlCommand

        Try
            'Conect to the dataBase
            connection = con.getConnection

            'Create object Command
            sql = "SELECT * FROM V_Payments " & _
                    " WHERE ID_Student = @Student_ID " & _
                    " ORDER BY datePayment DESC "

            command = New SqlCommand(sql, connection)
            If studentID = 0 Then
                command.Parameters.AddWithValue("@StudentID", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@StudentID", studentID)
            End If

            'Get DataReader
            dataReader = command.ExecuteReader()

            'Get Payments
            While dataReader.Read()
                Dim payment As New clsPayment
                With payment
                    If Not IsDBNull(dataReader("ID_Student")) Then
                        .StudentID = dataReader("ID_Student")
                    Else
                        .StudentID = Nothing
                    End If

                    If Not IsDBNull(dataReader("name")) And Not IsDBNull(dataReader("surname")) Then
                        .Name = dataReader("name") & " " & dataReader("surname")
                    Else
                        .Name = Nothing
                    End If

                    If Not IsDBNull(dataReader("DNI")) Then
                        .DNI = dataReader("DNI")
                    Else
                        .DNI = Nothing
                    End If

                    If Not IsDBNull(dataReader("ID_Group")) Then
                        .GroupID = dataReader("ID_Group")
                    Else
                        .GroupID = Nothing
                    End If

                    If Not IsDBNull(dataReader("description")) Then
                        .GroupName = dataReader("description")
                    Else
                        .GroupName = Nothing
                    End If

                    If Not IsDBNull(dataReader("datePayment")) Then
                        .DatePayment = dataReader("datePayment")
                    Else
                        .DatePayment = Nothing
                    End If

                    If Not IsDBNull(dataReader("paymentType")) Then
                        .PaymentType = dataReader("paymentType")
                    Else
                        .PaymentType = Nothing
                    End If

                    .PaymentDescription = payment.paymentTypeToString()

                    If Not IsDBNull(dataReader("amount")) Then
                        .Amount = dataReader("amount")
                    Else
                        .Amount = Nothing
                    End If

                    If Not IsDBNull(dataReader("status")) Then
                        .Status = dataReader("status")
                    Else
                        .Status = Nothing
                    End If
                End With

                listPayments.Add(payment)
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

        Return listPayments
    End Function

    ''' <summary>
    ''' Method that returns a list with all the unpaid Payments from the database
    ''' </summary>
    ''' <returns>A list of Student</returns>
    ''' <remarks></remarks>
    Public Function getAllUnpaidPayments() As List(Of clsPayment)
        Dim listPayments As New List(Of clsPayment)
        Dim connection As New SqlConnection
        Dim dataReader As SqlDataReader
        Dim sql As String
        Dim command As New SqlCommand

        Try
            'Conect to the dataBase
            connection = con.getConnection

            'Create object Command
            sql = "SELECT * FROM V_Payments " & _
                    " WHERE [status] = 0 " & _
                    " ORDER BY datePayment DESC "

            command = New SqlCommand(sql, connection)

            'Get DataReader
            dataReader = command.ExecuteReader()

            'Get Payments
            While dataReader.Read()
                Dim payment As New clsPayment
                With payment
                    If Not IsDBNull(dataReader("ID_Student")) Then
                        .StudentID = dataReader("ID_Student")
                    Else
                        .StudentID = Nothing
                    End If

                    If Not IsDBNull(dataReader("name")) And Not IsDBNull(dataReader("surname")) Then
                        .Name = dataReader("name") & " " & dataReader("surname")
                    Else
                        .Name = Nothing
                    End If

                    If Not IsDBNull(dataReader("DNI")) Then
                        .DNI = dataReader("DNI")
                    Else
                        .DNI = Nothing
                    End If

                    If Not IsDBNull(dataReader("ID_Group")) Then
                        .GroupID = dataReader("ID_Group")
                    Else
                        .GroupID = Nothing
                    End If

                    If Not IsDBNull(dataReader("description")) Then
                        .GroupName = dataReader("description")
                    Else
                        .GroupName = Nothing
                    End If

                    If Not IsDBNull(dataReader("datePayment")) Then
                        .DatePayment = dataReader("datePayment")
                    Else
                        .DatePayment = Nothing
                    End If

                    If Not IsDBNull(dataReader("paymentType")) Then
                        .PaymentType = dataReader("paymentType")
                    Else
                        .PaymentType = Nothing
                    End If

                    .PaymentDescription = payment.paymentTypeToString()

                    If Not IsDBNull(dataReader("amount")) Then
                        .Amount = dataReader("amount")
                    Else
                        .Amount = Nothing
                    End If

                    If Not IsDBNull(dataReader("status")) Then
                        .Status = dataReader("status")
                    Else
                        .Status = Nothing
                    End If
                End With

                listPayments.Add(payment)
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

        Return listPayments
    End Function

    ''' <summary>
    ''' Method that returns a list with all the defaulters from the database
    ''' </summary>
    ''' <returns>A list of Student</returns>
    ''' <remarks></remarks>
    Public Function getDefaulterPayments() As List(Of clsPayment)
        Dim listPayments As New List(Of clsPayment)
        Dim connection As New SqlConnection
        Dim dataReader As SqlDataReader
        Dim sql As String
        Dim command As New SqlCommand

        Try
            'Conect to the dataBase
            connection = con.getConnection

            'Create object Command
            sql = "SELECT * FROM V_Payments AS P " & _
                    " WHERE [status] = 0 AND amount > 0 AND " & _
                    " YEAR(datePayment) <= YEAR(GETDATE()) AND " & _
                    " MONTH(datePayment) <= DATEADD(MONTH,-1,GETDATE()) "

            command = New SqlCommand(sql, connection)

            'Get DataReader
            dataReader = command.ExecuteReader()

            'Get Payments
            While dataReader.Read()
                Dim payment As New clsPayment
                With payment
                    If Not IsDBNull(dataReader("ID_Student")) Then
                        .StudentID = dataReader("ID_Student")
                    Else
                        .StudentID = Nothing
                    End If

                    If Not IsDBNull(dataReader("name")) And Not IsDBNull(dataReader("surname")) Then
                        .Name = dataReader("name") & " " & dataReader("surname")
                    Else
                        .Name = Nothing
                    End If

                    If Not IsDBNull(dataReader("DNI")) Then
                        .DNI = dataReader("DNI")
                    Else
                        .DNI = Nothing
                    End If

                    If Not IsDBNull(dataReader("ID_Group")) Then
                        .GroupID = dataReader("ID_Group")
                    Else
                        .GroupID = Nothing
                    End If

                    If Not IsDBNull(dataReader("description")) Then
                        .GroupName = dataReader("description")
                    Else
                        .GroupName = Nothing
                    End If

                    If Not IsDBNull(dataReader("datePayment")) Then
                        .DatePayment = dataReader("datePayment")
                    Else
                        .DatePayment = Nothing
                    End If

                    If Not IsDBNull(dataReader("paymentType")) Then
                        .PaymentType = dataReader("paymentType")
                    Else
                        .PaymentType = Nothing
                    End If

                    .PaymentDescription = payment.paymentTypeToString()

                    If Not IsDBNull(dataReader("amount")) Then
                        .Amount = dataReader("amount")
                    Else
                        .Amount = Nothing
                    End If

                    If Not IsDBNull(dataReader("status")) Then
                        .Status = dataReader("status")
                    Else
                        .Status = Nothing
                    End If
                End With

                listPayments.Add(payment)
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

        Return listPayments
    End Function

    ''' <summary>
    ''' Method that makes a payment
    ''' </summary>
    ''' <param name="payment">An object clsPayment</param>
    ''' <returns>Returns an integer with the number of rows afected.</returns>
    ''' <remarks></remarks>
    Public Function makePayment(ByVal payment As clsPayment) As Integer
        Dim numReg As Integer
        Dim connection As New SqlConnection
        Dim sql As String
        Dim command As New SqlCommand

        Try
            'Conect to the dataBase
            connection = con.getConnection

            'Create object Command
            sql = "UPDATE Payment SET [status] = 1 " & _
                    " WHERE ID_Student = @StudentID AND " & _
                    " ID_Group = @GroupID AND " & _
                    " datePayment = @DatePayment AND " & _
                    " paymentType = @PaymentType "

            command = New SqlCommand(sql, connection)
            If payment.StudentID = 0 Then
                command.Parameters.AddWithValue("@StudentID", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@StudentID", payment.StudentID)
            End If

            If payment.GroupID = 0 Then
                command.Parameters.AddWithValue("@GroupID", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@GroupID", payment.GroupID)
            End If

            If payment.DatePayment = Nothing Then
                command.Parameters.AddWithValue("@DatePayment", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@DatePayment", payment.DatePayment)
            End If

            If payment.PaymentType = Nothing Or payment.PaymentType.Equals("") Then
                command.Parameters.AddWithValue("@PaymentType", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@PaymentType", payment.PaymentType)
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
    ''' Method that deletes the payments of a Student of a group
    ''' </summary>
    ''' <param name="studentID">An Integer</param>
    ''' <param name="groupID">An Integer</param>
    ''' <returns>Returns an integer with the number of rows afected.</returns>
    ''' <remarks></remarks>
    Public Function deleteStudentPayment(ByVal studentID As Integer, ByVal groupID As Integer) As Integer
        Dim numReg As Integer
        Dim connection As New SqlConnection
        Dim sql As String
        Dim command As New SqlCommand

        Try
            'Conect to the dataBase
            connection = con.getConnection

            'Create object Command
            sql = "DELETE FROM Payment " & _
                    " WHERE ID_Student = @StudentID AND " & _
                    " ID_Group = @GroupID "

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
    ''' Method that generates the monthly payments of all the students.
    ''' </summary>
    ''' <returns>Returns an integer with the number of rows afected.</returns>
    ''' <remarks></remarks>
    Public Function generateMonthlyPayments() As Integer
        Dim numReg As Integer
        Dim connection As New SqlConnection
        Dim command As New SqlCommand

        Try
            'Conect to the dataBase
            connection = con.getConnection

            'Create object Command
            command = New SqlCommand("generatePayments", connection)
            command.CommandType = CommandType.StoredProcedure

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
#End Region
End Class
