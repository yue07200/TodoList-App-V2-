using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoListApp
{
    public class TodoList
    {
        private BindingList<TodoItem> items;

        public TodoList()
        {
            items = new BindingList<TodoItem>();
        }

        public void AddItem(TodoItem item)
        {
            items.Add(item);
        }

        public void RemoveItem(TodoItem item)
        {
            items.Remove(item);
        }

        public BindingList<TodoItem> GetItems()
        {
            return items;
        }

        public void SaveToFile(string filePath)
        {
            string json = JsonConvert.SerializeObject(items);
            File.WriteAllText(filePath, json);
        }

        public void LoadFromFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                items = JsonConvert.DeserializeObject<BindingList<TodoItem>>(json);
            }
        }
    }

}
