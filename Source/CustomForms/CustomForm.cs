namespace CustomForms;

public partial class CustomForm : Form
{
	private CustomFormBorder? border;

	public CustomForm()
	{
		InitializeComponent();
	}

	protected override void OnLoad(EventArgs e)
	{
		base.OnLoad(e);

		border = new(this);
		border.Show();
	}

	private void UpdateBorderPos()
	{
		if (border != null)
			WinApi.SetWindowPos(border.Handle, Handle, 0, 0, 0, 0, WinApi.SetWindowPosFlags.NoActivate | WinApi.SetWindowPosFlags.NoSize | WinApi.SetWindowPosFlags.NoMove);
	}

	protected override void OnClosed(EventArgs e)
	{
		base.OnClosed(e);
		border?.Close();
	}

	protected override void OnActivated(EventArgs e)
	{
		base.OnActivated(e);

		if (border != null)
			UpdateBorderPos();
	}

	protected override void OnGotFocus(EventArgs e)
	{
		base.OnGotFocus(e);

		UpdateBorderPos();
	}

	protected override void OnAutoSizeChanged(EventArgs e)
	{
		base.OnAutoSizeChanged(e);
	}

	protected override void OnLocationChanged(EventArgs e)
	{
		base.OnLocationChanged(e);

		if (border != null)
			border.Location = Location - new Size(CustomFormBorder.ResizeBorderSize + 1, CustomFormBorder.ResizeBorderSize + 1);
	}

	protected override void OnSizeChanged(EventArgs e)
	{
		base.OnSizeChanged(e);
		MaximizedBounds = Screen.GetWorkingArea(this);
		if (border != null)
			border.Size = Size + new Size((CustomFormBorder.ResizeBorderSize * 2) + 2, (CustomFormBorder.ResizeBorderSize * 2) + 2);
	}
}
