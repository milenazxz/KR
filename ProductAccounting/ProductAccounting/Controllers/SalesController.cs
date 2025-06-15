using Microsoft.EntityFrameworkCore;
using ProductAccounting.Forms;
using ProductAccounting.Interfaces;
using ProductAccounting.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace ProductAccounting.Controllers
{
    public class SalesController
    {
        IDbFunctions dbFunctions = new DbFunctions();
        public async Task<List<Sales>> LoadData(Expression<Func<Sales, object>> WFunc, Expression<Func<Sales, object>> CFunc, Expression<Func<Sales, object>> EFunc)
        {
            using (var context = new ApplicationDbContext())
            {
                return await context.sales
                    .Include(WFunc)
                    .Include(CFunc)
                    .Include(EFunc)
                    .ToListAsync();
            }
        }

        public async Task DeleteItems(IList selectedItems)
        {
            foreach (var item in selectedItems)
            {
                if (item is Sales sale)
                {
                    await dbFunctions.DeleteItem<Sales>(sale, i => i.id == sale.id);
                }
            }
        }

        public async Task<IEnumerable<KeyValuePair<int, string>>> LoadForComboboxEmp()
        {
            using (var context = new ApplicationDbContext())
            {
                var result = await context.employees
                  .Select(e => new KeyValuePair<int, string>(e.id, e.name))
                  .ToListAsync();
                return result;
            }

        }

        public async Task<IEnumerable<KeyValuePair<int, string>>> LoadForComboboxWareh()
        {
            using (var context = new ApplicationDbContext())
            {
                var result = await context.warehouses
                  .Select(w => new KeyValuePair<int, string>(w.id, w.name))
                  .ToListAsync();
                return result;
            }

        }

        public async Task<IEnumerable<KeyValuePair<int, string>>> LoadForComboboxClient()
        {
            using (var context = new ApplicationDbContext())
            {
                var result = await context.clients
                  .Select(c => new KeyValuePair<int, string>(c.id, c.name))
                  .ToListAsync();
                return result;
            }

        }

        public async Task<List<Items>> LoadItemsAsync()
        {
            using (var context = new ApplicationDbContext())
            {
                return await context.items.ToListAsync();
            }
        }

        public async Task<int> AddSale(int inpId_head, int inpId_warehouse, int inpId_client, string date) 
        {
            using (ApplicationDbContext context = new ApplicationDbContext()) 
            {
                Sales sale = new Sales {id_client = inpId_client, id_employee = inpId_head, id_warehouse = inpId_warehouse, date = date};
                await dbFunctions.AddData<Sales>(sale);
                return sale.id;
            }
        }

        public async Task AddItemsForSale(int inpSale_id, List<int> items_id, List<int> items_quantity) 
        {
            //переписать на словарь item_id ключ, а items_quantity значение
            List<ItemForSale> items = new List<ItemForSale>();
            using (ApplicationDbContext context = new ApplicationDbContext()) 
            {
                for (int i = 0; i < items_id.Count(); i++) 
                {
                    items.Add(new ItemForSale { id_sale = inpSale_id, id_item = items_id[i], quantity = items_quantity[i] });
                }
                await context.AddRangeAsync(items);
                await context.SaveChangesAsync();
            }
        }

        public void SaleXml(int inpId_head, int inpId_warehouse, int clientID, string date, int inpSale_id, List<int> items_id, List<int> items_quantity) 
        {
            Clients clientData = null;
            List<Items> items = new List<Items>();
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                clientData = context.clients.FirstOrDefault(c => c.id == clientID);
                foreach (var id in items_id) 
                {
                    items.Add(context.items.FirstOrDefault(i => i.id == id));
                }
            }
                XDocument xdoc = XDocument.Load("ON_NSCHFDOPPR_1105312123110501001_1102212112110201001_20250605_997ca06b-153f-4464-be64-7fd65b5e834a_0_0_0_0_0_00.xml");
            XElement root = xdoc.Element("Файл");
            if (root == null) 
            {
                MessageBox.Show("Элемент \"Файл\" не найден");
                return;
            }
           
            XElement doc = root.Element("Документ");
            if(doc == null)
            {
                MessageBox.Show("Элемент \"Документ\" не найден");
                return;

            }
           
            XElement theInvoice = doc.Element("СвСчФакт");
            if(theInvoice != null)
            {
                List<XAttribute> attributes = theInvoice.Attributes().ToList();
                attributes[0].Value = inpSale_id.ToString();
                attributes[1].Value = date;
            }
            else
            {
                return;
            }

            XElement client = theInvoice.Element("СвПокуп");
            if(client != null) 
            {
                //supplier.Attribute
                List<XElement> allDescendants = client.Descendants().ToList();

                List<XAttribute> clientAtrs = allDescendants[1].Attributes().ToList(); //СвЮЛУч
                clientAtrs[0].Value = clientData.name; //Атрибут НаимОрг

                List<XAttribute> addressAtrs = allDescendants[3].Attributes().ToList();//АдрИнф 
                addressAtrs[2].Value = clientData.address; // Атрибут АдрТекст
            }
            else
            {
                MessageBox.Show("Элемент \"СвПокуп\" не найден");
            }

            XElement tableTheInvoice = doc.Element("ТаблСчФакт");
            if(tableTheInvoice != null)
            {
                List<XElement> allElementsTheInvoice = tableTheInvoice.Descendants().ToList();
                allElementsTheInvoice[0].Remove(); //Очищаем стврые сведенья о товарах
                foreach(Items item in items)
                {
                    XElement itemData = new XElement("СведТов");
                    XAttribute rowNub = new XAttribute("НомСтр", "1");
                    XAttribute itemName = new XAttribute("НаимТов", $"{item.name}");
                    XAttribute unitItem = new XAttribute("НаимЕдИзм", $"{item.unit}");
                    //XAttribute quantity = new XAttribute("КолТов", $"{item.unit}"); доставть из словаря по item.ifd значение
                    XAttribute priceItem = new XAttribute("ЦенаТов", $"{item.price}");
                    //XAttribute priceItems = new XAttribute("СтТовБезНДС", $"{item.price * }"); доставть из словаря по item.ifd значение и умножить
                    //XAttribute nsdItem = new XAttribute("НалСт", ${ item.nds });
                    //XAttribute totalPriceItems = new XAttribute("СтТовУчНал", $"{item.price * }"); доставть из словаря по item.ifd значение и умножить еще посичтать налог
                    XElement TaxForItem = new XElement("СумНал"); //нужно будет продублтровать при добавлении
                    XElement total = new XElement("ВсегоОпл");
                    //XAttribute totalSum = new XAttribute("СтТовБезНДСВсего", $"{item.price * }"); доставть из словаря по item.ifd значение и умножить
                    //XAttribute totalSumWithNds = new XAttribute("СтТовУчНалВсего", $"{item.price * }"); доставть из словаря по item.ifd значение и умножить
                    XElement totalSumTax = new XElement("СумНалВсего");

                }


            }
        }
    }
}
