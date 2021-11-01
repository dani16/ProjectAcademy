Public Class clsTimetableClassTextConverter
    Implements System.Windows.Data.IValueConverter

    'CLASS clsTimetableClassItemConverter

    '   This class contains the necessary methods to bind the Group information with
    '   the property Text from the TextBlock of the control ctrlTimetableClass.

    ''' <summary>
    ''' Method that converts the values of Text property from the textBlocks from the control ctrlTimetableClass with an object clsTimetable.
    ''' </summary>
    ''' <param name="value">An Object</param>
    ''' <param name="targetType">An object Type</param>
    ''' <param name="parameter">An Object</param>
    ''' <param name="culture">An object Globalization.CultureInfo</param>
    ''' <returns>Returns a String with the information we are asking for.</returns>
    ''' <remarks></remarks>
    Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As Globalization.CultureInfo) As Object Implements IValueConverter.Convert
        Dim result As String = Nothing
        Dim group As clsGroup = Application.oGroupManager.getGroup(CType(value, Integer))

        If parameter.Equals("GroupLevel") And Not group Is Nothing Then
            result = group.EnglishLevel
        Else
            If parameter.Equals("GroupName") And Not group Is Nothing Then
                result = group.Description
            ElseIf parameter.Equals("TeacherName") And Not group Is Nothing Then
                Dim teacher As clsTeacher = Application.oTeacherManager.getTeacher(group.TeacherID)
                If Not teacher Is Nothing Then
                    result = "(" & teacher.Name & " " & teacher.Surname & ")"
                End If
            End If
        End If

        Return result
    End Function

    ''' <summary>
    ''' Not implemented method.
    ''' </summary>
    ''' <param name="value">An Object</param>
    ''' <param name="targetType">An object Type</param>
    ''' <param name="parameter">An Object</param>
    ''' <param name="culture">An object Globalization.CultureInfo</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function ConvertBack(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
        Throw New NotImplementedException()
    End Function
End Class

