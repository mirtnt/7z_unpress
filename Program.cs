// See https://aka.ms/new-console-template for more information
using _7z_Uncompress;
using System.Diagnostics;

Console.WriteLine("Hello, World!\n");
Console.WriteLine("输入密码文件名");
var pwfile = Console.ReadLine();
//var file = new FileInfo("PW.txt");
//var file = new FileInfo(pwfile + ".txt");
var file = new FileInfo(pwfile);

var msg = new StreamReader(file.OpenRead());
var fileList = new List<SevenZ>();

//while(!msg.EndOfStream)
//{
//    Console.WriteLine(msg.ReadLine());
//}
var title = msg.ReadLine();
var pw = msg.ReadLine();
var name = msg.ReadLine();

if (msg.ReadLine() == "")
{
    fileList.Add(new SevenZ(title, name, pw));
}
while (!msg.EndOfStream)
{
    title = msg.ReadLine();
    pw = msg.ReadLine();
    name = msg.ReadLine();
    if (title != "" ) fileList.Add(new SevenZ(title, name, pw));
    msg.ReadLine();
}

foreach (var item in fileList)
{
    //Console.WriteLine(item);
    Console.WriteLine(item.uncompress(1));
    Console.WriteLine();
}


Console.WriteLine("运行结束,按任意键结束");
Console.ReadKey();


