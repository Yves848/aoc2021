using System.Text.RegularExpressions;

string home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);

List<string> file = args.Length > 0 ? File.ReadAllLines(args[0]).ToList() : File.ReadAllLines($"{home}/git/aoc2021/16/test.txt").ToList();
file = ["38006F45291200"];
(int, int) header(string packet, int pos)
{
  int version = Convert.ToInt32(packet.Substring(pos, 3), 2);
  int id = Convert.ToInt32(packet.Substring(pos + 3, 3), 2);
  return (version, id);
}

int process4(string packet, int pos)
{
  int r = pos;
  bool cont = true;
  string bits = "";
  while (cont)
  {
    cont = (packet[pos] == '1');
    bits += packet.Substring(pos + 1, 4);
    pos += 5;
  }
  r = pos / 4;
  if (r * 4 < pos) r++;
  // Console.WriteLine(r);
  return r * 4;
}

(int, int) process6(string packet, int pos)
{
  int r = 0;
  string I = packet.Substring(pos, 1);
  pos++;
  while (pos <= packet.Length)
  switch (I)
  {
    case "0":
      {
        int l = Convert.ToInt32(packet.Substring(pos, 15), 2);
        pos += 15;
        var (version, id) = header(packet, pos);
        
        switch (id)
        {
          case 4:
            {
              pos +=  process4(packet, pos + 6);
              r += version;
              break;
            }
          case 6:
            {
              r += version;
              int v = 0;
              (pos, v) = process6(packet, pos + 6);
              break;
            }
          case 3:
            {
              r += version;
              int v = 0;
              (pos, v) = process3(packet, pos + 6);
              break;
            }
        }
        break;
      }
    case "1":
      {
        break;
      }
  }
  pos++;
  return (pos,r);
};

(int, int) process3(string packet, int pos)
{
  (int, int) r = (pos, 0);

  return r;
};

void part1()
{
  int ans = 0;
  string packet = file[0];
  string bits = "";
  packet.ToList().ForEach(c =>
  {
    int d = Convert.ToInt32(c.ToString(), 16);
    bits += Convert.ToString(d, 2).PadLeft(4, '0');
  });
  int pointer = 0;
  while (true)
  {
    var (version,id) = header(bits, pointer);
    switch (id)
    {
      case 4:
        {
          pointer = process4(bits, pointer + 6);
          ans += version;
          break;
        }
      case 6:
        {
          ans += version;
          var (pos, v) = process6(bits, pointer + 6);
          pointer += pos;
          ans += v;
          break;
        }
      case 3:
        {
          ans += version;
          var (pos, v) = process3(bits, pointer + 6);
          pointer += pos;
          ans += v;
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
