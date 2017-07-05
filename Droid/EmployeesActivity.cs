
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace EmployeesPrep.Droid
{
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
            textViewEmployeeName.Text = fullName;
		}
    }
}
