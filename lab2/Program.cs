using System.Numerics;

public abstract class PosiadaczRachunku{
public abstract override string ToString();

}

public class OsobaFizyczna : PosiadaczRachunku{
    private string imie;
    public string Imie
    {
        get { return imie; }
        set { imie = value; }
    }
    private string drugieImie;
    
    public string DrugieImie
    {
        get { return drugieImie; }
        set { drugieImie = value; }
    }
    private string nazwisko;
    
    public string Nazwisko
    {
        get { return nazwisko; }
        set { nazwisko = value; }
    }
    private string? pesel;
    
    public string? Pesel
    {
        get { return pesel; }
        set { 
        if(!string.IsNullOrEmpty(value) && value.Length != 11 ){
            throw new Exception("PESEL za krotki");
        }
        else{
            pesel = value;
        }
        }
    }
    private string? numerPaszportu;
    
    public string? NumerPaszportu
    {
        get { return numerPaszportu; }
        set { numerPaszportu = value; }
    }
    
   public OsobaFizyczna(string i, string d, string n, string p, string np){
        if (string.IsNullOrEmpty(np) && string.IsNullOrEmpty(p))
        {
            throw new Exception("PESEL albo numer paszportu musza byc nie null");
        }
        if(!string.IsNullOrEmpty(p) && p.Length != 11 ){
            throw new Exception("PESEL za krotki");
        }
        imie = i;
        drugieImie = d;
        nazwisko = n;
        pesel = p;
        numerPaszportu = p;
   }
   
    public override string ToString(){
        string returner = "Osoba fizyczna: ";
        returner = returner + imie + " " + nazwisko;
        return returner;
    }
}

public class OsobaPrawna : PosiadaczRachunku{
    private string nazwa;
    public string Nazwa
    {
        get { return nazwa; }
    }
    private string siedziba;
    
    public string Siedziba
    {
        get { return siedziba; }
    }
    
    
   public OsobaPrawna(string n, string s){
        if (string.IsNullOrEmpty(n))
        {
            throw new Exception("Osoba Prawna musi miec nazwe!");
        }
        nazwa = n;
        siedziba = s;
   }
   
    public override string ToString(){
        string returner = "Osoba prawna: ";
        returner = returner + nazwa + ", " + siedziba;
        return returner;
    }
}

public class Transakcja{
    private RachunekBankowy? rachunekZrodlowy;
    public RachunekBankowy? RachunekZrodlowy
    {
        get { return rachunekZrodlowy; }
        set { rachunekZrodlowy = value; }
    }
    private RachunekBankowy? rachunekDocelowy;
    public RachunekBankowy? RachunekDocelowy
    {
        get { return rachunekDocelowy; }
        set { rachunekDocelowy = value; }
    }
    private decimal kwota;
    public decimal Kwota{
        get { return kwota ;}
        set { kwota = value; }
    }
    private string opis;
    public string Opis{
        get { return opis ;}
        set { opis = value; }
    }

    public Transakcja(RachunekBankowy rz, RachunekBankowy rd, decimal k, string o){
        if(rz == null && rd == null){
            throw new Exception("Rachunek zrodlowy i docelowy nie moze byc nullem na raz!");
        }
        rachunekZrodlowy = rz;
        rachunekDocelowy = rd;
        kwota = k;
        opis = o;
    }

    public override string ToString()
    {
        string returner = "Z rachunku: " + rachunekZrodlowy.Numer + " na rachunek: " + rachunekDocelowy.Numer + " kwota: " + kwota + " opis: " + opis;
        return returner;
    }


}









public class RachunekBankowy{
    private string numer;
    public string Numer
    {
        get { return numer; }
        set { numer = value; }
    }
    private decimal stanRachunku;
    public decimal StanRachunku
    {
        get { return stanRachunku; }
        set { stanRachunku = value; }
    }
    private bool czyDozwolonyDebet;
    public bool CzyDozwolonyDebet
    {
        get { return czyDozwolonyDebet; }
        set { czyDozwolonyDebet = value; }
    }
    private List<PosiadaczRachunku> _PosiadaczeRachunku = new List<PosiadaczRachunku>();
    public List<PosiadaczRachunku> PosiadaczeRachunku
    {
        get { return _PosiadaczeRachunku; }
        set { _PosiadaczeRachunku = value; }
    }

