using System.Drawing;

namespace BBAuto.App.GUI
{
  public static class BBColors
  {
    private static Color _bbGreen1;
    private static Color _bbGreen3;
    private static Color _bbGray4;
    private static Color _bbGray5;

    public static Color bbGreen1
    {
      get { return _bbGreen1; }
    }

    public static Color bbGreen3
    {
      get { return _bbGreen3; }
    }

    public static Color bbGray4
    {
      get { return _bbGray4; }
    }

    public static Color bbGray5
    {
      get { return _bbGray5; }
    }

    static BBColors()
    {
      _bbGreen1 = Color.FromArgb(0, 180, 130);
      _bbGreen3 = Color.FromArgb(115, 214, 186);
      _bbGray4 = Color.FromArgb(150, 150, 150);
      _bbGray5 = Color.FromArgb(230, 230, 230);
    }
  }
}
