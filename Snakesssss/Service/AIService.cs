using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ookii.Dialogs.Wpf;
using Microsoft.ML;
using Snakesssss.Model;

namespace Snakesssss.Service
{
    public static  class AIService
    {
        private static MLContext mlContext;
        public  const string AiFolder = @"Samples\";
        public static async Task LearnSnake(string data, string name)
        {
            mlContext = new MLContext();

            if (data != "")
            {
                await Task.Run(() => CopyDirectory(data, AiFolder + name, true));
            }

            var rdydata = await Task.Run(() => MLModelSnakes.LoadImageFromFolder(mlContext, AiFolder));
            var qwe =   await Task.Run(() => MLModelSnakes.RetrainModel(mlContext, rdydata));
            mlContext.Model.Save(qwe,rdydata.Schema, "MLModelSnakes.mlnet");
        }

        static void CopyDirectory(string sourceDir, string destinationDir, bool recursive)
        {
            // Get information about the source directory
            var dir = new DirectoryInfo(sourceDir);

            // Check if the source directory exists
            if (!dir.Exists)
                throw new DirectoryNotFoundException($"Source directory not found: {dir.FullName}");

            // Cache directories before we start copying
            DirectoryInfo[] dirs = dir.GetDirectories();

            // Create the destination directory
            Directory.CreateDirectory(destinationDir);

            // Get the files in the source directory and copy to the destination directory
            foreach (FileInfo file in dir.GetFiles())
            {
                string targetFilePath = Path.Combine(destinationDir, file.Name);
                file.CopyTo(targetFilePath,true);
            }

            // If recursive and copying subdirectories, recursively call this method
            if (recursive)
            {
                foreach (DirectoryInfo subDir in dirs)
                {
                    string newDestinationDir = Path.Combine(destinationDir, subDir.Name);
                    CopyDirectory(subDir.FullName, newDestinationDir, true);
                }
            }
        }
    }
}
