// WARNING
//
// This file has been generated automatically by Visual Studio from the outlets and
// actions declared in your storyboard file.
// Manual changes to this file will not be maintained.
//
using Foundation;
using System;
using System.CodeDom.Compiler;
using UIKit;

namespace Employees.iOS
{
    [Register ("EmployessViewController")]
    partial class EmployessViewController
    {
        [Outlet]
        [GeneratedCode ("iOS Designer", "1.0")]
        UIKit.UILabel labelFullName { get; set; }

        void ReleaseDesignerOutlets ()
        {
            if (labelFullName != null) {
                labelFullName.Dispose ();
                labelFullName = null;
            }
        }
    }
}