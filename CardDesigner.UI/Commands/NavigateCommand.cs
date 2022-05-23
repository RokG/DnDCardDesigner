using CardDesigner.Domain.Services;
using CardDesigner.Domain.Stores;
using CardDesigner.UI.ViewModels;
using System;

namespace CardDesigner.UI.Commands
{
    public class NavigateCommand : CommandBase
    {
        private readonly NavigationService _navigationService;

        public NavigateCommand(NavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public override void Execute(object? parameter)
        {
            _navigationService.Navigate();
        }
    }
}