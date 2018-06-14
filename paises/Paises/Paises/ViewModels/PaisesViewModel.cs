
namespace Paises.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Paises.Models;
    using Paises.Services;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using Xamarin.Forms;


    //Hereda de la BaseViewModel que contiene el metodo general para refrescar las propiedades 
    public class PaisesViewModel : BaseViewModel
    {
        #region Service
        private ApiService apiService;
        #endregion

        //Finalidad para poder refrescar los controles en tiempo de ejecución
        #region Atributos 
        private ObservableCollection<Land> paises;
        private bool isRefreshing;
        private string filter;
        private List<Land> landsList;
        #endregion

        #region Propiedades 
        public ObservableCollection<Land> Paises
        {
            get { return this.paises; }
            set { SetValue(ref this.paises, value); }
        }

        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { SetValue(ref this.isRefreshing, value); }
        }

        public string Filter
        {
            get { return this.filter; }
            set
            {
                SetValue(ref this.filter, value);
                //Una vez actualice la propiedad la consulta se ejecuta de nuevo 
                this.Search();
            }
        }
        #endregion

        #region Contructors 

        public PaisesViewModel()
        {
            //Instancia de la api apenas ingresa a la vista paises
            this.apiService = new ApiService();
            //Carga los paises 
            this.LoadLands();
        }
        #endregion

        #region Metodos
        private async void LoadLands()
        {
            //Refrescar la lista
            this.IsRefreshing = true;
            //Proando la conexión
            var connection = await this.apiService.CheckConnection();

            if (!connection.IsSuccess)
            {
                this.IsRefreshing = false;
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

            //Validar la respuesta del api
            if (!response.IsSuccess)
            {
                await Application.Current.MainPage.DisplayAlert("Error", response.Message, "Aceptar");

                //Devolverme a la pagina anterior
                await Application.Current.MainPage.Navigation.PopAsync();
                return;
            }

            //casteando la respuesta a una lista de paises y asignando al atributo tipo lista
            this.landsList = (List<Land>)response.Result;

            //Convertir la lista a una ObservableCollection 
            this.Paises = new ObservableCollection<Land>(this.landsList);
            this.IsRefreshing = false;
        }
        #endregion

        #region Commands
        // Propiedad
        public ICommand RefreshCommand
        {
            get
            {
                return new RelayCommand(LoadLands);
            }
        }

        public ICommand SearchCommand
        {
            get
            {
                return new RelayCommand(Search);
            }
        }

        private void Search()
        {
            //Si el filtro esta vacio cargar todos los paises
            if (string.IsNullOrEmpty(this.Filter))
            {
                this.Paises = new ObservableCollection<Land>(this.landsList);
            }
            else
            {
                this.Paises = new ObservableCollection<Land>(
                    this.landsList.Where(l =>l.Name.ToLower().Contains(this.Filter.ToLower())
                    || l.Capital.ToLower().Contains(this.Filter.ToLower())));
            }
        }

        #endregion
    }
}
