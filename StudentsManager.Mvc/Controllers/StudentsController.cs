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
        return View(await GetCreateStudentViewModelAsync());
    }

    public async Task<IActionResult> EditAsync(Guid studentId)
    {
        return View(await GetUpdateStudentViewModelAsync(studentId));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateStudentAsync(CreateStudentViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(await GetCreateStudentViewModelAsync());
        }

        var createStudentCommand = new CreateStudentCommand(model.Name!, model.SelectedCoursesIds);
        await _mediator.Send(createStudentCommand);
        
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditAsync(UpdateStudentViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(await GetUpdateStudentViewModelAsync(model.StudentId));
        }

        var updateStudentCommand = new UpdateStudentCommand(model.StudentId, model.Name!, model.SelectedCoursesIds);
        await _mediator.Send(updateStudentCommand);
        
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

    #region helpers

    private async Task<UpdateStudentViewModel> GetUpdateStudentViewModelAsync(Guid studentId)
    {
        var getStudentByIdQuery = new GetStudentByIdQuery(studentId);
        Student student = await _mediator.Send(getStudentByIdQuery);
        var getCoursesQuery = new GetCoursesQuery();
        IEnumerable<Course> allCourses = await _mediator.Send(getCoursesQuery);
        UpdateStudentViewModel updateStudentViewModel = UpdateStudentViewModel.CreateInstance(student, allCourses);
        return updateStudentViewModel;
    }

    private async Task<CreateStudentViewModel> GetCreateStudentViewModelAsync()
    {
        var getCoursesQuery = new GetCoursesQuery();
        IEnumerable<Course> courses = await _mediator.Send(getCoursesQuery);
        return CreateStudentViewModel.CreateInstance(courses);
    }
    
    #endregion
}