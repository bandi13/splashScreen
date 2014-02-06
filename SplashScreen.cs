// Written by: Fekete András
// Date: 7/31/2009
// No warranties or guarantees of any kind.
// Released under the GNU public license

using System.Threading;
using System.Diagnostics;

namespace SplashScreen {
	/// <summary>
	/// Summary description for SplashScreen.
	/// </summary>
	public class SplashScreen : System.Windows.Forms.Form {
		// Threading
		static SplashScreen ms_frmSplash = null;
		static Thread ms_oThread = null;

		// Fade in and out.
		private double m_dblOpacityIncrement = .05;
		private const int TIMER_INTERVAL = 50;

		// Self-calibration support
		private System.Windows.Forms.Timer timer1;
		private System.ComponentModel.IContainer components;

		/// <summary>Constructor</summary>
		public SplashScreen() {
			InitializeComponent();
			this.Opacity = 0;
			timer1.Interval = TIMER_INTERVAL;
			timer1.Start();
			this.ClientSize = this.BackgroundImage.Size;
		}

		/// <summary>Clean up any resources being used.</summary>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) components.Dispose();
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SplashScreen));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // SplashScreen
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.LightGray;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(415, 499);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SplashScreen";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SplashScreen";
            this.TopMost = true;
            this.DoubleClick += new System.EventHandler(this.SplashScreen_DoubleClick);
            this.ResumeLayout(false);

		}
		#endregion

		// ************* Static Methods *************** //

		// A static method to create the thread and 
		// launch the SplashScreen.
		static public void ShowSplashScreen() {
			// Make sure it's only launched once.
			if (ms_frmSplash != null) return;
			ms_oThread = new Thread(new ThreadStart(SplashScreen.ShowForm));
			ms_oThread.Name = "SplashScreen";
			ms_oThread.IsBackground = true;
			ms_oThread.SetApartmentState(ApartmentState.STA);
			ms_oThread.Start();
		}

		// A property returning the splash screen instance
		static public SplashScreen SplashForm { get { return ms_frmSplash; } }

		// A private entry point for the thread.
		static private void ShowForm() {
			ms_frmSplash = new SplashScreen();
			System.Windows.Forms.Application.Run(ms_frmSplash);
		}

		// A static method to close the SplashScreen
		static public void CloseForm() {
			if (ms_frmSplash != null && ms_frmSplash.IsDisposed == false) {
				// Make it start going away.
				ms_frmSplash.m_dblOpacityIncrement = -ms_frmSplash.m_dblOpacityIncrement;
			}
			ms_oThread = null;	// we don't need these any more.
			ms_frmSplash = null;
		}

		//********* Event Handlers ************

		// Tick Event handler for the Timer control.  Handle fade in and fade out.  Also
		// handle the smoothed progress bar.
		private void timer1_Tick(object sender, System.EventArgs e) {
			if (m_dblOpacityIncrement > 0) {
				if (this.Opacity < 1) this.Opacity += m_dblOpacityIncrement;
				else m_dblOpacityIncrement = -m_dblOpacityIncrement;
			} else {
				if (this.Opacity > 0) this.Opacity += m_dblOpacityIncrement; else this.Close();
			}
		}

		// Close the form if they double click on it.
		private void SplashScreen_DoubleClick(object sender, System.EventArgs e) { CloseForm(); }
	}
}
