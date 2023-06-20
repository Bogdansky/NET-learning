using Adapter.Adapter;
using Adapter.Composite;
using Adapter.Facade;

namespace Adapter
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // adapter
            var elements = new IntElements(new List<int> { 1, 2, 3, 4, 5 });
            var adapter = new Adapter<int>(elements);

            new Printer().Print(adapter);

            // composite
            var inputText = new InputText("name", "value");
            var label = new LabelText("label");
            var form = new Form("form");
            var headForm = new Form("head-form");

            form.AddComponent(inputText);
            form.AddComponent(label);

            headForm.AddComponent(form);

            Console.WriteLine(headForm.ConvertToString());

            // facade
            var productList = new List<Product>()
            {
                new Product() { Name = "iphone", Id = "1-1-1-1", Price = 1234m},
                new Product() { Name = "apple", Id = "2-2-2-2", Price = 1m},
                new Product() { Name = "TV", Id = "3-3-3-3", Price = 2500m}
            };
            var catalog = productList.ToDictionary(k => k.Id);
            var productCatalog = new AnyProductCatalog(catalog);
            var paymentSystem = new MIRPaymentSystem();
            var invoiceSystem = new AnyInvoiceSystem();
            var orderFacade = new OrderFacade(productCatalog, paymentSystem, invoiceSystem);

            orderFacade.PlaceOrder("1-1-1-1", 20, "banana@long.net");
        }
    }
}