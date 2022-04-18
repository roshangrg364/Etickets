using CoreModule.Source.BaseTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Source.Entity
{
    public class ShippingAddress
    {
        protected ShippingAddress()
        {

        }
        public ShippingAddress(string fullname, string address, string zipCode, string phone)
        {
            FullName = fullname;
            Address = address;
            ZipCode = zipCode;
            PhoneNumber = phone;
        }
        public int Id { get; protected set; }
        public string FullName { get; protected set; }
        public string Address { get; protected set; }
        public string ZipCode { get; protected set; }
        public MobileNumber PhoneNumber { get; protected set; }
    }
}
