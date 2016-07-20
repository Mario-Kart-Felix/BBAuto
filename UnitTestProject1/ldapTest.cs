using System;
using BBAuto.Domain;
using NUnit.Framework;

namespace UnitTestProject1
{
    [TestFixture]
    public class ldapTest
    {
        [TestCase("shelmaru", Result = "Maria.Shelyakova@bbraun.com")]
        public string GetEMailByLogin(string login)
        {
            Ldap ldap = new Ldap();

            return ldap.GetEmail(login);
        }
    }
}
