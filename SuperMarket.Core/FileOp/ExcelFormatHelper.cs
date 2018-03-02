using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Core.FileOp
{
    public static class ExcelFormatHelper
    {
        /// <summary>
        /// 将Csv文件转换为XLS文件
        /// </summary>
        /// <param name="FilePath">文件全路路径</param>
        /// <returns>返回转换后的Xls文件名</returns>
        public static string CSVSaveasXLS(string FilePath)
        {
            QuertExcel();
            string _NewFilePath = "";

            Microsoft.Office.Interop.Excel.Application excelApplication;
            Microsoft.Office.Interop.Excel.Workbooks excelWorkBooks = null;
            Microsoft.Office.Interop.Excel.Workbook excelWorkBook = null;
            Microsoft.Office.Interop.Excel.Worksheet excelWorkSheet = null;

            try
            {
                excelApplication = new Microsoft.Office.Interop.Excel.Application();
                excelWorkBooks = excelApplication.Workbooks;
                excelWorkBook = ((Microsoft.Office.Interop.Excel.Workbook)excelWorkBooks.Open(FilePath, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value));
                excelWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelWorkBook.Worksheets[1];
                excelApplication.Visible = false;
                excelApplication.DisplayAlerts = false;
                _NewFilePath = FilePath.Replace(".csv", ".xls");
                excelWorkBook.SaveAs(_NewFilePath, Microsoft.Office.Interop.Excel.XlFileFormat.xlAddIn8, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                QuertExcel();
                ExcelFormatHelper.DeleteFile(FilePath);
                //可以不用杀掉进程QuertExcel();


                try
                {
                excelApplication.Quit();
                }
                catch (Exception sss)
                { 
                }
             

                GC.WaitForPendingFinalizers();
                GC.Collect();



            }
            catch (Exception exc)
            {
                throw new Exception(exc.Message);
            }


            finally
            {
                GC.Collect();
            }

            return _NewFilePath;
        }

        /// <summary>
        /// 将xls文件转换为csv文件
        /// </summary>
        /// <param name="FilePath">文件全路路径</param>
        /// <returns>返回转换后的csv文件名</returns>
        public static string XLSSavesaCSV(string FilePath)
        {
            QuertExcel();
            string _NewFilePath = "";

            Microsoft.Office.Interop.Excel.Application excelApplication;
            Microsoft.Office.Interop.Excel.Workbooks excelWorkBooks = null;
            Microsoft.Office.Interop.Excel.Workbook excelWorkBook = null;
            Microsoft.Office.Interop.Excel.Worksheet excelWorkSheet = null;

            try
            {
                excelApplication = new Microsoft.Office.Interop.Excel.Application();
                excelWorkBooks = excelApplication.Workbooks;
                excelWorkBook = ((Microsoft.Office.Interop.Excel.Workbook)excelWorkBooks.Open(FilePath, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value));
                excelWorkSheet = (Microsoft.Office.Interop.Excel.Worksheet)excelWorkBook.Worksheets[1];
                excelApplication.Visible = false;
                excelApplication.DisplayAlerts = false;
                _NewFilePath = FilePath.Replace(".xlsx", ".csv").Replace(".xls", ".csv");
                // excelWorkSheet._SaveAs(FilePath, Excel.XlFileFormat.xlCSVWindows, Missing.Value, Missing.Value, Missing.Value,Missing.Value,Missing.Value, Missing.Value, Missing.Value);
                excelWorkBook.SaveAs(_NewFilePath, Microsoft.Office.Interop.Excel.XlFileFormat.xlCSV, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Missing.Value, Missing.Value, Missing.Value, Missing.Value, Missing.Value);
                QuertExcel();
                //ExcelFormatHelper.DeleteFile(FilePath);
                try
                {
                    excelApplication.Quit();
                }
                catch (Exception sss)
                {
                }


                GC.WaitForPendingFinalizers();
                GC.Collect();

            }
            catch (Exception exc)
            {
                throw new Exception(exc.Message);
            }
            return _NewFilePath;
        }

        /// <summary>
        /// 删除一个指定的文件
        /// </summary>
        /// <param name="FilePath">文件路径</param>
        /// <returns></returns>
        public static bool DeleteFile(string FilePath)
        {
            try
            {
                bool IsFind = File.Exists(FilePath);
                if (IsFind)
                {
                    File.Delete(FilePath);
                }
                else
                {
                    throw new IOException("指定的文件不存在");
                }
                return true;
            }
            catch (Exception exc)
            {
                throw new Exception(exc.Message);
            }

        }

        /// <summary>
        /// 执行过程中可能会打开多个EXCEL文件 所以杀掉
        /// </summary>
        private static void QuertExcel()
        {
            try
            {
                Process[] excels = Process.GetProcessesByName("EXCEL");
                foreach (var item in excels)
                {
                    item.Kill();
                }
            }
            catch (Exception ex)
            {

            }
            
        }
    }

}
