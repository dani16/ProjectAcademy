Imports Microsoft.Win32

Public Class dlgNewStudent
    ''' <summary>
    ''' Method that set the necessary elements when the window is loaded
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub dlgNewStudent_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Dim curApp As Application = Application.Current
        Dim mainWindow As Window = curApp.MainWindow

        'Set position of dialog on the center of the screen
        Me.Left = mainWindow.Left + (mainWindow.Width - Me.ActualWidth) / 2
        'Me.Top = mainWindow.Top + (mainWindow.Height - Me.ActualHeight) / 2
        Me.Top = 15
    End Sub

    ''' <summary>
    ''' Method that load the control for creating a new Student with a new Person
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub btnNewPersonStudent_Click(sender As Object, e As RoutedEventArgs) Handles btnNewPersonStudent.Click
        Dim ctrlNewPersonStudent As New ctrlNewPersonStudent
        stckPnlMenuNewStudent.Visibility = Windows.Visibility.Collapsed

        'Load Create new student control
        stckPnlCreateNewStudent.Children.Add(ctrlNewPersonStudent)
    End Sub

    ''' <summary>
    ''' Method that load the control for creating a Student with an already created Person
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub btnExistingPersonStudent_Click(sender As Object, e As RoutedEventArgs) Handles btnExistingPersonStudent.Click
        Dim ctrlExistingPersonStudent As New ctrlNewExistingPersonStudent
        stckPnlMenuNewStudent.Visibility = Windows.Visibility.Collapsed

        'Load Create new existing student control
        stckPnlCreateNewStudent.Children.Add(ctrlExistingPersonStudent)
    End Sub

    ''' <summary>
    ''' Method that cancels the creation of a new Student
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub btnExitCreateStudent_Click(sender As Object, e As RoutedEventArgs) Handles btnExitCreateStudent.Click
        'Close Window
        Dim parentWindow As Window = Window.GetWindow(Me)
        parentWindow.Close()
    End Sub

    ''' <summary>
    ''' Method that manage the window closing
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub gridClicked(sender As Object, e As RoutedEventArgs)
        If e.OriginalSource.ToString() = "System.Windows.Controls.Button: Cancel" Then
            'Delete NewPersonTeacher Control
            stckPnlCreateNewStudent.Children.RemoveAt(0)
            stckPnlMenuNewStudent.Visibility = Windows.Visibility.Visible
        End If

        'Automatically resize height, width and position relative to content
        Me.SizeToContent = SizeToContent.WidthAndHeight
        Me.Top = 15
    End Sub
End Class
