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

        /// <summary>
        /// 親アイテムのID
        /// </summary>
        public TodoItemID pearentItemID { get; set; }

        /// <summary>
        /// 子アイテムのID配列
        /// </summary>
        public List<TodoItemID> childItemIDs { get; set; }

        public TodoItem(string itemName, TodoItemID id,　ProjectID projectID, DateTime deadLine, Priority priority, Detail detail, TodoItemID pearentItemID, List<TodoItemID> childItemIDs)
        {
            this.itemName = itemName;
            this.id = id;
            this.projectID = projectID;
            this.deadLine = deadLine;
            this.priority = priority;
            this.detail = detail;
            this.pearentItemID = pearentItemID;
            this.childItemIDs = childItemIDs;
        }
    }

    /// <summary>
    /// 優先度enum
    /// </summary>
    public enum Priority
    {
        HIGH,
        MEDIUM,
        LOW
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
