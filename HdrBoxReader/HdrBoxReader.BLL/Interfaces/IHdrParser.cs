using HdrBoxReader.BO.Entities;

namespace HdrBoxReader.BLL
{
    public interface IHdrParser
    {
        HdrBoxContent ParseBoxLine(string contentLine);
        HdrBox ParseHDRBox(string hdrLine);
    }
}