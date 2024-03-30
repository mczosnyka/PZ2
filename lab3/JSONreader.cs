using System.Text.Json;
public class JSONreader{
    public String jsonString {get; set;}
    public Tweets ?tweets {get;}

    public JSONreader(){
        jsonString = System.IO.File.ReadAllText("C:\\Projekty\\C#Projects\\lab3\\data.json"); 
        tweets = JsonSerializer.Deserialize<Tweets>(jsonString);

    }

}