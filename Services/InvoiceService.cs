using E_CommerceSystem.Models;
using E_CommerceSystem.Repositories;
using QuestPDF.Fluent;

namespace E_CommerceSystem.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IOrderRepo _orderRepo;

        public InvoiceService(IOrderRepo orderRepo)
        {
            _orderRepo = orderRepo;
        }

        public byte[] GenerateInvoicePdf(int orderId)
        {
            var order = _orderRepo.GetOrderById(orderId);
            if (order == null)
                throw new KeyNotFoundException("Order not found");

            var invoice = new InvoiceDTO
            {
                OrderId = order.OID,
                CustomerName = order.user.UName,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                Items = order.OrderProducts.Select(op => new InvoiceItemDTO
                {
                    ProductName = op.product.ProductName,
                    Quantity = op.Quantity,
                    Price = op.product.Price
                }).ToList()
            };

            var document = QuestPDF.Fluent.Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Header().Text($"Invoice #{invoice.OrderId}");
                    page.Content().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                        });

                        table.Header(header =>
                        {
                            header.Cell().Text("Product");
                            header.Cell().Text("Qty");
                            header.Cell().Text("Price");
                            header.Cell().Text("Total");
                        });

                        foreach (var item in invoice.Items)
                        {
                            table.Cell().Text(item.ProductName);
                            table.Cell().Text(item.Quantity.ToString());
                            table.Cell().Text(item.Price.ToString("C"));
                            table.Cell().Text(item.Total.ToString("C"));
                        }
                    });

                    page.Footer().AlignCenter().Text($"Total: {invoice.TotalAmount:C}");
                });
            });

            return document.GeneratePdf();
        }
    }
}
