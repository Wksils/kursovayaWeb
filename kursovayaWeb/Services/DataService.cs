using System.Net.Http.Json;
using kursovayaWeb.Models;

namespace kursovayaWeb.Services
{
    // Аналог твоего GetListsService из WPF - один сервис на все GET-запросы
    public class DataService
    {
        private readonly HttpClient _http;

        public DataService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<ProductionBatch>> GetBatchesAsync()
        {
            var list = await _http.GetFromJsonAsync<List<ProductionBatch>>("http://localhost:5043/api/ProductionBatch");
            return list ?? new List<ProductionBatch>();
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            var list = await _http.GetFromJsonAsync<List<Product>>("http://localhost:5043/api/Product");
            return list ?? new List<Product>();
        }

        public async Task<List<BatchStepExecution>> GetStepExecutionsAsync(int batchId)
        {
            var list = await _http.GetFromJsonAsync<List<BatchStepExecution>>("http://localhost:5043/api/BatchStepExecution/" + batchId);
            return (list ?? new List<BatchStepExecution>())
                .Where(e => e.BatchId == batchId)
                .OrderBy(e => e.StepId)
                .ToList();
        }

        public async Task<List<TechStep>> GetStepsByCardAsync(int cardId)
        {
            var list = await _http.GetFromJsonAsync<List<TechStep>>("http://localhost:5043/api/TechStep");
            return (list ?? new List<TechStep>())
                .Where(s => s.CardId == cardId)
                .OrderBy(s => s.StepNumber)
                .ToList();
        }

        public async Task<List<ExtruderEvent>> GetExtruderEventsAsync(int executionId)
        {
            var list = await _http.GetFromJsonAsync<List<ExtruderEvent>>("http://localhost:5043/api/ExtruderEvent");
            return (list ?? new List<ExtruderEvent>())
                .Where(e => e.ExecutionId == executionId)
                .ToList();
        }

        // PATCH-запрос для начала шага
        public async Task<bool> StartStepAsync(int executionId, int userId)
        {
            var payload = new { StartedAt = DateTime.Now, StartedBy = userId, Status = "in_progress" };
            var response = await _http.PatchAsJsonAsync($"http://localhost:5043/api/TechStep/{executionId}", payload);
            return response.IsSuccessStatusCode;
        }

        // PATCH-запрос для завершения шага
        public async Task<bool> CompleteStepAsync(int executionId, int userId, string actualParams)
        {
            var payload = new
            {
                CompletedAt = DateTime.Now,
                CompletedBy = userId,
                Status = "completed",
                ActualParams = actualParams
            };
            var response = await _http.PatchAsJsonAsync($"http://localhost:5043/api/TechStep/{executionId}", payload);
            return response.IsSuccessStatusCode;
        }
    }
}
