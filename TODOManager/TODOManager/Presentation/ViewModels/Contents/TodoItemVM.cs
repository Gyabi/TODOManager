using MahApps.Metro.Converters;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODOManager.Domain.DomainModel;

namespace TODOManager.Presentation.ViewModels.Contents
{
    /// <summary>
    /// VM内で利用するドメインを加工したクラス
    /// </summary>
    public class TodoItemVM
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
        /// Project名
        /// </summary>
        public string projectName { get; set; }
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
        /// <summary>
        /// 親アイテム
        /// </summary>
        public TodoItem pearentItem { get; set; }
        /// <summary>
        /// 子アイテムの配列
        /// </summary>
        public List<TodoItem> childItems { get; set; }

        /// <summary>
        /// TodoItemからコンストラクタを起動する
        /// </summary>
        /// <param name="todoitem">参照するTodoItem</param>
        /// <param name="projects">プロジェクト一覧</param>
        public TodoItemVM(TodoItem todoItem, ObservableCollection<Project> projects)
        {
            //入力されたtodoItemから各メンバを設定する
            this.itemName = todoItem.itemName;
            this.id = todoItem.id;

            this.projectID = todoItem.projectID;

            //detailを解析して親子関係を構築する
        }

        //public string GetProjectNameById()
    }
}
