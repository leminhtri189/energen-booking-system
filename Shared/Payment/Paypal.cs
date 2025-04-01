using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Shared.Payment
{
    public class PayPal
    {
        public string? Mode { get; set; }
        public string? ClientId { get; set; }
        public string? ClientSecret { get; set; }
        public string? BaseUrl { get; set; }
        public string? Intent { get; set; }
        public string? CurrencyCode { get; set; }

        #region Request Process
        public async Task<string> GetAccessToken()
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"{BaseUrl}/v1/oauth2/token");
            var authHeader = Convert.ToBase64String(
                Encoding.UTF8.GetBytes($"{ClientId}:{ClientSecret}")
            );
            request.Headers.Authorization = new AuthenticationHeaderValue("Basic", authHeader);
            request.Content = new StringContent(
                "grant_type=client_credentials",
                Encoding.UTF8,
                "application/x-www-form-urlencoded"
            );
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.SendAsync(request);
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException("Do not Get Access Token from PayPal.");
                }

                var responseContent = await response.Content.ReadAsStringAsync();
                var jsonResponse = JsonSerializer.Deserialize<JsonElement>(responseContent);

                return jsonResponse.GetProperty("access_token").GetString();
            }
        }

        public async Task<string> CreatePaypalOrderDetail(
            decimal amount,
            string accessToken,
            string returnUrl
        )
        {
            try
            {
                var httpClient = new HttpClient();
                var url = $"{BaseUrl}/v2/checkout/orders";

                var payload = new
                {
                    intent = Intent,
                    purchase_units = new[]
                    {
                        new { amount = new { currency_code = CurrencyCode, value = amount } },
                    },
                    application_context = new
                    {
                        return_url = returnUrl,
                        cancel_url = returnUrl + "?Cancel=true&",
                    },
                };

                var requestMessage = new HttpRequestMessage(HttpMethod.Post, url);
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue(
                    "Bearer",
                    accessToken
                );
                requestMessage.Headers.Add(
                    "PayPal-Request-Id",
                    DateTimeOffset.Now.ToUnixTimeMilliseconds().ToString()
                );

                var content = new StringContent(
                    JsonSerializer.Serialize(payload),
                    Encoding.UTF8,
                    "application/json"
                );
                requestMessage.Content = content;

                var response = await httpClient.SendAsync(requestMessage);
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException("Do not create PayPal Oder.");
                }

                var responseContent = await response.Content.ReadAsStringAsync();
                var jsonResponse = JsonSerializer.Deserialize<JsonElement>(responseContent);

                var approvalLink = jsonResponse
                    .GetProperty("links")
                    .EnumerateArray()
                    .First(link => link.GetProperty("rel").GetString() == "approve")
                    .GetProperty("href")
                    .GetString();

                return approvalLink;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
        #endregion
        #region Response Request
        public async Task<JsonElement> SendCaptureRequest(string token)
        {
            using (var httpClient = new HttpClient())
            {
                var accessToken = await GetAccessToken();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(
                    "Bearer",
                    accessToken
                );

                var request = new HttpRequestMessage(
                    HttpMethod.Post,
                    $"{BaseUrl}/v2/checkout/orders/{token}/capture"
                );
                request.Headers.Authorization = new AuthenticationHeaderValue(
                    "Bearer",
                    accessToken
                );
                request.Content = new StringContent("{}", Encoding.UTF8, "application/json"); // Nội dung JSON trống

                var response = await httpClient.SendAsync(request);

                var responseContent = await response.Content.ReadAsStringAsync();

                Console.WriteLine($"Phản hồi từ PayPal: {responseContent}");

                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException(
                        $"Không thể capture thanh toán từ PayPal. Phản hồi từ PayPal: {responseContent}"
                    );
                }
                return JsonSerializer.Deserialize<JsonElement>(responseContent);
            }
        }
        #endregion
    }
}
