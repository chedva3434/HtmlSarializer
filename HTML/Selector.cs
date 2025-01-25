using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace HTML
{
    internal class Selector
    {
        public string TagName { get; set; }
        public string Id { get; set; }
        public List<string> Classes { get; set; }
        public Selector Child  { get; set; }
        public Selector Parent { get; set; }

        public static Selector ParseSelectorString(string selectorString)
        {
            Selector root = new Selector();
            Selector current=root;
            string[] parts = selectorString.Split(" ");
            foreach (string part in parts) 
            {
                Console.WriteLine("part: " + part);
                string[] qualities = part.Split('#', '.');
                string level = part;
                foreach (string quality in qualities) 
                {
                    Console.WriteLine("quality: " + quality);
                    Console.WriteLine("level: " + level);
                    if (level.StartsWith('#'))
                    {
                        current.Id = quality;
                        level = level.Substring(quality.Length + 1);
                    }

                    if (level.StartsWith('.'))
                    {
                        current.Classes.Add(quality);
                        level = level.Substring(quality.Length + 1);
                    }
                    else
                    {
                        if (HtmlHelper.Instance.TagsInHtml.Contains(quality))
                        {
                            level = level.Substring(quality.Length);
                            current.TagName = quality;
                        }
                        else
                            Console.WriteLine("invalid parameter");
                    }
                }
                if (current.Parent == null)
                    root =  current;
                Selector childSelector = new Selector();
                current.Child = childSelector;
                childSelector.Parent = current;
                current = childSelector;
            }
            current.Parent.Child = null;
            return root;

        }
    }
}
