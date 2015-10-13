using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AC.Code.Builder;
using AC.Code.Config;
using AC.Code.DbObjects;
using AC.Code.Helper;
using AC.Code.IBuilder;
using AC.Extension;
using Newtonsoft.Json;
using wychuan2.com.Areas.admin.Models;
using wychuan2.com.Areas.admin.Models.DbTools;
using wychuan2.com.Areas.admin.Models.HtmlTable;

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
            model.TableHasEditOper = true.ToString();
            model.TableHasGenerateCodeOper = false.ToString();
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
        public ActionResult Refresh(int viewType, string dbServer, string dbName = "", string dbTable = "", bool hasEdit = true, bool hasGenerateCode = false)
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
                model.HtmlTableResult = GetDataTableList(dbServer, dbName, hasEdit, hasGenerateCode);
            }
            else if (viewType == 3)
            {
                model.CurrentViewType = 3;
                model.CurrentDbName = dbName;
                model.CurrentTableName = dbTable;
                model.HtmlTableResult = GetDataTableDetails(dbServer, dbName, dbTable, hasEdit);
            }
            return View("_DatabaseList", model);
        }

        /// <summary>
        /// 获取表列表
        /// </summary>
        /// <param name="dbServer">服务器</param>
        /// <param name="dbName">数据库</param>
        /// <param name="hasEdit"></param>
        /// <param name="hasGenerateCode"></param>
        /// <returns></returns>
        private HtmlTableInfo GetDataTableList(string dbServer, string dbName, bool hasEdit=true,bool hasGenerateCode=false)
        {
            IDbObject dbObject = DbSetting.CreateDbObject(dbServer);
            List<TableInfo> lstTables = dbObject.GetTablesInfo(dbName);
            var htmlTable = new HtmlTableInfo
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
                HtmlTableTrItem tr = htmlTable.NewRow();
                tr.NewLinkTd("tableName", "javascript:;", "J_tableName", tableInfo.TabName);
                tr.NewTd(string.Empty, tableInfo.TabUser);
                tr.NewTd(string.Empty, tableInfo.TabDate);
                tr.NewTd(string.Empty, tableInfo.TabDesc);

                var linkTd = new HtmlTableTdLinkItem()
                {
                    Class = "J_editTableDesc",
                    Properties = "data-table={0} data-desc={1}".format(tableInfo.TabName, tableInfo.TabDesc),
                };
                if (hasEdit)
                {
                    LinkItem editLink = new LinkItem();
                    editLink.Text = "编辑描述";
                    editLink.Href = "#modalDescEdit";
                    editLink.Properties = "data-toggle='modal'";
                    linkTd.LinkItems.Add(editLink);
                }
                if (hasGenerateCode)
                {
                    LinkItem codeLink = new LinkItem();
                    codeLink.Text = "生成代码";
                    codeLink.Href = "#generateCodeSettings";
                    codeLink.Properties = "data-toggle='modal'";
                    linkTd.LinkItems.Add(codeLink);
                }
                tr.AddTd(linkTd);
            }
            return htmlTable;
        }

        /// <summary>
        /// 获取数据库列表
        /// </summary>
        /// <param name="dbServer">服务器</param>
        /// <returns></returns>
        private HtmlTableInfo GetDatabaseList(string dbServer)
        {
            IDbObject dbObject = DbSetting.CreateDbObject(dbServer);
            List<string> lstDbNames = dbObject.GetDBList();
            var htmlTable = new HtmlTableInfo
            {
                UseSection = false,
                UseStripped = true,
                ColumnNames = new ColumnNames{
                    "DbName",
                },
            };
            foreach (var dbName in lstDbNames)
            {
                HtmlTableTrItem tableTr = htmlTable.NewRow();
                tableTr.NewLinkTd("dbname", "javascript:;", dbName);
            }
            return htmlTable;
        }

        /// <summary>
        /// 获取表的详细信息（所有列名、类型及描述等）
        /// </summary>
        /// <param name="dbServer">服务器</param>
        /// <param name="dbName">数据库</param>
        /// <param name="dbTable">表</param>
        /// <returns></returns>
        private HtmlTableInfo GetDataTableDetails(string dbServer, string dbName, string dbTable, bool hasEdit=true)
        {
            IDbObject dbObject = DbSetting.CreateDbObject(dbServer);
            List<ColumnInfo> lstColumnInfo = dbObject.GetColumnInfoList(dbName, dbTable);
            var htmlTable = new HtmlTableInfo
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
                HtmlTableTrItem tr = htmlTable.NewRow();
                tr.AddTd(string.Empty, columnInfo.Colorder);
                tr.AddTd(string.Empty, columnInfo.ColumnName);
                tr.AddTd(string.Empty, columnInfo.TypeName);
                tr.AddTd(string.Empty, columnInfo.Length);
                tr.AddTd(string.Empty, columnInfo.Preci);
                tr.AddTd(string.Empty, columnInfo.Scale);
                tr.AddTd(string.Empty, columnInfo.IsIdentity ? "√" : string.Empty);
                tr.AddTd(string.Empty, columnInfo.IsPK ? "√" : string.Empty);
                tr.AddTd(string.Empty, columnInfo.cisNull ? "√" : string.Empty);
                tr.AddTd(string.Empty, columnInfo.DefaultVal);
                tr.AddTd(string.Empty, columnInfo.DeText);

                if (hasEdit)
                {
                    LinkItem editLink = new LinkItem();
                    editLink.Text = "编辑";
                    editLink.Href = "#modalDescEdit";
                    editLink.Properties = "data-toggle='modal'";

                    var linkTd = new HtmlTableTdLinkItem()
                    {
                        Class = "J_editColumnDesc",
                        Properties = "data-col={0} data-desc={1}".format(columnInfo.ColumnName, columnInfo.DeText),
                    };
                    linkTd.LinkItems.Add(editLink);

                    tr.Add(linkTd);
                }
            }
            return htmlTable;
        }
        #endregion

        #region CodeGenerate

        public ActionResult CodeGenerate()
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
            model.TableHasEditOper = true.ToString();
            model.TableHasGenerateCodeOper = true.ToString();

            //model.CodeResult = GenerateCode("127.0.0.1", "WYC", "Company", "Company", "1", "1", "1", 1);

            return View(model);
        }

        public ActionResult Generate(string dbServer, string dbName, string tableName, string modelName,
                                         string callStyle, string daoStyle, string codeLayer, int codeType)
        {
            var model = GenerateCode(dbServer, dbName, tableName, modelName, callStyle, daoStyle, codeLayer, codeType);
            
            return View("_CodeResult", model);
        }

        private CodeResult GenerateCode(string dbServer, string dbName, string tableName, string modelName,
            string callStyle, string daoStyle, string codeLayer, int codeType)
        {
            var model = new CodeResult();
            model.Language = (CodeLanguage)Enum.Parse(typeof(CodeLanguage), codeType.ToString());
            //model.Codes.Add(new CodeFileItem { Id = "tabService", TabText = "ServiceCode", Code = "Service" });
            //model.Codes.Add(new CodeFileItem { Id = "tabServiceImpl", TabText = "ServiceImplCode", Code = "ServiceImpl" });
            //model.Codes.Add(new CodeFileItem { Id = "tabDao", TabText = "DaoCode", Code = "Dao" });

            var codeGenerateConfig = new CodeGenerateConfig
            {
                CallStyleHashCode = callStyle,
                CodeLayerHashCode = codeLayer,
                DaoStyleHashCode = daoStyle,
                ModelName = modelName,
                CodeType = (CodeType)Enum.Parse(typeof(CodeType), codeType.ToString())
            };

            IDbObject dbObj = DbSetting.CreateDbObject(dbServer);

            //获取数据库表的详细列信息
            List<ColumnInfo> lstColumns = dbObj.GetColumnInfoList(dbName, tableName);
            //获取主键列信息
            List<ColumnInfo> lstKeys = (from columnInfo in lstColumns
                                        where columnInfo.IsPK
                                        select columnInfo).ToList();

            //Service  代码生成
            IBuilderService serviceBuilder = ServiceBuilder.Create()
                .SetGenerateConfig(codeGenerateConfig)
                .SetKeys(lstKeys)
                .GetServiceBuilder();
            string serviceCode = serviceBuilder.GetServiceCode();
            model.Codes.Add(new CodeFileItem { Id = "tabService", TabText = "ServiceCode", Code = serviceCode });

            //Dao 代码生成
            IBuilderDao daoBuilder = DaoBuilder.Create()
                .SetDbObj(dbObj)
                .SetDbName(dbName)
                .SetTableName(tableName)
                .SetColFields(lstColumns)
                .SetKeys(lstKeys)
                .SetGenerateConfig(codeGenerateConfig)
                .GetDaoBuilder();
            string daoCode = daoBuilder.GetDaoCode();
            model.Codes.Add(new CodeFileItem { Id = "tabDao", TabText = "DaoCode", Code = daoCode });

            //Service DTO 代码生成
            IBuilderDTO serviceDTOBuilder = DTOBuilder.Create(codeGenerateConfig, lstColumns).GetDTOCode();
            string serviceDTOCode = serviceDTOBuilder.GetServiceDTOCode();
            model.Codes.Add(new CodeFileItem { Id = "tabDTO", TabText = "DTOCode", Code = serviceDTOCode });

            //如果是Service五层架构，则继续生成Domain和ServiceImpl层
            if (codeGenerateConfig.CodeLayer == CodeLayer.ServiceLayerWithDomain)
            {
                //生成Domain层代码
                IBuilderDomain builderDomain = new BuilderDomain(lstKeys, codeGenerateConfig);
                string domainCode = builderDomain.GetDomainCode();
                model.Codes.Add(new CodeFileItem { Id = "tabDomain", TabText = "DomainCode", Code = domainCode });

                //生成ServiceImpl层代码
                IBuilderServiceImpl builderServiceImpl = new BuilderServiceImpl(lstKeys, codeGenerateConfig);
                string serviceImplCode = builderServiceImpl.GetServiceImplCode();
                model.Codes.Add(new CodeFileItem { Id = "tabServiceImpl", TabText = "ServiceImplCode", Code = serviceImplCode });
            }
            //如果是Service不带Domain层代码，只需要生成ServiceImpl层代码
            else if (codeGenerateConfig.CodeLayer == CodeLayer.ServiceLayerWithoutDomain)
            {
                IBuilderServiceImpl builderServiceImpl = new BuilderServiceImpl(lstKeys, codeGenerateConfig);
                string serviceImplCode = builderServiceImpl.GetServiceImplCode();
                model.Codes.Add(new CodeFileItem { Id = "tabServiceImpl", TabText = "ServiceImplCode", Code = serviceImplCode });

            }

            return model;
        }
        #endregion

        #region CodeTemplate

        public ActionResult CodeTemplate()
        {
            return View();
        }
        #endregion
    }
}