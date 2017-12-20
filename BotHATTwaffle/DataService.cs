﻿using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using System.IO;
using BotHATTwaffle.Modules.Json;
using HtmlAgilityPack;
using System.Threading.Tasks;
using CoreRCON;
using System.Net;

namespace BotHATTwaffle
{
    public class DataServices
    {
        JObject searchData;
        JObject serverData;
        JsonRoot root;
        List<JsonSeries> series;
        List<JsonServer> servers;
        Random _random;

        public DataServices(Random random)
        {
            Start();
            _random = random;
        }

        private void Start()
        {
            string searchDataPath = "searchData.json";
            searchData = JObject.Parse(File.ReadAllText(searchDataPath));
            root = searchData.ToObject<JsonRoot>();
            series = root.series;

            string serverDataPath = "servers.json";
            serverData = JObject.Parse(File.ReadAllText(serverDataPath));
            root = serverData.ToObject<JsonRoot>();
            servers = root.servers;

            
        }

        public JsonServer GetServer(string serverStr)
        {
            return servers.Find(x => x.Name == serverStr);
        }

        async public Task<string> RconCommand(string command, JsonServer server)
        {
            string reply = null;
            string botIP = new WebClient().DownloadString("http://icanhazip.com").Trim();
            IPHostEntry iPHostEntry = null;
            try
            {
                iPHostEntry = Dns.GetHostEntry(server.Address);
            }
            catch
            {
                return "HOST_NOT_FOUND";
            }

            //The object is in a using block because if it is not it will constantly spam the console of the sevrer with
            //Messages to test if it is still connected. When loggin is enabled, this floods logs ands console.
            //If multiple commmands were sent in a row with the default retry value of 30000 this would crash the bot. 
            //For some reason using a delay of 1, and then ensuring that we dispose of the object fixes this crash.
            using (var rcon = new RCON(IPAddress.Parse($"{iPHostEntry.AddressList[0]}"), 27015, server.Password,1))
            { reply = await rcon.SendCommandAsync(command); }

            reply = reply.Replace($"{botIP}", "69.420.MLG.1337"); //Remove the Bot's public IP from the string.

            Console.WriteLine(reply);
            if (reply.Length > 300)
                return reply.Substring(0,300) + "\n[OUTPUT OMITTED...]";

            return reply;
        }

        public List<List<string>> Search(string searchSeries, string searchTerm, bool isPrivate)
        {
            List<JsonTutorial> foundTutorials = new List<JsonTutorial>();
            List<List<string>> listResults = new List<List<string>>();
            string[] searchTermArray = searchTerm.Split(' ');

            if(searchSeries.ToLower() == "faq" || searchSeries.ToLower() == "f" || searchSeries.ToLower() == "7")
                return SearchFAQ(searchTerm, isPrivate);

            //V2 0
            if (searchSeries.ToLower() == "v2series" || searchSeries.ToLower() == "v2" || searchSeries.ToLower() == "1" || searchSeries.ToLower() == "all")
            {
                foreach(string s in searchTermArray)
                {
                    foundTutorials.AddRange(series[0].tutorial.FindAll(x => x.tags.Contains(s)));
                }
            }
            //Bootcamp 1
            if (searchSeries.ToLower() == "csgobootcamp" || searchSeries.ToLower() == "bc" || searchSeries.ToLower() == "2" || searchSeries.ToLower() == "all")
            {
                foreach (string s in searchTermArray)
                {
                    foundTutorials.AddRange(series[1].tutorial.FindAll(x => x.tags.Contains(s)));
                }
            }
            //3dsmax 2
            if (searchSeries.ToLower() == "3dsmax" || searchSeries.ToLower() == "3ds" || searchSeries.ToLower() == "3" || searchSeries.ToLower() == "all")
            {
                foreach (string s in searchTermArray)
                {
                    foundTutorials.AddRange(series[2].tutorial.FindAll(x => x.tags.Contains(s)));
                }
            }
            //Writtentutorials 3
            if (searchSeries.ToLower() == "writtentutorials" || searchSeries.ToLower() == "written" || searchSeries.ToLower() == "4" || searchSeries.ToLower() == "all")
            {
                foreach (string s in searchTermArray)
                {
                    foundTutorials.AddRange(series[3].tutorial.FindAll(x => x.tags.Contains(s)));
                }
            }
            //hammer troubleshooting 4
            if (searchSeries.ToLower() == "legacyseries" || searchSeries.ToLower() == "v1" || searchSeries.ToLower() == "lg" || searchSeries.ToLower() == "5")
            {
                foreach (string s in searchTermArray)
                {
                    foundTutorials.AddRange(series[4].tutorial.FindAll(x => x.tags.Contains(s)));
                }
            }
            //legacy 5
            if (searchSeries.ToLower() == "hammertroubleshooting" || searchSeries.ToLower() == "ht" || searchSeries.ToLower() == "6" || searchSeries.ToLower() == "all")
            {
                foreach (string s in searchTermArray)
                {
                    foundTutorials.AddRange(series[5].tutorial.FindAll(x => x.tags.Contains(s)));
                }
            }

            //Remove douplicates from list.
            List<JsonTutorial> noDoups = foundTutorials.Distinct().ToList();

            foreach (var result in noDoups)
            {
                List<string> singleResult = new List<string>();

                //Limit to 3 FAQ resusults. Let's add another one with a direct link to the page. Only limit for non-DM
                if (listResults.Count >= 2 && searchSeries == "all" && !isPrivate)
                {
                    singleResult.Clear();
                    singleResult.Add(@"View All Tutorials");
                    singleResult.Add("https://www.tophattwaffle.com/tutorials/");
                    singleResult.Add(@"There are more results than I can display without flooding chat. Consider viewing all tutorials, or do a search without `all`. If you DM me your search the results won't be limited.");
                    singleResult.Add(null);
                    listResults.Add(singleResult);
                    break;
                }
                
                HtmlWeb htmlWeb = new HtmlWeb();
                HtmlDocument htmlDocument = htmlWeb.Load(result.url);

                string title = (from x in htmlDocument.DocumentNode.Descendants()
                                where x.Name.ToLower() == "title"
                                select x.InnerText).FirstOrDefault();

                string description = null;
                //Get atricle content, this is by ID. Only works for my site.
                if (result.url.ToLower().Contains("tophattwaffle"))
                {
                    description = htmlDocument.GetElementbyId("content-area").InnerText;
                }
                //Fix the bad characters that get pulled from the web page.
                description = description.Replace(@"&#8211;", "-").Replace("\n", "").Replace(@"&#8220;", "\"").Replace(@"&#8221;", "\"").Replace(@"&#8217;", "'");
                title = title.Replace(@"&#8211;", "-").Replace("\n", "").Replace(" | TopHATTwaffle", "").Replace(@"&#8220;", "\"").Replace(@"&#8221;", "\"").Replace(@"&#8217;", "'");

                //Limit length if needed
                if (description.Length >= 250)
                {
                    description = description.Substring(0, 250) + "...";
                }

                //Get images on the page
                List<string> imgs = (from x in htmlDocument.DocumentNode.Descendants()
                                     where x.Name.ToLower() == "img"
                                     select x.Attributes["src"].Value).ToList<String>();
                //Set image to the first non-header image if it exists.
                string finalImg = "https://www.tophattwaffle.com/wp-content/uploads/2017/11/1024_png-300x300.png";
                if (imgs.Count > 1)
                    finalImg = imgs[_random.Next(0, imgs.Count)];

                singleResult.Add(title);
                singleResult.Add(result.url);
                singleResult.Add(description);
                singleResult.Add(finalImg);
                listResults.Add(singleResult);
            }
            return listResults;
        }

