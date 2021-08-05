using System.Collections.Generic;
using Domain.Entities;
using Domain.Interfaces.Services;
using Domain.Results;
using Domain.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/course")]
    public class CourseController : ControllerBase
    {
        readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            this._courseService = courseService;
        }

        [HttpPost]
        [Route("insert")]
        public Result Insert(Course course)
        {
            var courses = new List<Course>() { course };

            return this._courseService.Insert(courses);
        }

        [HttpGet]
        [Route("report")]
        public Result<IEnumerable<CourseGeneralReport>> GetDefaultReport()
        {
            return this._courseService.GetDefaultReport();
        }
    }
}