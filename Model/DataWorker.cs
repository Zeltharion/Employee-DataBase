using ManageStaffDBApp.Model.Data;
using System.Collections.Generic;
using System.Linq;

namespace ManageStaffDBApp.Model
{
    /// <summary>
    /// Добавление, удаление, редактирование, получение данных об организации
    /// </summary>
    public static class DataWorker
    {
        #region DEPARTMENT
        /// <summary>
        /// Получить все отделы
        /// </summary>
        /// <returns></returns>
        public static List<Department> GetAllDepartments()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Departments.ToList();
                return result;
            }
        }
        /// <summary>
        /// Создать отдел
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string CreateDepartment(string name)
        {
            string result = "Уже существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                bool checkIsExist = db.Departments.Any(el => el.Name == name);
                if (!checkIsExist)
                {
                    Department newDepartment = new Department { Name = name };
                    db.Departments.Add(newDepartment);
                    db.SaveChanges();
                    result = "Успешно!";
                }
                return result;
            }
        }
        /// <summary>
        /// Создать должность
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string CreatePosition(string name, decimal salary, int maxNumber, Department department)
        {
            string result = "Уже существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                bool checkIsExist = db.Positions.Any(el => el.Name == name && el.Salary == salary);
                if (!checkIsExist)
                {
                    Position newPosition = new Position
                    {
                        Name = name,
                        Salary = salary,
                        MaxNumber = maxNumber,
                        DepartmentId = department.Id
                    };
                    db.Positions.Add(newPosition);
                    db.SaveChanges();
                    result = "Успешно!";
                }
                return result;
            }
        }
        /// <summary>
        /// Удалить отдел
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string DeleteDepartment(Department department)
        {
            string result = "Такого отела не существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Departments.Remove(department);
                db.SaveChanges();
                result = "Успешно! Отдел " + department.Name + " удален";
            }
            return result;
        }
        /// <summary>
        /// Редактировать отдел
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string EditDepartment(Department oldDepartment, string newName)
        {
            string result = "Такого отдела не существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                Department department = db.Departments.FirstOrDefault(d => d.Id == oldDepartment.Id);
                department.Name = newName;
                db.SaveChanges();
                result = "Успешно! Отдел " + department.Name + " изменен";
            }
            return result;
        }
        #endregion

        #region POSITION
        /// <summary>
        /// Получить все должности
        /// </summary>
        /// <returns></returns>
        public static List<Position> GetAllPositions()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Positions.ToList();
                return result;
            }
        }
        /// <summary>
        /// Удалить должность
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string DeletePosition(Position position)
        {
            string result = "Такой должности не существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                //check position is exist
                db.Positions.Remove(position);
                db.SaveChanges();
                result = "Успешно! Должность " + position.Name + " удалена";
            }
            return result;
        }
        /// <summary>
        /// Редактировать должность
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string EditPosition(Position oldPosition, string newName, int newMaxNumber, decimal newSalary, Department newDepartment)
        {
            string result = "Такой должности не существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                Position position = db.Positions.FirstOrDefault(p => p.Id == oldPosition.Id);
                position.Name = newName;
                position.Salary = newSalary;
                position.MaxNumber = newMaxNumber;
                position.DepartmentId = newDepartment.Id;
                db.SaveChanges();
                result = "Успешно! Должность " + position.Name + " изменена";
            }
            return result;
        }
        #endregion

        #region USER
        /// <summary>
        /// Получить всех сотрудников
        /// </summary>
        /// <returns></returns>
        public static List<User> GetAllUsers()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var result = db.Users.ToList();
                return result;
            }
        }
        /// <summary>
        /// Создать работника
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string CreateUser(string name, string surName, string phone, Position position)
        {
            string result = "Уже существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                bool checkIsExist = db.Users.Any(el => el.Name == name && el.SurName == surName && el.Position == position);
                if (!checkIsExist)
                {
                    User newUser = new User
                    {
                        Name = name,
                        SurName = surName,
                        Phone = phone,
                        PositionId = position.Id
                    };
                    db.Users.Add(newUser);
                    db.SaveChanges();
                    result = "Успешно!";
                }
                return result;
            }
        }
        /// <summary>
        /// Удалить работника
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string DeleteUser(User user)
        {
            string result = "Такого работника не существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Users.Remove(user);
                db.SaveChanges();
                result = "Успешно! Работник " + user.Name + " уволен";
            }
            return result;
        }
        /// <summary>
        /// Редактировать работника
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string EditUser(User oldUser, string newName, string newSurName, string newPhone, Position newPosition)
        {
            string result = "Такого работника не существует";
            using (ApplicationContext db = new ApplicationContext())
            {
                //check user is exist
                User user = db.Users.FirstOrDefault(p => p.Id == oldUser.Id);
                if (user != null)
                {
                    user.Name = newName;
                    user.SurName = newSurName;
                    user.Phone = newPhone;
                    user.PositionId = newPosition.Id;
                    db.SaveChanges();
                    result = "Успешно! Работник " + user.Name + " изменен";
                }
            }
            return result;
        }
        #endregion

        #region GET BY ID
        /// <summary>
        /// Получение должности по id
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Position GetPositionById(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Position pos = db.Positions.FirstOrDefault(p => p.Id == id);
                return pos;
            }
        }
        /// <summary>
        /// Получение отдела по id
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static Department GetDepartmentById(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                Department pos = db.Departments.FirstOrDefault(p => p.Id == id);
                return pos;
            }
        }
        /// <summary>
        /// Получение всех работников по id должности
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static List<User> GetAllUsersByPositionId(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<User> users = (from user in GetAllUsers() where user.PositionId == id select user).ToList();
                return users;
            }
        }
        /// <summary>
        /// Получение всех должностей по id отдела
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static List<Position> GetAllPositionsByDepartmentId(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                List<Position> positions = (from position in GetAllPositions() where position.DepartmentId == id select position).ToList();
                return positions;
            }
        }
        #endregion
    }
}
