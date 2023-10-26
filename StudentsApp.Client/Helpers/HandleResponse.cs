using StudentsApp.Client.Models;
using System.Text.Json;

namespace StudentsApp.Client.Helpers
{
    public class HandleResponse
    {
        public string errorDivMessage { get; private set;}
        public string errorDivDetails { get; private set; }

        public HandleResponse()
        {
        }

        public void HandleError(string jsonResponse)
        {
            try
            {
                var response = JsonSerializer.Deserialize<ApiResponse>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (response != null && !response.Succeeded)
                {
                    errorDivMessage = response.Message != null ? response.Message : string.Empty;
                    errorDivDetails = response.Errors != null ? string.Join(", ", response.Errors) : string.Empty;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
