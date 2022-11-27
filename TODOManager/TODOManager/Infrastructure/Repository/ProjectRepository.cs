using MahApps.Metro.Converters;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            InsertData("001", "test1");

            SqliteController.SerachRecordData();

            return new List<Project>() { new Project("project1", new ProjectID("id1")), new Project("project2", new ProjectID("id2")) };
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

        public void InsertData(string id, string name)
        {
            //var query = "DELETE FROM PROJECTS";
            var query = "INSERT INTO PROJECTS (ID, NAME) VALUES ('id1', 'name')";
            //        var query = "INSERT INTO PROJECTS (ID, NAME) VALUES (" +
            //$"{id}, {name})";
            SqliteController.ExeQuery(query);
        }
    }
}
