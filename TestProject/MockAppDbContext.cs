using Mechanico_Api.Contexts;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using TestProject.RepositoryTests;

namespace TestProject;

public class MockAppDbContext
{
    public readonly AppDbContext AppDbContext;

    public MockAppDbContext()
    {
        var dataHelper =  DataHelper.Instance;
        
        var appDbContextMock = new Mock<AppDbContext>(new DbContextOptionsBuilder().UseSqlServer().Options);
        appDbContextMock.Setup( a => a.Users).ReturnsDbSet(dataHelper.GetUsers());
        appDbContextMock.Setup( a => a.SmsCodes).ReturnsDbSet(dataHelper.GetSmsCodes());
        
        AppDbContext = appDbContextMock.Object;
    }
}