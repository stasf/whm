using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Timers;
using System.Runtime.InteropServices;
using System.Threading;
using System.IO;

namespace WoWHealthMonitor
{
	public partial class Form1 : Form
	{
		[DllImport("kernel32.dll")]
		static extern IntPtr GetModuleHandle(string moduleName);

		[DllImport("user32.dll")]
		static extern IntPtr SetWindowsHookEx(UInt32 idHook, HookProc lpfn, IntPtr hMod, uint dwThreadId);

		[DllImport("user32.dll")]
		static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, uint wParam, ref KBDLLHOOKSTRUCT lParam);

		delegate IntPtr HookProc(int nCode, uint wParam, ref KBDLLHOOKSTRUCT lParam);

		System.Timers.Timer aTimer;
		Thread ScanThread, BotThread;

		private Process PID = null;

		private HookProc hookProc;
		private IntPtr hook;

		static UInt32 WH_KEYBOARD_LL = 0x0D;
		static UInt32 WM_KEYDOWN = 0x0100;
		static UInt32 WM_KEYUP = 0x0101;
		static UInt32 WM_SYSKEYDOWN = 0x0104;
		static UInt32 WM_SYSKEYUP = 0x0105;

		public Form1()
		{
			InitializeComponent();
			this.FormClosing += Form1_FormClosing;
		}

		void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			try
			{
				ScanThread.Abort();
				BotThread.Abort();
			}
			catch
			{
			}
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			BotRoutine.Init();

			hookProc = new HookProc(LowLevelKeyboardProc);
			hook = SetWindowsHookEx(WH_KEYBOARD_LL, hookProc, GetModuleHandle(null), 0);

			ScanThread = new Thread(ScreenScanner.ScanLoop);
			ScanThread.Start();
			GC.KeepAlive(ScanThread);

			BotThread = new Thread(BotRoutine.MainLoop);
			BotThread.Start();
			GC.KeepAlive(BotThread);

			aTimer = new System.Timers.Timer();
			aTimer.Elapsed += new ElapsedEventHandler(MainLoop);
			aTimer.Interval = 200;
			aTimer.AutoReset = true;
			aTimer.Enabled = true;
			aTimer.SynchronizingObject = this;
			GC.KeepAlive(aTimer);

			UpdatePIDList();
		}

		private void MainLoop(object source, ElapsedEventArgs e)
		{
			lvPlayers.Items.Clear();
			lvAbilities.Items.Clear();

			List<Player> players = new List<Player>();
			List<Ability> abilities = new List<Ability>();

			lock (ScreenScanner.WriteLock)
			{
				foreach (Player p in ScreenScanner.players)
					players.Add(p.ShallowCopy());

				foreach (Ability a in ScreenScanner.abilities)
					abilities.Add(a.ShallowCopy());
			}

			for( int i = 0; i < players.Count; i++)
			{
				ListViewItem lvi = new ListViewItem();

				if (!players[i].exists)
					continue;

				lvi.Text = players[i].name;

				lvi.SubItems.Add(players[i].HpCur.ToString());
				lvi.SubItems.Add(players[i].HpMax.ToString());
				if (i == 0)
				{
					lvi.SubItems.Add(players[i].ManaCur.ToString());
					lvi.SubItems.Add(players[i].ManaMax.ToString());
				}

				lvPlayers.Items.Add(lvi);
			}

			for (int i = 0; i < abilities.Count; i++)
			{
				ListViewItem lvi = new ListViewItem();

				lvi.Text = String.Format("Action{0}", i);

				lvi.SubItems.Add(abilities[i].inRange.ToString());
				lvi.SubItems.Add(abilities[i].onCooldown.ToString());

				lvAbilities.Items.Add(lvi);
			}
			if (players.Count != 0)
			{
				cbCasting.Checked = players[0].isCasting;
				cbCombat.Checked = players[0].inCombat;
				cbDrinking.Checked = players[0].isDrinking;
				cbFollowing.Checked = players[0].isFollowing;
				cbAutoAttack.Checked = players[0].isAutoAttack;
			}
		}


		IntPtr LowLevelKeyboardProc(int nCode, uint wParam, ref KBDLLHOOKSTRUCT lParam)
		{
			if (nCode < 0)
				return CallNextHookEx(IntPtr.Zero, nCode, wParam, ref lParam);

			if (wParam == WM_KEYUP || wParam == WM_SYSKEYUP)
			{
				if (lParam.vkCode == 19)
					BotRoutine.StartScript = !BotRoutine.StartScript;

				if (lParam.vkCode == Functions.VK_F)
					BotRoutine.doLoot = true;
				//this.Text = Convert.ToChar(lParam.vkCode) + " is up";
			}

			return CallNextHookEx(IntPtr.Zero, nCode, wParam, ref lParam);
		}

		private void btnStart_Click(object sender, EventArgs e)
		{
			if (BotRoutine.StartScript)
			{
				BotRoutine.StartScript = false;
				btnStart.Text = "Start";
			}
			else
			{
				BotRoutine.StartScript = true;
				btnStart.Text = "Stop";
			}
		}

		private void btnRefresh_Click(object sender, EventArgs e)
		{
			UpdatePIDList();
		}

		private void btnSelect_Click(object sender, EventArgs e)
		{
			if (lstProcessList.SelectedItem == null)
				return;

			try
			{
				String process = lstProcessList.SelectedItem.ToString();
				process = process.Substring(0, process.IndexOf('-'));

				PID = Process.GetProcessById(Int32.Parse(process, System.Globalization.NumberStyles.HexNumber));
				BotRoutine.PID = PID;
				this.Text = PID.MainWindowTitle + " - " + PID.Id;
				btnStart.Enabled = true;
			}

			catch
			{
				BotRoutine.StartScript = false;
				btnStart.Enabled = false;
			}
		}

		private void cbUseWand_CheckedChanged(object sender, EventArgs e)
		{
			lock (BotRoutine.wandLock)
			{
				BotRoutine.useWand = !BotRoutine.useWand;
			}
		}

		private void UpdatePIDList()
		{
			Process[] pList = Process.GetProcesses();
			UInt32 pCounter = 0;
			Process pFirst = null;

			//for when i wanna sort dis sheeeeeeit
			//gotta figure out how to sort the list properly
			IEnumerable<Process> sortedProcesses =
				from process in pList
				orderby process.Id ascending
				select process;

			lstProcessList.Items.Clear();
			foreach (Process p in pList)
			{
				try
				{
					if (p.MainWindowTitle.Contains("World of Warcraft"))
					{
						if (p.MainWindowTitle == "World of Warcraft")
							Functions.SetWindowText(p.MainWindowHandle, p.Id.ToString("X8") + "-" + p.MainWindowTitle);

						lstProcessList.Items.Add(p.Id.ToString("X8") + "-" + p.MainWindowTitle);
						if (pCounter == 0)
							pFirst = p;
						pCounter++;
					}
				}
				catch
				{

				}
			}

			if (pCounter == 1)
			{
				this.Text = pFirst.MainWindowTitle + " - " + pFirst.Id;
				PID = pFirst;
				BotRoutine.PID = PID;
				btnStart.Enabled = true;
			}


		}

	}
}
public struct KBDLLHOOKSTRUCT
{
	public Int32 vkCode;
	Int32 scanCode;
	public Int32 flags;
	Int32 time;
	IntPtr dwExtraInfo;
}