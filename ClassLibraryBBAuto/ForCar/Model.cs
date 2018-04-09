using System;
using System.Data;
using BBAuto.Logic.Abstract;

namespace BBAuto.Logic.ForCar
{
  public class Model : MainDictionary
  {
    public int MarkId { get; private set; }
    public string Name { get; set; }

    public Model(int idMark)
    {
      ID = 0;
      MarkId = idMark;
    }

    public Model(DataRow row)
    {
      fillFields(row);
    }

    private void fillFields(DataRow row)
    {
      ID = Convert.ToInt32(row.ItemArray[0]);
      Name = row.ItemArray[1].ToString();

      int idMark;
      int.TryParse(row.ItemArray[2].ToString(), out idMark);
      MarkId = idMark;
    }

    internal override void Delete()
    {
      _provider.Delete("Model", ID);
    }

    public override void Save()
    {
      _provider.Insert("Model", ID, Name, MarkId);
    }

    internal override object[] getRow()
    {
      return new object[2] {ID, Name};
    }
  }
}
