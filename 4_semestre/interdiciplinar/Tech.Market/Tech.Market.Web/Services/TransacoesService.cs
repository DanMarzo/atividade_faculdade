namespace Tech.Market.Web.Services
{
    public class TransacoesService : ITransacoesService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public TransacoesService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IEnumerable<TransacaoDTO>> GetAsync(Guid? idConta = null)
        {
            using HttpClient http = this._httpClientFactory.CreateClient(URIConts.URL_TECH);
            http.DefaultRequestHeaders.Add("idConta", idConta.ToString());
            using HttpResponseMessage response = await http.GetAsync($"/api/transacoes");
            if(response.IsSuccessStatusCode)
            {
                string content = await response.Content.ReadAsStringAsync();
                IEnumerable<TransacaoDTO> transacoes = JsonSerializer.Deserialize<IEnumerable<TransacaoDTO>>(content)
                        ?? Enumerable.Empty<TransacaoDTO>();
                return transacoes;
            }
            return Enumerable.Empty<TransacaoDTO>();
        }
    }
}
