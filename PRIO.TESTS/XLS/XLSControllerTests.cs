using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRIO.Controllers;
using PRIO.Data;
using PRIO.DTOS.ClusterDTOS;
using PRIO.DTOS.CompletionDTOS;
using PRIO.DTOS.FieldDTOS;
using PRIO.DTOS.InstallationDTOS;
using PRIO.DTOS.ReservoirDTOS;
using PRIO.DTOS.UserDTOS;
using PRIO.DTOS.WellDTOS;
using PRIO.DTOS.XLSDTOS;
using PRIO.DTOS.ZoneDTOS;
using PRIO.Models.Clusters;
using PRIO.Models.Completions;
using PRIO.Models.Fields;
using PRIO.Models.Installations;
using PRIO.Models.Reservoirs;
using PRIO.Models.Users;
using PRIO.Models.Wells;
using PRIO.Models.Zones;
using PRIO.ViewModels.Files;
namespace PRIO.TESTS.XLS
{
    [TestFixture]
    public class XLSControllerTests
    {
        private XlsxController _controller;
        private IMapper _mapper;
        private DataContext _context;
        private readonly string _pathXLS = "C:\\Users\\gabri\\source\\repos\\PrioANP\\backend\\PRIO\\PRIO.TESTS\\XLS\\base64Mock.txt";
        private RequestXslxViewModel _viewModel;
        [SetUp]
        public void Setup()
        {
            var contextOptions = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new DataContext(contextOptions);

            #region mapper config
            var mapperConfig = new MapperConfiguration(cfg =>
            {

                cfg.CreateMap<Cluster, ClusterDTO>();
                cfg.CreateMap<ClusterHistory, ClusterHistoryDTO>();

                cfg.CreateMap<Installation, InstallationDTO>();
                cfg.CreateMap<InstallationHistory, InstallationHistoryDTO>();

                cfg.CreateMap<Field, FieldDTO>();
                cfg.CreateMap<FieldHistory, FieldHistoryDTO>();

                cfg.CreateMap<Zone, ZoneDTO>();
                cfg.CreateMap<ZoneHistory, ZoneHistoryDTO>();

                cfg.CreateMap<Reservoir, ReservoirDTO>();
                cfg.CreateMap<ReservoirHistory, ReservoirHistoryDTO>();

                cfg.CreateMap<Well, WellDTO>();
                cfg.CreateMap<WellHistory, WellHistoryDTO>();

                cfg.CreateMap<Completion, CompletionDTO>();
                cfg.CreateMap<CompletionHistory, CompletionHistoryDTO>();
                cfg.CreateMap<User, UserDTO>();

            });
            #endregion

            _mapper = mapperConfig.CreateMapper();
            var existingUserId = Guid.NewGuid();
            var existingUser = new User
            {
                Id = existingUserId,
                Name = "garcia",
                Email = "garcia@mail.com",
                Password = "1234",
                Username = "asdsad",
                Type = "admin"
            };

            _context.Users.Add(existingUser);
            _context.SaveChanges();

            var httpContext = new DefaultHttpContext();
            httpContext.Items["Id"] = existingUserId;

            _controller = new XlsxController();
            _controller.ControllerContext.HttpContext = httpContext;
            var pathXls = "C:\\Users\\gabri\\source\\repos\\PrioANP\\backend\\PRIO\\PRIO.TESTS\\XLS\\base64Mock.txt";
            _viewModel = new RequestXslxViewModel
            {
                ContentBase64 = File.ReadAllText(_pathXLS)
            };
        }

        [TearDown]
        public void TearDown()
        {
            _context.Dispose();
        }

        [Test]
        public async Task FilesReturnCreatedAndMessageWhenSuccesfully()
        {
            var result = await _controller.PostBase64File(_viewModel, _context);

            Assert.IsInstanceOf<CreatedResult>(result);
            var createdResult = (CreatedResult)result;
            Assert.That(createdResult.Location, Is.EqualTo("xlsx"));
            Assert.That(((ImportResponseDTO)createdResult.Value).Message, Is.EqualTo("File imported successfully"));
        }

