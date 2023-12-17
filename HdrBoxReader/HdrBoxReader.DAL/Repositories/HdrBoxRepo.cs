using Dapper;
using HdrBoxReader.BO.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace Infrastructure.DAL.Repositories
{
    public class HdrBoxRepo : RepositoryBase, IHdrBoxRepo
    {
        

        public HdrBoxRepo(IDbTransaction transaction)
           : base(transaction)
        {
        }

        public int AddNew(HdrBox hdrBoxData)
        {
            var writeTime = DateTime.Now;
            var sqlBoxInsert = "INSERT INTO hdrbox(supplieridentifier, identifier, createdon, modifiedon) VALUES(@SupplierIdentifier, @Identifier, @CreatedOn, @ModifiedOn) RETURNING id";

            var newBoxId = Connection. ExecuteScalar<int>(sqlBoxInsert,
                param: new {
                    SupplierIdentifier = hdrBoxData.SupplierIdentifier,
                    Identifier = hdrBoxData.Identifier, 
                    CreatedOn = writeTime, ModifiedOn = writeTime
                       },
                transaction: Transaction
            );

            foreach (var contentItem in hdrBoxData.Contents)
            {
                InsertBoxContentItem(newBoxId, contentItem, writeTime);
            }
            return newBoxId;
        }

        public HdrBox? FindBySupplierAndBoxId(string suppplierId, string boxId)
        {
            var sql = "SELECT * FROM hdrbox WHERE supplieridentifier = @SupplierIdentifier and identifier = @BoxIdentifier";

            var result = Connection.QuerySingleOrDefault<HdrBox>(sql,
                param: new { SupplierIdentifier = suppplierId, BoxIdentifier = boxId },
                transaction: Transaction);
            
            if (result != null)
            {
                var sqlContent = "SELECT * FROM hdrboxcontent WHERE hdrboxid = @Id";
                
                var contentList = Connection.Query<HdrBoxContent>(sqlContent,
                    param: new { Id = result.Id },
                    transaction: Transaction);
                
               // var contentList = contentMapper.Read<HdrBoxContent>().ToList();
                result.Contents = contentList != null ? contentList.ToList() : new List<HdrBoxContent>();
            }

            return result;
        }

        public void UpdateBox(HdrBox existingBox, HdrBox incomingBox)
        {
            var writeTime = DateTime.Now;
            foreach (var incomingContentItem in incomingBox.Contents)
            {
                var contentForUpdate = existingBox.Contents.FirstOrDefault(x => x.Isbn == incomingContentItem.Isbn && x.PoNumber == incomingContentItem.PoNumber);
                if (contentForUpdate != null)
                {
                    if (contentForUpdate.Quantity != incomingContentItem.Quantity)
                    {
                        Connection.ExecuteScalar<int>(
                          "UPDATE hdrboxcontent SET quantity = @Quantity, modifiedon = @ModifiedOn WHERE id = @Id",
                        param: new
                        {
                            Quantity = incomingContentItem.Quantity,
                            Id = contentForUpdate.Id,
                            ModifiedOn = writeTime
                        },
                            transaction: Transaction
                        );
                    }
                }
                else
                {
                    InsertBoxContentItem(existingBox.Id, incomingContentItem, writeTime);
                }
            }
        }

        private int InsertBoxContentItem(int boxId, HdrBoxContent contentItem, DateTime writeTime)
        {
            var sqlBoxContentInsert = "INSERT INTO hdrboxcontent(hdrboxid, ponumber, isbn, quantity, createdon, modifiedon) VALUES(@HdrBoxId, @PONumber, @Isbn, @Quantity, @CreatedOn, @ModifiedOn)";

            var result = Connection.ExecuteScalar<int>(sqlBoxContentInsert,
                param: new
                {
                    HdrBoxId = boxId,
                    PONumber = contentItem.PoNumber,
                    Isbn = contentItem.Isbn,
                    Quantity = contentItem.Quantity,
                    CreatedOn = writeTime,
                    ModifiedOn = writeTime
                },
                transaction: Transaction
            );
            return result;
        }

    }
}
