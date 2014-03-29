using AutoMapper;
using Microsoft.Practices.Prism.StoreApps;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml.Navigation;

namespace GitHubCardApp.Win8
{
    public class GitHubCardPrismViewModel : ViewModel
    {
        #region Private Fields

        private string _name;
        private Uri _avatarUrl;
        private string _blogAddress;
        private DateTime _joinDate;
        private int _numFollowers;
        private int _totalPublicRepositories;
        private ObservableCollection<string> _repositories;
        ICommand _loadUserInformationCommand;

        #endregion Private Fields

        static GitHubCardPrismViewModel()
        {
            Mapper.CreateMap<GitHubCardModel, GitHubCardViewModel>();
        }

        public GitHubCardPrismViewModel()
        {
            // calling an async method from constructor
            //LoadUserInformationAsync();
        }

        #region Properties

        public ObservableCollection<string> Repositories
        {
            get { return _repositories ?? (_repositories = new ObservableCollection<string>()); }
        }

        public int TotalPublicRepositories
        {
            get { return _totalPublicRepositories; }
            set
            {
                if (_totalPublicRepositories == value)
                {
                    return;
                }

                _totalPublicRepositories = value;
                OnPropertyChanged("TotalPublicRepositories");
            }
        }

        public int NumFollowers
        {
            get { return _numFollowers; }
            set
            {
                if (_numFollowers == value)
                {
                    return;
                }

                _numFollowers = value;
                OnPropertyChanged("NumFollowers");
            }
        }

        public DateTime JoinDate
        {
            get { return _joinDate; }
            set
            {
                if (_joinDate == value)
                {
                    return;
                }

                _joinDate = value;
                OnPropertyChanged("JoinDate");
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (string.Equals(_name, value))
                {
                    return;
                }

                _name = value;
                OnPropertyChanged("Name");
            }
        }

        public Uri AvatarUrl
        {
            get { return _avatarUrl; }
            set
            {
                if (_avatarUrl == value)
                {
                    return;
                }

                _avatarUrl = value;
                OnPropertyChanged("AvatarUrl");
            }
        }

        public string BlogAddress
        {
            get { return _blogAddress; }
            set
            {
                if (string.Equals(_blogAddress, value))
                {
                    return;
                }

                _blogAddress = value;
                OnPropertyChanged("BlogAddress");
            }
        }

        #endregion Properties

        #region Public Methods

        public async override void OnNavigatedTo(
            object navigationParameter, 
            NavigationMode navigationMode, 
            Dictionary<string,object> viewModelState)
        {
            base.OnNavigatedTo(navigationParameter, navigationMode, viewModelState);

            await LoadUserInformationAsync();
        }

        //public async Task LoadUserInformationAsync()
        private async Task LoadUserInformationAsync()
        {
            var model = new GitHubCardModel();

            await model.DownloadUserInformationAsync("myUserName", "myPwd");
            
            Mapper.Map(model, this);

            this.JoinDate = DateTime.Now;
        }

        #endregion Public Methods
    }
}
