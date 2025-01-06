using System.IO.Pipelines;
using System.Text.RegularExpressions;

string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
var file = args.Length > 0 ? File.ReadAllText(args[0]) : File.ReadAllText($"{home}\\git\\aoc2021\\04\\test.txt");

var blocs = file.Split("\r\n\r\n");
var nums = blocs[0].Split(",").ToList().Select(p => int.Parse(p)).ToList();
List<Array> cards = [];
Regex re = new Regex(@"\d+");
int i = 1;
while (i < blocs.Count())
{
  MatchCollection d = re.Matches(blocs[i]);
  int r = 0;
  int c = 0;
  int[,] card = new int[6, 6];
  d.ToList().ForEach(cell =>
  {
    card[r, c] = int.Parse(cell.Value);
    c++;
    if (c > 4)
    {
      r++;
      c = 0;
    }
  });
  cards.Add(card);
  i++;
}

bool checkNum(int board, int num)
{
  int r = 0;
  int[,] card = (int[,])cards[board];
  bool result = false;
  while (r < 5)
  {
    int c = 0;
    while (c < 5)
    {
      if (card[r, c] == num)
      {
        Console.WriteLine($"board {board} num {num}");
        card[r, c] = -1;
        card[r, 5] += 1;
        card[5, c] += 1;
        result = card[r, 5] == 5 || card[5, c] == 5;
      }
      c++;
    }
    r++;
  }
  return result;
}

void part1()
{
  int ans = 0;
  int i = 0;
  bool win = false;
  while (i < nums.Count && !win)
  {
    int c = 0;
    while (c < cards.Count && !win)
    {
      win = checkNum(c, nums[i]);
      ans = nums[i];
      c++;
    }
    i++;
  }
  Console.WriteLine($"Part 1 - Answer : {ans}");
}


void part2()
{
  int ans = 0;

  Console.WriteLine($"Part 2 - Answer : {ans}");
}

part1();

part2();
