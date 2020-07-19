using SWCCorp.Models;
using SWCCorp.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCCorp.Data
{
    public class ProdRepository : IOrderRepository
    {

        //declare file stuff
        private string _filePath;

        public ProdRepository(string filePath)
        {
            _filePath = filePath;
        }

        public Orders LoadOrder(string orderDate)
        {
            var uorderDate = _filePath + "Orders_" + orderDate + ".txt";
            string oorderDate = orderDate;
            //check if file exists, else return error
            //if file exists, open file read line by line return specific account, else return error
            //store file name once in variable
            if (!File.Exists(uorderDate))
            {
                File.Create(uorderDate);

                return LookUpOrder(uorderDate, oorderDate);
            }
            else
            {
                return LookUpOrder(uorderDate, oorderDate);
            }

        }

        public List<Orders> FindLastOrder(string orderDate)
        {
            var orderDatePath = _filePath + "Orders_" + orderDate + ".txt";

            List<Orders> orders = new List<Orders>();

            if (!File.Exists(orderDatePath))
            {
                return null;
            }
            else
            {

                string[] rows = File.ReadAllLines(orderDatePath);

                for (int i = 1; i < rows.Length; i++)
                {
                    string[] columns = rows[i].Split(',');

                    Orders o = new Orders();

                    o.OrderNumber = int.Parse(columns[0]);
                    o.CustomerName = columns[1];
                    o.State = columns[2];
                    o.TaxRate = decimal.Parse(columns[3]);
                    o.ProductType = columns[4];
                    o.Area = decimal.Parse(columns[5]);
                    o.CostPerSquareFoot = decimal.Parse(columns[6]);
                    o.LaborCostPerSquareFoot = decimal.Parse(columns[7]);
                    o.MaterialCost = decimal.Parse(columns[8]);
                    o.LaborCost = decimal.Parse(columns[9]);
                    o.Tax = decimal.Parse(columns[10]);
                    o.Total = decimal.Parse(columns[11]);

                    orders.Add(o);
                };
                return orders;
            }

        }

        public List<Orders> FindOrder(string orderDate)
        {
            var orderDatePath = _filePath + "Orders_" + orderDate + ".txt";

            List<Orders> orders = new List<Orders>();

            if (!File.Exists(orderDatePath))
            {
                return null;
            }
            else
            {
                string[] rows = File.ReadAllLines(orderDatePath);

                for (int i = 1; i < rows.Length; i++)
                {
                    string[] columns = rows[i].Split(',');

                    Orders o = new Orders();

                    o.OrderNumber = int.Parse(columns[0]);
                    o.CustomerName = columns[1];
                    o.State = columns[2];
                    o.TaxRate = decimal.Parse(columns[3]);
                    o.ProductType = columns[4];
                    o.Area = decimal.Parse(columns[5]);
                    o.CostPerSquareFoot = decimal.Parse(columns[6]);
                    o.LaborCostPerSquareFoot = decimal.Parse(columns[7]);
                    o.MaterialCost = decimal.Parse(columns[8]);
                    o.LaborCost = decimal.Parse(columns[9]);
                    o.Tax = decimal.Parse(columns[10]);
                    o.Total = decimal.Parse(columns[11]);

                    orders.Add(o);
                };
                return orders;
            }

        }

        public Orders LookUpOrder(string uorderDate, string oorderDate)
        {

            using (StreamReader sr = new StreamReader(uorderDate))
            {
                sr.ReadLine();
                string line;
                //use try catch here
                DateTime dateo;
                dateo = DateTime.ParseExact(oorderDate, "MMddyyyy", CultureInfo.InvariantCulture);
                //create a list and return a list. 
                //Orders newOrder = new Orders();
                while ((line = sr.ReadLine()) != null)
                {
                    Orders newOrder = new Orders();

                    string[] columns = line.Split(',');
                    Console.WriteLine($"{dateo}");
                    newOrder.OrderNumber = int.Parse(columns[0]);
                    newOrder.CustomerName = columns[1];
                    newOrder.State = columns[2];
                    newOrder.TaxRate = decimal.Parse(columns[3]);
                    newOrder.ProductType = columns[4];
                    newOrder.Area = decimal.Parse(columns[5]);
                    newOrder.CostPerSquareFoot = decimal.Parse(columns[6]);
                    newOrder.LaborCostPerSquareFoot = decimal.Parse(columns[7]);
                    newOrder.MaterialCost = decimal.Parse(columns[8]);
                    newOrder.LaborCost = decimal.Parse(columns[9]);
                    newOrder.Tax = decimal.Parse(columns[10]);
                    newOrder.Total = decimal.Parse(columns[11]);

                    return newOrder;

                }
                return null;

            }
            //Console.WriteLine("It appears the Order you are looking for does not exist!");
            //return null;
        }

        public Tax LookUpTax(string stateName)
        {
            var taxPath = _filePath + "Taxes.txt";

            string[] rows = File.ReadAllLines(taxPath);

            for (int i = 1; i < rows.Length; i++)
            {
                string[] columns = rows[i].Split(',');

                Tax t = new Tax();

                t.StateAbbreviation = columns[0];
                t.StateName = columns[1];
                t.TaxRate = decimal.Parse(columns[2]);

                if (t.StateName == stateName)
                {
                    return t;
                }
            };

            Console.WriteLine("It appears the Tax you are looking for does not exist!");
            return null;

            /*using (StreamReader sr = new StreamReader(taxPath))
            {
                sr.ReadLine();
                string line;
                //use try catch here
                while ((line = sr.ReadLine()) != null)
                {
                    Tax newTax = new Tax();
                    string[] columns = line.Split(',');
                    if (stateName == newTax.StateName)
                    {
                        newTax.StateAbbreviation = columns[0];
                        newTax.StateName = columns[1];
                        newTax.TaxRate = decimal.Parse(columns[2]);

                        return newTax;
                    }
                    else
                    {
                          continue;
                    }

                }
                Console.WriteLine("It appears the Tax you are looking for does not exist!");
                return null;
            }*/
        }


        public Products ChooseProduct(string productType)
        {

            var productPath = _filePath + "Products.txt";

            using (StreamReader sr = new StreamReader(productPath))
            {
                sr.ReadLine();
                string line;

                while ((line = sr.ReadLine()) != null)
                {
                    Products newProduct = new Products();

                    string[] columns = line.Split(',');

                    newProduct.ProductType = columns[0];
                    newProduct.LaborCostPerSquareFoot = decimal.Parse(columns[1]);
                    newProduct.CostPerSquareFoot = decimal.Parse(columns[2]);

                    if (newProduct.ProductType == productType)
                    {
                        return newProduct;
                    }
                }
                Console.WriteLine("It appears the product type you entered does not exist! ");
                return null;
            }
        }

        public List<Products> LookUpProducts()
        {

            var productPath = _filePath + "Products.txt";

            string[] rows = File.ReadAllLines(productPath);

            List<Products> products = new List<Products>();

            for (int i = 1; i < rows.Length; i++)
            {
                string[] columns = rows[i].Split(',');

                Products p = new Products();

                p.ProductType = columns[0];
                p.CostPerSquareFoot = decimal.Parse(columns[1]);
                p.LaborCostPerSquareFoot = decimal.Parse(columns[2]);

                products.Add(p);
            };
            return products;

        }

        public void SaveOrder(Orders orders, string orderDate)
        {
            //check if file exists, open in Append Mode and write one line
            //else create a new file, add a header line, then add account line
            var fileDate = _filePath + "Orders_" + orderDate + ".txt";
            if (!File.Exists(fileDate))
            {
                //File.Create(_filePath);
                AccountAddHeaders(orderDate);
                OrdersAdd(orders, orderDate);
            }
            else
            {
                OrdersAdd(orders, orderDate);
                //OrdersUpdate(orders);
            }
        }

        public void OrdersUpdate(Orders orders, string orderDate)
        {

            var fileDate = _filePath + "Orders_" + orderDate + ".txt";
            string oorderDate = orderDate;

            //find order line, replace account data with new data from deposit/withdrawal
            using (StreamWriter sw = new StreamWriter(fileDate, true))
            {

                string line = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}", orders.OrderNumber,
                    orders.CustomerName, orders.State, orders.TaxRate, orders.ProductType, orders.Area, orders.CostPerSquareFoot,
                    orders.LaborCostPerSquareFoot, orders.MaterialCost, orders.LaborCost, orders.Tax, orders.Total);

                sw.WriteLine(line);
            }
        }

        public Orders OrdersAdd(Orders passOrderAdd, string orderDate)
        {

            var fileDate = _filePath + "Orders_" + orderDate + ".txt";
            string oorderDate = orderDate;

            using (StreamWriter sw = new StreamWriter(fileDate, true))
            {

                string line = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}", passOrderAdd.OrderNumber,
                    passOrderAdd.CustomerName, passOrderAdd.State, passOrderAdd.TaxRate, passOrderAdd.ProductType, passOrderAdd.Area, passOrderAdd.CostPerSquareFoot,
                    passOrderAdd.LaborCostPerSquareFoot, passOrderAdd.MaterialCost, passOrderAdd.LaborCost, passOrderAdd.Tax, passOrderAdd.Total);

                sw.WriteLine(line);
                return passOrderAdd;
            }

        }

        public void AccountAddHeaders(string orderDate)
        {

            var fileDate = _filePath + "Orders_" + orderDate + ".txt";
            using (StreamWriter sw = new StreamWriter(fileDate, true))
            {

                string line = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}", "OrderNumber",
                    "CustomerName", "State", "TaxRate", "ProductType", "Area", "CostPerSquareFoot",
                    "LaborCostPerSquareFoot", "MaterialCost", "LaborCost", "Tax", "Total");

                sw.WriteLine(line);
            }
        }

        public Orders OrdersEdit(Orders userEdit, string orderDate, int orderNumber)
        {

            var fileDate = _filePath + "Orders_" + orderDate + ".txt";

            List<Orders> newOrderList = new List<Orders>();

            string[] rows = File.ReadAllLines(fileDate);

            for (int i = 1; i < rows.Length; i++)
            {

                string[] columns = rows[i].Split(',');

                Orders o = new Orders();

                o.OrderNumber = int.Parse(columns[0]);
                o.CustomerName = columns[1];
                o.State = columns[2];
                o.TaxRate = decimal.Parse(columns[3]);
                o.ProductType = columns[4];
                o.Area = decimal.Parse(columns[5]);
                o.CostPerSquareFoot = decimal.Parse(columns[6]);
                o.LaborCostPerSquareFoot = decimal.Parse(columns[7]);
                o.MaterialCost = decimal.Parse(columns[8]);
                o.LaborCost = decimal.Parse(columns[9]);
                o.Tax = decimal.Parse(columns[10]);
                o.Total = decimal.Parse(columns[11]);

                if (orderNumber == o.OrderNumber)
                {
                    newOrderList.Add(userEdit);
                    continue;
                }
                else
                {
                    newOrderList.Add(o);
                }
            }

            File.Delete(fileDate);
            AccountAddHeaders(orderDate);

            using (StreamWriter sw = new StreamWriter(fileDate, true))
            {

                foreach (var y in newOrderList)
                {
                    string line = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}", y.OrderNumber,
                        y.CustomerName, y.State, y.TaxRate, y.ProductType, y.Area, y.CostPerSquareFoot,
                        y.LaborCostPerSquareFoot, y.MaterialCost, y.LaborCost, y.Tax, y.Total);

                    sw.WriteLine(line);
                }
                return userEdit;
            };
        }

        public Orders OrdersRemove(Orders userEdit, string orderDate, int orderNumber)
        {

            var fileDate = _filePath + "Orders_" + orderDate + ".txt";

            List<Orders> newOrderList = new List<Orders>();

            string[] rows = File.ReadAllLines(fileDate);

            for (int i = 1; i < rows.Length; i++)
            {

                string[] columns = rows[i].Split(',');

                Orders o = new Orders();

                o.OrderNumber = int.Parse(columns[0]);
                o.CustomerName = columns[1];
                o.State = columns[2];
                o.TaxRate = decimal.Parse(columns[3]);
                o.ProductType = columns[4];
                o.Area = decimal.Parse(columns[5]);
                o.CostPerSquareFoot = decimal.Parse(columns[6]);
                o.LaborCostPerSquareFoot = decimal.Parse(columns[7]);
                o.MaterialCost = decimal.Parse(columns[8]);
                o.LaborCost = decimal.Parse(columns[9]);
                o.Tax = decimal.Parse(columns[10]);
                o.Total = decimal.Parse(columns[11]);

                if (orderNumber == o.OrderNumber)
                {
                    continue;
                }
                else
                {
                    newOrderList.Add(o);
                }
            }

            File.Delete(fileDate);
            AccountAddHeaders(orderDate);

            using (StreamWriter sw = new StreamWriter(fileDate, true))
            {

                foreach (var y in newOrderList)
                {
                    string line = string.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10},{11}", y.OrderNumber,
                        y.CustomerName, y.State, y.TaxRate, y.ProductType, y.Area, y.CostPerSquareFoot,
                        y.LaborCostPerSquareFoot, y.MaterialCost, y.LaborCost, y.Tax, y.Total);

                    sw.WriteLine(line);
                }
                return userEdit;
            };
        }
    }
}


