namespace Employees.Droid
{
    using System;
	using Android.App;
    using Android.Content;
	using Android.OS;
	using Android.Views;
	using Android.Widget;
    using Employees.Models;
    using Employees.Services;

    [Activity(Label = "Employees", MainLauncher = true, Icon = "@mipmap/icon")]
	public class LoginActivity : Activity
    {
        #region Attributes
        ApiService apiService;
        DialogService dialogService;
        #endregion

        #region Widgets
        EditText editTextEmail;
		EditText editTextPassword;
        Button buttonLogin;
        ProgressBar progressBarActivityIndicator;
        Switch switchRememberme;
        #endregion

        #region Methods
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Login);

            editTextEmail = FindViewById<EditText>(Resource.Id.editTextEmail);
            editTextPassword = FindViewById<EditText>(Resource.Id.editTextPassword);
			buttonLogin = FindViewById<Button>(Resource.Id.buttonLogin);
			progressBarActivityIndicator = FindViewById<ProgressBar>(Resource.Id.progressBarActivityIndicator);
			switchRememberme = FindViewById<Switch>(Resource.Id.switchRememberme);

			apiService = new ApiService();
            dialogService = new DialogService();

            progressBarActivityIndicator.Visibility = ViewStates.Invisible;

			buttonLogin.Click += ButtonLogin_Click;
        }

        async void ButtonLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(editTextEmail.Text))
            {
                dialogService.ShowMessage(this, "Error", "Debes ingresar un email.");
                return;
            }

            if (string.IsNullOrEmpty(editTextPassword.Text))
            {
                dialogService.ShowMessage(this, "Error", "Debes ingresar una constraseña.");
                return;
            }

			progressBarActivityIndicator.Visibility = ViewStates.Visible;
            buttonLogin.Enabled = false;

            //var checkConnetion = await apiService.CheckConnection();
            //if (!checkConnetion.IsSuccess)
            //{
            //	progressBarActivityIndicator.Visibility = ViewStates.Invisible;
            //	buttonLogin.Enabled = true;
            //	ShowMessage("Error", checkConnetion.Message);
            //	return;
            //}

            var urlAPI = Resources.GetString(Resource.String.URLAPI);

			var token = await apiService.GetToken(
				urlAPI,
                editTextEmail.Text,
				editTextPassword.Text);

			if (token == null)
			{
                progressBarActivityIndicator.Visibility = ViewStates.Invisible;
                buttonLogin.Enabled = true;
				dialogService.ShowMessage(this, "Error", "El email o la contraseña es incorrecto.");
				editTextPassword.Text = null;
				return;
			}

			if (string.IsNullOrEmpty(token.AccessToken))
			{
				progressBarActivityIndicator.Visibility = ViewStates.Invisible;
				buttonLogin.Enabled = true;
				dialogService.ShowMessage(this, "Error", token.ErrorDescription);
				editTextPassword.Text = null;
				return;
			}

			var response = await apiService.GetEmployeeByEmailOrCode(
				urlAPI,
				"/api",
				"/Employees/GetGetEmployeeByEmailOrCode",
				token.TokenType,
				token.AccessToken,
				token.UserName);

			if (!response.IsSuccess)
			{
				progressBarActivityIndicator.Visibility = ViewStates.Invisible;
				buttonLogin.Enabled = true;
				dialogService.ShowMessage(this, "Error", "Problema con el usuario, contacte a Pandian.");
				return;
			}

			var employee = (Employee)response.Result;
			employee.AccessToken = token.AccessToken;
			employee.IsRemembered = switchRememberme.Checked;
			employee.Password = editTextPassword.Text;
			employee.TokenExpires = token.Expires;
			employee.TokenType = token.TokenType;

			progressBarActivityIndicator.Visibility = ViewStates.Invisible;
			buttonLogin.Enabled = true;

            var intent = new Intent(this, typeof(EmployeesActivity));
			intent.PutExtra("AccessToken", employee.AccessToken);
			intent.PutExtra("TokenType", employee.TokenType);
			intent.PutExtra("EmployeeId", employee.EmployeeId);
			intent.PutExtra("FullName", employee.FullName);

            StartActivity(intent);
		}
		#endregion
    }
}
