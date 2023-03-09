using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

namespace Vapor;

[JsonObject(MemberSerialization = MemberSerialization.OptIn)]
public sealed class Config
{
	[JsonProperty("games")]
	private readonly Dictionary<Guid, Game> games = new();

	public IReadOnlyDictionary<Guid, Game> Games => games;

	public void AddGame(Game game)
	{
		Guid guid;

		do
		{
			guid = Guid.NewGuid();
		}
		while (games.ContainsKey(guid));

		games.Add(guid, game.Copy());
	}

	public void RemoveGame(Guid guid)
	{
		games.Remove(guid);
	}

	public Game GetGame(Guid guid)
	{
		return games[guid];
	}

	public IEnumerable<KeyValuePair<Guid, Game>> SearchFor(string s)
	{
		GameSorter cmp = new();
		cmp.Search = s;

		return this.Games.Order(cmp);
	}

	private static string XorCipher(string str)
	{
		var chars = str.ToCharArray();

		for (var i = 0; i < str.Length; i++)
			chars[i] = (char)(chars[i] ^ 0xABCD);

		return new string(chars);
	}

	public void Save(string filePath)
	{
		var jsonString = JsonConvert.SerializeObject(this);

		jsonString = XorCipher(jsonString);

		File.WriteAllText(filePath, jsonString);
	}

	public static bool TryLoad(string filePath, [MaybeNullWhen(false)] out Config config)
	{
		if (!File.Exists(filePath))
		{
			config = null;
			return false;
		}

		var jsonString = File.ReadAllText(filePath);
		jsonString = XorCipher(jsonString);

		config = JsonConvert.DeserializeObject<Config>(jsonString);
		return config != null;
	}

	private class GameSorter : IComparer<KeyValuePair<Guid, Game>>
	{
		public string Search { get; set; } = string.Empty;
		public int Compare(KeyValuePair<Guid, Game> x, KeyValuePair<Guid, Game> y)
		{
			double xValue = int.MaxValue;
			double yValue = int.MaxValue;

			xValue *= Math.Max(
					Utils.StringComparer(this.Search, x.Key.ToString()),
					Utils.StringComparer(this.Search, x.Value.Name));
			yValue *= Math.Max(
					Utils.StringComparer(this.Search, y.Key.ToString()),
					Utils.StringComparer(this.Search, y.Value.Name));

			return (int) (yValue - xValue);
		}
	}
}
