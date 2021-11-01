Imports Microsoft.Win32
Imports System.IO

Public Class ctrlProfileSettingsTab
    ''' <summary>
    ''' Method that changes the photo of an user
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

        'Set New Photo to the User Object
        If Not openFileDialog.FileName Is "" Then
            imgPhoto.Source = New BitmapImage(New Uri(openFileDialog.FileName))
        End If
    End Sub

    ''' <summary>
    ''' Method that save the changes on the advanced settings into the database
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub btnSaveChangesProfile_Click(sender As Object, e As RoutedEventArgs) Handles btnSaveChangesProfile.Click
        Dim validation As Boolean = True
        Dim teacher As New clsTeacher
        Dim sex As New Char
        Dim numReg As New Integer

        'Valid Username
        If Not Application.oValidator.validateEmptyString(txtUsername.Text) Then
            txtUsername.BorderBrush = Brushes.Red
            lblErrorUsername.Visibility = Windows.Visibility.Visible
            lblErrorUsername.Text = CType(FindResource("error.username"), String)
            validation = False
        ElseIf Not Application.oValidator.validateUsername(txtUsername.Text) Then
            txtUsername.BorderBrush = Brushes.Red
            lblErrorUsername.Visibility = Windows.Visibility.Visible
            lblErrorUsername.Text = CType(FindResource("error.username_invalid"), String)
            validation = False
        Else
            txtUsername.ClearValue(TextBox.BorderBrushProperty)
            lblErrorUsername.Visibility = Windows.Visibility.Collapsed
        End If

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
        ElseIf Not Application.oValidator.validateExistDNI(txtDNI.Text) And Not txtDNI.Text = DNI.Text Then
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
            If imgPhoto.Source Is Nothing Then
                If sex = "M" Then
                    imgPhoto.Source = CType(FindResource("imgMan"), ImageSource)
                Else
                    imgPhoto.Source = CType(FindResource("imgWomen"), ImageSource)
                End If
            End If

            'Set photo
            teacher.Photo = imgPhoto


            teacher.Address = txtAddress.Text
            teacher.City = txtCity.Text
            teacher.PostalCode = txtPostalCode.Text
            teacher.PersonID = txtPersonID.Text
            teacher.TeacherID = txtTeacherID.Text

            'Save Profile
            numReg = Application.oUserManager.changeUsername(teacher.TeacherID, txtUsername.Text)
            If numReg > 0 Then
                numReg = Application.oPersonManager.updatePerson(teacher)

                If numReg > 0 Then
                    'Show messageBox confirm
                    Dim messageBoxResult As MessageBoxResult
                    Dim messageBoxOk As New msgBxOk(CType(FindResource("message_box.save_changes"), String), CType(FindResource("message_box.save_changes_success"), String))
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

    ''' <summary>
    ''' Method that opens a new changePassword dialog
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub btnChangePassword_Click(sender As Object, e As RoutedEventArgs) Handles btnChangePassword.Click
        Dim isChangePasswordWindowOpen As Boolean = False

        'Check if a dlgChangePassword Window is opened
        For Each w As Window In Application.Current.Windows
            If w.Title = "Change password" Then
                isChangePasswordWindowOpen = True
                w.Activate()
                MessageBox.Show("You have already opened a change password window", "Error", MessageBoxButton.OK, MessageBoxImage.Exclamation)
            End If
        Next

        'If there is no dlgChangePassword Window, create and show a new one.
        If Not isChangePasswordWindowOpen Then
            Dim newChangePasswordDialog As New dlgChangePassword(txtUsername.Text)
            newChangePasswordDialog.ShowDialog()
        End If
    End Sub

    ''' <summary>
    ''' Method that deletes the photo of an User
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub btnDeleteImageStudent_Click(sender As Object, e As RoutedEventArgs) Handles btnDeleteImageProfile.Click
        imgPhoto.Source = Nothing
    End Sub
End Class
