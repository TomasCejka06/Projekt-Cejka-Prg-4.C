using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;


namespace UvodDoAspNet.Web.Data
{
    
    public class AppDbContext : DbContext
    {
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=mysqlstudenti.litv.sssvt.cz;database=4c2_cejkatomas_db2;user=cejkatomas;password=123456");
        }

    }
}
