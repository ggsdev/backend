using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRIO.src.Modules.ControlAccess.Users.Infra.EF.Models;
using PRIO.src.Modules.FileImport.XLSX.Dtos;
using PRIO.src.Modules.FileImport.XLSX.Infra.Http.Controllers;
using PRIO.src.Modules.FileImport.XLSX.ViewModels;
using PRIO.src.Shared.Infra.EF;

namespace PRIO.TESTS.Files.XLS
{
    [TestFixture]
    internal class XLSControllerTests
    {
        private XLSXController _controller;
        private DataContext _context;
        private string _pathXLS;
        private RequestXslxViewModel _viewModel;
        private User _user;
        private IMapper _mapper;

        [SetUp]
        public void Setup()
        {
            var contextOptions = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            _context = new DataContext(contextOptions);

            var projectRoot = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\.."));
            var relativePath = Path.Combine("Files", "XLS", "base64Mock.txt");
            _pathXLS = Path.GetFullPath(Path.Combine(projectRoot, relativePath));

            var mapperConfig = new MapperConfiguration(cfg =>
            {
            });
            _mapper = mapperConfig.CreateMapper();

            _user = new User()
            {
                Name = "userTeste",
                Email = "userTeste@mail.com",
                Password = "1234",
                Username = "userTeste",
            };
            _context.Users.Add(_user);
            _context.SaveChanges();

            var httpContext = new DefaultHttpContext();
            httpContext.Items["Id"] = _user.Id;
            httpContext.Items["INSTANCE"] = "bravo";
            httpContext.Items["User"] = _user;

            _controller = new XLSXController(_mapper);
            _controller.ControllerContext.HttpContext = httpContext;
            _viewModel = new RequestXslxViewModel
            {
                ContentBase64 = File.ReadAllText(_pathXLS)
            };
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Test]
        public async Task FilesReturnCreatedAndMessageWhenSuccesfully()
        {
            var result = await _controller.PostBase64File(_viewModel, _context);
            var createdResult = (CreatedResult)result;

            Assert.IsInstanceOf<CreatedResult>(result);
            Assert.That(createdResult.Location, Is.EqualTo("xlsx"));
            Assert.That(((ImportResponseDTO)createdResult.Value).Message, Is.EqualTo("File imported successfully"));
        }

        //refatorar contando rows do xls * 2 (historicos)
        //[Test]
        //public async Task CheckIfTotalMatchesIfExcelRows()
        //{
        //    await _controller.PostBase64File(_viewModel, _context);

        //    var clustersAndHistoriesCount = _context.Clusters.Count() + _context.ClustersHistories.Count();
        //    var installationsAndHistoriesCount = _context.Installations.Count() + _context.InstallationHistories.Count();
        //    var fieldsAndHistoriesCount = _context.Fields.Count() + _context.FieldHistories.Count();
        //    var zonesAndHistoriesCount = _context.Zones.Count() + _context.ZoneHistories.Count();
        //    var reservoirsAndHistoriesCount = _context.Reservoirs.Count() + _context.ReservoirHistories.Count();
        //    var wellsAndHistoriesCount = _context.Wells.Count() + _context.WellHistories.Count();
        //    var completionsAndHistoriesCount = _context.Completions.Count() + _context.CompletionHistories.Count();

        //    Assert.That(clustersAndHistoriesCount, Is.EqualTo(Mock._totalClusters));
        //    Assert.That(installationsAndHistoriesCount, Is.EqualTo(Mock._totalInstallations));
        //    Assert.That(fieldsAndHistoriesCount, Is.EqualTo(Mock._totalFields));
        //    Assert.That(zonesAndHistoriesCount, Is.EqualTo(Mock._totalZones));
        //    Assert.That(reservoirsAndHistoriesCount, Is.EqualTo(Mock._totalReservoirs));
        //    Assert.That(wellsAndHistoriesCount, Is.EqualTo(Mock._totalWells));
        //    Assert.That(completionsAndHistoriesCount, Is.EqualTo(Mock._totalCompletions));
        //}

        [Test]
        public async Task ClustersSuccesfullyCreated()
        {
            await _controller.PostBase64File(_viewModel, _context);

            var clusters = await _context.Clusters.Include(x => x.User).ToListAsync();

            var forteCluster = clusters.FirstOrDefault(c => c.Name?.ToUpper() == Mock._clusterForte);
            var bravoCluster = clusters.FirstOrDefault(c => c.Name?.ToUpper() == Mock._clusterBravo);
            var valenteCluster = clusters.FirstOrDefault(c => c.Name?.ToUpper() == Mock._clusterValente);

            Assert.That(forteCluster, Is.Not.Null);
            Assert.That(forteCluster.User, Is.Not.Null);

            Assert.That(bravoCluster, Is.Not.Null);
            Assert.That(bravoCluster.User, Is.Not.Null);

            Assert.That(valenteCluster, Is.Not.Null);
            Assert.That(valenteCluster.User, Is.Not.Null);
        }

        [Test]
        public async Task InstallationsSuccesfullyCreated()
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

            var well74281026496 = wells.FirstOrDefault(c => c.CodWellAnp == Mock._well74281026496.CodWellAnp);

            var well74281026537 = wells.FirstOrDefault(c => c.CodWellAnp == Mock._well74281026537.CodWellAnp);

