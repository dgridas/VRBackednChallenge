using HdrBoxReader.BO.Entities;
using Infrastructure.DAL;
using Infrastructure.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HdrBoxReader.BLL.DataServices
{
    public class HdrKeeper
    {
        private int _batchChunkSize;

        private List<HdrBox> _chuckCollection;

        public void Init(int batchChunkSize)
        { 
            _batchChunkSize = batchChunkSize;
            ResetChunk();
        }

        public void PersistIncomingHdrBox(HdrBox incomingData, bool IsFinal = false)
        {
            _chuckCollection.Add(incomingData);
            if (_chuckCollection.Count() >= _batchChunkSize || IsFinal)
            {
                SaveChunkToDB();
            }
        }

        private void SaveChunkToDB()
        {
            using (var dbService = new UnitOfWork("Host=127.0.0.1; Port=7777; Database = hdrboxstorage; Username =postgres; Password =guest;"))
            {
                foreach (var item in _chuckCollection)
                {
                    var existingBox = dbService.HdrBoxRepo.FindBySupplierAndBoxId(item.SupplierIdentifier, item.Identifier);
                    if (existingBox == null)
                    {
                        dbService.HdrBoxRepo.AddNew(item);
                    }
                    else
                    {
                        dbService.HdrBoxRepo.UpdateBox(existingBox, item);
                    }
                }
                dbService.Commit();
            }
            ResetChunk();
        }

        private void ResetChunk()
        {
            if (_chuckCollection == null)
            {
                _chuckCollection = new List<HdrBox>();
            }
            else
            {
                _chuckCollection.Clear();
            }
        }
    }
}
