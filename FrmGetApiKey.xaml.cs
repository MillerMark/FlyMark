using System.Windows;
using System.Windows.Controls;

namespace FlyMark
{
	/// <summary>
	/// Interaction logic for FrmGetApiKey.xaml
	/// </summary>
	public partial class FrmGetApiKey : Window
	{
		public FrmGetApiKey()
		{
			InitializeComponent();
		}

		private void ApiChanged(object sender, TextChangedEventArgs e)
		{
			SetOkayEnabled();
		}

		private void PasswordChanged(object sender, RoutedEventArgs e)
		{
			SetOkayEnabled();
		}

		void SetOkayEnabled()
		{
			//! Careful when debugging not to unintentionally reveal secrets!
			btnOkay.IsEnabled = !string.IsNullOrEmpty(tbxTwitchUserName.Text) && !string.IsNullOrEmpty(tbxTwitchPassword.Password);
		}

		private void btnOkay_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = true;

			//! Careful when debugging not to unintentionally reveal secrets!
			FlyMark.CredentialManager.SaveKeys(tbxTwitchUserName.Text, tbxTwitchPassword.Password);
			Close();
		}

		private void btnCancel_Click(object sender, RoutedEventArgs e)
		{
			DialogResult = false;
			Close();
		}
	}
}
