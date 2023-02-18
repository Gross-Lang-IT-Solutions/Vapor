using System.Runtime.InteropServices;

namespace CustomForms;

internal static class WinApi
{
	public enum SetWindowPosFlags : uint
	{
		NoSize = 0x0001,
		NoMove = 0x0002,
		NoActivate = 0x0010,
	}

	public enum WindowCompositionAttribute
	{
		WCA_ACCENT_POLICY = 19
	}

	public enum AccentState
	{
		ACCENT_DISABLED = 0,
		ACCENT_ENABLE_GRADIENT = 1,
		ACCENT_ENABLE_TRANSPARENTGRADIENT = 2,
		ACCENT_ENABLE_BLURBEHIND = 3,
		ACCENT_INVALID_STATE = 4
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct WindowCompositionAttributeData
	{
		public WindowCompositionAttribute Attribute;
		public nint Data;
		public int SizeOfData;
	}

	[StructLayout(LayoutKind.Sequential)]
	public struct AccentPolicy
	{
		public AccentState AccentState;
		public int AccentFlags;
		public int GradientColor;
		public int AnimationId;
	}

	[DllImport("user32.dll")]
	public static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);

	[DllImport("user32.dll")]
	[return: MarshalAs(UnmanagedType.Bool)]
	public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, SetWindowPosFlags uFlags);

	[DllImport("gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
	public static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);
}
