﻿using System;
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
        public List<string> TagsInHtml { get; set; }

        public List<string> DoNotRequireClosingTags { get; set; }

        

        private HtmlHelper()
        {

            var HtmlTags = File.ReadAllText("files/HtmlTags.json");
            TagsInHtml = JsonSerializer.Deserialize<List<string>>(HtmlTags);

            var HtmlVoidTags = File.ReadAllText("files/HtmlVoidTags.json");
            DoNotRequireClosingTags = JsonSerializer.Deserialize<List<string>>(HtmlVoidTags);

        }

    }
}
