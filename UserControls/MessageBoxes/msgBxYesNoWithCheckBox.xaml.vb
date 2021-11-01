Public Class msgBxYesNoWithCheckBox
    'Properties
    Private _titleMessage As String
    Private _message As String
    Private _checkBox As New Boolean
    Private _checkBoxText As String
    Private _messageBoxResult As MessageBoxResult

    'Methods
    Public Sub New()
        InitializeComponent()
    End Sub

    Public Sub New(ByVal titleMessage As String, ByVal message As String, ByVal checkBox As Boolean, ByVal checkBoxText As String)
        Me._titleMessage = titleMessage
        Me._message = message
        Me._checkBox = checkBox
        Me._checkBoxText = checkBoxText
        InitializeComponent()
    End Sub

    'Getters and Setters
    Public Property TitleMessage() As String
        Get
            Return _titleMessage
        End Get
        Set(value As String)
            _titleMessage = value
        End Set
    End Property

    Public Property Message() As String
        Get
            Return _message
        End Get
        Set(value As String)
            _message = value
        End Set
    End Property

    Public Property CheckBox() As Boolean
        Get
            Return _checkBox
        End Get
        Set(value As Boolean)
            _checkBox = value
        End Set
    End Property

    Public Property CheckBoxText() As String
        Get
            Return _checkBoxText
        End Get
        Set(value As String)
            _checkBoxText = value
        End Set
    End Property

    'Methods
    ''' <summary>
    ''' Method that load the messageBox elements
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub msgBxYesNoWithCheckBox_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        txtTitleMessage.Text = Me.TitleMessage
        txtMessage.Text = Me.Message
        chckBxMessageBox.Content = Me.CheckBoxText
    End Sub

    ''' <summary>
    ''' Method that set the CheckBox to TRUE
    ''' </summary>
    ''' <remarks></remarks>
    Public Function ShowMessageBox() As MessageBoxResult
        Me.ShowDialog()
        Return _messageBoxResult
    End Function

    ''' <summary>
    ''' Method that set the MessageBox to Yes when the button Yes is clicked
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub btnYes_Click(sender As Object, e As RoutedEventArgs) Handles btnYes.Click
        _messageBoxResult = MessageBoxResult.Yes
        Me.Close()
    End Sub

    ''' <summary>
    ''' Method that set the MessageBox to Yes when the button Yes is clicked
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub btnNo_Click(sender As Object, e As RoutedEventArgs) Handles btnNo.Click
        _messageBoxResult = MessageBoxResult.No
        Me.Close()
    End Sub

    ''' <summary>
    ''' Method that set the CheckBox to TRUE
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub chckBxMessageBox_Checked(sender As Object, e As RoutedEventArgs) Handles chckBxMessageBox.Checked
        Me.CheckBox = True
    End Sub

    ''' <summary>
    ''' Method that set the CheckBox to FALSE
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub chckBxMessageBox_Unchecked(sender As Object, e As RoutedEventArgs) Handles chckBxMessageBox.Unchecked
        Me.CheckBox = False
    End Sub
End Class
