using System.Text.RegularExpressions;

string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
List<string> file = args.Length > 0 ? File.ReadAllLines(args[0]).ToList() : File.ReadAllLines($"{home}\\git\\aoc2021\\16\\test.txt").ToList();

int process4(string packet, int pos) {
  int r = pos;
  bool cont = true;
  string bits = "";
  while(cont) {
    cont = (packet[pos]=='1');
    bits += packet.Substring(pos+1,4);
    pos += 5;
  }
  return Convert.ToInt32(bits,2);
}

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
    int version = Convert.ToInt32(bits.Substring(pointer,3),2);
    int id = Convert.ToInt32(bits.Substring(pointer+3,3),2);
    int l = 0;
    switch (id) {
      case 4: {
        l = process4(bits,pointer+6);
        break;
      }
    }
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
