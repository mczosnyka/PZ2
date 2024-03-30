using System.Dynamic;
using System.Runtime.CompilerServices;

public class Region{
    public String ?regionid{get; set;}
    public String ?regiondescription{get; set;}
    public Region(String rid, String rd){
        regionid = rid;
        regiondescription = rd;
    }
}

public class Territory{
    public String ?territoryid{get; set;}
    public String ?territorydescription{get; set;}
    public String ?regionid{get; set;}
    public Territory(String tid, String td, String rid){
        territoryid = tid;
        territorydescription = td;
        regionid = rid;
    }

}

public class Employee{
    public String ?employeeid{get; set;}
    public String ?lastname{get; set;}
    public String ?firstname{get; set;}
    public String ?title{get; set;}
    public String ?titleofcourtesy{get; set;}
    public String ?birthdate{get; set;}
    public String ?hiredate{get; set;}
    public String ?address{get; set;}
    public String ?city{get; set;}
    public String ?region{get; set;}
    public String ?postalcode{get; set;}
    public String ?country{get; set;}
    public String ?homephone{get; set;}
    public String ?extension{get; set;}
    public String ?photo{get; set;}
    public String ?notes{get; set;}
    public String ?reportsto{get; set;}
    public String ?photopath{get; set;}
    public Employee(String eid, String ln, String fn, String tit, String toc, String bd, String hd, String ad, String cty, String r, String pc, String cry, String hp, String ex, String p, String n, String rep, String pp){
        employeeid = eid;
        lastname = ln;
        firstname = fn;
        title = tit;
        titleofcourtesy = toc;
        birthdate = bd;
        hiredate = hd;
        address = ad;
        city = cty;
        region = r;
        postalcode = pc;
        country = cry;
        homephone = hp;
        extension = ex;
        photo = p;
        notes = n;
        reportsto = rep;
        photopath = pp;
    }
}

public class EmployeeTerritory{
    public String ?employeeid{get; set;}
    public String ?territoryid{get; set;}
    public EmployeeTerritory(String eid, String tid){
        employeeid = eid;
        territoryid = tid;

    }
}

public class Order
{
    public String orderid { get; set; }
    public String customerid { get; set; }
    public String employeeid { get; set; }
    public String orderdate { get; set; }
    public String requireddate { get; set; }
    public String shippeddate { get; set; }
    public String shipvia { get; set; }
    public String freight { get; set; }
    public String shipname { get; set; }
    public String shipaddress { get; set; }
    public String shipcity { get; set; }
    public String shipregion { get; set; }
    public String shippostalcode { get; set; }
    public String shipcountry { get; set; }

    public Order(String oid, String cid, String eid, String odate, String rdate, String sdate, String svia, String fr, String sname, String saddr, String scity, String sregion, String spcode, String scountry)
    {
        orderid = oid;
        customerid = cid;
        employeeid = eid;
        orderdate = odate;
        requireddate = rdate;
        shippeddate = sdate;
        shipvia = svia;
        freight = fr;
        shipname = sname;
        shipaddress = saddr;
        shipcity = scity;
        shipregion = sregion;
        shippostalcode = spcode;
        shipcountry = scountry;
    }
}
    public class OrderDetails
{
    public String orderid { get; set; }
    public String productid { get; set; }
    public String unitprice { get; set; }
    public String quantity { get; set; }
    public String discount {get; set; }

    public OrderDetails(String oid, String pid, String up, String qty, String disc)
    {
        orderid = oid;
        productid = pid;
        unitprice = up;
        quantity = qty;
        discount = disc;
    }
}
