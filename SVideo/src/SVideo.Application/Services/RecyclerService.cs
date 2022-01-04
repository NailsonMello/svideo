using SVideo.Application.Interfaces;
using SVideo.Domain.Entities;
using SVideo.Domain.Repositories;
using System.Collections.Generic;

namespace SVideo.Application.Services
{
    public class RecyclerService : IRecyclerService
    {

        private readonly IRecyclerRepository _repository;
        private readonly IVideoRepository _videorepository;

        public RecyclerService(
            IRecyclerRepository repository,
            IVideoRepository videorepository
            )
        {
            _repository = repository;
            _videorepository = videorepository;
        }

        public void RemoveVideo(int days)
        {
            IList<Video> videos = _videorepository.ListVideoForDays(days);
            _videorepository.RemoveAllList(videos);
        }

        public string StatusRunning()
        => _repository.StatusRunning();

        public void UpdateRunning()
        {
            Recycler recycler = _repository.Get();
            if (recycler != null)
            {
                recycler.IsRunning = !recycler.IsRunning;
                _repository.Update(recycler);
            }

        }
    }
}
