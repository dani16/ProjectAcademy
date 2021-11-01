Public Class ctrlNewGroup
    ''' <summary>
    ''' Method that set the necessary elements when the window is loaded
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub dlgNewGroup_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Dim englishLevel As List(Of clsEnglishLevel)
        Dim listTeacher As List(Of clsTeacher)

        'Load English Level into comboBox
        englishLevel = Application.oEnglishLevelManager.getEnglishLevels()
        cmbBxEnglishLevel.ItemsSource = englishLevel

        'Load All the teachers
        listTeacher = Application.oTeacherManager.getAllTeachers()
        cmbBxTeacher.ItemsSource = listTeacher
    End Sub

    ''' <summary>
    ''' Method that creates a new Student
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub btnCreateNewGroup_Click(sender As Object, e As RoutedEventArgs) Handles btnCreateNewGroup.Click
        Dim numReg As Integer
        Dim group As New clsGroup
        Dim aux1() As String = txtFeeInscription.Text.Split(" ")
        Dim aux2() As String = txtFeeMonthly.Text.Split(" ")
        Dim validation As Boolean = True
        lblErrorFeeInscription.Visibility = Windows.Visibility.Collapsed
        lblErrorFeeMonthly.Visibility = Windows.Visibility.Collapsed

        'Inscription fee
        If txtFeeInscription.Text = "" Then
            group.FeeInscription = 0
        Else
            If Double.TryParse(aux1(0), New Double) Then
                If aux1(0) < 0 Then
                    validation = False
                    lblErrorFeeInscription.Visibility = Windows.Visibility.Visible
                End If
            Else
                validation = False
                lblErrorFeeInscription.Visibility = Windows.Visibility.Visible
            End If
        End If

        'Monthly fee
        If txtFeeMonthly.Text = "" Then
            group.FeeMonthly = 0
        Else
            If Double.TryParse(aux2(0), New Double) Then
                If aux2(0) < 0 Then
                    validation = False
                    lblErrorFeeMonthly.Visibility = Windows.Visibility.Visible
                End If
            Else
                validation = False
                lblErrorFeeMonthly.Visibility = Windows.Visibility.Visible
            End If
        End If

        If validation Then
            Dim level As clsEnglishLevel

            'Get English Level
            level = CType(cmbBxEnglishLevel.SelectedValue, clsEnglishLevel)
            group.EnglishLevel = level.EnglishLevel

            'Get Description
            group.Description = txtDescription.Text

            'Get FeeInscription
            If txtFeeInscription.Text = "" Then
                group.FeeInscription = 0
            Else
                group.FeeInscription = aux1(0)
            End If

            'Get FeeMonthly
            If txtFeeMonthly.Text = "" Then
                group.FeeMonthly = 0
            Else
                group.FeeMonthly = aux2(0)
            End If

            'Get Teacher
            group.TeacherID = cmbBxTeacher.SelectedValue

            'Save Group 
            numReg = Application.oGroupManager.insertGroup(group)

            If numReg > 0 Then
                'Close dialog
                Dim parentWindow As Window = Window.GetWindow(Me)
                parentWindow.Close()

                'Show messageBox confirm
                Dim messageBoxResult As MessageBoxResult
                Dim messageBoxOk As New msgBxOk("Create group", "Group successfully created")
                messageBoxResult = messageBoxOk.ShowMessageBox()
            Else
                'Show messageBox Error
                Dim messageBoxResult As MessageBoxResult
                Dim messageBoxOk As New msgBxOk(CType(FindResource("message_box.error_title"), String), CType(FindResource("message_box.error_message"), String))
                messageBoxResult = messageBoxOk.ShowMessageBox()
            End If
            
        End If
    End Sub

    ''' <summary>
    ''' Method that cancels the creation of a new Group
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub btnCancelCreateGroup_Click(sender As Object, e As RoutedEventArgs) Handles btnCancelNewGroup.Click
        'Close Window
        Dim parentWindow As Window = Window.GetWindow(Me)
        parentWindow.Close()
    End Sub
End Class