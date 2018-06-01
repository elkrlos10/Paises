
namespace Paises
{
    using Views;
    using Xamarin.Forms;

    public partial class App : Application
	{
        #region Constructors
        public App()
        {
            InitializeComponent();

            this.MainPage = new LoginPage();
        }

        #endregion
 
        #region Metodos
        protected override void OnStart()
        {
            // Cuando la app de ejecuta
        }

        protected override void OnSleep()
        {
            //Dormir la app
        }

        protected override void OnResume()
        {
            // Despertar la app
        } 
        #endregion
    }
}
