using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

namespace Vapor;

[JsonObject(MemberSerialization = Newtonsoft.Json.MemberSerialization.OptIn)]
public sealed class Config
{
	[JsonProperty("games")]
	public readonly List<Game> Games = new();

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
}
