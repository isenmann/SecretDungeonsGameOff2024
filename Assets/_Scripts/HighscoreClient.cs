
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

public class HighscoreClient
{
    private HttpClient _httpClient;
    private string _version = "0.1";
    
    public HighscoreClient()
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new System.Uri("https://<secret-url>")
        };
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("ApiKey", "<secret>");
    }

    public async Task<List<HighscoreEntryDto>> GetHighScore()
    {
        var request = await _httpClient.GetAsync($"Highscore?Version={_version}");
        if (!request.IsSuccessStatusCode)
        {
            return null;
        }

        var jsonString = await request.Content.ReadAsStringAsync();

        var highscore = JsonConvert.DeserializeObject<List<HighscoreEntryDto>>(jsonString);
        return highscore;
    }
    
    public async Task<long> SendHighscore(string username, long score)
    {
        var request = new AddScoreRequest
        {
            Name = username,
            Score = score,
            Version = _version
        };

        using var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("Highscore", content);
        if (!response.IsSuccessStatusCode)
        {
            return -1;
        }

        var rank = await response.Content.ReadAsStringAsync();
        return int.Parse(rank);
    }
}

public class AddScoreRequest
{
    public string Name { get; set; }
    public long Score { get; set; }
    public string Version { get; set; }
}

public class HighscoreEntryDto
{
    public string Name { get; set; }
    public long Score { get; set; }
}
