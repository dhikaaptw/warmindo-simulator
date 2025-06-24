using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace Warmindo_Simulator.src
{
    internal static class AssetManager
    {
        private static Dictionary<string, Image> imageCache = new Dictionary<string, Image>();

        public static Image LoadImage(string path)
        {
            if (imageCache.ContainsKey(path))
            {
                return imageCache[path];
            }

            if (File.Exists(path))
            {
                Image img = Image.FromFile(path);
                imageCache[path] = img;
                return img;
            }
            
            throw new FileNotFoundException($"Asset not found: {path}");
        }

        public static List<Image> LoadFrames(string folderPath, int frameCount)
        {
            List<Image> frames = new List<Image>();
            for (int i = 1; i <= frameCount; i++)
            {
                string filePath = Path.Combine(folderPath, $"{i}.png");
                if (File.Exists(filePath))
                {
                    frames.Add(LoadImage(filePath));
                }
                else
                {
                    Console.WriteLine($"Frame not found: {filePath}");
                }
            }
            return frames;
        }
    }
}