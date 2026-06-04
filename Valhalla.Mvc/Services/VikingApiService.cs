using Valhalla.Shared.DTOs;
using Valhalla.Shared.Constants;

namespace Valhalla.Mvc.Services
{
    public class VikingApiService
    {
        private readonly HttpClient _client;

        public VikingApiService(HttpClient client)
        {
            _client = client;
        }

        public async Task<List<VikingDto>> GetAll()
           => await _client.GetFromJsonAsync<List<VikingDto>>(ApiRoutes.Vikings.GetAll)
              ?? new List<VikingDto>();

        public async Task Create(VikingDto model)
            => await _client.PostAsJsonAsync(ApiRoutes.Vikings.Create, model);

        public async Task<VikingDto?> GetById(Guid id)
            => await _client.GetFromJsonAsync<VikingDto>(
                   string.Format(ApiRoutes.Vikings.GetById, id));

        public async Task Update(VikingDto model)
        {
            var response = await _client.PutAsJsonAsync(
                string.Format(ApiRoutes.Vikings.Update, model.Id), model);

            response.EnsureSuccessStatusCode();
        }

        public async Task Delete(Guid id)
            => await _client.DeleteAsync(
                   string.Format(ApiRoutes.Vikings.Delete, id));
    }
}

