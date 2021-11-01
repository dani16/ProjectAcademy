Public Class dlgChangePassword
    Private _username As String

    Public Sub New()
        InitializeComponent()
    End Sub

    Public Sub New(ByVal username As String)
        Me._username = username
        InitializeComponent()
    End Sub

    Public Property Username() As String
        Get
            Return _username
        End Get
        Set(ByVal value As String)
            _username = value
        End Set
    End Property

    ''' <summary>
    ''' Method that cancels the change of a password
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub btnCancelChangePassword_Click(sender As Object, e As RoutedEventArgs) Handles btnCancelChangePassword.Click
        'Close Window
        Dim parentWindow As Window = Window.GetWindow(Me)
        parentWindow.Close()
    End Sub

    ''' <summary>
    ''' Method that changes the password of an user
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub btnChangePassword_Click(sender As Object, e As RoutedEventArgs) Handles btnChangePassword.Click
        Dim validation As Boolean = True
        Dim user As New clsUser
        Dim numReg As Integer

        'Valid Current Password
        If Not Application.oValidator.validateEmptyString(txtCurrentPassword.Text) Then
            txtCurrentPassword.BorderBrush = Brushes.Red
            lblErrorCurrentPassword.Visibility = Windows.Visibility.Visible
            lblErrorCurrentPassword.Text = CType(FindResource("error.password_empty"), String)
            validation = False
        ElseIf Not Application.oValidator.validatePasswordFormat(txtCurrentPassword.Text) Then
            txtCurrentPassword.BorderBrush = Brushes.Red
            txtCurrentPassword.Visibility = Windows.Visibility.Visible
            lblErrorCurrentPassword.Text = CType(FindResource("error.password_invalid"), String)
            validation = False
        Else
            txtCurrentPassword.ClearValue(TextBox.BorderBrushProperty)
            lblCurrentPassword.Visibility = Windows.Visibility.Collapsed
        End If

        'Valid New Password
        If Not Application.oValidator.validateEmptyString(txtNewPassword.Text) Then
            txtNewPassword.BorderBrush = Brushes.Red
            lblErrorNewPassword.Visibility = Windows.Visibility.Visible
            lblErrorNewPassword.Text = CType(FindResource("error.new_password_empty"), String)
            validation = False
        ElseIf Not Application.oValidator.validatePasswordFormat(txtNewPassword.Text) Then
            txtNewPassword.BorderBrush = Brushes.Red
            txtNewPassword.Visibility = Windows.Visibility.Visible
            lblErrorNewPassword.Text = CType(FindResource("error.password_invalid"), String)
            validation = False
        Else
            txtNewPassword.ClearValue(TextBox.BorderBrushProperty)
            lblNewPassword.Visibility = Windows.Visibility.Collapsed
        End If

        'Valid Repeate New Password
        If Not Application.oValidator.validateEmptyString(txtRepeatNewPassword.Text) Then
            txtRepeatNewPassword.BorderBrush = Brushes.Red
            lblErrorRepeatNewPassword.Visibility = Windows.Visibility.Visible
            lblErrorRepeatNewPassword.Text = CType(FindResource("error.repeat_password_empty"), String)
            validation = False
        ElseIf Not Application.oValidator.validatePasswordFormat(txtRepeatNewPassword.Text) Then
            txtRepeatNewPassword.BorderBrush = Brushes.Red
            txtRepeatNewPassword.Visibility = Windows.Visibility.Visible
            lblErrorRepeatNewPassword.Text = CType(FindResource("error.password_invalid"), String)
            validation = False
        ElseIf Not Application.oValidator.validatePasswordRepeat(txtNewPassword.Text, txtRepeatNewPassword.Text) Then
            txtRepeatNewPassword.BorderBrush = Brushes.Red
            txtRepeatNewPassword.Visibility = Windows.Visibility.Visible
            lblErrorRepeatNewPassword.Text = CType(FindResource("error.repeat_password_match"), String)
            validation = False
        Else
            txtRepeatNewPassword.ClearValue(TextBox.BorderBrushProperty)
            lblRepeatNewPassword.Visibility = Windows.Visibility.Collapsed
        End If

        If validation Then
            'Save Password
            numReg = Application.oUserManager.changePassword(Username, txtCurrentPassword.Text, txtNewPassword.Text)

            If numReg > 0 Then
                'Close dialog
                Dim parentWindow As Window = Window.GetWindow(Me)
                parentWindow.Close()

                'Show messageBox confirm
                Dim messageBoxResult As MessageBoxResult
                Dim messageBoxOk As New msgBxOk(CType(FindResource("message_box.change_password"), String), CType(FindResource("message_box.change_password_success"), String))
                messageBoxResult = messageBoxOk.ShowMessageBox()
            Else
                'Show messageBox Error
                Dim messageBoxResult2 As MessageBoxResult
                Dim messageBoxOk2 As New msgBxOk(CType(FindResource("message_box.error_title"), String), CType(FindResource("message_box.error_message"), String))
                messageBoxResult2 = messageBoxOk2.ShowMessageBox()
            End If
        End If
    End Sub
End Class
