using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoListApp
{
    public class TodoItem
    {
        // public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime DueDate { get; set; }
        public int Status { get; set; }
        //
        public override string ToString()
        {
            // 回傳待辦清單項目的標題
            return Title;
            // 回傳待辦清單項目的標題、說明、預計完成日期
            // return $"標題: {Title}，說明: {Description}，預計完成日期: {DueDate}";
        }
    }
}
