using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LDPP_API.Common.DCLDPP
{
    public enum ICON_COLOR
    {
        Red = 0,
        Green = 1,
        Blue = 2,
        RedAndBlue = 3,
        RedAndGreen = 4,
        Unknown = 5
    };
    public class CaptureScreen
    {
        public static void PrintScreen(string picPath)
        {
            float scaling = GetScalingFactor();
            int width = (int)(Screen.PrimaryScreen.Bounds.Width * scaling);
            int height = (int)(Screen.PrimaryScreen.Bounds.Height * scaling);
            Bitmap printscreen = new Bitmap(width, height);
            Graphics graphics = Graphics.FromImage(printscreen as Image);
            graphics.CopyFromScreen(0, 0, 0, 0, printscreen.Size);
            string pt = Path.GetDirectoryName(picPath);
            if (!Directory.Exists(pt))
            {
                CreateDirRecursive(pt);
            }
            printscreen.Save(picPath, ImageFormat.Jpeg);
            printscreen.Dispose();
            //CompressionImage(picPath,Path.GetDirectoryName(picPath),1,"jpg");
            //Image img = Image.FromFile(picPath);
            //img = ZipImage(img, ImageFormat.Jpeg,200);
            //img.Save(Path.GetDirectoryName(picPath)+"/12.jpg");
        }

        //截图
        public static string ScreenShot(string fileNameBase, string component, string subDirectory = "ScreenShots")
        {
            string workingDirectory = Assembly.GetExecutingAssembly().Location;
            string projectDirectory = Directory.GetParent(workingDirectory).FullName;
            var artifactDirectory = Path.Combine(projectDirectory, subDirectory);
            if (!Directory.Exists(artifactDirectory))
            {
                Directory.CreateDirectory(artifactDirectory);
            }
            var screenShotfilepath = artifactDirectory + "\\" + component + "\\" + fileNameBase + "_" + DateTime.Now.ToString("yyyy-MM-dd_HH_mm_ss") + ".jpeg";
            PrintScreen(screenShotfilepath);
            return screenShotfilepath;
        }



        public static bool CreateDirRecursive(string path)
        {
            if (!Directory.Exists(Directory.GetParent(path).FullName))
            {
                CreateDirRecursive(Directory.GetParent(path).FullName);
            }
            Directory.CreateDirectory(path);
            if (Directory.Exists(path))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        [DllImport("gdi32.dll")]
        static extern int GetDeviceCaps(IntPtr hdc, int nIndex);
        public enum DeviceCap
        {
            VERTRES = 10,
            DESKTOPVERTRES = 117,
        }
        public static float GetScalingFactor()
        {
            Graphics g = Graphics.FromHwnd(IntPtr.Zero);
            IntPtr desktop = g.GetHdc();
            int LogicalScreenHeight = GetDeviceCaps(desktop, (int)DeviceCap.VERTRES);
            int PhysicalScreenHeight = GetDeviceCaps(desktop, (int)DeviceCap.DESKTOPVERTRES);
            float ScreenScalingFactor = (float)PhysicalScreenHeight / (float)LogicalScreenHeight;
            return ScreenScalingFactor;
        }


        public static void PrintScreenX(int srcX, int srcY, int width, int height, string picPath)
        {
            float scaling = GetScalingFactor();
            try
            {
                Bitmap printscreen = new Bitmap(width, height);
                Graphics graphics = Graphics.FromImage(printscreen as Image);
                graphics.CopyFromScreen(srcX, srcY, 0, 0, printscreen.Size);
                string pt = Path.GetDirectoryName(picPath);
                if (!Directory.Exists(pt))
                {
                    CreateDirRecursive(pt);
                }
                /*if (File.Exists(picPath))
                {
                    File.Delete(picPath);
                }*/
                printscreen.Save(picPath, ImageFormat.Jpeg);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

            }

        }
        //Marcus: Sometimes , we want to capture only a part of screen to deal with
        //It passed my test with Scale 125% on my P1
        public static Bitmap ScreenShotWithSpecificArea(int srcX, int srcY, int width, int height)
        {
            try
            {
                float scaling = GetScalingFactor();
                width = (int)(width * scaling);
                height = (int)(height * scaling);
                Bitmap printscreen = new Bitmap(width, height);
                Graphics graphics = Graphics.FromImage(printscreen as Image);
                graphics.CopyFromScreen((int)(srcX * scaling), (int)(srcY * scaling), 0, 0, new Size(width, height));
                return printscreen;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

            }
            return null;
        }

        public static ICON_COLOR GetColor(string picPath)
        {
            //PrintScreenX(300,300,"D:\\img\\test1.jpeg");
            Image img = Image.FromFile(picPath);
            Bitmap bm = new Bitmap(img);
            int Gcount = 0;
            int Rcount = 0;
            int Bcount = 0;
            for (int i = 0; i < bm.Height; i++)
                for (int j = 0; j < bm.Width; j++)
                {
                    Color color = bm.GetPixel(j, i);
                    byte R = color.R;
                    byte G = color.G;
                    byte B = color.B;
                    //string str = R.ToString() + ":" + G.ToString() + ":" + B.ToString();
                    //Console.WriteLine(str);
                    if (R < 100 && G > 150 && B < 150)
                    {
                        Gcount++;
                        // str = R.ToString() + ":" + G.ToString() + ":" + B.ToString();
                        //Console.WriteLine(str);
                    }
                    if (R > 150 && G < 100 && B < 100)
                    {
                        Rcount++;
                        //str = R.ToString() + ":" + G.ToString() + ":" + B.ToString();
                        //Console.WriteLine(str);
                    }
                    if ((R > 50 && R < 90) && G > 50 && B > 240)
                    {
                        Bcount++;
                        //str = R.ToString() + ":" + G.ToString() + ":" + B.ToString();
                        //Console.WriteLine(str);
                    }
                    // System.Diagnostics.Debug.WriteLine(str);
                }
            bool hasGreen = Gcount * 1.0 / (bm.Height * bm.Width) > 0.02;
            bool hasRed = Rcount * 1.0 / (bm.Height * bm.Width) > 0.02;
            bool hasBlue = Bcount * 1.0 / (bm.Height * bm.Width) > 0.02;
            if (hasRed && !hasGreen && !hasBlue)
            {
                return ICON_COLOR.Red;
            }
            if (hasGreen && !hasRed)
            {
                return ICON_COLOR.Green;
            }
            if (hasBlue && !hasRed)
            {
                return ICON_COLOR.Blue;
            }
            return ICON_COLOR.Unknown;
        }
        //https://cdc.tencent.com/2011/05/09/%E8%89%B2%E7%94%9F%E5%BF%83%E4%B8%AD%EF%BC%9A%E4%BA%BA%E6%80%A7%E5%8C%96%E7%9A%84hsl%E6%A8%A1%E5%9E%8B/
        public static ICON_COLOR GetColor_HSL(string picPath)
        {
            //PrintScreenX(300,300,"D:\\img\\test1.jpeg");
            bool hasGreen, hasRed, hasBlue;
            using (Image img = Image.FromFile(picPath))
            using (Bitmap bm = new Bitmap(img))
            {
                int Gcount = 0;
                int Rcount = 0;
                int Bcount = 0;
                for (int i = 0; i < bm.Height; i++)
                    for (int j = 0; j < bm.Width; j++)
                    {
                        Color color = bm.GetPixel(j, i);
                        byte R = color.R;
                        byte G = color.G;
                        byte B = color.B;
                        System.Drawing.Color hsl = System.Drawing.Color.FromArgb(color.ToArgb());
                        string str = string.Format("R={0} G={1}B={2}, H={3} S={4} L={5}", R, G, B, hsl.GetHue(), hsl.GetSaturation(), hsl.GetBrightness());

                        //Green
                        if ((hsl.GetHue() > 80 && hsl.GetHue() < 170) && hsl.GetSaturation() > 0.7 && (hsl.GetBrightness() > 0.35 && hsl.GetBrightness() < 0.6))
                        {
                            Gcount++;
                            //Console.WriteLine(str);
                        }

                        //朱红
                        if ((hsl.GetHue() > 0 && hsl.GetHue() < 10) && hsl.GetSaturation() > 0.6 && (hsl.GetBrightness() > 0.35 && hsl.GetBrightness() < 0.7))
                        {
                            Rcount++;
                            //Console.WriteLine(str);
                        }
                        //大红
                        if ((hsl.GetHue() >= 330) && hsl.GetSaturation() > 0.6 && (hsl.GetBrightness() > 0.45 && hsl.GetBrightness() < 0.7))
                        {
                            Rcount++;
                            //Console.WriteLine(str);
                        }
                        //Blue
                        if ((hsl.GetHue() >= 170 && hsl.GetHue() < 260) && hsl.GetSaturation() > 0.6 && (hsl.GetBrightness() > 0.35 && hsl.GetBrightness() < 0.6))
                        {
                            Bcount++;
                            str = R.ToString() + ":" + G.ToString() + ":" + B.ToString();
                            //Console.WriteLine(str);
                        }
                        HashSet<string> debugInfo = new HashSet<string>();
                        debugInfo.Add(str);
                        foreach (string _colorinfo in debugInfo)
                        {
                            //System.Diagnostics.Debug.WriteLine(_colorinfo);
                        }
                        //System.Diagnostics.Debug.WriteLine(str);
                    }
                hasGreen = Gcount * 1.0 / (bm.Height * bm.Width) > 0.02;
                hasRed = Rcount * 1.0 / (bm.Height * bm.Width) > 0.02;
                hasBlue = Bcount * 1.0 / (bm.Height * bm.Width) > 0.02;
            }
            if (hasRed && !hasGreen && !hasBlue)
            {
                return ICON_COLOR.Red;
            }
            if (hasGreen && !hasRed)
            {
                return ICON_COLOR.Green;
            }
            if (hasBlue && !hasRed)
            {
                return ICON_COLOR.Blue;
            }
            return ICON_COLOR.Unknown;
        }
        //https://www.cnblogs.com/lihaiping/p/RGB.html , How to create color by R.G.B
        //https://www.sojson.com/web/panel.html , watch how R.G.B change 
        //http://hslpicker.com/#f8a9a0
        public static ICON_COLOR GetColor(int srcX, int srcY, int width, int height)
        {
            float scaling = GetScalingFactor();

            Bitmap printscreen = new Bitmap(width, height);
            Graphics graphics = Graphics.FromImage(printscreen as Image);
            graphics.CopyFromScreen(srcX, srcY, 0, 0, printscreen.Size);

            int Gcount = 0;
            int Rcount = 0;
            int Bcount = 0;
            for (int i = 0; i < printscreen.Height; i++)
                for (int j = 0; j < printscreen.Width; j++)
                {
                    Color color = printscreen.GetPixel(j, i);
                    byte R = color.R;
                    byte G = color.G;
                    byte B = color.B;
                    string str = R.ToString() + ":" + G.ToString() + ":" + B.ToString();
                    //Console.WriteLine(str);
                    if (R < 100 && G > 150 && B < 150)
                    {
                        Gcount++;
                        str = R.ToString() + ":" + G.ToString() + ":" + B.ToString();
                        //Console.WriteLine(str);
                    }
                    if (R > 150 && G < 100 && B < 100)
                    {
                        Rcount++;
                        str = R.ToString() + ":" + G.ToString() + ":" + B.ToString();
                        //Console.WriteLine(str);
                    }
                    if ((R > 50 && R < 90) && G > 50 && B > 250)
                    {
                        Bcount++;
                        str = R.ToString() + ":" + G.ToString() + ":" + B.ToString();
                        //Console.WriteLine(str);
                    }

                }
            bool hasGreen = Gcount * 1.0 / (printscreen.Height * printscreen.Width) > 0.02;
            bool hasRed = Rcount * 1.0 / (printscreen.Height * printscreen.Width) > 0.02;
            bool hasBlue = Bcount * 1.0 / (printscreen.Height * printscreen.Width) > 0.02;
            if (hasRed && !hasGreen && !hasBlue)
            {
                return ICON_COLOR.Red;
            }
            if (hasGreen && !hasRed)
            {
                return ICON_COLOR.Green;
            }
            if (hasBlue && !hasRed)
            {
                return ICON_COLOR.Blue;
            }
            return ICON_COLOR.Unknown;
        }

        #region 图片压缩  效果不明显

        /// <summary>
        /// 图片压缩
        /// </summary>
        /// <param name="imagePath">图片文件路径</param>
        /// <param name="targetFolder">保存文件夹</param>
        /// <param name="quality">压缩质量</param>
        /// <param name="fileSuffix">压缩后的文件名后缀（防止直接覆盖原文件）</param>
        public static void CompressionImage(string imagePath, string targetFolder, long quality = 100, string fileSuffix = "compress")
        {
            if (!File.Exists(imagePath))
            {
                throw new FileNotFoundException();
            }
            if (!Directory.Exists(targetFolder))
            {
                Directory.CreateDirectory(targetFolder);
            }
            var fileInfo = new FileInfo(imagePath);
            var fileName = fileInfo.Name.Replace(fileInfo.Extension, "");
            var fileFullName = Path.Combine($"{targetFolder}", $"{fileName}_{fileSuffix}{fileInfo.Extension}");

            var imageByte = CompressionImage(imagePath, quality);
            var ms = new MemoryStream(imageByte);
            var image = Image.FromStream(ms);
            image.Save(fileFullName);
            ms.Close();
            ms.Dispose();
            image.Dispose();
        }
        private static byte[] CompressionImage(string imagePath, long quality)
        {
            using (var fileStream = new FileStream(imagePath, FileMode.Open))
            {
                using (var img = Image.FromStream(fileStream))
                {
                    using (var bitmap = new Bitmap(img))
                    {
                        var codecInfo = GetEncoder(img.RawFormat);
                        var myEncoder = System.Drawing.Imaging.Encoder.Quality;
                        var myEncoderParameters = new EncoderParameters(1);
                        var myEncoderParameter = new EncoderParameter(myEncoder, quality);
                        myEncoderParameters.Param[0] = myEncoderParameter;
                        using (var ms = new MemoryStream())
                        {
                            bitmap.Save(ms, codecInfo, myEncoderParameters);
                            myEncoderParameters.Dispose();
                            myEncoderParameter.Dispose();
                            return ms.ToArray();
                        }
                    }
                }
            }
        }

        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            var codecs = ImageCodecInfo.GetImageDecoders();
            return codecs.FirstOrDefault(codec => codec.FormatID == format.Guid);
        }

        #endregion

        #region  图片压缩2 

        /// <summary>
        /// 压缩图片至200 Kb以下
        /// </summary>
        /// <param name="img">图片</param>
        /// <param name="format">图片格式</param>
        /// <param name="targetLen">压缩后大小</param>
        /// <param name="srcLen">原始大小</param>
        /// <returns>压缩后的图片</returns>
        public static Image ZipImage(Image img, ImageFormat format, long targetLen, long srcLen = 0)
        {
            //设置大小偏差幅度 10kb
            const long nearlyLen = 10240;
            //内存流  如果参数中原图大小没有传递 则使用内存流读取
            var ms = new MemoryStream();
            if (0 == srcLen)
            {
                img.Save(ms, format);
                srcLen = ms.Length;
            }

            //单位 由Kb转为byte 若目标大小高于原图大小，则满足条件退出
            targetLen *= 1024;
            if (targetLen > srcLen)
            {
                ms.SetLength(0);
                ms.Position = 0;
                img.Save(ms, format);
                img = Image.FromStream(ms);
                return img;
            }

            //获取目标大小最低值
            var exitLen = targetLen - nearlyLen;

            //初始化质量压缩参数 图像 内存流等
            var quality = (long)Math.Floor(100.00 * targetLen / srcLen);
            var parms = new EncoderParameters(1);

            //获取编码器信息
            ImageCodecInfo formatInfo = null;
            var encoders = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo icf in encoders)
            {
                if (icf.FormatID == format.Guid)
                {
                    formatInfo = icf;
                    break;
                }
            }

            //使用二分法进行查找 最接近的质量参数
            long startQuality = quality;
            long endQuality = 100;
            quality = (startQuality + endQuality) / 2;

            while (true)
            {
                //设置质量
                parms.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);

                //清空内存流 然后保存图片
                ms.SetLength(0);
                ms.Position = 0;
                img.Save(ms, formatInfo, parms);

                //若压缩后大小低于目标大小，则满足条件退出
                if (ms.Length >= exitLen && ms.Length <= targetLen)
                {
                    break;
                }
                else if (startQuality >= endQuality) //区间相等无需再次计算
                {
                    break;
                }
                else if (ms.Length < exitLen) //压缩过小,起始质量右移
                {
                    startQuality = quality;
                }
                else //压缩过大 终止质量左移
                {
                    endQuality = quality;
                }

                //重新设置质量参数 如果计算出来的质量没有发生变化，则终止查找。这样是为了避免重复计算情况{start:16,end:18} 和 {start:16,endQuality:17}
                var newQuality = (startQuality + endQuality) / 2;
                if (newQuality == quality)
                {
                    break;
                }
                quality = newQuality;
                //Console.WriteLine("start:{0} end:{1} current:{2}", startQuality, endQuality, quality);
            }
            img = Image.FromStream(ms);
            return img;
        }

        /// <summary>
        ///获取图片格式
        /// </summary>
        /// <param name="img">图片</param>
        /// <returns>默认返回JPEG</returns>
        public ImageFormat GetImageFormat(Image img)
        {
            if (img.RawFormat.Equals(ImageFormat.Jpeg))
            {
                return ImageFormat.Jpeg;
            }
            if (img.RawFormat.Equals(ImageFormat.Gif))
            {
                return ImageFormat.Gif;
            }
            if (img.RawFormat.Equals(ImageFormat.Png))
            {
                return ImageFormat.Png;
            }
            if (img.RawFormat.Equals(ImageFormat.Bmp))
            {
                return ImageFormat.Bmp;
            }
            return ImageFormat.Jpeg;//根据实际情况选择返回指定格式还是null
        }
        #endregion
    }
}
