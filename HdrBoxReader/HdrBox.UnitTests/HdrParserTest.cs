using TestDataBundle;

namespace HdrBoxReader.BLL.UnitTests
{
    [TestClass]
    public class HdrParserTest
    {
        private IHdrParser _hdrParser;

        [TestInitialize]
        public void Initialize() 
        {
            _hdrParser = new HdrParser();
        }

        [TestMethod]
        public void ParseHDRBox_SuccessfulParse()
        {
            var result = _hdrParser.ParseHDRBox(HdrFileData.HdrLine);
            Assert.AreEqual(HdrFileData.ExpectedHdrBoxResult.Identifier, result.Identifier);
            Assert.AreEqual(HdrFileData.ExpectedHdrBoxResult.SupplierIdentifier, result.SupplierIdentifier);
        }

        [TestMethod]
        public void ParseHDRBoxContent_SuccessfulParse()
        {
            var result = _hdrParser.ParseBoxLine(HdrFileData.HdrContentLine);
            Assert.AreEqual(HdrFileData.ExpectedHdrBoxResult.Contents.First().Isbn, result.Isbn);
            Assert.AreEqual(HdrFileData.ExpectedHdrBoxResult.Contents.First().Quantity, result.Quantity);
            Assert.AreEqual(HdrFileData.ExpectedHdrBoxResult.Contents.First().PoNumber, result.PoNumber);
        }

        [TestMethod]
        public void ParseHDRBox_IsNullOnWrongHdrLIne()
        {
            var result = _hdrParser.ParseHDRBox(HdrFileData.WrongHdrLine);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void ParseHDRBoxContent_IsNullOnWrongHdrLIne()
        {
            var result = _hdrParser.ParseBoxLine(HdrFileData.WrongHdrLine);
            Assert.IsNull(result);
        }
    }
}