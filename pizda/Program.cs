using System.Diagnostics;
using Process = System.Diagnostics.Process;

namespace pizda
{
    internal class Program
    {
        static string Crack;
        static int pos = 2;
        static void Main()
        {
            Console.WriteLine("Тыкни конченый пробел");
            Console.CursorVisible = false;
            diskInfo();
        }

        private static void diskInfo()
        {
            pos = 2;
            DriveInfo[] allDrives = DriveInfo.GetDrives();
            var key = Console.ReadKey();
            while (key.Key != ConsoleKey.Enter)
            {
                Console.Clear();
                Console.WriteLine("   Диск\t        Тип\t        Всего\t        Занято\n" + "   " + "---------------------------------------------------");
                foreach (DriveInfo disk in allDrives)
                {
                    Console.WriteLine("   " + disk.Name +
                              "\t\t" + disk.DriveFormat +
                              "\t\t" + disk.TotalSize / 1073741824 + " Гб" +
                              "\t\t" + disk.TotalFreeSpace / 1073741824 + " Гб");
                }

                key = Arrow(key, ref pos);


            }

            Console.Clear();
            Crack = allDrives[pos - 2].Name;
            PokashiMnyInside();
        }

        static void PokashiMnyInside()
        {
            pos = 2;
            Console.WriteLine("   " + "Имя" + "\t\t\t   " + "        Последние изменение | " + "" + "F1 для создания папки | F2 для создания файла |\n" + "   " + "------------------------------------------------------------------------------------------------------");

            string[] allDirectories = Directory.GetDirectories(Crack);
            int b = 2;
            foreach (string dir in allDirectories)
            {
                string dirName = new DirectoryInfo(dir).Name;
                DateTime dateTime = File.GetLastWriteTime(dir);

                Console.Write("   " + dirName);
                Console.SetCursorPosition(35, b);
                b++;
                Console.WriteLine(dateTime);
            }

            string[] allFiles = Directory.GetFiles(Crack);
            foreach (string file in allFiles)
            {
                string fileName = new FileInfo(file).Name;
                DateTime dateTime = File.GetLastWriteTime(file);

                Console.Write("   " + fileName);
                Console.SetCursorPosition(35, b);
                b++;
                Console.WriteLine(dateTime);
            }

            var key = Console.ReadKey(true);
            while (key.Key != ConsoleKey.Enter)
            {
                for (int i = 0; i < (allFiles.Length + allDirectories.Length + 2); i++)
                {
                    Console.SetCursorPosition(0, i);
                    Console.Write("   ");
                }

                key = Arrow(key, ref pos);
            }

            Console.Clear();
            if (pos < allDirectories.Length + 2)
            {
                Crack = allDirectories[pos - 2];
                PokashiMnyInside();
            }
            else
            {
                Process.Start
                (
                    new ProcessStartInfo
                    {
                        FileName = allFiles[pos - (allDirectories.Length + 2)],
                        UseShellExecute = true
                    }
                );
            }
        }

        private static ConsoleKeyInfo Arrow(ConsoleKeyInfo key, ref int position)
        {
            switch (key.Key)
            {
                case ConsoleKey.UpArrow:
                    position--;
                    break;
                case ConsoleKey.DownArrow:
                    position++;
                    break;
                case ConsoleKey.LeftArrow:
                    diskInfo();
                    break;
                case ConsoleKey.F1:
                    AddDir();
                    break;
                case ConsoleKey.F2:
                    AddFile();
                    break;
                case ConsoleKey.Escape:
                    Console.Clear();
                    Console.WriteLine("Программа закрыта");
                    pos = 5;
                    Environment.Exit(Environment.ExitCode);
                    break;
            }

            Console.SetCursorPosition(0, position);
            Console.WriteLine("->");
            key = Console.ReadKey();
            return key;
        }

        static void AddDir()
        {
            Console.SetCursorPosition(65, 2);
            Console.WriteLine("Название папки: \n");
            Console.SetCursorPosition(65, 3);
            string name = Console.ReadLine();
            string crack = Crack + "\\" + name;
            Directory.CreateDirectory(crack);
            Console.Clear();
            PokashiMnyInside();
        }

        static void AddFile()
        {
            Console.SetCursorPosition(65, 2);
            Console.WriteLine("Название файла: \n");
            Console.SetCursorPosition(65, 3);
            string name = Console.ReadLine();
            string crack = Crack + "\\" + name;
            File.Create(crack);
            Console.Clear();
            PokashiMnyInside();
        }
    }
}