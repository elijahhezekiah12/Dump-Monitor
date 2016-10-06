using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweetinvi;
using SHDocVw;
using mshtml;


//ktruant@free.fr


namespace Twitter_API_Crawler
{
    class Program
    {
        static void Main(string[] args)
        {
            int counter = 0;
            string found = "";
            // Set up your credentials (https://apps.twitter.com)
            Auth.SetUserCredentials("","","","");                //Throw in twitter keys here
            var tweets = Timeline.GetUserTimeline("dumpmon");
            SHDocVw.InternetExplorer IE = new SHDocVw.InternetExplorer();
            IE.Navigate("google.com");
            List<string> searchTerms = new List<String>();
            var tweetArray = tweets.ToArray();

            while (IE.ReadyState!= SHDocVw.tagREADYSTATE.READYSTATE_COMPLETE)
            {
                Console.Write("Setting up environment");
                System.Threading.Thread.Sleep(500);
                Console.Clear();
                Console.Write("Setting up environment.");
                System.Threading.Thread.Sleep(500);
                Console.Clear();
                Console.Write("Setting up environment..");
                System.Threading.Thread.Sleep(500);
                Console.Clear();
                Console.Write("Setting up environment...");
                System.Threading.Thread.Sleep(500);
                Console.Clear();
            }

            while (!searchTerms.Contains("done"))
            {
                Console.WriteLine("Enter a term to search for or type '<>' to start the search. Type '!!' to exit");
                string text = Console.ReadLine();
                if (text !="<>" && text != "!!") 
                {
                    searchTerms.Add(text);
                }
                else if (text =="!!")
                {
                    Environment.Exit(0);
                }
                else
                {
                    searchTerms.Add("done");
                }
            }

            searchTerms.Remove("done");

            for (int i = 0; i < tweets.Count(); i++)
            {
                string url = tweetArray[i].ToString();
                url =url.Remove(23,url.Length-23);
//Trim tweet to just URL
                IE.Navigate(url);

                while (IE.ReadyState != SHDocVw.tagREADYSTATE.READYSTATE_COMPLETE)
                {

                }

                //Gather HTML for checking
                mshtml.IHTMLDocument2 doc = IE.Document as mshtml.IHTMLDocument2;
                for (int j = 0; j < searchTerms.Count; j++)
                {
                    if (doc.body.innerText.Contains(searchTerms[j]))
                    {
                        Console.WriteLine(url + " DETAILS FOUND!");
                        Console.WriteLine("DETAILS FOUND!");
                        found = found +( url + "\n");
                        counter++;
                    }
                    else
                    {
                        Console.WriteLine(url + " Checked against check data #" +(j+1)+"------> 0 hits");
                    }
                }
            }
            Console.WriteLine("\n\n");
            Console.Write(counter + " hit/s ");
            Console.WriteLine(found);
            if (found != "")
            { string strPath = Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory);
                string strDate = DateTime.Today.Date.ToString("yyyy-MM-dd");
                    System.IO.File.WriteAllText(strPath+"\\TwitterCrawlerLog_"+strDate +".text", found);
                Console.Write("Log file written to desktop");
            }
            Console.Read();
        }
    }
}


