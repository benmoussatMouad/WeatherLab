using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using _Excel = Microsoft.Office.Interop.Excel;

namespace WeatherLab.Synthése
{
    class ExcelClass
    {
        string path = "";
        _Application excel = new _Excel.Application();
        public Workbook wb;
        public Worksheet ws;

        public ExcelClass(string path, int sheet)
        {
            this.path = path;
            wb = excel.Workbooks.Open(path);
            ws = wb.Worksheets[sheet];
        }

        public double ReadCell(int i, int j)
        {
            i++;
            j++;
            if (ws.Cells[i, j].Value2 != null)
            {
                return ws.Cells[i, j].Value2;
            }
            return -1;
        }

        public void WriteCell(int i, int j, double Value)
        {
            ws.Cells[i, j].Value2 = Value;
        }

        public void Save()
        {
            wb.Save();
        }

        public void SaveAs(string path)
        {
            wb.SaveAs(path);
        }

        public void Close()
        {
            wb.Close();
        }

        public void CreateNewFile()
        {
            this.wb = excel.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            this.ws = wb.Worksheets[1];
        }

        public void CreateNewSheet()
        {
            Worksheet TempSheet = wb.Worksheets.Add(After: ws);
        }

        public void SelectWorksheet(int i)
        {
            this.ws = wb.Worksheets[i];
        }

        public void DeleteWorkSheet(int i)
        {
            if (i <= wb.Worksheets.Count)
            {
                wb.Worksheets[i].Delete();
            }
        }

        public void ProtectSheet()
        {
            ws.Protect();
        }

        public void ProtectSheet(string password)
        {
            ws.Protect(Password: password);
        }

        public void UnprotectSheet()
        {
            ws.Unprotect();
        }

        public void UnprotectSheet(string password)
        {
            ws.Unprotect(Password: password);
        }

        public double[,] ReadRange(int starti, int endi, int startj, int endj)
        {
            Range range = (Range)ws.Range[ws.Cells[starti, startj], ws.Cells[endi, endj]];
            object[,] holder = range.Value2;
            double[,] returnString = new double[endi - starti, endj - startj];

            for (int p = 1; p < endi - starti; p++)
            {
                for (int q = 1; q < endj - startj; q++)
                {
                    returnString[p - 1, q - 1] = (double)holder[p, q];
                }
            }
            return returnString;
        }

        public void WriteRange(List<ImportData> DATA)
        {
            int i = 2;
            foreach (var item in DATA)
            {
                ws.Cells[1, i].Value2 = item.Parametre;
                for (int j = 0; j < item.Data.Count; j++)
                {
                    WriteCell(j + 2, i, item.Data.ElementAt(j));
                }
                i++;
            }
        }

        public void WriteDate (List<string> dates)
        {
            ws.Cells[1, 1] = "Date";
            int i = 2;
            foreach (string item in dates)
            {
                ws.Cells[i, 1] = item;
                i++;
            }
        }
    }
}
