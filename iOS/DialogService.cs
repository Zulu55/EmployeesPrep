namespace Employees.iOS
{
    using UIKit;

	public class DialogService
    {
		public void ShowMessage(string title, string message)
		{
			var alert = new UIAlertView(
				title,
				message,
				null,
				"Aceptar",
				null);
			alert.Show();
        }
	}
}
