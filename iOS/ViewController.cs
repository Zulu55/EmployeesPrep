namespace Employees.iOS
{
    using System;
	using UIKit;
    using Foundation;

	public partial class ViewController : UIViewController
    {
        #region Attributes
        DialogService dialogService;
        IntPtr handle;
        #endregion

        #region Methods
        public ViewController(IntPtr handle) : base(handle)
        {
            this.handle = handle;
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            dialogService = new DialogService();

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

            var URLAPI = NSBundle.MainBundle.LocalizedString("URLAPI", "URLAPI");
            Console.WriteLine(URLAPI);
		}
        #endregion
    }
}
