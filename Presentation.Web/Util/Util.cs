using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;

namespace Presentation.Web.Helpers
{
    public class Util
    {
        public static DataTable ReadExcelToDataTable(string filePath)
        {
            //Create a new DataTable.
            DataTable dt;

            //Open the Excel file using ClosedXML.
            using (XLWorkbook workBook = new XLWorkbook(filePath))
            {
                //Read the first Sheet from Excel file.
                IXLWorksheet workSheet = workBook.Worksheet(1);

                //Create a new DataTable.
                dt = new DataTable();

                //Loop through the Worksheet rows.
                bool firstRow = true;
                bool lastRow = false;
                foreach (IXLRow row in workSheet.Rows())
                {
                    //Use the first row to add columns to DataTable.
                    if (firstRow)
                    {

                        foreach (IXLCell cell in row.Cells())
                        {
                            dt.Columns.Add(cell.Value.ToString().Trim());

                        }
                        firstRow = false;
                    }
                    else
                    {
                        //Add rows to DataTable.

                        int i = 0;
                        foreach (IXLCell cell in row.Cells(false))
                        {
                            if (i == 0 && cell.Value.ToString().Trim() != String.Empty)
                            {
                                dt.Rows.Add();

                            }
                            else
                            {
                                if (i == 0 && cell.Value.ToString().Trim() == String.Empty)
                                {
                                    lastRow = true;
                                    break;
                                }

                            }
                            dt.Rows[dt.Rows.Count - 1][i] = cell.Value.ToString();
                            i++;

                        }
                    }
                    if (lastRow)
                        break;

                }
            }
            return dt;
        }

        public static string ValidarString(DataRow row, string column, List<string> errores, bool obligatorio)
        {



            if (obligatorio)
            {
                if (String.IsNullOrEmpty(row[column].ToString().Trim()))
                {
                    string formatObligatorio = "El campo {0} es obligatorio";
                    errores.Add(String.Format(formatObligatorio, column));
                    return null;
                }

            }

            return row[column].ToString().Trim();
        }

        public static bool ValidarPosibleValores(DataRow row, string column, List<string> errores, string posibleValores)
        {

            string[] list = posibleValores.Replace(", ", ",").ToLower().Split(',');

            string value = row[column].ToString().Trim().ToLower();

            if (list.Contains(value))
                return true;
            else
            {
                string formatError = "El campo {0} solo permite estos valores (" + posibleValores + ")";
                errores.Add(String.Format(formatError, column));
                return false;
            }


        }
        public static double? ValidarDouble(DataRow row, string column, List<string> errores, bool obligatorio)
        {

            double value;
            string formatDouble = "El campo {0} debe ser un número decimal";
            if (obligatorio)
            {
                if (String.IsNullOrEmpty(row[column].ToString().Trim()))
                {
                    string formatObligatorio = "El campo {0} es obligatorio";
                    errores.Add(String.Format(formatObligatorio, column));
                    return null;
                }

            }

            if (!String.IsNullOrEmpty(row[column].ToString().Trim()))
            {
                if (Double.TryParse(row[column].ToString().Trim(), out value))
                    return value;
                else
                {
                    errores.Add(String.Format(formatDouble, column));
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public static int ValidarInt(DataRow row, string column, List<string> errores, bool obligatorio)
        {

            int value;
            string formatInt = "El campo {0} debe ser un número entero";
            if (obligatorio)
            {
                if (String.IsNullOrEmpty(row[column].ToString().Trim()))
                {
                    string formatObligatorio = "El campo {0} es obligatorio";
                    errores.Add(String.Format(formatObligatorio, column));
                    return -1;
                }

            }

            if (!String.IsNullOrEmpty(row[column].ToString().Trim()))
            {
                if (Int32.TryParse(row[column].ToString(), out value))
                    return value;
                else
                {
                    errores.Add(String.Format(formatInt, column));
                    return -1;
                }
            }
            else
            {
                return -1;
            }
        }

        public static DateTime? ValidarDateTime(DataRow row, string column, List<string> errores, bool obligatorio)
        {

            DateTime value;
            string formatDouble = "El campo {0} debe ser una fecha valida YYYY-MM-DD";
            if (obligatorio)
            {
                if (String.IsNullOrEmpty(row[column].ToString().Trim()))
                {
                    string formatObligatorio = "El campo {0} es obligatorio";
                    errores.Add(String.Format(formatObligatorio, column));
                    return null;
                }

            }

            if (!String.IsNullOrEmpty(row[column].ToString().Trim()))
            {
                

                

                //the new format
                string format = "yyyy-MM-dd";

                if (DateTime.TryParseExact(row[column].ToString(), format, CultureInfo.InvariantCulture,
                       DateTimeStyles.None, out value))
                    return value;
                else
                {
                    errores.Add(String.Format(formatDouble, column));
                    return null;
                }
            }
            else
            {
                return null;
            }

        }

        public static Stream GetStream(XLWorkbook excelWorkbook)
        {
            Stream fs = new MemoryStream();
            excelWorkbook.SaveAs(fs);
            fs.Position = 0;
            return fs;
        }
        public static void Set_AllBorder(IXLRow iXLRow)
        {

            foreach (var cell in iXLRow.Cells())
            {
                cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                //cell.Style.Border.RightBorder = XLBorderStyleValues.Thin;
            }
        }
    }
}