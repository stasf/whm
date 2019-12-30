using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

using System.Diagnostics;

using System.Runtime.InteropServices;
using System.Threading;
using System.IO;


namespace WoWHealthMonitor
{
	public static class Buffs
	{
		public static int PowerWordFortitude = 1;
		public static int Renew = 2;
		public static int PowerWordShield = 4;
		public static int WeakenedSoul = 8;
		public static int Drink = 16;
	}

	public class Player
	{
		public String name;
		public Boolean exists;
		public Boolean inCombat, isCasting, isDrinking, isFollowing, isAutoAttack, hasMagicDebuff, hasDiseaseDebuff;
		public Int32 HpMax, HpCur;
		public Int32 HpPercent;

		public Int32 ManaMax, ManaCur;
		public Int32 ManaPercent;

		public Int32 UnitBuffs;

		public Player()
		{
			name = "";
			exists = false;
			inCombat = false;
			isCasting = false;
			isDrinking = false;
			isFollowing = false;
			isAutoAttack = false;
			HpMax = 0;
			HpCur = 0;
			HpPercent = 0;
			ManaMax = 0;
			ManaCur = 0;
			ManaPercent = 0;
			UnitBuffs = 0;
		}

		public Boolean HasBuff(int buff)
		{
			if ((UnitBuffs & buff) == buff)
				return true;
			return false;
		}

		public void ProcessStats()
		{
			HpCur = Convert.ToInt32((HpMax / 100.0) * HpPercent);
			ManaCur = Convert.ToInt32((ManaMax / 100.0) * ManaPercent);

			isDrinking = this.HasBuff(Buffs.Drink);
		}

		public Player ShallowCopy()
		{
			return (Player)(this.MemberwiseClone());
		}
	}

	public class Ability
	{
		public Boolean inRange;
		public Boolean onCooldown;

		public Ability()
		{
			inRange = false;
			onCooldown = false;
		}
		public Ability ShallowCopy()
		{
			return (Ability)(this.MemberwiseClone());
		}
	}

	public static class ScreenScanner
	{
		public static Int32 winOffset = 1920;

		public static List<Player> players = new List<Player>(5);
		public static List<Ability> abilities = new List<Ability>(5);

		public static Object WriteLock = new Object();

		public static void ScanLoop()
		{
			while(true)
			{
				if( !BotRoutine.StartScript || BotRoutine.PID == null )
				{
					Thread.Sleep(100);
					continue;
				}

				//bottom left of primary screen (middle)
				//0, 1039 (with task bar)
				List<Player> tmpPlayers = new List<Player>(5);
				List<Ability> tmpAbilities = new List<Ability>(5);
				Color color;


				//Player/party health & buffs
				for (int i = 0; i < 5; i++)
				{
					tmpPlayers.Add(new Player());

					//read health
					color = Functions.GetPixelColor(winOffset + 6 * i, 1039);
					if (color != Color.FromArgb(0, 0, 0))
					{
						if (i == 0)
							tmpPlayers[i].name = "player";
						else
							tmpPlayers[i].name = String.Format("party{0}", i);

						tmpPlayers[i].exists = true;
						tmpPlayers[i].HpPercent = color.B;
						tmpPlayers[i].HpMax = (color.G << 8) + color.R;
					}

					//read buffs
					color = Functions.GetPixelColor(winOffset + 6 * i, 1039 - 6);
					tmpPlayers[i].UnitBuffs = color.R;
					tmpPlayers[i].hasMagicDebuff = (color.B != 0 ? true : false);
				}

				//isCasting
				color = Functions.GetPixelColor(winOffset, 1039 - 18);
				if (color == Color.FromArgb(255, 0, 0))
					tmpPlayers[0].isCasting = true;
				else if (color == Color.FromArgb(0, 255, 0))
					tmpPlayers[0].isCasting = false;

				//inCombat
				color = Functions.GetPixelColor(winOffset + 6, 1039 - 18);
				if (color == Color.FromArgb(255, 0, 0))
					tmpPlayers[0].inCombat = true;
				else if (color == Color.FromArgb(0, 255, 0))
					tmpPlayers[0].inCombat = false;

				//isFollowing
				color = Functions.GetPixelColor(winOffset + 12, 1039 - 18);
				if (color == Color.FromArgb(255, 0, 0))
					tmpPlayers[0].isFollowing = true;
				else if (color == Color.FromArgb(0, 255, 0))
					tmpPlayers[0].isFollowing = false;

				//read mana
				color = Functions.GetPixelColor(winOffset + 18, 1039 - 18);
				tmpPlayers[0].ManaPercent = color.B;
				tmpPlayers[0].ManaMax = (color.G << 8) + color.R;

				//isAutoAttack
				color = Functions.GetPixelColor(winOffset + 24, 1039 - 18);
				if (color == Color.FromArgb(0, 255, 0))
					tmpPlayers[0].isAutoAttack = true;
				else if (color == Color.FromArgb(0, 0, 0))
					tmpPlayers[0].isAutoAttack = false;


				//read abilites
				for (int i = 0; i < 5; i++)
				{
					tmpAbilities.Add(new Ability());

					//read health
					color = Functions.GetPixelColor(winOffset + 6 * i, 1039 - 12);

					if( color.R == 0)
						tmpAbilities[i].inRange = true;
					else
						tmpAbilities[i].inRange = false;

					if (color.G == 0)
						tmpAbilities[i].onCooldown = true;
					else
						tmpAbilities[i].onCooldown = false;
				}

				lock (ScreenScanner.WriteLock)
				{
					players.Clear();
					foreach(Player p in tmpPlayers)
					{
						p.ProcessStats();
						players.Add(p.ShallowCopy());
					}

					abilities.Clear();
					foreach (Ability a in tmpAbilities)
					{
						abilities.Add(a.ShallowCopy());
					}
				}

				Thread.Sleep(50);
			}
		}
	}
}
