using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using World_Football.Common;

namespace World_Football.Helper
{
    public class DbHelper
    {
        public static void CopyReferenceDb()
        {
            IsolatedStorageFile iso = IsolatedStorageFile.GetUserStoreForApplication();
            using (Stream input = Application.GetResourceStream(new Uri(Commons.DbName, UriKind.Relative)).Stream)
            {
                using (IsolatedStorageFileStream output = iso.CreateFile(Commons.DbName))
                {
                    byte[] buffer = new byte[4096];
                    int bytesRead = -1;
                    while ((bytesRead = input.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        output.Write(buffer, 0, bytesRead);
                    }
                }
            }
        }
    }
}
