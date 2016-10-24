using System;
using System.Text.RegularExpressions;
using UIKit;

namespace CardValidation
{
	public static class CreditCard
	{
		public static bool IsValidCardNumber(int cardNumber)
		{
			return IsValidCardNumber(cardNumber.ToString());
		}

		public static bool IsValidCardNumber(string cardNumber)
		{
			cardNumber = cardNumber.Replace(" ", "");

			//FIRST STEP: Double each digit starting from the right
			int[] doubledDigits = new int[cardNumber.Length / 2];
			int k = 0;
			for (int i = cardNumber.Length - 2; i >= 0; i -= 2) {
				int digit = int.Parse(cardNumber[i].ToString());
				doubledDigits[k] = digit * 2;
				k++;
			}

			//SECOND STEP: Add up separate digits
			int total = 0;
			foreach (int i in doubledDigits) {
				string number = i.ToString();
				for (int j = 0; j < number.Length; j++) {
					total += int.Parse(number[j].ToString());
				}
			}

			//THIRD STEP: Add up other digits
			int total2 = 0;
			for (int i = cardNumber.Length - 1; i >= 0; i -= 2) {
				int digit = int.Parse(cardNumber[i].ToString());
				total2 += digit;
			}

			//FOURTH STEP: Total
			int final = total + total2;

			return final % 10 == 0; //Well formed will divide evenly by 10
		}

		public static Card FindCardType(string cardNumber)
		{
			foreach (CardTypeInfo info in _cardTypeInfo) {
				if (cardNumber.Length == info.Length &&
					Regex.IsMatch(cardNumber, info.RegEx))
					return new Card(info.Type, info.CardImage);
			}

			return new Card(CardType.Unknown, null);
		}

		private static CardTypeInfo[] _cardTypeInfo =
		{
			new CardTypeInfo("^(51|52|53|54|55)", 16, CardType.MasterCard, UIImage.FromBundle("mastercard")),
		  new CardTypeInfo("^(4)", 16, CardType.VISA,UIImage.FromBundle("visa")),
		  new CardTypeInfo("^(4)", 13, CardType.VISA,UIImage.FromBundle("visa")),
		  new CardTypeInfo("^(34|37)", 15, CardType.Amex,UIImage.FromBundle("amex")),
		  new CardTypeInfo("^(6011)", 16, CardType.Discover,UIImage.FromBundle("discover")),
		  new CardTypeInfo("^(300|301|302|303|304|305|36|38)",
						   14, CardType.DinersClub,UIImage.FromBundle("dinersclub")),
		  new CardTypeInfo("^(3)", 16, CardType.JCB,UIImage.FromBundle("jcb")),
		  new CardTypeInfo("^(2131|1800)", 15, CardType.JCB,UIImage.FromBundle("jcb")),
		};
	}

	class CardTypeInfo
	{
		public CardTypeInfo(string regEx, int length, CardType type, UIImage cardImage)
		{
			this.RegEx = regEx;
			this.Length = length;
			this.Type = type;
			this.CardImage = cardImage;
		}

		public string RegEx { get; set; }
		public int Length { get; set; }
		public CardType Type { get; set; }
		public UIImage CardImage { get; set; }

	}
}
