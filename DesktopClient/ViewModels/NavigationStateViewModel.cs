using System;

namespace DesktopClient.ViewModels
{
    public class NavigationStateViewModel : BaseViewModel
    {
        private readonly Action _action;
        private readonly bool _isCurrentState;

        public NavigationStateViewModel(Action action, bool isCurrenState, string displayName)
        {
            _action = action;
            _isCurrentState = isCurrenState;

            DisplayName = displayName;
        }

        public bool IsCurrentState
        {
            get { return _isCurrentState; }
            set
            {
                if (value)
                {
                    _action();
                }
            }
        }

        public string DisplayName { get; set; }
    }
}
