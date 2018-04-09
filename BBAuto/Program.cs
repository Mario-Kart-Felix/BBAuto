using System;
using System.Windows.Forms;
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

      DataBase.InitDataBase();
      Provider.InitSQLProvider();

      if (User.Login())
        Application.Run(new mainForm());
      else
        MessageBox.Show("У вас недостаточно прав для работы с программой", "Доступ заблокирован", MessageBoxButtons.OK,
          MessageBoxIcon.Warning);
    }
  }
}
