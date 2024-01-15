using System;
using System.Net.Http.Json;
using System.Numerics;
using System.Text;
using System.Text.Json;
using MovieDbApi;

namespace MovieDbApi { 
internal class Program {
        static async Task Main()
        {
            string apiUrl = "https://api4.thetvdb.com/v4/series/72924";
            Environment.SetEnvironmentVariable("SHOWS_API_KEY", "20edbba5-4778-446d-bef9-3265259424b7");
            string? apiKey = Environment.GetEnvironmentVariable("SHOWS_API_KEY", EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable("SHOW_BEARER_TOKEN", "eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJhZ2UiOiIiLCJhcGlrZXkiOiIyMGVkYmJhNS00Nzc4LTQ0NmQtYmVmOS0zMjY1MjU5NDI0YjciLCJjb21tdW5pdHlfc3VwcG9ydGVkIjpmYWxzZSwiZXhwIjoxNzA3ODg4NTIzLCJnZW5kZXIiOiIiLCJoaXRzX3Blcl9kYXkiOjEwMDAwMDAwMCwiaGl0c19wZXJfbW9udGgiOjEwMDAwMDAwMCwiaWQiOiIyNDUyOTM3IiwiaXNfbW9kIjpmYWxzZSwiaXNfc3lzdGVtX2tleSI6ZmFsc2UsImlzX3RydXN0ZWQiOmZhbHNlLCJwaW4iOiIiLCJyb2xlcyI6W10sInRlbmFudCI6InR2ZGIiLCJ1dWlkIjoiIn0.k6Tp19Aml-EIypCvtWp6UVjQ03cNlosavC37TYvX-EfktVNTe0nIkPrgHkriW1_EtugZzW4jdXHu0k6hrfAc1tsVk2g-B3Hk0RMDdnFXGu8jIovXopVvRwDm1xJRy6Aeube8lbIRHhaQYnVaRsCda3xmx09PQ_O6-giBw2pHt72WpS3w7pUcfk7A13Wj2rySuH1y_eoLGDecrAYEh4niaRCQCqW8pJ1DFl_y9ZbwiGyYjZ12Gw39gVFkwnHvCNN4pKHmsKF7F64NGatzYxtfwxK85wemNbTBN4DyOb4o7jE9DuM3cdxOWtez80ZDIYfqmnuv1gjFeP0ospHOfKL7HCI_786RMiQnaUf8QmGivpuwjN9fMOAnTuJM9COVe-MOKK_DUGjsc_G_gF38pHz7lsezBD_nwAmpwQLRHk5YdQPhUMH1bp4Xop1mfqPOIdWgfOVyVMeTrWIJnw7jd-F-FbHu9KFk6Zt6ApCYiTbIiqREyBGUwxjegPCuFGDLegmKcVbOb4Kdy9X736PwYHRlpRbzuNFaV1sflW8Fum5X3eaJ_M26U4w4ja4Z8UUMe1Kjw1BdFxHIUuGKxhdsJTzHeSsfiOeWmYgDgGr6gJDq1R9kxLD_SOUlznW-vVA_l9GC4m5QAt1UWRCDiI-cR1OWDmEFXYuoLcSVh0MErBpvMKQ");
            string? accessToken = Environment.GetEnvironmentVariable("SHOW_BEARER_TOKEN");
            using (HttpClient client = new HttpClient())
            {

                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
                string responseJson = await GetOneShowById(new Uri(apiUrl), client: client);
                if (!string.IsNullOrEmpty(responseJson))
                {
                    Show show = MapJsonToShow(responseJson);
                    Console.WriteLine($"Show Name: {show.Name}, Id: {show.Id}, Description: {show.Description}");
                }

            }
        }

        static async Task<string?> GetApiResponse(string apiKey, System.Uri apiUrl, HttpClient client)

        {

            using (HttpResponseMessage response = await client.GetAsync(apiUrl))
            {
                if (response.IsSuccessStatusCode)
                {
                    await Console.Out.WriteLineAsync(response.StatusCode.ToString());
                    string show = await response.Content.ReadAsStringAsync();
                    await Console.Out.WriteLineAsync(show);
                    return show;
                }
                else
                {
                    string error = response.StatusCode.ToString();
                    return error;
                }


            }
        }

   static async Task<string> GetOneShowById(System.Uri apiUrl, HttpClient client)
    {
        using(HttpResponseMessage response = await client.GetAsync(apiUrl))
        {
            if (response.IsSuccessStatusCode)
            {
                await Console.Out.WriteLineAsync(response.StatusCode.ToString());
                return await response.Content.ReadAsStringAsync();
               
            }
            else
            {
                await Console.Out.WriteLineAsync($"Error: {response.StatusCode}");
                return null;
            }
        }
    }

    static Show MapJsonToShow(string json)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        var apiResponse = JsonSerializer.Deserialize<ApiResponse>(json, options);

        return new Show
        {
            Id = apiResponse.Data.Id,
            Name = apiResponse.Data.Name,
            Description = apiResponse.Data.Overview,
            ImageUrl = apiResponse.Data.Image,
            ReleaseDate = DateTime.Parse(apiResponse.Data.FirstAired),
            FinalEpisodeAired = !string.IsNullOrEmpty(apiResponse.Data.LastAired) ? (DateTime?)DateTime.Parse(apiResponse.Data.LastAired) : null,
            Score = apiResponse.Data.Score,
            Status = apiResponse.Data.Status.Name,
            OriginalCountry = apiResponse.Data.OriginalCountry,
            OriginalLanguage = apiResponse.Data.OriginalLanguage
        };
        
    }

    async void PostLogin(string apikey, System.Uri apiUrl, HttpClient client)
    {
        Login login = new Login
        {
            ApiKey = apikey
        };
        var json = JsonSerializer.Serialize(login);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var response = client.PostAsync("login", content).Result;
        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            await Console.Out.WriteLineAsync(responseContent);
        }
        else
        {
            await Console.Out.WriteLineAsync("Error: " + response.StatusCode);
        }

    }
}
}