            var well74281026753 = wells.FirstOrDefault(c => c.CodWellAnp == Mock._well74281026753.CodWellAnp);

            var well74281029234 = wells.FirstOrDefault(c => c.CodWellAnp == Mock._well74281029234.CodWellAnp);

            var well74281029209 = wells.FirstOrDefault(c => c.CodWellAnp == Mock._well74281029209.CodWellAnp);

            var well74281029222 = wells.FirstOrDefault(c => c.CodWellAnp == Mock._well74281029222.CodWellAnp);

            var well74281028110 = wells.FirstOrDefault(c => c.CodWellAnp == Mock._well74281028110.CodWellAnp);

            var well74281029154 = wells.FirstOrDefault(c => c.CodWellAnp == Mock._well74281029154.CodWellAnp);

            var well74281029266 = wells.FirstOrDefault(c => c.CodWellAnp == Mock._well74281029266.CodWellAnp);

            var well742810163700 = wells.FirstOrDefault(c => c.CodWellAnp == Mock._well742810163700.CodWellAnp);

            var well74281026747 = wells.FirstOrDefault(c => c.CodWellAnp == Mock._well74281026747.CodWellAnp);

            Assert.That(well74281026496, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(well74281026496.Name, Is.EqualTo(Mock._well74281026496.Name));
                Assert.That(well74281026496.WellOperatorName, Is.EqualTo(Mock._well74281026496.WellOperatorName));
                Assert.That(well74281026496.CodWellAnp, Is.EqualTo(Mock._well74281026496.CodWellAnp));
                Assert.That(well74281026496.CategoryAnp, Is.EqualTo(Mock._well74281026496.CategoryAnp));
                Assert.That(well74281026496.CategoryReclassificationAnp, Is.EqualTo(Mock._well74281026496.CategoryReclassificationAnp));
                Assert.That(well74281026496.CategoryOperator, Is.EqualTo(Mock._well74281026496.CategoryOperator));
                Assert.That(well74281026496.StatusOperator, Is.EqualTo(Mock._well74281026496.StatusOperator));
                Assert.That(well74281026496.Type, Is.EqualTo(Mock._well74281026496.Type));
                Assert.That(well74281026496.WaterDepth, Is.EqualTo(Mock._well74281026496.WaterDepth));
                Assert.That(well74281026496.TopOfPerforated, Is.EqualTo(Mock._well74281026496.TopOfPerforated));
                Assert.That(well74281026496.BaseOfPerforated, Is.EqualTo(Mock._well74281026496.BaseOfPerforated));
                Assert.That(well74281026496.ArtificialLift, Is.EqualTo(Mock._well74281026496.ArtificialLift));
                Assert.That(well74281026496.Latitude4C, Is.EqualTo(Mock._well74281026496.Latitude4C));
                Assert.That(well74281026496.Longitude4C, Is.EqualTo(Mock._well74281026496.Longitude4C));
                Assert.That(well74281026496.LatitudeDD, Is.EqualTo(Mock._well74281026496.LatitudeDD));
                Assert.That(well74281026496.LongitudeDD, Is.EqualTo(Mock._well74281026496.LongitudeDD));
                Assert.That(well74281026496.DatumHorizontal, Is.EqualTo(Mock._well74281026496.DatumHorizontal));
                Assert.That(well74281026496.TypeBaseCoordinate, Is.EqualTo(Mock._well74281026496.TypeBaseCoordinate));
                Assert.That(well74281026496.CoordX, Is.EqualTo(Mock._well74281026496.CoordX));
                Assert.That(well74281026496.CoordY, Is.EqualTo(Mock._well74281026496.CoordY));

            });
            Assert.That(well74281026496.User, Is.Not.Null);
            Assert.That(well74281026496.User.Name, Is.EqualTo(_user.Name));
            Assert.That(well74281026496.Field, Is.Not.Null);
            Assert.That(well74281026496.Field.Name?.ToUpper(), Is.EqualTo(Mock._fieldTubaraoMartelo));

