using ProductAccounting.Controllers;
using ProductAccounting.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ProductAccounting.Forms
{
    /// <summary>
    /// Логика взаимодействия для FormForEmployees.xaml
    /// </summary>
    
    public class EmployeeDTO
    {
        private string _name { get; set; }
        private string _post { get; set; }
        private string _contacts { get; set; }

        public string Name 
        {
            get => _name;
            set
            {
                _name = value;
            }
        }
        public string Post
        {
            get => _post;
            set
            {
                _post = value;
            }

        }
        public string Contacts
        {
            get => _contacts;
            set
            {
                _contacts = value;
            }
        }
    }

    public class EmployeeUpdateDTO:INotifyPropertyChanged
    {
        private string _name { get; set; }
        private string _post { get; set; }
        private string _contacts { get; set; }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }
        public string Post
        {
            get => _post;
            set
            {
                _post = value;
                OnPropertyChanged();
            }

        }
        public string Contacts
        {
            get => _contacts;
            set
            {
                _contacts = value;
                OnPropertyChanged();
            }
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }

    }
    public static class EmployeeComparer
    {
        public static List<string> GetChangedFields(EmployeeDTO originalDTO, EmployeeUpdateDTO currentDTO)
        {
            List<string> changedFields = new List<string>();
            if (originalDTO.Name != currentDTO.Name) changedFields.Add(nameof(currentDTO.Name));
            if (originalDTO.Post != currentDTO.Post) changedFields.Add(nameof(currentDTO.Post));
            if (originalDTO.Contacts != currentDTO.Contacts) changedFields.Add(nameof(currentDTO.Contacts));
            return changedFields;
        }
    }
    public partial class FormForEmployees : Window
    {
        EmployeesController controller = new EmployeesController();
        private EmployeeDTO _origrnalDTO;
        private EmployeeUpdateDTO _currentDTO;
        private bool _forCreate;
        private int _idEmployee;
        public FormForEmployees()
        {
            InitializeComponent();
            _currentDTO = new EmployeeUpdateDTO();
            this.DataContext = _currentDTO;
            _forCreate = true;
        }
        public FormForEmployees(int ID)
        {
            InitializeComponent();
            _currentDTO = new EmployeeUpdateDTO();
            this.DataContext = _currentDTO;
            _idEmployee = ID;
            _forCreate = false;
            Loaded += async (s, e) =>
            {
                await LoadSupplierData(ID);
            };
        }
        public async Task LoadSupplierData(int ID)
        {
            _origrnalDTO = await controller.LoadDataEmp(ID);
            _currentDTO = new EmployeeUpdateDTO
            {
                Name = _origrnalDTO.Name,
                Post = _origrnalDTO.Post,
                Contacts = _origrnalDTO.Contacts,
            };
            NameEmployee.Text = _currentDTO.Name;
            PostEmployee.Text = _currentDTO.Post;
            ContacntEmployee.Text = _currentDTO.Contacts;
        }
        public async void AddEmployee(object sender, EventArgs e)
        {
            _currentDTO.Name = NameEmployee.Text;
            _currentDTO.Post = PostEmployee.Text;
            _currentDTO.Contacts = ContacntEmployee.Text;
            
            if (!string.IsNullOrEmpty(_currentDTO.Name) 
                && !string.IsNullOrEmpty(_currentDTO.Post)
                && !string.IsNullOrEmpty(_currentDTO.Contacts)) 
            {
                if (_forCreate)
                {
                    bool succses = await controller.AddEmp(_currentDTO);
                    if (succses)
                    {
                        if (succses)
                        {
                            DialogResult = true;
                        }
                        else
                        {
                            MessageBox.Show("Возникла ошибка при добавлении поставщика");
                        }
                    }
                }
                else
                {
                    List<string> changes = EmployeeComparer.GetChangedFields(_origrnalDTO, _currentDTO);
                    
                    if(changes.Count == 0)
                    {
                        MessageBox.Show("Нет изменений для сохранения");
                        return;
                    }
                    else
                    {
                        bool success = await controller.ChangeEmployee(_idEmployee, _currentDTO, changes);
                        if (success) DialogResult = true;
                        else MessageBox.Show("Ошибка при обновлении склада");
                        Logger.Log($"Пользователь внес изменения в справочник сотрудники {DateTime.Now} \n");
                    }
                }
                
            }
            else
            {
                MessageBox.Show("Пожалуйста, заполните все поля формы");
            }

        }


    }

       
    }


