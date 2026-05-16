using back_cursos.models;

namespace back_cursos.services
{
    public class CourseService
    {

        private List<Course> courses = new List<Course>
        {
            new Course{idCo = 1, name = "Calculo Diferencial",      teachName = "Carlos Ramírez",    slots = 30, takenSlots = 18, available = true},
            new Course{idCo = 2, name = "Programación I",           teachName = "María González",    slots = 25, takenSlots = 25, available = false},
            new Course{idCo = 3, name = "Base de Datos",            teachName = "Luis Andrade",      slots = 28, takenSlots = 20, available = true},
            new Course{idCo = 4, name = "Diseño Web",               teachName = "Andrea López",      slots = 30, takenSlots = 12, available = true},
            new Course{idCo = 5, name = "Programacion II",          teachName = "Jorge Mendoza",     slots = 20, takenSlots = 19, available = true},
            new Course{idCo = 6, name = "Inglés Técnico",           teachName = "Sofía Herrera",     slots = 35, takenSlots = 22, available = true},
            new Course{idCo = 7, name = "Arquitectura de Software", teachName = "Fernando Castillo", slots = 24, takenSlots = 24, available = false},
            new Course{idCo = 8, name = "Sistemas Operativos",      teachName = "Patricia Vera",     slots = 26, takenSlots = 15, available = true},
            new Course{idCo = 9, name = "Gestión de Proyectos",     teachName = "Roberto Salazar",   slots = 32, takenSlots = 28, available = true},
            new Course{idCo = 10,name = "Inteligencia Artificial",  teachName = "Daniela Torres",    slots = 20, takenSlots = 20, available = false}
        };


        public List<Course> getAllCourses()
        {
            return courses;
        }

        public Course? findCourseById(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El id del curso debe ser mayor a 0.");

            Course? courF = null;
            foreach (Course c in courses)
            {
                if (c.idCo == id)
                {
                    courF = c;
                    break;
                }
            }
            return courF;
        }

        public Course saveCourse(CourseDTO c)
        {
            if (c == null)
                throw new ArgumentNullException(nameof(c), "El curso no puede ser nulo.");

            validateCourseData(c.name, c.teachName, c.slots, c.takenSlots);

            //Obtenemos la longitud y le agregamos un +1 porque el array inicia desde cero, asi obtenemos siguiente ID
            int nextId = courses.Count + 1;

            //Mandamos el estado del curso como la comparacion de que si los cupos son mayores a los cupos tomados, eso retorna
            //true o false
            Course nc = new Course
            {
                idCo = nextId,
                name = c.name,
                teachName = c.teachName,
                available = c.takenSlots < c.slots,
                slots = c.slots,
                takenSlots = c.takenSlots
            };
            courses.Add(nc);
            return nc;
        }

        public Course updateCourse(Course c, int id)
        {
            if (c == null)
                throw new ArgumentNullException(nameof(c), "El curso no puede ser nulo.");

            if (id <= 0)
                throw new ArgumentException("El id del curso debe ser mayor a 0.");

            validateCourseData(c.name, c.teachName, c.slots, c.takenSlots);

            foreach (Course cou in courses)
            {
                if (cou.idCo == id)
                {
                    cou.name = c.name.Trim();
                    cou.teachName = c.teachName.Trim();
                    cou.slots = c.slots;
                    cou.takenSlots = c.takenSlots;
                    cou.available = c.takenSlots < c.slots;

                    return cou;
                }
            }

            throw new KeyNotFoundException("No existe un curso con ese id.");
        }

        //PARA NO HACER TAN EXTENSO LOS METODOS CON LAS VALIDACIONES, MEJOR DESFRAGMENTAR
        private void validateCourseData(string name, string teachName, int slots, int takenSlots)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("El nombre del curso es obligatorio.");

            if (string.IsNullOrWhiteSpace(teachName))
                throw new ArgumentException("El nombre del docente es obligatorio.");

            if (slots <= 0)
                throw new ArgumentException("Los cupos totales deben ser mayor a 0.");

            if (takenSlots < 0)
                throw new ArgumentException("Los cupos tomados no pueden ser negativos.");

            if (takenSlots > slots)
                throw new ArgumentException("Los cupos tomados no pueden superar los cupos totales.");
        }
        public bool deleteCourse(int id)
        {
            if (id <= 0)
                throw new ArgumentException("El id del curso debe ser mayor a 0.");

            Course? course = findCourseById(id);

            if (course == null)
                return false;

            courses.Remove(course);

            return true;
        }
    }
}