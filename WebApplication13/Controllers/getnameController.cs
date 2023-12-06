using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using WebApplication13;

namespace WebApplication13.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class getnameController : ControllerBase
    {



        [HttpGet("{ro}")]
        public IEnumerable<getname> Get(string RO)
        {
            List<getname> li = new List<getname>();
            //SqlConnection conn1 = new SqlConnection("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\96650\\OneDrive\\المستندات\\project.mdf;Integrated Security=True;Connect Timeout=30");
            var builder = WebApplication.CreateBuilder();
            string conStr = builder.Configuration.GetConnectionString("WebApplication13Context");
            SqlConnection conn1 = new SqlConnection(conStr);
            string sql;
            sql = "SELECT * FROM userall where Role ='" + RO + "' ";
            SqlCommand comm = new SqlCommand(sql, conn1);
            conn1.Open();
            SqlDataReader reader = comm.ExecuteReader();

            while (reader.Read())
            {
                li.Add(new getname
                {
                    name = (string)reader["name"],
                });


            }

            reader.Close();
            conn1.Close();
            return li;
        }

    }
}
