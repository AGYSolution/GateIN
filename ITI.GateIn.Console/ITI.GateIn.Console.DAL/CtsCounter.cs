﻿using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.GateIn.Console.DAL
{
    public class CtsCounter
    {
        public static bool CheckAvailable(string code)
        {
            bool result = false;
            try
            {
                using (NpgsqlConnection npgsqlConnection = AppConfig.GetConnection())
                {
                    string query = "SELECT * FROM ctscounter WHERE code=@code FOR UPDATE ";
                    using (NpgsqlCommand npgsqlCommand = new NpgsqlCommand(query, npgsqlConnection))
                    {
                        npgsqlCommand.Parameters.AddWithValue("@code", code);
                        using (NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader())
                        {
                            if (npgsqlDataReader.Read())
                            {
                                result = true;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }
        private static void InsertCtsCounter(String code, long count)
        {
            try
            {
                using (NpgsqlConnection npgsqlConnection = AppConfig.GetConnection())
                {
                    string query = "INSERT INTO ctscounter(code,cnt) VALUES(@code,@cnt) ";
                    using (NpgsqlCommand npgsqlCommand = new NpgsqlCommand(query, npgsqlConnection))
                    {
                        npgsqlCommand.Parameters.AddWithValue("@code", code);

                        npgsqlCommand.Parameters.AddWithValue("@cnt", count);
                        npgsqlCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private static void UpdateCtsCounter(String code, long count)
        {
            try
            {
                using (NpgsqlConnection npgsqlConnection = AppConfig.GetConnection())
                {
                    string query = "UPDATE ctscounter SET cnt=@cnt WHERE code=@code ";
                    using (NpgsqlCommand npgsqlCommand = new NpgsqlCommand(query, npgsqlConnection))
                    {
                        npgsqlCommand.Parameters.AddWithValue("@code", code);

                        npgsqlCommand.Parameters.AddWithValue("@cnt", count);
                        npgsqlCommand.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static long NextValCtsCounter(string code)
        {
            long count = 0;
            if (CheckAvailable(code))
            {
                count += 1;
                UpdateCtsCounter(code, count);
            }
            else
            {
                InsertCtsCounter(code, count);
            }

            return count;

        }
        public static void SetValCtsCounter(string code, long val)
        {
            long count = 0;
            if (CheckAvailable(code))
            {
                count = val;
                UpdateCtsCounter(code, count);
            }
            else
            {
                InsertCtsCounter(code, count);
            }

        }

    }
}
