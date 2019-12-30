using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;

using System.Diagnostics;

using System.Runtime.InteropServices;
using System.Threading;


namespace WoWHealthMonitor
{
	public static class Functions
	{
		static public UInt32 VK_ENTER = 0x0D;
		static public UInt32 VK_SPACE = 0x20;
		static public UInt32 VK_PAUSE = 0x13;
		static public UInt32 VK_ESCAPE = 0x1B;

		static public UInt32 VK_A = 0x41;
		static public UInt32 VK_B = 0x42;
		static public UInt32 VK_C = 0x43;
		static public UInt32 VK_D = 0x44;
		static public UInt32 VK_E = 0x45;
		static public UInt32 VK_F = 0x46;
		static public UInt32 VK_G = 0x47;
		static public UInt32 VK_H = 0x48;
		static public UInt32 VK_I = 0x49;
		static public UInt32 VK_J = 0x4A;
		static public UInt32 VK_K = 0x4B;
		static public UInt32 VK_L = 0x4C;
		static public UInt32 VK_M = 0x4D;
		static public UInt32 VK_N = 0x4E;
		static public UInt32 VK_O = 0x4F;
		static public UInt32 VK_P = 0x50;
		static public UInt32 VK_Q = 0x51;
		static public UInt32 VK_R = 0x52;
		static public UInt32 VK_S = 0x53;
		static public UInt32 VK_T = 0x54;
		static public UInt32 VK_U = 0x55;
		static public UInt32 VK_V = 0x56;
		static public UInt32 VK_W = 0x57;
		static public UInt32 VK_X = 0x58;
		static public UInt32 VK_Y = 0x59;
		static public UInt32 VK_Z = 0x5A;

		public static UInt32 VK_0 = 0x30;
		public static UInt32 VK_1 = 0x31;
		public static UInt32 VK_2 = 0x32;
		public static UInt32 VK_3 = 0x33;
		public static UInt32 VK_4 = 0x34;
		public static UInt32 VK_5 = 0x35;
		public static UInt32 VK_6 = 0x36;
		public static UInt32 VK_7 = 0x37;
		public static UInt32 VK_8 = 0x38;
		public static UInt32 VK_9 = 0x39;

		public static UInt32 VK_F1 = 0x70;
		public static UInt32 VK_F2 = 0x71;
		public static UInt32 VK_F3 = 0x72;
		public static UInt32 VK_F4 = 0x73;
		public static UInt32 VK_F5 = 0x74;
		public static UInt32 VK_F6 = 0x75;
		public static UInt32 VK_F7 = 0x76;
		public static UInt32 VK_F8 = 0x77;
		public static UInt32 VK_F9 = 0x78;

		public static UInt32 VK_NUMPAD0 = 0x60;
		public static UInt32 VK_NUMPAD1 = 0x61;
		public static UInt32 VK_NUMPAD2 = 0x62;
		public static UInt32 VK_NUMPAD3 = 0x63;
		public static UInt32 VK_NUMPAD4 = 0x64;
		public static UInt32 VK_NUMPAD5 = 0x65;
		public static UInt32 VK_NUMPAD6 = 0x66;
		public static UInt32 VK_NUMPAD7 = 0x67;
		public static UInt32 VK_NUMPAD8 = 0x68;
		public static UInt32 VK_NUMPAD9 = 0x69;

		public static UInt32 WM_KEYDOWN = 0x0100;
		public static UInt32 WM_KEYUP = 0x0101;

		public static UInt32 KEYEVENTF_EXTENDEDKEY = 0x1;
		public static UInt32 KEYEVENTF_KEYUP = 0x2;


		public static UInt32 REPEAT_BIT = 0x00000001;
		public static UInt32 SCANCODE_BIT = 0x00010000;
		public static UInt32 EXT_KEY_BIT = 0x01000000;
		public static UInt32 CONTEXT_BIT = 0x20000000;
		public static UInt32 PREV_STATE_BIT = 0x40000000;
		public static UInt32 TRANS_STATE_BIT = 0x80000000;

		public static UInt32 MOUSEEVENTF_RIGHTDOWN = 0x08;
		public static UInt32 MOUSEEVENTF_RIGHTUP = 0x10;

		public static UInt32 MOUSEEVENTF_ABSOLUTE = 0x8000;

		[DllImport("user32.dll")]
		public static extern void mouse_event(uint dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

		[DllImport("user32.dll")]
		public static extern IntPtr GetDC(IntPtr hwnd);

		[DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
		public static extern bool SetWindowText(IntPtr hwnd, String lpString);

		[DllImport("user32.dll")]
		public static extern Int32 ReleaseDC(IntPtr hwnd, IntPtr hdc);

		[DllImport("gdi32.dll")]
		public static extern UInt32 GetPixel(IntPtr hdc, int nXPos, int nYPos);

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
		public static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, UInt32 wParam, UInt32 lParam);

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern UInt32 MapVirtualKey(UInt32 uCode, Int32 uMapType);


		static public Color GetPixelColor(int x, int y)
		{
			IntPtr hdc = GetDC(IntPtr.Zero);

			UInt32 pixel = GetPixel(hdc, x, y);
			ReleaseDC(IntPtr.Zero, hdc);
			
			return Color.FromArgb((int)(pixel & 0xFF), (int)(pixel & 0xFF00) >> 8, (int)(pixel & 0xFF0000) >> 16);
			//return Color.FromArgb((int)pixel);
		}

		static public void SendKeyUp(Process PID, UInt32 msg)
		{
			if (PID == null)
				return;

			UInt32 hi = msg;
			UInt32 lo = REPEAT_BIT | (SCANCODE_BIT * MapVirtualKey(msg, 0)) | EXT_KEY_BIT | PREV_STATE_BIT | TRANS_STATE_BIT;
			SendMessage(PID.MainWindowHandle, WM_KEYUP, hi, lo);
		}

		static public void SendKeyDown(Process PID, UInt32 msg)
		{
			if (PID == null)
				return;

			UInt32 hi = msg;
			UInt32 lo = REPEAT_BIT | (SCANCODE_BIT * MapVirtualKey(msg, 0)) | EXT_KEY_BIT;
			SendMessage(PID.MainWindowHandle, WM_KEYDOWN, hi, lo);
		}

	}
}
