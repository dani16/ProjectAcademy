Imports Spire.Pdf
Imports Spire.Pdf.Graphics
Imports Spire.Pdf.Grid
Imports System.Drawing

Public Class clsGenerateReceipt
#Region "Attributes"
    'Private doc As New PdfDocument()
#End Region

#Region "Constructors"
    'Public Sub New()
    '    doc = New PdfDocument()
    'End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' Method that generates a Receipt
    ''' </summary>
    ''' <param name="payment">An object clsPayment</param>
    ''' <remarks></remarks>
    Public Function generateReceipt(ByVal payment As clsPayment, ByVal teacherID As Integer) As PdfDocument
        Dim doc As New PdfDocument
        Dim student As clsStudent = Application.oStudentManager.getStudent(payment.StudentID)
        Dim namePDF As String = generateNamePDF(payment)
        Dim brushTitle As PdfBrush = PdfBrushes.AliceBlue
        Dim brush As PdfBrush = PdfBrushes.Black
        Dim fontTitle As New PdfTrueTypeFont(New Font("Arial", 16.0F, FontStyle.Bold))
        Dim font As New PdfTrueTypeFont(New Font("Arial", 6.0F, FontStyle.Bold))
        Dim formatCenter As New PdfStringFormat(PdfTextAlignment.Center)
        Dim formatLeft As New PdfStringFormat(PdfTextAlignment.Left)
        Dim formatRight As New PdfStringFormat(PdfTextAlignment.Right)

        'Margin
        Dim unitCvtr As New PdfUnitConvertor()
        Dim margin As New PdfMargins()
        margin.Top = unitCvtr.ConvertUnits(0.54F, PdfGraphicsUnit.Centimeter, PdfGraphicsUnit.Point)
        margin.Bottom = margin.Top
        margin.Left = unitCvtr.ConvertUnits(1.17F, PdfGraphicsUnit.Centimeter, PdfGraphicsUnit.Point)
        margin.Right = margin.Left

        ' Create one page
        Dim page As PdfPageBase = doc.Pages.Add(PdfPageSize.A4, margin)
        Dim y As Single = 5
        Dim x1 As Single = page.Canvas.ClientSize.Width

        'Grid
        Dim grid As New PdfGrid()
        grid.Style.CellPadding = New PdfPaddings(2, 2, 2, 2)
        grid.Style.CellSpacing = 3
        grid.Style.TextPen = New PdfPen(Color.Transparent, 0.02F)

        'Headers
        Dim header() As String = New String("Date;Description;Amount").Split(";")
        grid.Columns.Add(header.Length)
        Dim width As Single = page.Canvas.ClientSize.Width - (grid.Columns.Count + 1)
        grid.Columns(0).Width = width * 0.15F
        grid.Columns(1).Width = width * 0.65F
        grid.Columns(2).Width = width * 0.2F

        'Cells
        Dim rows As PdfGridRow = grid.Rows.Add()
        rows.Cells(0).Value = "RECEIPT"
        rows.Cells(0).Style.Font = New PdfTrueTypeFont(New Font("Arial", 15.0F, FontStyle.Bold), True)
        rows.Cells(0).StringFormat = New PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Bottom)
        rows.Cells(1).Value = "Date: " & payment.DatePayment
        rows.Cells(1).StringFormat = New PdfStringFormat(PdfTextAlignment.Right, PdfVerticalAlignment.Bottom)
        rows.Cells(2).Value = "Nº: " & generateNamePDF(payment)
        rows.Cells(2).StringFormat = New PdfStringFormat(PdfTextAlignment.Right, PdfVerticalAlignment.Bottom)

        'Draw Line
        rows = grid.Rows.Add()
        Dim pen As New PdfPen(Color.Black, 0.02F)
        page.Canvas.DrawLine(pen, New PointF(0, y), New PointF(page.Canvas.ClientSize.Width, y))

        rows = grid.Rows.Add()
        rows.Cells(0).Value = "Receive from:  "
        rows.Cells(0).StringFormat = New PdfStringFormat(PdfTextAlignment.Right, PdfVerticalAlignment.Middle)
        rows.Cells(1).Value = student.Name & " " & student.Surname
        rows.Cells(1).Style.BackgroundBrush = PdfBrushes.Gray
        rows.Cells(1).StringFormat = New PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle)

        rows = grid.Rows.Add()
        rows.Cells(0).Value = "Amount:  "
        rows.Cells(0).StringFormat = New PdfStringFormat(PdfTextAlignment.Right, PdfVerticalAlignment.Middle)
        rows.Cells(1).Value = payment.Amount.ToString
        rows.Cells(1).Style.BackgroundBrush = PdfBrushes.Gray
        rows.Cells(1).StringFormat = New PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle)
        rows.Cells(2).Value = "€"
        rows.Cells(2).StringFormat = New PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Middle)

        rows = grid.Rows.Add()
        rows.Cells(0).Value = "For payment of:  "
        rows.Cells(0).StringFormat = New PdfStringFormat(PdfTextAlignment.Right, PdfVerticalAlignment.Middle)
        rows.Cells(1).Value = payment.paymentTypeToString
        rows.Cells(1).Style.BackgroundBrush = PdfBrushes.Gray
        rows.Cells(1).StringFormat = New PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle)

        rows = grid.Rows.Add()
        rows.Cells(0).Value = "Received by: "
        rows.Cells(0).StringFormat = New PdfStringFormat(PdfTextAlignment.Right, PdfVerticalAlignment.Middle)
        rows.Cells(1).Value = Application.oTeacherManager.getTeacher(teacherID).Name & " " & Application.oTeacherManager.getTeacher(teacherID).Surname
        rows.Cells(1).Style.BackgroundBrush = PdfBrushes.Gray
        rows.Cells(1).StringFormat = New PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle)

        rows = grid.Rows.Add()
        rows.Cells(1).Value = "Sign:  "
        rows.Cells(1).StringFormat = New PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Middle)

        'Grid without borders
        Dim border As PdfBorders = New PdfBorders()
        border.All = New PdfPen(Color.Transparent)

        For Each pgr As PdfGridRow In grid.Rows
            For Each pgc As PdfGridCell In pgr.Cells
                pgc.Style.Borders = border
            Next
        Next

        'Draw Grid
        Dim result As PdfLayoutResult = grid.Draw(page, New PointF(0, y))
        y = y + result.Bounds.Height + 25

        'Draw Line
        rows = grid.Rows.Add()
        page.Canvas.DrawLine(pen, New PointF(0, y), New PointF(page.Canvas.ClientSize.Width, y))

        'Save pdf file.
        doc.SaveToFile(namePDF)
        doc.Close()

        'Launching the Pdf file.
        PDFDocumentViewer(namePDF)

        Return doc
    End Function

    ''' <summary>
    ''' Method that 
    ''' </summary>
    ''' <param name="fileName"></param>
    ''' <remarks></remarks>
    Private Sub PDFDocumentViewer(ByVal fileName As String)
        Try
            Process.Start(fileName)
        Catch
        End Try
    End Sub

    ''' <summary>
    ''' Method that generates the name of the PDF file
    ''' </summary>
    ''' <param name="payment">An object clsPayment</param>
    ''' <returns>A String</returns>
    ''' <remarks></remarks>
    Private Function generateNamePDF(ByVal payment As clsPayment) As String
        Return "P" & payment.StudentID & payment.GroupID & "-" & payment.DatePayment.Day & payment.DatePayment.Month & payment.DatePayment.Year
    End Function
#End Region
End Class
