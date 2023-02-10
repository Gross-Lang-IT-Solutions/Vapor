using Newtonsoft.Json;

namespace Vapor;

[JsonObject(MemberSerialization = Newtonsoft.Json.MemberSerialization.OptIn)]
public class Game
{
    [JsonProperty("name")]
    public string Name;

    [JsonProperty("executablePath")]
    public string ExecutablePath;

    [JsonProperty("installDateTime")]
    public DateTime InstallDateTime;

    [JsonProperty("isFavorite")]
    public bool IsFavorite = false;

    [JsonProperty("playTime")]
    public TimeSpan PlayTime = TimeSpan.Zero;

    [JsonProperty("lastPlayedDateTime")]
    public DateTime LastPlayedDateTime = DateTime.UnixEpoch;

    [JsonProperty("category")]
    public string Category = "Uncategorized";

    [JsonProperty("publisher")]
    public string Publisher = "Unknown publisher";

    [JsonProperty("ageRating")]
    public uint AgeRating = 0;

    public Game(string name, string executablePath, DateTime installDateTime)
    {
        Name = name;
        ExecutablePath = executablePath;
        InstallDateTime = installDateTime;
    }

    public Game Copy() => new(Name, ExecutablePath, InstallDateTime)
    {
        IsFavorite = IsFavorite,
        PlayTime = PlayTime,
        LastPlayedDateTime = LastPlayedDateTime,
        Category = Category,
        Publisher = Publisher,
        AgeRating = AgeRating
    };
}
