using System.Data;
using BBAuto.Logic.Abstract;
using BBAuto.Logic.Dictionary;
using BBAuto.Logic.Entities;
using BBAuto.Logic.Lists;
using BBAuto.Logic.Static;

namespace BBAuto.Logic.ForDriver
{
  public class UserAccess : MainDictionary
  {
    public int RoleId { get; set; }

    public Driver Driver
    {
      get => DriverList.getInstance().getItem(ID);
      set => ID = value.ID;
    }

    public string Role
    {
      get
      {
        if (RoleId == 0)
          return "Нет доступа";

        var roles = Roles.getInstance();
        return roles.getItem(RoleId);
      }
    }

    public UserAccess()
    {
      ID = 0;
      RoleId = 0;
    }

    internal UserAccess(DataRow row)
    {
      fillFields(row);
    }

    private void fillFields(DataRow row)
    {
      int id;
      int.TryParse(row.ItemArray[0].ToString(), out id);
      ID = id;

      int idRole;
      int.TryParse(row.ItemArray[1].ToString(), out idRole);
      RoleId = idRole;
    }

    internal override object[] getRow()
    {
      return new object[] {ID, Driver.Login, Driver.GetName(NameType.Full), Role};
    }

    internal override void Delete()
    {
      _provider.DoOther("exec UserAccess_Delete @p1, @p2", ID, RoleId);
    }

    public override void Save()
    {
      _provider.Insert("UserAccess", ID, RoleId);
    }
  }
}
