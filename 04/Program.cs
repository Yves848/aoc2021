using System.Data;
using System.Globalization;
using System.IO.Pipelines;
using System.Text.RegularExpressions;

string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
var file = args.Length > 0 ? File.ReadAllText(args[0]) : File.ReadAllText($"{home}\\git\\aoc2021\\04\\test.txt");

var blocs = file.Split("\r\n\r\n");
var nums = blocs[0].Split(",").ToList().Select(p => int.Parse(p)).ToList();
List<Array> cards = [];
Regex re = new Regex(@"\d+");


void fillCards()
{
  cards.Clear();
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
        // Console.WriteLine($"board {board} num {num}");
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

int totalBoard(int board)
{
  int result = 0;
  int[,] card = (int[,])cards[board];
  int r = 0;
  while (r < 5)
  {
    int c = 0;
    while (c < 5)
    {
      if (card[r, c] != -1) result += card[r, c];
      c++;
    }
    r++;
  }
  return result;
}

void part1()
{
  fillCards();
  int ans = 0;
  int i = 0;
  int card = 0;
  bool win = false;
  while (i < nums.Count && !win)
  {
    int c = 0;
    while (c < cards.Count && !win)
    {
      win = checkNum(c, nums[i]);
      ans = nums[i];
      card = c;
      c++;
    }
    i++;
  }

  Console.WriteLine($"Part 1 - Answer : {ans * totalBoard(card)} card : {card}");
}


void part2()
{
  fillCards();
  int ans = 0;
  int i = 0;
  int card = 0;
  bool win = false;
  HashSet<int> wins = [];
  while (i < nums.Count && wins.Count < cards.Count)
  {
    int c = 0;
    while (c < cards.Count && wins.Count < cards.Count)
    {
      win = checkNum(c, nums[i]);
      if (win)
      {
        ans = nums[i];
        card = c;
        wins.Add(card);
      }
      c++;
    }
    i++;

  }
  Console.WriteLine($"Part 2 - Answer : {ans * totalBoard(card)}");
}

part1();

part2();
