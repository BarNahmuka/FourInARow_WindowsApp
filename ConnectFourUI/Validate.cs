using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using connectFourLogic;

namespace connectFourUI
{
	public class Validate
    {
        public static int GetInputFromTheUser()
        {
            int minSizeBoard = 4;
            int maxSizeBoard = 8;
            String stringFromTheUser = Console.ReadLine();
            int numericUserInput = ValidateMeasurement(stringFromTheUser, minSizeBoard, maxSizeBoard);

            return numericUserInput;
        }

        public static int ValidateMeasurement(string i_UserInput, int i_MinimunRange, int i_MaximumRange)
		{
			int.TryParse(i_UserInput, out int numericUserInput);

			while ((numericUserInput < i_MinimunRange) || (numericUserInput > i_MaximumRange))
			{
				Console.WriteLine("Invalid input. Please try again");
				i_UserInput = Console.ReadLine();
				int.TryParse(i_UserInput, out numericUserInput);
			}

			return numericUserInput;
		}

		public static int ValidateAnswer(string i_GameOption)
		{
			int.TryParse(i_GameOption, out int numericUserInput);

			while (numericUserInput != 1 && numericUserInput != 2)
			{
				Console.WriteLine("Invalid input. Please try again");
				i_GameOption = Console.ReadLine();
				int.TryParse(i_GameOption, out numericUserInput);
			}

			return numericUserInput;
		}

		public static int ValidatePlayerMove(Game i_Game, string i_UserInput, int i_MaximumRange)
		{
			int numericValueFromUser = 0;
			int minimunRange = 1;

			if (i_UserInput == "Q" || i_UserInput == "q")
			{
				i_Game.GiveUp();
			}
			else
			{
				numericValueFromUser = ValidateMeasurement(i_UserInput, minimunRange, i_MaximumRange);
			}

			return numericValueFromUser;
		}

	}
}
