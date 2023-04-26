using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAslarTestDotNetPractical
{
    public static class ExportHelper
    {
        public static byte[] GenerateWorksheet<T>(IEnumerable<T> data, List<string> properties)
        {
            var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Data");

            for (int i = 0; i < properties.Count; i++)
            {
                worksheet.Cells[1, i + 1].Value = properties[i];
            }

            for (int i = 0; i < data.Count(); i++)
            {
                var item = data.ElementAt(i);

                for (int j = 0; j < properties.Count; j++)
                {
                    var propertyInfo = item.GetType().GetProperty(properties[j]);
                    var propertyValue = propertyInfo != null ? propertyInfo.GetValue(item) : null;
                    if (propertyValue != null)
                    {
                        if (propertyValue is DateTime)
                        {
                            worksheet.Cells[i + 2, j + 1].Value = ((DateTime)propertyValue).ToString("yyyy-MM-dd HH:mm:ss");
                        }
                        else
                        {
                            worksheet.Cells[i + 2, j + 1].Value = propertyValue.ToString();
                        }
                    }
                    else
                    {
                        worksheet.Cells[i + 2, j + 1].Value = string.Empty;
                    }
                }
            }

            return package.GetAsByteArray();
        }
    }
}