using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;

namespace CarsSale.WebUi.Attributes
{
    public class ValidateHttpPostedFileBaseAsImageAttribute : RequiredAttribute
    {
        public override bool IsValid(object value)
        {
            if (!(value is HttpPostedFileBase file))
            {
                return false;
            }

            var allowedFormats = new []
            {
                ImageFormat.Jpeg,
                ImageFormat.Png,
                ImageFormat.Gif,
                ImageFormat.Bmp
            };

            try
            {
                using (var img = Image.FromStream(file.InputStream))
                {
                    return allowedFormats.Contains(img.RawFormat);
                }
            }
            catch (Exception)
            {
                return false;
            }            
        }
    }
}