
namespace Paises.Services
{
    using Paises.Models;
    using Plugin.Connectivity;
    using System.Threading.Tasks;

    public class ApiService
    {
        //Provar conexión a internet
        public async Task<Response> CheckConnection()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Prenda el internet por favor"
                };
            }

            var isResachable = await CrossConnectivity.Current.IsRemoteReachable("www.google.com");

            if (!isResachable)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Conecntese a internet"
                };
            }

            return new Response
            {
                IsSuccess = true,
                Message = "Ok"
            };

        }


    }
}
