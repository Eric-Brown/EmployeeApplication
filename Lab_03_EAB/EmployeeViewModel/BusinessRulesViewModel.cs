﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Lab_03_EAB.Helpers;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Lab_03_EAB.EmployeeViewModel
{
    class BusinessRulesViewModel : INotifyPropertyChanged
    {
        #region Constants
        private const string
            ERROR_CAPTION = "Error",
            NEW_EMP_LINE = "-------New Employee-------\n",
            INVALID_EMP_TYPE_ERROR = "Invalid Employee Type",
            ALL_EMP_LINE = "\n------All Employees-----";
        /// <summary>
        /// Used to create random first names for employees.
        /// </summary>
        private static readonly string[] FIRST_NAMES = {
            "D'Marcus", "T'varisuness", "Tyroil", "D'Squarius", "Ibrahim",
            "T.J.", "Jackmerius", "D'Isiah", "D'Jasper", "Leoz", "Javaris", "Davoin",
            "Grant", "Hingle", "L'Carpetron", "J'Dinkalage", "Xmus Jaxon",
            "Greg", "Saggitariutt", "D'Glester", "Swirvithan", "Quatro", "Beezer",
            "Micheal", "Shakiraquan", "X-Wing", "Sequester", "Scoish", "T.J. A.J.",
            "Seth", "Donkey", "Torque", "Eeeeeee", "Coznesster", "Elipses", "Nyquillus",
            "Anthony", "Bismo", "Decatholac", "Mergatroid", "Quiznatodd", "D'Pez", "Quackadilly",
            "Matthew", "Goolius", "Bisquiteen", "Fartrell", "Blyrone", "Cartoons", "Jammie",
            "Jonathon", "Equine", "Dahistorius", "Ewokoniad", "Eqqsnuizitine", "Huka'laknaka",
            "Jenny", "King", "Ladennifer", "Harvard", "Firstname", "Creme", "Cosgrove", "Ha Ha",
            "Sam", "Doink", "Legume", "Leger", "Quisperny", "Grunky","D'Brickashaw", "Strunk",
            "Stumptavian", "Cornelius", "Vagonius", "Marmadune","Swordless", "Prince", "J.R. Junior",
            "Faux", "Fozzy", "Myriad", "Busters", "Turdine", "Rerutweeds", "Ishmaa'ily", "Takittothu'",
            "Snarf", "Frostee", "Splendiferous", "Triple", "Logjammer"
        };
        /// <summary>
        /// Used to create random last names for employees.
        /// </summary>
        private static readonly string[] LAST_NAMES = {
            "Linder", "Williums", "juckson", "King", "Smoochie-Wallace", "Green",
            "Brown", "Moizoos", "Tacktheritrix", "Billings-Clyde", "Probincrux III",
            "DePoirot", "Jilliumz", "Javarison-Lamar", "Shower-Handel", "McCringleberry",
            "Johnson", "Dookmarriot", "Morgoone", "Flaxon-Waxon", "Jefferspin", "Hardunkichud",
            "Williams", "L'Goodling-Splatt", "Quatro", "Buckshank", "Washingbeard", "Carter",
            "Xavier", "@Aliciousness", "Grundelplith", "Maloish", "Backslishinfourth V",
            "Green", "Eeeeeeee", "Teeth", "Lewith", "Smiff", "Corter", "Dillwad", "Funyuns",
            "Goldberg", "Mango", "Skittle", "Bidness", "Poopsie", "Blip", "Boozler", "Trisket",
            "Greenburg", "Cluggins", "Blashington", "Plural", "Jammie-Jammie", "Ducklings",
            "Flotsam", "Lamystorius", "Sigourneth JuniorStein", "Buble-Schineslow", "Hakanakaheekalucka'hukahakafaka",
            "Jenkins", "Chambermaid", "Jadaniston", "University", "Lastname", "De La Creme",
            "Jensen", "Shumway", "Clinton-Dix", "Ahanahue","Duprix","Douzable","G'Dunzoid Sr.",
            "Null", "Peep", "Ferguson","Flugget","Roboclick", "Carradine","Thicket-Suede","Shazbot",
            "Mimetown", "Amukamara","Juniors Jr.", "Doadles","Whittaker","Profiteroles","Brownce",
            "Cupcake","Myth","Kitchen","Limit","Mintz-Plasse","Rucker","Finch","Parakeet-Shoes",
            "D'Baggagecling", "Ron Rodgers"
        };
        /// <summary>
        /// Default number of random employees to create when creating test employees.
        /// </summary>
        private const int DEFAULT_NUM_TEST_EMPS = 10;
        private const string OPEN_FILE_MSG = "This file is already open.";
        #endregion
        public event PropertyChangedEventHandler PropertyChanged;

        #region Data Properties
        private Employee selectedEmployee;
        public Employee SelectedEmployee
        {
            get => selectedEmployee;
            set
            {
                selectedEmployee = (Employee)value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(SelectedEmployee)));
                SelectedEmployeeDescription = selectedEmployee.ToString();
            }
        }

        private void OnPropertyChanged(PropertyChangedEventArgs propertyChangedEventArgs)
        {
            PropertyChanged?.Invoke(this, propertyChangedEventArgs);
        }

        public int Index {
            get;
            set; 
}
        private BusinessRules employees;
        public BusinessRules Employees
        {
            get => employees;
            set
            {
                employees = (BusinessRules)value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(Employees)));
            }
        }
        private bool? closeWindowFlag;
        public bool? CloseWindowFlag
        {
            get => closeWindowFlag;
            set
            {
                closeWindowFlag = value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(CloseWindowFlag)));
            }
        }
        private ObservableCollection<BusinessRules> employeesCollections;
        public ObservableCollection<BusinessRules> EmployeesCollections
        {
            get => employeesCollections;
            set
            {
                employeesCollections = (ObservableCollection<BusinessRules>)value;
                OnPropertyChanged(new PropertyChangedEventArgs(nameof(EmployeesCollections)));
            }
        }
        private string selectedEmployeeDescr;
        public string SelectedEmployeeDescription
        {
            get => selectedEmployeeDescr;
            private set
            {
                selectedEmployeeDescr = value;
                PropertyChanged?.Invoke(this, (new PropertyChangedEventArgs(nameof(SelectedEmployeeDescription))));
            }
        }
        #endregion
        #region Command Properties

        public RelayCommand RemoveEmployeeCommand
        {
            get; set;
        }
        private void RemoveEmployee(object employee)
        {
            employees.Remove(employee as Employee);
        }

        public RelayCommand MoveEmployeeCommand
        {
            get; set;
        }
        public RelayCommand AddEmployeeCommand
        {
            get;set;
        }
        private void AddEmployee(object parameter)
        {
            Add_Emp_Window add_Emp_Window = new Add_Emp_Window();
            add_Emp_Window.DataContext = new EmployeeViewModel(Employees);
            add_Emp_Window.Show();
        }

        public RelayCommand ModifyEmployeeCommand
        {
            get;set;
        }
        private void ModifyEmployee(object employee)
        {
            Add_Emp_Window add_Emp_Window = new Add_Emp_Window();
            Employee toPass = employee as Employee;
            add_Emp_Window.DataContext = new EmployeeViewModel(toPass, Employees);
            add_Emp_Window.Show();
        }

        public RelayCommand SaveFileCommand
        {
            get; set;
        }
        private void SaveFile(object parameter)
        {
            try
            {
                using (FileIO thefile = new FileIO(Employees))
                {
                    thefile.OpenSaveFileDB();
                    thefile.WriteFileDB();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public RelayCommand OpenFileCommand
        {
            get; set;
        }
        private void OpenFile(object parameter)
        {
            BusinessRules toAdd;
            try
            {
                using (FileIO theFile = new FileIO(toAdd = new BusinessRules()))
                {
                    theFile.OpenFileDB();
                    theFile.ReadFileDB();
                    if (EmployeesCollections.Where(a => a.FilePath == toAdd.FilePath).Count() == 0)
                        EmployeesCollections.Add(toAdd);
                    else MessageBox.Show(OPEN_FILE_MSG, ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ERROR_CAPTION, MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public RelayCommand NewFileCommand
        {
            get; set;
        }
        private void NewFile(object parameter)
        {
            EmployeesCollections.Add(new BusinessRules());
        }

        public RelayCommand CreateTestEmployeesCommand
        {
            get; set;
        }
        private void CreateTestEmployees(object parameter)
        {
            Random random = new Random();
            //RTBxOutput.Document.Blocks.Clear();
            BusinessRules toAdd = new BusinessRules();
            int numETypes = Enum.GetNames(typeof(ETYPE)).Length;
            //Iterate through the numbers and choose randomly from the first and last names. Other values are randomly generated using Random
            for (int i = 0; i < DEFAULT_NUM_TEST_EMPS; i++)
            {
                switch ((ETYPE)(i % numETypes))
                {
                    case ETYPE.CONTRACT:
                        toAdd.Add(new Contract((uint)i,
                            FIRST_NAMES.ElementAt(random.Next(0, FIRST_NAMES.Length - 1)),
                            LAST_NAMES.ElementAt(random.Next(0, LAST_NAMES.Length - 1)),
                            (decimal)(random.NextDouble() * random.Next())));
                        break;
                    case ETYPE.HOURLY:
                        toAdd.Add(new Hourly((uint)i,
                            FIRST_NAMES.ElementAt(random.Next(0, FIRST_NAMES.Length - 1)),
                            LAST_NAMES.ElementAt(random.Next(0, LAST_NAMES.Length - 1)),
                            (decimal)(random.NextDouble() * random.Next()),
                            random.NextDouble() * random.Next()));
                        break;
                    case ETYPE.SALARY:
                        toAdd.Add(new Salary((uint)i,
                            FIRST_NAMES.ElementAt(random.Next(0, FIRST_NAMES.Length - 1)),
                            LAST_NAMES.ElementAt(random.Next(0, LAST_NAMES.Length - 1)),
                            (decimal)(random.NextDouble() * random.Next())));
                        break;
                    case ETYPE.SALES:
                        toAdd.Add(new Sales((uint)i,
                            FIRST_NAMES.ElementAt(random.Next(0, FIRST_NAMES.Length - 1)),
                            LAST_NAMES.ElementAt(random.Next(0, LAST_NAMES.Length - 1)),
                            (decimal)(random.Next() * random.NextDouble()),
                            (decimal)(random.Next() * random.NextDouble()),
                            (decimal)(random.Next() * random.NextDouble())));
                        break;
                }//End Switch
            }//End for loop
            EmployeesCollections.Add(toAdd);
        }

        #endregion

        public BusinessRulesViewModel()
        {
            EmployeesCollections = new ObservableCollection<BusinessRules>();
            CreateTestEmployees(null);
            Employees = EmployeesCollections.First<BusinessRules>();
            //Set up commands next
            CreateTestEmployeesCommand = new RelayCommand(CreateTestEmployees);
            SaveFileCommand = new RelayCommand(SaveFile);
            NewFileCommand = new RelayCommand(NewFile);
            OpenFileCommand = new RelayCommand(OpenFile);
            RemoveEmployeeCommand = new RelayCommand(RemoveEmployee);
            ModifyEmployeeCommand = new RelayCommand(ModifyEmployee);
            AddEmployeeCommand = new RelayCommand(AddEmployee);
        }





    }//End Class BusinessRulesViewModel
}//End Namespace
