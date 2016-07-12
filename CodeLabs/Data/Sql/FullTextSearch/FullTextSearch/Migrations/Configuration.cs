using System.Net.Http;
using System.Text.RegularExpressions;
using FullTextSearch.Models;
using HtmlAgilityPack;

namespace FullTextSearch.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Db.WikiContext>
    {
        private static readonly string[] Pages = new[] {
            "https://nl.wikipedia.org/wiki/Basiliek_van_Onze-Lieve-Vrouw_van_Afrika",
            "https://nl.wikipedia.org/wiki/Iximch%C3%A9",
            "https://nl.wikipedia.org/wiki/Rijksmuseum_van_Oudheden",
            "https://nl.wikipedia.org/wiki/Gallo-Romeins_Museum_(Tongeren)",
            "https://nl.wikipedia.org/wiki/Vincent_Icke",
            "https://nl.wikipedia.org/wiki/SGR_1806-20",
            "https://nl.wikipedia.org/wiki/Don_McLean",
            "https://nl.wikipedia.org/wiki/Charles_de_Brouck%C3%A8re_(1796-1860)",
            "https://nl.wikipedia.org/wiki/Fytoplankton",
            "https://nl.wikipedia.org/wiki/Ampeliscidae",
            "https://nl.wikipedia.org/wiki/Ceawlin",
            "https://nl.wikipedia.org/wiki/Hendrick_Lonck",
            "https://nl.wikipedia.org/wiki/Cezoram",
            "https://nl.wikipedia.org/wiki/Herodes_de_Grote",
            "https://nl.wikipedia.org/wiki/Aluminium",
            "https://nl.wikipedia.org/wiki/CompactFlash",
            "https://nl.wikipedia.org/wiki/Geneeskunde",
            "https://nl.wikipedia.org/wiki/C-3PO"
        };

        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        public void Seed(Db.WikiContext context)
        {
            if (context.WikiReferences.Any())
                return;

            foreach (var page in Pages)
            {
                var client = new HttpClient();
                var result = client.GetAsync(new Uri(page)).Result
                    .Content.ReadAsStringAsync().Result;

                var document = new HtmlDocument();
                document.LoadHtml(result);
                var title = document.DocumentNode.SelectSingleNode("//head/title").InnerText;
                var text = document.GetElementbyId("mw-content-text").InnerText;

                context.WikiReferences.Add(new WikiReference
                {
                    Id = Guid.NewGuid(),
                    Title = Regex.Replace(title, @"\s+", " ").Trim(),
                    Url = page,
                    Content = 
                        Regex.Replace(text, @"\s+", " ")
                        .Replace("'", "").Replace(@"\", "").Trim()
                });
            }

            context.SaveChanges();

            base.Seed(context);
        }
    }
}
