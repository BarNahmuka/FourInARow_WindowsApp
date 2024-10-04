using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace connectFourLogic
{
	public class Player
	{
		private readonly string m_PlayerSign;
		private int m_PlayerScore;

		public Player(string i_sign)
		{
			m_PlayerSign = i_sign;
			m_PlayerScore = 0;
		}

		public string PlayerSign
		{
			get { return m_PlayerSign; }
		}

		public int PlayerScore
		{
			get { return m_PlayerScore; }
		}

		public void AddScore()
		{
			m_PlayerScore++;
		}
	}
}
