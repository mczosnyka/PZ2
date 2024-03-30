public class XMLParser{
    public System.Xml.Serialization.XmlSerializer ?x;
    public void saveXML(Tweets twt){
        x = new System.Xml.Serialization.XmlSerializer(twt.GetType());
        using (StreamWriter writer = File.CreateText("C:\\Projekty\\C#Projects\\lab3\\tweets.xml"))
        {
            x.Serialize(writer, twt);
        }

    }

    public Tweets readXML(){
        using (StreamReader reader = new StreamReader("C:\\Projekty\\C#Projects\\lab3\\tweets.xml"))
        {
            Tweets twet = (Tweets)x.Deserialize(reader);
            return twet;
        }
    }

}