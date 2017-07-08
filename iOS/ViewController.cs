namespace Employees.iOS
{
    using System;
	using UIKit;
    using Foundation;
    using Employees.Services;
    using System.Threading.Tasks;
	using Employees.Helpers;
	using Employees.Models;

	public partial class ViewController : UIViewController
    {
		#region Attributes
		DialogService dialogService;
		ApiService apiService;
		#endregion

		#region Methods
		public ViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            dialogService = new DialogService();
            apiService = new ApiService();

            activityIndicator.Hidden = true;
        }

        public override void DidReceiveMemoryWarning()
        {
            base.DidReceiveMemoryWarning();
            // Release any cached data, images, etc that aren't in use.		
        }

        partial void ButtonLogin_TouchUpInside(UIButton sender)
        {
			if (string.IsNullOrEmpty(textFieldEmail.Text))
			{
				dialogService.ShowMessage(this, "Error", "Debes ingresar un email.");
				return;
			}

			if (string.IsNullOrEmpty(textFieldPassword.Text))
			{
				dialogService.ShowMessage(this, "Error", "Debes ingresar una constraseña.");
				return;
			}

			activityIndicator.Hidden = false;
			buttonLogin.Enabled = false;
			
            var ok = AsyncHelpers.RunSync<bool>(() => Login());
			
            activityIndicator.Hidden = true;
			buttonLogin.Enabled = true;
			
            if (!ok)
            {
                return;
            }
        }

        async Task<bool> Login()
        {
			var URLAPI = NSBundle.MainBundle.LocalizedString("URLAPI", "URLAPI");

			var token = await apiService.GetToken(
				URLAPI,
				textFieldEmail.Text,
				textFieldPassword.Text);

			if (token == null)
			{
				dialogService.ShowMessage(this, "Error", "Usuario o contraseña no válidos.");
                textFieldPassword.Text = null;
                return false;
			}

			if (string.IsNullOrEmpty(token.AccessToken))
			{
				dialogService.ShowMessage(this, "Error", token.ErrorDescription);
				textFieldPassword.Text = null;
                return false;
			}

			var response = await apiService.GetEmployeeByEmailOrCode(
				URLAPI,
				"/api",
				"/Employees/GetGetEmployeeByEmailOrCode",
				token.TokenType,
				token.AccessToken,
				token.UserName);

			if (!response.IsSuccess)
			{
				dialogService.ShowMessage(this, "Error", "Problema con el usuario, contacte a Pandian.");
                return false;
			}

			var employee = (Employee)response.Result;
			employee.AccessToken = token.AccessToken;
            employee.IsRemembered = switchRememberme.On;
            employee.Password = textFieldPassword.Text;
			employee.TokenExpires = token.Expires;
			employee.TokenType = token.TokenType;

			return true;
		}
        #endregion
    }
}
