using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Bc.Web.Mvc.Html
{
    public static class ImageExtensions
    {
        public static MvcHtmlString PhotoUploader(this HtmlHelper html, string name, string currentImageUrl = "")
        {
            // Contenedor principal
            TagBuilder container = new TagBuilder("div");
            container.AddCssClass("photo-uploader-container");

            // Imagen de vista previa
            TagBuilder img = new TagBuilder("img");
            img.Attributes.Add("id", $"preview-{name}");
            img.Attributes.Add("src", string.IsNullOrEmpty(currentImageUrl) ? "/content/placeholder.png" : currentImageUrl);
            img.Attributes.Add("style", "width:200px; height:200px; display:block; border:1px solid #ccc; margin-bottom:10px; object-fit:cover;");

            // Input de archivo (el que el usuario clickea)
            TagBuilder fileInput = new TagBuilder("input");
            fileInput.Attributes.Add("type", "file");
            fileInput.Attributes.Add("id", $"file-{name}");
            fileInput.Attributes.Add("accept", "image/*");
            fileInput.Attributes.Add("onchange", $"previewImage(this, '{name}')");

            // Input oculto (el que realmente envía la data al controlador)
            string hiddenInput = html.Hidden(name, "").ToHtmlString();

            container.InnerHtml = img.ToString(TagRenderMode.SelfClosing) +
                                 fileInput.ToString(TagRenderMode.SelfClosing) +
                                 hiddenInput;

            return MvcHtmlString.Create(container.ToString());
        }
    }
}
