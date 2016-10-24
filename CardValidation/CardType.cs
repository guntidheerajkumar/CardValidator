using System;
using UIKit;

namespace CardValidation
{
	public enum CardType
	{
		Unknown = 0,
		MasterCard = 1,
		VISA = 2,
		Amex = 3,
		Discover = 4,
		DinersClub = 5,
		JCB = 6,
		enRoute = 7
	}

	public class Card
	{
		public Card(CardType type, UIImage image)
		{
			this.Type = type;
			this.CardImage = image;
		}

		CardType Type { get; set; }
		UIImage CardImage { get; set; }
	}

}
