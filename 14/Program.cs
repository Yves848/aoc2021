using System.Text.RegularExpressions;

string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
var blocs = args.Length > 0 ? File.ReadAllText(args[0]).Split("\r\n\r\n") : File.ReadAllText($"{home}\\git\\aoc2021\\14\\test.txt").Split("\r\n\r\n");

Regex reRules = new Regex(@"(\w+) -> (\w+)");
Dictionary<string, string> rules = [];
string template = blocs[0];
reRules.Matches(blocs[1]).ToList().ForEach(r =>
{
  rules.Add(r.Groups[1].Value, r.Groups[2].Value);
});

void part1()
{
  int ans = 0;
  string polymer = template;
  for (int i = 0; i < 10; i++)
  {
    int pos = 0;
    string temp = "";
    while (pos < polymer.Length - 1)
    {
      string rule = polymer.Substring(pos, 2);
      if (rules.ContainsKey(rule))
      {
        if (temp.Length == 0) temp += rule[0];
        temp += rules[rule] + rule[1];
      }
      pos++;
    }
    polymer = temp;
    Console.WriteLine(polymer);
  }
  var charCounts = polymer.GroupBy(c => c)
                   .ToDictionary(g => g.Key, g => g.Count());
  int min = int.MaxValue;
  int max = int.MinValue;
  foreach (var kvp in charCounts)
  {
    if (kvp.Value < min) min = kvp.Value;
    if (kvp.Value > max) max = kvp.Value;
  }
  ans = max - min;
  Console.WriteLine($"Part 1 - Answer : {ans}");
}


void part2()
{
  long ans = 0;
  
  Console.WriteLine($"Part 2 - Answer : {ans}");
}

part1();

part2();
