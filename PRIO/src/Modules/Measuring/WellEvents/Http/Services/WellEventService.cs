using PRIO.src.Modules.Measuring.WellEvents.Interfaces;
using PRIO.src.Modules.Measuring.WellEvents.ViewModels;

namespace PRIO.src.Modules.Measuring.WellEvents.Http.Services
{
    public class WellEventService
    {
        private readonly IWellEventRepository _wellEventRepository;
        public WellEventService(IWellEventRepository wellEventRepository)
        {

            _wellEventRepository = wellEventRepository;
        }
        public async Task CreateEvent(CreateClosingEventViewModel body)
        {

        }
    }
}
