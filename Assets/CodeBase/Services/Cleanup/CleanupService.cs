using CodeBase.Data;
using CodeBase.Services.LogicFactory;
using CodeBase.Services.PersistentProgress;
using CodeBase.Services.StaticData;

namespace CodeBase.Services.Cleanup
{
    public class CleanupService : ICleanupService
    {
        private readonly ILogicFactory _logicFactory;
        private readonly IPersistentProgressService _progressService;
        private readonly IStaticDataService _dataService;

        public CleanupService(ILogicFactory logicFactory, IPersistentProgressService progressService, IStaticDataService dataService)
        {
            _logicFactory = logicFactory;
            _progressService = progressService;
            _dataService = dataService;
        }

        public void Cleanup()
        {
            _logicFactory.Cleanup();

            _progressService.PlayerProgress = new PlayerProgress(_dataService.PlayerData.StartHealth);
        }
    }
}