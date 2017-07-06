namespace Employees.Droid
{
    using System.Collections.Generic;
    using System.Linq;
    using Android.App;
	using Android.Content;
	using Android.OS;
	using Android.Views;
	using Android.Widget;
    using Employees.Models;
	using Employees.Services;

	[Activity(Label = "Employees", MainLauncher = false, Icon = "@mipmap/icon")]
	public class EmployeesActivity : Activity
    {
        #region Attributes
        string accessToken;
        string tokenType;
        string employeeId;
        string fullName;
		ApiService apiService;
		DialogService dialogService;
		#endregion

		#region Widgets
		TextView textViewFullName;
        ListView listViewEmployees;
        ProgressBar progressBarActivityIndicator;
        #endregion

        #region Methods
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Employees);

			textViewFullName = FindViewById<TextView>(Resource.Id.textViewFullName);
			listViewEmployees = FindViewById<ListView>(Resource.Id.listViewEmployees);
			progressBarActivityIndicator = FindViewById<ProgressBar>(Resource.Id.progressBarActivityIndicator);

			progressBarActivityIndicator.Visibility = ViewStates.Invisible;

            apiService = new ApiService();
            dialogService = new DialogService();

            accessToken = Intent.GetStringExtra("AccessToken");
			tokenType = Intent.GetStringExtra("TokenType");
			employeeId = Intent.GetStringExtra("EmployeeId");
			fullName = Intent.GetStringExtra("FullName");

            textViewFullName.Text = fullName;

            LoadEmployees();
		}

        async void LoadEmployees()
        {
			progressBarActivityIndicator.Visibility = ViewStates.Visible;

            var urlAPI = Resources.GetString(Resource.String.URLAPI);

            var response = await apiService.GetList<Employee>(
				urlAPI,
				"/api",
				"/Employees",
				tokenType,
				accessToken);

			if (!response.IsSuccess)
			{
				progressBarActivityIndicator.Visibility = ViewStates.Invisible;
				dialogService.ShowMessage(this, "Error", response.Message);
				return;
			}

            var employees = ((List<Employee>)response.Result)
                .OrderBy(e => e.FirstName)
                .ThenBy(e => e.LastName)
                .ToList();

			listViewEmployees.Adapter = new EmployeesAdapter(
				this,
				employees,
                Resource.Layout.EmployeeItem,
                Resource.Id.imageFullPicture,
                Resource.Id.textViewFullName,
                Resource.Id.textViewEmail,
                Resource.Id.textViewEmployeeCode,
                Resource.Id.textViewPhone,
                Resource.Id.textViewAddress);
            
			progressBarActivityIndicator.Visibility = ViewStates.Invisible;
		}
        #endregion
    }
}