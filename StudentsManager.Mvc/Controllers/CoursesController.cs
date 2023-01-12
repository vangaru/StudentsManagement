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
        return View(await GetCreateCourseViewModelAsync());
    }

    [HttpGet]
    public async Task<IActionResult> EditAsync(Guid courseId)
    {
        return View(await GetUpdateCourseViewModelAsync(courseId));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> CreateCourseAsync(CreateCourseViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(await GetCreateCourseViewModelAsync());
        }

        var createCourseCommand = new CreateCourseCommand(model.Name!, model.Description, model.SelectedStudentsIds);
        await _mediator.Send(createCourseCommand);

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditAsync(UpdateCourseViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(await GetUpdateCourseViewModelAsync(model.CourseId));
        }

        var updateCourseCommand =
            new UpdateCourseCommand(model.CourseId, model.Name!, model.Description, model.SelectedStudentsIds);
        await _mediator.Send(updateCourseCommand);

        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteAsync(Guid courseId)
    {
        var command = new DeleteCourseCommand(courseId);
        await _mediator.Send(command);
        return RedirectToAction(nameof(Index));
    }

    #region helpers

    private async Task<UpdateCourseViewModel> GetUpdateCourseViewModelAsync(Guid courseId)
    {
        var getCourseByIdQuery = new GetCourseByIdQuery(courseId);
        Course course = await _mediator.Send(getCourseByIdQuery);
        var getStudentsQuery = new GetStudentsQuery();
        IEnumerable<Student> allStudents = await _mediator.Send(getStudentsQuery);
        UpdateCourseViewModel updateCourseViewModel = UpdateCourseViewModel.CreateInstance(course, allStudents);
        return updateCourseViewModel;
    }

    private async Task<CreateCourseViewModel> GetCreateCourseViewModelAsync()
    {
        var getStudentsQuery = new GetStudentsQuery();
        IEnumerable<Student> students = await _mediator.Send(getStudentsQuery);
        return CreateCourseViewModel.CreateInstance(students);
    }

    #endregion
}