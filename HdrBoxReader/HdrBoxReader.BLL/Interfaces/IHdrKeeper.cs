using HdrBoxReader.BO.Entities;

namespace HdrBoxReader.BLL.DataServices
{
    public interface IHdrKeeper
    {
        void Init();
        void PersistIncomingHdrBox(HdrBox incomingData, bool IsFinal = false);
    }
}