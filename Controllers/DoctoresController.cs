﻿using Microsoft.AspNetCore.Mvc;
using MvcCoreAdoNet.Repositories;
using MvcCoreAdoNet.Models;

namespace MvcCoreAdoNet.Controllers
{
    public class DoctoresController : Controller
    {
        RepositoryDoctor repo;

        public DoctoresController()
        {
            this.repo = new RepositoryDoctor();
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> DoctoresEspecialidad()
        {
            List<Doctor> doctores = await this.repo.GetDoctoresAsync();
            List<string> especialidades = await this.repo.GetEspecialidadesAsync();
            ViewBag.Especialidades = especialidades;
            return View(doctores);
        }

        [HttpPost]
        public async Task<IActionResult> DoctoresEspecialidad(string especialidad)
        {
            List<Doctor> doctores = await this.repo.GetDoctoresEspecialidadAsync(especialidad);
            List<string> especialidades = await this.repo.GetEspecialidadesAsync();
            ViewBag.Especialidades = especialidades;
            return View(doctores);
        }
    }
}
