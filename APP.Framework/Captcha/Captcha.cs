using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.Processing;
using System;
using System.IO;
using System.Linq;
using System.Numerics;

namespace APP.Framework.Captcha
{
    public class ImageCaptcha
    {
        public CaptchaModel Generate()
        {
            var r = new CaptchaModel {Point = new Point(), Id = Guid.NewGuid()};
            var imgPath = Path.GetFullPath(@"CaptchaImage");
            var imgs = Directory.GetFiles(imgPath);
            var img = imgs.OrderBy(i => Guid.NewGuid()).First();
            var sourceImg = Image.Load(img);
            using (var normalImg = sourceImg.Clone(ctx => ctx.Crop(new Rectangle(0, 0, 280, 155))))
            {
                r.Point.X = new Random().Next(60, 280 - 50);
                r.Point.Y = new Random().Next(1, 155 - 50);
                using (var slideImg = normalImg.Clone(ctx => ctx.Crop(new Rectangle(r.Point.X, r.Point.Y, 50, 50))))
                {
                    var slideLine = new PointF[] {
                    new Vector2(0, 0),
                    new Vector2(50, 0),
                    new Vector2(50, 50),
                    new Vector2(0, 50)
                };

                    slideImg.Mutate(x => x.DrawPolygon(Color.White, 2, slideLine));
                    using (var ms = new MemoryStream())
                    {
                        slideImg.SaveAsPng(ms);
                        r.SlideImg = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
                    }

                }
                normalImg.Mutate(ctx => ctx.Opacity(0.5f, new Rectangle(r.Point.X, r.Point.Y, 50, 50)));
                var normalLine = new PointF[] {
                    new Vector2(r.Point.X, r.Point.Y),
                    new Vector2(r.Point.X + 50, r.Point.Y),
                    new Vector2(r.Point.X + 50, r.Point.Y + 50),
                    new Vector2(r.Point.X, r.Point.Y + 50) };
                normalImg.Mutate(x => x.DrawPolygon(Color.White, 2, normalLine));
                using (var ms = new MemoryStream())
                {
                    normalImg.SaveAsPng(ms);
                    r.NormalImg = "data:image/png;base64," + Convert.ToBase64String(ms.ToArray());
                }
            }
            return r;
        }

        public static string CaptchaVerify(Point source, Point target)
        {
            var isChecked = Math.Abs(source.X - target.X) <= 2 && Math.Abs(source.Y - target.Y) <= 2;
            if (isChecked)
            {
                var guid = Guid.NewGuid().ToString();
                var code = guid.Substring(guid.Length - 10);
                return code;
            }
            return null;
        }
    }

    public class CaptchaModel
    {
        public Guid Id { get; set; }
        public string NormalImg { get; set; }
        public string SlideImg { get; set; }
        public Point Point { get; set; }
    }

    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

}
