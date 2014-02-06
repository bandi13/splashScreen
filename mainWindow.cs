// Written by: Fekete András
// Date: 7/31/2009
// No warranties or guarantees of any kind.
// Released under the GNU public license

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LNL_Interface {
	public partial class mainWindow : Form {
		public mainWindow() {
			InitializeComponent();
            SplashScreen.SplashScreen.ShowSplashScreen();
        }

        private void button1_Click(object sender, EventArgs e) { this.Close(); }
	}
}