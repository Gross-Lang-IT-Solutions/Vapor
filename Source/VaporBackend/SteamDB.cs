using Newtonsoft.Json;

namespace Vapor;
public static class SteamDB
{
	private const string gameslist = "https://api.steampowered.com/ISteamApps/GetAppList/v2/";
	private const string gameinfo = "https://store.steampowered.com/api/appdetails?appids=";
	private static IEnumerable<GameAppIDPair>? cache;
	private static DateTime lastCacheUpdate = DateTime.MinValue;

	public static IEnumerable<GameAppIDPair> GetListOfGames()
	{
		if (cache is not null && (DateTime.Now - lastCacheUpdate) < new TimeSpan(0, 30, 0))
			return cache;

		List<GameAppIDPair> apps = new();

		using HttpClient web = new();

		string json = web.GetStringAsync(gameslist).GetAwaiter().GetResult();
		apps = JsonConvert.DeserializeObject<List<GameAppIDPair>>(json[json.IndexOf('[')..(json.LastIndexOf(']') + 1)])?? new();
		cache = apps;

		return apps;
	}

	public static GameInfo? GetInfo(int appID)
	{
		GameInfoQuery? query;

		using HttpClient web = new();

		string json = web.GetStringAsync(gameinfo + appID.ToString()).GetAwaiter().GetResult();
		query = JsonConvert.DeserializeObject<GameInfoQuery>(json[json.IndexOf('{', 1)..(json.Length - 1)]);

		return query?.success?? false ? query.data! : null;
	}

	private record GameInfoQuery(bool success, GameInfo? data);
	public record GameAppIDPair(int appid, string name);
	public record GameInfo(string name,
			               int steam_appid,
						   string required_age,
						   string short_description,
						   string header_image,
						   IEnumerable<string> developers,
						   IEnumerable<string> publishers);
}

