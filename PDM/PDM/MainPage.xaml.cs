using Newtonsoft.Json;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PDM
{
    public partial class MainPage : ContentPage
    {



        public int Count = 0;
        public short Counter = 0;
        public int SlidePosition = 0;
        string heightList;
        int heightRowsList = 90;
        private const string Url = "http://rallycoding.herokuapp.com/api/music_albums";
        // This handles the Web data request
        private HttpClient _client = new HttpClient();
        public MainPage()
        {
            InitializeComponent();
            // We call the OnGetList from Here 
            OnGetList();
        }
        protected async void OnGetList()
        {
            if (CrossConnectivity.Current.IsConnected)
            {

                try
                {
                    //Activity indicator visibility on
                    activity_indicator.IsRunning = true;
                    //Getting JSON data from the Web
                    var content = await _client.GetStringAsync(Url);
                    //We deserialize the JSON data from this line
                    var tr = JsonConvert.DeserializeObject<List<Items>>(content);
                    //After deserializing , we store our data in the List called ObservableCollection
                    ObservableCollection<Items> trends = new ObservableCollection<Items>(tr);

                    //Then finally we attach the List to the ListView. Seems Simple :)
                    myList.ItemsSource = trends;


                    //We check the number of Items in the Observable Collection
                    int i = trends.Count;
                    if (i > 0)
                    {
                        //If they are greater than Zero we stop the activity indicator
                        activity_indicator.IsRunning = false;
                    }

                    //Here we Wrap  the size of the ListView according to the number of Items which have been retrieved 
                    i = (trends.Count * heightRowsList);
                    activity_indicator.HeightRequest = i;

                }
                catch (Exception ey)
                {
                    Debug.WriteLine("" + ey);
                }

            }


        }
    }
}
