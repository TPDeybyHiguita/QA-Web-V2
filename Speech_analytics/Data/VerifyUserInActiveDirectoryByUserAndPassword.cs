using Speech_analytics.Models;
using System;
using System.DirectoryServices;
using System.Reflection.PortableExecutable;

namespace Speech_analytics.Data
{
    public class AzureADAuthenticator
    {
        private readonly DirectoryActive directoryActive;

        public AzureADAuthenticator(DirectoryActive directoryActive)
        {
            this.directoryActive = directoryActive;
        }

        public bool Autenticar()
        {
            try
            {
                System.DirectoryServices.DirectoryEntry directorio = new System.DirectoryServices.DirectoryEntry("LDAP://teleperformance.co", directoryActive.user, directoryActive.password, System.DirectoryServices.AuthenticationTypes.Secure);
                DirectorySearcher buscador = new DirectorySearcher(directorio);
                object obj = directorio.NativeObject;
                return true;
            }
            catch (DirectoryServicesCOMException ex)
            {
                // Autenticación fallida
                return false;
            }
        }

    }

}