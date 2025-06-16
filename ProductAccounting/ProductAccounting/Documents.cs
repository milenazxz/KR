using ClosedXML.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Xml;
using System.Xml.Linq;

namespace ProductAccounting
{
    public class Documents
    {
        public void CreatrDocumentXlsx()
        {
            XDocument xdoc = XDocument.Load("ON_NSCHFDOPPR_1105312123110501001_1102212112110201001_20250605_997ca06b-153f-4464-be64-7fd65b5e834a_0_0_0_0_0_00.xml");
            string excelTemplatePath = "Бланк УПД.xlsx";
            string outputExcelPath = "УПД.xlsx";

            XElement doc = xdoc.Element("Документ");

            XElement theInvoice = doc.Element("СвСчФакт");
            XElement saller = theInvoice.Element("СвПрод");


            string sellerName = saller.Attribute("СокрНаим").Value;

            XElement sallerAddress = saller?.Element("Адрес");
            XElement sallerAddressInf = sallerAddress?.Element("АдрИнф");

            string sellerAddress = sallerAddressInf?.Attribute("АдрТекст")?.Value ?? "";

            XElement buyer = theInvoice.Element("СвПокуп");
            string buyerName = buyer.Attribute("СокрНаим").Value;

            XElement buyerAddress = buyer?.Element("Адрес");
            XElement buyerAddressInf = buyerAddress?.Element("АдрИнф");

            string buyerAddressstr = buyerAddressInf?.Attribute("АдрТекст")?.Value ?? "";

            XElement tableTheInvoice = doc.Element("ТаблСчФакт");


            var products = new List<Product>();
            foreach (XElement productNode in tableTheInvoice?.Elements("СведТов") ?? Enumerable.Empty<XElement>())
            {
                products.Add(new Product
                {
                    Name = productNode.Attribute("НаимТов")?.Value ?? "",
                    Unit = productNode.Attribute("НаимЕдИзм")?.Value ?? "",
                    Quantity = double.TryParse(productNode.Attribute("КолТов")?.Value?.Replace(',', '.'), out var q) ? q : 0,
                    Price = double.TryParse(productNode.Attribute("ЦенаТов")?.Value?.Replace(',', '.'), out var p) ? p : 0,
                    TotalWithoutTax = double.TryParse(productNode.Attribute("СтТовБезНДС")?.Value?.Replace(',', '.'), out var twn) ? twn : 0,
                    TaxRate = int.TryParse(productNode.Attribute("НалСт")?.Value, out var tr) ? tr : 0,
                    TotalWithTax = double.TryParse(productNode.Attribute("СтТовУчНал")?.Value?.Replace(',', '.'), out var twt) ? twt : 0
                });
            }

            // Открываем Excel шаблон
            using (var workbook = new XLWorkbook(excelTemplatePath))
            {
                int itemNum = 1;
                var worksheet = workbook.Worksheet(1);

                // Заполняем данные продавца
                worksheet.Cell("Y5").Value = sellerName;
                worksheet.Cell("Y6").Value = sellerAddress;

                // Заполняем данные покупателя
                worksheet.Cell("Y12").Value = buyerName;
                worksheet.Cell("Y13").Value = buyerAddressstr;

                IXLRow sourceRow = worksheet.Row(21);
                worksheet.Range(21, 9, 21, 11).Value = itemNum; // Номер товара
                worksheet.Range(21, 12, 21,17).Value = products[0].Name; // Наименование товара
                worksheet.Range(21,23,21,29 ).Value = products[0].Unit; // Единица измерения
                worksheet.Range(21,30,21,33).Value = products[0].Quantity; // Количество
                worksheet.Range(21,34,21,38).Value = products[0].Price; // Цена за единицу
                worksheet.Range(21,39,21,43).Value = products[0].TotalWithoutTax; // Стоимость без налога
                worksheet.Range(21,50,21, 53).Value = products[0].TaxRate; // Налоговая ставка
                worksheet.Range(21,62,21,66).Value = products[0].TotalWithTax; // Стоимость с налогом*/


                products.RemoveAt(0);
                // Заполняем таблицу товаров
                int row = 21; // Начинаем с первой строки таблицы
                int newRow = row;
                foreach (var product in products)
                {
                    newRow++;
                    itemNum++;
                    worksheet.Row(row).InsertRowsBelow(1);
                    sourceRow.CopyTo(worksheet.Row(newRow));
                    worksheet.Row(newRow).Clear(XLClearOptions.Contents);
                    worksheet.Range(newRow, 9, newRow, 11).Value = itemNum; // Номер товара
                    worksheet.Range(newRow, 12, newRow, 17).Value = product.Name; // Наименование товара
                    worksheet.Range(newRow, 23, newRow, 29).Value = product.Unit; // Единица измерения
                    worksheet.Range(newRow, 30, newRow, 33).Value = product.Quantity; // Количество
                    worksheet.Range(newRow, 34, newRow, 38).Value = product.Price; // Цена за единицу
                    worksheet.Range(newRow, 39, newRow, 43).Value = product.TotalWithoutTax; // Стоимость без налога
                    worksheet.Range(newRow, 50, newRow, 53).Value = product.TaxRate; // Налоговая ставка
                    worksheet.Range(newRow, 62, newRow, 66).Value = product.TotalWithTax; // Стоимость с налогом
                    
                    row = newRow;
                }

                // Сохраняем Excel файл
                workbook.SaveAs(outputExcelPath);
            }

            Console.WriteLine("Данные успешно перенесены в Excel.");
        }
    }

    class Product
    {
        public string Name { get; set; }
        public string Unit { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
        public double TotalWithoutTax { get; set; }
        public int TaxRate { get; set; }
        public double TotalWithTax { get; set; }
    }
}

