using Microsoft.EntityFrameworkCore;
using Web_Api_CRUD.Infraestructure;

public class ApplicationDbContextMock : ApplicationDbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("FinancialAccountInMemory");
    }
}