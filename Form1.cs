using System.Windows.Forms;

namespace TodoListApp
{
    public partial class Form1 : Form
    {
        private TodoList todoList;
        private string filePath = "todolist.json";

        public Form1()
        {
            InitializeComponent();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (listBox1.SelectedIndex != -1)
            //{
            //    string itemName = listBox1.SelectedItem.ToString();
            //    MessageBox.Show(itemName + "�Q��F!");
            //}
            if (listBox1.SelectedIndex >= 0)
            {
                TodoItem item = (TodoItem)listBox1.SelectedItem;
                //
                tbTitle.Text = item.Title;
                tbDesc.Text = item.Description;
                tbCreatedDate.Text=item.CreatedDate.ToString();
                dtPickerDue.Value = item.DueDate;
                //
                rbStatus0.Checked = true;
                rbStatus1.Checked=item.Status==1;
                rbStatus2.Checked=item.Status==2;

                //������s���A
                btnAdd.Enabled=false;
                btnModify.Enabled=true;
                btnDelete.Enabled=true;
                btnCancel.Enabled=true;
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            todoList = new TodoList();
            todoList.LoadFromFile(filePath);
            listBox1.DataSource = todoList.GetItems();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            
            //�ˬd��J���
            if (string.IsNullOrEmpty(tbTitle.Text))
            {
                MessageBox.Show("�п�J���D!");
            }
            if (string.IsNullOrEmpty(tbDesc.Text))
            {
                MessageBox.Show("�п�J���e����!");
                return;
            }
            //�s�W�ݿ�M�涵��
            string title = tbTitle.Text;
            string description = tbDesc.Text;
            DateTime now = DateTime.Now;
            DateTime dueDate = dtPickerDue.Value;
            //
            TodoItem newItem = new TodoItem
            {
                Title = title,
                Description = description,
                CreatedDate = now,
                DueDate = dueDate,
                Status = rbStatus1.Checked ? 1 : (rbStatus2.Checked ? 2 : 0)
            };
            //��s�e��
            //[1].�]�wDataSource�ݩʬ�todoList.GetItems(),�i�H��ListBox
            todoList.AddItem(newItem);

            //�M�ſ�J��
            tbTitle.Text = "";
            tbDesc.Text = "";
            tbCreatedDate.Text = "";
            dtPickerDue.Value = DateTime.Today;
            rbStatus0.Checked = true;
            //
            listBox1.ClearSelected();
            //��ܰT��
            MessageBox.Show("�s�W�N��M�涵�ئ��\!");
        }

        private void btnModify_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0)
            {
                TodoItem item = (TodoItem)listBox1.SelectedItem;
                //
                item.Title = tbTitle.Text;
                item.Description = tbDesc.Text;
                item.DueDate = dtPickerDue.Value;
                item.Status = rbStatus1.Checked ? 1 : (rbStatus2.Checked ? 2 : 0);
                //
                //Refresh!
                listBox1.DataSource = null;
                listBox1.DataSource = todoList.GetItems();

                //
                //��ܰT��
                MessageBox.Show("�ק�N��M�涵�ئ��\!");
                //
                resetUI();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            TodoItem item = (TodoItem)listBox1.SelectedItem;
            //
            DialogResult confirm = MessageBox.Show("�T�w�n�R����?", "�R���N��M�涵��", MessageBoxButtons.YesNo);

            if (confirm == DialogResult.Yes)
            {
                todoList.RemoveItem(item);
                //
                //��ܰT��
                MessageBox.Show("�R���N��M�涵�ئ��\! ");
                //
                resetUI();
            }
        }
        private void resetUI()
        {
            //�M�ſ�J��
            tbTitle.Text = "";
            tbDesc.Text = "";
            tbCreatedDate.Text = "";
            dtPickerDue.Value = DateTime.Today;
            rbStatus0.Checked = true;
            //
            listBox1.ClearSelected();
            //������s�����A
            btnAdd.Enabled = true;
            btnModify.Enabled = false;
            btnDelete.Enabled = false;
            btnCancel.Enabled = false;
        }
    }
}
