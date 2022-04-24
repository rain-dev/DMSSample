namespace Pinewood.DMSSample.Business.Clients
{
    public interface IPartAvailabilityClient
    {
        Task<int> GetAvailability(string stockCode);
    }
}
