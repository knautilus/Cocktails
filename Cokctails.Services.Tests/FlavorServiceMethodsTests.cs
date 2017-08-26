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

        [SetUp]
        public void SetUp()
        {
            InitMapper();
            _token = new CancellationToken();
        }

        [Test]
        public async Task AddFlavorCallsRepositoryMethods()
        {
            var flavorModel = new FlavorModel { Name = "flavor1" };

            var flavor = new Flavor { Id = Guid.NewGuid(), Name = flavorModel.Name };

            var repositoryMock = new Mock<IRepository<Flavor>>();
            repositoryMock
                .Setup(x => x.InsertAsync(It.IsAny<Flavor>(), _token))
                .Returns(Task.FromResult(flavor));
            repositoryMock
                .Setup(x => x.GetSingleAsync(It.IsAny<Func<IQueryable<Flavor>, IQueryable<Flavor>>>(), _token))
                .Returns(Task.FromResult(flavor));

            var service = new FlavorService(repositoryMock.Object, _mapper);

            var result = await service.CreateAsync(flavorModel, _token);

            Assert.AreEqual(flavorModel.Name, result.Name);
            repositoryMock.Verify(x => x.InsertAsync(It.IsAny<Flavor>(), _token), Times.Once);
            repositoryMock.Verify(x => x.GetSingleAsync(It.IsAny<Func<IQueryable<Flavor>, IQueryable<Flavor>>>(), _token), Times.Once);
            repositoryMock.Verify(x => x.UpdateAsync(It.IsAny<Flavor>(), _token), Times.Never);
            repositoryMock.Verify(x => x.DeleteAsync(It.IsAny<Flavor>(), _token), Times.Never);
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
