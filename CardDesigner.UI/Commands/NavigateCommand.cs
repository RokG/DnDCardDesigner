﻿using CardDesigner.Domain.Interfaces;
using CardDesigner.Domain.Services;
using CardDesigner.Domain.Stores;
using CardDesigner.UI.ViewModels;
using System;

namespace CardDesigner.UI.Commands
{
    public class NavigateCommand<TViewModel> : CommandBase where TViewModel : IViewModelBase
    {
        private readonly NavigationService<TViewModel> _navigationService;

        public NavigateCommand(NavigationService<TViewModel> navigationService)
        {
            _navigationService = navigationService;
        }

        public override void Execute(object? parameter)
        {
            _navigationService.Navigate();
        }
    }
}