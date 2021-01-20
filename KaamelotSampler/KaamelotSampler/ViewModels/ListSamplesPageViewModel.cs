using KaamelotSampler.DataAccessLayer;
using KaamelotSampler.Interfaces;
using KaamelotSampler.Models;
using KaamelotSampler.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace KaamelotSampler.ViewModels
{
    public class ListSamplesPageViewModel : BaseViewModel 
    {
        private INavigation NavigationService;
        public ListSamplesPageViewModel(INavigation navigationService)
        {
            NavigationService = navigationService;
            ClearCommand = new Command(ClearCommandExecute, ClearCommandCanExecute);
            SelectionCommand = new Command(SelectionCommandExecute);
            
            LoadSamples();
            MyTitre = $"Liste des Saamples";

            using (var db = new KaamelottDB())
            {
                db.Database.EnsureCreated();
            }
        }

        

        #region BindableProperties/Commands

        private string searchtext;
        public string SearchText
        {
            get { return searchtext; }
            set 
            { 
                searchtext = value;
                OnPropertyChange("SearchText");
                FilterSamples();
                ClearCommand.ChangeCanExecute();
            }
        }


        private List<string> listePerso;
        public List<string> ListePerso
        {
            get { return listePerso; }
            set 
            { 
                listePerso = value;
                OnPropertyChange("ListePerso");
            }
        }


        private string selectedPerso;
        public string SelectedPerso
        {
            get { return selectedPerso; }
            set 
            { 
                selectedPerso = value;
                OnPropertyChange("SelectedPerso");
                FilterSamples();
                ClearCommand.ChangeCanExecute();
            }
        }

        private List<Saample> AllSamples;
        private List<Saample> listeSaamples;
        public List<Saample> ListeSaamples
        {
            get { return listeSaamples; }
            set 
            { 
                listeSaamples = value;
                OnPropertyChange("ListeSaamples");
            }
        }


        private Saample currentSample;
        public Saample CurrentSample
        {
            get { return currentSample; }
            set
            {
                currentSample = value;
                OnPropertyChange("CurrentSample");
            }
        }


        public Command ClearCommand { get; }
        private void ClearCommandExecute()
        {
            IsBusy = true;
            SearchText = null;
            SelectedPerso = null;
            FilterSamples();
            IsBusy = false;
        }

        private bool ClearCommandCanExecute()
        {
            if (String.IsNullOrWhiteSpace(SearchText) && String.IsNullOrWhiteSpace(SelectedPerso))
            {
                return false;
            }
            else
                return true;
        }

        public ICommand SelectionCommand { get; }
        private void SelectionCommandExecute()
        {

            NavigationService.PushAsync(new DetailSamplePage(CurrentSample));
            
        }

        #endregion

        #region Private Methods

        private void LoadSamples()
        {
            IsBusy = true;
            Task.Factory.StartNew(async () =>
            {
                //Si on a internet, on synchronise (Api -> DB)
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    KaamelottAPI api = new KaamelottAPI();
                    var result = await api.GetSaamplesListAsync();
                    if (result != null && result.Count > 0)
                    {
                        using (var db = new KaamelottDB())
                        {
                            foreach (var apisample in result)
                            {
                                if (db.Saamples.Where(x => x.Title == apisample.Title).FirstOrDefault() == null)
                                {
                                    db.Saamples.Add(apisample);
                                }
                                else
                                {
                                    db.Saamples.Update(apisample);
                                }
                            }
                            await db.SaveChangesAsync();
                        }
                    }

                }

                //Dans tous les cas désormais on récupère les infos de la DB
                using (var db = new KaamelottDB())
                {
                    AllSamples = db.Saamples.ToList();
                    ListePerso = AllSamples.Select(x => x.Character).Distinct().OrderBy(x => x).ToList();
                    FilterSamples();
                }
            });
            IsBusy = false;
        }

        private void FilterSamples()
        {
            if (!String.IsNullOrWhiteSpace(SearchText) && !String.IsNullOrWhiteSpace(SelectedPerso))
            {
                ListeSaamples = AllSamples.Where(x => x.Title.ToLower().Contains(SearchText.ToLower()) && x.Character == SelectedPerso).ToList();
            }
            else if (String.IsNullOrWhiteSpace(SearchText) && !String.IsNullOrWhiteSpace(SelectedPerso))
            {
                ListeSaamples = AllSamples.Where(x => x.Character == SelectedPerso).ToList();
            }
            else if (!String.IsNullOrWhiteSpace(SearchText) && String.IsNullOrWhiteSpace(SelectedPerso))
            {
                ListeSaamples = AllSamples.Where(x => x.Title.ToLower().Contains(SearchText.ToLower())).ToList();
            }
            else
            {
                ListeSaamples = AllSamples;
            }
            MyTitre = $"Liste des saamples ({ListeSaamples.Count}/{AllSamples.Count})";
        }

        #endregion

        

    }
}
