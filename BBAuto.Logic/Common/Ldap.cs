using System;
using System.DirectoryServices;

namespace BBAuto.Logic.Common
{
  public class Ldap : IDisposable
  {
    private const string ServerIpAddress = "LDAP://bbmag.bbraun.com";

    private readonly DirectoryEntry _rootDE;
    private readonly DirectorySearcher _searcher;

    public Ldap()
    {
      _rootDE = new DirectoryEntry(ServerIpAddress);
      _searcher = new DirectorySearcher(_rootDE);
    }

    public string GetEmail(string login)
    {
      if (login == string.Empty)
        return string.Empty;

      _searcher.Filter = $"(&(objectClass=user)(samAccountName={login}))"; //strLogonName);
      //var queryFormat = "(&(objectClass=user)(objectCategory=person)(|(SAMAccountName=*{0}*)(cn=*{0}*)(gn=*{0}*)(sn=*{0}*)(email=*{0}*)))";

      _searcher.SearchScope = SearchScope.Subtree;

      SearchResult result = _searcher.FindOne();

      return result != null && result.Properties["mail"].Count > 0
        ? result.Properties["mail"][0].ToString()
        : string.Empty;
    }

    public void Dispose()
    {
      _rootDE.Close();
    }
  }
}
