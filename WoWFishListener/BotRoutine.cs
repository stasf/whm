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
	public static class BotRoutine
	{
		public static Boolean StartScript = false;
		public static Process PID = null;

		public static Boolean doLoot = false;
		public static Boolean useWand = false;
		public static Object wandLock = new Object();

		public static System.Timers.Timer AutoAttackTimer;

		public static Random rand;
		public static void Init()
		{
			rand = new Random();
		}

		public static void StartFollowing()
		{
			Functions.SendKeyDown(PID, Functions.VK_NUMPAD7);
			Functions.SendKeyUp(PID, Functions.VK_NUMPAD7);
			Thread.Sleep(50);
		}

		public static void StopFollowing()
		{
			Functions.SendKeyDown(PID, Functions.VK_NUMPAD8);
			Functions.SendKeyUp(PID, Functions.VK_NUMPAD8);
			Thread.Sleep(50);
		}

		public static void Drink()
		{
			Functions.SendKeyDown(PID, Functions.VK_NUMPAD9);
			Functions.SendKeyUp(PID, Functions.VK_NUMPAD9);
			Thread.Sleep(2000);
		}

		public static void Assist()
		{
			Functions.SendKeyDown(PID, Functions.VK_F6);
			Functions.SendKeyUp(PID, Functions.VK_F6);
			Thread.Sleep(50);
		}

		public static void StopDrinking()
		{
			Functions.SendKeyDown(PID, Functions.VK_W);
			Functions.SendKeyUp(PID, Functions.VK_W);
			Thread.Sleep(200);
		}

		public static void Target(String tar)
		{
			switch(tar)
			{
				case "player":
					Functions.SendKeyDown(PID, Functions.VK_F1);
					Functions.SendKeyUp(PID, Functions.VK_F1);
					break;

				case "party1":
					Functions.SendKeyDown(PID, Functions.VK_F2);
					Functions.SendKeyUp(PID, Functions.VK_F2);
					break;

				case "party2":
					Functions.SendKeyDown(PID, Functions.VK_F3);
					Functions.SendKeyUp(PID, Functions.VK_F3);
					break;

				case "party3":
					Functions.SendKeyDown(PID, Functions.VK_F4);
					Functions.SendKeyUp(PID, Functions.VK_F4);
					break;

				case "party4":
					Functions.SendKeyDown(PID, Functions.VK_F5);
					Functions.SendKeyUp(PID, Functions.VK_F5);
					break;
			}
			Thread.Sleep(150);
		}

		public static void CancelAction()
		{
			Functions.SendKeyDown(PID, Functions.VK_S);
			Functions.SendKeyUp(PID, Functions.VK_S);
			Thread.Sleep(50);
		}


		public static void MainLoop()
		{
			while (true)
			{
				if (!StartScript || PID == null)
				{
					Thread.Sleep(100);
					continue;
				}
				
				List<Player> players = new List<Player>();
				List<Ability> abilities = new List<Ability>();

				lock (ScreenScanner.WriteLock)
				{
					foreach (Player p in ScreenScanner.players)
					{
						if( p.exists)
							players.Add(p.ShallowCopy());
					}

					foreach (Ability a in ScreenScanner.abilities)
						abilities.Add(a.ShallowCopy());
				}

				if( players.Count == 0 )
				{
					Thread.Sleep(100);
					continue;
				}
				Player me = players[0];

				Player tar = players.Aggregate((p1, p2) => ((p1.HpMax - p1.HpCur) > (p2.HpMax - p2.HpCur) ? p1 : p2));

				//check mana
				if (!me.isDrinking && me.ManaPercent <= 50 && !me.isCasting && !me.inCombat)
				{
					if (me.isFollowing)
					{
						StopFollowing();
						continue;
					}
					else
					{
						Drink();
						continue;
					}
				}

				//check combat, should I be following?
				//if (me.inCombat && me.isFollowing)
				//{
					//StopFollowing();
					//continue;
				//}

				if (tar.HpPercent != 100 && !me.isDrinking)
				{
					if ( tar.HpPercent < 70 && !tar.HasBuff(Buffs.WeakenedSoul) && !abilities[0].onCooldown && abilities[0].inRange)
					{
						Target(tar.name);
						if (me.isAutoAttack)
							Thread.Sleep(2000);
						//shield
						if (!abilities[0].onCooldown && abilities[0].inRange)
						{
							Functions.SendKeyDown(PID, Functions.VK_1);
							Functions.SendKeyUp(PID, Functions.VK_1);
							Thread.Sleep(200);
							continue;
						}
					}
					else if (tar.HpPercent < 55 || (tar.HpPercent <= 60 && tar.HasBuff(Buffs.Renew) ))
					{
						//heal
						if (!abilities[2].onCooldown && abilities[2].inRange)
						{
							Target(tar.name);
							StopFollowing();
							if (me.isAutoAttack)
								Thread.Sleep(2000);

							Functions.SendKeyDown(PID, Functions.VK_3);
							Functions.SendKeyUp(PID, Functions.VK_3);
							Thread.Sleep(3000);
							continue;
						}
					}
					else if( tar.HpPercent < 100 && !tar.HasBuff(Buffs.Renew))
					{
						//renew
						if ( !abilities[3].onCooldown && abilities[3].inRange)
						{
							Target(tar.name);
							if (me.isAutoAttack)
								Thread.Sleep(2000);

							Functions.SendKeyDown(PID, Functions.VK_4);
							Functions.SendKeyUp(PID, Functions.VK_4);
							Thread.Sleep(200);
							continue;
						}
					}
				}			

				if( !me.isFollowing && (!me.isDrinking || me.ManaPercent == 100))
				{
					if( players.Count > 1 && players[1].exists)
					{
						//Target("party1");
						StartFollowing();
						continue;
					}
				}

				if (me.inCombat && !me.isAutoAttack && useWand)
				{
					StopFollowing();
					Assist();
					Functions.SendKeyDown(PID, Functions.VK_T);
					Functions.SendKeyUp(PID, Functions.VK_T);
					Thread.Sleep(1000);
					continue;
				}

				if( doLoot )
				{
					doLoot = false;
					Functions.SendKeyDown(PID, Functions.VK_F6);
					Functions.SendKeyUp(PID, Functions.VK_F6);
					Thread.Sleep(50);
					Functions.SendKeyDown(PID, Functions.VK_F8);
					Functions.SendKeyUp(PID, Functions.VK_F8);
					Thread.Sleep(50);
					continue;
				}


				//



				//Functions.SendKeyDown(PID, Functions.VK_4);
				//Functions.SendKeyUp(PID, Functions.VK_4);

				/*
				if (!IsFishing && (DateTime.Now - FishingTimeDelay).TotalMilliseconds > rand.Next(2000, 2500))
				{
					BobberFound = false;
					BobberSplashed = false;

					FishingTimeDelay = DateTime.Now;
					Functions.SendKeyDown(PID, Functions.VK_3);
					Functions.SendKeyUp(PID, Functions.VK_3);
				}

				if (Functions.GetPixelColor(1935, 1038) != Color.FromArgb(0, 255, 0))
				{
					IsFishing = false;
					Thread.Sleep(50);
					continue;
				}
				else
				{
					if (!IsFishing)
					{
						IsFishing = true;
						curX = startX;
						curY = startY;
					}
				}


				if (!BobberFound)
				{
					if (Functions.GetPixelColor(1935, 1031) != Color.FromArgb(0, 255, 0))
						Cursor.Position = new Point(curX, curY);
					else
						BobberFound = true;

					curX += 20;

					if (curX > endX)
					{
						curY += 30;
						curX = startX;
					}
				}

				if (Functions.GetPixelColor(1935, 1025) != Color.FromArgb(0, 255, 0))
					BobberSplashed = false;
				else
					BobberSplashed = true;


				/*
				if (BobberSplashed && BobberFound)
				{
					Thread.Sleep(rand.Next(100, 500));

					Functions.mouse_event(Functions.MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
					Thread.Sleep(rand.Next(5, 20));
					Functions.mouse_event(Functions.MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
					FishingTimeDelay = DateTime.Now;
				}
				*/






			}
		}

	}
}
