using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.Utils.Menu;
using DevExpress.XtraScheduler;

namespace WindowsApplication1 {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void schedulerControl1_PreparePopupMenu(object sender, DevExpress.XtraScheduler.PreparePopupMenuEventArgs e) {
            if (e.Menu.Id == SchedulerMenuItemId.AppointmentMenu) {
                DXMenuItem oldDelete = GetMenuItem(e.Menu, "&Delete");
                oldDelete.Visible = false;
                DXMenuItem newDelete = new DXMenuItem("&Delete", Deleted_Click, oldDelete.Image);
                e.Menu.Items.Add(newDelete);
            }
        }

        private bool deleteBtnClicked = false;
        void Deleted_Click(object sender, EventArgs e) {
            deleteBtnClicked = true;
            this.schedulerStorage1.Appointments.Remove(this.schedulerControl1.SelectedAppointments[0]);
        }

        private void schedulerStorage1_AppointmentDeleting(object sender, PersistentObjectCancelEventArgs e) {
            if (deleteBtnClicked) {
                DialogResult res = MessageBox.Show("Are you sure?", "Please confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res != DialogResult.Yes)
                    e.Cancel = true;
            }
            deleteBtnClicked = false;
        }

        protected DXMenuItem GetMenuItem(SchedulerPopupMenu menu, string menuItemName) {
            DXMenuItem foundItem = null;
            foreach (DXMenuItem item in menu.Items) {
                if (item.Caption == menuItemName)
                    foundItem = item;
            }
            return foundItem;
        }
    }
}