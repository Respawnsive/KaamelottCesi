using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace KaamelotSampler.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {

        private string myTitre;
        public string MyTitre
        {
            get { return myTitre; }
            set 
            { 
                myTitre = value;
                OnPropertyChange("MyTitre");
            }
        }


        private bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set 
            { 
                isBusy = value;
                OnPropertyChange();
            }
        }


        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChange(string PropertyName = null)
        {
            if (PropertyChanged == null)
                return;
            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        #endregion
    }

}
