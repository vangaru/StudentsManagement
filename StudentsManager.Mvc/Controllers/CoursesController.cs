using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudentsManagement.Domain.Commands;
using StudentsManagement.Domain.Models;
using StudentsManagement.Domain.Queries;
using StudentsManager.Mvc.Models;

namespace StudentsManager.Mvc.Controllers;

public class CoursesController : Controller
{
    private readonly IMediator _mediator;

    public CoursesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<IActionResult> IndexAsync()
    {
        IEnumerable<Course> students = await _mediator.Send(new GetCoursesQuery());
        return View(students);
    }

    [HttpGet]
    public async Task<IActionResult> CreateCourseAsync()
    {
        var getStudentsQuery = new GetStudentsQuery();
        IEnumerable<Student> students = await _mediator.Send(getStudentsQuery);
        return View(CreateCourseViewModel.CreateInstance(students));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateCourseAsync(CreateCourseViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var createCourseCommand = new CreateCourseCommand(model.Name!, model.Description, model.SelectedStudentsIds);
        await _mediator.Send(createCourseCommand);

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> DeleteAsync(Guid courseId)
    {
        var command = new DeleteCourseCommand(courseId);
        await _mediator.Send(command);
        return RedirectToAction(nameof(Index));
    }
}