    private List<Transakcja> _Transakcje = new List<Transakcja>();

    public RachunekBankowy(string n, decimal s, bool c, List<PosiadaczRachunku> l){
        if(l==null || l.Count == 0){
           throw new Exception("Lista posiadaczy rachunku musi miec co najmniej jedna wartosc"); 
        }
        numer = n;
        stanRachunku = s;
        czyDozwolonyDebet = c;
        _PosiadaczeRachunku = l;
    }

    public static RachunekBankowy operator-(RachunekBankowy rachunek, PosiadaczRachunku posiadacz){
        if(rachunek._PosiadaczeRachunku.Count == 1 || !rachunek._PosiadaczeRachunku.Contains(posiadacz)){
            throw new Exception("Nie mozna usunac posiadacza rachunku");
        }
        rachunek._PosiadaczeRachunku.Remove(posiadacz);
        return rachunek;
    }

    public static RachunekBankowy operator+(RachunekBankowy rachunek, PosiadaczRachunku posiadacz){
        if(rachunek._PosiadaczeRachunku.Contains(posiadacz)){
            throw new Exception("Nie mozna dodac posiadacza rachunku");
        }
        rachunek._PosiadaczeRachunku.Add(posiadacz);
        return rachunek;
    }

    public override string ToString()
    {
        string returner = "Rachunek numer: " + numer + " Stan rachunku: " + stanRachunku + " Posiadacze: ";
        foreach (var item in PosiadaczeRachunku){
            returner += item.ToString();
            returner += " ";
        }
        returner += "Transakcje: ";
        foreach (var item in _Transakcje){
            returner += item.ToString();
            returner += " ";
        }
        return returner;
    }


    public static void DokonajTransakcji(RachunekBankowy zrodlo, RachunekBankowy cel, decimal ilosc, string desc){
        if(ilosc<0 || (zrodlo == null && cel == null) || (zrodlo != null && zrodlo.czyDozwolonyDebet == false && ilosc>zrodlo.StanRachunku)){
            throw new Exception("sprawdz dane transakcji");
        }
        if(zrodlo == null){
            cel.stanRachunku += ilosc;
            Transakcja transakcja = new Transakcja(zrodlo, cel, ilosc, desc);
            cel._Transakcje.Add(transakcja);
        }
        else if(cel == null){
            zrodlo.stanRachunku -= ilosc;
            Transakcja transakcja = new Transakcja(zrodlo, cel, ilosc, desc);
            zrodlo._Transakcje.Add(transakcja);
        }
        else{
            zrodlo.stanRachunku -= ilosc;
            cel.stanRachunku += ilosc;
            Transakcja transakcja = new Transakcja(zrodlo, cel, ilosc, desc);
            zrodlo._Transakcje.Add(transakcja);
            cel._Transakcje.Add(transakcja);
        }
    }
}















    class Program
{
    static void Main()
    {
        // Przykład użycia
        OsobaFizyczna klient = new OsobaFizyczna("Jan","Antoni", "Kowalski", "12345678910", null);
        OsobaFizyczna klient2 = new OsobaFizyczna("Anna","Maria", "Nowak", "01987654321", null);
        List<PosiadaczRachunku> l = new List<PosiadaczRachunku>();
        List<PosiadaczRachunku> l2 = new List<PosiadaczRachunku>();
        l.Add(klient);
        l2.Add(klient2);
        RachunekBankowy r = new RachunekBankowy("123", 1000, false, l);
        RachunekBankowy r2 = new RachunekBankowy("456", 100000, false, l2);
        Transakcja t = new Transakcja(r, r2, 1000, "witam");
        RachunekBankowy.DokonajTransakcji(r, r2, 1000, "wszystkie pieniadze");
        Console.WriteLine(r.ToString());
        r = r+klient2;
        Console.WriteLine(r.ToString());
        r = r-klient;
        Console.WriteLine(r.ToString());
    }   
}