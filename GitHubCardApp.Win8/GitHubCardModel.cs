
namespace GitHubCardApp.Win8
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Octokit;

    public class GitHubCardModel : NotifyPropertyChanged
    {
        #region Private Fields

        private string _name;
        private Uri _avatarUrl;
        private string _blogAddress;
        private DateTime _joinDate;
        private int _numFollowers;
        private int _totalPublicRepositories;
        private List<string> _repositories;

        #endregion Private Fields

        #region Properties

        public List<string> Repositories
        {
            get { return _repositories; }
            internal set
            {
                _repositories = value;
                OnPropertyChanged(() => Repositories);
            }
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
                OnPropertyChanged(() => TotalPublicRepositories);
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
                OnPropertyChanged(() => NumFollowers);
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
                OnPropertyChanged(() => JoinDate);
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
                OnPropertyChanged(() => Name);
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
                OnPropertyChanged(() => AvatarUrl);
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
                OnPropertyChanged(() => BlogAddress);
            }
        }

        #endregion Properties

        #region Methods

        public async Task DownloadUserInformationAsync(string login, string pwd)
        {
            var github = new GitHubClient(new ProductHeaderValue("GitHubCardApp"))
            {
                Credentials = new Credentials(login, pwd)
            };

            var user = await github.User.Get(login);
            var repositories = await github.Repository.GetAllForUser(login);

            Name = user.Name;
            AvatarUrl = new Uri(user.AvatarUrl);
            JoinDate = user.CreatedAt.DateTime;
            NumFollowers = user.Followers;
            BlogAddress = user.Blog;
            TotalPublicRepositories = user.PublicRepos;
            Repositories = repositories.Select(repository => repository.FullName).ToList();
        }

        #endregion Methods
    }
}