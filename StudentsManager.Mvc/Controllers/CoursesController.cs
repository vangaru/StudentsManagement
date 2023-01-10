using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudentsManagement.Domain.Models;
using StudentsManagement.Domain.Queries;

namespace StudentsManager.Mvc.Controllers;

public class CoursesController : Controller
{
    private readonly IMediator _mediator;

    public CoursesController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public async Task<IActionResult> Index()
    {
        IEnumerable<Course> students = await _mediator.Send(new GetCoursesQuery());
        return View(students);
    }
}