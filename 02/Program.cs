using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
var file = args.Length > 0 ? File.ReadAllLines(args[0]).ToList() : File.ReadAllLines($"{home}\\git\\aoc2021\\02\\test.txt").ToList();
Dictionary<string,(int,int)> dirs = [];
dirs.Add("forward",(0,1));
dirs.Add("up",(-1,0));
dirs.Add("down",(1,0));
void part1()
{
  int ans = 0;
  int y = 0;
  int x = 0;
  file.ForEach(instruction => {
    var i = instruction.Split();
    var (dy,dx) = dirs[i[0]];
    y += dy*int.Parse(i[1]);
    x += dx*int.Parse(i[1]);
  });
  ans = y*x;
  Console.WriteLine($"Part 1 - Answer : {ans}");
}



void part2()
{
  int ans = 0;
  int aim = 0;
  int y = 0;
  int x = 0;
  file.ForEach(instruction => {
    var i = instruction.Split();
    string inst = i[0];
    var (dy,dx) = dirs[i[0]];
    var nb = int.Parse(i[1]);
    switch (inst) {
      case "up":
      case "down": {
        aim += dy*nb;
        break;
      }
      case "forward": {
        x += dx*nb;
        y += aim*nb;
      break;
      }
    }
    
  });
  ans = y*x;

  Console.WriteLine($"Part 2 - Answer : {ans}");
}

part1();

part2();
