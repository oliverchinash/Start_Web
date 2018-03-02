using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 
using System.IO;
using System.Data;

namespace SuperMarket.Core.FileOp
{
  public  class DataTableUtil
    {
      public static DataTable GetDataTableCsv(string filename)
      {
          DataTable mycsvdt = new DataTable();
          if (filename == null || !File.Exists(filename))
          {
              return mycsvdt;
          }
          int intColCount = 0;
          bool blnFlag = true;

          DataColumn mydc;
          DataRow mydr;

          string strline;
          string[] aryline;
          StreamReader mysr = new StreamReader(filename, System.Text.Encoding.Default);

          while ((strline = mysr.ReadLine()) != null)
          {
              if (strline != "")
              {
                  aryline = strline.Split(new char[] { ';' });

                  //给datatable加上列名
                  if (blnFlag)
                  {
                      blnFlag = false;
                      intColCount = aryline.Length;
                      int col = 0;
                      for (int i = 0; i < aryline.Length; i++)
                      {
                          col = i + 1;
                          mydc = new DataColumn(col.ToString());
                          mycsvdt.Columns.Add(mydc);
                      }
                  }

                  //填充数据并加入到datatable中
                  mydr = mycsvdt.NewRow();
                  for (int i = 0; i < intColCount; i++)
                  {
                      mydr[i] = aryline[i].Trim('\"');
                  }
                  mycsvdt.Rows.Add(mydr);
              }
          }
          mysr.Close();
          mysr.Dispose();
          return mycsvdt;

      }
      public void ExportToSvc(System.Data.DataTable dt, string strName, string path)
      {

          string strPath = path + strName + ".csv"; 

          if (File.Exists(strPath))
          { 
              File.Delete(strPath); 
          }

          //先打印标头

          StringBuilder strColu = new StringBuilder();

          StringBuilder strValue = new StringBuilder();

          int i = 0;

 
              StreamWriter sw = new StreamWriter(new FileStream(strPath, FileMode.CreateNew), Encoding.GetEncoding("GB2312"));

              for (i = 0; i <= dt.Columns.Count - 1; i++)
              {

                  strColu.Append(dt.Columns[i].ColumnName);

                  strColu.Append(",");

              }

              strColu.Remove(strColu.Length - 1, 1);//移出掉最后一个,字符

              sw.WriteLine(strColu);

              foreach (DataRow dr in dt.Rows)
              {
                  strValue.Remove(0, strValue.Length);//移出



                  for (i = 0; i <= dt.Columns.Count - 1; i++)
                  {

                      strValue.Append(dr[i].ToString());

                      strValue.Append(",");
                  }

                  strValue.Remove(strValue.Length - 1, 1);//移出掉最后一个,字符

                  sw.WriteLine(strValue);

              }



              sw.Close();

         

          System.Diagnostics.Process.Start(strPath);



      }
    }
}
