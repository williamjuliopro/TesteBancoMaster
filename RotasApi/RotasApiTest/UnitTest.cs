using System.Net;

namespace PediosApiTest
{
    public class UnitTest1
    {

        static HttpClient client = new HttpClient();

        [Fact]
        public async void RotaDeViagem()
        {
            //Arrange            
            var origem = "GRU";
            var destino = "CDG";

            var RoutaExperada = "Melhor rota: GRU - BRC - SCL - ORL - CDG ao custo de $40";
            var result = string.Empty;

            //Act            
            Uri uri = new Uri("https://localhost:7198/" + string.Format("Rotas?origem={0}&destino={1}", origem, destino));
            var response = await client.GetAsync(uri);
            if (response.IsSuccessStatusCode)
            {
                result = response.Content.ReadAsStringAsync().Result;                                                                    
            }            

            //Assert
            Assert.Equal(RoutaExperada, result);            
        }
    }
}