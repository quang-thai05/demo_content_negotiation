using DemoContentNegotiation.Models;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Text;

namespace DemoContentNegotiation.Formatters
{
    public class CsvOutputFormatter : TextOutputFormatter
    {
        public CsvOutputFormatter()
        {
            SupportedMediaTypes.Add("text/csv");
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        protected override bool CanWriteType(Type? type)
        {
            if (type == typeof(Blog))
            {
                return true;
            }
            else
            {
                Type enumerableType = typeof(IEnumerable<Blog>);
                return enumerableType.IsAssignableFrom(type);
            }
        }

        public async override Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var httpContext = context.HttpContext;
            var buffer = new StringBuilder();

            buffer.Append($"Name, Description, BlogPosts");
            buffer.AppendLine();
            if (context.Object is IEnumerable<Blog> blogs)
            {
                foreach (var blog in blogs)
                {
                    FormatCSV(buffer, blog);
                }
            }
            else
            {
                FormatCSV(buffer, (Blog)context.Object!);
            }

            await httpContext.Response.WriteAsync(buffer.ToString(), selectedEncoding);
        }

        private void FormatCSV(StringBuilder buffer, Blog contact)
        {
            buffer.Append($"{contact.Name},");
            buffer.Append($"{contact.Description},");
            foreach (var blogPost in contact.BlogPosts)
            {
                buffer.Append($"{blogPost.Title},");
                buffer.Append($"{blogPost.MetaDescription},");
                buffer.Append($"{blogPost.Published},");
            }
            buffer.AppendLine();
        }
    }
}
