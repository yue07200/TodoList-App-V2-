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
            //    MessageBox.Show(itemName + "被選了!");
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

                //控制按鈕狀態
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
            
            //檢查輸入資料
            if (string.IsNullOrEmpty(tbTitle.Text))
            {
                MessageBox.Show("請輸入標題!");
            }
            if (string.IsNullOrEmpty(tbDesc.Text))
            {
                MessageBox.Show("請輸入內容說明!");
                return;
            }
            //新增待辦清單項目
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
            //更新畫面
            //[1].設定DataSource屬性為todoList.GetItems(),可以讓ListBox
            todoList.AddItem(newItem);

            //清空輸入框
            tbTitle.Text = "";
            tbDesc.Text = "";
            tbCreatedDate.Text = "";
            dtPickerDue.Value = DateTime.Today;
            rbStatus0.Checked = true;
            //
            listBox1.ClearSelected();
            //顯示訊息
            MessageBox.Show("新增代辦清單項目成功!");
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
                //顯示訊息
                MessageBox.Show("修改代辦清單項目成功!");
                //
                resetUI();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            TodoItem item = (TodoItem)listBox1.SelectedItem;
            //
            DialogResult confirm = MessageBox.Show("確定要刪除嗎?", "刪除代辦清單項目", MessageBoxButtons.YesNo);

            if (confirm == DialogResult.Yes)
            {
                todoList.RemoveItem(item);
                //
                //顯示訊息
                MessageBox.Show("刪除代辦清單項目成功! ");
                //
                resetUI();
            }
        }
        private void resetUI()
        {
            //清空輸入框
            tbTitle.Text = "";
            tbDesc.Text = "";
            tbCreatedDate.Text = "";
            dtPickerDue.Value = DateTime.Today;
            rbStatus0.Checked = true;
            //
            listBox1.ClearSelected();
            //控制按鈕的狀態
            btnAdd.Enabled = true;
            btnModify.Enabled = false;
            btnDelete.Enabled = false;
            btnCancel.Enabled = false;
        }
    }
}
