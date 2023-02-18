using System.Runtime.InteropServices;

namespace VaporExperiment;

public partial class VaporTitlebar : UserControl
{
	private const int WM_NCLBUTTONDOWN = 0xA1;
	private const int HTCAPTION = 0x2;

	[DllImport("user32")]
	[return: MarshalAs(UnmanagedType.Bool)]
	private static extern bool ReleaseCapture();

	[DllImport("user32")]
	private static extern IntPtr SendMessage(IntPtr hWnd, uint uMsg, int wParam, int lParam);

	private Form? owner;
	public Form? Owner
	{
		set
		{
			if (owner != null)
				owner.SizeChanged -= Owner_SizeChanged;
			owner = value;
			if (owner == null)
				return;

			owner.SizeChanged += Owner_SizeChanged;
		}
	}

	private void Owner_SizeChanged(object? sender, EventArgs e)
	{
		if (owner == null)
			throw new();

		MaximizeRestoreButton.BackgroundImage = owner.WindowState == FormWindowState.Normal
			? new Bitmap(Properties.Resources.IconMaximize, new(12, 12))
			: new Bitmap(Properties.Resources.IconRestore, new(12, 12));
	}

	public VaporTitlebar()
	{
		InitializeComponent();

		SetStyle(ControlStyles.ResizeRedraw, true);

		CloseButton.BackgroundImage = new Bitmap(Properties.Resources.IconClose, new(12, 12));
		CloseButton.BackgroundImageLayout = ImageLayout.Center;
		MaximizeRestoreButton.BackgroundImage = new Bitmap(Properties.Resources.IconMaximize, new(12, 12));
		MaximizeRestoreButton.BackgroundImageLayout = ImageLayout.Center;
		MinimizeButton.BackgroundImage = new Bitmap(Properties.Resources.IconMinimize, new(12, 12));
		MinimizeButton.BackgroundImageLayout = ImageLayout.Center;

		CloseButton.MouseEnter += (sender, args) => CloseButton.BackColor = Color.FromArgb(200, 20, 20);
		CloseButton.MouseLeave += (sender, args) => CloseButton.BackColor = BackColor;
		MaximizeRestoreButton.MouseEnter += (sender, args) => MaximizeRestoreButton.BackColor = Color.FromArgb(20, 20, 20);
		MaximizeRestoreButton.MouseLeave += (sender, args) => MaximizeRestoreButton.BackColor = BackColor;
		MinimizeButton.MouseEnter += (sender, args) => MinimizeButton.BackColor = Color.FromArgb(20, 20, 20);
		MinimizeButton.MouseLeave += (sender, args) => MinimizeButton.BackColor = BackColor;
	}

	const int logoPadding = 6;

	protected override void OnPaint(PaintEventArgs pe)
	{
		base.OnPaint(pe);

		using var col = new SolidBrush(BackColor);
		pe.Graphics.FillRectangle(col, DisplayRectangle);

		var logoImg = Properties.Resources.VaporLogo;
		var size = logoImg.Size;
		size /= logoImg.Height / (Height - logoPadding);
		var loc = new Point((Width - size.Width) / 2, (Height - size.Height) / 2);
		pe.Graphics.DrawImage(Properties.Resources.VaporLogo, new Rectangle(loc, size));
	}

	private bool mouseDown = false;

	protected override void OnMouseDown(MouseEventArgs e)
	{
		base.OnMouseDown(e);
		mouseDown = true;
	}

	protected override void OnDoubleClick(EventArgs e)
	{
		base.OnDoubleClick(e);
		if (owner != null)
			owner.WindowState = owner.WindowState == FormWindowState.Normal ? FormWindowState.Maximized : FormWindowState.Normal;
	}

	protected override void OnMouseMove(MouseEventArgs e)
	{
		base.OnMouseMove(e);

		if (mouseDown)
		{
			ReleaseCapture();
			if (owner != null)
				SendMessage(owner.Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
		}

	}

	private void CloseButton_Click(object sender, EventArgs e)
	{
		owner?.Close();
	}

	private void MaximizeRestoreButton_Click(object sender, EventArgs e)
	{

		if (owner != null)
			owner.WindowState = owner.WindowState == FormWindowState.Normal ? FormWindowState.Maximized : FormWindowState.Normal;
	}

	private void MinimizeButton_Click(object sender, EventArgs e)
	{
		if (owner != null)
			owner.WindowState = FormWindowState.Minimized;
	}
}
