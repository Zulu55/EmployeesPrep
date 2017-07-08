using Foundation;
using System;
using System.IO;
using UIKit;

namespace Employees.iOS
{
    public partial class EmployessViewController : UIViewController
    {
        public EmployessViewController (IntPtr handle) : base (handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

			var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
			var fileName = Path.Combine(documents, "Employees.txt");
            var text = File.ReadAllText(fileName);
            var fields = text.Split(',');
		}
    }
}