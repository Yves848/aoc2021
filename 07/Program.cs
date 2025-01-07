using System.Collections.Immutable;
using System.Text.RegularExpressions;

string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
var file = args.Length > 0 ? File.ReadAllText(args[0]) : File.ReadAllText($"{home}\\git\\aoc2021\\07\\test.txt");
List<int> positions = file.Split(',').ToList().Select(p => int.Parse(p)).ToList();
positions.Sort();
Dictionary<long, long> counter = positions.GroupBy(x => (long)x).ToDictionary(group => group.Key, group => (long)group.Count());
void part1()
{
  long ans = long.MaxValue;
  int j = 0;
  while (j < counter.Count)
  {
    int i = 0;
    long to = counter.ToList()[j].Key;
    long fuel = 0;
    while (i < counter.Count)
    {
      long from = counter.ToList()[i].Key;
      fuel += Math.Abs(from - to) * counter[from];
      i++;
    }
    if (fuel < ans) ans = fuel;
    j++;
  }
  Console.WriteLine($"Part 1 - Answer : {ans}");
}

long consumption(long from, long to) {
  long diff = Math.Abs(from - to);
  int i = 1;
  long fuel = 0;
  while(diff > 0) {
    fuel += i;
    i++;
    diff--;
  }
  return fuel;
}

void part2()
{
  long ans = long.MaxValue;
  int j = (int)counter.ToList()[0].Key;
  while (j < counter.ToList()[counter.Count-1].Key)
  {
    int i = 0;
    long to = j;
    long fuel = 0;
    while (i < counter.Count)
    {
      long from = counter.ToList()[i].Key;
      fuel += consumption(from,to) * counter[from];
      i++;
    }
    if (fuel < ans) ans = fuel;
    j++;
  }
  Console.WriteLine($"Part 2 - Answer : {ans}");
}


part1();

part2();


