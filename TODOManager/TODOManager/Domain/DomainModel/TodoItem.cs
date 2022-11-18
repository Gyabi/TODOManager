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
