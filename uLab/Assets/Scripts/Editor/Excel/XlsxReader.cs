using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using Excel;
using System.Data;
using UnityEngine.UI;

namespace Locke
{
	public class XlsxReader : Singleton<XlsxReader>
	{
		public DataSet Read(string filePath)
		{
			FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
			IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

			DataSet result = excelReader.AsDataSet();
			return result;

			//int columns = result.Tables[0].Columns.Count;
			//int rows = result.Tables[0].Rows.Count;

			/*
			string readData = "";
			for (int i = 0; i < rows; i++)
			{
				for (int j = 0; j < columns; j++)
				{
					string nvalue = result.Tables[0].Rows[i][j].ToString();
					Debug.Log(nvalue);
					if (i > 0)
					{
						readData += "\t\t" + nvalue;
					}
					else
					{
						readData += "   \t" + nvalue;
					}
				}
				readData += "\n";
			}*/
		}

	}

}