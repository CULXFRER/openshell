using System.Diagnostics;

public class Program
{
    static void Main()
    {
        Console.WriteLine("Hello, this is simple open shell!");
        string id = "@";
    Run:  
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write(Environment.UserName);
        if (id == "@")
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
        }

        Console.Write($" {id} ");
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write(Environment.UserDomainName + " ");
        Console.ResetColor();

        string i = Console.ReadLine().ToString();

        if (i == "help" | i == "info")
        {
            Console.WriteLine("This is a cmd like program. Writen by Matnazarov Sobirjon.\n" +
                              "Type command or get-command to view available commands.");
            goto Run;
        }
        else if (i == "clear")
        {
            Console.Clear();
            goto Run;
        }
        else if (i == "exit")
        {
            Environment.Exit(0);
        }
        else if (i.Contains("echo"))
        {
            if (i.Length > 4) Console.WriteLine(i.Remove(0, 4).TrimStart());
            goto Run;
        }
        else if (i == "get-command" | i == "command")
        {
            Console.WriteLine(
                              "help or info           | helps with using the program\n" +
                              "clear                  | сlears the window\n" +
                              "echo                   | displaying what you type\n" +
                              "get-command or command | viewing available commands\n" +
                              "cmd                    | runs a windows shell\n" +
                              "powershell             | runs a windows powershell\n" +
                              "adm                    | shell running mode\n" +
                              "reset                  | reset the shell\n" +
                              "exit                   | exit from the shell\n"
                             );
            goto Run;
        }
        else if (i == "cmd")
        {
            try
            {
                if (id == "@")
                {
                    Process proc = new Process();
                    proc.StartInfo.FileName = "cmd.exe";
                    proc.StartInfo.WorkingDirectory = Environment.CurrentDirectory;
                    proc.StartInfo.Arguments = "";
                    proc.Start();
                    proc.WaitForExit();
                    goto Run;
                }
                else if (id == "#")
                {
                    Process proc = new Process();
                    proc.StartInfo.FileName = "cmd.exe";
                    proc.StartInfo.WorkingDirectory = Environment.CurrentDirectory;
                    proc.StartInfo.UseShellExecute = true;
                    proc.StartInfo.Verb = "runas";
                    proc.Start();
                    goto Run;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in launching process. Details are in the system log:");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.ToString());
                Console.ResetColor();
                goto Run;
            }
        }
        else if (i == "powershell")
        {
            try
            {
                if (id == "@")
                {
                    Process proc = new Process();
                    proc.StartInfo.FileName = "powershell.exe";
                    proc.StartInfo.WorkingDirectory = Environment.CurrentDirectory;
                    proc.StartInfo.Arguments = "";
                    proc.Start();
                    proc.WaitForExit();
                    goto Run;
                }
                if (id == "#")
                {
                    Process proc = new Process();
                    proc.StartInfo.FileName = "powershell.exe";
                    proc.StartInfo.WorkingDirectory = Environment.CurrentDirectory;
                    proc.StartInfo.UseShellExecute = true;
                    proc.StartInfo.Verb = "runas";
                    proc.Start();
                    goto Run;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in launching process. Details are in the system log:");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Message);
                Console.ResetColor();
                goto Run;
            }
        }
        else if (i.Contains("adm"))
        {
            if (i == "adm")
            {
                Console.WriteLine("adm - shell runing mode.\n" +
                                  "   Arguments:\n" +
                                  "   on - switches to superuser mode\n" +
                                  "   off - switches to normal mode\n");
                goto Run;
            }
            if (i == "adm on")
            {
                id = "#";
                Console.WriteLine("You have switched to superuser mode. Please, respect people's privacy");
                goto Run;
            }
            if (i == "adm off")
            {
                id = "@";
                goto Run;
            }
            else
            {
                Console.WriteLine("adm: unavailable command");
                goto Run;
            }
        }
        else if (i == "reset")
        {
            Application.Restart();
        }
        else
        {
            Process proc = new Process();

            if (i != "")
            {
                try
                {
                    try
                    {
                        proc.StartInfo.FileName = i.Replace(i.Remove(0, i.Split()[0].Length), "");
                    }
                    catch(Exception e)
                    {
                        proc.StartInfo.FileName = i;
                    }
                    proc.StartInfo.Arguments = i.Remove(0, i.Split()[0].Length);
                    proc.StartInfo.WorkingDirectory = Environment.CurrentDirectory;
                    if (id == "#")
                    {
                        proc.StartInfo.Verb = "runas";
                    }
                    proc.Start();
                    proc.WaitForExit();
                    goto Run;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error in launching process. Details are in the system log:");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(e.Message);
                    Console.ResetColor();
                    goto Run;
                }
            }
            else
            {
                Console.WriteLine("Null command!");
                goto Run;
            }
        }
    }
}