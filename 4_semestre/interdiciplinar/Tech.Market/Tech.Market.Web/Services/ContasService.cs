namespace Tech.Market.Web.Services
{
    public class ContasService : IContasService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ContasService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IEnumerable<ContaDTO>> GetAsync()
        {
            using HttpClient http = this._httpClientFactory.CreateClient(URIConts.URL_TECH);
            using HttpResponseMessage response = await http.GetAsync($"/api/contas");
            string content = await response.Content.ReadAsStringAsync();
            IEnumerable<ContaDTO> contas = JsonSerializer.Deserialize<IEnumerable<ContaDTO>>(content)
                    ?? Enumerable.Empty<ContaDTO>();
            return contas;
        }

        public async Task<ContaDTO?> GetAsync(Guid id)
        {
            {
                using HttpClient http = this._httpClientFactory.CreateClient(URIConts.URL_TECH);
                using HttpResponseMessage response = await http.GetAsync($"/api/contas/{id}");
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    ContaDTO conta = JsonSerializer.Deserialize<ContaDTO>(content)!;
                    return conta;
                }
                return null;
            }
        }
    }
}
