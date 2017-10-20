using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Shopping.Models
{
    public class Cart
    {
        public int? cart_key { get; set; }
        public int? user_key { get; set; }
        public int? book_key { get; set; }
        public int? quantity { get; set; }
    }
    public class CartLayer
    {
        internal static List<Book> GetBooksInCart(User data)
        {
            var books = new List<Book>();
            SqlDataReader BookReader = null;
            try
            {
                var query = $"SELECT b.book_key, b.name, b.author, b.pages, b.description, b.price, c.quantity FROM shop.cart c INNER JOIN shop.books b on c.book_key = b.book_key WHERE user_key = {data.user_key}";
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
        public static int BuyBooks(Cart data)
        {
            var AddToCartParameters = new List<SqlParameter>();
            AddToCartParameters.Add(new SqlParameter("@user_key", data.user_key));
            AddToCartParameters.Add(new SqlParameter("@book_key", data.book_key));
            AddToCartParameters.Add(new SqlParameter("@quantity", data.quantity));
            return SQLConnect.ExecuteNonQuery("shop.BUY_BOOK", AddToCartParameters, CommandType.StoredProcedure);
        }
        public static int DeleteFromCart(Cart data)
        {
            var AddToCartParameters = new List<SqlParameter>();
            AddToCartParameters.Add(new SqlParameter("@user_key", data.user_key));
            AddToCartParameters.Add(new SqlParameter("@book_key", data.book_key));
            return SQLConnect.ExecuteNonQuery("shop.DeleteFromCart", AddToCartParameters, CommandType.StoredProcedure);
        }
    }
}