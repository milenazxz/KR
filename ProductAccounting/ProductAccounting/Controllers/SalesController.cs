using Microsoft.EntityFrameworkCore;
using ProductAccounting.Forms;
using ProductAccounting.Interfaces;
using ProductAccounting.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
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

        public async Task AddItemsForSale(int inpSale_id, ObservableCollection<ItemsforsaleDTO> itemsforsalesDTO) 
        {
            //переписать на словарь item_id ключ, а items_quantity значение
            List<ItemForSale> items = new List<ItemForSale>();
            using (ApplicationDbContext context = new ApplicationDbContext()) 
            {
                foreach (var item in itemsforsalesDTO) 
                {
                    items.Add(new ItemForSale { id_sale = inpSale_id, id_item = item.Id, quantity = item.Quantity, nds = item.NDS});
                }
                await context.AddRangeAsync(items);
                await context.SaveChangesAsync();
            }
        }

        public async Task SaleXml(int IdSale) 
        {
            double totalWithoutNDS = 0;
            double totalWithNDS = 0;
            double sumNal = 0;
            int itemNumber = 1;
            double itemSum = 0;
            Sales saleData;
            List<ItemForSale> itemsData = new List<ItemForSale>();


            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                saleData = context.sales
                    .Include(s => s.IdClientNavigation)
                    .FirstOrDefault(s => s.id == IdSale);
                itemsData = await context.itemsforsales
                    .Include(ifs => ifs.IdItemNavigation)
                    .Where(s => s.id_sale == IdSale).ToListAsync();
            }
            XDocument xdoc = XDocument.Load("ON_NSCHFDOPPR_1105312123110501001_1102212112110201001_20250605_997ca06b-153f-4464-be64-7fd65b5e834a_0_0_0_0_0_00.xml");
            /*XElement root = xdoc.Element("Файл");
            if (root == null) 
            {
                MessageBox.Show("Элемент \"Файл\" не найден");
                return;
            }*/
           
            XElement doc = xdoc.Element("Документ");
            if(doc == null)
            {
                MessageBox.Show("Элемент \"Документ\" не найден");
                return;

            }
           
            XElement theInvoice = doc.Element("СвСчФакт");
            if(theInvoice != null)
            {
                List<XAttribute> attributes = theInvoice.Attributes().ToList();
                attributes[0].Value = IdSale.ToString();
                attributes[1].Value = saleData.date;
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
                clientAtrs[0].Value = saleData.IdClientNavigation.name; //Атрибут НаимОрг

                List<XAttribute> addressAtrs = allDescendants[3].Attributes().ToList();//АдрИнф 
                addressAtrs[2].Value = saleData.IdClientNavigation.address; // Атрибут АдрТекст
            }
            else
            {
                MessageBox.Show("Элемент \"СвПокуп\" не найден");
            }

            XElement tableTheInvoice = doc.Element("ТаблСчФакт");
            if(tableTheInvoice != null)
            {
                var oldItems = tableTheInvoice.Elements("СведТов").ToList();
                foreach (var elem in oldItems)
                    elem.Remove();//Очищаем старые данные

                var oldTotalSum = tableTheInvoice.Elements("ВсегоОпл").ToList();
                foreach (var elem in oldTotalSum)
                    elem.Remove();

                foreach (ItemForSale item in itemsData)
                {
                    totalWithoutNDS += item.IdItemNavigation.price * item.quantity;
                    totalWithNDS += (itemSum) + (itemSum * item.nds / 100);
                    itemSum = item.IdItemNavigation.price * item.quantity;
                    sumNal += itemSum * item.nds / 100;
                    XElement itemData = new XElement("СведТов",
                        new XAttribute("НомСтр", itemNumber.ToString()),
                        new XAttribute("НаимТов", $"{item.IdItemNavigation.name}"),
                        new XAttribute("НаимЕдИзм", $"{item.IdItemNavigation.unit}"),
                        new XAttribute("КолТов", $"{item.quantity}"),
                        new XAttribute("ЦенаТов", $"{item.IdItemNavigation.price}"),
                        new XAttribute("СтТовБезНДС", $"{itemSum}"),
                        new XAttribute("НалСт", $"{item.nds}"),
                        new XAttribute("СтТовУчНал", $"{(itemSum) + (itemSum * item.nds / 100)}")
                        );
                    tableTheInvoice.Add(itemData);
                    itemNumber++;
                }
                XElement totalSum = new XElement("ВсегоОпл",
                        new XAttribute("СтТовБезНДСВсего", $"{totalWithoutNDS}"),
                        new XAttribute("СтТовУчНалВсего", $"{totalWithNDS}")
                    );
                XElement sumNalTotal = new XElement("СумНалВсего");
                XElement SumNal = new XElement("СумНал", sumNal.ToString("F2", CultureInfo.InvariantCulture));
                tableTheInvoice.Add(totalSum);
                totalSum.Add(sumNalTotal);
                sumNalTotal.Add(SumNal);

            }
            doc.Save("ON_NSCHFDOPPR_1105312123110501001_1102212112110201001_20250605_997ca06b-153f-4464-be64-7fd65b5e834a_0_0_0_0_0_00.xml");
        }
    }
}
