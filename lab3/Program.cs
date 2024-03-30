using System.Text.RegularExpressions;

public class Program{  

    static void Main(string[] args){
        JSONreader readr = new JSONreader();
        Tweets ?readtweets = readr.tweets;
        XMLParser parser = new XMLParser();
        parser.saveXML(readtweets);
        Tweets t = parser.readXML();
        t.sortByDate();
        t.sortByUserName();
        t.printOldestAndNewest();
        Dictionary<string, Tweet> userTweets = new Dictionary<string, Tweet>();
        foreach(Tweet tw in t.data){
            userTweets[tw.UserName] = tw;
        }
        Dictionary<string, int> occurances = new Dictionary<string, int>();
        foreach(Tweet tw in t.data){
            Regex rgx = new Regex("[^a-zA-Z0-9 -]");
            string trimmer = rgx.Replace(tw.Text, "");
            string[] output = trimmer.Split(' ');
            foreach(string s in output){
                if (occurances.ContainsKey(s))
                {
                    occurances[s]++;
                }
                else
                {
                    occurances[s] = 1;
                }
            }

        }

        List<int> valuesList = occurances.Values.ToList();

        valuesList.Sort((a, b) => b.CompareTo(a));
        int count = 0;
        foreach(int i in valuesList){
            string key = occurances.FirstOrDefault(x => x.Value == i).Key;
            if(key.Length >= 5){
                Console.WriteLine(key);
                count += 1;
            }
            if(count == 10){
                break;
            }
        }
        int sum = occurances.Skip(1).Sum(x => x.Value);
            // idfs[k] = Math.Log(Math.E, Math.E);
        Dictionary<string, double> IDFS = new Dictionary<string, double>();
        foreach(Tweet tw in t.data){
            foreach(String k in occurances.Keys){
                if(tw.Text != null && tw.Text.Contains(k)){
                    if (IDFS.ContainsKey(k))
                    {
                    IDFS[k]++;
                    }
                else
                    {
                    IDFS[k] = 1;
                    }
                }
            }
        }
        foreach(String s in IDFS.Keys){
            IDFS[s] = Math.Log((t.data.Count/IDFS[s]), Math.E);
        }




    }





}
