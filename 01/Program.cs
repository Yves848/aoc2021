using System.Text.RegularExpressions;
using System.Transactions;

string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
var file = args.Length > 0 ? File.ReadAllText(args[0]) : File.ReadAllText($"{home}\\git\\aoc2021\\01\\test.txt");
var num = file.Split("\r\n").ToList().Select(p => int.Parse(p)).ToList();

void part1()
{
  int ans = 0;
  int prev = num[0];
  num.ToList().ForEach(n =>
  {
    if (n > prev)
    {
      ans++;
    }
      prev = n;
  });
  Console.WriteLine($"Part 1 - Answer : {ans}");
}

int sum(int i) {
  return num.Skip(i).Take(3).Sum();
}

void part2()
{
  int ans = 0;
  int i = 0;
  int prev = sum(0);
  int j = 0;
  while (i < num.Count-2) {
    int n = sum(j+i);
    if (n > prev)
    {
      ans++;
    }
      prev = n;
    i++;
  }

  Console.WriteLine($"Part 2 - Answer : {ans}");
}

part1();

part2();
