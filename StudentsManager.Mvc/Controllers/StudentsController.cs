using MediatR;
using Microsoft.AspNetCore.Mvc;
using StudentsManagement.Domain.Commands;
using StudentsManagement.Domain.Queries;

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

    [HttpDelete]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteAsync(Guid guid)
    {
        var command = new DeleteStudentCommand(guid);
        await _mediator.Send(command);
        return RedirectToAction(nameof(Index));
    }
}