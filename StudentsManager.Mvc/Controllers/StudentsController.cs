using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudentsManagement.Domain.Commands;
using StudentsManagement.Domain.Models;
using StudentsManagement.Domain.Queries;
using StudentsManager.Mvc.Models;

namespace StudentsManager.Mvc.Controllers;

public class StudentsController : Controller
{
    private readonly IMediator _mediator;

    public StudentsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<IActionResult> IndexAsync()
    {
        var students = await _mediator.Send(new GetStudentsQuery());
        return View(students);
    }

    [HttpGet]
    public async Task<IActionResult> CreateStudentAsync()
    {
        var getCoursesQuery = new GetCoursesQuery();
        IEnumerable<Course> courses = await _mediator.Send(getCoursesQuery);
        return View(CreateStudentViewModel.CreateInstance(courses));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateStudentAsync(CreateStudentViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var createStudentCommand = new CreateStudentCommand(model.Name!, model.SelectedCoursesIds);
        await _mediator.Send(createStudentCommand);
        
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteAsync(Guid studentId)
    {
        var command = new DeleteStudentCommand(studentId);
        await _mediator.Send(command);
        return RedirectToAction(nameof(Index));
    }
}