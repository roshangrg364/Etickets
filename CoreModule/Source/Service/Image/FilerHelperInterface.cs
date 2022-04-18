using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreModule.Source.Service.Image
{
    public interface FilerHelperInterface
    {
       
        string SaveImageAndGetFileName(string fileName, string filePrefix = "");
        void RemoveFile(string fileName);
        bool IsValidImage(string fileName);
       
    }
}
