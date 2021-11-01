Public Class ctrlDecimalTextBox
    'Properties
    Private _MAXVALUE As Double = 10
    Private _MINVALUE As Double = 0

    'Dependency Property
    Public Property NumericValue() As Double
        Get
            Return CType(GetValue(NumericValueProperty), Double)
        End Get
        Set(ByVal value As Double)
            SetValue(NumericValueProperty, value)
        End Set
    End Property

    Public Shared NumericValueProperty As DependencyProperty = DependencyProperty.Register("NumericValue", _
                                                                                                    GetType(Double), _
                                                                                                    GetType(ctrlDecimalTextBox), _
                                                                                                    New FrameworkPropertyMetadata(0.0, AddressOf NumericValuePropertyChanged))

    Private Shared Sub NumericValuePropertyChanged(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
        Dim txtBoxNumeric As ctrlDecimalTextBox = CType(d, ctrlDecimalTextBox)
        txtBoxNumeric.NumericValue = CType(e.NewValue, Double)
        txtBoxNumeric.txtBxDecimal.Text = txtBoxNumeric.NumericValue
    End Sub

#Region "Methods"
    ''' <summary>
    ''' Method that clears the value of the control
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub clearText()
        NumericValue = 0
    End Sub
#End Region

#Region "Events"
    ''' <summary>
    ''' Method that loads the first value of the TextBox
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ctrlDecimalTextBox_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        txtBxDecimal.Text = NumericValue

        If NumericValue < 5 Then
            txtBxDecimal.Background = CType(FindResource("clrRed"), Brush)
        End If
    End Sub

    ''' <summary>
    ''' Method that increases the value on 0,01
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub btnIncrease_Click(sender As Object, e As RoutedEventArgs)
        If NumericValue < _MAXVALUE Then
            NumericValue += 0.01
            txtBxDecimal.Text = NumericValue
        End If

        'Chagne color
        If NumericValue < 5 Then
            txtBxDecimal.Background = CType(FindResource("clrRed"), Brush)
        Else
            txtBxDecimal.Background = Brushes.White
        End If
    End Sub

    ''' <summary>
    ''' Method that decreases the value on 0,01
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub btnDecrease_Click(sender As Object, e As RoutedEventArgs)
        If NumericValue > _MINVALUE Then
            NumericValue -= 0.01
            txtBxDecimal.Text = NumericValue
        End If

        'Chagne color
        If NumericValue < 5 Then
            txtBxDecimal.Background = CType(FindResource("clrRed"), Brush)
        Else
            txtBxDecimal.Background = Brushes.White
        End If
    End Sub

    ''' <summary>
    ''' Method that decreases the value on 0,01
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object TextChangedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub txtBxDecimal_TextChanged(sender As Object, e As TextChangedEventArgs)
        If IsNumeric(txtBxDecimal.Text) Then
            Dim var As Double = CType(txtBxDecimal.Text, Double)

            If var >= _MINVALUE And var <= _MAXVALUE Then
                NumericValue = Math.Round(CType(txtBxDecimal.Text, Double), 2, MidpointRounding.AwayFromZero)
            Else
                txtBxDecimal.Text = NumericValue
            End If
        Else
            If txtBxDecimal.Text = "" Then
                NumericValue = 0
                'txtBxDecimal.Text = "0"
            Else
                txtBxDecimal.Text = NumericValue
            End If
        End If

        'Chagne color
        If NumericValue < 5 Then
            txtBxDecimal.Background = CType(FindResource("clrRed"), Brush)
        Else
            txtBxDecimal.Background = Brushes.White
        End If
    End Sub

    Private Sub txtBxDecimal_LostFocus(sender As Object, e As RoutedEventArgs) Handles txtBxDecimal.LostFocus
        If txtBxDecimal.Text = "" Then
            NumericValue = 0
            txtBxDecimal.Text = "0"
        End If
    End Sub

    ''' <summary>
    ''' Method that avoid the letters and the uneccesary character
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object KeyEventArgs</param>
    ''' <remarks></remarks>
    Private Sub ctrlDecimalTextBox_PreviewKeyDown(sender As Object, e As KeyEventArgs) Handles Me.PreviewKeyDown
        'Accept only numbers, return, supr, arrows right and left, and coma
        If (e.Key >= Key.D0 And e.Key <= Key.D9) Or (e.Key >= Key.NumPad0 And e.Key <= Key.NumPad9) _
            Or e.Key = 2 Or e.Key = 32 Or e.Key = 23 Or e.Key = 25 Or e.Key = 142 Then
            'MessageBox.Show(e.Key)
        Else
            e.Handled = True
        End If
    End Sub
#End Region
End Class
