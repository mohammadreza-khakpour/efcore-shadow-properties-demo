using System;
using System.Collections.Generic;
using System.Linq;

namespace ShadowPropertiesDemo
{
    class Program
    {
        static DataBaseContext db = new DataBaseContext();
        static void Main(string[] args)
        {
            int blogId = AddBlog("blog01");
            int blogId02 = AddBlog("blog02");
            int blogId03 = AddBlog("blog03");

            GetBlogs().ForEach(blog =>
            {
                Console.WriteLine(String.Format("blog id: {0}, blog title: {1}"
                    ,blog.Id,blog.Title));
            });

            Console.WriteLine("\n--------------");
            UpdateBlog(blogId, "new title blog01");
            UpdateBlog(blogId02, "new title blog02");
            UpdateBlog(blogId03, "new title blog03");

        }

        static List<Blog> GetBlogs()
        {
            return db.Blogs.ToList();
        }
        static int AddBlog(string blogTitle) {
            Blog blog01 = new Blog
            {
                Title = blogTitle
            };
            db.Blogs.Add(blog01);
            db.SaveChanges();
            return blog01.Id;
        }
        static int UpdateBlog(int blogId, string newBlogTitle)
        {
            Blog theBlog = db.Blogs.Find(blogId);
            theBlog.Title = newBlogTitle;
            db.SaveChanges();
            return theBlog.Id;
        }
    }
}
