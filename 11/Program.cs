using System.Data;
using System.Runtime.InteropServices.Marshalling;
using System.Text.RegularExpressions;

string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
List<string> file = args.Length > 0 ? File.ReadAllLines(args[0]).ToList() : File.ReadAllLines($"{home}\\git\\aoc2021\\11\\test.txt").ToList();
List<(int, int)> dirs = [(-1, -1), (-1, 0), (-1, 1), (0, -1), (0, 1), (1, -1), (1, 0), (1, 1)];

int[,] grid = new int[10, 10];
int w = file[0].Length;
int h = file.Count;

void fillGrid()
{
  grid = new int[10, 10];
  int r = 0;
  while (r < file.Count)
  {
    int c = 0;
    while (c < file[0].Length)
    {
      grid[r, c] = int.Parse(file[r].Substring(c, 1));
      c++;
    }
    r++;
  }
}

void printGrid()
{
  int r = 0;
  while (r < file.Count)
  {
    int c = 0;
    while (c < file[0].Length)
    {
      Console.CursorLeft = c;
      Console.CursorTop = r;
      int cell = grid[r, c];
      Console.ForegroundColor = cell == 0 ? ConsoleColor.White : ConsoleColor.DarkGray;
      Console.Write(grid[r, c]);
      c++;
    }
    r++;
  }
  Console.WriteLine();
}

int flash()
{
  HashSet<(int, int)> seen = [];
  Stack<(int, int)> Q = [];
  int nb = 0;
  int r = 0;
  while (r < file.Count)
  {
    int c = 0;
    while (c < file[0].Length)
    {
      int cell = grid[r, c] + 1;
      if (cell > 9)
      {
        Q.Push((r, c));
      }
      grid[r, c] = cell;
      c++;
    }
    r++;
  }

  while (Q.Count > 0)
  {
    var (y, x) = Q.Pop();
    if (seen.Contains((y, x))) continue;
    seen.Add((y, x));
    int cell = grid[y, x];
    if (cell > 9)
    {
      nb++;
      grid[y, x] = 0;
      dirs.ForEach(d =>
      {
        // printGrid();
        var (dy, dx) = d;
        if (x + dx >= 0 && x + dx < w && y + dy >= 0 && y + dy < h && !seen.Contains((y + dy, x + dx)))
        {
          grid[y + dy, x + dx] += 1;
          if (grid[y + dy, x + dx] > 9)
            Q.Push((y + dy, x + dx));
        }
      });
    }
    else
    {
      grid[y, x] = cell + 1;
    }
  }
  return nb;
}

void part1()
{
  Console.Clear();
  Console.CursorVisible = false;
  fillGrid(); ;
  int i = 0;
  int ans = 0;
  while (i < 100)
  {
    ans += flash();
    // printGrid();
    i++;
  }
  Console.WriteLine($"Part 1 - Answer : {ans}");
  Console.CursorVisible = true;
}


void part2()
{
  // Console.Clear();
  Console.CursorVisible = false;
  fillGrid(); ;
  int i = 1;
  int ans = 0;
  while (true)
  {
    ans = flash();
    // printGrid();
    if (ans == 100) break;
    i++;
  }
  ans = i;
  Console.WriteLine($"Part 2 - Answer : {ans}");
  Console.CursorVisible = true;
}

part1();

part2();
