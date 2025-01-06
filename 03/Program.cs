using System.Net;
using System.Text.RegularExpressions;

string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
var file = args.Length > 0 ? File.ReadAllLines(args[0]).ToList() : File.ReadAllLines($"{home}\\git\\aoc2021\\03\\test.txt").ToList();

void part1()
{
  int ans = 0;
  string gamma = "";
  string epsilon = "";
  foreach (int i in Enumerable.Range(0, file[0].Length))
  {
    int zero = 0;
    int one = 1;
    file.ToList().ForEach(l =>
    {
      if (l.Substring(i, 1) == "0")
      {
        zero++;
      }
      else one++;
    });
    if (zero > one)
    {
      gamma += "0";
      epsilon += "1";
    }
    else
    {
      gamma += "1";
      epsilon += "0";
    }
  }
  Console.WriteLine($"Part 1 - gamma : {gamma} epsilon : {epsilon}");
  ans = Convert.ToInt32(gamma, 2) * Convert.ToInt32(epsilon, 2);
  Console.WriteLine($"Part 1 - Answer : {ans}");
}

string filter(List<string> list, int pos, bool value)
{
  while (list.Count > 1)
  {
    int i = 0;
    List<string> zero = [];
    List<string> one = [];
    while (i < list.Count)
    {
      if (list[i].Substring(pos, 1) == "1")
      {
        one.Add(list[i]);
      }
      else
      {
        zero.Add(list[i]);
      }
      i++;
    }
    if (value)
    {
      // keep the O2Gen List
      if (zero.Count > one.Count)
      {
        list = zero;
      }
      else list = one;
    }
    else
    {
      // Keep the scrub list
      if (zero.Count <= one.Count)
      {
        list = zero;
      }
      else list = one;
    }
    pos++;
  }

  return list[0];
}

void part2()
{
  int ans = 0;
  int O2Gen = Convert.ToInt32(filter([.. file], 0, true),2);
  int O2Scrub = Convert.ToInt32(filter([.. file], 0, false),2);;

  ans = O2Gen * O2Scrub;
  
  Console.WriteLine($"Part 2 - Answer : {ans}");
}

part1();

part2();
