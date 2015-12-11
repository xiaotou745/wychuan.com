using System.Collections.Generic;

namespace wychuan2.com.Areas.admin.Models.DbTools
{
    public class CodeSettingTemplate
    {
        private CodeSettingTemplate() { }
        private static CodeSettingTemplate instance;
        private Dictionary<string, string> templates = new Dictionary<string,string>();

        public static CodeSettingTemplate Create()
        {
            if (instance == null)
            {
                instance = new CodeSettingTemplate();
                instance.templates.Add("1-1", DtoCodeOfCsharp());
                instance.templates.Add("2-1", DtoCodeOfJava());

                instance.templates.Add("1-2", ServiceInterCodeOfCsharp());
                instance.templates.Add("2-2", ServiceInterCodeOfJava());
            }
            return instance;
        }

        #region dto

        private static string DtoCodeOfCsharp()
        {
            return @"<pre class='brush:csharp'>
using System;

namespace {0}
{
    /// <summary>
    /// 实体类{1}DTO 。(属性说明自动提取数据库字段的描述信息)
    /// Generate By: {2}
    /// Generate Time: 2015-11-19 20:29:42
    /// </summary>
    public class {1}DTO
    {
        ///TODO class code.
    }
}</pre>";
        }

        private static string DtoCodeOfJava()
        {
            return @"<pre class='brush:java'>package {0};
 
import java.util.Date;
 
/**
 * 实体类{1}. (属性说明自动提取数据库字段的描述信息)
 * @author {2}
 * @date 2015-11-19 21:09:00
 *
 */
public class {1} {
    //something?
}</pre>";
        }
        #endregion

        #region ServiceInter
        /// <summary>
        /// 0:命名空间 1：ModelName 2: author 3: modelName 4:dto namespace
        /// </summary>
        /// <returns></returns>
        private static string ServiceInterCodeOfCsharp()
        {
            return @"<pre class='brush:csharp'>
using System;
using System.Collections.Generic;
using {4};

namespace {0}
{
    /// <summary>
    /// 业务逻辑类I{1}Service 的摘要说明。
    /// Generate By: {2}
    /// Generate Time: 2015-11-19 23:01:45
    /// </summary>
    public interface I{1}Service
    {
        /// <summary>
        /// 新增一条记录
        ///<param name='{3}'>要新增的对象</param>
        /// </summary>
        int Create({1}DTO {3});
 
        /// <summary>
        /// 修改一条记录
        ///<param name='{3}'>要修改的对象</param>
        /// </summary>
        void Modify({1}DTO {3});
 
        /// <summary>
        /// 删除一条记录
        /// </summary>
        void Remove(int id);
 
        /// <summary>
        /// 根据Id得到一个对象实体
        /// </summary>
        {1}DTO GetById(int id);
 
        /// <summary>
        /// 查询方法
        /// </summary>
        IList<{1}DTO> Query({1}QueryDTO {3}QueryDTO);
    }
}</pre>";
        }
        /// <summary>
        /// 0:命名空间 1：ModelName 2: author 3: modelName 4:dto namespace
        /// </summary>
        /// <returns></returns>
        private static string ServiceInterCodeOfJava()
        {
            return @"
<pre class='brush:java'>
package {0};

import java.util.List;
import {4}.{1};
 
/**
 * 业务逻辑类I{1}Service 的摘要说明。
 * @author {2}
 * @date 2015-11-19 23:01:45
 */
public interface I{1}Service {
    /**
	 * 新增一条记录
	 * @param {3} 要新增的对象
	 * @return 自增ID
	 */
	int create({1} {3});

    /**
	 * 修改一条记录
	 * @param {3} 要修改的对象
	 */
	void modify({1} {3});

    /**
	 * 删除一条记录
	 * @param id 要删除的记录ID
	 */
	void remove(int id);

	/**
	 * 获取指定Id的数据
	 * @param id 要获取数据的Id
	 * @return 返回指定Id的对象
	 */
	{1} getById(int id);

    /**
	 * 查询
	 * @param queryParams 查询条件
	 * @return 返回满足查询条件的对象列表
	 */
    List<{1}> query({1}QueryParams queryParams);
}
</pre>";
        }
        #endregion
        public Dictionary<string, string> GetTemplates()
        {
            return this.templates;
        }
    }
}