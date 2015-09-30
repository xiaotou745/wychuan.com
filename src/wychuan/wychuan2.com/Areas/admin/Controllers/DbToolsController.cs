using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AC.Code.Config;
using AC.Code.DbObjects;
using AC.Code.Helper;
using AC.Extension;
using Newtonsoft.Json;
using wychuan2.com.Areas.admin.Models;

namespace wychuan2.com.Areas.admin.Controllers
{
    public class DbToolsController : BaseController
    {
        #region DbSettings

        private DbSettingsBuilder dbSettings;

        /// <summary>
        /// 数据库工厂对象
        /// </summary>
        protected DbSettingsBuilder DbSetting
        {
            get
            {
                if (dbSettings == null)
                {
                    string filePath = Path.Combine(Server.MapPath("~"), @"Content\Data\dbservers.xml");
                    dbSettings = DbSettingsBuilder.Create(filePath);
                }
                return dbSettings;
            }
        }

        #endregion

        #region table view

        // GET: admin/dbtools/tableview
        public ActionResult TableView()
        {
            var model = new TableViewModel();
            //服务器列表
            IList<string> dbServers = DbSetting.GetDbServers();
            model.DbServers = dbServers;
            //默认选中第一个服务器
            model.CurrentDbServer = dbServers[0];
            model.CurrentDbName = string.Empty;//默认没有选中数据库
            //默认显示类型为数据库列表
            model.CurrentViewType = 1;
            model.HtmlTableResult = GetDatabaseList(model.CurrentDbServer);

            //当前选择的服务器,如果初始，则默认选中第一个服务器
            //string dbServerSelected = string.IsNullOrEmpty(Request["dbServer"]) ? dbServers[0] : Request["dbServer"];
            //model.CurrentDbServer = dbServerSelected;

            //string dbNameSelected = string.IsNullOrEmpty(Request["dbName"]) ? string.Empty : Request["dbName"];
            ////当前选择的数据库，如果初始，则空；
            //model.CurrentDbName = dbNameSelected;

            //如果当前没有选择数据库，那就显示数据库列表，否则显示表列表
            //if (string.IsNullOrEmpty(dbNameSelected))
            //{
            //    //ViewData["Type"] = 1;
            //    //ViewData["TypeHtml"] = GetHtmlDbList(dbServerSelected);
            //    model.HtmlTableResult = GetDatabaseList(dbServerSelected);
            //}
            //else
            //{
            //    //ViewData["Type"] = 2;
            //    //ViewData["TypeHtml"] = GetHtmlTableList(dbServerSelected, dbNameSelected);
            //    model.HtmlTableResult = GetDataTableList(dbServerSelected, dbNameSelected);
            //}

            //IDbObject dbObject = DbSetting.CreateDbObject(dbServerSelected);

            //model.DbNameSource = JsonConvert.SerializeObject(dbObject.GetDBList());

            //model.DbTableSource = string.IsNullOrEmpty(dbNameSelected)
            //                                ? string.Empty
            //                                : JsonConvert.SerializeObject(dbObject.GetTables(dbNameSelected));

            return View(model);
        }

        /// <summary>
        /// 更新描述信息
        /// </summary>
        /// <param name="dbServer">服务器</param>
        /// <param name="dbName">数据库</param>
        /// <param name="dbTable">表</param>
        /// <param name="columnName">列名</param>
        /// <param name="desc">描述信息</param>
        /// <returns></returns>
        public bool UpdateDesc(string dbServer, string dbName, string dbTable, string columnName, string desc)
        {
            IDbObject dbObject = DbSetting.CreateDbObject(dbServer);
            bool result = dbObject.UpdateProperty(dbName, dbTable, columnName, desc);
            return result;
        }

