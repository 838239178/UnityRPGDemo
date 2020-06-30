using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelDataReader;

public interface IDataInitialization
{
    void Initial(DataRow collect);
}

[Serializable]
public class Skill : IDataInitialization
{
    public string ID;
    public string name;
    public string damage;
    public string describe;

    public void Initial(DataRow collect)
    {

    }
}
