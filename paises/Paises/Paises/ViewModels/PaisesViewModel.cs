
namespace Paises.ViewModels
{
    using Paises.Models;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class PaisesViewModel : BaseViewModel
    {
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
            this.LoadLands();
        }
        #endregion

        #region Metodos
        private void LoadLands()
        {

        }

        #endregion
    }
}
