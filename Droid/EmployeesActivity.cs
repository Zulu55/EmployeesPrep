namespace EmployeesPrep.Droid
{
    using Android.App;
	using Android.Content;
	using Android.OS;
	using Android.Widget;

	[Activity(Label = "Employees", MainLauncher = false, Icon = "@mipmap/icon")]
	public class EmployeesActivity : Activity
	{
		#region Attributes
		string accessToken;
		string tokenType;
		string employeeId;
		string fullName;
		#endregion

		#region Widgets
		TextView textViewEmployeeName;
        ListView listViewEmployees;
		#endregion

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.Employees);

			accessToken = Intent.GetStringExtra("AccessToken");
			tokenType = Intent.GetStringExtra("TokenType");
			employeeId = Intent.GetStringExtra("EmployeeId");
			fullName = Intent.GetStringExtra("FullName");

			textViewEmployeeName = FindViewById<TextView>(Resource.Id.textViewEmployeeName);
			listViewEmployees = FindViewById<ListView>(Resource.Id.listViewEmployees);

            textViewEmployeeName.Text = fullName;

            LoadEmployees();
		}

        void LoadEmployees()
        {
            
        }
	}
}   