        public List<List<string>> SearchFAQ(string searchTerm, bool isPrivate)
        {
            List<List<string>> listResults = new List<List<string>>();
            
            string faqurl = "https://www.tophattwaffle.com/wp-admin/admin-ajax.php?action=epkb-search-kb&epkb_kb_id=1&search_words=";
            try
            {
                HtmlWeb faqWeb = new HtmlWeb();
                HtmlDocument faqDocument = faqWeb.Load($"{faqurl}{searchTerm}");
                foreach (HtmlNode link in faqDocument.DocumentNode.SelectNodes("//a[@href]"))
                {
                    List<string> singleResult = new List<string>();

                    //Limit to 3 FAQ resusults. Let's add another one with a direct link to the page.  Only limit for non-DM.
                    if (listResults.Count >= 2 && !isPrivate)
                    {
                        singleResult.Clear();
                        singleResult.Add(@"I cannot display any more results!");
                        singleResult.Add("http://tophattwaffle.com/faq");
                        singleResult.Add(@"I found more results than I can display here. Consider going directly to the FAQ main page and searching from there. If you DM me your search results won't be limited.");
                        singleResult.Add(null);
                        listResults.Add(singleResult);
                        break;
                    }

                    //Setup the web request for this specific link found. Format it so we can get data about it.
                    string finalUrl = link.GetAttributeValue("href", string.Empty).Replace(@"\", "").Replace("\"", "");
                    HtmlWeb htmlWeb = new HtmlWeb();
                    HtmlDocument htmlDocument = htmlWeb.Load(finalUrl);

                    //Get page title.
                    string title = (from x in htmlDocument.DocumentNode.Descendants()
                                    where x.Name.ToLower() == "title"
                                    select x.InnerText).FirstOrDefault();

                    //Get atricle content, this is by ID. Only works for my site.
                    string description = null;
                    //Get atricle content, this is by ID. Only works for my site.
                    if (finalUrl.ToLower().Contains("tophattwaffle"))
                    {
                        description = htmlDocument.GetElementbyId("kb-article-content").InnerText;
                    }

                    description = description.Replace(@"&#8211;", "-").Replace("\n", "").Replace(@"&#8220;", "\"").Replace(@"&#8221;", "\"").Replace(@"&#8217;", "'");
                    title = title.Replace(@"&#8211;", "-").Replace("\n", "").Replace(" | TopHATTwaffle", "").Replace(@"&#8220;", "\"").Replace(@"&#8221;", "\"").Replace(@"&#8217;", "'");

                    //Limit length if needed
                    if (description.Length >= 180)
                    {
                        description = description.Substring(0, 180) + "...";
                    }

                    //Get images on the page
                    List<string> imgs = (from x in htmlDocument.DocumentNode.Descendants()
                                         where x.Name.ToLower() == "img"
                                         select x.Attributes["src"].Value).ToList<String>();

                    //Set image to the first non-header image if it exists.
                    string finalImg = "https://www.tophattwaffle.com/wp-content/uploads/2017/11/1024_png-300x300.png";
                    if (imgs.Count > 1)
                        finalImg = imgs[_random.Next(0, imgs.Count)];

                    //Add results to list.
                    singleResult.Add(title);
                    singleResult.Add(finalUrl);
                    singleResult.Add(description);
                    singleResult.Add(finalImg);
                    listResults.Add(singleResult);
                }
            }
            catch(Exception)
            {
                //Do nothing. The command that called this will handle the no results found message.
            }
            return listResults;
        }

    }
}
