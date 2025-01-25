
using HTML;
using System.Diagnostics.Tracing;
using System.Text.RegularExpressions;


var html = await Load("https://learn.malkabruk.co.il/");

var cleanHtml = new Regex("\\s").Replace(html, "");

var htmlLines = new Regex("<(.*?)>").Split(cleanHtml).Where(s => s.Length > 0);

Console.ReadLine();

HtmlElement root = null;
HtmlElement current = null;

List<string> lines = htmlLines.ToList();

foreach (var line in lines)
{
    var firstWord = line.Split(' ')[0];
    if (firstWord == "html/")
        break;
    else if (firstWord.StartsWith("/"))
    {
        if (current.Parent != null)
            current = current?.Parent;

    }
    else if (HtmlHelper.Instance.TagsInHtml.Contains(firstWord))
    {
        var newElement = new HtmlElement(firstWord);
        var attributeMatch = Regex.Match(line, "([a-zA-Z]+)=\"([^\"]+)\"");
        while (attributeMatch.Success)
        {
            var attributeName = attributeMatch.Groups[1].Value;
            var attributeValue = attributeMatch.Groups[2].Value;
            newElement.AddAttribute(attributeName, attributeValue);
            if (attributeName.ToLower() == "class")
            {
                newElement.Classes.AddRange(attributeValue.Split(' '));
            }

            attributeMatch = attributeMatch.NextMatch();
        }

        newElement.Name = firstWord;
        newElement.Id = newElement.Attributes.Find(attr => attr.Name.ToLower() == "id")?.Value;

        if (current != null)
        {
            current.AddChild(newElement);
            newElement.Parent = current;
        }
        else
        {
            root = newElement;
        }

        current = newElement;

        if (line.EndsWith("/") || HtmlHelper.Instance.DoNotRequireClosingTags.Contains(firstWord))
        {
            if (current.Parent != null)
                current = current.Parent;
        }

    }
    else
    {
        if (current != null)
        {
            current.InnerHtml += line;
        }
    }




}


async Task<string> Load(string url)
{
    HttpClient client = new HttpClient();
    var response = await client.GetAsync(url);
    var html = await response.Content.ReadAsStringAsync();
    return html;
}

static void PrintTree(HtmlElement element)
{
    // Print the current element
    Console.WriteLine(element.ToString());

    // Recursively print the children
    foreach (var child in element.Children)
    {
        PrintTree(child);
    }
}

static void printSelector(Selector selector)
{
    if (selector == null)
        return;
    Console.WriteLine(selector.ToString());
    printSelector(selector.Child);
}


PrintTree(root);
Selector rootElment = Selector.ParseSelectorString("div div a.home-hero-button1 button");
printSelector(rootElment);
HashSet<HtmlElement> result = new HashSet<HtmlElement>();
result = root.FindElementsBySelector(rootElment);
await Console.Out.WriteLineAsync("------res--------");
Console.WriteLine(result.Count());
result.ToList().ForEach(r => Console.WriteLine(r.ToString()));


