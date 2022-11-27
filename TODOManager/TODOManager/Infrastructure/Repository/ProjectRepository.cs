using MahApps.Metro.Converters;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using TODOManager.Domain.DomainModel;
using TODOManager.Domain.DomainService.Repository;

namespace TODOManager.Infrastructure.Repository
{
    public class ProjectRepository : IProjectRepository
    {
        public List<Project> ReadProjects()
        {
            //テーブル作成
            this.CreateTable();
            //データ取得
            List<List<string>> datas = SqliteController.ReadAllData("PROJECTS");
            //データ変換して返却
            List<Project> outdata = new List<Project>();
            foreach(List<string> data in datas)
            {
                outdata.Add(new Project(data[1], new ProjectID(data[0])));
            }

            return outdata;
        }

        public void InsertData(string id, string name)
        {
            string[] fieldDatas = new string[] { "ID", ", NAME" };
            string[] insertDatas = new string[] { $"'{id}'", $", '{name}'" };
            SqliteController.InsertData("PROJECTS", fieldDatas, insertDatas);
        }

        public void DeleteData(string id)
        {
            SqliteController.DeleteData("PROJECTS", id);
        }


        public void DeleteAllData()
        {
            var query = "DELETE FROM PROJECTS";
            SqliteController.ExeQuery(query);
        }
        public void CreateTable()
        {
            string[] queryStrings = new string[]
            {
                " ID TEXT NOT NULL",
                " , NAME TEXT",
                " , primary key (ID)"
            };
            //テーブル作成
            SqliteController.CreateTable("PROJECTS", queryStrings);
        }
    }
}
