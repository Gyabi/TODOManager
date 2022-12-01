using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using TODOManager.Domain.DomainModel;
using TODOManager.Domain.DomainService.Repository;
using TODOManager.Helpers;
using TODOManager.Presentation.ViewModels.Contents;

namespace TODOManager.Infrastructure.Repository
{
    public class TodoRepository : ITodoRepository
    {
        /// <summary>
        /// DBから保存済みアイテムを呼び出す
        /// </summary>
        /// <returns></returns>
        public List<TodoItem> ReadTodoItems()
        {
            //テーブル作成
            CreateTable();
            //データ取得
            List<List<string>> todoDatas = SqliteController.ReadAllData("TODOITEMS");
            List<List<string>> todoIndexs = SqliteController.ReadAllData("TODOINDEX");
            //データを整形して返却
            List<TodoItem> tmptodoItems = new List<TodoItem>();

            //todoデータを取得
            foreach(List<string> todoData in todoDatas)
            {
                var todoItem = new TodoItem(todoData[1], new TodoItemID(todoData[0]), new ProjectID(todoData[2]),
                    System.Convert.ToBoolean(todoData[3]), DateTime.Parse(todoData[4]), PriorityHelper.StringToPriority(todoData[5]),
                    new Detail(todoData[6]));

                tmptodoItems.Add(todoItem);
            }

            //indexデータを取得
            //自動で昇順にソートされる辞書型
            SortedDictionary<int, string> indexs = new SortedDictionary<int, string>();
            foreach(List<string> todoIndex in todoIndexs)
            {
                indexs.Add(int.Parse(todoIndex[1]), todoIndex[0]);
            }

            //返却するデータを取得する
            List<TodoItem> todoItems = new List<TodoItem>();
            foreach(string id in indexs.Values)
            {
                foreach(TodoItem item in tmptodoItems)
                {
                    if(item.id.id == id)
                    {
                        todoItems.Add(item);
                    }
                }
            }
            return todoItems;
        }

        /// <summary>
        /// データの追加
        /// </summary>
        /// <param name="todoItem"></param>
        /// <param name="index">登録する表示順</param>
        public void InsertData(TodoItem todoItem, int index)
        {
            string[] fieldDatas = new string[] { "ID", ", ITEMNAME", ", PROJECTID", ", USEDEADLINE", ", DEADLINE", ", PRIORITY", ", DETAIL" };
            string[] insertDatas = new string[] { $"'{todoItem.id.id}'", $", '{todoItem.itemName}'", $", '{todoItem.projectID.id}'", $", '{todoItem.useDeadLine.ToString()}'"
            , $", '{todoItem.deadLine.ToString()}'", $", '{PriorityHelper.PriorityToString(todoItem.priority)}'", $", '{todoItem.detail.detail}'"};
            SqliteController.InsertData("TODOITEMS", fieldDatas, insertDatas);

            fieldDatas = new string[] { "ID", ", POS"};
            insertDatas = new string[] { $"'{todoItem.id.id}'", $", {index}"};
            SqliteController.InsertData("TODOINDEX", fieldDatas, insertDatas);

        }

        /// <summary>
        /// データのアップデート
        /// </summary>
        /// <param name="id"></param>
        /// <param name="todoItem"></param>
        /// <param name="index">新規に登録する表示順</param>
        public void UpdateData(TodoItemID id, TodoItem todoItem, int index)
        {
            string[] fieldDatas = new string[] { "ID", "ITEMNAME", "PROJECTID", "USEDEADLINE", "DEADLINE", "PRIORITY", "DETAIL" };
            string[] insertDatas = new string[] { $"'{todoItem.id.id}'", $"'{todoItem.itemName}'", $"'{todoItem.projectID.id}'", $"'{todoItem.useDeadLine.ToString()}'"
            , $"'{todoItem.deadLine.ToString()}'", $"'{PriorityHelper.PriorityToString(todoItem.priority)}'", $"'{todoItem.detail.detail}'"};

            SqliteController.UpdateData("TODOITEMS", fieldDatas, insertDatas, id.id);

            fieldDatas = new string[] { "ID", "POS" };
            insertDatas = new string[] { $"'{todoItem.id.id}'", $"{index}" };
            SqliteController.UpdateData("TODOINDEX", fieldDatas, insertDatas, id.id);

        }

        /// <summary>
        /// indexデータのアップデート
        /// </summary>
        /// <param name="id"></param>
        /// <param name="todoItem"></param>
        /// <param name="index">新規に登録する表示順</param>
        public void UpdateDataOnlyIndex(TodoItemID id, int index)
        {
            string[] fieldDatas = new string[] { "ID", "POS" };
            string[] insertDatas = new string[] { $"'{id.id}'", $"{index}" };
            SqliteController.UpdateData("TODOINDEX", fieldDatas, insertDatas, id.id);

        }


        public void DeleteData(TodoItemID id)
        {
            SqliteController.DeleteData("TODOITEMS", id.id);
            SqliteController.DeleteData("TODOINDEX", id.id);
        }


        public void DeleteAllmData()
        {
            var query = "DELETE FROM TODOITEMS";
            SqliteController.ExeQuery(query);
            query = "DELETE FROM TODOINDEX";
            SqliteController.ExeQuery(query);
        }

        public void CreateTable()
        {

            string[] queryStrings = new string[]
            {
                " ID TEXT NOT NULL",
                " , ITEMNAME TEXT",
                " , PROJECTID TEXT",
                " , USEDEADLINE TEXT",
                " , DEADLINE TEXT",
                " , PRIORITY TEXT",
                " , DETAIL TEXT",
                " , primary key (ID)"
            };
            //テーブル作成
            SqliteController.CreateTable("TODOITEMS", queryStrings);

            //格納順を管理するテーブル
            string[] queryStrings2 = new string[]
            {
                " ID TEXT NOT NULL",
                " , POS INTEGER",
                " , primary key (ID)"
            };
            //テーブル作成
            SqliteController.CreateTable("TODOINDEX", queryStrings2);
        }
    }
}
