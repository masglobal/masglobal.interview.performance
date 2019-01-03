using System.Collections.Generic;
using System.Text;
using masglobal.interview.performance.web.Models;
using masglobal.interview.performance.web.Util;
using Microsoft.EntityFrameworkCore.Migrations;
using Newtonsoft.Json;

namespace masglobal.interview.performance.web.Migrations
{
    public partial class InitialData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var p = LoadProducts();
            var c = LoadCategories();
            var pc = LoadProductCategories(p, c);


            StringBuilder sb = new StringBuilder(90000);

            foreach (Product product in p)
            {
                sb.AppendFormat("INSERT INTO Products (Id, Name, Price) VALUES ('{0}', '{1}', {2:F2});",
                    product.Id, product.Name, product.Price);
            }

            foreach (Category category in c)
            {
                sb.AppendFormat("INSERT INTO Categories (Id, Name) VALUES ('{0}', '{1}');",
                    category.Id, category.Name);
            }

            foreach (ProductCategory productCategory in pc)
            {
                sb.AppendFormat("INSERT INTO ProductCategory (ProductId, CategoryId) VALUES ('{0}', '{1}');",
                    productCategory.ProductId, productCategory.CategoryId);
            }
            migrationBuilder.Sql(sb.ToString());
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }


        private List<Product> LoadProducts()
        {
            var rawJson = ResourceHelper.GetEmbeddedResource("Resources.products.json");
            var products = JsonConvert.DeserializeObject<List<Product>>(rawJson);
            return products;
        }

        private List<Category> LoadCategories()
        {
            var rawJson = ResourceHelper.GetEmbeddedResource("Resources.categories.json");
            var categories = JsonConvert.DeserializeObject<List<Category>>(rawJson);
            return categories;
        }

        private List<ProductCategory> LoadProductCategories(List<Product> products, List<Category> categories)
        {
            var productCategories = new List<ProductCategory>();
            int limit = categories.Count;
            foreach (Product product in products)
            {
                int cat = 0;
                for (int i = 0; i < 3; i++, cat++)
                {
                    productCategories.Add(new ProductCategory
                    {
                        ProductId = product.Id,
                        CategoryId = categories[cat % limit].Id,
                        Product = product,
                        Category = categories[cat % limit]
                    });
                }

            }

            return productCategories;
        }

    }
}
