using System.Drawing;
using System.Windows.Forms;
using PresentationControls;

namespace BBAuto.App.GUI
{
  internal class ControlsFactory
  {
    internal Button CreateButton(string Name, string Text, Point point, Size size)
    {
      Button button = new Button();
      button.Name = Name;
      button.Text = Text;
      button.Location = point;
      button.Size = size;
      button.Anchor = (AnchorStyles.Right | AnchorStyles.Top);

      return button;
    }

    public void AddLabel()
    {
      Label label = new System.Windows.Forms.Label();
      label.AutoSize = true;
      label.Location = new System.Drawing.Point(10, 14);
      label.Name = "label1";
      label.Size = new System.Drawing.Size(59, 13);
      label.TabIndex = 9;
      label.Text = "Фамилия:";
    }

    internal Label CreateLabelForFilter(int pointY, string Text)
    {
      Label label = new Label();
      label.Location = new Point(pointY, 56);
      label.Name = string.Concat("label", Text);
      label.Text = string.Concat(Text, ":");

      return label;
    }

    internal CheckBoxComboBox CreateComboBoxForFilter(int pointY, string Text)
    {
      CheckBoxComboBox combo = new CheckBoxComboBox();
      combo.Location = new Point(pointY, 70);
      combo.Name = Text;
      combo.DropDownStyle = ComboBoxStyle.DropDownList;
      combo.DropDownHeight = 500;

      return combo;
    }
  }
}
