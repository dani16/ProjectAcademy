Public Class ctrlNewExistingPersonStudent
    Dim _listExistingPeople As List(Of clsPerson)
    Dim _person As clsPerson

    ''' <summary>
    ''' Method that load all the existing People that can be a student
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub ctrlNewExistintingPersonStudent_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Dim englishLevel As List(Of clsEnglishLevel)

        'Load English Level into comboBox
        englishLevel = Application.oEnglishLevelManager.getEnglishLevels()
        cmbBxEnglishLevel.ItemsSource = englishLevel
    End Sub

    ''' <summary>
    ''' Method that search for People when the user write on the searcher textBox
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub txtBxSearchStudent_TextChanged(sender As Object, e As TextChangedEventArgs) Handles txtBxSearchStudent.TextChanged
        txtMessageSearch.Visibility = Windows.Visibility.Hidden

        If txtBxSearchStudent.Text.Length >= 3 Then
            'Get deleted people from the database
            _listExistingPeople = Application.oPersonManager.getAllDeletedPeople(txtBxSearchStudent.Text)
            _listExistingPeople.AddRange(Application.oTeacherManager.getNotStudentTeachers(txtBxSearchStudent.Text)) 'Teacher that can also be a Student
            lstBxExistingPeople.ItemsSource = _listExistingPeople

            'Show message: Not results found
            If _listExistingPeople.Count = 0 Then
                txtMessageSearch.Visibility = Windows.Visibility.Visible
            End If
        Else
            lstBxExistingPeople.ItemsSource = Nothing
        End If
    End Sub

    ''' <summary>
    ''' Method that loads a student to the grid when a student is selected from the listBox
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub lstBxStudents_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles lstBxExistingPeople.SelectionChanged
        'When you change the tab and you return to the Student tab the Selected Index is -1.
        If Me.lstBxExistingPeople.SelectedIndex >= 0 Then
            _person = _listExistingPeople.Item(Me.lstBxExistingPeople.SelectedIndex)
        End If

        Me.gridNewStudent.DataContext = _person
    End Sub

    ''' <summary>
    ''' Method that creates a new Student
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub btnCreateNewStudent_Click(sender As Object, e As RoutedEventArgs) Handles btnCreateNewStudent.Click
        Dim numReg As Integer
        Dim student As New clsStudent
        Dim level As New clsEnglishLevel

        If Not _person Is Nothing Then
            'Create new student
            With student
                .PersonID = _person.PersonID
                .Name = _person.Name
                .Surname = _person.Surname
                .DNI = _person.DNI
                .Photo = _person.Photo
                .Sex = _person.Sex
                .BirthDate = _person.BirthDate
                .Address = _person.Address
                .City = _person.City
                .PostalCode = _person.PostalCode
                .Telephone = _person.Telephone
                .Email = _person.Email
                .Situation = txtSituation.Text
                level = CType(cmbBxEnglishLevel.SelectedValue, clsEnglishLevel)
                .EnglishLevel = level.EnglishLevel
            End With

            'If the Selected type is a clsPerson it means that it is a Deleted_Person
            If Me.lstBxExistingPeople.SelectedValue.GetType() Is New clsPerson().GetType Then
                numReg = Application.oPersonManager.insertPerson(student)

                If numReg > 0 Then
                    student.PersonID = Application.oPersonManager.getLastPersonID()
                    numReg = Application.oStudentManager.insertStudent(student)
                End If
            Else
                'If the Selected type is a Teacher we do not have to create a new person
                numReg = Application.oStudentManager.insertStudent(student)
            End If

            If numReg > 0 Then
                'Close Window
                Dim parentWindow As Window = Window.GetWindow(Me)
                parentWindow.Close()

                'Show messageBox confirm
                Dim messageBoxResult As MessageBoxResult
                Dim messageBoxOk As New msgBxOk(CType(FindResource("message_box.create_student"), String), CType(FindResource("message_box.create_student_success"), String))
                messageBoxResult = messageBoxOk.ShowMessageBox()
            End If

            If numReg <= 0 Then
                'Show messageBox Error
                Dim messageBoxResult2 As MessageBoxResult
                Dim messageBoxOk2 As New msgBxOk(CType(FindResource("message_box.error_title"), String), CType(FindResource("message_box.error_message"), String))
                messageBoxResult2 = messageBoxOk2.ShowMessageBox()
            End If
        End If
    End Sub
End Class
