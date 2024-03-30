using Microsoft.VisualBasic;

class Program{
    static void Main(string[] args)
    {
        Reader<Region> rg = new Reader<Region>();
        List<Region> Regiony = rg.readList("C:\\Projekty\\C#Projects\\lab4\\data\\regions.csv",
            x => new Region(x[0], x[1]));
        Reader<Territory> tt = new Reader<Territory>();
        List<Territory> Terytoria = tt.readList("C:\\Projekty\\C#Projects\\lab4\\data\\territories.csv",
            x => new Territory(x[0], x[1], x[2]));
        Reader<EmployeeTerritory> et = new Reader<EmployeeTerritory>();
        List<EmployeeTerritory> TerytoriaPracownikow = et.readList("C:\\Projekty\\C#Projects\\lab4\\data\\employee_territories.csv",
            x => new EmployeeTerritory(x[0], x[1]));
        Reader<Employee> ee = new Reader<Employee>();
        List<Employee> Pracownicy = ee.readList("C:\\Projekty\\C#Projects\\lab4\\data\\employees.csv",
            x => new Employee(x[0], x[1], x[2], x[3], x[4], x[5], x[6], x[7], x[8], x[9], x[10], x[11], x[12], x[13], x[14], x[15], x[16], x[17]));
    
        var results = from p in Pracownicy select new{ p.lastname};
        foreach(var n in results){
            Console.WriteLine(n.lastname);
        }
        var results2 = from p in Pracownicy join t in TerytoriaPracownikow on p.employeeid equals t.employeeid join ter in Terytoria on t.territoryid equals ter.territoryid join r in Regiony on ter.regionid equals r.regionid select new{lastname = p.lastname, region = r.regiondescription, territory = ter.territorydescription};
        foreach(var n in results2){
            Console.WriteLine("nazwisko " + n.lastname + " region " + n.region + " terytorium " + n.territory);
        }
        
        var results3 = from p2 in (from r in Regiony
        join t in Terytoria on r.regionid equals t.regionid join terr in Terytoria on r.regionid equals terr.regionid
            join terrp in TerytoriaPracownikow on terr.territoryid equals terrp.territoryid join p in Pracownicy on terrp.employeeid equals p.employeeid
        select new {pracownik = p, region = r})
        group p2 by p2.region.regiondescription into zgrupowane
        select zgrupowane;
        foreach (var r in results3)
        {
            Console.WriteLine("{0}:", r.Key);
            foreach (var pr in r)
                {
                    Console.WriteLine("  {0}", pr.pracownik.lastname);
                }
            }     
        foreach (var r in results3)
        {
            Console.WriteLine("Count: {0}", r.Distinct().Count());
            }       


    Reader<OrderDetails> ord = new Reader<OrderDetails>();
    List<OrderDetails> SzczegolyZamowien = ord.readList("C:\\Projekty\\C#Projects\\lab4\\data\\orders_details.csv",
            x => new OrderDetails(x[0], x[1], x[2], x[3], x[4]));
    Reader<Order> orr = new Reader<Order>();
    List<Order> Zamowienia = orr.readList("C:\\Projekty\\C#Projects\\lab4\\data\\orders.csv",
            x => new Order(x[0], x[1], x[2], x[3], x[4], x[5], x[6], x[7], x[8], x[9], x[10], x[11], x[12], x[13]));
    
    var results4 = from p2 in (from p in Pracownicy
    join z in Zamowienia on p.employeeid equals z.employeeid join sz in SzczegolyZamowien on z.orderid equals sz.orderid
    select new {nazwisko = p.lastname, zamowienie = z.orderid, ilosc = sz.quantity, cena = sz.unitprice, rabat = sz.discount}) 
    group p2 by p2.nazwisko into zgrupowane
    select new {nazwa = zgrupowane.Key, 
        count = zgrupowane.Count(),
        avg = zgrupowane.Average(zgr => double.Parse(zgr.ilosc, System.Globalization.CultureInfo.InvariantCulture) * double.Parse(zgr.cena, System.Globalization.CultureInfo.InvariantCulture) - double.Parse(zgr.rabat, System.Globalization.CultureInfo.InvariantCulture)),
        max = zgrupowane.Max(zgr => double.Parse(zgr.ilosc, System.Globalization.CultureInfo.InvariantCulture) * double.Parse(zgr.cena, System.Globalization.CultureInfo.InvariantCulture) - double.Parse(zgr.rabat, System.Globalization.CultureInfo.InvariantCulture))};
    foreach (var r in results4)
        {
            Console.WriteLine(r.nazwa);
            Console.WriteLine(r.count); 
            Console.WriteLine(r.avg);
            Console.WriteLine(r.max);

}    

    }
}
    