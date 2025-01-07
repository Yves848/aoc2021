using System.Collections;
using System.Text.RegularExpressions;

string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
var file = args.Length > 0 ? File.ReadAllLines(args[0]).ToList() : File.ReadAllLines($"{home}\\git\\aoc2021\\08\\test.txt").ToList();
List<string> digits = [];
digits.Add("123567"); //0
digits.Add("36"); //1 
digits.Add("13457"); // 2
digits.Add("13467"); // 3
digits.Add("2346"); // 4
digits.Add("12467"); // 5
digits.Add("124567"); // 6
digits.Add("136"); // 7
digits.Add("1234567"); // 8
digits.Add("123467"); // 9

// Dictionary<int, string> segments = [];
Dictionary<string, int> segments = [];
// file = ["acedgfb cdfbe gcdfa fbcad dab cefabd cdfgeb eafb cagedb ab | cdfeb fcadb cdfeb cdbaf"];
void part1()
{
  int ans = 0;
  file.ToList().ForEach(line =>
  {
    var parts = line.Split(" | ");
    var right = parts[1].Split(' ').ToList();
    right.ForEach(d =>
    {
      if (new[] { 2, 4, 3, 7 }.Contains(d.Trim().Length)) ans++;
    });
  });
  Console.WriteLine($"Part 1 - Answer : {ans}");
}

void part2()
{
  int ans = 0;
  file.ToList().ForEach(line =>
  {
    var parts = line.Split(" | ");
    var left = parts[0].Split(' ').ToList();
    segments.Clear();
    new[] { 2, 3, 4, 5, 6, 7 }.ToList().ForEach(l =>
    {
      switch (l)
      {
        case 2:
          {
            List<int> s = [3, 6];
            left.ForEach(digit =>
            {
              if (digit.Length == l)
              {
                digit.ToList().ForEach(d =>
                {
                  if (!segments.ContainsKey(d.ToString()))
                  {
                    segments.Add(d.ToString(), s[0]);
                    s.RemoveAt(0);
                  }
                });
              }
            });
            break;
          }
        case 4:
          {
            List<int> s = [2, 4];
            left.ForEach(digit =>
            {
              if (digit.Length == l)
              {
                digit.ToList().ForEach(d =>
                {
                  if (!segments.ContainsKey(d.ToString()))
                  {
                    segments.Add(d.ToString(), s[0]);
                    s.RemoveAt(0);
                  }
                });
              }
            });
            break;
          }
        case 3:
          {
            List<int> s = [1];
            left.ForEach(digit =>
            {
              if (digit.Length == l)
              {
                digit.ToList().ForEach(d =>
                {
                  if (!segments.ContainsKey(d.ToString()))
                  {
                    segments.Add(d.ToString(), s[0]);
                    s.RemoveAt(0);
                  }
                });
              }
            });
            break;
          }
        case 5:
          {
            List<int> s = [7];
            List<char> intersect = [];
            left.ForEach(digit =>
            {
              if (digit.Length == l)
              {
                if (intersect.Count == 0)
                {
                  intersect = [.. digit];
                }
                else
                {
                  List<char> temp = [.. digit];
                  intersect = intersect.Intersect(temp).ToList();
                }
              }
            });
            intersect.ToList().ForEach(d =>
                {
                  if (!segments.ContainsKey(d.ToString()))
                  {
                    segments.Add(d.ToString(), s[0]);
                    s.RemoveAt(0);
                  }
                });
            break;
          }
        case 7:
          {
            List<int> s = [5];
            left.ForEach(digit =>
            {
              if (digit.Length == l)
              {
                digit.ToList().ForEach(d =>
                {
                  if (!segments.ContainsKey(d.ToString()))
                  {
                    segments.Add(d.ToString(), s[0]);
                    s.RemoveAt(0);
                  }
                });
              }
            });
            break;
          }
      }
    });
    // process right part .....
    var right = parts[1].Split(' ').ToList();
    string value = "";
    right.ForEach(d =>
    {
      List<string> seg = [];
      d.ToList().ForEach(c =>
      {
        seg.Add(segments[c.ToString()].ToString());
      });
      seg.Sort();
      string temp = "";
      seg.ToList().ForEach(s =>
      {
        temp += s;
      });
      value += digits.IndexOf(temp).ToString();
    });
    ans += int.Parse(value);
  });
  Console.WriteLine($"Part 2 - Answer : {ans}");
}

part1();

part2();
