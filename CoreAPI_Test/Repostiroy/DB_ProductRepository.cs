using System.Data;
using CoreAPI_Test.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace CoreAPI_Test.Repostiroy
{
    public class DB_ProductRepository
    {
        private readonly string _connectionString;
        public DB_ProductRepository(string conn)
        {
            _connectionString = conn;
        }

        // Get All
        public async Task<IEnumerable<DB_ProductModel>> GetAllProduct()
        {
            using IDbConnection dbConnection = new SqlConnection( _connectionString );
            dbConnection.Open();
            string sql = "select * from Product";
            return await dbConnection.QueryAsync<DB_ProductModel>(sql);
        }

        // Get ByID
        public async Task<DB_ProductModel?> GetProductById(int id)
        {
            using IDbConnection dbConnection = new SqlConnection(_connectionString);
            dbConnection.Open();
            string sql = "select * from Product where ProductID = @Id";
            return await dbConnection.QueryFirstOrDefaultAsync<DB_ProductModel>(sql, new { Id = id });
        }


        // Post
        public async Task<int> InsertProduct(DB_ProductModel model)
        {
            using IDbConnection dbConnection = new SqlConnection(_connectionString);
            dbConnection.Open();
            string sql = @"insert into Product (ProductName, UnitPrice, Discount, CategoryID, Explain)
                           values(@ProductName, @UnitPrice, @Discount, @CategoryID, @Explain)";
            return await dbConnection.ExecuteAsync(sql, model); // 回傳成功 更新/刪除 的紀錄條數
        }


        // Put
        public async Task<int> UpdateProduct(DB_ProductModel model)
        {
            using IDbConnection dbConnection = new SqlConnection(_connectionString);
            dbConnection.Open();
            string sql = @"update Product
                           set ProductName = @ProductName,
                            UnitPrice = @UnitPrice,
                            Explain = @Explain
                           where ProductID = @ProductID";

            return await dbConnection.ExecuteAsync(sql, model);     // 回傳成功 更新/刪除 的紀錄條數
        }

        // delete
        public async Task<int> DeleteProduct(int id)
        {
            using IDbConnection dbConnection = new SqlConnection(_connectionString);
            dbConnection.Open();
            string sql = @"delete from Product
                           where ProductID = @ProductID";
            return await dbConnection.ExecuteAsync(sql, id);
        }



    }
}
