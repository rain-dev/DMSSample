namespace Pinewood.DMSSample.Data.Models
{
    public class Customer : ModelBase<int>
    {
        public Customer(int id, string name, string address)
        {
            Id = id;
            Name = name;
            Address = address;
        }

        public string Name { get; set; }
        public string Address { get; set; }
    }
}
