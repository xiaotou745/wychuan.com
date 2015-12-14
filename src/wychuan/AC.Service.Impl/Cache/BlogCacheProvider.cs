using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AC.Cache;
using AC.Dao.Blog;
using AC.Extension;
using AC.Service.DTO.Blog;

namespace AC.Service.Impl.Cache
{
    public class BlogCacheProvider
    {
        private static readonly BlogCategoryDao categoryDao = new BlogCategoryDao();
        private static readonly BlogTagsDao tagsDao = new BlogTagsDao();

        #region 随笔分类

        /// <summary>
        /// 获取指定用户的随笔分类
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>返回指定用户的随笔分类</returns>
        public static IList<BlogCategoryDTO> GetBlogCategoriesFromCache(int userId)
        {
            var allCategories =
                DataCache.GetCache(Consts.ALL_BLOG_CATEGORIES_CACHE_KEY.format(userId)) as IList<BlogCategoryDTO>;
            if (allCategories == null)
            {
                return RefreshBlogCategoriesInCache(userId);
            }
            return allCategories;
        }

        /// <summary>
        /// 刷新缓存中指定用户的随笔分类
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static IList<BlogCategoryDTO> RefreshBlogCategoriesInCache(int userId)
        {
            var allCategories = categoryDao.GetByUserId(userId);
            DataCache.SetCacheSliding(Consts.ALL_BLOG_CATEGORIES_CACHE_KEY.format(userId), allCategories,
                TimeSpan.FromDays(1));
            return allCategories;
        }

        #endregion

        #region 随笔标签

        /// <summary>
        /// 获取指定用户的随笔tag列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <returns>返回指定用户的随笔tag列表</returns>
        public static IList<BlogTagsDTO> GetBlogTagsFromCache(int userId)
        {
            string key = Consts.ALL_BLOG_TAGS_CACHE_KEY.format(userId);

            var allTags = DataCache.GetCache(key) as IList<BlogTagsDTO>;
            if (allTags == null)
            {
                return RefreshBlogTagsInCache(userId);
            }
            return allTags;
        }

        /// <summary>
        /// 刷新缓存中的指定用户的随笔tags
        /// </summary>
        /// <param name="userId">指定用户ID</param>
        /// <returns>返回刷新后指定用户在缓存中的tag列表</returns>
        public static IList<BlogTagsDTO> RefreshBlogTagsInCache(int userId)
        {
            string key = Consts.ALL_BLOG_TAGS_CACHE_KEY.format(userId);

            var allTags = tagsDao.GetByUserId(userId);
            DataCache.SetCacheSliding(key, allTags, TimeSpan.FromDays(1));

            return allTags;
        }

        #endregion
    }
}