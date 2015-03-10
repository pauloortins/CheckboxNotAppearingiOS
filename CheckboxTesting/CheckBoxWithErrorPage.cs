using System;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using XLabs.Forms.Controls;

namespace CheckboxTesting
{
	public class CheckBoxWithErrorPage : ContentPage
	{
		public CheckBoxWithErrorPage ()
		{
			var users = new ObservableCollection<User> () {
				new User() {Name = "Click On Me to See a Error Page", IsChecked=true},
				new User() {Name = "Click On Me to See a Error Page", IsChecked=false},
			};

			var listView = new ListView () {
				ItemsSource = users,
				ItemTemplate = new DataTemplate(typeof(CheckboxCell))
			};

			var stack = new StackLayout {
				Spacing = 0,
				Children = {
					listView
				}
			};

			var relativeLayout = new RelativeLayout () {
				HorizontalOptions = LayoutOptions.FillAndExpand
			};

			relativeLayout.Children.Add (
				stack, 
				Constraint.Constant (0),
				Constraint.Constant (0)
			);

			Content = relativeLayout;
		}
	}

	public class CheckboxCell : ViewCell 
	{
		protected override void OnBindingContextChanged ()
		{
			base.OnBindingContextChanged ();

			var user = this.BindingContext as User;

			var checkbox = new CheckBox() {
				BindingContext = user
			};

			checkbox.SetBinding<User> (CheckBox.CheckedProperty, x => x.IsChecked, BindingMode.TwoWay);

			var label = new Label () {
				BindingContext = user,
				HorizontalOptions = LayoutOptions.FillAndExpand
			};
			label.SetBinding<User> (Label.TextProperty, x => x.Name, BindingMode.TwoWay);

			var grid = new Grid () {
				Padding = new Thickness(0, 10, 0, 10),
			};

			grid.Children.Add (checkbox, 0, 0);	
			grid.Children.Add (label, 1, 0);

			grid.GestureRecognizers.Add (new TapGestureRecognizer (async (view, obj) => {
				await (view.ParentView.ParentView.ParentView.ParentView as Page).Navigation.PopAsync();
			}));

			View = grid;
		}
	}

	public class User : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		protected virtual void OnPropertyChanged ([CallerMemberName] string propertyName = null)
		{
			if (PropertyChanged != null) {
				PropertyChanged (this, new PropertyChangedEventArgs (propertyName));
			}
		}

		protected bool SetProperty<T> (ref T storage, T value, [CallerMemberName] string propertyName = null)
		{
			if (Object.Equals (storage, value)) {
				return false;
			}

			storage = value;
			OnPropertyChanged (propertyName);
			return true;
		}

		private bool _isChecked;
		public bool IsChecked {
			get {return _isChecked;}
			set {SetProperty (ref _isChecked, value);}
		}

		private string _name;
		public string Name {
			get {return _name;}
			set {SetProperty (ref _name, value);}
		}
	}
}

