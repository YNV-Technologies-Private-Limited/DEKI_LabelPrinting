using DEKI_LabelPrinting.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DEKI_LabelPrinting
{
    public partial class FormSearchOrderNo : Form
    {
        public string OrderNo { get; set; }
        public List<string> ListOrderNOs { get; set; }
        List<StringWrapper> ERPOrders = new List<StringWrapper>();
        public FormSearchOrderNo()
        {
            InitializeComponent();
            btnSearch.Visible = false;
            SetupDataGridView();
        }
        private void SetupDataGridView()
        {
            // Configure DataGridView appearance and behavior
            dgvWeight.AutoGenerateColumns = false;
            dgvWeight.AllowUserToAddRows = false;
            dgvWeight.AllowUserToDeleteRows = false;
            dgvWeight.ReadOnly = true;
            dgvWeight.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvWeight.MultiSelect = false;

            dgvWeight.CellClick += dgvWeight_CellClick;
            dgvWeight.CellDoubleClick += dgvWeight_CellDoubleClick;
            dgvWeight.KeyDown += dgvWeight_KeyDown;
            // Add column for the string values
            DataGridViewTextBoxColumn textColumn = new DataGridViewTextBoxColumn();
            textColumn.HeaderText = "Production Orders";
            textColumn.DataPropertyName = "Value"; // This will bind to the string value
            textColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvWeight.Columns.Add(textColumn);
        }

        private void BindDataToList()
        {
            // Method 1: Using DataSource with custom object
            var bindingList = new List<StringWrapper>();
            foreach (var item in ListOrderNOs)
            {
                bindingList.Add(new StringWrapper(item));
            }
            ERPOrders = bindingList as List<StringWrapper>;
            dgvWeight.DataSource = bindingList;

            // Alternative Method 2: Direct binding (simpler but less flexible)
            // dataGridView1.DataSource = stringList.Select(s => new { Value = s }).ToList();
        }
        private void FormSearchOrderNo_Load(object sender, EventArgs e)
        {
            BindDataToList();
        }

        private void dgvWeight_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                ShowSelectedItemDetails();
            }
        }

        private void dgvWeight_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                PerformDoubleClickAction();
            }
        }

        private void dgvWeight_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && dgvWeight.SelectedRows.Count > 0)
            {
                PerformDoubleClickAction();
                e.Handled = true;
            }
        }

        private void ShowSelectedItemDetails()
        {
            if (dgvWeight.SelectedRows.Count > 0)
            {
                var selectedProducts = dgvWeight.SelectedRows
                    .Cast<DataGridViewRow>()
                    .Select(row => (StringWrapper)row.DataBoundItem)
                    .ToList();

                if (selectedProducts.Count == 1)
                {
                    var product = selectedProducts[0];
                    OrderNo = product.Value;
                    //txtDetails.Text = $"ID: {product.Id}\nName: {product.Name}\nPrice: {product.Price:C}";
                }
            }
        }

        private void PerformDoubleClickAction()
        {
            if (dgvWeight.SelectedRows.Count == 1)
            {
                var selectedProduct = (StringWrapper)dgvWeight.SelectedRows[0].DataBoundItem;
                //MessageBox.Show($"Double-click action performed on:\n{selectedProduct.Value}",
                //              "Action Triggered", MessageBoxButtons.OK, MessageBoxIcon.Information);

                OrderNo = selectedProduct.Value;
                this.DialogResult = DialogResult.OK;
            }
        }

        private class StringWrapper
        {
            public string Value { get; set; }

            public StringWrapper(string value)
            {
                Value = value;
            }
        }

        private void txtProductionOrderNoSearch_TextChanged(object sender, EventArgs e)
        {
            if (txtProductionOrderNoSearch.Text.Length > 2)
            {
                List<StringWrapper> Orders = dgvWeight.DataSource as List<StringWrapper>;
                var searchOrders = Orders.Where(order => order.Value.Contains(txtProductionOrderNoSearch.Text)).ToList();
                dgvWeight.DataSource = searchOrders;
                //var searchOrders= Orders.Select(order=> order.Value.Contains(txtProductionOrderNoSearch.Text)).ToList();

            }
            else
            {
                dgvWeight.DataSource = ERPOrders;
            }
        }
    }
}
