using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductAccounting.Controllers
{
    public class ChangesController
    {
        private protected string path = "Changes.txt";
        public async Task<string> LoadChanges() 
        {
            string text = "";
            using (StreamReader stream = new StreamReader(path))
            {
                 text = await stream.ReadToEndAsync();
                return text;
            }
        }
    }
}
