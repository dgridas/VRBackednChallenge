using HdrBoxReader.BO.Entities;

namespace HdrBoxReader.BLL
{
    public class HdrParser : IHdrParser
    {
        public HdrParser() { }


        public HdrBox ParseHDRBox(string hdrLine)
        {
            var result = new HdrBox() { Contents = new List<HdrBoxContent>() };
            result.Identifier = hdrLine.Substring(5, 7);
            result.SupplierIdentifier = hdrLine.Substring(97, 8);
            return result;
        }

        public HdrBoxContent ParseBoxLine(string contentLine)
        {
            var result = new HdrBoxContent();
            result.PoNumber = contentLine.Substring(5, 37).Trim();
            result.Quantity = int.Parse(contentLine.Substring(76, contentLine.Length - 76).Trim());
            result.Isbn = contentLine.Substring(42, 34).Trim();

            return result;
        }


    }
}
