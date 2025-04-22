using Microsoft.EntityFrameworkCore;
using ProductAccounting.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductAccounting.Controllers
{
    internal class ClientsController
    {
        public async Task<List<Clients>> LoadData()
        {
            using (var context = new ApplicationDbContext()) 
            {
                List<Clients> loadData = await context.Set<Clients>().ToListAsync();
                return loadData;
            }
        }

        public async Task<Clients> LoadDataClient(int ID) 
        {
            using (var context = new ApplicationDbContext()) 
            {
                var item = await context.Set<Clients>().FirstOrDefaultAsync(i => i.id == ID);
                return item;
            }
        }

        public async Task<bool> AddClient(string Name, string Organform, string City, string Address, string Phonenumber, string Email) 
        {
            Clients client = new Clients {name = Name, organform = Organform, city = City, address = Address, phonenumber = Phonenumber, email= Email};
            await DbFunctions.AddData<Clients>(client);
            return true;
        }

        public async Task DeleteClient(IList selectedClients) 
        {
            foreach (var selectedClient in selectedClients) 
            {
                if (selectedClient is Clients client)
                {
                    await DbFunctions.DeleteItem<Clients>(client, c => c.id == client.id);
                }
            }
            
        }

        public async Task<List<Clients>> RefreshAsync() 
        {
            using (var context = new ApplicationDbContext()) 
            {
                List<Clients> updatedData = await context.Set<Clients>().ToListAsync();
                return updatedData;
            }
        }
    }
}
