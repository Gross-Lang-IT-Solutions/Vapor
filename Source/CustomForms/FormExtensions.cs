using System.Drawing.Drawing2D;

namespace CustomForms;

internal static class FormExtensions
{
	public static unsafe void EnableTransparentBlur(this Form self)
	{
		var accent = new WinApi.AccentPolicy
		{
			AccentState = WinApi.AccentState.ACCENT_ENABLE_BLURBEHIND
		};

		var accentStructSize = sizeof(WinApi.AccentPolicy);

		var data = new WinApi.WindowCompositionAttributeData
		{
			Attribute = WinApi.WindowCompositionAttribute.WCA_ACCENT_POLICY,
			SizeOfData = accentStructSize,
			Data = (nint)(&accent)
		};
		WinApi.SetWindowCompositionAttribute(self.Handle, ref data);

		self.BackColor = Color.LightPink;
		self.TransparencyKey = Color.LightPink;
	}
	private static GraphicsPath CreatePath(Rectangle rect, int nRadius)
	{
		var path = new GraphicsPath();
		path.AddArc(rect.X, rect.Y, nRadius, nRadius, 180f, 90f);
		path.AddArc(rect.Right - nRadius, rect.Y, nRadius, nRadius, 270f, 90f);
		path.AddArc(rect.Right - nRadius, (rect.Bottom - nRadius), nRadius, nRadius, 0f, 90f);
		path.AddArc(rect.X, (rect.Bottom - nRadius), nRadius, nRadius, 90f, 90f);
		path.CloseFigure();
		return path;
	}
}