        /// <summary>
        /// 刷新页面表格内容(table)
        /// </summary>
        /// <param name="viewType">要刷新的内容类型（1：数据库列表2：表列表3：字段列表）</param>
        /// <param name="dbServer">服务器</param>
        /// <param name="dbName">数据库</param>
        /// <param name="dbTable">表</param>
        /// <returns></returns>
        public ActionResult Refresh(int viewType, string dbServer, string dbName = "", string dbTable = "")
        {
            var model = new TableViewModel();
            model.CurrentDbServer = dbServer;
            if (viewType == 1)//当前显示的是数据库列表
            {
                model.CurrentViewType = 1;
                model.HtmlTableResult = GetDatabaseList(dbServer);
            }
            else if (viewType == 2)//当前显示的是表列表
            {
                model.CurrentViewType = 2;
                model.CurrentDbName = dbName;
                model.HtmlTableResult = GetDataTableList(dbServer, dbName);
            }
            else if (viewType == 3)
            {
                model.CurrentViewType = 3;
                model.CurrentDbName = dbName;
                model.CurrentTableName = dbTable;
                model.HtmlTableResult = GetDataTableDetails(dbServer, dbName, dbTable);
            }
            return View("_DatabaseList", model);
        }

        /// <summary>
        /// 获取表列表
        /// </summary>
        /// <param name="dbServer">服务器</param>
        /// <param name="dbName">数据库</param>
        /// <returns></returns>
        private CommonTableInfo GetDataTableList(string dbServer, string dbName)
        {
            IDbObject dbObject = DbSetting.CreateDbObject(dbServer);
            List<TableInfo> lstTables = dbObject.GetTablesInfo(dbName);
            var commonTableInfo = new CommonTableInfo
            {
                UseSection = false,
                UseStripped = true,
                ColumnNames = new ColumnNames{
                    "TableName",
                    "Owner",
                    "CreateTime",
                    "Desc",
                    "Operation",
                },
            };
            foreach (var tableInfo in lstTables)
            {
                var tableRow = new TableRow();
                tableRow.AddItem(new TableRowItem
                {
                    Name = "tableName",
                    Data = tableInfo.TabName,
                    Class = "J_tableName",
                    AsLink = true.ToString(),
                    LinkHref = "javascript:;",
                });
                tableRow.AddItem(string.Empty, tableInfo.TabUser);
                tableRow.AddItem(string.Empty, tableInfo.TabDate);
                tableRow.AddItem(string.Empty, tableInfo.TabDesc);
                tableRow.AddItem(new TableRowItem
                {
                    Name = string.Empty,
                    Data = "编辑",
                    Class = "J_editTableDesc",
                    AsLink = true.ToString(),
                    LinkHref = "#modalDescEdit",
                    LinkProperty = "data-toggle='modal'",
                    Properties = "data-table={0} data-desc={1}".format(tableInfo.TabName, tableInfo.TabDesc),
                });
                commonTableInfo.AddRow(tableRow);
            }
            return commonTableInfo;
        }

        /// <summary>
        /// 获取数据库列表
        /// </summary>
        /// <param name="dbServer">服务器</param>
        /// <returns></returns>
        private CommonTableInfo GetDatabaseList(string dbServer)
        {
            IDbObject dbObject = DbSetting.CreateDbObject(dbServer);
            List<string> lstDbNames = dbObject.GetDBList();
            var commonTableInfo = new CommonTableInfo
            {
                UseSection = false,
                UseStripped = true,
                ColumnNames = new ColumnNames{
                    "DbName",
                },
            };
            foreach (var dbName in lstDbNames)
            {
                var tableRow = new TableRow();
                var item = new TableRowItem
                {
                    Name = "dbname",
                    Data = dbName,
                    AsLink = true.ToString(),
                    LinkHref = "javascript:;"
                };
                tableRow.AddItem(item);

                commonTableInfo.AddRow(tableRow);
            }
            return commonTableInfo;
        }

