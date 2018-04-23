using System.Windows.Forms;
using BBAuto.Logic.Entities;

namespace BBAuto.App.FormsForCar
{
  public interface ICarForm
  {
    DialogResult ShowDialog(Car car);
  }
}
