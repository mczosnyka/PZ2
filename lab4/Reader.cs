public class Reader<T>{
    public List<T> readList(String path, Func<String[], T> generuj)
    {
         List<T> dataList = new List<T>();
        try
        {
            using (StreamReader sr = new StreamReader(path))
            {
                string ?headerLine = sr.ReadLine();
                string ?line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] fields = line.Split(',');
                    dataList.Add(generuj(fields));
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"error: {e.Message}");
        }
         return dataList;  
    }
}