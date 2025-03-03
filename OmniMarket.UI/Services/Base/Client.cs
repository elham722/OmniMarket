// OmniMarket.UI/Services/Base/Client.cs
using System.Net.Http;

namespace OmniMarket.UI.Services.Base
{
    public partial class Client(HttpClient httpClient) : IClient
    {
        public HttpClient HttpClient => httpClient;
    }
}