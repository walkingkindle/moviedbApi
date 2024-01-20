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
            Environment.SetEnvironmentVariable("SHOWS_API_KEY", "api-key");
            string? apiKey = Environment.GetEnvironmentVariable("SHOWS_API_KEY", EnvironmentVariableTarget.Process);
            Environment.SetEnvironmentVariable("SHOW_BEARER_TOKEN", "bearer");
            string? accessToken = Environment.GetEnvironmentVariable("SHOW_BEARER_TOKEN");

            using (HttpClient client = new HttpClient())
            {

                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {accessToken}");
               for(int i = 192; i < 1000; i++) {
                    await Console.Out.WriteLineAsync($"Current page:" + i.ToString());
                    string apiUrl = "https://api4.thetvdb.com/v4/series?page=" + i.ToString();
                    string responseJson = await GetApiResponse(apiKey: apiKey, apiUrl: new Uri(apiUrl), client: client);
                    if (responseJson != null)
                    {
                        List<Show> shows = MapJsonToShow(responseJson);
                        addToDatabase(shows);

                    }
                    else
                    {
                        await Console.Out.WriteLineAsync("Page number " + i.ToString());
                        await Console.Out.WriteLineAsync("It looks like it's done.");
                    }

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
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    await Console.Out.WriteLineAsync($"Error: {response.StatusCode}");
                    return null;
                }
            }

        }

   static async Task<string?> GetOneShowById(System.Uri apiUrl, HttpClient client)
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

    static List<Show>? MapJsonToShow(string json)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
        var apiResponse = JsonSerializer.Deserialize<ApiResponse>(json, options);

            var shows = new List<Show>();

            if(apiResponse != null && apiResponse.Data != null)
            {
                try
                {
                    foreach (var showData in apiResponse.Data)
                    {
                        shows.Add(new Show
                        {
                            Name = showData.Name,
                            Description = showData.Overview,
                            ImageUrl = showData.Image,
                            ReleaseDate = DateTime.Parse(showData.FirstAired),
                            FinalEpisodeAired = !string.IsNullOrEmpty(showData.LastAired) ? (DateTime?)DateTime.Parse(showData.LastAired) : null,
                            Score = showData.Score,
                            Status = showData.Status.Name,
                            OriginalCountry = showData.OriginalCountry,
                            OriginalLanguage = showData.OriginalLanguage
                        });
                    }
                }catch (System.ArgumentNullException e)
                {
                    Console.WriteLine("We will skip one show.");
                   
                }catch(System.FormatException s)
                {
                    Console.WriteLine("Could not add show");
                }
                           
                }
            return shows;
        
    }

    public static void addToDatabase(List<Show> shows)
        {
            using(var context = new AppDbContext())
            {
                foreach(var show in shows)
                {
                    var showExists = context.Shows.FirstOrDefault(s => s.Name == show.Name);
                    if (showExists == null)
                    {
                        context.Shows.Add(show);
                        Console.WriteLine("One show added.");
                    }
                    else
                    {
                        Console.WriteLine($"Show with name {show.Name} already exists.");
                    }
                }
                context.SaveChanges();
            }
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

