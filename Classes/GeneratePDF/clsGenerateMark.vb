Imports Spire.Pdf
Imports Spire.Pdf.Graphics
Imports Spire.Pdf.Grid
Imports System.Drawing

Public Class clsGenerateMark
#Region "Attributes"
    Private _doc As New PdfDocument()
    Private _student As New clsStudent
    Private _teacherID As Integer
    Private _mark As New clsMark
#End Region

#Region "Constructors"
    Public Sub New()
        _doc = New PdfDocument()
    End Sub

    Public Sub New(ByVal student As clsStudent, ByVal teacherID As Integer, ByVal mark As clsMark)
        _doc = New PdfDocument()
        _student = student
        _teacherID = teacherID
        _mark = mark
    End Sub
#End Region

#Region "Methods"
    ''' <summary>
    ''' Method that generates a Receipt
    ''' </summary>
    ''' <remarks></remarks>
    Public Function generateReceipt() As PdfDocument
        Dim namePDF As String = generateNamePDF()
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
        Dim page As PdfPageBase = _doc.Pages.Add(PdfPageSize.A4, margin)
        Dim y As Single = 5
        Dim x1 As Single = page.Canvas.ClientSize.Width

        'Mark Information
        Dim gridMarksInformation As New PdfGrid()
        gridMarksInformation.Style.CellPadding = New PdfPaddings(2, 2, 2, 2)
        gridMarksInformation.Style.TextPen = New PdfPen(Color.Transparent, 0.02F)

        'Grid without borders
        Dim border As PdfBorders = New PdfBorders()
        border.All = New PdfPen(Color.Transparent)

        'Headers
        Dim header() As String = New String("Date;Class").Split(";")
        gridMarksInformation.Columns.Add(header.Length)
        Dim width As Single = page.Canvas.ClientSize.Width - (gridMarksInformation.Columns.Count + 1)
        gridMarksInformation.Columns(0).Width = width * 0.7F
        gridMarksInformation.Columns(1).Width = width * 0.3F

        'Cells
        Dim rows As PdfGridRow = gridMarksInformation.Rows.Add()
        Dim term As String = Nothing

        Select Case (_mark.getTerm())
            Case 0
                term = "1º Evaluación"
            Case 1
                term = "2º Evaluación"
            Case 2
                term = "3º Evaluación"
            Case 3
                term = "Verano"
        End Select

        rows.Cells(0).Value = term
        rows.Cells(0).Style.Font = New PdfTrueTypeFont(New Font("Arial", 17.0F, FontStyle.Bold), True)
        rows.Cells(0).StringFormat = New PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Middle)
        rows.Cells(1).Value = "Curso: " & _mark.DateMark.Year
        rows.Cells(1).Style.Font = New PdfTrueTypeFont(New Font("Arial", 17.0F, FontStyle.Bold), True)
        rows.Cells(1).StringFormat = New PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Middle)

        rows = gridMarksInformation.Rows.Add()
        rows.Cells(0).Value = "Alumno/a: " & _student.Name & " " & _student.Surname
        rows.Cells(0).StringFormat = New PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Middle)

        Dim group As clsGroup = Application.oGroupManager.getGroup(_mark.GroupID)
        rows.Cells(1).Value = "Clase: " & " " & group.EnglishLevel & "  " & group.Description
        rows.Cells(1).StringFormat = New PdfStringFormat(PdfTextAlignment.Left, PdfVerticalAlignment.Middle)

        For Each pgr As PdfGridRow In gridMarksInformation.Rows
            For Each pgc As PdfGridCell In pgr.Cells
                pgc.Style.Borders = border
            Next
        Next

        'Draw Grid
        Dim result As PdfLayoutResult = gridMarksInformation.Draw(page, New PointF(0, y))

        'Grid Marks
        Dim gridMarks As New PdfGrid()
        gridMarks.Style.CellPadding = New PdfPaddings(2, 2, 2, 2)
        gridMarks.Style.TextPen = New PdfPen(Color.Transparent, 0.02F)

        'Headers
        header = New String("Listening;Speaking;Reading;Writing;Exam;Overall").Split(";")
        gridMarks.Columns.Add(header.Length)
        width = page.Canvas.ClientSize.Width - (gridMarks.Columns.Count + 1)
        gridMarks.Columns(0).Width = width * 0.16F
        gridMarks.Columns(1).Width = width * 0.16F
        gridMarks.Columns(2).Width = width * 0.16F
        gridMarks.Columns(3).Width = width * 0.16F
        gridMarks.Columns(4).Width = width * 0.16F
        gridMarks.Columns(5).Width = width * 0.16F

        'Cells
        rows = gridMarks.Rows.Add()
        rows.Cells(0).Value = "Listening" & vbCr & "(Escuchar)"
        rows.Cells(0).StringFormat = New PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle)
        rows.Cells(1).Value = "Speaking" & vbCr & "(Hablar)"
        rows.Cells(1).StringFormat = New PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle)
        rows.Cells(2).Value = "Reading" & vbCr & "(Leer)"
        rows.Cells(2).StringFormat = New PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle)
        rows.Cells(3).Value = "Writing" & vbCr & "(Escribir)"
        rows.Cells(3).StringFormat = New PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle)
        rows.Cells(4).Value = "Exam" & vbCr & "(Examen)"
        rows.Cells(4).StringFormat = New PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle)
        rows.Cells(5).Value = "Overall" & vbCr & "(Global)"
        rows.Cells(5).StringFormat = New PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle)

        rows = gridMarks.Rows.Add()
        rows.Cells(0).Value = _mark.Listening.ToString
        rows.Cells(0).StringFormat = New PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle)
        rows.Cells(1).Value = _mark.Speaking.ToString
        rows.Cells(1).StringFormat = New PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle)
        rows.Cells(2).Value = _mark.Reading.ToString
        rows.Cells(2).StringFormat = New PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle)
        rows.Cells(3).Value = _mark.Writing.ToString
        rows.Cells(3).StringFormat = New PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle)
        rows.Cells(4).Value = _mark.Exam.ToString
        rows.Cells(4).StringFormat = New PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle)
        rows.Cells(5).Value = _mark.Overall.ToString
        rows.Cells(5).StringFormat = New PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle)

        'Draw Grid
        result = gridMarks.Draw(page, New PointF(0, y + 50))

        'Sign and comment
        Dim teacher As clsTeacher = Application.oTeacherManager.getTeacher(_teacherID)
        'Grid Marks
        Dim gridBottom As New PdfGrid()
        gridBottom.Style.CellPadding = New PdfPaddings(2, 2, 2, 2)
        gridBottom.Style.TextPen = New PdfPen(Color.Transparent, 0.02F)

        'Headers
        header = New String("Comments;Teacher's Signature").Split(";")
        gridBottom.Columns.Add(header.Length)
        width = page.Canvas.ClientSize.Width - (gridMarks.Columns.Count + 1)
        gridBottom.Columns(0).Width = width * 0.5F
        gridBottom.Columns(1).Width = width * 0.5F

        'Cells
        rows = gridBottom.Rows.Add()
        rows.Cells(0).Value = "Comments" & vbCr & "(Observaciones)"
        rows.Cells(0).StringFormat = New PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle)
        rows.Cells(1).Value = "Teacher's signature" & vbCr & "(Firma del profesor)"
        rows.Cells(1).StringFormat = New PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle)

        rows = gridBottom.Rows.Add()
        rows.Cells(1).Value = teacher.Name & " " & teacher.Surname
        rows.Cells(1).StringFormat = New PdfStringFormat(PdfTextAlignment.Center, PdfVerticalAlignment.Middle)

        For Each pgr As PdfGridRow In gridBottom.Rows
            For Each pgc As PdfGridCell In pgr.Cells
                pgc.Style.Borders = border
            Next
        Next

        'Draw Grid
        result = gridBottom.Draw(page, New PointF(0, y + 120))

        'Save pdf file.
        _doc.SaveToFile(namePDF)
        _doc.Close()

        'Launching the Pdf file.
        PDFDocumentViewer(namePDF)

        Return _doc
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
    ''' <returns>A String</returns>
    ''' <remarks></remarks>
    Private Function generateNamePDF() As String
        Return _student.Name & "_" & _student.Surname & "_" & _mark.getTerm & "(" & _mark.DateMark.Year & ")"
    End Function
#End Region
End Class
