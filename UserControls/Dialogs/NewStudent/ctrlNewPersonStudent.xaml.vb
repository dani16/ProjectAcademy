Imports Microsoft.Win32

Public Class ctrlNewPersonStudent
    Dim isImageChange As Boolean = False

    ''' <summary>
    ''' Method that set the necessary elements when the window is loaded
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub dlgNewStudent_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Dim englishLevel As List(Of clsEnglishLevel)

        'Load English Level into comboBox
        englishLevel = Application.oEnglishLevelManager.getEnglishLevels()
        cmbBxEnglishLevel.ItemsSource = englishLevel

        'Set DataPicker DisplayDateEnd
        txtBirthDate.DisplayDateEnd = Date.Today
    End Sub

    ''' <summary>
    ''' Method that sets the photo of an Student
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub btnChangeImageProfile_Click(sender As Object, e As RoutedEventArgs) Handles btnChangeImageProfile.Click
        Dim openFileDialog As New OpenFileDialog()
        Dim numReg As New Integer

        'The user search on his computer for a new Photo
        openFileDialog.ShowDialog()
        openFileDialog.Title = "Select Photo"
        'openFileDialog.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files(*.png)|*.png|JPG"
        openFileDialog.DefaultExt = ".jpeg"

        'Set New Photo to the Student Object
        If Not openFileDialog.FileName Is "" Then
            imgPhotoStudent.Source = New BitmapImage(New Uri(openFileDialog.FileName))
            isImageChange = True
        End If
    End Sub

    ''' <summary>
    ''' Method that creates a new Student
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub btnCreateNewStudent_Click(sender As Object, e As RoutedEventArgs) Handles btnCreateNewStudent.Click
        Dim validation As Boolean = True
        Dim student As New clsStudent
        Dim sex As New Char
        Dim numReg As Integer
        Dim age As Integer

        'Valid Name
        If Not Application.oValidator.validateEmptyString(txtName.Text) Then
            txtName.BorderBrush = Brushes.Red
            lblErrorName.Visibility = Windows.Visibility.Visible
            validation = False
        Else
            student.Name = txtName.Text
            txtName.ClearValue(TextBox.BorderBrushProperty)
            lblErrorName.Visibility = Windows.Visibility.Collapsed
        End If

        'Valid Surname
        If Not Application.oValidator.validateEmptyString(txtSurname.Text) Then
            txtSurname.BorderBrush = Brushes.Red
            lblErrorSurname.Visibility = Windows.Visibility.Visible
            validation = False
        Else
            student.Surname = txtSurname.Text
            txtSurname.ClearValue(TextBox.BorderBrushProperty)
            lblErrorSurname.Visibility = Windows.Visibility.Collapsed
        End If

        'Validate DNI
        lblErrorDNI.Text = ""
        If Not Application.oValidator.validateEmptyString(txtDNI.Text) Then
            txtDNI.BorderBrush = Brushes.Red
            lblErrorDNI.Visibility = Windows.Visibility.Visible
            lblErrorDNI.Text = CType(FindResource("error.DNI_empty"), String)
            validation = False
        ElseIf Not Application.oValidator.validateDNI(txtDNI.Text) Then
            txtDNI.BorderBrush = Brushes.Red
            lblErrorDNI.Visibility = Windows.Visibility.Visible
            lblErrorDNI.Text = CType(FindResource("error.DNI_invalid"), String)
            validation = False
        ElseIf Not Application.oValidator.validateExistDNI(txtDNI.Text) Then
            txtDNI.BorderBrush = Brushes.Red
            lblErrorDNI.Visibility = Windows.Visibility.Visible
            lblErrorDNI.Text = CType(FindResource("error.DNI_existing"), String)
            validation = False
        Else
            student.DNI = txtDNI.Text
            txtDNI.ClearValue(TextBox.BorderBrushProperty)
            lblErrorDNI.Visibility = Windows.Visibility.Collapsed
        End If

        'Validate Date
        If txtBirthDate.SelectedDate Is Nothing Then
            student.BirthDate = Nothing
            txtBirthDate.ClearValue(TextBox.BorderBrushProperty)
            lblErrorDateBirth.Visibility = Windows.Visibility.Collapsed
        Else
            If Not IsDate(txtBirthDate.SelectedDate) Or txtBirthDate.SelectedDate.Value > Date.Today Or txtBirthDate.SelectedDate.Value.Year < 1754 Then
                txtBirthDate.BorderBrush = Brushes.Red
                lblErrorDateBirth.Visibility = Windows.Visibility.Visible
                txtBirthDate.SelectedDate = Nothing
                validation = False
            Else
                student.BirthDate = txtBirthDate.SelectedDate
                txtBirthDate.BorderBrush = Brushes.White
                lblErrorDateBirth.Visibility = Windows.Visibility.Collapsed
            End If
        End If

        'Validate Telephone
        If Application.oValidator.validateEmptyString(txtTelephone.Text) And Not Application.oValidator.validateTelephone(txtTelephone.Text) Then
            txtTelephone.BorderBrush = Brushes.Red
            lblErrorTelephone.Visibility = Windows.Visibility.Visible
            validation = False
        Else
            student.Telephone = txtTelephone.Text
            txtTelephone.ClearValue(TextBox.BorderBrushProperty)
            lblErrorTelephone.Visibility = Windows.Visibility.Collapsed
        End If

        'Validate Email
        If Application.oValidator.validateEmptyString(txtEmail.Text) And Not Application.oValidator.validateEmail(txtEmail.Text) Then
            txtEmail.BorderBrush = Brushes.Red
            lblErrorEmail.Visibility = Windows.Visibility.Visible
            validation = False
        Else
            student.Email = txtEmail.Text
            txtEmail.ClearValue(TextBox.BorderBrushProperty)
            lblErrorEmail.Visibility = Windows.Visibility.Collapsed
        End If

        If validation Then
            'Get sex
            If (rdBtnSexMale.IsChecked) Then
                sex = "M"
            Else
                sex = "F"
            End If

            student.Situation = txtSituation.Text
            Dim level As clsEnglishLevel
            level = CType(cmbBxEnglishLevel.SelectedValue, clsEnglishLevel)
            student.EnglishLevel = level.EnglishLevel
            student.Sex = sex

            'Get Age
            If Not txtBirthDate.SelectedDate Is Nothing Then
                age = Date.Today.Year - txtBirthDate.SelectedDate.Value.Year
                If txtBirthDate.SelectedDate.Value > Date.Today.AddYears(-age) Then
                    age -= 1
                End If
            Else
                age = 18
            End If

            'If any photo has been attached
            If Not isImageChange Then
                If sex = "M" Then
                    If age >= 18 Then
                        imgPhotoStudent.Source = CType(FindResource("imgMan"), ImageSource)
                    Else
                        imgPhotoStudent.Source = CType(FindResource("imgBoy"), ImageSource)
                    End If
                Else
                    If sex = "F" Then
                        If age >= 18 Then
                            imgPhotoStudent.Source = CType(FindResource("imgWomen"), ImageSource)
                        Else
                            imgPhotoStudent.Source = CType(FindResource("imgGirl"), ImageSource)
                        End If
                    End If
                End If
            End If

            'Set photo
            student.Photo = imgPhotoStudent

            student.Address = txtAddress.Text
            student.City = txtCity.Text
            student.PostalCode = txtPostalCode.Text

            'Save Student 
            numReg = Application.oPersonManager.insertPerson(student)

            If numReg > 0 Then
                student.PersonID = Application.oPersonManager.getLastPersonID()

                If numReg > 0 Then
                    numReg = Application.oStudentManager.insertStudent(student)

                    'Close dialog
                    Dim parentWindow As Window = Window.GetWindow(Me)
                    parentWindow.Close()

                    'Show messageBox confirm
                    Dim messageBoxResult As MessageBoxResult
                    Dim messageBoxOk As New msgBxOk("Create student", "Student successfully created")
                    messageBoxResult = messageBoxOk.ShowMessageBox()
                End If
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
