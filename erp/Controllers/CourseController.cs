
using ERP.Data;
using ERP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

public class CourseController : Controller
{
    private readonly ApplicationDbContext _context;

    public CourseController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: COURSES
    public async Task<IActionResult> Index()
    {
        var courses = await _context.Courses
            .Include(c => c.Department)
            .ToListAsync();

        return View(courses);
    }

    // GET: COURSES/Details/5
    public async Task<IActionResult> Details(int? courseid)
    {
        if (courseid == null)
        {
            return NotFound();
        }

        var course = await _context.Courses
            .FirstOrDefaultAsync(m => m.CourseId == courseid);
        if (course == null)
        {
            return NotFound();
        }

        return View(course);
    }

    // GET: COURSES/Create
    public IActionResult Create()
    {
        ViewBag.Departments = new SelectList(
       _context.Departments,
       "DepartmentId",
       "DepartmentName"
   );
        return View();
    }

    // POST: COURSES/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
    [Bind("CourseId,CourseName,Duration,DepartmentId")] Course course)
    {
        if (ModelState.IsValid)
        {
            _context.Add(course);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        ViewBag.Departments = new SelectList(
            _context.Departments,
            "DepartmentId",
            "DepartmentName",
            course.DepartmentId
        );

        return View(course);
    }

    // GET: COURSES/Edit/5
    public async Task<IActionResult> Edit(int? courseid)
    {
        if (courseid == null)
        {
            return NotFound();
        }

        var course = await _context.Courses.FindAsync(courseid);

        if (course == null)
        {
            return NotFound();
        }

        ViewBag.Departments = new SelectList(
            _context.Departments,
            "DepartmentId",
            "DepartmentName",
            course.DepartmentId
        );

        return View(course);
    }

    // POST: COURSES/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
    int? courseid,
    [Bind("CourseId,CourseName,Duration,DepartmentId")] Course course)
    {
        if (courseid != course.CourseId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(course);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(course.CourseId))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        ViewBag.Departments = new SelectList(
            _context.Departments,
            "DepartmentId",
            "DepartmentName",
            course.DepartmentId
        );

        return View(course);
    }

    // GET: COURSES/Delete/5
    public async Task<IActionResult> Delete(int? courseid)
    {
        if (courseid == null)
        {
            return NotFound();
        }

        var course = await _context.Courses
            .FirstOrDefaultAsync(m => m.CourseId == courseid);
        if (course == null)
        {
            return NotFound();
        }

        return View(course);
    }

    // POST: COURSES/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? courseid)
    {
        var course = await _context.Courses.FindAsync(courseid);
        if (course != null)
        {
            _context.Courses.Remove(course);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool CourseExists(int? courseid)
    {
        return _context.Courses.Any(e => e.CourseId == courseid);
    }
}
