using System;
using Xamarin.Forms;

namespace CheckboxTesting
{
	public class FirstPage : ContentPage
	{
		public FirstPage() 
		{
			var button = new Button () {
				Text = "click to see checkboxes"
			};

			button.Clicked += (object sender, EventArgs e) => 
			{
				this.Navigation.PushAsync(new CheckBoxWithErrorPage());
			};

			Content = button;
		}
	}
}

