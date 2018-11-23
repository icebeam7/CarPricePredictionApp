using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using CarPricePredictionApp.Models;
using CarPricePredictionApp.Services;

namespace CarPricePredictionApp.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class PredictionPage : ContentPage
	{
		public PredictionPage ()
		{
			InitializeComponent ();
		}

        private async void btnPredict_Clicked(object sender, EventArgs e)
        {
            var car = new Car()
            {
                Make = txtMake.Text,
                BodyStyle = txtBodyStyle.Text,
                EngineSize = int.Parse(txtEngineSize.Text),
                HighwayMPG = int.Parse(txtHighwayMPG.Text),
                HorsePower = int.Parse(txtHorsePower.Text),
                PeakRPM = int.Parse(txtPeakRPM.Text),
                WheelSize = int.Parse(txtWheelSize.Text),
                Price = 0,
            };

            car.Price = await PredictionService.PredictPrice(car);
            txtPrice.Text = car.Price.ToString();
        }
    }
}