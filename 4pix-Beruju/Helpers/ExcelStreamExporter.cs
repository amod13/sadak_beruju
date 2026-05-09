using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Configuration;

namespace _4pix_Beruju.Helpers
{
    public static class ExcelStreamExporter
    {

        private static readonly HashSet<string> HtmlFields;
        static ExcelStreamExporter()
        {
            var htmlFieldsConfig = ConfigurationManager.AppSettings["HtmlStripFields"];

            HtmlFields = new HashSet<string>(
                (htmlFieldsConfig ?? "")
                    .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(x => x.Trim()),
                StringComparer.OrdinalIgnoreCase
            );
        }

        public static void ExportToExcel(
            HttpResponseBase response,
            string fileName,
            string connectionString,
            string query,
            List<SqlParameter> parameters,
            List<string> headers,
            List<string> fieldMappings
        )
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();

                using (var cmd = new SqlCommand(query, conn))
                {
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters.ToArray());

                    using (var reader = cmd.ExecuteReader())
                    {
                        // =========================
                        // 🔥 USE MEMORY STREAM (FIX)
                        // =========================
                        using (var memoryStream = new MemoryStream())
                        {
                            using (var spreadsheet = SpreadsheetDocument.Create(
                                memoryStream,
                                SpreadsheetDocumentType.Workbook,
                                true))
                            {
                                var workbookPart = spreadsheet.AddWorkbookPart();
                                workbookPart.Workbook = new Workbook();

                                var worksheetPart = workbookPart.AddNewPart<WorksheetPart>();

                                using (var writer = OpenXmlWriter.Create(worksheetPart))
                                {
                                    writer.WriteStartElement(new Worksheet());
                                    writer.WriteStartElement(new SheetData());

                                    // =========================
                                    // HEADER
                                    // =========================
                                    WriteRow(writer, headers);

                                    int count = 0;

                                    // =========================
                                    // DATA ROWS
                                    // =========================
                                    while (reader.Read())
                                    {
                                        var rowValues = new List<string>();

                                        foreach (var field in fieldMappings)
                                        {
                                            var value = reader[field]?.ToString();

                                            if (HtmlFields.Contains(field))
                                            {
                                                value = StripHtml(value);
                                            }

                                            rowValues.Add(value);
                                        }

                                        WriteRow(writer, rowValues);

                                        count++;
                                    }

                                    writer.WriteEndElement(); // SheetData
                                    writer.WriteEndElement(); // Worksheet
                                }

                                var sheets = workbookPart.Workbook.AppendChild(new Sheets());

                                sheets.Append(new Sheet()
                                {
                                    Id = workbookPart.GetIdOfPart(worksheetPart),
                                    SheetId = 1,
                                    Name = "Report"
                                });

                                workbookPart.Workbook.Save();
                            }

                            // =========================
                            // RETURN FILE TO RESPONSE
                            // =========================
                            memoryStream.Position = 0;

                            response.Clear();
                            response.BufferOutput = false;
                            response.ContentType =
                                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

                            response.AddHeader("content-disposition",
                                $"attachment;filename={fileName}");

                            memoryStream.CopyTo(response.OutputStream);

                            HttpContext.Current.ApplicationInstance.CompleteRequest();
                        }
                    }
                }
            }
        }

        // =========================
        // WRITE ROW HELPER
        // =========================
        private static void WriteRow(OpenXmlWriter writer, IEnumerable<string> values)
        {
            writer.WriteStartElement(new Row());

            foreach (var val in values)
            {
                writer.WriteElement(new Cell
                {
                    DataType = CellValues.String,
                    CellValue = new CellValue(val ?? "")
                });
            }

            writer.WriteEndElement();
        }


        private static string StripHtml(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return "";

            string text = Regex.Replace(input, "<.*?>", string.Empty);

            return HttpUtility.HtmlDecode(text).Trim();
        }
    }
}