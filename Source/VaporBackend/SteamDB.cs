using Newtonsoft.Json;

namespace Vapor;
public static class SteamDB
{
	private const string gameslist = "https://api.steampowered.com/ISteamApps/GetAppList/v2/";
	public static IEnumerable<GameAppIDPair> GetListOfGames()
	{
		List<GameAppIDPair> apps = new();

		using HttpClient web = new();

		string json = web.GetStringAsync(gameslist).GetAwaiter().GetResult();
		Console.WriteLine(json);

		apps = JsonConvert.DeserializeObject<List<GameAppIDPair>>(json[json.IndexOf('[')..(json.LastIndexOf(']') + 1)])?? new();

		return apps;
	}
	public record GameAppIDPair(int appid, string name);
	public record GameInfo(string name,
			               int steam_appid,
						   string required_age,
						   string short_description,
						   string header_image,
						   IEnumerable<string> developers,
						   IEnumerable<string> publishers);
}

