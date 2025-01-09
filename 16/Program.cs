using System.Text.RegularExpressions;

string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
List<string> file = args.Length > 0 ? File.ReadAllLines(args[0]).ToList() : File.ReadAllLines($"{home}\\git\\aoc2021\\16\\test.txt").ToList();

void part1()
{
  int ans = 0;
  string packet = file[0];
  string bits = "";
  packet.ToList().ForEach(c => {
    int d = Convert.ToInt32(c.ToString(),16);
    bits += Convert.ToString(d,2).PadLeft(4,'0');
  });
  int pointer = 0;
  while (true) {

    if (pointer >= bits.Length) break;
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
