using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Xml.Linq;

namespace TODOManager.Infrastructure.Repository
{
    public class SqliteController
    {
        public static string DBName { get; } = "TodoDb.sqlite";

        /// <summary>
        /// テーブルの作成
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="createQuerys"></param>

        public static void CreateTable(string tableName, string[] createQuerys)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("CREATE TABLE IF NOT EXISTS " + tableName + " (");
            foreach (string query in createQuerys)
            {
                sb.Append(query);
            }
            sb.Append(")");

            ExeQuery(sb.ToString());
        }
        /// <summary>
        /// データのインサート
        /// </summary>
        /// <param name="tableName">保存対象のテーブル名</param>
        /// <param name="fieldDatas">保存するデータのフィールド名</param>
        /// <param name="insertDatas">保存する各データ</param>
        public static void InsertData(string tableName, string[] fieldDatas, string[] insertDatas)
        {
            //以下のフォーマットでクエリを作る
            //INSERT INTO PROJECTS (A, B) VALUES ('A_data', 'B_data')
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO " + tableName + " (");
            foreach(string field in fieldDatas)
            {
                sb.Append(field);
            }
            sb.Append(") ");

            sb.Append("VALUES (");
            foreach(string insert in insertDatas)
            {
                sb.Append(insert);
            }
            sb.Append(")");

            //実行
            ExeQuery(sb.ToString());
        }

        /// <summary>
        /// id指定された要素を削除する
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="id"></param>
        public static void DeleteData(string tableName, string id)
        {
            string query = "DELETE FROM " + tableName + " WHERE ID = " + $"'{id}'";
            ExeQuery(query);
        }

        /// <summary>
        /// クエリの実行
        /// </summary>
        /// <param name="query">実行するクエリ</param>
        public static void ExeQuery(string query)
        {
            //DB自体を作成（存在しない場合のみ）
            using (var conn = new SQLiteConnection("Data Source=" + DBName))
            {
                //DB接続
                conn.Open();

                //コマンドの実行
                using (var commnad = conn.CreateCommand())
                {
                    commnad.CommandText = query;
                    commnad.ExecuteNonQuery();
                }

                conn.Close();
            }
        }

        /// <summary>
        /// 全データの読み出し
        /// </summary>
        /// <returns></returns>
        public static List<List<string>> ReadAllData(string tableName)
        {
            // 検索条件
            var query = "SELECT * FROM " + tableName;

            var result = new List<List<string>>();
            // 接続先を指定
            using (var conn = new SQLiteConnection("Data Source=" + DBName))
            using (var command = conn.CreateCommand())
            {
                // 接続
                conn.Open();

                // コマンドの実行処理
                command.CommandText = query;
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        List<string> data = new List<string>();
                        foreach(int index in Enumerable.Range(0, reader.FieldCount))
                        {
                            data.Add(reader.GetValue(index).ToString());
                        }
                        result.Add(data);
                    }
                }
            }
            return result;
        }
    }
}
