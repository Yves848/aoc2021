using System.Text.RegularExpressions;

string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
var file = args.Length > 0 ? File.ReadAllLines(args[0]).ToList() : File.ReadAllLines($"{home}\\git\\aoc2021\\05\\test.txt").ToList();
Regex reNum = new Regex(@"\d+");
Dictionary<(int, int), int> points = [];

int addPoint(int x, int y)
{
  if (points.ContainsKey((x, y)))
  {
    points[(x, y)] += 1;
  }
  else
  {
    points.Add((x, y), 1);
  }
  return points[(x, y)];
}

void part1()
{
  int ans = 0;
  file.ToList().ForEach(line =>
  {
    MatchCollection nums = reNum.Matches(line);
    List<int> x = [int.Parse(nums[0].Value), int.Parse(nums[2].Value)];
    List<int> y = [int.Parse(nums[1].Value), int.Parse(nums[3].Value)];
    x.Sort();
    y.Sort();
    int i = 0;
    if (x[1] == x[0])
    {
      while (y[0] + i <= y[1])
      {
        if (addPoint(x[0], y[0] + i) == 2) ans++;
        i++;
      }
    }
    if (y[0] == y[1])
    {
      while (x[0] + i <= x[1])
      {
        if (addPoint(x[0] + i, y[0]) == 2) ans++;
        i++;
      }
    }
  });
  Console.WriteLine($"Part 1 - Answer : {ans}");
}

void part2()
{
  points.Clear();
  int ans = 0;
  file.ToList().ForEach(line =>
    {
      MatchCollection nums = reNum.Matches(line);
      List<int> x = [int.Parse(nums[0].Value), int.Parse(nums[2].Value)];
      List<int> y = [int.Parse(nums[1].Value), int.Parse(nums[3].Value)];
      // x.Sort();
      // y.Sort();
      int i = 0;
      int dx = 1;
      int dy = 1;
      if (x[1] == x[0])
      {
        dx = 0;
      }
      else
      {
        if (x[1] > x[0])
        {
          i = x[1] - x[0];
          dx = 1;
        }
        else
        {
          i = x[0] - x[1];
          dx = -1;
        }
      }
      if (y[0] == y[1])
      {
        dy = 0;
      }
      else
      {
        if (y[1] > y[0])
        {
          i = y[1] - y[0];
          dy = 1;
        }
        else
        {
          i = y[0] - y[1];
          dy = -1;
        }
      }
      int x2 = x[0];
      int y2 = y[0];
      while (i >= 0)
      {
        if (addPoint(x2, y2) == 2) ans++;
        x2+=dx;
        y2+=dy;
        i--;
      }
    });
  Console.WriteLine($"Part 2 - Answer : {ans}");
}

part1();

part2();
