using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Net;
using static System.Net.WebRequestMethods;
using System.Runtime.CompilerServices;

namespace WeatherApplication
{
    public partial class Form1 : Form
    {
        readonly string APIKey = "0c45ebf53a0bfbc0a705a53b7c5523a9";
        private double lon;
        private double lat;
        public Form1()
        {
            InitializeComponent();
        }

        private void Label1_Click(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            GetWeather();
        }
        private void GetWeather()
        {
            try
            {
                using (WebClient web = new WebClient())
                {
                    var city = TBCity.Text;
                    var url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={APIKey}";
                    var json = web.DownloadString(url);
                    WeatherInfo.root Info = JsonConvert.DeserializeObject<WeatherInfo.root>(json);

                    SetWeatherInfo(Info);
                   
                    lon = Info.coord.longitude;
                    lat = Info.coord.latitude;
                }
            }
            catch (Exception ex)
            {
                messageOutput.Text = ex.Message;
            }
            
        }
        private void SetWeatherInfo(WeatherInfo.root Info)
        {
            picIcon.ImageLocation = "https://openweathermap.org/img/w/" + Info.weather[0].icon + ".png";
            labCondition.Text = Info.weather[0].main;
            labDetails.Text = Info.weather[0].description;
            labSunset.Text = convertDateTime(Info.sys.sunset).ToShortTimeString();
            labSunrise.Text = convertDateTime(Info.sys.sunrise).ToShortTimeString();
            labWindSpeed.Text = Info.wind.speed.ToString();
            labWindPressure.Text = Info.main.pressure.ToString();
        }
        private DateTime convertDateTime(long milliseconds)
        {
            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).ToLocalTime();
            return epoch.AddSeconds(milliseconds).ToLocalTime();
        }
        private void label2_Click(object sender, EventArgs e)
        {

        }
    }   
}
