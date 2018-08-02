

namespace Paises.ViewModels
{
    using Paises.Models;
    using System.Collections.Generic;
 
    public class MainViewModel
    {
        //Propiedades tipo ViewModel de cada vista 
        #region ViewModel
        public LoginViewModel Login { get; set; }
        public PaisesViewModel Paises { get; set; }
        public PaisViewModel Pais { get; set; }
        #endregion

        #region Propiedades

        public List<Land> LandsList { get; set; }

        #endregion



        #region Contructores

        public MainViewModel()
        {
            //Instanciado esta clase
            instance = this;
            //Instanciando la loginViewModel 
            this.Login = new LoginViewModel();
        }
        #endregion

        #region Singleton
        private static MainViewModel instance;
        public static MainViewModel GetInstance()
        {
            if (instance== null)
            {
                return new MainViewModel();
            }
            return instance;
        }
        #endregion
    }
}
