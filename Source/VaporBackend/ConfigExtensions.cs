using System.Text;

namespace Vapor;
public static class ConfigExtensions
{
	public static IEnumerable<KeyValuePair<Guid, Game>> SearchFor(this Config config, string s)
	{
		GameSorter cmp = new();
		cmp.Search = s;

		return config.Games.Order(cmp);
	}

	private class GameSorter : IComparer<KeyValuePair<Guid, Game>>
	{
		public string Search { get; set; } = string.Empty;
		public int Compare(KeyValuePair<Guid, Game> x, KeyValuePair<Guid, Game> y)
		{
			double xValue = int.MaxValue;
			double yValue = int.MaxValue;

			xValue *= Math.Max(
					Similarity(this.Search, x.Key.ToString()),
					Similarity(this.Search, x.Value.Name));
			yValue *= Math.Max(
					Similarity(this.Search, y.Key.ToString()),
					Similarity(this.Search, y.Value.Name));

			return (int) (yValue - xValue);
		}
		private static double Similarity(string a, string b)
		{
			if (a == b)
				return 1;
			if (a == string.Empty)
				return 0;
			if (b == string.Empty)
				return 0;

			Dictionary<char, int> aStringContent = new();
			Dictionary<char, int> bStringContent = new();
			{
				Action<Dictionary<char, int>, string> populate = (d, s) => 
				{
					foreach (var c in s)
					{
						char u = char.ToUpper(c);
						d[u] = d.ContainsKey(u) ?
							d[u] + 1 : 1;
					}
				};

				populate(aStringContent, a);
				populate(bStringContent, b);
			}

			Dictionary<char, int> difference = new();

			foreach (var kvp in aStringContent)
			{
				if (!bStringContent.ContainsKey(kvp.Key))
					continue;

				difference[kvp.Key] = kvp.Value - bStringContent[kvp.Key];
			}

			StringBuilder stringA = new();
			StringBuilder stringB = new();
			{
				Action<StringBuilder, string> populate = (sb, s) =>
				{
					foreach (var c in s)
						if (difference.ContainsKey(char.ToUpper(c)))
							sb.Append(char.ToUpper(c));
				};

				populate(stringA, a);
				populate(stringB, b);
			}

			for (int i = 0; i < Math.Min(stringA.Length, stringB.Length); )
			{

				if (stringA[i] == stringB[i])
				{
					i++;
					continue;
				}

				(char dis, char next) charsA = (stringA[i], '\0');
				(char dis, char next) charsB = (stringB[i], '\0');

				if (Math.Min(stringA.Length, stringB.Length) - i >= 2)
				{
					charsA.next = stringA[i + 1];
					charsB.next = stringB[i + 1];
				}

				switch ((charsA.dis == charsB.next, charsB.dis == charsA.next))
				{
					case (true, true):
						(stringA[i], stringA[i + 1]) =
							(stringA[i + 1], stringA[i]);
						break;
					case (true, false):
						difference[charsB.dis] += 1;
						stringB.Remove(i, 1);
						break;
					case (false, true):
						difference[charsA.dis] -= 1;
						stringA.Remove(i, 1);
						break;
					default:
						{
							bool failed = false;

							if (difference[charsA.dis] > 0)
							{
								difference[charsA.dis] -= 1;
								stringA.Remove(i, 1);
							}
							else
								failed |= true;

							if (difference[charsB.dis] < 0)
							{
								difference[charsB.dis] += 1;
								stringB.Remove(i, 1);
							}
							else
								failed &= true;

							if (failed)
								i++;
						}
						break;
				}
			}

			double similarity = 0;
			similarity += (double) stringA.Length / a.Length;
			similarity += (double) stringB.Length / b.Length;
			similarity /= 2;

			return similarity;
		}
	}
}
