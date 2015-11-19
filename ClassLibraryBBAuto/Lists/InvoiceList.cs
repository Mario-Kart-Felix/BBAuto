using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace ClassLibraryBBAuto
{
    public class InvoiceList : MainList
    {
        private List<Invoice> list;
        private static InvoiceList uniqueInstance;

        private InvoiceList()
        {
            list = new List<Invoice>();

            loadFromSql();
        }

        public static InvoiceList getInstance()
        {
            if (uniqueInstance == null)
                uniqueInstance = new InvoiceList();

            return uniqueInstance;
        }

        protected override void loadFromSql()
        {
            DataTable dt = _provider.Select("Invoice");

            foreach (DataRow row in dt.Rows)
            {
                Invoice invoice = new Invoice(row);
                Add(invoice);
            }
        }

        public void Add(Invoice invoice)
        {
            if (list.Exists(item => item == invoice))
                return;

            list.Add(invoice);
        }

        public Invoice getItem(int idInvoice)
        {
            var invoices = list.Where(item => item.IsEqualsID(idInvoice));

            return (invoices.Count() > 0) ? invoices.First() : new Invoice(0);
        }

        public Invoice getItem(Car car)
        {
            var invoices = from invoice in list
                           where invoice.isEqualCarID(car) && invoice.DateMove != string.Empty
                           orderby invoice.Date descending, invoice.Number descending
                           select invoice;

            return (invoices.Count() > 0) ? invoices.First() : null;
        }

        public DataTable ToDataTable()
        {
            var invoices = from invoice in list
                           orderby invoice.Date descending, invoice.Number descending
                           select invoice;

            return createTable(invoices.ToList());
        }

        public DataTable ToDataTable(Car car)
        {
            var invoices = from invoice in list
                           where invoice.isEqualCarID(car)
                           orderby invoice.Date descending, invoice.Number descending
                           select invoice;

            return createTable(invoices.ToList());
        }

        private DataTable createTable(List<Invoice> invoices)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("id");
            dt.Columns.Add("idCar");
            dt.Columns.Add("Бортовой номер");
            dt.Columns.Add("Регистрационный знак");
            dt.Columns.Add("№ накладной", Type.GetType("System.Int32"));
            dt.Columns.Add("Откуда");
            dt.Columns.Add("Сдал");
            dt.Columns.Add("Куда");
            dt.Columns.Add("Принял");
            dt.Columns.Add("Дата накладной", Type.GetType("System.DateTime"));
            dt.Columns.Add("Дата передачи", Type.GetType("System.DateTime"));

            foreach (Invoice invoice in invoices)
                dt.Rows.Add(invoice.getRow());

            return dt;
        }

        public void Delete(int idInvoice)
        {
            Invoice invoice = getItem(idInvoice);

            list.Remove(invoice);
            invoice.Delete();
        }

        internal int GetNextNumber()
        {
            var invoices = list.Where(item => item.Date.Year == DateTime.Today.Year).OrderByDescending(item => Convert.ToInt32(item.Number));

            return (invoices.Count() == 0) ? 1 : Convert.ToInt32(invoices.First().Number) + 1;
        }
    }
}
