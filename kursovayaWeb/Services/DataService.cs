using System.Net.Http.Json;
using System.Net.Http.Headers;
using kursovayaWeb.Models;

namespace kursovayaWeb.Services
{
	public class DataService
	{
		private readonly HttpClient _http;

		public DataService(HttpClient http)
		{
			_http = http;
		}


		public async Task<List<ProductionBatch>> GetBatchesAsync()
		{
			_http.DefaultRequestHeaders.Authorization =
	new AuthenticationHeaderValue(
		"Bearer",
		RegisterUser.access_token);
			var list = await _http.GetFromJsonAsync<List<ProductionBatch>>("api/ProductionBatch");
			if (list != null) return list;
			return null!;
		}

		public async Task<List<Product>> GetProductsAsync()
		{
			
			_http.DefaultRequestHeaders.Authorization =
	new AuthenticationHeaderValue(
		"Bearer",
		RegisterUser.access_token);
			var list = await _http.GetFromJsonAsync<List<Product>>("api/Product");
			return list ?? new List<Product>();
		}

		public async Task<List<BatchStepExecution>> GetStepExecutionsAsync(int batchId)
		{
			_http.DefaultRequestHeaders.Authorization =
	new AuthenticationHeaderValue(
		"Bearer",
		RegisterUser.access_token);
			var list = await _http.GetFromJsonAsync<List<BatchStepExecution>>("api/BatchStepExecution");
			return (list ?? new List<BatchStepExecution>())
				.Where(e => e.BatchId == batchId)
				.OrderBy(e => e.StepId)
				.ToList();
		}

		public async Task<List<TechStep>> GetStepsByCardAsync(int cardId)
		{
			_http.DefaultRequestHeaders.Authorization =
	new AuthenticationHeaderValue(
		"Bearer",
		RegisterUser.access_token);
			var list = await _http.GetFromJsonAsync<List<TechStep>>("api/TechStep");
			return (list ?? new List<TechStep>())
				.Where(s => s.CardId == cardId)
				.OrderBy(s => s.StepNumber)
				.ToList();
		}

		public async Task<List<ExtruderEvent>> GetExtruderEventsAsync(int executionId)
		{
			_http.DefaultRequestHeaders.Authorization =
	new AuthenticationHeaderValue(
		"Bearer",
		RegisterUser.access_token);
			var list = await _http.GetFromJsonAsync<List<ExtruderEvent>>("api/ExtruderEvent");
			return (list ?? new List<ExtruderEvent>())
				.Where(e => e.ExecutionId == executionId)
				.ToList();
		}

		public async Task<bool> StartStepAsync(BatchStepExecution execution, int userId)
		{
			_http.DefaultRequestHeaders.Authorization =
	new AuthenticationHeaderValue(
		"Bearer",
		RegisterUser.access_token);
			var payload = new BatchStepExecution
			{
				ExecutionId = execution.ExecutionId,
				BatchId = execution.BatchId,
				StepId = execution.StepId,
				Status = "in_progress",
				StartedAt = DateTime.Now,
				CompletedAt = execution.CompletedAt,
				StartedBy = userId,
				CompletedBy = execution.CompletedBy,
				ActualParams = execution.ActualParams,
				Notes = execution.Notes
			};
			var response = await _http.PutAsJsonAsync($"api/BatchStepExecution/{execution.ExecutionId}", payload);
			return response.IsSuccessStatusCode;
		}

		public async Task<bool> CompleteStepAsync(BatchStepExecution execution, int userId, string actualParams)
		{
			_http.DefaultRequestHeaders.Authorization =
	new AuthenticationHeaderValue(
		"Bearer",
		RegisterUser.access_token);
			var payload = new BatchStepExecution
			{
				ExecutionId = execution.ExecutionId,
				BatchId = execution.BatchId,
				StepId = execution.StepId,
				Status = "completed",
				StartedAt = execution.StartedAt,
				CompletedAt = DateTime.Now,
				StartedBy = execution.StartedBy,
				CompletedBy = userId,
				ActualParams = actualParams,
				Notes = execution.Notes
			};
			var response = await _http.PutAsJsonAsync($"api/BatchStepExecution/{execution.ExecutionId}", payload);
			return response.IsSuccessStatusCode;
		}
	}
}
