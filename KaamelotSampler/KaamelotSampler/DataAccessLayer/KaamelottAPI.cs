using KaamelotSampler.Models;
using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KaamelotSampler.DataAccessLayer
{
    public class KaamelottAPI
    {
        const string ApiURL = @"https://storagekaamelot.blob.core.windows.net/kaamlotcontainer/sounds.json";

        public async Task<List<Saample>> GetSaamplesListAsync()
        {
            try
            {
                HttpClient client = new HttpClient();
                string result = await client.GetStringAsync(ApiURL);
                List<Saample> liste = JsonConvert.DeserializeObject<List<Saample>>(result);
                return liste;
            }
            catch(Exception ex)
            {
                Crashes.TrackError(ex);
                return new List<Saample>();
            }
        }
    }
}
