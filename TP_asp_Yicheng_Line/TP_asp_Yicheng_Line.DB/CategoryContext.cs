using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using TP_asp_Yicheng_Line.DB.Models;

namespace TP_asp_Yicheng_Line.DB
{

    public class CategoryContext
    {
        private String connectionString;

        public CategoryContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<Category> GetAll()
        {
            List<Category> categories = new List<Category>();
            using (MySqlConnection c= new MySqlConnection(connectionString))
            {
             
                c.Open();
                MySqlCommand command = c.CreateCommand();
                command.CommandText = "SELECT identifiant, libelle, date FROM category ORDER BY identifiant";

                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Category category = new Category();

                    category.Identifiant = reader.GetInt32("identifiant");
                    category.Libelle = reader.GetString("libelle");
                    category.Date = reader.GetDateTime("date");

                    categories.Add(category);
                }


            }

            return categories;
        }

        public Category Get(int id)
        {
            Category category = null;
            using (MySqlConnection c = new MySqlConnection(connectionString))
            {
                c.Open();
                MySqlCommand command = c.CreateCommand();
                command.CommandText = "@SELECT identifiant, libelle, date FROM category WHERE identifiant=@identifiant";
                command.Parameters.AddWithValue("identifiant", id);

                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    category = new Category();
                    category.Identifiant = reader.GetInt32("identifiant");
                    category.Libelle = reader.GetString("libelle");
                    category.Date = reader.GetDateTime("date");

                }


            }

            return category;
        }

        public bool Insert(Category category)
        {
            int nbLignes = 0;
            using (MySqlConnection c = new MySqlConnection(connectionString))
            {
                c.Open();
                MySqlCommand command = c.CreateCommand();
                command.CommandText = "INSERT INTO category(libelle, date) VALUE(@libelle, @date)";

                command.Parameters.AddWithValue("libelle", category.Libelle);
                command.Parameters.AddWithValue("date", category.Date);


                nbLignes = command.ExecuteNonQuery();

            }
            return nbLignes > 0;
        }

        public bool Update(Category category)
        {
            int nbLignes = 0;
            using (MySqlConnection c = new MySqlConnection(connectionString))
            {
                c.Open();
                MySqlCommand command = c.CreateCommand();
                command.CommandText = @"
                        UPDATE category SET libelle = @libelle, date=@date
                        WHERE identifiant = @identifiant
                    ";

                command.Parameters.AddWithValue("identifiant", category.Identifiant);
                command.Parameters.AddWithValue("libelle", category.Libelle);
                command.Parameters.AddWithValue("date", category.Date);
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
                command.CommandText = "DELETE FROM category WHERE identifiant = @id";

                command.Parameters.AddWithValue("id", id);
                nbLignes = command.ExecuteNonQuery();

            }
            return nbLignes > 0;
        }
    }
}
