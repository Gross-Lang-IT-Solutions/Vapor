using System.ComponentModel;
using System.Reflection;

namespace CustomForms;

internal sealed class CustomFormBorder : Form
{
	protected override bool ShowWithoutActivation { get { return true; } }

	private readonly CustomForm owner;

	public const int ResizeBorderSize = 8;

	protected override CreateParams CreateParams
	{
		get
		{

			const int WS_EX_TOOLWINDOW = 0x00000080;

			var cp = base.CreateParams;
			// Hide from taskbar and Alt+Tab
			cp.ExStyle |= WS_EX_TOOLWINDOW;
			return cp;
		}
	}

	public CustomFormBorder(CustomForm owner)
	{
		this.owner = owner;

		SetStyle(ControlStyles.ResizeRedraw, true);

		DoubleBuffered = true;
		FormBorderStyle = FormBorderStyle.None;
		TransparencyKey = Color.LightPink;
		BackColor = Color.LightPink;
		ShowInTaskbar = false;
	}

	private void InsertAfterOwner() => WinApi.SetWindowPos(Handle, owner.Handle, 0, 0, 0, 0, WinApi.SetWindowPosFlags.NoActivate | WinApi.SetWindowPosFlags.NoSize | WinApi.SetWindowPosFlags.NoMove);
	private void InsertOwnerAfter() => WinApi.SetWindowPos(owner.Handle, Handle, 0, 0, 0, 0, WinApi.SetWindowPosFlags.NoActivate | WinApi.SetWindowPosFlags.NoSize | WinApi.SetWindowPosFlags.NoMove);

	protected override void OnClosing(CancelEventArgs e)
	{
		base.OnClosing(e);

		e.Cancel = true;
	}

	protected override void OnPaint(PaintEventArgs e)
	{
		base.OnPaint(e);
		var rect = DisplayRectangle;
		rect.Offset(ResizeBorderSize, ResizeBorderSize);
		rect.Size -= new Size((ResizeBorderSize * 2) + 1, (ResizeBorderSize * 2) + 1);
		e.Graphics.DrawRectangle(Pens.DimGray, rect);
	}

	protected override void OnGotFocus(EventArgs e)
	{
		base.OnGotFocus(e);

		InsertOwnerAfter();
		InsertAfterOwner();
	}

	protected override void OnResizeBegin(EventArgs e)
	{
		base.OnResizeBegin(e);

		InsertOwnerAfter();
		InsertAfterOwner();
	}

	protected override void OnResizeEnd(EventArgs e)
	{
		base.OnResizeEnd(e);

		owner.Focus();
		InsertAfterOwner();
	}

	protected override void OnSizeChanged(EventArgs e)
	{
		base.OnSizeChanged(e);

		owner.Size = Size - new Size((ResizeBorderSize * 2) + 2, (ResizeBorderSize * 2) + 2);

		InsertOwnerAfter();
		InsertAfterOwner();

		MinimumSize = owner.MinimumSize + new Size((ResizeBorderSize * 2) + 2, (ResizeBorderSize * 2) + 2);
	}

	protected override void OnLocationChanged(EventArgs e)
	{
		base.OnLocationChanged(e);

		owner.Location = Location + new Size(ResizeBorderSize + 1, ResizeBorderSize + 1);
	}

	private Rectangle borderTopRect => new(0, 0, ClientSize.Width, ResizeBorderSize);
	private Rectangle borderLeftRect => new(0, 0, ResizeBorderSize, ClientSize.Height);
	private Rectangle borderBottomRect => new(0, ClientSize.Height - ResizeBorderSize, ClientSize.Width, ResizeBorderSize);
	private Rectangle borderRightRect => new(ClientSize.Width - ResizeBorderSize, 0, ResizeBorderSize, ClientSize.Height);
	private Rectangle borderTopLeftRect => new(0, 0, ResizeBorderSize, ResizeBorderSize);
	private Rectangle borderTopRightRect => new(ClientSize.Width - ResizeBorderSize, 0, ResizeBorderSize, ResizeBorderSize);
	private Rectangle borderBottomLeftRect => new(0, ClientSize.Height - ResizeBorderSize, ResizeBorderSize, ResizeBorderSize);
	private Rectangle borderBottomRightRect => new(ClientSize.Width - ResizeBorderSize, ClientSize.Height - ResizeBorderSize, ResizeBorderSize, ResizeBorderSize);

	protected override void WndProc(ref Message m)
	{
		const int WM_NCHITTEST = 0x0084;

		const nint HTLEFT = 10;
		const nint HTRIGHT = 11;
		const nint HTTOP = 12;
		const nint HTTOPLEFT = 13;
		const nint HTTOPRIGHT = 14;
		const nint HTBOTTOM = 15;
		const nint HTBOTTOMLEFT = 16;
		const nint HTBOTTOMRIGHT = 17;

		base.WndProc(ref m);

		if (m.Msg == WM_NCHITTEST)
		{
			var mp = PointToClient(Cursor.Position);

			if (borderTopLeftRect.Contains(mp))
				m.Result = HTTOPLEFT;
			else if (borderTopRightRect.Contains(mp))
				m.Result = HTTOPRIGHT;
			else if (borderBottomLeftRect.Contains(mp))
				m.Result = HTBOTTOMLEFT;
			else if (borderBottomRightRect.Contains(mp))
				m.Result = HTBOTTOMRIGHT;
			else if (borderTopRect.Contains(mp))
				m.Result = HTTOP;
			else if (borderLeftRect.Contains(mp))
				m.Result = HTLEFT;
			else if (borderRightRect.Contains(mp))
				m.Result = HTRIGHT;
			else if (borderBottomRect.Contains(mp))
				m.Result = HTBOTTOM;
		}
	}
}
