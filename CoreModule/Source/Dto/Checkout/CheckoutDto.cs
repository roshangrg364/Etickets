using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Source.Dto.Checkout
{
    public class CheckoutDto
    {
        public CheckoutDto(string userId,decimal paidAmount,string name,string address,string zip, string phoneNumber)
        {
            UserId = userId;
            PaidAmount = paidAmount;
            FullName = name;
            Address = address;
            ZipCode = zip;
            PhoneNumber = phoneNumber;
        }
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string PhoneNumber { get; set; }
        public decimal PaidAmount { get; set; }

    }
    

}
