namespace GitHubCardApp.Win8
{
    using System;
    using System.ComponentModel;
    using System.Linq.Expressions;

    public class NotifyPropertyChanged : INotifyPropertyChanged
    {
        #region Implementation INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        protected void OnPropertyChanged(Expression<Func<object>> expression)
        {
            var member = expression.Body as MemberExpression;
            var unary = expression.Body as UnaryExpression;
            var underlyingMemberExpression = 
                    member ?? (unary != null ? unary.Operand as MemberExpression : null);

            if (underlyingMemberExpression == null)
            {
                return;
            }

            PropertyChanged(this, new PropertyChangedEventArgs(underlyingMemberExpression.Member.Name));
        }

        #endregion Implementation INotifyPropertyChanged
    }
}