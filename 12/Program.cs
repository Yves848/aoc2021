using System.Runtime.ExceptionServices;
using System.Security.AccessControl;
using System.Text.RegularExpressions;

string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
List<string> file = args.Length > 0 ? File.ReadAllLines(args[0]).ToList() : File.ReadAllLines($"{home}\\git\\aoc2021\\12\\test.txt").ToList();
List<(string,string)> dir = [];

void fillDir() {
  dir.Clear();
  file.ToList().ForEach(f => {
    var p = f.Split("-");
    dir.Add((p[0],p[1]));
  });
}

void part1()
{
  int ans = 0;
  List<string> chemins = [];
  fillDir();
  Stack<string> Q = [];
  dir.ForEach(d => {
    var (a1,a2) = d;
    if (a1 == "start") {
      Q.Push(a2);
    } 
    if (a2 == "start") {
      Q.Push(a1);
    }
    List<string> chemin = ["start"];
    HashSet<string> seen = [];
    List<(string,string)> dir2 = [..dir];
    while(Q.Count > 0) {
      string ch = Q.Pop();
      if (ch == "end") {
        chemin.Add(ch);
        chemins.Add(string.Join(',',chemin));
        Console.WriteLine(string.Join(',',chemin));
      }
      if (ch == ch.ToLower()) {
        if (seen.Contains(ch)) continue;
      }
      int i = 0;
      while(i < dir2.Count) {
        var (b1,b2) = dir2[i];
        if (b1 != "start" && b2 != "start") {
          if (b1 == ch) {
            Q.Push(b2);
            chemin.Add(b2);
            dir2.RemoveAt(i);
            break;
          }
          if (b2 == ch) {
            Q.Push(b1);
            chemin.Add(b1);
            dir2.RemoveAt(i);
          }
          i++;
        } else dir2.RemoveAt(i);
      }
    }
  });
  Console.WriteLine($"Part 1 - Answer : {ans}");
}


void part2()
{
  int ans = 0;

  Console.WriteLine($"Part 2 - Answer : {ans}");
}

part1();

part2();
