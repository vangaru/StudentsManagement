using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudentsManagement.Domain.Queries;

namespace StudentsManager.Mvc.Controllers;

public class StudentsController : Controller
{
    private readonly IMediator _mediator;

    public StudentsController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public async Task<IActionResult> Index()
    {
        var students = await _mediator.Send(new GetStudentsQuery());
        return View(students);
    }
}