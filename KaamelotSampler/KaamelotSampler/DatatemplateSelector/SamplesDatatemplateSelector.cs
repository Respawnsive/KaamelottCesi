using KaamelotSampler.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace KaamelotSampler.DatatemplateSelector
{
    public class SamplesDatatemplateSelector : DataTemplateSelector
    {
        public DataTemplate NormalDatatemplate { get; set; }
        public DataTemplate HihtligtedDatatemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var currentsample = (Saample)item;
            if (currentsample.Character == "Arthur" || currentsample.Character == "Merlin")
            {
                return HihtligtedDatatemplate;
            }
            else
                return NormalDatatemplate;
        }
    }
}
