namespace Employees.Droid
{
    using System.Collections.Generic;
    using Android.App;
    using Android.OS;
    using Employees.Models;

    public class EmployeeFragment : Fragment
    {
        public List<Employee> employees
        {
            get;
            set;
        }

		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			RetainInstance = true;
		}
	}
}
