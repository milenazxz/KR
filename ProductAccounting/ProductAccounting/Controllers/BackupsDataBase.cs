using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ProductAccounting.Controllers
{
    public class BackupsDataBase
    {
        public static void CreateBackup(string pgPath, string host, string port, string dbName, string name, string user, string password, string outputPath, int dayToKeep = 7) 
        {
            DeleteOldBackups(outputPath, dayToKeep);

            string time = DateTime.Now.ToString("dd.MM.yyyy_HH:mm:ss");
            string backupFile = Path.Combine(outputPath, $"backup_{time}.backup");

            var psi = new ProcessStartInfo
            {
                FileName = pgPath,
                Arguments = $"-h {host} -p {port} -U {user} -F c -b -v -f \"{backupFile}\" {dbName}",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
            };

            psi.EnvironmentVariables["PGPASSWORD"] = password;

            using (var process = Process.Start(psi))
            {
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();
                process.WaitForExit();

                if(process.ExitCode == 0)
                {
                    Console.WriteLine("Резервная копия успешно создана: " + backupFile);
                }
                else
                {
                    Console.WriteLine("Ошибка при создании резервной копии:");
                    Console.WriteLine(error);
                }
            }
        }

        private static void DeleteOldBackups(string directory, int daysToKeep) 
        {
            if (!Directory.Exists(directory))
                return;
            var files = Directory.GetFiles(directory, "backup_*.backup");

            foreach (var file in files) 
            {
                var creationTime = File.GetLastWriteTime(file);
                if(creationTime < DateTime.Now.AddDays(-daysToKeep))
                {
                    try
                    {
                        File.Delete(file);
                        Console.WriteLine("Cтарый бэкап удалён: " + file);
                    }
                    catch(Exception ex) 
                    {
                        Console.WriteLine($"Не удалось удалить файл {file}: {ex.Message}");
                    }
                }
            }
        }
    }
}
