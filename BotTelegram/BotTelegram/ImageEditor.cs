using Microsoft.Bot.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Bot.Builder.Adapters;

namespace BotTelegram
{
    public class ImageEditor
    {
        public void ConvertToGrayscale(Attachment attach, string name)
        {
            DirectoryInfo di = new DirectoryInfo(Directory.GetCurrentDirectory() + @"\image\");
            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            var request = WebRequest.Create(attach.ContentUrl);
            var response = request.GetResponse();
            var responseStream = response.GetResponseStream();
            Bitmap bmp = new Bitmap(responseStream);
            // Process the pixels.
            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    Color c = bmp.GetPixel(i, j);

                    //Apply conversion equation
                    byte gray = (byte)(.21 * c.R + .71 * c.G + .071 * c.B);

                    //Set the color of this pixel
                    bmp.SetPixel(i, j, Color.FromArgb(gray, gray, gray));
                }
            }
            var img = (Image)bmp;
            img.Save(Directory.GetCurrentDirectory() + @"\image\" + name + @".jpeg");
        }
    }
}
