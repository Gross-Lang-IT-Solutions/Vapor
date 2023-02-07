using System.Text;
using Vapor;

const string configFilePath = "config.dat";

if (!Config.TryLoad(configFilePath, out var config))
	config = new Config();

Console.WriteLine(
	"Welcome to Vapor CLI!\n" +
	"Copyright (C) 2023 Groß & Lang IT Solutions and GSODev. All rights reserved." +

var sb = new StringBuilder();
var running = true;
while (running)
{
	ReadLine(sb);

	var segs = sb.ToString().Trim().Split(' ').Select(s => s.Trim()).ToArray();

	switch (segs[0])
	{
		case "quit":
		case "exit":
			running = false;
			break;
		default:
			Console.WriteLine("");
			break;
	}

	sb.Clear();
}

config.Save(configFilePath);

static void ReadLine(StringBuilder sb)
{
	Console.Write("> ");
	var read = true;
	while (read)
	{
		var c = Console.ReadKey(true);

		switch (c.Key)
		{
			case ConsoleKey.Backspace:
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
}
