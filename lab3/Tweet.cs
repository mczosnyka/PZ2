using System.Globalization;
using System.Text;
public class Tweet{
    public string? Text { get; set; }
    public string? UserName { get; set; }
    public string? LinkToTweet { get; set; }
    public string? FirstLinkUrl { get; set; }
    public string? CreatedAt { get; set; }
    public string? TweetEmbedCode { get; set; }
    public override String ToString(){
        return Text + " " + UserName + " " + LinkToTweet + " " + FirstLinkUrl + " " + CreatedAt + " " + TweetEmbedCode;
    }
}

public class Tweets
{
    public List<Tweet> ?data { get; set; }

    public void sortByDate(){
        foreach(Tweet t in data){
            t.CreatedAt = t.CreatedAt.Replace(",", string.Empty).Replace(" at", string.Empty).Replace("AM", " AM").Replace("PM", " PM");
        }
        data = data.OrderBy(t => DateTime.ParseExact(t.CreatedAt, "MMMM dd yyyy hh:mm tt", CultureInfo.InvariantCulture)).ToList();
    }

    public void sortByUserName(){
        data = data.OrderBy(o => o.UserName).ToList();
    }

    public void printOldestAndNewest(){
        List <Tweet> copy = data.OrderBy(t => DateTime.ParseExact(t.CreatedAt, "MMMM dd yyyy hh:mm tt", CultureInfo.InvariantCulture)).ToList();
        Console.WriteLine(copy.ElementAt(0).ToString());
        Console.WriteLine(copy.ElementAt(copy.Count-1).ToString());
    }


} 