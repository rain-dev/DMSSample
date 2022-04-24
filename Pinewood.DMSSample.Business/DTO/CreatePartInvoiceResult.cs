namespace Pinewood.DMSSample.Business.DTO
{
    public class CreatePartInvoiceResult
    {
        public CreatePartInvoiceResult(bool success)
        {
            Success = success;
        }

        public bool Success { get; }
    }
}