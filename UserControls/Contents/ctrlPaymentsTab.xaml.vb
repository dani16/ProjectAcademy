Imports System.Drawing
Imports Spire.Pdf
Imports System.Drawing.Printing
Imports System.Windows.Controls.Primitives

Public Class ctrlPaymentsTab
    Dim _listPayments As List(Of clsPayment)
    Dim _payment As clsPayment

    ''' <summary>
    ''' Method that load all the payment into the dataGrid
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub ctrlPaymentsTab_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        'Load Students into comboBox
        cmbBxSearchStudent.ItemsSource = Application.oStudentManager.getAllStudents

        'Load Years into comboBox
        cmbBxYear.ItemsSource = Enumerable.Range(1950, DateTime.Today.Year).ToList()
        cmbBxYear.SelectedValue = DateTime.Today.Year

        'Load Month into comboBox
        cmbBxMonth.SelectedIndex = DateTime.Today.Month - 1

        'Load payments
        If dataGridPayments.ItemsSource Is Nothing Then
            _listPayments = Application.oPaymentManager.getAllPayments()
            dataGridPayments.ItemsSource = _listPayments
        End If
    End Sub

    ''' <summary>
    ''' Method that save the changes made on the datagrid
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object DataGridRowEditEndingEventArgs</param>
    ''' <remarks></remarks>
    Private Sub DataGrid_RowEditEnding(sender As Object, e As DataGridRowEditEndingEventArgs)
        Dim numReg As Integer
        _payment = CType(e.Row.DataContext, clsPayment)
        numReg = Application.oPaymentManager.makePayment(_payment)
    End Sub

    ''' <summary>
    ''' Method that searchs for payments by filters
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub cmbBxSearchStudent_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbBxSearchStudent.SelectionChanged
        _listPayments = Application.oPaymentManager.getAllPayments(cmbBxSearchStudent.SelectedValue, cmbBxYear.SelectedValue, cmbBxMonth.SelectedIndex + 1, chckBxShowUnpaidPayments.IsChecked)
        dataGridPayments.ItemsSource = _listPayments
    End Sub

    ''' <summary>
    ''' Method that searchs for payments by filters
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub cmbBxYear_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbBxYear.SelectionChanged
        _listPayments = Application.oPaymentManager.getAllPayments(cmbBxSearchStudent.SelectedValue, cmbBxYear.SelectedValue, cmbBxMonth.SelectedIndex + 1, chckBxShowUnpaidPayments.IsChecked)
        dataGridPayments.ItemsSource = _listPayments
    End Sub

    ''' <summary>
    ''' Method that searchs for payments by filters
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub cmbBxMonth_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbBxMonth.SelectionChanged
        _listPayments = Application.oPaymentManager.getAllPayments(cmbBxSearchStudent.SelectedValue, cmbBxYear.SelectedValue, cmbBxMonth.SelectedIndex + 1, chckBxShowUnpaidPayments.IsChecked)
        dataGridPayments.ItemsSource = _listPayments
    End Sub

    ''' <summary>
    ''' Method that searchs for payments by filters
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub chckBxShowUnpaidPayments_Checked(sender As Object, e As RoutedEventArgs) Handles chckBxShowUnpaidPayments.Checked
        _listPayments = Application.oPaymentManager.getAllPayments(cmbBxSearchStudent.SelectedValue, cmbBxYear.SelectedValue, cmbBxMonth.SelectedIndex + 1, chckBxShowUnpaidPayments.IsChecked)
        dataGridPayments.ItemsSource = _listPayments
    End Sub

    ''' <summary>
    ''' Method that searchs for payments by filters
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub chckBxShowUnpaidPayments_Unchecked(sender As Object, e As RoutedEventArgs) Handles chckBxShowUnpaidPayments.Unchecked
        _listPayments = Application.oPaymentManager.getAllPayments(cmbBxSearchStudent.SelectedValue, cmbBxYear.SelectedValue, cmbBxMonth.SelectedIndex + 1, chckBxShowUnpaidPayments.IsChecked)
        dataGridPayments.ItemsSource = _listPayments
    End Sub

    ''' <summary>
    ''' Method that clean the Date filter 
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub btnCleanFilterDate_Click(sender As Object, e As RoutedEventArgs) Handles btnCleanFilterDate.Click
        cmbBxYear.SelectedValue = Nothing
        cmbBxMonth.SelectedValue = Nothing
        _listPayments = Application.oPaymentManager.getAllPayments(cmbBxSearchStudent.SelectedValue, cmbBxYear.SelectedValue, cmbBxMonth.SelectedIndex + 1, chckBxShowUnpaidPayments.IsChecked)
        dataGridPayments.ItemsSource = _listPayments
    End Sub

    ''' <summary>
    ''' Method that clean the Student filter 
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub btnCleanFilterStudent_Click(sender As Object, e As RoutedEventArgs) Handles btnCleanFilterStudent.Click
        cmbBxSearchStudent.SelectedValue = Nothing
        _listPayments = Application.oPaymentManager.getAllPayments(cmbBxSearchStudent.SelectedValue, cmbBxYear.SelectedValue, cmbBxMonth.SelectedIndex + 1, chckBxShowUnpaidPayments.IsChecked)
        dataGridPayments.ItemsSource = _listPayments
    End Sub

    ''' <summary>
    ''' Method that download a receipt of a Payment
    ''' </summary>
    ''' <param name="sender">An Object</param>
    ''' <param name="e">An object RoutedEventArgs</param>
    ''' <remarks></remarks>
    Private Sub btnDownloadReceipt_Click(sender As Object, e As RoutedEventArgs)
        Dim originalSource As Control = e.OriginalSource
        Dim item As ContentPresenter = CType(originalSource.TemplatedParent, ContentPresenter)
        Dim payment As clsPayment = CType(item.DataContext, clsPayment)

        'Generate Receipt
        Dim generateReceipt As New clsGenerateReceipt
        generateReceipt.generateReceipt(payment, txtTeacherID.Text)
    End Sub
End Class
