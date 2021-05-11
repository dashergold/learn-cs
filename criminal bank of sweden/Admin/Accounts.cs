using System;
using System.Collections.Generic;
using System.IO;

namespace Admin
{


public class Accounts
{
    public static Dictionary<string, Account> loadAccounts(string filename)
    {
        var file = File.OpenText(filename);
        var line = file.ReadLine();
        var accs=new Dictionary<string, Account>();
        while(line != null)
        {
            var a=line.Split(",");
            var acc =  new Account();
            acc.Username=a[0];
            acc.Password=a[1];
            acc.Name=a[2];
            acc.Balance=Convert.ToDecimal(a[3]); 
            accs.Add(acc.Username, acc);
            line = file.ReadLine();
            
        }
        file.Close();
        return accs;
        
    }

}
public class Account
{
    public string Username;
    public string Password;
    public string Name;
    public decimal Balance;

}

}