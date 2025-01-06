using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
var file = args.Length > 0 ? File.ReadAllText(args[0]) : File.ReadAllText($"{home}\\git\\aoc2021\\06\\test.txt");

void part1()
{
  int ans = 0;
  List<int> fishes = file.Split(',').ToList().Select(p => int.Parse(p.ToString())).ToList();
  int nb = 80;
  while (nb > 0)
  {
    int newFishes = 0;
    int f = 0;
    while (f < fishes.Count)
    {
      int fish = fishes[f];
      if (fish == 0)
      {
        fish = 6;
        newFishes++;
      }
      else fish--;
      fishes[f] = fish;
      f++;
    };
    for (int i = 0; i < newFishes; i++)
    {
      fishes.Add(8);
    }
    nb--;
  }
  ans = fishes.Count;
  Console.WriteLine($"Part 1 - Answer : {ans}");
}



void part2()
{
  long ans = 0;
  List<long> fishes = file.Split(',').ToList().Select(p => long.Parse(p.ToString())).ToList();
  Dictionary<long, long> counter = fishes.GroupBy(x => (long)x).ToDictionary(group => group.Key, group => (long)group.Count());
  int nb = 256;
  while (nb > 0)
  {
    Dictionary<long, long> Y = [];
    foreach (var kvp in counter)
    {
      var (x, cnt) = kvp;
      if (x == 0)
      {
        if (Y.ContainsKey(6))
        {
          Y[6] += cnt;
        }
        else
        {
          Y.Add(6, cnt);
        }
        if (Y.ContainsKey(8))
        {
          Y[8] += cnt;
        }
        else
        {
          Y.Add(8, cnt);
        }
      }
      else
      {
        if (Y.ContainsKey(x - 1))
        {
          Y[x - 1] += cnt;
        }
        else
        {
          Y.Add(x - 1, cnt);
        }
      }
    }
    counter = Y;
    nb--;
  }
  ans = counter.Values.ToList().Sum();
  Console.WriteLine($"Part 2 - Answer : {ans}");
}

part1();

part2();
