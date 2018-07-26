

namespace Paises.ViewModels
{

    using Paises.Models;
    using Paises.ViewModels.ItemsViewModels;

    public class PaisViewModel
    {
        #region Propiedades
        public Land Pais
        {
            get;
            set;
        }
        #endregion

        #region Constructor
        public PaisViewModel(Land pais)
        {
            this.Pais = pais;
        }
        #endregion

    }
}
