using MahApps.Metro.Converters;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TODOManager.Domain.DomainModel;
using TODOManager.Helpers;

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
        public string deadLine { get; set; }
        /// <summary>
        /// 優先度
        /// </summary>
        public Priority priority { get; set; }
        /// <summary>
        /// 詳細
        /// </summary>
        public Detail detail { get; set; }
        /// <summary>
        /// 子アイテムの配列
        /// </summary>
        public List<TodoItemChildVM> childItems { get; set; }

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
            this.projectName = ProjectHelper.GetProjNameByID(projects, todoItem.projectID);
            this.projectID = todoItem.projectID;
            this.useDeadLine = todoItem.useDeadLine;
            this.deadLine = todoItem.deadLine.ToShortDateString();
            this.priority = todoItem.priority;

            //detailを解析して親子関係を構築する
            //this.detail = todoItem.detail;
            DetailParser detailParser = new DetailParser(todoItem.detail.detail);
            ParseRootData root = detailParser.Execute();

            this.detail = new Detail(root.detail);
            this.childItems = GenerateChildren(root.childs);
        }

        /// <summary>
        /// 解析済みの子要素を入力しVMに反映させて返却する
        /// </summary>
        /// <param name="ParseChilds"></param>
        /// <returns></returns>
        public List<TodoItemChildVM> GenerateChildren(List<ParseChildData> ParseChilds)
        {
            List<TodoItemChildVM> outData = new List<TodoItemChildVM>();
            foreach(ParseChildData child in ParseChilds)
            {
                outData.Add(new TodoItemChildVM(child, this));
            }

            return outData;
        }
    }

    public class TodoItemChildVM
    {
        /// <summary>
        /// Todoのアイテム名
        /// </summary>
        public string itemName { get; set; }
        /// <summary>
        /// 詳細
        /// </summary>
        public Detail detail { get; set; }
        /// <summary>
        /// 対応する親detailのテキスト行数
        /// </summary>
        public int row { get; set; }
        /// <summary>
        /// タスクのステータス
        /// </summary>
        public Status status { get; set; }
        /// <summary>
        /// 親アイテム
        /// </summary>
        public TodoItemVM pearentItem { get; set; }
        /// <summary>
        /// 子アイテムの配列
        /// </summary>
        public List<TodoItemChildVM> childItems { get; set; } = new List<TodoItemChildVM>();

        public TodoItemChildVM(ParseChildData parseChild, TodoItemVM pearent)
        {
            this.itemName = parseChild.titleData.data;
            this.detail = new Detail(parseChild.detail);
            this.row = parseChild.titleData.index;
            this.status = parseChild.isActive ? Status.ACTIVE : Status.DONE;
            this.pearentItem = pearent;

            //子要素が存在するなら再帰的に生成
            foreach(ParseChildData child in parseChild.childs)
            {
                this.childItems.Add(new TodoItemChildVM(child, this.pearentItem));
            }
        }
    }

    public enum Status
    {
        ACTIVE,
        DONE
    }
}
