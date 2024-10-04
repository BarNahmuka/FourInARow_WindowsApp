using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using connectFourLogic;

namespace connectFourUI
{
	public class Menu
	{
		public static Game SetupGame()
		{
			int userInputForCols;
			int userInputForRows;
			string gameModeOption;
			int gameModeByNumeric;

			Console.WriteLine("Hi,Thank you for choosing our 4 in a row game.");
			Console.WriteLine("Please enter desired number of cols");
			userInputForCols = Validate.GetInputFromTheUser();
			Console.WriteLine("Please enter desired number of rows");
			userInputForRows = Validate.GetInputFromTheUser();
			Console.WriteLine("Please choose desired game mode " + Environment.NewLine + "1 VS Player" + Environment.NewLine + "2 VS PC");
			gameModeOption = Console.ReadLine();
			gameModeByNumeric = Validate.ValidateAnswer(gameModeOption);
			Game gameConnectFour;
			Player playerOne = new Player("X");
			if (gameModeByNumeric == 1)
			{ 
				Player playerTwo = new Player("O");
				gameConnectFour = new Game(playerOne, playerTwo, userInputForRows, userInputForCols);
			}
			else
			{
				PC computer = new PC("O");
				gameConnectFour = new Game(playerOne, computer, userInputForRows, userInputForCols);
			}
			
			GameManagement.ManageGame(gameConnectFour);
			Console.ReadLine();

			return gameConnectFour;
		}
	}
}
