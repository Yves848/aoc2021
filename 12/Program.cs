using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices.Marshalling;
using System.Security.AccessControl;
using System.Text.RegularExpressions;

string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
List<string> file = args.Length > 0 ? File.ReadAllLines(args[0]).ToList() : File.ReadAllLines($"{home}\\git\\aoc2021\\12\\test.txt").ToList();
List<(string, string)> dir = [];

void fillDir()
{
  dir.Clear();
  file.ToList().ForEach(f =>
  {
    var p = f.Split("-");
    dir.Add((p[0], p[1]));
    dir.Add((p[1], p[0]));
  });
}
int solve(bool p1)
{
  int ans = 0;
  List<string> chemins = [];
  fillDir();
  Stack<(string, List<string>, string)> Q = [];
  Q.Push(("start", ["start"], ""));

  while (Q.Count > 0)
  {
    var (pos, small, twice) = Q.Pop();
    if (pos == "end")
    {
      ans += 1;
      continue;
    }
    dir.Where(w => w.Item1 == pos).ToList().ForEach(y =>
    {
      if (!small.Contains(y.Item2))
      {
        List<string> new_small = [.. small];
        if (y.Item2 == y.Item2.ToLower()) new_small.Add(y.Item2);
        Q.Push((y.Item2, new_small, twice));
      }
      else
      {
        if (small.Contains(y.Item2) && twice == "" && !(new[] { "start", "end" }).Contains(y.Item2) && !p1) Q.Push((y.Item2, small, y.Item2));
      }

    });
  }
  return ans;
}

void part1()
{
  int ans = solve(true);

  Console.WriteLine($"Part 1 - Answer : {ans}");
}


void part2()
{
  int ans = 0;
  ans = solve(false);
  Console.WriteLine($"Part 2 - Answer : {ans}");
}

part1();

part2();
