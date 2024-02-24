using capaEntidad;
using capaNegocio;

namespace capaPresentacion
{
    public partial class frClientes : Form
    {
        CNClient cNClient = new CNClient();
        public frClientes()
        {
            InitializeComponent();
        }

        private void frClientes_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            gridData.DataSource = cNClient.GetData().Tables["clientsTable"];
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            ResetValues();
        }

        private void ResetValues()
        {
            numericUpDownId.Value = 0;
            txtName.Text = "";
            txtLastName.Text = "";
            pictureBox1.Image = null;
        }

        private void linkLabelPhoto_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.FileName = "";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.Load(openFileDialog1.FileName);
            }

            openFileDialog1.FileName = "";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            bool Result;
            CEClients cECliente = new CEClients();
            cECliente.Id = (int)numericUpDownId.Value;
            cECliente.Name = txtName.Text;
            cECliente.LastName = txtLastName.Text;
            cECliente.PhotoPath = pictureBox1.ImageLocation;

            Result = cNClient.ValidateData(cECliente);
            if (Result == false)
            {
                return;
            }

            if (cECliente.Id == 0)
            {
                cNClient.CreateClient(cECliente);
            }
            else
            {
                cNClient.UpdateClient(cECliente);
            }

            LoadData();
            ResetValues();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (numericUpDownId.Value == 0) return;

            if (MessageBox.Show("Are you sure you want to delete this record?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                CEClients cECliente = new CEClients();
                cECliente.Id = (int)numericUpDownId.Value;
                cNClient.DeleteClient(cECliente);
                LoadData();
                ResetValues();
            }
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            cNClient.CheckMySql();
        }

        private void gridData_CellDoubleClick_1(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                numericUpDownId.Value = (int)gridData.Rows[e.RowIndex].Cells["id"].Value;
                txtName.Text = gridData.Rows[e.RowIndex].Cells["name"].Value.ToString();
                txtLastName.Text = gridData.Rows[e.RowIndex].Cells["last_name"].Value.ToString();
                pictureBox1.Load(gridData.Rows[e.RowIndex].Cells["photo_path"].Value.ToString());
            }
            catch (Exception)
            {
                MessageBox.Show("Select a valid row.");
                return;
            }

        }
    }
}