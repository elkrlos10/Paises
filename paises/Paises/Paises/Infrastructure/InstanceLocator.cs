using System;
using System.Collections.Generic;
using System.Text;

namespace Paises.Infrastructure
{
    using ViewModels;

    public class InstanceLocator
    {
        #region Propiedades

        public MainViewModel Main { get; set; }

        #endregion

        #region Contructor

        public InstanceLocator()
        {
            this.Main = new MainViewModel();
        }
         
        #endregion

    }
}
