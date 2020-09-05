using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using RomashkaClientBase.Model;

namespace RomashkaClientBase.ViewModel
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));   
        
        public ObservableCollection<Company> Companies { get; private set; }
        public ObservableCollection<User> Users { get; private set; }

        public MainWindowViewModel()
        {
            Companies = new ObservableCollection<Company>(CompaniesManager.GetAllCompanies());
            Users = new ObservableCollection<User>();

            InitCommands();
        }

        #region SelectedItems
        private User selectedUser;
        public User SelectedUser
        {
            get { return selectedUser; }
            set
            {
                selectedUser = value;
                OnPropertyChanged();
            }
        }

        private Company selectedCompany;
        public Company SelectedCompany
        {
            get { return selectedCompany; }
            set
            {
                selectedCompany = value;
                OnPropertyChanged();

                if(selectedCompany != null) 
                { 
                    Users = new ObservableCollection<User>(UsersManager.GetUsersByCompany(selectedCompany));
                    OnPropertyChanged(nameof(Users));
                    //OnPropertyChanged(nameof(SelectedUser));
                }
            }
        }
        #endregion

        #region Commands
        private void InitCommands()
        {
            AddCompanyCommand = new Command(OnAddCompanyCommandExecuted);
            DeleteCompanyCommand = new Command(OnDeleteCompanyCommandExecuted, CanDeleteCompanyCommandExecute);
            SaveCompaniesChangesCommand = new Command(OnSaveCompaniesChangesCommandExecuted);

            AddUserCommand = new Command(OnAddUserCommandExecuted,CanAddUserCommandExecute);
            DeleteUserCommand = new Command(OnDeleteUserCommandExecuted, CanDeleteUserCommandExecute);
            SaveUsersChangesCommand = new Command(OnSaveUsersChangesCommandExecuted);

            // данные для примера
            InitDBDataCommand = new Command(OnInitDBDataExecuted);
        }

        #region InitDBData - данные для примера
        public ICommand InitDBDataCommand { get; private set; }

        private void OnInitDBDataExecuted(object parameter)
        {
            var companies = 
                new []
                { 
                    new Company {Name = "1 company", ContractStatus = ContractStatuses.NotYetConcluded}, 
                    new Company {Name = "2 company", ContractStatus = ContractStatuses.Concluded}, 
                    new Company {Name = "3 company", ContractStatus = ContractStatuses.Terminated}     
                };

            int usersCount = 6;

            foreach (var company in companies)
            {
                var users = new List<User>(usersCount);
                
                for (int i = 0; i < usersCount; ++i)
                {
                    users.Add(
                        new User
                        {
                            Company = company,
                            Name = $"user {i+1} of \"{company.Name}\"",
                            Login = $"user{i+1}",
                            Password = String.Concat(Enumerable.Repeat($"{i}", 6))
                        });
                }
                
                company.Users = users;
                CompaniesManager.AddCompany(company);

                usersCount -= 2;
            }

            Companies = new ObservableCollection<Company>(CompaniesManager.GetAllCompanies());
            OnPropertyChanged(nameof(Companies));
        }
        #endregion

        #region AddCompany
        public ICommand AddCompanyCommand { get; private set; }

        private void OnAddCompanyCommandExecuted(object parameter)
        {
            var newCompany = new Company() 
            {
                Name = "Наименование",
                ContractStatus = ContractStatuses.NotYetConcluded 
            };

            CompaniesManager.AddCompany(newCompany);
            Companies.Add(newCompany);
            SelectedCompany = newCompany;
        }
        #endregion

        #region DeleteCompany
        public ICommand DeleteCompanyCommand { get; private set; }

        private void OnDeleteCompanyCommandExecuted(object parameter)
        {
            var company = selectedCompany;

            CompaniesManager.DeleteCompany(company);
            Companies.Remove(company);

            Users.Clear();
        }

        private bool CanDeleteCompanyCommandExecute(object parameter) => SelectedCompany != null;
        #endregion

        #region SaveCompaniesChanges
        public ICommand SaveCompaniesChangesCommand { get; private set; }

        private void OnSaveCompaniesChangesCommandExecuted(object parameter)
        {
            foreach (var company in Companies)
                CompaniesManager.UpdateCompany(company);
        }
        #endregion

        #region AddUser
        public ICommand AddUserCommand { get; private set; }

        private void OnAddUserCommandExecuted(object parameter)
        {
            var newUser = new User() 
            {
                Name = "Имя",
                Login = "Логин",
                Password = "Пароль",
                CompanyId = selectedCompany.Id
            };

            UsersManager.AddUser(newUser);
            Users.Add(newUser);
            selectedUser = newUser;

            OnPropertyChanged(nameof(SelectedUser));
        }

        private bool CanAddUserCommandExecute(object parameter) => SelectedCompany != null;
        #endregion

        #region DeleteUser
        public ICommand DeleteUserCommand { get; private set; }

        private void OnDeleteUserCommandExecuted(object parameter)
        {
            var user = selectedUser;

            UsersManager.DeleteUser(user);
            Users.Remove(user);

            OnPropertyChanged(nameof(SelectedUser));
        }

        private bool CanDeleteUserCommandExecute(object parameter) => SelectedUser != null;
        #endregion

        #region SaveUsersChanges
        public ICommand SaveUsersChangesCommand { get; private set; }

        private void OnSaveUsersChangesCommandExecuted(object parameter)
        {
            foreach (var user in Users)
                UsersManager.UpdateUser(user);
        }
        #endregion

        #endregion
    }
}