        /// <summary>
        /// 获取表的详细信息（所有列名、类型及描述等）
        /// </summary>
        /// <param name="dbServer">服务器</param>
        /// <param name="dbName">数据库</param>
        /// <param name="dbTable">表</param>
        /// <returns></returns>
        private CommonTableInfo GetDataTableDetails(string dbServer, string dbName, string dbTable)
        {
            IDbObject dbObject = DbSetting.CreateDbObject(dbServer);
            List<ColumnInfo> lstColumnInfo = dbObject.GetColumnInfoList(dbName, dbTable);
            var commonTableInfo = new CommonTableInfo
            {
                UseSection = false,
                UseStripped = true,
                ColumnNames = new ColumnNames
                                                            {
                                                                "No",
                                                                "列名",
                                                                "类型",
                                                                "长度",
                                                                "精度",
                                                                "Scale",
                                                                "自增",
                                                                "主键",
                                                                "Null",
                                                                "默认值",
                                                                "描述",
                                                                "Oper",
                                                            }
            };
            foreach (ColumnInfo columnInfo in lstColumnInfo)
            {
                var tableRow = new TableRow();
                tableRow.AddItem(string.Empty, columnInfo.Colorder);
                tableRow.AddItem(string.Empty, columnInfo.ColumnName);
                tableRow.AddItem(string.Empty, columnInfo.TypeName);
                tableRow.AddItem(string.Empty, columnInfo.Length);
                tableRow.AddItem(string.Empty, columnInfo.Preci);
                tableRow.AddItem(string.Empty, columnInfo.Scale);
                tableRow.AddItem(string.Empty, columnInfo.IsIdentity ? "√" : string.Empty);
                tableRow.AddItem(string.Empty, columnInfo.IsPK ? "√" : string.Empty);
                tableRow.AddItem(string.Empty, columnInfo.cisNull ? "√" : string.Empty);
                tableRow.AddItem(string.Empty, columnInfo.DefaultVal);
                tableRow.AddItem(string.Empty, columnInfo.DeText);
                tableRow.AddItem(new TableRowItem
                {
                    Name = string.Empty,
                    Data = "编辑",
                    Class = "J_editColumnDesc",
                    AsLink = true.ToString(),
                    LinkHref = "#modalDescEdit",
                    LinkProperty = "data-toggle='modal'",
                    Properties = "data-col={0} data-desc={1}".format(columnInfo.ColumnName,columnInfo.DeText),
                });
                commonTableInfo.AddRow(tableRow);
            }
            return commonTableInfo;
        }
        #endregion

        #region CodeGenerate

        public ActionResult CodeGenerate()
        {
            var model = new TableViewModel();
            //服务器列表
            IList<string> dbServers = DbSetting.GetDbServers();
            model.DbServers = dbServers;

            //当前选择的服务器,如果初始，则默认选中第一个服务器
            string dbServerSelected = string.IsNullOrEmpty(Request["dbServer"]) ? dbServers[0] : Request["dbServer"];
            model.CurrentDbServer = dbServerSelected;

            string dbNameSelected = string.IsNullOrEmpty(Request["dbName"]) ? string.Empty : Request["dbName"];
            //当前选择的数据库，如果初始，则空；
            model.CurrentDbName = dbNameSelected;

            //如果当前没有选择数据库，那就显示数据库列表，否则显示表列表
            if (string.IsNullOrEmpty(dbNameSelected))
            {
                //ViewData["Type"] = 1;
                //ViewData["TypeHtml"] = GetHtmlDbList(dbServerSelected);
            }
            else
            {
                //ViewData["Type"] = 2;
                //ViewData["TypeHtml"] = GetHtmlTableList(dbServerSelected, dbNameSelected);
            }

            IDbObject dbObject = DbSetting.CreateDbObject(dbServerSelected);

            model.DbNameSource = JsonConvert.SerializeObject(dbObject.GetDBList());

            model.DbTableSource = string.IsNullOrEmpty(dbNameSelected)
                                            ? string.Empty
                                            : JsonConvert.SerializeObject(dbObject.GetTables(dbNameSelected));
            return View();
        }
        #endregion
    }
}