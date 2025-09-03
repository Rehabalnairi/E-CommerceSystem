namespace E_CommerceSystem.Services
{
    public interface IInvoiceService
    {
        byte[] GenerateInvoicePdf(int orderId);
    }
}