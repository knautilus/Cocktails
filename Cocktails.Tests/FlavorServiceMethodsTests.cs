using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Moq;
using NUnit.Framework;

using Cocktails.Data.Domain;
using Cocktails.Data.EntityFramework.Repositories;
using Cocktails.Mapper;
using Cocktails.Services;
using Cocktails.ViewModels;

namespace Cocktails.Tests
{
    [TestFixture]
    class FlavorServiceMethodsTests
    {
        private IModelMapper _mapper;
        private CancellationToken _token;
        private Mock<IRepository<Flavor>> _repositoryMock;
        private FlavorService _service;

        [SetUp]
        public void SetUp()
        {
            InitMapper();
            _token = new CancellationToken();
            _repositoryMock = new Mock<IRepository<Flavor>>();
            _service = new FlavorService(_repositoryMock.Object, _mapper);
        }

        [Test]
        public async Task AddFlavorCallsRepositoryMethods()
        {
            var flavorModel = new FlavorModel { Name = "new flavor" };

            var flavor = new Flavor { Id = Guid.NewGuid(), Name = flavorModel.Name };

            _repositoryMock
                .Setup(x => x.InsertAsync(It.IsAny<Flavor>(), _token))
                .Returns(Task.FromResult(flavor));
            _repositoryMock
                .Setup(x => x.GetSingleAsync(It.IsAny<Func<IQueryable<Flavor>, IQueryable<Flavor>>>(), _token))
                .Returns(Task.FromResult(flavor));

            var result = await _service.CreateAsync(flavorModel, _token);

            Assert.AreEqual(flavorModel.Name, result.Name);
            _repositoryMock.Verify(x => x.InsertAsync(It.Is<Flavor>(y => y.Name == flavorModel.Name), _token), Times.Once);
            _repositoryMock.Verify(x => x.GetSingleAsync(It.IsAny<Func<IQueryable<Flavor>, IQueryable<Flavor>>>(), _token), Times.Once);
            _repositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Flavor>(), _token), Times.Never);
            _repositoryMock.Verify(x => x.DeleteAsync(It.IsAny<Flavor>(), _token), Times.Never);
        }

        [Test]
        public async Task UpdateFlavorCallsRepositoryMethods()
        {
            var id = Guid.NewGuid();

            var flavorModel = new FlavorModel { Id = id, Name = "new flavor" };

            var flavor = new Flavor { Id = flavorModel.Id, Name = flavorModel.Name };

            _repositoryMock
                .Setup(x => x.UpdateAsync(It.IsAny<Flavor>(), _token))
                .Returns(Task.FromResult(flavor));
            _repositoryMock
                .Setup(x => x.GetSingleAsync(It.IsAny<Func<IQueryable<Flavor>, IQueryable<Flavor>>>(), _token))
                .Returns(Task.FromResult(flavor));

            var result = await _service.UpdateAsync(id, flavorModel, _token);

            Assert.AreEqual(flavorModel.Name, result.Name);
            _repositoryMock.Verify(x => x.UpdateAsync(It.Is<Flavor>(y => y.Id == id), _token), Times.Once);
            _repositoryMock.Verify(x => x.GetSingleAsync(It.IsAny<Func<IQueryable<Flavor>, IQueryable<Flavor>>>(), _token), Times.Once);
            _repositoryMock.Verify(x => x.InsertAsync(It.IsAny<Flavor>(), _token), Times.Never);
            _repositoryMock.Verify(x => x.DeleteAsync(It.IsAny<Flavor>(), _token), Times.Never);
        }

        [Test]
        public async Task DeleteFlavorCallsRepositoryMethods()
        {
            var id = Guid.NewGuid();

            await _service.DeleteAsync(id, _token);

            _repositoryMock.Verify(x => x.DeleteAsync(It.Is<Flavor>(y => y.Id == id), _token), Times.Once);
            _repositoryMock.Verify(x => x.GetSingleAsync(It.IsAny<Func<IQueryable<Flavor>, IQueryable<Flavor>>>(), _token), Times.Never);
            _repositoryMock.Verify(x => x.InsertAsync(It.IsAny<Flavor>(), _token), Times.Never);
            _repositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Flavor>(), _token), Times.Never);

        }

        private void InitMapper()
        {
            AutoMapper.Mapper.Initialize(x =>
            {
                x.AddProfile<MappingProfile>();
            });

            AutoMapper.Mapper.AssertConfigurationIsValid();

            _mapper = new ModelMapper(AutoMapper.Mapper.Instance);
        }
    }
}
