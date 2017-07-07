namespace Employees.iOS
{
    using UIKit;

	public class DialogService
    {
		public void ShowMessage(UIViewController owner, string title, string message)
		{
			var alert = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);
			alert.AddAction(UIAlertAction.Create("Aceptar", UIAlertActionStyle.Default, null));
			owner.PresentViewController(alert, true, null);
		}
    }
}
