using System.Diagnostics;
using System.Text;
using Vapor;

const string configFilePath = "config.dat";

if (!Config.TryLoad(configFilePath, out var config))
    config = new Config();

Console.WriteLine(
    "Welcome to Vapor CLI!\n" +
    "Copyright (C) 2023 Groß & Lang IT Solutions and GSODev. All rights reserved.");

var running = true;
while (running)
{
    Console.Write("> ");
    var segs = ReadLine().Trim().Split(' ').Select(s => s.Trim()).ToArray();

    switch (segs[0])
    {
        case "quit":
        case "exit":
            running = false;
            break;
        case "add":
            {
                if (segs.Length != 1)
                {
                    Console.WriteLine("Usage: 'add'");
                    break;
                }

                Console.Write("Game title: ");
                var name = ReadLine();
                Console.Write("Executable path: ");
                var path = ReadLine();

                var game = new Game(name, path, DateTime.Now);
                config.AddGame(game);
            }
            break;
		case "SteamDB":
			{
				switch (segs.Length)
				{
					case <= 1:
						foreach (var g in SteamDB.GetListOfGames())
						{
							Console.WriteLine(g);
							if (Console.ReadKey(true).Key == ConsoleKey.Q)
								break;
						}
						break;

					case <= 2:
						int appid;
						if (!int.TryParse(segs[1], out appid))
							goto default;

						Console.WriteLine(SteamDB.GetInfo(appid));
						break;

					default:
						foreach (var g in SteamDB.GetInfos(string.Join(' ', segs[1..^0])))
						{
							Console.WriteLine(g);
							if (Console.ReadKey(true).Key == ConsoleKey.Q)
								break;
						}
						break;
				}
			}
			break;
        case "remove":
            {
                if (segs.Length != 2)
                {
                    Console.WriteLine("Usage: 'remove <guid>'");
                    break;
                }

                if (!Guid.TryParse(segs[1], out var guid) || !config.Games.ContainsKey(guid))
                {
                    Console.WriteLine("There is no game with the specified GUID your list of games.");
                    break;
                }
                config.RemoveGame(guid);
            }
            break;
        case "list":
            {
                if (segs.Length != 1)
                {
                    Console.WriteLine("Usage: 'list'");
                    break;
                }

                if (config.Games.Count == 0)
                {
                    Console.WriteLine("There are no games in your list.");
                    break;
                }

                foreach (var g in config.Games.OrderBy(g => g.Value.Name))
                    Console.WriteLine($"{g.Key}: '{g.Value.Name}'");
            }
            break;
        case "search":
            {
                if (segs.Length == 1)
                    break;
				foreach (var g in config.SearchFor(string.Join(' ', segs[1..^0])))
                    Console.WriteLine($"{g.Key}: '{g.Value.Name}'");
            }
            break;
        case "details":
            {
                if (segs.Length != 2)
                {
                    Console.WriteLine("Usage: 'details <guid>'");
                    break;
                }

                if (!Guid.TryParse(segs[1], out var guid) || !config.Games.ContainsKey(guid))
                {
                    Console.WriteLine("There is no game with the specified GUID your list of games.");
                    break;
                }

                var game = config.Games[guid];
                Console.WriteLine(
                    $"Name: '{game.Name}'\n" +
                    $"Executable path: '{game.ExecutablePath}'\n" +
                    $"Install date and time: {game.InstallDateTime}\n" +
                    $"Is favorite? {(game.IsFavorite ? "Yes" : "No")}\n" +
                    $"Play time: {game.PlayTime}\n" +
                    $"Last played date and time: {game.LastPlayedDateTime}\n" +
                    $"Category: {game.Category}\n" +
                    $"Publisher: {game.Publisher}\n" +
                    $"Age rating: {game.AgeRating}");
            }
            break;
        case "start":
            {
                if (segs.Length != 2)
                {
                    Console.WriteLine("Usage: 'start <guid>'");
                    break;
                }

                if (!Guid.TryParse(segs[1], out var guid) || !config.Games.ContainsKey(guid))
                {
                    Console.WriteLine("There is no game with the specified GUID your list of games.");
                    break;
                }

                Process.Start(new ProcessStartInfo(config.Games[guid].ExecutablePath));
                break;
            }
            break;
        default:
            Console.WriteLine($"Command '{segs[0]}' was not recognized.");
            break;
    }
}

config.Save(configFilePath);

static string ReadLine()
{
    var sb = new StringBuilder();

    var read = true;
    while (read)
    {
        var c = Console.ReadKey(true);

        switch (c.Key)
        {
            case ConsoleKey.Backspace:
                if (sb.Length == 0)
                    break;
                sb.Remove(sb.Length - 1, 1);
                Console.Write("\b \b");
                break;
            case ConsoleKey.Enter:
                Console.Write('\n');
                read = false;
                break;
            default:
                sb.Append(c.KeyChar);
                Console.Write(c.KeyChar);
                break;
        }
    }

    return sb.ToString();
}
