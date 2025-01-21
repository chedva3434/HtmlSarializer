using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace HTML
{
    internal class HtmlHelper
    {
        private readonly static HtmlHelper _instance = new HtmlHelper();
        public static HtmlHelper Instance => _instance;
        public List<string> ClosingTags { get; set; }

        public List<string> OpeningTags { get; set; }

        private HtmlHelper()
        {

            var HtmlTags = File.ReadAllText("JSON Files/HtmlTags.json");
            ClosingTags = JsonSerializer.Deserialize<List<string>>(HtmlTags);

            var HtmlVoidTags = File.ReadAllText("JSON Files/HtmlVoidTags.json");
            OpeningTags = JsonSerializer.Deserialize<List<string>>(HtmlVoidTags);

        }

    }
}
