using back_cursos.models;
using back_cursos.services;
using Microsoft.AspNetCore.Mvc;

namespace back_cursos.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CourseController : ControllerBase
    {
        private readonly CourseService courseService;

        public CourseController(CourseService courseService)
        {
            this.courseService = courseService;
        }

        [HttpGet]
        public ActionResult<List<Course>> findAll()
        {
            return Ok(courseService.getAllCourses());
        }

        [HttpGet("{id}")]
        public ActionResult<Course?> findCourseById(int id)
        {
            Course? c = courseService.findCourseById(id);
            if(c is null)
            {
                return NotFound("NO EXISTE CURSO CON ID " + id);
            }
            return Ok(c);
        }

        [HttpPost]
        public ActionResult<Course> createCourse(CourseDTO course)
        {
            Course cou = courseService.saveCourse(course);

            if(cou is null)
            {
                return BadRequest("CUERPO INCORRECTO DE SOLICITUD INGRESADO");
            }
                return Ok(cou);
        }

        [HttpPut("{id}")]
        public ActionResult<Course> updateCourse(Course c, int id)
        {
            return Ok(courseService.updateCourse(c,id));
        }
    }
}