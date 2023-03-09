using System.Text;

namespace Vapor;

public static class Utils
{
	public static double StringComparer(string a, string b)
	{
		if (a == b)
			return 1;
		if (a == string.Empty)
			return 0;
		if (b == string.Empty)
			return 0;

		StringBuilder stringA = new(a.ToUpper());
		StringBuilder stringB = new(b.ToUpper());

		Dictionary<char, int> difference = new();

		foreach (var c in stringA.ToString())
		{
			difference[c] = difference.ContainsKey(c) ?
				difference[c] + 1 : 1;
		}

		foreach (var c in stringB.ToString())
		{
			difference[c] = difference.ContainsKey(c) ?
				difference[c] - 1 : -1;
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
