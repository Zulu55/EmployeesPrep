namespace Employees.Models
{
    using System;
    using Xamarin.Forms;

    public class Employee
	{
		public int EmployeeId { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public int EmployeeCode { get; set; }

		public int DocumentTypeId { get; set; }

		public int LoginTypeId { get; set; }

		public string Document { get; set; }

		public string Picture { get; set; }

		public string Email { get; set; }

		public string Phone { get; set; }

		public string Address { get; set; }

		public byte[] ImageArray { get; internal set; }

		public string AccessToken { get; set; }

		public string TokenType { get; set; }

		public DateTime TokenExpires { get; set; }

		public string Password { get; set; }

		public bool IsRemembered { get; set; }

		public string FullName
		{
			get { return string.Format("{0} {1}", FirstName, LastName); }
		}

		public string FullPicture
		{
			get
			{
				if (string.IsNullOrEmpty(Picture))
				{
					return "avatar_user.png";
				}

				if (LoginTypeId == 1)
				{
					var urlBackend = "http://tataappapi.azurewebsites.net";
					return string.Format("{0}/{1}", urlBackend, Picture.Substring(1));
				}

				return Picture;
			}
		}

		public override int GetHashCode()
		{
			return EmployeeId;
		}

        public override string ToString()
        {
            return string.Format(
                "EmployeeId={0}, FirstName={1}, LastName={2}, " +
                "EmployeeCode={3}, DocumentTypeId={4}, LoginTypeId={5}, " +
                "Document={6}, Picture={7}, Email={8}, Phone={9}, " +
                "Address={10}, ImageArray={11}, " +
                "AccessToken={12}, TokenType={13}, " +
                "TokenExpires={14}, Password={15}, " +
                "IsRemembered={16}, FullName={17}, " +
                "FullPicture={18}]", EmployeeId, FirstName, LastName, 
                EmployeeCode, DocumentTypeId, LoginTypeId, Document, Picture, 
                Email, Phone, Address, ImageArray, AccessToken, TokenType, 
                TokenExpires, Password, IsRemembered, FullName, FullPicture);
        }
	}
}