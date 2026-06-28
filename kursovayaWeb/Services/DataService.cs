using System.Net.Http.Json;
using System.Net.Http.Headers;
using kursovayaWeb.Models;

namespace kursovayaWeb.Services
{
	public class DataService
	{
		private  HttpClient _http = new HttpClient();
		//private readonly AuthState _authState; // Внедряем состояние авторизации

		public DataService()
		{
			//_http = http;
			//_authState = authState;
			_http.DefaultRequestHeaders.Add("Authorization", "Bearer " + RegisterUser.access_token);
		}

		// Вспомогательный метод для автоматической подстановки токена в заголовки
		//private void AppendAuthorizationHeader()
		//{
		//	if (_authState.IsAuthenticated && !string.IsNullOrEmpty(_authState.AccessToken))
		//	{
		//		_http.DefaultRequestHeaders.Authorization =
		//			new AuthenticationHeaderValue("Bearer", _authState.AccessToken);
		//	}

		//}

		public async Task<List<ProductionBatch>> GetBatchesAsync()
		{
			//AppendAuthorizationHeader();
			// Используем относительный путь api/...
			var list = await _http.GetFromJsonAsync<List<ProductionBatch>>("api/ProductionBatch");
			if (list != null) return list;
			return null!;
		}

		public async Task<List<Product>> GetProductsAsync()
		{
			//AppendAuthorizationHeader();
			var list = await _http.GetFromJsonAsync<List<Product>>("api/Product");
			return list ?? new List<Product>();
		}

		public async Task<List<BatchStepExecution>> GetStepExecutionsAsync(int batchId)
		{
			//AppendAuthorizationHeader();
			var list = await _http.GetFromJsonAsync<List<BatchStepExecution>>("api/BatchStepExecution/" + batchId);
			return (list ?? new List<BatchStepExecution>())
				.Where(e => e.BatchId == batchId)
				.OrderBy(e => e.StepId)
				.ToList();
		}

		public async Task<List<TechStep>> GetStepsByCardAsync(int cardId)
		{
			//AppendAuthorizationHeader();
			var list = await _http.GetFromJsonAsync<List<TechStep>>("api/TechStep");
			return (list ?? new List<TechStep>())
				.Where(s => s.CardId == cardId)
				.OrderBy(s => s.StepNumber)
				.ToList();
		}

		public async Task<List<ExtruderEvent>> GetExtruderEventsAsync(int executionId)
		{
			//AppendAuthorizationHeader();
			var list = await _http.GetFromJsonAsync<List<ExtruderEvent>>("api/ExtruderEvent");
			return (list ?? new List<ExtruderEvent>())
				.Where(e => e.ExecutionId == executionId)
				.ToList();
		}

		public async Task<bool> StartStepAsync(int executionId, int userId)
		{
			//AppendAuthorizationHeader();
			var payload = new { StartedAt = DateTime.Now, StartedBy = userId, Status = "in_progress" };
			var response = await _http.PatchAsJsonAsync($"api/TechStep/{executionId}", payload);
			return response.IsSuccessStatusCode;
		}

		public async Task<bool> CompleteStepAsync(int executionId, int userId, string actualParams)
		{
			//AppendAuthorizationHeader();
			var payload = new
			{
				CompletedAt = DateTime.Now,
				CompletedBy = userId,
				Status = "completed",
				ActualParams = actualParams
			};
			var response = await _http.PatchAsJsonAsync($"api/TechStep/{executionId}", payload);
			return response.IsSuccessStatusCode;
		}
	}
}
