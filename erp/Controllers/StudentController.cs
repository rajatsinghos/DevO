
using ERP.Data;
using ERP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

public class StudentController : Controller
{
    private readonly ApplicationDbContext _context;

    public StudentController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: STUDENTS
    public async Task<IActionResult> Index()
    {
        var students = await _context.Students
            .Include(s => s.Course)
            .ToListAsync();

        return View(students);
    }

    // GET: STUDENTS/Details/5
    public async Task<IActionResult> Details(int? studentid)
    {
        if (studentid == null)
        {
            return NotFound();
        }

        var student = await _context.Students
            .Include(s => s.Course)
            .FirstOrDefaultAsync(m => m.StudentId == studentid);

        if (student == null)
        {
            return NotFound();
        }

        return View(student);
    }

    // GET: STUDENTS/Create
    public IActionResult Create()
    {
        ViewBag.Courses = new SelectList(
            _context.Courses,
            "CourseId",
            "CourseName"
        );

        return View();
    }

    // POST: STUDENTS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
    [Bind("StudentId,Name,Email,Phone,DateOfBirth,Gender,Address,AdmissionDate,CourseId")] Student student)
    {
        if (ModelState.IsValid)
        {
            _context.Add(student);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        ViewBag.Courses = new SelectList(
            _context.Courses,
            "CourseId",
            "CourseName",
            student.CourseId
        );

        return View(student);
    }

    // GET: STUDENTS/Edit/5
    public async Task<IActionResult> Edit(int? studentid)
    {
        if (studentid == null)
        {
            return NotFound();
        }

        var student = await _context.Students.FindAsync(studentid);

        if (student == null)
        {
            return NotFound();
        }

        ViewBag.Courses = new SelectList(
            _context.Courses,
            "CourseId",
            "CourseName",
            student.CourseId
        );

        return View(student);
    }

    // POST: STUDENTS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(
    int? studentid,
    [Bind("StudentId,Name,Email,Phone,DateOfBirth,Gender,Address,AdmissionDate,CourseId")] Student student)
    {
        if (studentid != student.StudentId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(student);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(student.StudentId))
                {
                    return NotFound();
                }

                throw;
            }

            return RedirectToAction(nameof(Index));
        }

        ViewBag.Courses = new SelectList(
            _context.Courses,
            "CourseId",
            "CourseName",
            student.CourseId
        );

        return View(student);
    }

    // GET: STUDENTS/Delete/5
    public async Task<IActionResult> Delete(int? studentid)
    {
        if (studentid == null)
        {
            return NotFound();
        }

        var student = await _context.Students
            .FirstOrDefaultAsync(m => m.StudentId == studentid);
        if (student == null)
        {
            return NotFound();
        }

        return View(student);
    }

    // POST: STUDENTS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? studentid)
    {
        var student = await _context.Students.FindAsync(studentid);
        if (student != null)
        {
            _context.Students.Remove(student);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool StudentExists(int? studentid)
    {
        return _context.Students.Any(e => e.StudentId == studentid);
    }
}
