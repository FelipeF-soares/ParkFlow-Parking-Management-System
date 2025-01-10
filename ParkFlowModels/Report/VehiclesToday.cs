using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.xmp.impl;
using ParkFlowModels.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkFlowModels.Report;

public class VehiclesToday
{
    private  string wwwRootPath;
    private  string path;
    private  BaseFont baseFont;
    private  Font cellFont;

    public VehiclesToday(string wwwRootPath)
    {
        this.wwwRootPath = wwwRootPath;
        baseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false);
        cellFont = new Font(baseFont, 10, Font.NORMAL, BaseColor.BLACK);
    }

    public string ReportPDF(List<EntryExitAccess> accesses)
    {
        ClearDirectory();
        var pdf = ConfigurationOfPage(15F, 15F, 15F, 20F, true);
        var reportName = CreateReportName("ReportParking");
        var folder = CreateReportInPath(reportName);
        var write = CreatePdfWrite(pdf, folder);
        pdf.Open();
        var title = MakeTitle("Report Parking", 32);
        pdf.Add(title);
        string logoPath = wwwRootPath + @"\images\logoBlack.png";
        AddLogo(logoPath,pdf,write);
        if (accesses.Any())
        {
            float[] widhtCol = { 1.5f, 1.5f, 1.5f, 1.5f, 2.5f, 2.5f };
            string[] valueList = { "Placa", "Marca", "Modelo", "Cor", "Data/Hora Entrada", "Data/Hora Saída" };
            var table = MakeTable(6, widhtCol);
            LinkCellInTable(table,valueList);
            LinkValueInCell(table,accesses);
            pdf.Add(table);

        }
        else
        {
            var paragraf = MakeParagraph("Lista vazia");
            pdf.Add(paragraf);
        }
        pdf.Close();
        folder.Close();
        return wwwRootPath +"\\report\\"+ reportName;
        
    }
    private Document ConfigurationOfPage(float leftSide,float rightSide, float top, float bottom, bool horizontal)
    {
        var pxForMm = 72 / 25.2F;
        top = top * pxForMm;
        bottom = bottom * pxForMm;
        leftSide = leftSide * pxForMm;
        rightSide = rightSide * pxForMm;
        if(horizontal) 
        {
            return new Document(PageSize.A4.Rotate(), leftSide, rightSide,top,bottom);
        }
        return new Document(PageSize.A4,leftSide,rightSide,top,bottom);
    }

    private string CreateReportName(string reportName)
    {
        return $"{reportName}-{DateTime.Now.ToString("dd.MM.yyyy.HH.mm.ss")}.pdf";
    }

    private FileStream CreateReportInPath(string reportName)
    {
        path = wwwRootPath + "\\report\\" + reportName;
        return new FileStream(path, FileMode.Create);
    }

    private PdfWriter CreatePdfWrite(Document pdf, FileStream path)
    {
        return PdfWriter.GetInstance(pdf, path);
    }
    private Paragraph MakeTitle(string title, int fontSize) 
    {
        var fontTitle = new Font(baseFont, fontSize, Font.NORMAL, BaseColor.BLACK);
        var paragrph = new Paragraph($"{title}\n\n", fontTitle);
        paragrph.Alignment = Element.ALIGN_LEFT;
        paragrph.SpacingAfter = 4;
        return paragrph;
    }
    private Paragraph MakeParagraph(string content)
    {
        var fontTitle = new Font(baseFont, 12, Font.NORMAL, BaseColor.BLACK);
        var paragrph = new Paragraph($"{content}\n\n", fontTitle);
        paragrph.Alignment = Element.ALIGN_LEFT;
        paragrph.SpacingAfter = 4;
        return paragrph;
    }
    private PdfPTable MakeTable(int cols, float[] widthCol)
    {
        var table = new PdfPTable(cols);
        table.SetWidths(widthCol);
        table.DefaultCell.BorderWidth = 0;
        table.WidthPercentage = 100;
        return table;
    }
    private PdfPCell MakeCell(string name, PdfPTable table)
    {
        var bgColor = BaseColor.WHITE;
        if(table.Rows.Count %2 == 1)
        {
            bgColor = new BaseColor(0.95f, 0.95f, 0.95f);
        }
        var cell = new PdfPCell(new Phrase(name, cellFont));
        cell.HorizontalAlignment = PdfPCell.ALIGN_LEFT;
        cell.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
        cell.Border = 0;
        cell.BorderWidthBottom = 1;
        cell.FixedHeight = 25;
        cell.PaddingBottom = 5;
        cell.BackgroundColor = bgColor;
        return cell;
    }
    private void LinkCellInTable(PdfPTable table, string[] valueList)
    {
        foreach (var value in valueList)
        {
            var cell = MakeCell(value, table);
            table.AddCell(cell);
        }
    }
    private void LinkValueInCell(PdfPTable table, List<EntryExitAccess> accesses)
    {
        for(int i = 0; i < accesses.Count; i++)
        {
            var licensePlate = MakeCell(accesses[i].Vehicle.LicensePlate, table);
            table.AddCell(licensePlate);
            var brand = MakeCell(accesses[i].Vehicle.Brand, table);
            table.AddCell(brand);
            var model = MakeCell(accesses[i].Vehicle.Model, table);
            table.AddCell(model);
            var cor = MakeCell(accesses[i].Vehicle.Color, table);
            table.AddCell(cor);
            var entry = MakeCell(accesses[i].EntryTime.ToString(), table);
            table.AddCell(entry);
            if (accesses[i].ExitTime == null)
            {
                var exit = MakeCell("", table);
                table.AddCell(exit);
            }
            else
            {
                var exit = MakeCell(accesses[i].ExitTime.ToString(), table);
                table.AddCell(exit);
            }
        }
    }

    private void AddLogo(string logoPath,Document pdf, PdfWriter writer)
    {
        if (File.Exists(logoPath))
        {
            iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(logoPath);
            float widthForHeight = logo.Width / logo.Height;
            float heightLogo = 200;
            float widthLogo = heightLogo * widthForHeight;
            logo.ScaleToFit(widthLogo, heightLogo);
            var marginLeft = pdf.PageSize.Width - pdf.RightMargin - widthLogo;
            var marginTop = pdf.PageSize.Height - pdf.TopMargin - 140;
            logo.SetAbsolutePosition(marginLeft, marginTop);
            writer.DirectContent.AddImage(logo);
        }
    }

    private void ClearDirectory()
    {
        string[] files = Directory.GetFiles(wwwRootPath + "\\report");
        if(files.Length > 0)
        {
            foreach(string file in files) 
            {
                File.Delete(file);
            }
        }
    }
}
