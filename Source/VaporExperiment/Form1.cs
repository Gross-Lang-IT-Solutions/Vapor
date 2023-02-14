using System.Runtime.InteropServices;

namespace VaporExperiment;

public partial class Form1 : Form
{
    public const int WM_NCLBUTTONDOWN = 0xA1;
    public const int HTCAPTION = 0x2;


    [LibraryImport("user32")]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static partial bool ReleaseCapture();

    [DllImport("user32")]
    public static extern IntPtr SendMessage(IntPtr hWnd, uint uMsg, int wParam, int lParam);

    public Form1()
    {
        InitializeComponent();
        titlebar.MouseDown += (sender, args) =>
        {
            ReleaseCapture();
            SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
        };
    }

    protected override void OnPaintBackground(PaintEventArgs e)
    {
        base.OnPaintBackground(e);
    }

    private const nint HTLEFT = 10;
    private const nint HTRIGHT = 11;
    private const nint HTTOP = 12;
    private const nint HTTOPLEFT = 13;
    private const nint HTTOPRIGHT = 14;
    private const nint HTBOTTOM = 15;
    private const nint HTBOTTOMLEFT = 16;
    private const nint HTBOTTOMRIGHT = 17;

    protected override void WndProc(ref Message m)
    {
        base.WndProc(ref m);
        if (m.Msg == 0x84)
        {
            var mp = PointToClient(Cursor.Position);

            if (TopLeft.Contains(mp))
                m.Result = HTTOPLEFT;
            else if (TopRight.Contains(mp))
                m.Result = HTTOPRIGHT;
            else if (BottomLeft.Contains(mp))
                m.Result = HTBOTTOMLEFT;
            else if (BottomRight.Contains(mp))
                m.Result = HTBOTTOMRIGHT;
            else if (Top.Contains(mp))
                m.Result = HTTOP;
            else if (Left.Contains(mp))
                m.Result = HTLEFT;
            else if (Right.Contains(mp))
                m.Result = HTRIGHT;
            else if (Bottom.Contains(mp))
                m.Result = HTBOTTOM;
        }
    }

    private Random rng = new Random();
    public Color randomColour()
    {
        return Color.FromArgb(255, rng.Next(255), rng.Next(255), rng.Next(255));
    }

    const int ImaginaryBorderSize = 4;

    public new Rectangle Top => new(0, 0, ClientSize.Width, ImaginaryBorderSize);
    public new Rectangle Left => new(0, 0, ImaginaryBorderSize, ClientSize.Height);
    public new Rectangle Bottom => new(0, ClientSize.Height - ImaginaryBorderSize, ClientSize.Width, ImaginaryBorderSize);
    public new Rectangle Right => new(ClientSize.Width - ImaginaryBorderSize, 0, ImaginaryBorderSize, ClientSize.Height);
    public Rectangle TopLeft => new(0, 0, ImaginaryBorderSize, ImaginaryBorderSize);
    public Rectangle TopRight => new(ClientSize.Width - ImaginaryBorderSize, 0, ImaginaryBorderSize, ImaginaryBorderSize);
    public Rectangle BottomLeft => new(0, ClientSize.Height - ImaginaryBorderSize, ImaginaryBorderSize, ImaginaryBorderSize);
    public Rectangle BottomRight => new(ClientSize.Width - ImaginaryBorderSize, ClientSize.Height - ImaginaryBorderSize, ImaginaryBorderSize, ImaginaryBorderSize);
}
