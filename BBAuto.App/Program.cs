using System;
using System.Windows.Forms;
using BBAuto.App.config;
using BBAuto.Logic.DataBase;
using BBAuto.Logic.Static;

namespace BBAuto.App
{
  static class Program
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    static void Main()
    {
      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);

      AutoMapperConfiguration.Initialize();

      WindsorConfiguration.Register();

      DataBase.InitDataBase();
      Provider.InitSQLProvider();

      var container = WindsorConfiguration.Container;

      var form = container.Resolve<IForm>();

      if (User.Login())
        Application.Run((Form)form);
      else
        MessageBox.Show("У вас недостаточно прав для работы с программой", "Доступ заблокирован", MessageBoxButtons.OK,
          MessageBoxIcon.Warning);
    }
  }
}
