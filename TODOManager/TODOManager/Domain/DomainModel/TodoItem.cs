using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.RightsManagement;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media.TextFormatting;

namespace TODOManager.Domain.DomainModel
{
    public class TodoItem
    {
        /// <summary>
        /// Todoのアイテム名
        /// </summary>
        public string itemName { get; set; }

        /// <summary>
        /// ID
        /// </summary>
        public TodoItemID id { get; set; }

        /// <summary>
        /// 対応するプロジェクトのID
        /// </summary>
        public ProjectID projectID { get; set; }

        /// <summary>
        /// 期限の使用可否
        /// </summary>
        public bool useDeadLine { get; set; }
        /// <summary>
        /// 期限
        /// </summary>
        public DateTime deadLine { get; set; }

        /// <summary>
        /// 優先度
        /// </summary>
        public Priority priority { get; set; }

        /// <summary>
        /// 詳細
        /// </summary>
        public Detail detail { get; set; }

        public TodoItem(string itemName, TodoItemID id,　ProjectID projectID, bool useDeadLine, DateTime deadLine, Priority priority, Detail detail)
        {
            this.itemName = itemName;
            this.id = id;
            this.projectID = projectID;
            this.useDeadLine = useDeadLine;
            this.deadLine = deadLine;
            this.priority = priority;
            this.detail = detail;
        }

        /// <summary>
        /// 子要素にあたるdetail内のステータス情報を書き換える
        /// </summary>
        /// <param name="item"></param>
        /// <param name=""></param>
        /// <param name="id"></param>
        /// <param name="row"></param>
        /// <returns></returns>
        public static TodoItem ChangeChildStatus(TodoItem item, int row)
        {
            //detailを分割して編集する行を特定
            string rawData = item.detail.detail;
            string[] datas = rawData.Split("\n");

            //☑がついていたら削除
            if (datas[row][datas[row].Length-1] == '☑')
            {
                datas[row] = datas[row].Remove(datas[row].Length - 1);
            }
            //無ければ付与
            else
            {
                datas[row] += "☑";
            }

            //改行で再度結合して戻す
            item.detail.detail = string.Join("\n", datas);
            return item;
        }
    }

    /// <summary>
    /// 優先度enum
    /// </summary>
    public enum Priority
    {
        HIGH,
        MEDIUM,
        LOW,
        NONE
    }

    public class Detail
    {
        public string detail { get; set; }

        public Detail(string detail)
        {
            this.detail = detail;
        }
    }
}
