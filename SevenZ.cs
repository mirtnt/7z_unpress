using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7z_Uncompress
{
    internal class SevenZ
    {
        public string file_title { get; set; }
        public string file_name { get; set; }
        public string password { get; set; }

        public SevenZ()
        {

        }
        public SevenZ(string title, string name, string pw)
        {
            file_title = title;
            file_name = name;
            password = pw;
        }
        public override string ToString()
        {
            return $"title {this.file_title} \n" +
                $"name {this.file_name} \n" +
                $"password {this.password} \n";
        }
        public bool uncompress(int Mod)
        {
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            //process.StartInfo.CreateNoWindow = true;
            process.StartInfo.CreateNoWindow = false;

            process.Start();
            var path = System.Environment.CurrentDirectory;

            if(Directory.Exists($"{path}\\{this.file_name}"))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{this.file_name} 文件是文件夹形式");
                Console.ForegroundColor = ConsoleColor.White;
                //File.WriteAllTextAsync($"{path}\\this.file_name\\this.file_name.txt", this.ToString());
                //using var fs = new FileStream($"{path}\\{this.file_name}\\{this.file_name}.txt", FileMode.OpenOrCreate);
                using var fs = new FileStream($"{path}\\{this.file_name}\\{file_title[..(file_title.Length > 50 ? 50 : file_title.Length)]}.md", FileMode.OpenOrCreate);
                using var msg = new StreamWriter(fs);
                msg.WriteLine(this.ToString());

                msg.Close();

                return false;
            }

            if (!File.Exists($"{path}\\{this.file_name}"))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{this.file_name} 文件不存在");
                Console.ForegroundColor = ConsoleColor.White;
                return false;
            }

            //Console.WriteLine("运行路径 : "+System.Environment.CurrentDirectory);
            foreach (char c in Path.GetInvalidFileNameChars())
            {
                file_title = file_title.Replace(c, '_');
            }
            Console.WriteLine("标题名" + file_title);
            //process.StandardInput.WriteLine($"7z x {this.file_name} -p{this.password} -od:\\doc");
            //Console.WriteLine($"7z x {this.file_name} -p{this.password} -o{path}\\{file_title[..(file_title.Length > 50 ? 50 : file_title.Length)]}");
            process.StandardInput.WriteLine($"7z x {this.file_name} -p{this.password} -o\"{path}\\{file_title[..(file_title.Length > 50 ? 50 : file_title.Length)]}\"");
            //$".\\{this.file_title}");


            //Console.WriteLine("输入关闭");
            process.StandardInput.Close();

            var message = process.StandardOutput.ReadToEnd();

            process.Close();

            //Console.WriteLine("以下是message输出");
            //Console.WriteLine(message);
            if (message.Contains("Everything is Ok"))
            {
                using var fs = new FileStream($"{path}\\{file_title[..(file_title.Length > 50 ? 50 : file_title.Length)]}\\{file_title[..(file_title.Length > 50 ? 50 : file_title.Length)]}.md", FileMode.OpenOrCreate);
                using var msg = new StreamWriter(fs);
                msg.WriteLine(this.ToString());
                //msg.Close();
                return true;
            }
                

            Console.WriteLine("Error ! \n");
            Console.ForegroundColor = ConsoleColor.Yellow;
            var messages = message.Split('\n').Skip(3).SkipLast(3);
            //var messages=message.Split('\n').Skip(3);
            Console.ForegroundColor = ConsoleColor.Red;
            foreach (string c in messages)
            {
                Console.WriteLine(c);
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n");

            return false;
        }

        public bool uncompress()
        {
            Process process = new Process();

            //process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.FileName = "powershell.exe";
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;
            //process.StartInfo.CreateNoWindow = true;
            process.StartInfo.CreateNoWindow = false;

            process.Start();
            //process.StandardInput.WriteLine("7z.exe x '.\\PW - 副本.txt'");
            process.StandardInput.WriteLine("7z x .\\502pw.rar");
            //var message = process.StandardOutput.ReadToEnd();
            var message = process.StandardOutput.ReadLine();
            //Console.WriteLine(message);
            //process.StandardInput.WriteLine("ipconfig");
            //process.StandardInput.WriteLine("7z.exe -h");

            while (!process.StandardOutput.EndOfStream)
            {
                message = process.StandardOutput.ReadLine();
                Console.WriteLine(message);
            }

            Console.WriteLine("输入关闭");
            process.StandardInput.Close();
            //process.StandardInput.WriteLine("exit");
            //message+= process.StandardOutput.ReadToEnd();


            message = process.StandardOutput.ReadToEnd();
            //var message = process.StandardOutput.ReadToEnd();
            //var message=process.StandardError.ReadToEnd();

            process.Close();

            Console.WriteLine("以下是message输出");
            Console.WriteLine(message);
            if (message.Contains("Everything is Ok")) return true;
            return false;
        }
    }
}
