using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using TP_asp_Yicheng_Line.DB.Models;

namespace TP_asp_Yicheng_Line.DB
{
    public class ProductContext
    {
        private String connectionString;
        public ProductContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Product> GetAll()
        {
            List<Product> products = new List<Product>();
            using (MySqlConnection c = new MySqlConnection(connectionString))
            {
                c.Open();
                MySqlCommand command = c.CreateCommand();
                command.CommandText = "SELECT identifiant, titre, prix, identifiantCategory FROM product ORDER BY identifiant";

                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Product product = new Product();

                    product.Identifiant = reader.GetInt32("identifiant");

                    product.Titre = reader.GetString("titre");
                    product.Prix = reader.GetDouble("prix");
                    product.IdentifiantCategory = reader.GetInt32("identifiantCategory");


                    products.Add(product);
                }
                return products;
            }
        }
        public Product Get(int id)
        {
            Product product = null;
            using (MySqlConnection c = new MySqlConnection(connectionString))
            {
                c.Open();
                MySqlCommand command = c.CreateCommand();
                command.CommandText = @"SELECT identifiant, titre, prix, identifiantSeverite FROM product
                  WHERE identifiant = @identifiant";

                command.Parameters.AddWithValue("identifiant", id);

                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    product = new Product();
                    product.Identifiant = reader.GetInt32("identifiant");

                    product.Titre = reader.GetString("titre");
                    product.Prix = reader.GetDouble("prix");
                    product.IdentifiantCategory = reader.GetInt32("identifiantSeverite");
                    
                }
            }
            return product;
        }

        public bool Insert(Product product)
        {
            int nbLignes = 0;
            using (MySqlConnection c = new MySqlConnection(connectionString))
            {
                c.Open();
                MySqlCommand command = c.CreateCommand();
                command.CommandText = "INSERT INTO product( prix, titre, identifiantCategory) VALUE(@prix, @titre, @identifiantCategory)";

            
                command.Parameters.AddWithValue("date", product.Prix);
                command.Parameters.AddWithValue("titre", product.Titre);
                command.Parameters.AddWithValue("identifiantCategory", product.IdentifiantCategory);

                nbLignes = command.ExecuteNonQuery();

            }
            return nbLignes > 0;
        }

        public bool Update(Product product)
        {
            int nbLignes = 0;
            using (MySqlConnection c = new MySqlConnection(connectionString))
            {
                c.Open();
                MySqlCommand command = c.CreateCommand();
                command.CommandText = @"
                        UPDATE product SET  prix = @prix, titre = @titre, identifiantCategory = @identifiantCategory 
                        WHERE identifiant = @identifiant
                    ";

                command.Parameters.AddWithValue("identifiant", product.Identifiant);
                command.Parameters.AddWithValue("prix", product.Prix);
             
                command.Parameters.AddWithValue("titre", product.Titre);
                command.Parameters.AddWithValue("identifiantCategory", product.IdentifiantCategory);

                nbLignes = command.ExecuteNonQuery();

            }
            return nbLignes > 0;
        }

        public bool Delete(int id)
        {
            int nbLignes = 0;
            using (MySqlConnection c = new MySqlConnection(connectionString))
            {
                c.Open();
                MySqlCommand command = c.CreateCommand();
                command.CommandText = "DELETE FROM product WHERE identifiant = @id";

                command.Parameters.AddWithValue("id", id);
                nbLignes = command.ExecuteNonQuery();

            }
            return nbLignes > 0;
        }
    }
}
