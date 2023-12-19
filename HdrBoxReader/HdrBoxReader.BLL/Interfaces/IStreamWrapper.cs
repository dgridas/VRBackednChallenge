using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HdrBoxReader.BLL.Interfaces
{
    public interface IStreamWrapper
    {
        StreamReader StreamReader(string path);
    }
}
