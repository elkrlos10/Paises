

namespace Paises.ViewModels
{

    using Paises.Models;
    using Paises.ViewModels.ItemsViewModels;
    using System;
    using System.Collections.ObjectModel;
    using System.Linq;

    public class PaisViewModel : BaseViewModel
    {
        #region Atributos
        private ObservableCollection<Borders> borders;
        private ObservableCollection<Currency> currencies;
        private ObservableCollection<Language> languages;
        #endregion

        #region Propiedades
        public Land Pais { get; set; }

        public ObservableCollection<Borders> Borders
        {
            get { return this.borders; }
            set { SetValue(ref this.borders, value); }
        }

        public ObservableCollection<Currency> Currencies
        {
            get { return this.currencies; }
            set { SetValue(ref this.currencies, value); }
        }

        public ObservableCollection<Language> Languages
        {
            get { return this.languages; }
            set { SetValue(ref this.languages, value); }
        }
        #endregion

        #region Constructor
        public PaisViewModel(Land pais)
        {
            this.Pais = pais;
            this.LoadBorders();
            this.Currencies = new ObservableCollection<Currency>(this.Pais.Currencies);
            this.Languages = new ObservableCollection<Language>(this.Pais.Languages);
        }
        #endregion

        #region Metodos
        private void LoadBorders()
        {
            this.Borders = new ObservableCollection<Borders>();
            //El país seleccionado de la lista de paises y que llega por medio del constructor
            //tiene en sus propiedades una lista de paises con los que limita, dicha lista se recorre 
            //para encontrar el nombre completo de los paises limitantes
            foreach (var item in this.Pais.Borders)
            {
                var pais = MainViewModel.GetInstance().LandsList.
                                         Where(p => p.Alpha3Code == item).
                                         FirstOrDefault();

                if (pais != null)
                {
                    this.Borders.Add(new Borders
                    {
                        Code = pais.Alpha3Code,
                        Name = pais.Name
                    });
                }
            }
        }
        #endregion

    }
}
