

using System.Data;

using UnityEngine;
using UnityEditor;

using Locke;


public class Xlsx_To_X
{
	[MenuItem("Locke_Tools/Excel/xlsx_to_txt")]
	static void xlsx_to_txt()
	{
		Object[] selectedObjects = Selection.GetFiltered(typeof(Object), SelectionMode.Editable | SelectionMode.DeepAssets);

		for (int i = 0; i < selectedObjects.Length; i++)
		{
			Object obj = selectedObjects[i] as Object;

			string filePath = Application.dataPath + "/" + obj.name;
			DataSet dataSet = XlsxReader.Instance.Read(filePath);
			DataTable tableCollection = dataSet.Tables[0];

			/*int columns = dataSet.Tables[0].Columns.Count;
			int rows = dataSet.Tables[0].Rows.Count;
			string readData = "";
			for (int i = 0; i < rows; i++)
			{
				for (int j = 0; j < columns; j++)
				{
					string nvalue = dataSet.Tables[0].Rows[i][j].ToString();
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


	[MenuItem("Locke_Tools/Excel/xlsx_to_cs")]
	static void xlsx_to_cs()
	{
		Object[] selectedObjects = Selection.GetFiltered(typeof(Object), SelectionMode.Editable | SelectionMode.DeepAssets);

		for (int i = 0; i < selectedObjects.Length; i++)
		{
			Object gobj = selectedObjects[i] as Object;

		}
	}

	private string ReadExcelFile(DataTable dataTable)
	{
		System.Text.StringBuilder resultFile = new System.Text.StringBuilder();

		try
		{
			int columnsCount = dataTable.Columns.Count;
			bool[] isBoolean = new bool[columnsCount];

			// is bool
			;
			//string nvalue = dataTable.Rows[3][j].ToString();
			DataRow rowCollection = dataTable.Rows[3];
			foreach (Cell cell in rowCollection)
			{
				int cellColumnIndex = GetColumnIndex(GetColumnName(cell.CellReference));
				string cellInfo = GetValueOfCell(spreadsheetDocument, cell, false);
				if (cellInfo.Equals("bool"))
					isBoolean[cellColumnIndex] = true;
				else
					isBoolean[cellColumnIndex] = false;
			}

			// Add rows into DataTable, because cell in original table may be empty 
			foreach (Row row in rowcollection)
			{
				DataRow tempRow = dt.NewRow();
				int columnIndex = 0;
				foreach (Cell cell in row.Descendants<Cell>())
				{
					// Get Cell Column Index
					int cellColumnIndex = GetColumnIndex(GetColumnName(cell.CellReference));

					if (columnIndex < cellColumnIndex)
					{
						do
						{
							tempRow[columnIndex] = string.Empty;
							columnIndex++;
						}
						while (columnIndex < cellColumnIndex);
					}

					string cellValue = GetValueOfCell(spreadsheetDocument, cell, isBoolean[cellColumnIndex]);
					tempRow[columnIndex] = cellValue;
					columnIndex++;
				}

				// Add the row to DataTable
				dt.Rows.Add(tempRow);
			}
			// Here remove header row
			dt.Rows.RemoveAt(0);

			string defaultText = "";
			foreach (DataRow row in dt.Rows)
			{
				for (int j = 0; j < columnsCount; ++j)
				{
					string cellText = row[j].ToString();
					cellText = cellText.Replace("\n", "\\n");

					if (cellText == null)
						cellText = defaultText;
					if (j + 1 == columnsCount)
						resultFile.Append(cellText);
					else
					{
						resultFile.Append(cellText);
						resultFile.Append("\t");
					}

				}
				resultFile.Append("\n");

			}
			// remove last '\n'
			resultFile.Remove(resultFile.Length - 1, 1);
			
			return resultFile.ToString();
		}
		catch (System.Exception ex)
		{
			throw new System.Exception(ex.Message);
		}
	}

}