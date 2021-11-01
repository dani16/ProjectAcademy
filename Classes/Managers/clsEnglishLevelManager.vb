Imports System.Data.SqlClient

Public Class clsEnglishLevelManager
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
    ''' Method that returns an EnglishLevel from the database
    ''' </summary>
    ''' <pre>None</pre>
    ''' <param name="level">A String</param>
    ''' <returns>An object clsEnglishLevel</returns>
    ''' <pos>Returns the user.</pos>
    ''' <remarks></remarks>
    Public Function getEnglishLevel(level As String) As clsEnglishLevel
        Dim connection As New SqlConnection
        Dim dataReader As SqlDataReader
        Dim sql As String
        Dim command As New SqlCommand
        Dim englishLevel As New clsEnglishLevel

        Try
            'Conect to the dataBase
            connection = con.getConnection

            'Create object Command
            sql = "SELECT * FROM EnglishLevel " & _
                " WHERE englishLevel LIKE @EnglishLevel "
            command = New SqlCommand(sql, connection)
            If level = "" Then
                command.Parameters.AddWithValue("@EnglishLevel", DBNull.Value)
            Else
                command.Parameters.AddWithValue("@EnglishLevel", level)
            End If

            'Get DataReader
            dataReader = command.ExecuteReader()

            'Get Student
            If dataReader.HasRows Then
                dataReader.Read()
                With englishLevel
                    If Not IsDBNull(dataReader("englishLevel")) Then
                        .EnglishLevel = dataReader("englishLevel")
                    Else
                        .EnglishLevel = Nothing
                    End If

                    If Not IsDBNull(dataReader("[description]")) Then
                        .Description = dataReader("[description]")
                    Else
                        .Description = Nothing
                    End If
                End With
            Else
                englishLevel = Nothing
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

        Return englishLevel
    End Function


    ''' <summary>
    ''' Method that return all the EnglishLevel from the database.
    ''' </summary>
    ''' <returns>A list of clsEnglishLevels</returns>
    ''' <remarks></remarks>
    Public Function getEnglishLevels() As List(Of clsEnglishLevel)
        Dim listEnglishLevel As New List(Of clsEnglishLevel)
        Dim connection As New SqlConnection
        Dim dataReader As SqlDataReader
        Dim sql As String
        Dim command As New SqlCommand

        Try
            'Conect to the dataBase
            connection = con.getConnection

            'Create object Command
            sql = "SELECT * FROM EnglishLevel "

            command = New SqlCommand(sql, connection)

            'Get DataReader
            dataReader = command.ExecuteReader()

            'Get Product
            While dataReader.Read()
                Dim englishLevel As New clsEnglishLevel

                With englishLevel
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
                End With

                listEnglishLevel.Add(englishLevel)
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

        Return listEnglishLevel
    End Function
#End Region
End Class
