namespace Pinewood.DMSSample.Business.Clients
{
    public class PartAvailabilityClient : IPartAvailabilityClient
    {
        private readonly HttpClient _httpClient;

        public PartAvailabilityClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<int> GetAvailability(string stockCode)
        {
            HttpResponseMessage responseMessage = await _httpClient.GetAsync($"https://www.api.pinewood.com/parts/availability/{stockCode}");

            if (!responseMessage.IsSuccessStatusCode)
                throw new Exception($"Could not get part availability for {stockCode}");

            string responseString = await responseMessage.Content.ReadAsStringAsync();
            return int.Parse(responseString);
        }
    }
}
