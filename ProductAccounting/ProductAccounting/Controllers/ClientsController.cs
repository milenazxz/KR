using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ProductAccounting.Forms;
using ProductAccounting.Interfaces;
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
        IDbFunctions dbFunctions = new DbFunctions();
        public async Task<List<Clients>> LoadData()
        {
            using (var context = new ApplicationDbContext()) 
            {
                List<Clients> loadData = await context.Set<Clients>().ToListAsync();
                return loadData;
            }
        }

        public async Task<ClientDTO> LoadDataClient(int ID) 
        {
            using (var context = new ApplicationDbContext()) 
            {
                Clients client = await context.Set<Clients>().FirstOrDefaultAsync(i => i.id == ID);
                ClientDTO clientDTO = new ClientDTO
                {
                    Name = client.name,
                    Organform = client.organform,
                    City = client.city,
                    Address = client.address,
                    Phonenumber = client.phonenumber,
                    Email = client.email,
                };
                return clientDTO;
            }
        }

        public async Task<bool> AddClient(ClientUpdateDTO currentDTO) 
        {
            Clients client = new Clients {name = currentDTO.Name, organform = currentDTO.Organform, city = currentDTO.City, address = currentDTO.Address, phonenumber = currentDTO.Phonenumber, email= currentDTO.Email };
            await dbFunctions.AddData<Clients>(client);
            return true;
        }
        public async Task<bool> ChangeClient(int IdClient, ClientUpdateDTO currentDTO, List<string> changedFields)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                Clients client = context.Set<Clients>().FirstOrDefault(w => w.id == IdClient);
                if (client == null) return false;
                if (changedFields.Contains(nameof(ClientUpdateDTO.Name)))
                    client.name = currentDTO.Name;

                if (changedFields.Contains(nameof(ClientUpdateDTO.Organform)))
                    client.organform = currentDTO.Organform;

                if (changedFields.Contains(nameof(ClientUpdateDTO.City)))
                    client.city = currentDTO.City;

                if (changedFields.Contains(nameof(ClientUpdateDTO.Address)))
                    client.address = currentDTO.Address;

                if (changedFields.Contains(nameof(ClientUpdateDTO.Phonenumber)))
                    client.phonenumber = currentDTO.Phonenumber;

                if (changedFields.Contains(nameof(ClientUpdateDTO.Email)))
                    client.email = currentDTO.Email;

                await dbFunctions.ChangeData<Clients>(client);
                return true;
            }
        }

        public async Task DeleteClient(IList selectedClients) 
        {
            foreach (var selectedClient in selectedClients) 
            {
                if (selectedClient is Clients client)
                {
                    await dbFunctions.DeleteItem<Clients>(client, c => c.id == client.id);
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
