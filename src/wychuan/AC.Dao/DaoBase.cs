using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AC.Data;
using AC.Data.Core;
using AC.Data.Generic;
using AC.Extension;
using AC.Helper;
using AC.Page;
using AC.Service;
using AC.Service.DTO.Blog;

namespace AC.Dao
{
    public class DaoBase : AbstractDaoBase
    {
        /// <summary>
        /// ETS数据库连接字符串
        /// </summary>
        protected string ConnStringOfAchuan
        {
            get { return GetConnString("ConnStringOfAchuan"); }
        }

        /// <summary>
        /// 将Table数据转化为对象列表
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="table">数据表</param>
        /// <returns></returns>
        protected IList<T> MapRows<T>(DataTable table)
        {
            IList<T> lstT = new List<T>();

            if (!table.HasData())
            {
                return lstT;
            }

            Type type = typeof(T);
            PropertyInfo[] typeProperties = type.GetProperties();
            foreach (DataRow dataRow in table.Rows)
            {
                T t = (T)type.Assembly.CreateInstance(type.FullName);
                lstT.Add(t);

                foreach (DataColumn dataColumn in table.Columns)
                {
                    var properties = typeProperties.Where(p => p.Name.ToLower() == dataColumn.ColumnName.ToLower());
                    if (!properties.Any()) //没找到与字段名相同的属性
                    {
                        continue;
                    }

                    PropertyInfo propertyInfo = properties.First();
                    object obj = (dataRow[dataColumn.ColumnName] == DBNull.Value) ? "" : dataRow[dataColumn.ColumnName];
                    if (!string.IsNullOrEmpty(obj.ToString()))
                    {
                        if (dataColumn.DataType == typeof(DateTime) && propertyInfo.PropertyType == typeof(String))
                        {
                            obj = ((DateTime)dataRow[dataColumn.ColumnName]).ToString();
                        }

                        if (dataColumn.DataType == typeof(String) && propertyInfo.PropertyType == typeof(DateTime))
                        {
                            DateTime temp;
                            obj = DateTime.TryParse(obj.ToString(), out temp);
                        }
                    }
                    //时间类型或者整形,则跳过赋值
                    if (string.IsNullOrEmpty(obj.ToString())
                        && (propertyInfo.PropertyType == typeof(DateTime)
                            || propertyInfo.PropertyType == typeof(int)
                            || propertyInfo.PropertyType == typeof(Int16)
                            || propertyInfo.PropertyType == typeof(Int32)
                            || propertyInfo.PropertyType == typeof(Int64)
                           )
                        )
                    {
                        continue;
                    }
                    try
                    {
                        propertyInfo.SetValue(t, obj, null);
                    }
                    catch (Exception ex)
                    {
                        //throw ex;
                    }
                }
            }

            return lstT;
        }

        /// <summary>
        /// DataTable转为List类
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public static IList<T> ConvertDataTableList<T>(DataTable dataTable)
        {
            if (!dataTable.HasData())
            {
                return new List<T>();
            }

            IList<T> lstT = new List<T>();
            foreach (DataRow dataRow in dataTable.Rows)
            {
                if (dataRow == null)
                {
                    continue;
                }
                try
                {
                    T type = default(T);
                    Type tbType = typeof(T);
                    type = (T)tbType.Assembly.CreateInstance(tbType.FullName);
                    for (int j = 0; j < dataTable.Columns.Count; j++)
                    {
                        var column = dataTable.Columns[j];
                        if (string.IsNullOrWhiteSpace(column.ColumnName))
                        {
                            continue;
                        }
                        try
                        {
                            PropertyInfo pi = tbType.GetProperty(column.ColumnName);
                            if (pi == null || dataRow[j] == null || DBNull.Value == dataRow[j])
                            {
                                continue;
                            }
                            pi.SetValue(type, dataRow[j], null);
                        }
                        catch
                        {
                            continue;
                        }

                    }
                    lstT.Add(type);
                }
                catch
                {
                    continue;
                }
            }
            return lstT;
        }