            Assert.That(well74281026537, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(well74281026537.Name, Is.EqualTo(Mock._well74281026537.Name));
                Assert.That(well74281026537.WellOperatorName, Is.EqualTo(Mock._well74281026537.WellOperatorName));
                Assert.That(well74281026537.CodWellAnp, Is.EqualTo(Mock._well74281026537.CodWellAnp));
                Assert.That(well74281026537.CategoryAnp, Is.EqualTo(Mock._well74281026537.CategoryAnp));
                Assert.That(well74281026537.CategoryReclassificationAnp, Is.EqualTo(Mock._well74281026537.CategoryReclassificationAnp));
                Assert.That(well74281026537.CategoryOperator, Is.EqualTo(Mock._well74281026537.CategoryOperator));
                Assert.That(well74281026537.StatusOperator, Is.EqualTo(Mock._well74281026537.StatusOperator));
                Assert.That(well74281026537.Type, Is.EqualTo(Mock._well74281026537.Type));
                Assert.That(well74281026537.WaterDepth, Is.EqualTo(Mock._well74281026537.WaterDepth));
                Assert.That(well74281026537.TopOfPerforated, Is.EqualTo(Mock._well74281026537.TopOfPerforated));
                Assert.That(well74281026537.BaseOfPerforated, Is.EqualTo(Mock._well74281026537.BaseOfPerforated));
                Assert.That(well74281026537.ArtificialLift, Is.EqualTo(Mock._well74281026537.ArtificialLift));
                Assert.That(well74281026537.Latitude4C, Is.EqualTo(Mock._well74281026537.Latitude4C));
                Assert.That(well74281026537.Longitude4C, Is.EqualTo(Mock._well74281026537.Longitude4C));
                Assert.That(well74281026537.LatitudeDD, Is.EqualTo(Mock._well74281026537.LatitudeDD));
                Assert.That(well74281026537.LongitudeDD, Is.EqualTo(Mock._well74281026537.LongitudeDD));
                Assert.That(well74281026537.DatumHorizontal, Is.EqualTo(Mock._well74281026537.DatumHorizontal));
                Assert.That(well74281026537.TypeBaseCoordinate, Is.EqualTo(Mock._well74281026537.TypeBaseCoordinate));
                Assert.That(well74281026537.CoordX, Is.EqualTo(Mock._well74281026537.CoordX));
                Assert.That(well74281026537.CoordY, Is.EqualTo(Mock._well74281026537.CoordY));

            });
            Assert.That(well74281026537.User, Is.Not.Null);
            Assert.That(well74281026537.User.Name, Is.EqualTo(_user.Name));
            Assert.That(well74281026537.Field, Is.Not.Null);
            Assert.That(well74281026537.Field.Name?.ToUpper(), Is.EqualTo(Mock._fieldTubaraoMartelo));

            Assert.That(well74281026753, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(well74281026753.Name, Is.EqualTo(Mock._well74281026753.Name));
                Assert.That(well74281026753.WellOperatorName, Is.EqualTo(Mock._well74281026753.WellOperatorName));
                Assert.That(well74281026753.CodWellAnp, Is.EqualTo(Mock._well74281026753.CodWellAnp));
                Assert.That(well74281026753.CategoryAnp, Is.EqualTo(Mock._well74281026753.CategoryAnp));
                Assert.That(well74281026753.CategoryReclassificationAnp, Is.EqualTo(Mock._well74281026753.CategoryReclassificationAnp));
                Assert.That(well74281026753.CategoryOperator, Is.EqualTo(Mock._well74281026753.CategoryOperator));
                Assert.That(well74281026753.StatusOperator, Is.EqualTo(Mock._well74281026753.StatusOperator));
                Assert.That(well74281026753.Type, Is.EqualTo(Mock._well74281026753.Type));
                Assert.That(well74281026753.WaterDepth, Is.EqualTo(Mock._well74281026753.WaterDepth));
                Assert.That(well74281026753.TopOfPerforated, Is.EqualTo(Mock._well74281026753.TopOfPerforated));
                Assert.That(well74281026753.BaseOfPerforated, Is.EqualTo(Mock._well74281026753.BaseOfPerforated));
                Assert.That(well74281026753.ArtificialLift, Is.EqualTo(Mock._well74281026753.ArtificialLift));
                Assert.That(well74281026753.Latitude4C, Is.EqualTo(Mock._well74281026753.Latitude4C));
                Assert.That(well74281026753.Longitude4C, Is.EqualTo(Mock._well74281026753.Longitude4C));
                Assert.That(well74281026753.LatitudeDD, Is.EqualTo(Mock._well74281026753.LatitudeDD));
                Assert.That(well74281026753.LongitudeDD, Is.EqualTo(Mock._well74281026753.LongitudeDD));
                Assert.That(well74281026753.DatumHorizontal, Is.EqualTo(Mock._well74281026753.DatumHorizontal));
                Assert.That(well74281026753.TypeBaseCoordinate, Is.EqualTo(Mock._well74281026753.TypeBaseCoordinate));
                Assert.That(well74281026753.CoordX, Is.EqualTo(Mock._well74281026753.CoordX));
                Assert.That(well74281026753.CoordY, Is.EqualTo(Mock._well74281026753.CoordY));
            });
            Assert.That(well74281026753.User, Is.Not.Null);
            Assert.That(well74281026753.User.Name, Is.EqualTo(_user.Name));
            Assert.That(well74281026753.Field, Is.Not.Null);
            Assert.That(well74281026753.Field.Name?.ToUpper(), Is.EqualTo(Mock._fieldTubaraoMartelo));

            Assert.That(well74281029234, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(well74281029234.Name, Is.EqualTo(Mock._well74281029234.Name));
                Assert.That(well74281029234.WellOperatorName, Is.EqualTo(Mock._well74281029234.WellOperatorName));
                Assert.That(well74281029234.CodWellAnp, Is.EqualTo(Mock._well74281029234.CodWellAnp));
                Assert.That(well74281029234.CategoryAnp, Is.EqualTo(Mock._well74281029234.CategoryAnp));
                Assert.That(well74281029234.CategoryReclassificationAnp, Is.EqualTo(Mock._well74281029234.CategoryReclassificationAnp));
                Assert.That(well74281029234.CategoryOperator, Is.EqualTo(Mock._well74281029234.CategoryOperator));
                Assert.That(well74281029234.StatusOperator, Is.EqualTo(Mock._well74281029234.StatusOperator));
                Assert.That(well74281029234.Type, Is.EqualTo(Mock._well74281029234.Type));
                Assert.That(well74281029234.WaterDepth, Is.EqualTo(Mock._well74281029234.WaterDepth));
                Assert.That(well74281029234.TopOfPerforated, Is.EqualTo(Mock._well74281029234.TopOfPerforated));
                Assert.That(well74281029234.BaseOfPerforated, Is.EqualTo(Mock._well74281029234.BaseOfPerforated));
                Assert.That(well74281029234.ArtificialLift, Is.EqualTo(Mock._well74281029234.ArtificialLift));
                Assert.That(well74281029234.Latitude4C, Is.EqualTo(Mock._well74281029234.Latitude4C));
                Assert.That(well74281029234.Longitude4C, Is.EqualTo(Mock._well74281029234.Longitude4C));
                Assert.That(well74281029234.LatitudeDD, Is.EqualTo(Mock._well74281029234.LatitudeDD));
                Assert.That(well74281029234.LongitudeDD, Is.EqualTo(Mock._well74281029234.LongitudeDD));
                Assert.That(well74281029234.DatumHorizontal, Is.EqualTo(Mock._well74281029234.DatumHorizontal));
                Assert.That(well74281029234.TypeBaseCoordinate, Is.EqualTo(Mock._well74281029234.TypeBaseCoordinate));
                Assert.That(well74281029234.CoordX, Is.EqualTo(Mock._well74281029234.CoordX));
                Assert.That(well74281029234.CoordY, Is.EqualTo(Mock._well74281029234.CoordY));

            });
            Assert.That(well74281029234.User, Is.Not.Null);
            Assert.That(well74281029234.User.Name, Is.EqualTo(_user.Name));
            Assert.That(well74281029234.Field, Is.Not.Null);
            Assert.That(well74281029234.Field.Name?.ToUpper(), Is.EqualTo(Mock._fieldPolvo));

            Assert.That(well74281029209, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(well74281029209.Name, Is.EqualTo(Mock._well74281029209.Name));
                Assert.That(well74281029209.WellOperatorName, Is.EqualTo(Mock._well74281029209.WellOperatorName));
                Assert.That(well74281029209.CodWellAnp, Is.EqualTo(Mock._well74281029209.CodWellAnp));
                Assert.That(well74281029209.CategoryAnp, Is.EqualTo(Mock._well74281029209.CategoryAnp));
                Assert.That(well74281029209.CategoryReclassificationAnp, Is.EqualTo(Mock._well74281029209.CategoryReclassificationAnp));
                Assert.That(well74281029209.CategoryOperator, Is.EqualTo(Mock._well74281029209.CategoryOperator));
                Assert.That(well74281029209.StatusOperator, Is.EqualTo(Mock._well74281029209.StatusOperator));
                Assert.That(well74281029209.Type, Is.EqualTo(Mock._well74281029209.Type));
                Assert.That(well74281029209.WaterDepth, Is.EqualTo(Mock._well74281029209.WaterDepth));
                Assert.That(well74281029209.TopOfPerforated, Is.EqualTo(Mock._well74281029209.TopOfPerforated));
                Assert.That(well74281029209.BaseOfPerforated, Is.EqualTo(Mock._well74281029209.BaseOfPerforated));
                Assert.That(well74281029209.ArtificialLift, Is.EqualTo(Mock._well74281029209.ArtificialLift));
                Assert.That(well74281029209.Latitude4C, Is.EqualTo(Mock._well74281029209.Latitude4C));
                Assert.That(well74281029209.Longitude4C, Is.EqualTo(Mock._well74281029209.Longitude4C));
                Assert.That(well74281029209.LatitudeDD, Is.EqualTo(Mock._well74281029209.LatitudeDD));
                Assert.That(well74281029209.LongitudeDD, Is.EqualTo(Mock._well74281029209.LongitudeDD));
                Assert.That(well74281029209.DatumHorizontal, Is.EqualTo(Mock._well74281029209.DatumHorizontal));
                Assert.That(well74281029209.TypeBaseCoordinate, Is.EqualTo(Mock._well74281029209.TypeBaseCoordinate));
                Assert.That(well74281029209.CoordX, Is.EqualTo(Mock._well74281029209.CoordX));
                Assert.That(well74281029209.CoordY, Is.EqualTo(Mock._well74281029209.CoordY));
            });
            Assert.That(well74281029209.User, Is.Not.Null);
            Assert.That(well74281029209.User.Name, Is.EqualTo(_user.Name));
            Assert.That(well74281029209.Field, Is.Not.Null);
            Assert.That(well74281029209.Field.Name?.ToUpper(), Is.EqualTo(Mock._fieldPolvo));

            Assert.That(well74281029222, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(well74281029222.Name, Is.EqualTo(Mock._well74281029222.Name));
                Assert.That(well74281029222.WellOperatorName, Is.EqualTo(Mock._well74281029222.WellOperatorName));
                Assert.That(well74281029222.CodWellAnp, Is.EqualTo(Mock._well74281029222.CodWellAnp));
                Assert.That(well74281029222.CategoryAnp, Is.EqualTo(Mock._well74281029222.CategoryAnp));
                Assert.That(well74281029222.CategoryReclassificationAnp, Is.EqualTo(Mock._well74281029222.CategoryReclassificationAnp));
                Assert.That(well74281029222.CategoryOperator, Is.EqualTo(Mock._well74281029222.CategoryOperator));
                Assert.That(well74281029222.StatusOperator, Is.EqualTo(Mock._well74281029222.StatusOperator));
                Assert.That(well74281029222.Type, Is.EqualTo(Mock._well74281029222.Type));
                Assert.That(well74281029222.WaterDepth, Is.EqualTo(Mock._well74281029222.WaterDepth));
                Assert.That(well74281029222.TopOfPerforated, Is.EqualTo(Mock._well74281029222.TopOfPerforated));
                Assert.That(well74281029222.BaseOfPerforated, Is.EqualTo(Mock._well74281029222.BaseOfPerforated));
                Assert.That(well74281029222.ArtificialLift, Is.EqualTo(Mock._well74281029222.ArtificialLift));
                Assert.That(well74281029222.Latitude4C, Is.EqualTo(Mock._well74281029222.Latitude4C));
                Assert.That(well74281029222.Longitude4C, Is.EqualTo(Mock._well74281029222.Longitude4C));
                Assert.That(well74281029222.LatitudeDD, Is.EqualTo(Mock._well74281029222.LatitudeDD));
                Assert.That(well74281029222.LongitudeDD, Is.EqualTo(Mock._well74281029222.LongitudeDD));
                Assert.That(well74281029222.DatumHorizontal, Is.EqualTo(Mock._well74281029222.DatumHorizontal));
                Assert.That(well74281029222.TypeBaseCoordinate, Is.EqualTo(Mock._well74281029222.TypeBaseCoordinate));
                Assert.That(well74281029222.CoordX, Is.EqualTo(Mock._well74281029222.CoordX));
                Assert.That(well74281029222.CoordY, Is.EqualTo(Mock._well74281029222.CoordY));
            });
            Assert.That(well74281029222.User, Is.Not.Null);
            Assert.That(well74281029222.User.Name, Is.EqualTo(_user.Name));
            Assert.That(well74281029222.Field, Is.Not.Null);
            Assert.That(well74281029222.Field.Name?.ToUpper(), Is.EqualTo(Mock._fieldPolvo));

            Assert.That(well74281028110, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(well74281028110.Name, Is.EqualTo(Mock._well74281028110.Name));
                Assert.That(well74281028110.WellOperatorName, Is.EqualTo(Mock._well74281028110.WellOperatorName));
                Assert.That(well74281028110.CodWellAnp, Is.EqualTo(Mock._well74281028110.CodWellAnp));
                Assert.That(well74281028110.CategoryAnp, Is.EqualTo(Mock._well74281028110.CategoryAnp));
                Assert.That(well74281028110.CategoryReclassificationAnp, Is.EqualTo(Mock._well74281028110.CategoryReclassificationAnp));
                Assert.That(well74281028110.CategoryOperator, Is.EqualTo(Mock._well74281028110.CategoryOperator));
                Assert.That(well74281028110.StatusOperator, Is.EqualTo(Mock._well74281028110.StatusOperator));
                Assert.That(well74281028110.Type, Is.EqualTo(Mock._well74281028110.Type));
                Assert.That(well74281028110.WaterDepth, Is.EqualTo(Mock._well74281028110.WaterDepth));
                Assert.That(well74281028110.TopOfPerforated, Is.EqualTo(Mock._well74281028110.TopOfPerforated));
                Assert.That(well74281028110.BaseOfPerforated, Is.EqualTo(Mock._well74281028110.BaseOfPerforated));
                Assert.That(well74281028110.ArtificialLift, Is.EqualTo(Mock._well74281028110.ArtificialLift));
                Assert.That(well74281028110.Latitude4C, Is.EqualTo(Mock._well74281028110.Latitude4C));
                Assert.That(well74281028110.Longitude4C, Is.EqualTo(Mock._well74281028110.Longitude4C));
                Assert.That(well74281028110.LatitudeDD, Is.EqualTo(Mock._well74281028110.LatitudeDD));
                Assert.That(well74281028110.LongitudeDD, Is.EqualTo(Mock._well74281028110.LongitudeDD));
                Assert.That(well74281028110.DatumHorizontal, Is.EqualTo(Mock._well74281028110.DatumHorizontal));
                Assert.That(well74281028110.TypeBaseCoordinate, Is.EqualTo(Mock._well74281028110.TypeBaseCoordinate));
                Assert.That(well74281028110.CoordX, Is.EqualTo(Mock._well74281028110.CoordX));
                Assert.That(well74281028110.CoordY, Is.EqualTo(Mock._well74281028110.CoordY));
            });
            Assert.That(well74281028110.User, Is.Not.Null);
            Assert.That(well74281028110.User.Name, Is.EqualTo(_user.Name));
            Assert.That(well74281028110.Field, Is.Not.Null);
            Assert.That(well74281028110.Field.Name?.ToUpper(), Is.EqualTo(Mock._fieldAlbacoraLeste));

            Assert.That(well74281029154, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(well74281029154.Name, Is.EqualTo(Mock._well74281029154.Name));
                Assert.That(well74281029154.WellOperatorName, Is.EqualTo(Mock._well74281029154.WellOperatorName));
                Assert.That(well74281029154.CodWellAnp, Is.EqualTo(Mock._well74281029154.CodWellAnp));
                Assert.That(well74281029154.CategoryAnp, Is.EqualTo(Mock._well74281029154.CategoryAnp));
                Assert.That(well74281029154.CategoryReclassificationAnp, Is.EqualTo(Mock._well74281029154.CategoryReclassificationAnp));
                Assert.That(well74281029154.CategoryOperator, Is.EqualTo(Mock._well74281029154.CategoryOperator));
                Assert.That(well74281029154.StatusOperator, Is.EqualTo(Mock._well74281029154.StatusOperator));
                Assert.That(well74281029154.Type, Is.EqualTo(Mock._well74281029154.Type));
                Assert.That(well74281029154.WaterDepth, Is.EqualTo(Mock._well74281029154.WaterDepth));
                Assert.That(well74281029154.TopOfPerforated, Is.EqualTo(Mock._well74281029154.TopOfPerforated));
                Assert.That(well74281029154.BaseOfPerforated, Is.EqualTo(Mock._well74281029154.BaseOfPerforated));
                Assert.That(well74281029154.ArtificialLift, Is.EqualTo(Mock._well74281029154.ArtificialLift));
                Assert.That(well74281029154.Latitude4C, Is.EqualTo(Mock._well74281029154.Latitude4C));
                Assert.That(well74281029154.Longitude4C, Is.EqualTo(Mock._well74281029154.Longitude4C));
                Assert.That(well74281029154.LatitudeDD, Is.EqualTo(Mock._well74281029154.LatitudeDD));
                Assert.That(well74281029154.LongitudeDD, Is.EqualTo(Mock._well74281029154.LongitudeDD));
                Assert.That(well74281029154.DatumHorizontal, Is.EqualTo(Mock._well74281029154.DatumHorizontal));
                Assert.That(well74281029154.TypeBaseCoordinate, Is.EqualTo(Mock._well74281029154.TypeBaseCoordinate));
                Assert.That(well74281029154.CoordX, Is.EqualTo(Mock._well74281029154.CoordX));
                Assert.That(well74281029154.CoordY, Is.EqualTo(Mock._well74281029154.CoordY));

            });
            Assert.That(well74281029154.User, Is.Not.Null);
            Assert.That(well74281029154.User.Name, Is.EqualTo(_user.Name));
            Assert.That(well74281029154.Field, Is.Not.Null);
            Assert.That(well74281029154.Field.Name?.ToUpper(), Is.EqualTo(Mock._fieldAlbacoraLeste));

            Assert.That(well74281029266, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(well74281029266.Name, Is.EqualTo(Mock._well74281029266.Name));
                Assert.That(well74281029266.WellOperatorName, Is.EqualTo(Mock._well74281029266.WellOperatorName));
                Assert.That(well74281029266.CodWellAnp, Is.EqualTo(Mock._well74281029266.CodWellAnp));
                Assert.That(well74281029266.CategoryAnp, Is.EqualTo(Mock._well74281029266.CategoryAnp));
                Assert.That(well74281029266.CategoryReclassificationAnp, Is.EqualTo(Mock._well74281029266.CategoryReclassificationAnp));
                Assert.That(well74281029266.CategoryOperator, Is.EqualTo(Mock._well74281029266.CategoryOperator));
                Assert.That(well74281029266.StatusOperator, Is.EqualTo(Mock._well74281029266.StatusOperator));
                Assert.That(well74281029266.Type, Is.EqualTo(Mock._well74281029266.Type));
                Assert.That(well74281029266.WaterDepth, Is.EqualTo(Mock._well74281029266.WaterDepth));
                Assert.That(well74281029266.TopOfPerforated, Is.EqualTo(Mock._well74281029266.TopOfPerforated));
                Assert.That(well74281029266.BaseOfPerforated, Is.EqualTo(Mock._well74281029266.BaseOfPerforated));
                Assert.That(well74281029266.ArtificialLift, Is.EqualTo(Mock._well74281029266.ArtificialLift));
                Assert.That(well74281029266.Latitude4C, Is.EqualTo(Mock._well74281029266.Latitude4C));
                Assert.That(well74281029266.Longitude4C, Is.EqualTo(Mock._well74281029266.Longitude4C));
                Assert.That(well74281029266.LatitudeDD, Is.EqualTo(Mock._well74281029266.LatitudeDD));
                Assert.That(well74281029266.LongitudeDD, Is.EqualTo(Mock._well74281029266.LongitudeDD));
                Assert.That(well74281029266.DatumHorizontal, Is.EqualTo(Mock._well74281029266.DatumHorizontal));
                Assert.That(well74281029266.TypeBaseCoordinate, Is.EqualTo(Mock._well74281029266.TypeBaseCoordinate));
                Assert.That(well74281029266.CoordX, Is.EqualTo(Mock._well74281029266.CoordX));
                Assert.That(well74281029266.CoordY, Is.EqualTo(Mock._well74281029266.CoordY));
            });
            Assert.That(well74281029266.User, Is.Not.Null);
            Assert.That(well74281029266.User.Name, Is.EqualTo(_user.Name));
            Assert.That(well74281029266.Field, Is.Not.Null);
            Assert.That(well74281029266.Field.Name?.ToUpper(), Is.EqualTo(Mock._fieldAlbacoraLeste));

            Assert.That(well742810163700, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(well742810163700.Name, Is.EqualTo(Mock._well742810163700.Name));
                Assert.That(well742810163700.WellOperatorName, Is.EqualTo(Mock._well742810163700.WellOperatorName));
                Assert.That(well742810163700.CodWellAnp, Is.EqualTo(Mock._well742810163700.CodWellAnp));
                Assert.That(well742810163700.CategoryAnp, Is.EqualTo(Mock._well742810163700.CategoryAnp));
                Assert.That(well742810163700.CategoryReclassificationAnp, Is.EqualTo(Mock._well742810163700.CategoryReclassificationAnp));
                Assert.That(well742810163700.CategoryOperator, Is.EqualTo(Mock._well742810163700.CategoryOperator));
                Assert.That(well742810163700.StatusOperator, Is.EqualTo(Mock._well742810163700.StatusOperator));
                Assert.That(well742810163700.Type, Is.EqualTo(Mock._well742810163700.Type));
                Assert.That(well742810163700.WaterDepth, Is.EqualTo(Mock._well742810163700.WaterDepth));
                Assert.That(well742810163700.TopOfPerforated, Is.EqualTo(Mock._well742810163700.TopOfPerforated));
                Assert.That(well742810163700.BaseOfPerforated, Is.EqualTo(Mock._well742810163700.BaseOfPerforated));
                Assert.That(well742810163700.ArtificialLift, Is.EqualTo(Mock._well742810163700.ArtificialLift));
                Assert.That(well742810163700.Latitude4C, Is.EqualTo(Mock._well742810163700.Latitude4C));
                Assert.That(well742810163700.Longitude4C, Is.EqualTo(Mock._well742810163700.Longitude4C));
                Assert.That(well742810163700.LatitudeDD, Is.EqualTo(Mock._well742810163700.LatitudeDD));
                Assert.That(well742810163700.LongitudeDD, Is.EqualTo(Mock._well742810163700.LongitudeDD));
                Assert.That(well742810163700.DatumHorizontal, Is.EqualTo(Mock._well742810163700.DatumHorizontal));
                Assert.That(well742810163700.TypeBaseCoordinate, Is.EqualTo(Mock._well742810163700.TypeBaseCoordinate));
                Assert.That(well742810163700.CoordX, Is.EqualTo(Mock._well742810163700.CoordX));
                Assert.That(well742810163700.CoordY, Is.EqualTo(Mock._well742810163700.CoordY));
            });
            Assert.That(well742810163700.User, Is.Not.Null);
            Assert.That(well742810163700.User.Name, Is.EqualTo(_user.Name));
            Assert.That(well742810163700.Field, Is.Not.Null);
            Assert.That(well742810163700.Field.Name?.ToUpper(), Is.EqualTo(Mock._fieldFrade));

            Assert.That(well74281026747, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(well74281026747.Name, Is.EqualTo(Mock._well74281026747.Name));
                Assert.That(well74281026747.WellOperatorName, Is.EqualTo(Mock._well74281026747.WellOperatorName));
                Assert.That(well74281026747.CodWellAnp, Is.EqualTo(Mock._well74281026747.CodWellAnp));
                Assert.That(well74281026747.CategoryAnp, Is.EqualTo(Mock._well74281026747.CategoryAnp));
                Assert.That(well74281026747.CategoryReclassificationAnp, Is.EqualTo(Mock._well74281026747.CategoryReclassificationAnp));
                Assert.That(well74281026747.CategoryOperator, Is.EqualTo(Mock._well74281026747.CategoryOperator));
                Assert.That(well74281026747.StatusOperator, Is.EqualTo(Mock._well74281026747.StatusOperator));
                Assert.That(well74281026747.Type, Is.EqualTo(Mock._well74281026747.Type));
                Assert.That(well74281026747.WaterDepth, Is.EqualTo(Mock._well74281026747.WaterDepth));
                Assert.That(well74281026747.TopOfPerforated, Is.EqualTo(Mock._well74281026747.TopOfPerforated));
                Assert.That(well74281026747.BaseOfPerforated, Is.EqualTo(Mock._well74281026747.BaseOfPerforated));
                Assert.That(well74281026747.ArtificialLift, Is.EqualTo(Mock._well74281026747.ArtificialLift));
                Assert.That(well74281026747.Latitude4C, Is.EqualTo(Mock._well74281026747.Latitude4C));
                Assert.That(well74281026747.Longitude4C, Is.EqualTo(Mock._well74281026747.Longitude4C));
                Assert.That(well74281026747.LatitudeDD, Is.EqualTo(Mock._well74281026747.LatitudeDD));
                Assert.That(well74281026747.LongitudeDD, Is.EqualTo(Mock._well74281026747.LongitudeDD));
                Assert.That(well74281026747.DatumHorizontal, Is.EqualTo(Mock._well74281026747.DatumHorizontal));
                Assert.That(well74281026747.TypeBaseCoordinate, Is.EqualTo(Mock._well74281026747.TypeBaseCoordinate));
                Assert.That(well74281026747.CoordX, Is.EqualTo(Mock._well74281026747.CoordX));
                Assert.That(well74281026747.CoordY, Is.EqualTo(Mock._well74281026747.CoordY));
            });
            Assert.That(well74281026747.User, Is.Not.Null);
            Assert.That(well74281026747.User.Name, Is.EqualTo(_user.Name));
            Assert.That(well74281026747.Field, Is.Not.Null);
            Assert.That(well74281026747.Field.Name?.ToUpper(), Is.EqualTo(Mock._fieldTubaraoMartelo));
        }

        [Test]
        public async Task CompletionsSuccesfullyCreated()
        {
            await _controller.PostBase64File(_viewModel, _context);

            var completions = await _context.Completions.Include(x => x.Well).Include(x => x.Reservoir).Include(x => x.User).ToListAsync();

            var completionWellCod74281026753NoReservoir = completions.FirstOrDefault(c => c.Name?.ToUpper() == Mock._completionWellCod74281026753NoReservoir);
            var completionWellCod74281026537 = completions.FirstOrDefault(c => c.Name?.ToUpper() == Mock._completionWellCod74281026537);
            var _completionWellCod74281029209 = completions.FirstOrDefault(c => c.Name?.ToUpper() == Mock._completionWellCod74281029209);
            var completionWellCod74281029234 = completions.FirstOrDefault(c => c.Name?.ToUpper() == Mock._completionWellCod74281029234);
            var completionWellCod74281019769 = completions.FirstOrDefault(c => c.Name?.ToUpper() == Mock._completionWellCod74281019769);
            var completionWellCod74281022570 = completions.FirstOrDefault(c => c.Name?.ToUpper() == Mock._completionWellCod74281022570);
            var completionWellCod74281024180 = completions.FirstOrDefault(c => c.Name?.ToUpper() == Mock._completionWellCod74281024180);

            Assert.That(completionWellCod74281026753NoReservoir, Is.Not.Null);
            Assert.That(completionWellCod74281026753NoReservoir.User, Is.Not.Null);
            Assert.That(completionWellCod74281026753NoReservoir.Well, Is.Not.Null);
            Assert.That(completionWellCod74281026753NoReservoir.Well.CodWellAnp?.ToUpper(), Is.EqualTo(Mock._well74281026753.CodWellAnp));
            Assert.That(completionWellCod74281026753NoReservoir.Reservoir, Is.Null);

            Assert.That(completionWellCod74281026537, Is.Not.Null);
            Assert.That(completionWellCod74281026537.User, Is.Not.Null);
            Assert.That(completionWellCod74281026537.Well, Is.Not.Null);
            Assert.That(completionWellCod74281026537.Well.CodWellAnp?.ToUpper(), Is.EqualTo(Mock._well74281026537.CodWellAnp));
            Assert.That(completionWellCod74281026537.Reservoir, Is.Not.Null);
            Assert.That(completionWellCod74281026537.Reservoir.Name?.ToUpper(), Is.EqualTo(Mock._reservoirQuissama_Tmbt));

            Assert.That(_completionWellCod74281029209, Is.Not.Null);
            Assert.That(_completionWellCod74281029209.User, Is.Not.Null);
            Assert.That(_completionWellCod74281029209.Well, Is.Not.Null);
            Assert.That(_completionWellCod74281029209.Well.CodWellAnp?.ToUpper(), Is.EqualTo(Mock._well74281029209.CodWellAnp));
            Assert.That(_completionWellCod74281029209.Reservoir, Is.Not.Null);
            Assert.That(_completionWellCod74281029209.Reservoir.Name?.ToUpper(), Is.EqualTo(Mock._reservoirQuissama_Pol));

            Assert.That(completionWellCod74281029234, Is.Not.Null);
            Assert.That(completionWellCod74281029234.User, Is.Not.Null);
            Assert.That(completionWellCod74281029234.Well, Is.Not.Null);
            Assert.That(completionWellCod74281029234.Well.CodWellAnp?.ToUpper(), Is.EqualTo(Mock._well74281029234.CodWellAnp));
            Assert.That(completionWellCod74281029234.Reservoir, Is.Not.Null);
            Assert.That(completionWellCod74281029234.Reservoir.Name?.ToUpper(), Is.EqualTo(Mock._reservoirTuroniano));

            Assert.That(completionWellCod74281019769, Is.Not.Null);
            Assert.That(completionWellCod74281019769.User, Is.Not.Null);
            Assert.That(completionWellCod74281019769.Well, Is.Not.Null);
            Assert.That(completionWellCod74281019769.Well.CodWellAnp?.ToUpper(), Is.EqualTo(Mock._well74281019769));
            Assert.That(completionWellCod74281019769.Reservoir, Is.Null);

            Assert.That(completionWellCod74281022570, Is.Not.Null);
            Assert.That(completionWellCod74281022570.User, Is.Not.Null);
            Assert.That(completionWellCod74281022570.Well, Is.Not.Null);
            Assert.That(completionWellCod74281022570.Well.CodWellAnp?.ToUpper(), Is.EqualTo(Mock._well74281022570));
            Assert.That(completionWellCod74281022570.Reservoir, Is.Null);

            Assert.That(completionWellCod74281024180, Is.Not.Null);
            Assert.That(completionWellCod74281024180.User, Is.Not.Null);
            Assert.That(completionWellCod74281024180.Well, Is.Not.Null);
            Assert.That(completionWellCod74281024180.Well.CodWellAnp?.ToUpper(), Is.EqualTo(Mock._well74281024180));
            Assert.That(completionWellCod74281024180.Reservoir, Is.Not.Null);
            Assert.That(completionWellCod74281024180.Reservoir.Name?.ToUpper(), Is.EqualTo(Mock._reservoirN560D));

        }
    }
}