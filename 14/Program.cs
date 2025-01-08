using System.Net.Http.Headers;
using System.Numerics;
using System.Runtime.Serialization.Formatters;
using System.Security.Cryptography;
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
    // Console.WriteLine(polymer);
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
  long  ans = 0;
  Dictionary<string, long> counter = [];
  for (int i = 0; i < template.Length - 1; i += 1)
  {
    string t = template.Substring(i, 2);
    if (!counter.ContainsKey(t)) counter[t] = 0;
    counter[t]++;
  }
  int j = 0;
  Dictionary<string, long> c2 = [];
  while (j < 40)
  {
    c2.Clear();
    counter.ToList().ForEach(c =>
    {
      var (k, v) = c;
      string s = rules[k];
      string p1 = k[0] + s;
      string p2 = s + k[1];
      if (!c2.ContainsKey(p1)) c2[p1] = 0;
      c2[p1]+=v;
      if (!c2.ContainsKey(p2)) c2[p2] = 0;
      c2[p2]+=v;
    });
    counter = c2.ToDictionary();
    j++;
  }
  Dictionary<string, long> cf = [];
  counter.ToList().ForEach(c =>
  {
    var (k, n) = c;
    if (!cf.ContainsKey(k[0].ToString())) cf[k[0].ToString()] = 0;
    cf[k[0].ToString()]+=n;
    // if (!cf.ContainsKey(k[1].ToString())) cf[k[1].ToString()] = 0;
    // cf[k[1].ToString()]+=n;
  });
  if (!cf.ContainsKey(template.Last().ToString())) cf[template.Last().ToString()] = 0;
  cf[template.Last().ToString()] ++;
  ans = cf.Values.Max() - cf.Values.Min();
  Console.WriteLine($"Part 2 - Answer : {ans}");
}

part1();

part2();
