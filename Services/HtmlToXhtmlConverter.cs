using HtmlAgilityPack;
using System.IO;
using System.Text;

namespace HomeLibrary.Services
{
    public static class HtmlToXhtmlConverter
    {
        public static string Convert(string html)
        {
            if (string.IsNullOrWhiteSpace(html))
                return string.Empty;

            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            // Опция для корректного вывода самозакрывающихся тегов
            doc.OptionWriteEmptyNodes = true;

            using (var writer = new StringWriter())
            {
                doc.Save(writer);
                return writer.ToString();
            }
        }
    }
}