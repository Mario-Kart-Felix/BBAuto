using System;
using System.Data;
using BBAuto.Domain.Abstract;

namespace BBAuto.Domain.Tables
{
  public class Region : MainDictionary
  {
    public string Name { get; private set; }

    public Region(DataRow row)
    {
      int id;
      int.TryParse(row[0].ToString(), out id);
      ID = id;

      Name = row[1].ToString();
    }

    public Region(string name)
    {
      Name = name;
    }

    internal override object[] getRow()
    {
      throw new NotImplementedException();
    }

    public override void Save()
    {
      int id;
      int.TryParse(_provider.Insert("Region", ID, Name), out id);

      ID = id;
    }
  }
}
