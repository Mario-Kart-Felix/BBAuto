using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using BBAuto.Logic.Services.Dealer;
using BBAuto.Repositories;
using BBAuto.Repositories.Entities;
using Common;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoMoq;

namespace BBAuto.UnitTests.Service.Dealer
{
  [TestFixture]
  public class DealerServiceTests
  {
    private IFixture _fixture;
    private Mock<IDbContext> _dbContextMock;

    [SetUp]
    public void SetUp()
    {
      AutoProfiler.RegisterProfiles(new[]
      {
        Assembly.GetAssembly(typeof(DealerMappingProfile))
      });

      _fixture = new Fixture().Customize(new AutoConfiguredMoqCustomization());
      _dbContextMock = _fixture.Freeze<Mock<IDbContext>>();
    }

    [Test]
    public void ShouldSuccessGetDealers()
    {
      //Given
      var dealerId = _fixture.Create<int>();
      var dealerName = _fixture.Create<string>();
      var dealerContacts = _fixture.Create<string>();

      var dealers = new List<DbDealer>
      {
        new DbDealer
        {
          Id = dealerId,
          Name = dealerName,
          Contacts = dealerContacts
        },
        _fixture.Create<DbDealer>()
      };

      _dbContextMock.Setup(c => c.Dealer.GetDealers())
        .Returns(dealers);

      var sut = GetSut();

      //When
      var resultDealerList = sut.GetDealers();

      //Then
      resultDealerList.Should().NotBeNull();
      resultDealerList.Count.Should().BeGreaterOrEqualTo(1);
      resultDealerList[0].Id.Should().Be(dealerId);
      resultDealerList[0].Name.Should().BeEquivalentTo(dealerName);
      resultDealerList[0].Contacts.Should().BeEquivalentTo(dealerContacts);
    }

    private DealerService GetSut()
    {
      return _fixture.Create<DealerService>();
    }
  }
}
