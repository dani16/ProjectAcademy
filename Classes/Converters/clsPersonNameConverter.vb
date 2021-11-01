Public Class clsPersonNameConverter
    Implements System.Windows.Data.IValueConverter

    'CLASS clsPersonNameConverter
    '   This class contains the necessary methods to bind the ID of a person
    '   to a name of a person.
    ''' <summary>
    ''' Method that converts that allows to get the name of a person using its ID
    ''' </summary>
    ''' <param name="value">A list of Objects</param>
    ''' <param name="targetType">An object Type</param>
    ''' <param name="parameter">An Object</param>
    ''' <param name="culture">An object Globalization.CultureInfo</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function Convert(ByVal value As Object, ByVal targetType As Type, ByVal parameter As Object, ByVal culture As Globalization.CultureInfo) As Object Implements IValueConverter.Convert
        Dim result As String
        Dim student As clsStudent = Application.oStudentManager.getStudent(value)

        result = student.Name & " " & student.Surname

        Return result
    End Function

    ''' <summary>
    ''' Not implemented method
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
