using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Shopping.Models
{
    public class Book
    {
        public int? book_key { get; set; }
        public string name { get; set; }
        public string author { get; set; }
        public int? pages { get; set; }
        public string description { get; set; }
        public double? price { get; set; }
        public int? quantity { get; set; }
    }
    public class BookLayer
    {
        public static List<Book> GetBooks()
        {
            var books = new List<Book>();
            SqlDataReader BookReader = null;
            try
            {
                var query = "select * from shop.books";
                BookReader = SQLConnect.GetSqlDataReader(query);
                while (BookReader.Read())
                {
                    var b = new Book
                    {
                        book_key = !DBNull.Value.Equals(BookReader["book_key"]) ? Convert.ToInt32(BookReader["book_key"]) : (int?)null,
                        name = !DBNull.Value.Equals(BookReader["name"]) ? Convert.ToString(BookReader["name"]) : string.Empty,
                        author = !DBNull.Value.Equals(BookReader["author"]) ? Convert.ToString(BookReader["author"]) : string.Empty,
                        pages = !DBNull.Value.Equals(BookReader["pages"]) ? Convert.ToInt32(BookReader["pages"]) : (int?)null,
                        description = !DBNull.Value.Equals(BookReader["description"]) ? Convert.ToString(BookReader["description"]) : string.Empty,
                        price = !DBNull.Value.Equals(BookReader["price"]) ? Convert.ToDouble(BookReader["price"]) : (double?)null,
                        quantity = !DBNull.Value.Equals(BookReader["quantity"]) ? Convert.ToInt32(BookReader["quantity"]) : (int?)null,
                    };
                    books.Add(b);
                }
                return books;
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                if (BookReader != null)
                    SQLConnect.CloseSqlDataReader(BookReader);
            }
        }
        public static int AddToCart(Cart data)
        {
            var AddToCartParameters = new List<SqlParameter>();
            AddToCartParameters.Add(new SqlParameter("@user_key", data.user_key));
            AddToCartParameters.Add(new SqlParameter("@book_key", data.book_key));
            AddToCartParameters.Add(new SqlParameter("@quantity", data.quantity));
            var res = SQLConnect.ExecuteNonQuery("shop.AddToCart", AddToCartParameters, CommandType.StoredProcedure);
            return res;
        }
    }
}