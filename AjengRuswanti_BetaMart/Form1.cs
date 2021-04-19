using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AjengRuswanti_BetaMart
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void TxtCariBarang_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                if (string.IsNullOrEmpty(txtCariBarang.Text))
                    dataGridView.DataSource = barangBindingSource;
                else
                {
                    var query = from o in this.barangData.Barang
                                where o.KodeBarang.Contains(txtCariBarang.Text) || o.NamaBarang == txtCariBarang.Text || o.HargaBarang == txtCariBarang.Text || o.JumlahBarang.Contains(txtCariBarang.Text)
                                select o;
                    dataGridView.DataSource = query.ToList();
                }
            }
        }

        private void DataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Delete)
            {
                if (MessageBox.Show("Apakah Anda yakin akan menghapus data ini?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    barangBindingSource.RemoveCurrent();
            }
        }

        private void BtnTambah_Click(object sender, EventArgs e)
        {
            try
            {
                panel.Enabled = true;
                txtKode.Focus();
                this.barangData.Barang.AddBarangRow(this.barangData.Barang.NewBarangRow());
                barangBindingSource.MoveLast();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                barangBindingSource.ResetBindings(false);
            }
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            panel.Enabled = true;
            txtKode.Focus();
        }

        private void BtnBatal_Click(object sender, EventArgs e)
        {
            panel.Enabled = false;
            barangBindingSource.ResetBindings(false);
        }

        private void BtnSimpan_Click(object sender, EventArgs e)
        {
            try
            {
                barangBindingSource.EndEdit();
                barangTableAdapter.Update(this.barangData.Barang);
                panel.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                barangBindingSource.ResetBindings(false);
            }
        }

        private void BtnHapus_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Apakah Anda yakin akan menghapus data ini?", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                barangBindingSource.RemoveCurrent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'barangData.Barang' table. You can move, or remove it, as needed.
            this.barangTableAdapter.Fill(this.barangData.Barang);
            barangBindingSource.DataSource = this.barangData.Barang;
        }
    }
}