        [Test]
        public async Task CheckIfFilesWereCorrectStoredInBank()
        {
            await _controller.PostBase64File(_viewModel, _context);

            var clustersCount = _context.Clusters.Count();
            var installationsCount = _context.Installations.Count();
            var fieldsCount = _context.Fields.Count();
            var zonesCount = _context.Zones.Count();
            var reservoirsCount = _context.Reservoirs.Count();
            var wellsCount = _context.Wells.Count();
            var completionsCount = _context.Completions.Count();

            Assert.That(clustersCount, Is.EqualTo(Mock._totalClusters));
            Assert.That(installationsCount, Is.EqualTo(Mock._totalInstallations));
            Assert.That(fieldsCount, Is.EqualTo(Mock._totalFields));
            Assert.That(zonesCount, Is.EqualTo(Mock._totalZones));
            Assert.That(reservoirsCount, Is.EqualTo(Mock._totalReservoirs));
            Assert.That(wellsCount, Is.EqualTo(Mock._totalWells));
            Assert.That(completionsCount, Is.EqualTo(Mock._totalCompletions));
        }

        [Test]
        public async Task ClusterSuccesfullyCreated()
        {
            await _controller.PostBase64File(_viewModel, _context);

            var clusters = await _context.Clusters.Include(x => x.User).ToListAsync();

            var forteCluster = clusters.FirstOrDefault(c => c.Name?.ToUpper() == Mock._clusterForte);
            var bravoCluster = clusters.FirstOrDefault(c => c.Name?.ToUpper() == Mock._clusterBravo);
            var valenteCluster = clusters.FirstOrDefault(c => c.Name?.ToUpper() == Mock._clusterValente);

            Assert.That(forteCluster, Is.Not.Null);
            Assert.That(forteCluster.User, Is.Not.Null);
            Assert.That(forteCluster.UepCode?.ToUpper(), Is.EqualTo(Mock._clusterForteUepCode));

            Assert.That(bravoCluster, Is.Not.Null);
            Assert.That(bravoCluster.User, Is.Not.Null);
            Assert.That(bravoCluster.UepCode?.ToUpper(), Is.EqualTo(Mock._clusterBravoUepCode));

            Assert.That(valenteCluster, Is.Not.Null);
            Assert.That(valenteCluster.User, Is.Not.Null);
            Assert.That(valenteCluster.UepCode?.ToUpper(), Is.EqualTo(Mock._clusterValenteUepCode));
        }

        [Test]
        public async Task InstallationSuccesfullyCreated()
        {
            await _controller.PostBase64File(_viewModel, _context);

            var installations = await _context.Installations.Include(x => x.Cluster).Include(x => x.User).ToListAsync();

            var fpsoForte = installations.FirstOrDefault(c => c.Name?.ToUpper() == Mock._installationForte);
            var polvoA = installations.FirstOrDefault(c => c.Name?.ToUpper() == Mock._installationPolvoA);
            var fpsoBravo = installations.FirstOrDefault(c => c.Name?.ToUpper() == Mock._installationBravo);
            var fpsoFrade = installations.FirstOrDefault(c => c.Name?.ToUpper() == Mock._installationFrade);

            Assert.That(fpsoForte, Is.Not.Null);
            Assert.That(fpsoForte.User, Is.Not.Null);
            Assert.That(fpsoForte.Cluster, Is.Not.Null);
            Assert.That(fpsoForte.Cluster.Name?.ToUpper(), Is.EqualTo(Mock._clusterForte));


            Assert.That(polvoA, Is.Not.Null);
            Assert.That(polvoA.User, Is.Not.Null);
            Assert.That(polvoA.Cluster, Is.Not.Null);
            Assert.That(polvoA.Cluster.Name?.ToUpper(), Is.EqualTo(Mock._clusterBravo));

            Assert.That(fpsoBravo, Is.Not.Null);
            Assert.That(fpsoBravo.User, Is.Not.Null);
            Assert.That(fpsoBravo.Cluster, Is.Not.Null);
            Assert.That(fpsoBravo.Cluster.Name?.ToUpper(), Is.EqualTo(Mock._clusterBravo));

            Assert.That(fpsoFrade, Is.Not.Null);
            Assert.That(fpsoFrade.User, Is.Not.Null);
            Assert.That(fpsoFrade.Cluster, Is.Not.Null);
            Assert.That(fpsoFrade.Cluster.Name?.ToUpper(), Is.EqualTo(Mock._clusterValente));
        }

