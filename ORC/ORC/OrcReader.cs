namespace ORC.Helper
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;

    public class OrcReader
    {
        public static async Task<string> ReadAsync(IFormFile file, string currentDirectory = null)
        {
            currentDirectory ??= Directory.GetCurrentDirectory();
            
            var tempImagePath = $"{currentDirectory}/StaticFiles/{Guid.NewGuid()}{file.FileName}";
            await using (var fileStream = new FileStream(tempImagePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
                    
            var tempReadResultFile = ReadImage(currentDirectory, tempImagePath);

            var tempReadResultTextFile = $"{tempReadResultFile}.txt";
            var result = await File.ReadAllTextAsync(tempReadResultTextFile);

            File.Delete(tempImagePath);
            File.Delete(tempReadResultTextFile);
            
            return result;
        }

        private static string ReadImage(string currentDirectory, string tempImagePath)
        {
            var tempReadResultFile = $"{currentDirectory}/StaticFiles/{Guid.NewGuid()}";
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = $"{currentDirectory}/StaticFiles/ExecTesseract.sh",
                    Arguments = $"{tempImagePath} {tempReadResultFile}"
                }
            };

            process.Start();
            //Needs time to process the new file for some reason
            Thread.Sleep(1000);
            
            return tempReadResultFile;
        }
    }
}