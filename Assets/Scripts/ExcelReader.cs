using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using System.IO;
using ExcelDataReader;

public interface IDataInitialization
{
    void Initial(DataRow collect);
}


/// <summary>
/// 读取excel表格的数据，泛型类需有无参构造函数并且实现接口IDataInitialization
/// </summary>
/// <typeparam name="T">适用于Data.cs中的类</typeparam>
public static class ExcelReader<T> where T:  IDataInitialization, new()
{
    public static T[] ReadDataExcel(string filePath) 
    {
        List<T> list = new List<T>();
        int column = 0;
        int row = 0;
        DataRowCollection collect = ReadExcel(filePath, ref column, ref row);

        for(int i = 1; i< row; i++)
        {
            if (IsEmptyRow(collect[i], column)) continue;

            T data = new T();
            data.Initial(collect[i]);
            list.Add(data);
        }
        return list.ToArray();
    }

    private static bool IsEmptyRow(DataRow collect, int columnNum)
    {
        for (int i = 0; i < columnNum; i++)
        {
            if (!collect.IsNull(i)) return false;
        }
        return true;
    }

    private static DataRowCollection ReadExcel(string filePath, ref int columnNum, ref int rowNum)
    {
        FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
        IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);

        DataSet result = excelReader.AsDataSet();
        //Tables[0] 下标0表示excel文件中第一张表的数据
        columnNum = result.Tables[0].Columns.Count;
        rowNum = result.Tables[0].Rows.Count;
        return result.Tables[0].Rows;
    }
}