        [Test]
        public async Task FieldsSuccesfullyCreated()
        {
            await _controller.PostBase64File(_viewModel, _context);

            var fields = await _context.Fields.Include(x => x.Installation).Include(x => x.User).ToListAsync();

            var albacoraLeste = fields.FirstOrDefault(c => c.Name?.ToUpper() == Mock._fieldAlbacoraLeste);
            var polvo = fields.FirstOrDefault(c => c.Name?.ToUpper() == Mock._fieldPolvo);
            var tubaraoMartelo = fields.FirstOrDefault(c => c.Name?.ToUpper() == Mock._fieldTubaraoMartelo);
            var frade = fields.FirstOrDefault(c => c.Name?.ToUpper() == Mock._fieldFrade);
            var wahoo = fields.FirstOrDefault(c => c.Name?.ToUpper() == Mock._fieldWahoo);

            Assert.That(albacoraLeste, Is.Not.Null);
            Assert.That(albacoraLeste.User, Is.Not.Null);
            Assert.That(albacoraLeste.Installation, Is.Not.Null);
            Assert.That(albacoraLeste.Installation.Name?.ToUpper(), Is.EqualTo(Mock._installationForte));

            Assert.That(polvo, Is.Not.Null);
            Assert.That(polvo.User, Is.Not.Null);
            Assert.That(polvo.Installation, Is.Not.Null);
            Assert.That(polvo.Installation.Name?.ToUpper(), Is.EqualTo(Mock._installationPolvoA));

            Assert.That(tubaraoMartelo, Is.Not.Null);
            Assert.That(tubaraoMartelo.User, Is.Not.Null);
            Assert.That(tubaraoMartelo.Installation, Is.Not.Null);
            Assert.That(tubaraoMartelo.Installation.Name?.ToUpper(), Is.EqualTo(Mock._installationBravo));

            Assert.That(frade, Is.Not.Null);
            Assert.That(frade.User, Is.Not.Null);
            Assert.That(frade.Installation, Is.Not.Null);
            Assert.That(frade.Installation.Name?.ToUpper(), Is.EqualTo(Mock._installationFrade));

            Assert.That(wahoo, Is.Not.Null);
            Assert.That(wahoo.User, Is.Not.Null);
            Assert.That(wahoo.Installation, Is.Not.Null);
            Assert.That(wahoo.Installation.Name?.ToUpper(), Is.EqualTo(Mock._installationFrade));
        }

        [Test]
        public async Task ZonesSuccesfullyCreated()
        {
            await _controller.PostBase64File(_viewModel, _context);

            var zones = await _context.Zones.Include(x => x.Field).Include(x => x.User).ToListAsync();

            var zone11775 = zones.FirstOrDefault(c => c.CodZone?.ToUpper() == Mock._zoneCodTuroniano11775);
            var zone7890 = zones.FirstOrDefault(c => c.CodZone?.ToUpper() == Mock._zoneCodAB1407890);
            var zone12081 = zones.FirstOrDefault(c => c.CodZone?.ToUpper() == Mock._zoneCod12081);
            var zone12434 = zones.FirstOrDefault(c => c.CodZone?.ToUpper() == Mock._zoneCod12434);

            Assert.That(zone11775, Is.Not.Null);
            Assert.That(zone11775.User, Is.Not.Null);
            Assert.That(zone11775.Field, Is.Not.Null);
            Assert.That(zone11775.Field.Name?.ToUpper(), Is.EqualTo(Mock._fieldPolvo));

            Assert.That(zone7890, Is.Not.Null);
            Assert.That(zone7890.User, Is.Not.Null);
            Assert.That(zone7890.Field, Is.Not.Null);
            Assert.That(zone7890.Field.Name?.ToUpper(), Is.EqualTo(Mock._fieldAlbacoraLeste));

            Assert.That(zone12081, Is.Not.Null);
            Assert.That(zone12081.User, Is.Not.Null);
            Assert.That(zone12081.Field, Is.Not.Null);
            Assert.That(zone12081.Field.Name?.ToUpper(), Is.EqualTo(Mock._fieldTubaraoMartelo));

            Assert.That(zone12434, Is.Not.Null);
            Assert.That(zone12434.User, Is.Not.Null);
            Assert.That(zone12434.Field, Is.Not.Null);
            Assert.That(zone12434.Field.Name?.ToUpper(), Is.EqualTo(Mock._fieldPolvo));
        }

