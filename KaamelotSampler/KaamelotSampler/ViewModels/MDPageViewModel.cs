using KaamelotSampler.Models;
using KaamelotSampler.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;

namespace KaamelotSampler.ViewModels
{
    public class MDPageViewModel : BaseViewModel
    {
        public MDPageViewModel()
        {
            ListeMenus = new List<MDMenuItem>();
            ListeMenus.Add(new MDMenuItem() { Title = "Liste des Saamples", TargetType = typeof(ListSamplesPage) });
            ListeMenus.Add(new MDMenuItem() { Title = "A Propos", TargetType = typeof(AboutPage) });
            SelectedMenu = listeMenus[0];
            CheckAndRequestPermission();
        }

        private List<MDMenuItem> listeMenus;
        public List<MDMenuItem> ListeMenus
        {
            get { return listeMenus; }
            set
            {
                listeMenus = value;
                OnPropertyChange("ListeMenus");
            }
        }

        private MDMenuItem selectedMenu;
        public MDMenuItem SelectedMenu
        {
            get { return selectedMenu; }
            set
            {
                selectedMenu = value;
                OnPropertyChange("SelectedMenu");
            }
        }

        public async void CheckAndRequestPermission()
        {
            var result = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

            if (result != PermissionStatus.Granted)
            {
                result = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();
            }

            if (result != PermissionStatus.Granted)
            {
                //Display
            }

        }

    }
}
