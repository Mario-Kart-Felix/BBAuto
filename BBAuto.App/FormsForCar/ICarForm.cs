using System.Windows.Forms;
using BBAuto.Logic.Entities;

namespace BBAuto.App.FormsForCar
{
  public interface ICarForm : IForm
  {
    DialogResult ShowDialog(Car car);
  }
}
