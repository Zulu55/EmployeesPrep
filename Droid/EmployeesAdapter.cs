namespace Employees.Droid
{
    using System.Collections.Generic;
	using Android.App;
	using Android.Views;
	using Android.Widget;
	using Employees.Models;
    using Koush;

	public class EmployeesAdapter : BaseAdapter<Employee>
	{
		#region Attributes
		List<Employee> employees;
		Activity context;
		int itemLayoutTemplate;
		int fullPictureId;
		int fullNameId;
		int emailId;
        int employeeCodeId;
        int phoneId;
        int addressId;
        #endregion

        #region Constructor
        public EmployeesAdapter(
            Activity context,
            List<Employee> employees,
            int itemLayoutTemplate,
			int fullPictureId,
    		int fullNameId,
    		int emailId,
    		int employeeCodeId,
    		int phoneId,
    		int addressId)
        {
            this.context = context;
            this.employees = employees;
            this.itemLayoutTemplate = itemLayoutTemplate;
			this.fullPictureId = fullPictureId;
			this.fullNameId = fullNameId;
			this.emailId = emailId;
			this.employeeCodeId = employeeCodeId;
			this.phoneId = phoneId;
			this.addressId = addressId;
		}
		#endregion

		#region Properties
		public override Employee this[int position]
		{
			get
			{
				return employees[position];
			}
		}

		public override int Count
		{
			get
			{
				return employees.Count;
			}
		}        
        #endregion

        #region Methods
        public override long GetItemId(int position)
        {
			return employees[position].EmployeeId;
		}

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            var item = employees[position];
			View itemView;
			if (convertView == null)
			{
				itemView = context.LayoutInflater.Inflate(itemLayoutTemplate, null);
			}
			else
			{
				itemView = convertView;
			}

			var image = itemView.FindViewById<ImageView>(fullPictureId);
            UrlImageViewHelper.SetUrlDrawable(image, item.FullPicture);
			itemView.FindViewById<TextView>(fullNameId).Text = item.FullName;
			itemView.FindViewById<TextView>(emailId).Text = item.Email;
			itemView.FindViewById<TextView>(employeeCodeId).Text = item.EmployeeCode.ToString();
			itemView.FindViewById<TextView>(phoneId).Text = item.Phone;
			itemView.FindViewById<TextView>(addressId).Text = item.Address;
			return itemView;        
        }
        #endregion
    }
}