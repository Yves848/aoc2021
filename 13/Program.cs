using System.IO.Pipelines;
using System.Text.RegularExpressions;

string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
var file = args.Length > 0 ? File.ReadAllText(args[0]) : File.ReadAllText($"{home}\\git\\aoc2021\\13\\data.txt");

var blocs = file.Split("\r\n\r\n").ToList();

Regex reCoord = new Regex(@"(\d+),(\d+)");
Regex reFold = new Regex(@"(\w)=(\d+)");
List<(int, int)> grid = [];
List<(string, int)> folds = [];

void fillGrid()
{
  grid.Clear();
  reCoord.Matches(blocs[0]).ToList().ForEach(m =>
  {
    grid.Add(((int.Parse(m.Groups[1].Value)), int.Parse(m.Groups[2].Value)));
  });
}



reFold.Matches(blocs[1]).ToList().ForEach(f =>
{
  folds.Add((f.Groups[1].Value, int.Parse(f.Groups[2].Value)));

});

List<(int, int)> foldY(int yy)
{
  List<(int, int)> result = [];
  int i = 0;
  while (i < grid.Count)
  {
    var (x, y) = grid[i];
    if (yy < y)
    {
      int y2 = y - yy;
      if (!grid.Contains((x, yy- y2))) result.Add((x, yy- y2));
    }
    else result.Add((x, y));
    i++;
  }
  return result;
}

List<(int, int)> foldX(int xx)
{
  List<(int, int)> result = [];
  int i = 0;
  while (i < grid.Count)
  {
    var (x, y) = grid[i];
    if (xx < x)
    {
      int x2 = x - xx;
      if (!grid.Contains((xx - x2, y))) result.Add((xx - x2, y));
    }
    else result.Add((x, y));
    i++;
  }
  return result;
}

void part1()
{
  fillGrid();
  // Console.WriteLine(blocs[0]);
  grid = foldX(655);
  // grid = foldX(5);
  int ans = grid.Count;
  Console.WriteLine($"Part 1 - Answer : {ans}");
}

void print() {
List<string> output = [];
  for(int i = 0; i < 20;i++) {
    output.Add("".PadLeft(20,' '));
  }
  grid.ForEach(g => {
    var (x,y) = g;
    
    output[y] = output[y].PadRight(x+1,' ').Remove(x,1).Insert(x,"#");
  });
  output.ForEach(o => {
    Console.WriteLine(o);
  });
    Console.WriteLine("");

}


void part2()
{
  fillGrid();
  int ans = 0;
  // print();
  folds.ForEach(f =>
  {
    var (d, n) = f;
    if (d == "x") grid = foldX(n);
    if (d == "y") grid = foldY(n);
    // print();
  });
  grid.Sort();
  print();
  Console.WriteLine($"Part 2 - Answer : {ans}");
}

part1();

part2();
