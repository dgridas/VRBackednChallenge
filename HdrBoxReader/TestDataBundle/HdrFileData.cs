using HdrBoxReader.BO.Entities;

namespace TestDataBundle
{
    public class HdrFileData
    {
        public static readonly string WrongHdrLine = "wrong line";
        public static readonly string HdrLine = "HDR  TRSP117                                                                                     6874453I   ";
        public static readonly string HdrContentLine = "LINE P000001661                           9781473663800                     18     ";

        public static readonly HdrBox ExpectedHdrBoxResult = new HdrBox()
        {
            Identifier = "TRSP117",
            SupplierIdentifier = "6874453I",
            Contents = new List<HdrBoxContent>() 
            { 
                new HdrBoxContent() { 
                    PoNumber = "P000001661", 
                    Isbn = "9781473663800",
                    Quantity = 18
                } 
            }
        };
    }
}