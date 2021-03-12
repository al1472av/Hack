using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Threading;
using Hack.Extensions;
using Point = System.Drawing.Point;

namespace Hack
{
    public class Overlay : ComponentBase
    {
        protected override int _sleepTime => 1000;
        private Form _window;

        public Overlay()
        {
            _window = new Form
            {
                Name = "Overlay Window",
                Text = "Overlay Window",
                MinimizeBox = false,
                MaximizeBox = false,
                FormBorderStyle = FormBorderStyle.None,
                TopMost = true,
                Width = 500,
                Height = 500,
                Left = 500,
                Top = -500,
                StartPosition = FormStartPosition.Manual,
            };

            _window.Load += OnFormLoaded;
            _window.SizeChanged += ExtendFrameIntoClientArea;
            _window.LocationChanged += ExtendFrameIntoClientArea;
            _window.Closed += (sender, args) => System.Windows.Application.Current.Shutdown();

            _window.Show();
        }

        protected override void Action()
        {
            Update(GameHacker.WindowRectangleClient);
        }

        private void Update(Rectangle windowRectangleClient)
        {
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                _window.BackColor = Color.Blue; // TODO: temporary
                _window.Location = GameHacker.IsWindowActive ? windowRectangleClient.Location : new Point(0, 0);
                _window.Size = GameHacker.IsWindowActive ? windowRectangleClient.Size : new Size(0, 0);
                
            }, DispatcherPriority.Normal);
        }

        private void OnFormLoaded(object sender, EventArgs args)
        {
            var style = User32.GetWindowLong(_window.Handle, User32.GWL_EXSTYLE);
            style |= User32.WS_EX_LAYERED;
            style |= User32.WS_EX_TRANSPARENT;

            User32.SetWindowLong(_window.Handle, User32.GWL_EXSTYLE, (IntPtr) style);

            User32.SetLayeredWindowAttributes(_window.Handle, 0, 255, User32.LWA_ALPHA);
        }

        private void ExtendFrameIntoClientArea(object sender, EventArgs args)
        {
            var margins = new Margins
            {
                Left = -1,
                Right = -1,
                Top = -1,
                Bottom = -1,
            };
            Dwmapi.DwmExtendFrameIntoClientArea(_window.Handle, ref margins);
        }
    }
}