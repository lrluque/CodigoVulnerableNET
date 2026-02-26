using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Parte2.Data;
using Parte2.Models;

namespace Parte2.Controllers
{
    public class TodoController : Controller
    {
        private readonly TodoContext _context;

        public TodoController(TodoContext context)
        {
            _context = context;
        }

        // Vulnerabilidad: Falta de autenticación y autorización
        public async Task<IActionResult> Index()
        {
            return View(await _context.TodoItems.ToListAsync());
        }

        // GET: Todo/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Todo/Create
        [HttpPost]
        [ValidateAntiForgeryToken] // Protección CSRF incluida
        public async Task<IActionResult> Create([Bind("Title,Description")] TodoItem todoItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(todoItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(todoItem);
        }

        // Vulnerabilidad: Uso de consultas concatenadas, susceptible a SQL Injection
        public async Task<IActionResult> Search(string query)
        {
            var items = await _context.TodoItems
                .FromSqlRaw($"SELECT * FROM TodoItems WHERE Title LIKE '%{query}%' OR Description LIKE '%{query}%'")
 var items = await _context.TodoItems
     .FromSqlInterpolated($"SELECT * FROM TodoItems WHERE Title LIKE '%{query}%' OR Description LIKE '%{query}%'")
    .ToListAsync();
    }
}
