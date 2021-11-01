Imports Microsoft.Win32

Public Class ctrlNewPersonTeacher
    Dim _isImageChange As Boolean = False

    ''' <summary>
    ''' Method that sets the photo of a Teacher
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub btnChangeImageProfile_Click(sender As Object, e As RoutedEventArgs) Handles btnChangeImageProfile.Click
        Dim openFileDialog As New OpenFileDialog()
        Dim numReg As New Integer

        'The user search on his computer for a new Photo
        openFileDialog.ShowDialog()
        openFileDialog.Title = CType(FindResource("dialog.photo_title"), String)
        'openFileDialog.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files(*.png)|*.png|JPG"
        openFileDialog.DefaultExt = ".jpeg"

        'Set New Photo to the Teacher Object
        If Not openFileDialog.FileName Is "" Then
            imgPhotoTeacher.Source = New BitmapImage(New Uri(openFileDialog.FileName))
            _isImageChange = True
        End If
    End Sub

    ''' <summary>
    ''' Method that creates a new Teacher
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub btnCreateNewTeacher_Click(sender As Object, e As RoutedEventArgs) Handles btnCreateNewTeacher.Click
        Dim validation As Boolean = True
        Dim teacher As New clsTeacher
        Dim sex As New Char
        Dim numReg As Integer

        'Valid Name
        If Not Application.oValidator.validateEmptyString(txtName.Text) Then
            txtName.BorderBrush = Brushes.Red
            lblErrorName.Visibility = Windows.Visibility.Visible
            validation = False
        Else
            teacher.Name = txtName.Text
            txtName.ClearValue(TextBox.BorderBrushProperty)
            lblErrorName.Visibility = Windows.Visibility.Collapsed
        End If

        'Valid Surname
        If Not Application.oValidator.validateEmptyString(txtSurname.Text) Then
            txtSurname.BorderBrush = Brushes.Red
            lblErrorSurname.Visibility = Windows.Visibility.Visible
            validation = False
        Else
            teacher.Surname = txtSurname.Text
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
            teacher.DNI = txtDNI.Text
            txtDNI.ClearValue(TextBox.BorderBrushProperty)
            lblErrorDNI.Visibility = Windows.Visibility.Collapsed
        End If

        'Validate Date
        If txtBirthDate.SelectedDate Is Nothing Then
            teacher.BirthDate = Nothing
        Else
            teacher.BirthDate = txtBirthDate.SelectedDate
        End If

        'Validate Telephone
        If Application.oValidator.validateEmptyString(txtTelephone.Text) And Not Application.oValidator.validateTelephone(txtTelephone.Text) Then
            txtTelephone.BorderBrush = Brushes.Red
            lblErrorTelephone.Visibility = Windows.Visibility.Visible
            validation = False
        Else
            teacher.Telephone = txtTelephone.Text
            txtTelephone.ClearValue(TextBox.BorderBrushProperty)
            lblErrorTelephone.Visibility = Windows.Visibility.Collapsed
        End If

        'Validate Email
        If Application.oValidator.validateEmptyString(txtEmail.Text) And Not Application.oValidator.validateEmail(txtEmail.Text) Then
            txtEmail.BorderBrush = Brushes.Red
            lblErrorEmail.Visibility = Windows.Visibility.Visible
            validation = False
        Else
            teacher.Email = txtEmail.Text
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

            teacher.Sex = sex

            'If any photo has been attached
            If Not _isImageChange Then
                If sex = "M" Then
                    imgPhotoTeacher.Source = CType(FindResource("imgMan"), ImageSource)
                Else
                    imgPhotoTeacher.Source = CType(FindResource("imgWomen"), ImageSource)
                End If
            End If

            'Set photo
            teacher.Photo = imgPhotoTeacher

            teacher.Address = txtAddress.Text
            teacher.City = txtCity.Text
            teacher.PostalCode = txtPostalCode.Text

            'Save Student 
            numReg = Application.oPersonManager.insertPerson(teacher)

            If numReg > 0 Then
                teacher.PersonID = Application.oPersonManager.getLastPersonID()
                numReg = Application.oTeacherManager.insertTeacher(teacher)

                If numReg > 0 Then
                    'Close dialog
                    Dim parentWindow As Window = Window.GetWindow(Me)
                    parentWindow.Close()

                    'Show messageBox confirm
                    Dim messageBoxResult As MessageBoxResult
                    Dim messageBoxOk As New msgBxOk(CType(FindResource("message_box.create_teacher"), String), CType(FindResource("message_box.create_teacher_success"), String))
                    messageBoxResult = messageBoxOk.ShowMessageBox()
                End If
            End If

            If numReg <= 0 Then
                'Show messageBox Error
                Dim messageBoxResult As MessageBoxResult
                Dim messageBoxOk As New msgBxOk(CType(FindResource("message_box.error_title"), String), CType(FindResource("message_box.error_message"), String))
                messageBoxResult = messageBoxOk.ShowMessageBox()
            End If
        End If
    End Sub
End Class
