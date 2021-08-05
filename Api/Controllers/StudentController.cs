using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/student")]
    public class StudentController : ControllerBase
    {
        readonly IStudentService _studentService;
        
        public StudentController(IStudentService studentService)
        {
            this._studentService = studentService;
        }

        [HttpPost]
        [Route("signUp")]
        public async Task<Result> SignUp(Student student)
        {
            return await this._studentService.SignUp(student);
        }
    }
}
