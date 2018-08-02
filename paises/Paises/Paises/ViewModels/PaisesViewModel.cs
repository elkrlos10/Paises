
namespace Paises.ViewModels
{
    using GalaSoft.MvvmLight.Command;
    using Paises.Models;
    using Paises.Services;
    using Paises.ViewModels.ItemsViewModels;
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
        //PaisItemViewModel es un espejo del modelo o clase land de la cual hereda, para no dañar
        //la arquitectura y poder agregar el command en la itemViewModel
        private ObservableCollection<PaisItemViewModel> paises;
        private bool isRefreshing;
        private string filter;
       
        #endregion

        #region Propiedades 
        public ObservableCollection<PaisItemViewModel> Paises
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

            //casteando la respuesta del apiService a una lista de paises y asignando al atributo tipo lista
            MainViewModel.GetInstance().LandsList = (List<Land>)response.Result;

            //Convertir la lista a una ObservableCollection 
            this.Paises = new ObservableCollection<PaisItemViewModel>(this.TolandItemViewModel());
            this.IsRefreshing = false;
        }

        //Metodo para convertir la lista landlist a una lista de PaisItemViewModel
        private IEnumerable<PaisItemViewModel> TolandItemViewModel()
        {
            return MainViewModel.GetInstance().LandsList.Select(l => new PaisItemViewModel
            {
                Alpha2Code = l.Alpha2Code,
                Alpha3Code = l.Alpha3Code,
                AltSpellings = l.AltSpellings,
                Area = l.Area,
                Borders = l.Borders,
                Capital = l.Capital,
                Cioc = l.Cioc,
                Currencies = l.Currencies,
                Demonym = l.Demonym,
                Flag = l.Flag,
                Gini = l.Gini,
                Languages = l.Languages,
                Latlng = l.Latlng,
                Name = l.Name,
                NativeName = l.NativeName,
                NumericCode = l.NumericCode,
                Population = l.Population,
                Region = l.Region,
                RegionalBlocs = l.RegionalBlocs,
                Subregion = l.Subregion,
                Timezones = l.Timezones,
                TopLevelDomain = l.TopLevelDomain,
                Translations = l.Translations,
            });
        }

        private void Search()
        {
            //Si el filtro esta vacio cargar todos los paises
            if (string.IsNullOrEmpty(this.Filter))
            {
                this.Paises = new ObservableCollection<PaisItemViewModel>(this.TolandItemViewModel());
            }
            else
            {
                this.Paises = new ObservableCollection<PaisItemViewModel>(
                   this.TolandItemViewModel().Where(l => l.Name.ToLower().Contains(this.Filter.ToLower())
                                          || l.Capital.ToLower().Contains(this.Filter.ToLower())));
            }
        }
        #endregion

    }
}
  