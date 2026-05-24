using System;

namespace FurnitureStore.ViewModels
{
    public class AddressViewModel
    {
        public int AddressId { get; set; }
        public string AddressName { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public bool IsDefault { get; set; }
    }
}
