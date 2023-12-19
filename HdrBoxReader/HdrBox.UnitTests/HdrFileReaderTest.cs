using HdrBoxReader.BLL.Interfaces;
using Moq;
using System.Text;
using TestDataBundle;

namespace HdrBoxReader.BLL.UnitTests
{
    [TestClass]
    public class HdrFileReaderTest
    {
        private IHdrFileReader _hdrReader;
        
        [TestInitialize]
        public void Initialize() 
        {
            byte[] fakeFileBytes = Encoding.UTF8.GetBytes($"{HdrFileData.HdrLine}\n{HdrFileData.HdrContentLine}");

            MemoryStream fakeMemoryStream = new MemoryStream(fakeFileBytes);
           
            var streamMock = new Mock<IStreamWrapper>();
            streamMock.Setup(x => x.StreamReader(It.IsAny<string>())).Returns(() => new StreamReader(fakeMemoryStream));
            
            _hdrReader = new HdrFileReader(streamMock.Object);
            _hdrReader.Init(string.Empty);
        }

        [TestMethod]
        public void ReadHDRBoxLine_SuccessfullyReturnFileLines()
        {
            //first line
            var hdrLine = _hdrReader.GetLine();
            Assert.AreEqual(hdrLine, HdrFileData.HdrLine);

            //second line
            var hdrContentLine = _hdrReader.GetLine();
            Assert.AreEqual(hdrContentLine, HdrFileData.HdrContentLine);

            //third line
            var endOfFile = _hdrReader.GetLine();
            Assert.IsNull(endOfFile);
        }
    }
}