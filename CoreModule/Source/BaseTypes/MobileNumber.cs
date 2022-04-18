using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Source.BaseTypes
{
    public class MobileNumber
    {
       
            public MobileNumber(string mobileNumber)
            {
                if (string.IsNullOrWhiteSpace(mobileNumber))
                    throw new ArgumentNullException();
                var num = mobileNumber.Trim();
                if (num.Length < 10 || num.Length > 14) throw new ArgumentException("Mobile number is not valid");

                value = num;
            }

            public string value { get; }

            public override string ToString()
            {
                return value;
            }

            public static implicit operator string(MobileNumber name)
            {
                return name.value;
            }

            public static implicit operator MobileNumber(string value)
            {
                return new MobileNumber(value);
            }
        }
    }

