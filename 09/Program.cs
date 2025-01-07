using System.Security.Cryptography;
using System.Text.RegularExpressions;

string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
List<string> file = args.Length > 0 ? File.ReadAllLines(args[0]).ToList() : File.ReadAllLines($"{home}\\git\\aoc2021\\09\\data.txt").ToList();
List<(int, int)> directions = [(-1, 0), (0, 1), (0, -1), (1, 0)];
int w = file[0].Length;
int h = file.Count;

List<(int, int)> lows = [];
HashSet<(int, int)> seen = [];
HashSet<(int, int)> bassin = [];
void part1()
{
  int ans = 0;
  int y = 0;
  while (y < h)
  {
    int x = 0;
    while (x < w)
    {
      bool low = true;
      int reference = int.Parse(file[y].Substring(x, 1));
      directions.ForEach(d =>
      {
        var (dx, dy) = d;
        if (x + dx >= 0 && x + dx < w && y + dy >= 0 && y + dy < h)
        {
          int target = int.Parse(file[y + dy].Substring(x + dx, 1));
          low = low && reference < target;
        }
      });
      if (low)
      {
        lows.Add((x, y));
        ans += reference + 1;
      }
      x++;
    }
    y++;
  }
  Console.WriteLine($"Part 1 - Answer : {ans}");
}

void flood(int x, int y)
{
  int reference = int.Parse(file[y].Substring(x, 1));
  if (!seen.Contains((x, y)) && reference != 9)
  {
    seen.Add((x, y));
    bassin.Add((x, y));
    directions.ForEach(d =>
        {
          var (dx, dy) = d;
          if (x + dx >= 0 && x + dx < w && y + dy >= 0 && y + dy < h)
          {
            int target = int.Parse(file[y + dy].Substring(x + dx, 1));
            if (target != 9)
            {
              flood(x + dx, y + dy);
            }
          }
        });
  }
}

void part2()
{
  long ans = 1;
  List<int> sizes = [];
  // lows.ToList().ForEach(l =>
  // {
  //   bassin.Clear();
  //   flood(l.Item1, l.Item2);
  //   sizes.Add(bassin.Count);
  // });
  int r = 0;
  while (r < h) {
    int c = 0;
    while (c < w) {
      bassin.Clear();
      flood(c,r);
      sizes.Add(bassin.Count);
      c++;
    }
    r++;
  }
  sizes.Sort();
  sizes.TakeLast(3).ToList().ForEach(i =>
  {
    ans *= i;
  });


  Console.WriteLine($"Part 2 - Answer : {ans}");
}

part1();

part2();