        [Test]
        public async Task ReservoirsSuccesfullyCreated()
        {
            await _controller.PostBase64File(_viewModel, _context);

            var reservoirs = await _context.Reservoirs.Include(x => x.Zone).Include(x => x.User).ToListAsync();

            var reservoirAB140 = reservoirs.FirstOrDefault(c => c.Name?.ToUpper() == Mock._reservoirAB140);
            var reservoirTuroniano = reservoirs.FirstOrDefault(c => c.Name?.ToUpper() == Mock._reservoirTuroniano);
            var reservoirN545D = reservoirs.FirstOrDefault(c => c.Name?.ToUpper() == Mock._reservoirN545D);
            var reservoirN570U = reservoirs.FirstOrDefault(c => c.Name?.ToUpper() == Mock._reservoirN570U);

            Assert.That(reservoirAB140, Is.Not.Null);
            Assert.That(reservoirAB140.User, Is.Not.Null);
            Assert.That(reservoirAB140.Zone, Is.Not.Null);
            Assert.That(reservoirAB140.Zone.CodZone?.ToUpper(), Is.EqualTo(Mock._zoneCodAB1407890));

            Assert.That(reservoirTuroniano, Is.Not.Null);
            Assert.That(reservoirTuroniano.User, Is.Not.Null);
            Assert.That(reservoirTuroniano.Zone, Is.Not.Null);
            Assert.That(reservoirTuroniano.Zone.CodZone?.ToUpper(), Is.EqualTo(Mock._zoneCodTuroniano11775));

            Assert.That(reservoirN545D, Is.Not.Null);
            Assert.That(reservoirN545D.User, Is.Not.Null);
            Assert.That(reservoirN545D.Zone, Is.Not.Null);
            Assert.That(reservoirN545D.Zone.CodZone?.ToUpper(), Is.EqualTo(Mock._zoneCod545D9121));

            Assert.That(reservoirN570U, Is.Not.Null);
            Assert.That(reservoirN570U.User, Is.Not.Null);
            Assert.That(reservoirN570U.Zone, Is.Not.Null);
            Assert.That(reservoirN570U.Zone.CodZone?.ToUpper(), Is.EqualTo(Mock._zoneCodN570U9123));
        }

        [Test]
        public async Task WellsSuccesfullyCreated()
        {
            await _controller.PostBase64File(_viewModel, _context);

            var wells = await _context.Wells.Include(x => x.Field).Include(x => x.User).ToListAsync();

            var well74281026496 = wells.FirstOrDefault(c => c.CodWellAnp?.ToUpper() == Mock._well74281026496);
            var well74281026537 = wells.FirstOrDefault(c => c.CodWellAnp?.ToUpper() == Mock._well74281026537);
            var well74281026753 = wells.FirstOrDefault(c => c.CodWellAnp?.ToUpper() == Mock._well74281026753);
            var well74281029234 = wells.FirstOrDefault(c => c.CodWellAnp?.ToUpper() == Mock._well74281029234);

            Assert.That(well74281026496, Is.Not.Null);
            Assert.That(well74281026496.User, Is.Not.Null);
            Assert.That(well74281026496.Field, Is.Not.Null);
            Assert.That(well74281026496.Field.Name?.ToUpper(), Is.EqualTo(Mock._fieldTubaraoMartelo));

            Assert.That(well74281026537, Is.Not.Null);
            Assert.That(well74281026537.User, Is.Not.Null);
            Assert.That(well74281026537.Field, Is.Not.Null);
            Assert.That(well74281026537.Field.Name?.ToUpper(), Is.EqualTo(Mock._fieldTubaraoMartelo));

            Assert.That(well74281026753, Is.Not.Null);
            Assert.That(well74281026753.User, Is.Not.Null);
            Assert.That(well74281026753.Field, Is.Not.Null);
            Assert.That(well74281026753.Field.Name?.ToUpper(), Is.EqualTo(Mock._fieldTubaraoMartelo));

            Assert.That(well74281029234, Is.Not.Null);
            Assert.That(well74281029234.User, Is.Not.Null);
            Assert.That(well74281029234.Field, Is.Not.Null);
            Assert.That(well74281029234.Field.Name?.ToUpper(), Is.EqualTo(Mock._fieldPolvo));
        }


