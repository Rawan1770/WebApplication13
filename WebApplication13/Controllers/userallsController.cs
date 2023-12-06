using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebApplication13.Data;
using WebApplication13.Models;

namespace WebApplication13.Controllers
{
    public class userallsController : Controller
    {
        private readonly WebApplication13Context _context;

        public userallsController(WebApplication13Context context)
        {
            _context = context;
        }
        public IActionResult login()
        {
            return View();
        }

        [HttpPost, ActionName("login")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> login(string na, string pa)
        {
            var builder = WebApplication.CreateBuilder();
            string conStr = builder.Configuration.GetConnectionString("WebApplication9Context");
            SqlConnection conn1 = new SqlConnection(conStr);
            string sql;
            sql = "SELECT * FROM userall where name ='" + na + "' and  pass ='" + pa + "' ";
            SqlCommand comm = new SqlCommand(sql, conn1);
            conn1.Open();
            SqlDataReader reader = comm.ExecuteReader();

            if (reader.Read())
            {
                string id = Convert.ToString((int)reader["Id"]);
                string na1 = (string)reader["name"];
                string ro = (string)reader["role"];
                HttpContext.Session.SetString("userid", id);
                HttpContext.Session.SetString("Name", na1);
                HttpContext.Session.SetString("Role", ro);
                reader.Close();
                conn1.Close();
                return RedirectToAction("home", "userall");
            }
            else
            {
                ViewData["Message"] = "wrong user name password";
                return View();
            }
        }
        // GET: useralls
        public async Task<IActionResult> Index()
        {
              return _context.userall != null ? 
                          View(await _context.userall.ToListAsync()) :
                          Problem("Entity set 'WebApplication13Context.userall'  is null.");
        }

        // GET: useralls/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.userall == null)
            {
                return NotFound();
            }

            var userall = await _context.userall
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userall == null)
            {
                return NotFound();
            }

            return View(userall);
        }

        // GET: useralls/Create

        public async Task<IActionResult> Search()
        {
            List<userall> brItems = new List<userall>();

            return View(brItems);

        }

        [HttpPost]
        public async Task<IActionResult> Search(string s)
        {
            var brItems = await _context.items.FromSqlRaw("select * from userall where info LIKE '%" + s + "%' ").ToListAsync();
            return View(brItems);
        }

 


        public IActionResult Create()
        {
            return View();
        }

        // POST: useralls/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,name,password,role,RegistDate")] userall userall)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userall);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userall);
        }

       

            // GET: useralls/Edit/5
            public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.userall == null)
            {
                return NotFound();
            }

            var userall = await _context.userall.FindAsync(id);
            if (userall == null)
            {
                return NotFound();
            }
            return View(userall);
        }
        public IActionResult edit()
        {
            return View();
        }
        // POST: useralls/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,name,password,role,RegistDate")] userall userall)
        {
            if (id != userall.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userall);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!userallExists(userall.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(userall);
        }

        // GET: useralls/Delete/5

        public IActionResult addadmin()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> addadmin([Bind("name,password")] userall userall)
        {

            var builder = WebApplication.CreateBuilder();
            string conStr = builder.Configuration.GetConnectionString("Web2ProjectContext");
            SqlConnection conn1 = new SqlConnection(conStr);
            string sql;
            sql = "select * from userall where name ='" + userall.name + "' ";
            Boolean flage = false;
            SqlCommand comm = new SqlCommand(sql, conn1);
            conn1.Open();
            SqlDataReader reader = comm.ExecuteReader();
            if (reader.Read())
            {
                flage = true;
            }
            reader.Close();
            if (flage == true)
            {
                ViewData["message"] = "name already exists";
            }
            else
            {
                var role = "admin";
                sql = "insert into userall (name,password,role) values ('" + userall.name + "','" + userall.password + "','" + role + "')";
                comm = new SqlCommand(sql, conn1);
                comm.ExecuteNonQuery();
                return RedirectToAction(nameof(Index));
            }
            conn1.Close();

            return View();
        }

        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration([Bind("name,password")] userall userall)
        {

            var builder = WebApplication.CreateBuilder();
            string conStr = builder.Configuration.GetConnectionString("Web2ProjectContext");
            SqlConnection conn1 = new SqlConnection(conStr);
            string sql;
            sql = "select * from userall where name ='" + userall.name + "' ";
            Boolean flage = false;
            SqlCommand comm = new SqlCommand(sql, conn1);
            conn1.Open();
            SqlDataReader reader = comm.ExecuteReader();
            if (reader.Read())
            {
                flage = true;
            }
            reader.Close();
            if (flage == true)
            {
                ViewData["message"] = "name already exists";
            }
            else
            {
                var role = "Customer";
                sql = "insert into userall (name,password,role) values ('" + userall.name + "','" + userall.password + "','" + role + "')";
                comm = new SqlCommand(sql, conn1);
                comm.ExecuteNonQuery();
                return RedirectToAction(nameof(Index));
            }
            conn1.Close();

            return View();
        }





        public IActionResult email()
        {
            ViewData["Message"] = "";
            return View();
        }



        [HttpPost, ActionName("email")]
        [ValidateAntiForgeryToken]
        public IActionResult email(string address, string subject, string body)
        {
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            var mail = new MailMessage();
            mail.From = new MailAddress("aixxii@gmail.com");
            mail.To.Add(address); // receiver email address
            mail.Subject = subject;
            mail.IsBodyHtml = true;
            mail.Body = body;
            SmtpServer.Port = 587;
            SmtpServer.UseDefaultCredentials = false;
            SmtpServer.Credentials = new System.Net.NetworkCredential("aixxi@gmail.com", "wicapwibsxnmklpb");
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);
            ViewData["Message"] = "Email sent.";
            return View();

        }
        public async Task<IActionResult> getname()
        {

            {
                userall brItem = new userall();

                return View(brItem);
            }
        }

        [HttpPost]

        public async Task<IActionResult> getname(string na)
        {
            var bkItems = await _context.userall.FromSqlRaw("select * from userall where name = '" + na + "' ").FirstOrDefaultAsync();

            return View(bkItems);
        }


    
    public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.userall == null)
            {
                return NotFound();
            }

            var userall = await _context.userall
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userall == null)
            {
                return NotFound();
            }

            return View(userall);
        }



        // POST: useralls/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.userall == null)
            {
                return Problem("Entity set 'WebApplication13Context.userall'  is null.");
            }
            var userall = await _context.userall.FindAsync(id);
            if (userall != null)
            {
                _context.userall.Remove(userall);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool userallExists(int id)
        {
          return (_context.userall?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
