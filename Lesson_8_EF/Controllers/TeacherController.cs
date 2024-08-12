﻿using Lesson_8_EF.Models.Entities.Concrets;
using Lesson_8_EF.Models.ViewModels.Student;
using Lesson_8_EF.Models.ViewModels.Teacher;
using Lesson_8_EF.Repositories.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace Lesson_8_EF.Controllers;

public class TeacherController : Controller
{
    public ITeacherRepository _teacherRepo { get; set; }

    public TeacherController(ITeacherRepository repository)
    {
        _teacherRepo = repository;
    }


    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Add()
    {
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> Add(TeacherVM vM)
    {
        if (ModelState.IsValid)
        {

            var teacher = new Teacher()
            {
                Name = vM.Name,
                Surname = vM.Surname,
                FatherName = vM.FatherName,
                Salary = vM.Salary,
            };


            await _teacherRepo.AddAsync(teacher);
            await _teacherRepo.SaveChangeAsync();
            return RedirectToAction("AllTeachers");

        }
        return View(vM);
    }

    [HttpGet]
    public async Task<IActionResult> AllTeachers()
    {
        var teachers = await _teacherRepo.GetAllAsync();

        var teachersV = new List<TeacherVM>();

        foreach (var teacher in teachers)
        {
            var newT = new TeacherVM()
            {
                Id = teacher.Id,
                Name = teacher.Name,
                Surname = teacher.Surname,
                FatherName = teacher.FatherName,
                Salary = teacher.Salary,
            };
            teachersV.Add(newT);
        }

        return View(teachersV);
    }


    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var teacher = await _teacherRepo!.GetByIdAsync(id);
        if (teacher is null)
        {
            ViewBag.Error = $@"{id} -li Element tapilmadi";
            return RedirectToAction("GetAll");
        }

        await _teacherRepo.DeleteAsync(teacher);
        await _teacherRepo.SaveChangeAsync();

        return RedirectToAction("AllTeachers");
    }

    [HttpGet]
    public async Task<IActionResult> Update(int id)
    {
        var entity = await _teacherRepo?.GetByIdAsync(id)!;
        return View(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Update(TeacherVM stVM, int id)
    {

        var entity = await _teacherRepo?.GetByIdAsync(id)!;

        entity.Name = stVM.Name;
        entity.Surname = stVM.Surname;
        entity.FatherName = stVM.FatherName;
        entity.Salary = stVM.Salary;
        entity.UpdatedDate = DateTime.Now;


        await _teacherRepo!.UpdateAsync(entity);
        await _teacherRepo.SaveChangeAsync();

        return RedirectToAction("AllTeachers");
    }
}
