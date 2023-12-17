using HdrBoxReader.BO.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.DAL.Repositories
{
    public  interface IHdrBoxRepo
    {
        int AddNew(HdrBox hdrBoxData);
        HdrBox? FindBySupplierAndBoxId(string suppplierId, string boxId);
        void UpdateBox(HdrBox existingBox, HdrBox incomingBox);
    }
}
