
namespace Paises.ViewModels.ItemsViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Paises.Models;
    using Paises.Views;
    using System;
    using System.Windows.Input;
    using Xamarin.Forms;

    public class PaisItemViewModel : Land
    {
        #region Commands
        public ICommand SelectPaisCommand
        {
            get
            {
                return new RelayCommand(SelectPais);

            }
        }

        private async void SelectPais()
        {
            MainViewModel.GetInstance().Pais = new PaisViewModel(this);
            await Application.Current.MainPage.Navigation.PushAsync(new PaisTabbedPage());
        }

        #endregion
    }
}
