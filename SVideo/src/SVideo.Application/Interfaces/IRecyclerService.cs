namespace SVideo.Application.Interfaces
{
    public interface IRecyclerService
    {
        void RemoveVideo(int days);
        string StatusRunning();
        void UpdateRunning();
    }
}
