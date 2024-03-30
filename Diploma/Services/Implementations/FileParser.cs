using Diploma.Models;
using OfficeOpenXml;

namespace Diploma.Services.Implementations
{
    public class FileParser : IFileParser
    {
        public List<ExpirementRow> Parse(IFormFile file)
        {
            var fileName = Path.GetFileName(file.FileName);
            var fileExtension = Path.GetExtension(fileName).ToLower();

            return fileExtension switch
            {
                ".xlsx" => ParseXlsx(file),
                ".csv" => ParseCsv(file),
                _ => throw new ArgumentException()
            };
        }

        private List<ExpirementRow> ParseXlsx(IFormFile file)
        {
            var fileStream = file.OpenReadStream();
            var data = new List<ExpirementRow>();

            using (var package = new ExcelPackage(fileStream))
            {
                var worksheet = package.Workbook.Worksheets[0];
                var rowCount = worksheet.Dimension.End.Row;
                var colCount = worksheet.Dimension.End.Column;

                for (int row = 2; row <= rowCount; row++)
                {
                    var rowData = new ExpirementRow();

                    for (int col = 1; col <= colCount; col++)
                    {
                        double.TryParse(worksheet.Cells[row, col].Value.ToString(), out var time);
                        double.TryParse(worksheet.Cells[row, col].Value.ToString(), out var fz);

                        rowData.Time = time;
                        rowData.Fz = fz;
                    }

                    data.Add(rowData);
                }
            }

            return data;
        }

        private List<ExpirementRow> ParseCsv(IFormFile file)
        {
            throw new NotImplementedException();
        }
    }
}