        public IPagedList<T> QueryPaged<T>(PagedQueryBuilder<T> queryInfo)
        {
            IDbParameters dbParameters = queryInfo.DbParameters;

            object recordResult = DbHelper.ExecuteScalar(ConnStringOfAchuan, queryInfo.GetRecordCountSqlText(), dbParameters);
            if (recordResult == null)
            {
                return new PagedList<T>(new List<T>(), 0, 0);
            }
            int recordCount = ParseHelper.ToInt(recordResult);
            
            int pageCount = recordCount % queryInfo.PageSize == 0
                             ? recordCount / queryInfo.PageSize
                             : recordCount / queryInfo.PageSize + 1;


            var lstResult = dbParameters.Count > 0
                ? DbHelper.QueryWithRowMapper(ConnStringOfAchuan, queryInfo.GetRecordResultSqlText(),
                    dbParameters, queryInfo.RowMapper)
                : DbHelper.QueryWithRowMapper(ConnStringOfAchuan, queryInfo.GetRecordResultSqlText(),
                    queryInfo.RowMapper);
            var result = new PagedList<T>(lstResult, recordCount, pageCount);
            result.PageIndex = queryInfo.PageIndex;
            result.PageSize = queryInfo.PageSize;
            return result;
        }
    }

    public class PagedQueryBuilder<T>
    {
        public Paginator Paginator { get; set; }

        public PagedQueryBuilder<T> SetPaginator(Paginator paginator)
        {
            this.Paginator = paginator;
            return this;
        }
        public IDbParameters DbParameters { get; set; }

        public PagedQueryBuilder<T> SetDbParameters(IDbParameters dbParameters)
        {
            this.DbParameters = dbParameters;
            return this;
        }
        public string OrderByColumn { get; set; }

        public PagedQueryBuilder<T> SetOrderByColumn(string orderBy)
        {
            OrderByColumn = orderBy;
            return this;
        }
        public string ColumnList { get; set; }

        public PagedQueryBuilder<T> SetColumnList(string columnList)
        {
            ColumnList = columnList;
            return this;
        }
        public string TableList { get; set; }

        public PagedQueryBuilder<T> SetTableList(string tableList)
        {
            TableList = tableList;
            return this;
        }
        public string Where { get; set; }

        public PagedQueryBuilder<T> SetWhere(string where)
        {
            Where = where;
            return this;
        }

        public string GroupBy { get; set; }

        public PagedQueryBuilder<T> SetGroupBy(string groupBy)
        {
            this.GroupBy = groupBy;
            return this;
        }

        public IDataTableRowMapper<T> RowMapper { get; set; }

        public PagedQueryBuilder<T> SetRowMapper(IDataTableRowMapper<T> rowMapper)
        {
            this.RowMapper = rowMapper;
            return this;
        }

        public int PageSize
        {
            get
            {

                int pageSize = Paginator.PageSize == 0 ? 10 : Paginator.PageSize;
                return pageSize;
            }
        }

        public int PageIndex
        {
            get
            {
                int pageIndex = Paginator.PageIndex == 0 ? 1 : Paginator.PageIndex;
                return pageIndex;
            }
        }
        
        private PagedQueryBuilder() { }

        public static PagedQueryBuilder<T> Create()
        {
            return new PagedQueryBuilder<T>();
        }

        public string GetRecordCountSqlText()
        {
            if (string.IsNullOrEmpty(GroupBy))
            {
                return @"select count(1) from {0} where {1}".format(TableList, Where);
            }
            return
                @"select isnull(sum(t2.[no]),0) from ( select 1 as [no] from {0} where {1} group by {2}) as t2"
                    .format(TableList, Where, GroupBy);
        }

        public string GetRecordResultSqlText()
        {
            int rowStart = (PageIndex - 1) * PageSize + 1;
            int rowEnd = rowStart + PageSize - 1;
            if (string.IsNullOrEmpty(GroupBy))
            {
                return @"
with t as(
select  row_number() over ( order by {2} desc ) as rowNo,
        {3}
from    {0}
where   {1}
)
select * from t where t.rowNo between {4} and {5}".format(TableList, Where, OrderByColumn, ColumnList, rowStart, rowEnd);
            }
            return @"
with t as(
select  row_number() over ( order by {2} desc ) as rowNo,
        {3}
from    {0}
where   {1}
group by {6}
)
select * from t where t.rowNo between {4} and {5}"
                .format(TableList, Where, OrderByColumn, ColumnList, rowStart, rowEnd, GroupBy);
        }
    }
}