        [Test]
        public async Task CompletionsSuccesfullyCreated()
        {
            await _controller.PostBase64File(_viewModel, _context);

            var completions = await _context.Completions.Include(x => x.Well).Include(x => x.Reservoir).Include(x => x.User).ToListAsync();

            var completionWellCod74281026753NoReservoir = completions.FirstOrDefault(c => c.Name?.ToUpper() == Mock._completionWellCod74281026753NoReservoir);
            var completionWellCod74281026537 = completions.FirstOrDefault(c => c.Name?.ToUpper() == Mock._completionWellCod74281026537);
            var _completionWellCod74281029209 = completions.FirstOrDefault(c => c.Name?.ToUpper() == Mock._completionWellCod74281026753NoReservoir);
            var completionWellCod74281029234 = completions.FirstOrDefault(c => c.Name?.ToUpper() == Mock._completionWellCod74281029234);

            Assert.That(completionWellCod74281026753NoReservoir, Is.Not.Null);
            Assert.That(completionWellCod74281026753NoReservoir.User, Is.Not.Null);
            Assert.That(completionWellCod74281026753NoReservoir.Well, Is.Not.Null);
            Assert.That(completionWellCod74281026753NoReservoir.Well.CodWellAnp?.ToUpper(), Is.EqualTo(Mock._well74281026753));
            Assert.That(completionWellCod74281026753NoReservoir.Reservoir, Is.Null);

            Assert.That(completionWellCod74281026537, Is.Not.Null);
            Assert.That(completionWellCod74281026537.User, Is.Not.Null);
            Assert.That(completionWellCod74281026537.Well, Is.Not.Null);
            Assert.That(completionWellCod74281026537.Well.CodWellAnp?.ToUpper(), Is.EqualTo(Mock._well74281026537));
            Assert.That(completionWellCod74281026537.Reservoir, Is.Not.Null);
            Assert.That(completionWellCod74281026537.Reservoir.Name?.ToUpper(), Is.EqualTo(Mock._reservoirQuissama_Tmbt));

            Assert.That(_completionWellCod74281029209, Is.Not.Null);
            Assert.That(_completionWellCod74281029209.User, Is.Not.Null);
            Assert.That(_completionWellCod74281029209.Well, Is.Not.Null);
            Assert.That(_completionWellCod74281029209.Well.CodWellAnp?.ToUpper(), Is.EqualTo(Mock._well74281029209));
            Assert.That(_completionWellCod74281029209.Reservoir, Is.Not.Null);
            Assert.That(_completionWellCod74281029209.Reservoir.Name?.ToUpper(), Is.EqualTo(Mock._reservoirQuissama_Pol));

            Assert.That(completionWellCod74281029234, Is.Not.Null);
            Assert.That(completionWellCod74281029234.User, Is.Not.Null);
            Assert.That(completionWellCod74281029234.Well, Is.Not.Null);
            Assert.That(completionWellCod74281029234.Well.CodWellAnp?.ToUpper(), Is.EqualTo(Mock._well74281029234));
            Assert.That(completionWellCod74281029234.Reservoir, Is.Not.Null);
            Assert.That(completionWellCod74281029234.Reservoir.Name?.ToUpper(), Is.EqualTo(Mock._reservoirTuroniano));
        }
    }
}