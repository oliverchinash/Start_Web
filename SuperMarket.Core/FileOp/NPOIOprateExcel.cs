﻿using System;
using System.Data;
using System.Data.OleDb; 
using System.IO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;

namespace SuperMarket.Core.FileOp
{



	/// <summary>
	/// Summary description for ExcelReader.
	/// </summary>
	public class NPOIOprateExcel  
	{
        /// <summary>  
        /// 将excel导入到datatable  
        /// </summary>  
        /// <param name="filePath">excel路径</param>  
        /// <param name="isColumnName">第一行是否是列名</param>  
        /// <returns>返回datatable</returns>  
        public static DataTable ExcelToDataTable(string filePath, bool isColumnName=false)
        {
            DataTable dataTable = null;
            FileStream fs = null;
            DataColumn column = null;
            DataRow dataRow = null;
            IWorkbook workbook = null;
            ISheet sheet = null;
            IRow row = null;
            ICell cell = null;
            int startRow = 0;
            try
            {
                using (fs = File.OpenRead(filePath))
                {
                    // 2007版本  
                    if (filePath.IndexOf(".xlsx") > 0)
                        workbook = new XSSFWorkbook(fs);
                    // 2003版本  
                    else if (filePath.IndexOf(".xls") > 0)
                        workbook = new HSSFWorkbook(fs);

                    if (workbook != null)
                    {
                        sheet = workbook.GetSheetAt(0);//读取第一个sheet，当然也可以循环读取每个sheet  
                        dataTable = new DataTable();
                        if (sheet != null)
                        {
                            int rowCount = sheet.LastRowNum;//总行数  
                            if (rowCount > 0)
                            {
                                IRow firstRow = sheet.GetRow(0);//第一行  
                                int cellCount = firstRow.LastCellNum;//列数  

                                //构建datatable的列  
                                if (isColumnName)
                                {
                                    startRow = 1;//如果第一行是列名，则从第二行开始读取  
                                    for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                                    {
                                        cell = firstRow.GetCell(i);
                                        if (cell != null)
                                        {
                                            if (cell.StringCellValue != null)
                                            {
                                                column = new DataColumn(cell.StringCellValue);
                                                dataTable.Columns.Add(column);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                                    {
                                        column = new DataColumn("column" + (i + 1));
                                        dataTable.Columns.Add(column);
                                    }
                                }

                                //填充行  
                                for (int i = startRow; i <= rowCount; ++i)
                                {
                                    row = sheet.GetRow(i);
                                    if (row == null) continue;

                                    dataRow = dataTable.NewRow();
                                    for (int j = row.FirstCellNum; j < cellCount; ++j)
                                    {
                                        cell = row.GetCell(j);
                                        if (cell == null)
                                        {
                                            dataRow[j] = "";
                                        }
                                        else
                                        {
                                            //CellType(Unknown = -1,Numeric = 0,String = 1,Formula = 2,Blank = 3,Boolean = 4,Error = 5,)  
                                            switch (cell.CellType)
                                            {
                                                case CellType.Blank:
                                                    dataRow[j] = "";
                                                    break;
                                                case CellType.Numeric:
                                                    short format = cell.CellStyle.DataFormat;
                                                    //对时间格式（2015.12.5、2015/12/5、2015-12-5等）的处理  
                                                    if (format == 14 || format == 31 || format == 57 || format == 58)
                                                        dataRow[j] = cell.DateCellValue;
                                                    else
                                                        dataRow[j] = cell.NumericCellValue;
                                                    break;
                                                case CellType.String:
                                                    dataRow[j] = cell.StringCellValue;
                                                    break;
                                            }
                                        }
                                    }
                                    dataTable.Rows.Add(dataRow);
                                }
                            }
                        }
                    }
                }
                return dataTable;
            }
            catch (Exception ex)
            {
                if (fs != null)
                {
                    fs.Close();
                }
                return null;
            }
        }
        public static bool DataTableToExcel(DataTable dt,string filepath)
        {
            bool result = false;
            IWorkbook workbook = null;
            FileStream fs = null;
            IRow row = null;
            ISheet sheet = null;
            ICell cell = null;
            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    workbook = new HSSFWorkbook();
                    sheet = workbook.CreateSheet("Sheet0");//创建一个名称为Sheet0的表  
                    int rowCount = dt.Rows.Count;//行数  
                    int columnCount = dt.Columns.Count;//列数   
                    //设置列头  
                    //row = sheet.CreateRow(0);//excel第一行设为列头  
                    //for (int c = 0; c < columnCount; c++)
                    //{
                    //    cell = row.CreateCell(c);
                    //    cell.SetCellValue(dt.Columns[c].ColumnName);
                    //} 
                    //设置每行每列的单元格,  
                    for (int i = 0; i < rowCount; i++)
                    {
                        row = sheet.CreateRow(i);
                        for (int j = 0; j < columnCount; j++)
                        {
                            cell = row.CreateCell(j);//excel第二行开始写入数据  
                            cell.SetCellValue(dt.Rows[i][j].ToString());
                        }
                    }
                    using (fs = File.OpenWrite(filepath))
                    {
                        workbook.Write(fs);//向打开的这个xls文件中写入数据  
                        result = true;
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                if (fs != null)
                {
                    fs.Close();
                }
                return false;
            }
        }

        public static bool setExcelCellValue(String ExcelPath, String sheetname, int column, int row, String value)
        {
            bool returnb = false;
            try
            {
                HSSFWorkbook wk = null;
                using (FileStream fs = File.Open(ExcelPath, FileMode.Open,FileAccess.Read, FileShare.ReadWrite))
                {
                    //把xls文件读入workbook变量里，之后就可以关闭了  
                    wk = new HSSFWorkbook(fs);
                    fs.Close();
                }


                //把xls文件读入workbook变量里，之后就可以关闭了  




                ISheet sheet = wk.GetSheet(sheetname);
                ICell cell = sheet.GetRow(row).GetCell(column);




                cell.SetCellValue(value);


                using (FileStream fileStream = File.Open(ExcelPath,FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    wk.Write(fileStream);
                    fileStream.Close();
                }


                returnb = true;
            }
            catch (Exception)
            {
                returnb = false;
                throw;
            }


            return returnb;


        }
        
        public static String getExcelCellValue(string ExcelPath, String sheetname, int column, int row)
        {


            String returnStr = null;
            try
            {
                HSSFWorkbook wk = null;
                using (FileStream fs = File.Open(ExcelPath, FileMode.Open,
                FileAccess.Read, FileShare.ReadWrite))
                {
                    //把xls文件读入workbook变量里，之后就可以关闭了  
                    wk = new HSSFWorkbook(fs);
                    fs.Close();
                }


                //把xls文件读入workbook变量里，之后就可以关闭了  




                ISheet sheet = wk.GetSheet(sheetname);
                ICell cell = sheet.GetRow(row).GetCell(column);


                returnStr = cell.ToString();
            }
            catch (Exception)
            {
                returnStr = "Exception";
                throw;
            }


            return returnStr;


        }

    }
}
