﻿using EBana.WpfUI.Core;
using EBana.WpfUI.Core.Command;
using EBana.WpfUI.Core.ViewModel;
using System;
using System.Windows.Input;

namespace EBana.WpfUI.ViewModels
{
    public class MaintenanceMenuViewModel : Notifier
    {
        public ICommand GoToArticlesUpdaterCommand { get; private set; }
        public ICommand GoToPictureManagerCommand { get; private set; }
        public ICommand GoToPasswordUpdaterCommand { get; private set; }

        private readonly INavigationService navigator;

        public MaintenanceMenuViewModel(INavigationService navigator)
        {
            if (navigator == null)
                throw new ArgumentNullException(nameof(navigator));

            this.navigator = navigator;

            GoToArticlesUpdaterCommand = new RelayCommand(GoToArticlesUpdater);
            GoToPictureManagerCommand = new RelayCommand(GoToPictureManager);
            GoToPasswordUpdaterCommand = new RelayCommand(GoToPasswordUpdater);
        }

        private void GoToArticlesUpdater()
        {
            navigator.NavigateTo("ArticlesUpdater");
        }

        private void GoToPictureManager()
        {
            navigator.NavigateTo("PictureManager");
        }

        private void GoToPasswordUpdater()
        {
            navigator.NavigateTo("PasswordUpdater");
        }
    }
}
