
namespace Paises.ViewModels
{
    using Paises.Models;
    using Paises.Services;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using Xamarin.Forms;

    public class PaisesViewModel : BaseViewModel
    {
        #region Service
        private ApiService apiService;
        #endregion

        #region Atributos 
        private ObservableCollection<Land> paises;
        #endregion

        #region Propiedades 
        public ObservableCollection<Land> Paises
        {
            get { return this.paises; }
            set { SetValue(ref this.paises, value); }
        }
        #endregion

        #region Contructors 

        public PaisesViewModel()
        {
            this.apiService = new ApiService();
            this.LoadLands();
        }
        #endregion

        #region Metodos
        private async void LoadLands()
        {
            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                //Probar conexión a internet
                await Application.Current.MainPage.DisplayAlert("Error", connection.Message, "Aceptar");

                //Regresar pagina anterior 
                await Application.Current.MainPage.Navigation.PopAsync();
                return;
            }

            var response = await this.apiService.GetList<Land>(
                "http://restcountries.eu",
                "/rest",
                "/v2/all");

            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");

                //Devolverme a la pagina anterior
                await Application.Current.MainPage.Navigation.PopAsync();
                return;
            }

            //casteando la respuesta a una lista de paises
            var lista = (List<Land>)response.Result;

            //Convertir la lista a una ObservableCollection 
            this.Paises = new ObservableCollection<Land>(lista);

        }

        #endregion
    }
}
