using Lesson_8_EF.Models.Entities.Concrets;
using Lesson_8_EF.Models.ViewModels;
using Lesson_8_EF.Models.ViewModels.Student;
using Lesson_8_EF.Repositories.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace Lesson_8_EF.Controllers;

public class GroupController : Controller
{
    public readonly IGroupRepository? _groupRepo;

    public GroupController(IGroupRepository? groupRepo)
    {
        _groupRepo = groupRepo;
    }

    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Add(AddGroupVM model)
    {
        if(!ModelState.IsValid)
            return View(model);

        Group group = new Group()
        {
            Name = model.Name,
            Code = model.Code,
            CreatedDate = DateTime.Now
        };

        await _groupRepo?.AddAsync(group)!;
        await _groupRepo.SaveChangeAsync();
        return RedirectToAction("GetAll");
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var groups = await _groupRepo?.GetAllAsync()!;

        var groupsVm = new List<ShowGroupVM>();
        foreach (var group in groups)
        {
            var newG = new ShowGroupVM()
            {
                Id = group.Id,
                Name = group.Name,
                Code = group.Code,
                CreatedDate = group.CreatedDate,
                TeacherName = group?.Teacher?.Name,
                UpdatedDate = group.UpdatedDate,
            };
            groupsVm.Add(newG);
        }
        

        return View(groupsVm);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var entity = await _groupRepo?.GetByIdAsync(id)!;
        if(entity is null)
        {
            ViewBag.Error = $@"{id} -li Element tapilmadi";
            return RedirectToAction("GetAll");
        }

        await _groupRepo.DeleteAsync(entity);
        await _groupRepo.SaveChangeAsync();

        return RedirectToAction("GetAll");
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var entity = await _groupRepo?.GetByIdAsync(id)!;
        return View(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Update(AddGroupVM groupVM, int id)
    {

        var entity = await _groupRepo?.GetByIdAsync(id)!;


        entity.Code = groupVM.Code;
        entity.Name = groupVM.Name;
        entity.UpdatedDate = DateTime.Now;


        await _groupRepo!.UpdateAsync(entity);
        await _groupRepo.SaveChangeAsync();

        return RedirectToAction("GetAll");
    }



}
