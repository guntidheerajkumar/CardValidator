using System;
using CardValidation;
using UIKit;

namespace SampleApp
{
	public partial class ViewController : UIViewController
	{
		protected ViewController(IntPtr handle) : base(handle)
		{
			// Note: this .ctor should not contain any initialization logic.
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();
			var outPut = CreditCard.FindCardType("5546370220635590");
		}
	}
}
