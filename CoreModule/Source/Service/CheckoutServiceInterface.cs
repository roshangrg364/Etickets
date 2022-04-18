using CoreModule.Source.Dto.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Source.Service
{
    public interface CheckoutServiceInterface
    {
        Task<int> Checkout(CheckoutDto dto);
    }
}
