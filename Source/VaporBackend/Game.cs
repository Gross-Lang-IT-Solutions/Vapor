using Newtonsoft.Json;

namespace Vapor;

[JsonObject(MemberSerialization = Newtonsoft.Json.MemberSerialization.OptIn)]
public class Game
{
    [JsonProperty("name")]
    public string Name;

    [JsonProperty("executablePath")]
    public string ExecutablePath;

    [JsonProperty("isFavorite")]
    public bool IsFavorite = false;

    [JsonProperty("playTime")]
    public TimeSpan PlayTime = TimeSpan.Zero;

    public Game(string name, string executablePath)
    {
        Name = name;
        ExecutablePath = executablePath;
    }

    public Game Copy() => new(Name, ExecutablePath)
    {
        IsFavorite = IsFavorite,
        PlayTime = PlayTime
    };
}
