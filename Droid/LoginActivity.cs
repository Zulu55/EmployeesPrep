namespace EmployeesPrep.Droid
{
    using System;
	using Android.App;
	using Android.OS;
	using Android.Widget;
    using Android.Content;
	using EmployeesPrep.Services;
	using EmployeesPrep.Models;


	[Activity(Label = "Employees", MainLauncher = true, Icon = "@mipmap/icon")]
	public class LoginActivity : Activity
    {
        #region Attributes
        ApiService apiService; 
        #endregion

        #region Widgets
        EditText editTextEmail;
		EditText editTextPassword;
        Button buttonLogin;
        #endregion

        #region Methods
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Login);

            editTextEmail = FindViewById<EditText>(Resource.Id.editTextEmail);
            editTextPassword = FindViewById<EditText>(Resource.Id.editTextPassword);
            buttonLogin = FindViewById<Button>(Resource.Id.buttonLogin);

            apiService = new ApiService();

            buttonLogin.Click += ButtonLogin_Click;
        }

        async void ButtonLogin_Click(object sender, EventArgs e)
        {
			if (string.IsNullOrEmpty(editTextEmail.Text))
			{
				ShowMessage("Error", "Debe ingresar un email.");
				return;
			}

			if (string.IsNullOrEmpty(editTextPassword.Text))
			{
				ShowMessage("Error", "Debe ingresar una contraseña.");
				return;
			}

            var urlAPI = Resources.GetString(Resource.String.UrlApi);

			var token = await apiService.GetToken(
				  urlAPI,
				  editTextEmail.Text,
				  editTextPassword.Text);

			if (token == null)
			{
				ShowMessage("Error", "Usuario o contraseña incorrectos.");
				editTextPassword.Text = null;
				return;
			}

			if (string.IsNullOrEmpty(token.AccessToken))
			{
				ShowMessage("Error", token.ErrorDescription);
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
				ShowMessage("Error", "Problema recuperando información de usuario.");
				return;
			}

			var employee = (Employee)response.Result;
			employee.AccessToken = token.AccessToken;
			employee.IsRemembered = true;
			employee.Password = editTextPassword.Text;
			employee.TokenExpires = token.Expires;
			employee.TokenType = token.TokenType;

            var intent = new Intent(this, typeof(EmployeesActivity));
			intent.PutExtra("AccessToken", employee.AccessToken);
			intent.PutExtra("TokenType", employee.TokenType);
			intent.PutExtra("EmployeeId", employee.EmployeeId);
			intent.PutExtra("FullName", employee.FullName);
			StartActivity(intent);
        }

		void ShowMessage(string title, string message)
		{
			var builder = new AlertDialog.Builder(this);
			var alert = builder.Create();
			alert.SetTitle(title);
			alert.SetIcon(Resource.Mipmap.Icon);
			alert.SetMessage(message);
			alert.SetButton("Acceptar", (s, ev) => { });
			alert.Show();
		}

		#endregion
	}
}
