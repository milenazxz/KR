using Dapper;
using Microsoft.EntityFrameworkCore;
using ProductAccounting.Forms;
using ProductAccounting.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows;

namespace ProductAccounting.Controllers
{
    public class UserController
    {
        private const int SaltSize = 16;
        private const int HashSize = 32;
        private const int Iterations = 100_000;
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public UserController()
        {

        }
        public async Task Registration(string password, UserUpdateDTO currentDTO) 
        {
            employees employee = null;
            using (var connection = _context.Database.GetDbConnection())
            {
                connection.Open();

                var request = "SELECT * FROM employees WHERE login = @Login";
                employee = connection.QueryFirstOrDefault<employees>(request, new { Login = currentDTO.Login });
            }
            using (ApplicationDbContext context = new ApplicationDbContext()) 
            {
                if (employee == null)
                {
                    if (LoginValidation(currentDTO.Login) && PasswordValidation(password))
                    {
                        string hashPassword = HashPassword(password);
                        
                            employees newEmployee = new employees { name = currentDTO.Name, post = currentDTO.Post, contacts = currentDTO.Post, login = currentDTO.Login, emp_password = hashPassword };
                            await context.AddAsync(newEmployee);
                            await context.SaveChangesAsync();
                      
                    }

                }
                else
                {
                    MessageBox.Show("Пользователь с таким логином уже существует");
                }
            }
                       
        }

        public bool PasswordValidation(string password) 
        {
            if (string.IsNullOrEmpty(password)) 
            {
                MessageBox.Show("Поле \"пароль\" не заполнено\n" +
                    "Пожалуйста, заполните поле" );
                return false;
            }

            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMinMaxChars = new Regex(@".{6,12}");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasSymbols = new Regex(@"[!@#$%^&*;?/-]");

            if (!hasNumber.IsMatch(password))
            {
                MessageBox.Show("Пароль должен содержать хотя бы одну цифру");
                return false;
            }
            else if (!hasUpperChar.IsMatch(password))
            {
                MessageBox.Show("Пароль должен содержать хотя бы одну латинскую заглавную бкуву");
                return false;
            }
            else if (!hasMinMaxChars.IsMatch(password))
            {
                MessageBox.Show("Пароль должен быть длинной не менее 6 и не более 12 символов");
                return false;
            }
            else if (!hasLowerChar.IsMatch(password))
            {
                MessageBox.Show("Пароль должен содержать хотя бы одну латинскую строчную бкуву");
                return false;
            }
            else if (!hasSymbols.IsMatch(password)) 
            {
                MessageBox.Show("Пароль должен содержать хотя бы один из символов ! @ # $ % ^ & * ; ? / -");
                return false;
            }
            else { return true; }
        }

        public bool LoginValidation(string login)
        {
            if (string.IsNullOrEmpty(login))
            {
                MessageBox.Show("Поле \"Логин\" не заполнено\n" +
                    "Пожалуйста, заполните поле");
                return false;
            }

            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMinMaxChars = new Regex(@".{6,12}");
            var hasLowerChar = new Regex(@"[a-z]+");

            if (!hasNumber.IsMatch(login))
            {
                MessageBox.Show("Логин должен содержать хотя бы одну цифру");
                return false;
            }
            else if (!hasUpperChar.IsMatch(login))
            {
                MessageBox.Show("Логин должен содержать хотя бы одну латинскую заглавную бкуву");
                return false;
            }
            else if (!hasMinMaxChars.IsMatch(login))
            {
                MessageBox.Show("Логин должен быть длинной не менее 6 и не более 12 символов");
                return false;
            }
            else if (!hasLowerChar.IsMatch(login))
            {
                MessageBox.Show("Логин должен содержать хотя бы одну латинскую строчную бкуву");
                return false;
            }
            else { return true; }
        }

        private string HashPassword(string password) 
        {
            byte[] salt = new byte[SaltSize];
            using (var rand = RandomNumberGenerator.Create()) 
            {
                rand.GetBytes(salt);
            }
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256)) 
            {
                byte[] hash = pbkdf2.GetBytes(HashSize);

                byte[] hashBytes = new byte[SaltSize + HashSize];
                Array.Copy(salt, 0, hashBytes, 0, SaltSize);
                Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);
                return Convert.ToBase64String(hashBytes);
            }
        }

        private bool VerifyPassword(string password, string storedHash) 
        {
            byte[] hashBytes = Convert.FromBase64String(storedHash);

            //Извлекаем соль
            byte[] salt = new byte[SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, SaltSize);

            // Вычисляем хэш для введённого пароля
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithmName.SHA256)) 
            {
                byte[] hash = pbkdf2.GetBytes(HashSize);
                // Сравниваем байты
                for (int i = 0; i < HashSize; i++)
                {
                    if (hashBytes[i + SaltSize] != hash[i])
                        return false;
                }
            }
            return true;

        }

        public CurrentUserData Authorization(string inpLogin, string inpPassword, string inpRole) 
        {
            employees employee = null;
            var connection = _context.Database.GetDbConnection();
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
            string request = "SELECT * FROM employees WHERE login = @Login AND post = @Role";
            employee = connection.QueryFirstOrDefault<employees>(request, new { Login = inpLogin, Role = inpRole });
            if (VerifyPassword(inpPassword, employee.emp_password))
            {
                CurrentUserData currentUser = new CurrentUserData(employee.name, employee.post);
                connection.Close();
                return currentUser;
            }
            connection.Close();
            return null;
            
        }

        public async Task<List<employees>> LoadDataUsers()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                return await context.Set<employees>().ToListAsync();
            }
        }

        public async Task<bool> DeleteUser(IList items)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                foreach(var item in items)
                {
                    if(item is employees user)
                    {
                        await DbFunctions.DeleteItem<employees>(user, i => i.id == user.id);
                    }
                }
                return true;
            }
        }

        public async Task<UserDTO> LoadDataUser(int ID) 
        {
            using(ApplicationDbContext context = new ApplicationDbContext()) 
            {
                employees userData = await context.Set<employees>().FirstOrDefaultAsync(u => u.id == ID);
                UserDTO user = new UserDTO
                {
                    Name = userData.name,
                    Post = userData.post,
                    Contacts = userData.contacts,
                    Login = userData.login,
                };
                return user;
            }
        }
        public async Task<bool> ChangeDataUser(int ID, UserUpdateDTO currentDTO, List<string> changesFields)
        {
            using(ApplicationDbContext context = new ApplicationDbContext())
            {
                employees userData = await context.Set<employees>().FirstOrDefaultAsync(u => u.id == ID);
                if (userData == null) return false;

                if (changesFields.Contains(nameof (UserUpdateDTO.Name)))
                    userData.name = currentDTO.Name;
                if(changesFields.Contains(nameof(UserDTO.Post)))
                    userData.post = currentDTO.Post;
                if(changesFields.Contains(nameof(UserDTO.Login)))
                    userData.login = currentDTO.Login;
                if(changesFields.Contains(nameof(UserDTO.Contacts)))
                    userData.contacts = currentDTO.Contacts;
                await DbFunctions.ChangeData<employees>(userData);
                return true;
            }
        }
    }
}
