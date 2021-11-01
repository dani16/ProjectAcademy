Imports System.Text.RegularExpressions

Public Class clsValidator
    ''' <summary>
    ''' This method validates if a string is empty.
    ''' </summary>
    ''' <param name="sentence">String</param>
    ''' <pre>None</pre>
    ''' <pos>Returns TRUE if it is the string is not empty or FALSE if is not.</pos>
    ''' <returns>A boolean</returns>
    ''' <remarks></remarks>
    Public Function validateEmptyString(ByVal sentence As String) As Boolean
        Dim result As Boolean = True

        If sentence = "" Then
            result = False
        End If

        Return result
    End Function

    ''' <summary>
    ''' Method that validates that an username has between 4 and 15 characters
    ''' </summary>
    ''' <param name="username">String</param>
    ''' <pre>None</pre>
    ''' <pos>Returns TRUE if it is the DNI is valid or FALSE if is not.</pos>
    ''' <returns>A boolean</returns>
    ''' <remarks></remarks>
    Public Function validateUsername(ByVal username As String) As Boolean
        Dim result As Boolean = True
        Dim regex As New Regex("^[a-zA-Z\d-_]{4,15}$")

        If Not regex.IsMatch(username) Then
            result = False
        End If

        Return result
    End Function

    ''' <summary>
    ''' Method that validates that a password only has numbers or letters and has between 4 and 15 characters
    ''' </summary>
    ''' <param name="password">String</param>
    ''' <pre>None</pre>
    ''' <pos>Returns TRUE if it is the Password is valid or FALSE if is not.</pos>
    ''' <returns>A boolean</returns>
    ''' <remarks></remarks>
    Public Function validatePasswordFormat(ByVal password As String) As Boolean
        Dim result As Boolean = True
        Dim regex As New Regex("^[a-z\d-_]{4,15}$")

        If Not regex.IsMatch(password) Then
            result = False
        End If

        Return result
    End Function

    ''' <summary>
    ''' Method that validates that two passwords math
    ''' </summary>
    ''' <param name="firstPassword">String</param>
    ''' <param name="secondPassword">String</param>
    ''' <pre>None</pre>
    ''' <pos>Returns TRUE if it is the Password is valid or FALSE if is not.</pos>
    ''' <returns>A boolean</returns>
    ''' <remarks></remarks>
    Public Function validatePasswordRepeat(ByVal firstPassword As String, ByVal secondPassword As String) As Boolean
        Dim result As Boolean = True

        If Not firstPassword.Equals(secondPassword) Then
            result = False
        End If

        Return result
    End Function

    ''' <summary>
    ''' Method that validates a DNI and NIE Extranjeros
    ''' </summary>
    ''' <param name="DNI">String</param>
    ''' <pre>None</pre>
    ''' <pos>Returns TRUE if it is the DNI is valid or FALSE if is not.</pos>
    ''' <returns>A boolean</returns>
    ''' <remarks></remarks>
    Public Function validateDNI(ByVal DNI As String) As Boolean
        Dim result As Boolean = True
        Dim regex As New Regex("(([X-Z]{1})([-]?)(\d{7})([-]?)([A-Z]{1}))|((\d{8})([-]?)([A-Z]{1}))$")

        'Invalid DNI format
        If Not regex.IsMatch(DNI) Then
            result = False
        End If

        Return result
    End Function

    ''' <summary>
    ''' Method that validates if a DNI exists
    ''' </summary>
    ''' <param name="DNI">String</param>
    ''' <pre>None</pre>
    ''' <pos>Returns TRUE if it is the DNI exists or FALSE if is not.</pos>
    ''' <returns>A boolean</returns>
    ''' <remarks></remarks>
    Public Function validateExistDNI(ByVal DNI As String) As Boolean
        Dim result As Boolean = True

        'DNI already exists
        If Application.oPersonManager.existDNI(DNI) Then
            result = False
        End If

        Return result
    End Function

    ''' <summary>
    ''' Method that validates a telephone number
    ''' </summary>
    ''' <param name="telephone">String</param>
    ''' <pre>None</pre>
    ''' <pos>Returns TRUE if it is the Telephone is valid or FALSE if is not.</pos>
    ''' <returns>A boolean</returns>
    ''' <remarks></remarks>
    Public Function validateTelephone(ByVal telephone As String) As Boolean
        Dim result As Boolean = True
        Dim regex As New Regex("[679]{1}[0-9]{8}$")

        If Not regex.IsMatch(telephone) Then
            result = False
        End If

        Return result
    End Function

    ''' <summary>
    ''' Method that validates an email
    ''' </summary>
    ''' <param name="email">String</param>
    ''' <pre>None</pre>
    ''' <pos>Returns TRUE if it is the Email is valid or FALSE if is not.</pos>
    ''' <returns>A boolean</returns>
    ''' <remarks></remarks>
    Public Function validateEmail(ByVal email As String) As Boolean
        Dim result As Boolean = True
        Dim regex As New Regex("^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,3})$")

        If Not regex.IsMatch(email) Then
            result = False
        End If

        Return result
    End Function

    ''' <summary>
    ''' Method that validates a decimal number
    ''' </summary>
    ''' <param name="email">String</param>
    ''' <pre>None</pre>
    ''' <pos>Returns TRUE if it is a decimal or FALSE if is not.</pos>
    ''' <returns>A boolean</returns>
    ''' <remarks></remarks>
    Public Function validateDecimal(ByVal email As String) As Boolean
        Dim result As Boolean = True
        Dim regex As New Regex("^[_a-z0-9-]+(\.[_a-z0-9-]+)*@[a-z0-9-]+(\.[a-z0-9-]+)*(\.[a-z]{2,3})$")

        If Not regex.IsMatch(email) Then
            result = False
        End If

        Return result
    End Function
End Class
