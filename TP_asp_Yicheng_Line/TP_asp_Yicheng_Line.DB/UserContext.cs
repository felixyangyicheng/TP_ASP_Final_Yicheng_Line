using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

using TP_asp_Yicheng_Line.DB.Models;


namespace TP_asp_Yicheng_Line.DB
{
    public class UserContext
    {
        private String connectionString;
        public UserContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public List<User> GetAll()
        {
            List<User> users = new List<User>();
            using (MySqlConnection c = new MySqlConnection(connectionString))
            {
                c.Open();
                MySqlCommand command = c.CreateCommand();
                command.CommandText = "SELECT identifiant, nom, prenom, identifiantCivilite, identifiantRole FROM user ORDER BY identifiant";

                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    User user = new User();

                    user.Identifiant = reader.GetInt32("identifiant");
                    user.Nom = reader.GetString("nom");
                    user.Prenom = reader.GetString("prenom");
                    user.IdentifiantCivilite = reader.GetInt32("identifiantCivilite");
                    user.IdentifiantRole = reader.GetInt32("identifiantRole");

                    users.Add(user);
                }
                return users;
            }
        }

        public User Get(int id)
        {
            User user = null;
            using (MySqlConnection c = new MySqlConnection(connectionString))
            {
                c.Open();
                MySqlCommand command = c.CreateCommand();
                command.CommandText = @"SELECT identifiant, nom, prenom, identifiantCivilite, identifiantRole FROM user
                  WHERE identifiant = @identifiant";

                command.Parameters.AddWithValue("identifiant", id);

                MySqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    user = new User();
                    user.Identifiant = reader.GetInt32("identifiant");

                    user.Nom = reader.GetString("nom");
                    user.Prenom = reader.GetString("prenom");
                    user.IdentifiantCivilite = reader.GetInt32("identifiantCivilite");
                    user.IdentifiantRole = reader.GetInt32("identifiantRole");
                }
            }
            return user;
        }

        public bool Insert(User user)
        {
            int nbLignes = 0;
            using (MySqlConnection c = new MySqlConnection(connectionString))
            {
                c.Open();
                MySqlCommand command = c.CreateCommand();
                command.CommandText = "INSERT INTO user(nom, prenom, identifiantCivilite, identifiantRole) VALUE(@nom, @prenom, @identifiantCivilite, @identifiantRole)";

                command.Parameters.AddWithValue("nom", user.Nom);
                command.Parameters.AddWithValue("prenom", user.Prenom);
                command.Parameters.AddWithValue("identifiantCivilite", user.IdentifiantCivilite);
                command.Parameters.AddWithValue("identifiantRole", user.IdentifiantRole);

                nbLignes = command.ExecuteNonQuery();

            }
            return nbLignes > 0;
        }

        public bool Update(User user)
        {
            int nbLignes = 0;
            using (MySqlConnection c = new MySqlConnection(connectionString))
            {
                c.Open();
                MySqlCommand command = c.CreateCommand();
                command.CommandText = @"
                        UPDATE user SET nom = @nom, prenom = @prenom, identifiantCivilite = @identifiantCivilite, identifiantRole = @identifiantRole 
                        WHERE identifiant = @identifiant
                    ";

                command.Parameters.AddWithValue("identifiant", user.Identifiant);
                command.Parameters.AddWithValue("nom", user.Nom);
                command.Parameters.AddWithValue("prenom", user.Prenom);
                command.Parameters.AddWithValue("identifiantCivilite", user.IdentifiantCivilite);
                command.Parameters.AddWithValue("identifiantRole", user.IdentifiantRole);

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
                command.CommandText = "DELETE FROM user WHERE identifiant = @id";

                command.Parameters.AddWithValue("id", id);
                nbLignes = command.ExecuteNonQuery();

            }
            return nbLignes > 0;
        }

    }
}
