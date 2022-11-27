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

        public static void CreateTable(string tableName, string[] createQuerys)
        {
            //DB自体を作成（存在しない場合のみ）
            using (var conn = new SQLiteConnection("Data Source=" + DBName))
            {
                //DB接続
                conn.Open();

                //コマンドの実行
                using(var commnad = conn.CreateCommand())
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append("CREATE TABLE IF NOT EXISTS " + tableName + " (");
                    foreach(string query in createQuerys)
                    {
                        sb.Append(query);
                    }
                    sb.Append(")");


                    commnad.CommandText = sb.ToString();
                    commnad.ExecuteNonQuery();
                }

                conn.Close();
            }
        }
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
        public static List<string> SerachRecordData()
        {
            // 検索条件
            var query = "SELECT * FROM PROJECTS";

            var result = new List<string>();
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
                        var array = new string[]
                        {
                            reader.GetValue(0).ToString(),
                            reader.GetValue(1).ToString()
                        };
                        result.AddRange(array);
                    }
                }
            }

            return result;
        }
    }
}
