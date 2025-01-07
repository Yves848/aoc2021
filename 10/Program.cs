using System.Text.RegularExpressions;

string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
List<string> file = args.Length > 0 ? File.ReadAllLines(args[0]).ToList() : File.ReadAllLines($"{home}\\git\\aoc2021\\10\\data.txt").ToList();
List<string> open = ["(", "[", "{", "<"];
List<string> close = [")", "]", "}", ">"];
List<int> score = [3, 57, 1197, 25137];
List<string> correct = [];
/*
): 3 points.
]: 57 points.
}: 1197 points.
>: 25137 points.
*/

void part1()
{
  int ans = 0;
  Stack<string> Q = [];
  file.ToList().ForEach(l =>
  {
    bool valid = true;
    Q.Clear();
    l.ToList().ForEach(c =>
    {
      string ch = c.ToString();
      if (open.Contains(ch))
      {
        Q.Push(ch);
      }
      else
      {
        int index = close.IndexOf(ch);
        string ch2 = Q.Pop();
        if (open.IndexOf(ch2) != index)
        {
          ans += score[index];
          valid = false;
        }
      }
    });
    if (valid) correct.Add(l);
  });
  Console.WriteLine($"Part 1 - Answer : {ans}");
}


void part2()
{
  long ans = 0;
  Stack<string> Q = [];
  List<long> scores = [];
  correct.ToList().ForEach(l =>
  {
    Q.Clear();
    l.ToList().ForEach(c =>
    {
      string ch = c.ToString();
      if (open.Contains(ch))
      {
        Q.Push(ch);
      }
      else
      {
        int index = close.IndexOf(ch);
        string ch2 = Q.Pop();
      }
    });
    while (Q.Count > 0) {
      string ch = Q.Pop();
      ans *= 5;
      ans += open.IndexOf(ch)+1;
    }
    scores.Add(ans);
    ans = 0;
  });
  scores.Sort();
  ans = scores[(scores.Count -1) / 2];
  Console.WriteLine($"Part 2 - Answer : {ans}");
}

part1();

part2();
