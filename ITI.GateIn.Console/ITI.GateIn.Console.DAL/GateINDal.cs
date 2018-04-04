using System;
using Npgsql;
using System.Collections.Generic;
using System.Data;

namespace ITI.GateIn.Console.DAL
{
    public class GateINDal
    {
        public const string DEFAULT_COLUMN = " {0}contcardid,{0}cardmode,{0}refid,{0}cont,{0}size,{0}type,{0}dtm1,{0}loc1,{0}dtm2,{0}loc2,{0}remark,{0}dtm3,{0}continoutid,{0}userid3,{0}seal1,{0}seal2,{0}seal3,{0}seal4,{0}nomobilout,{0}angkutanout,{0}eiroutno,{0}token,{0}iscombo ";
        public const string DEFAULT_TABLE = "contcard";

        private static void MappingDataReaderToContCard(NpgsqlDataReader npgsqlDataReader, ContCard contCard)
        {
            contCard.ContCardID = npgsqlDataReader.GetInt64(npgsqlDataReader.GetOrdinal("contcardid"));
            contCard.CardMode = npgsqlDataReader.GetString(npgsqlDataReader.GetOrdinal("cardmode"));
            contCard.RefID = npgsqlDataReader.GetInt64(npgsqlDataReader.GetOrdinal("refid"));
            contCard.Cont = npgsqlDataReader.GetString(npgsqlDataReader.GetOrdinal("cont"));
            contCard.Size = npgsqlDataReader.GetString(npgsqlDataReader.GetOrdinal("size"));
            contCard.Type = npgsqlDataReader.GetString(npgsqlDataReader.GetOrdinal("type"));
            if (npgsqlDataReader["dtm1"] != DBNull.Value)
            {
                contCard.Dtm1 = npgsqlDataReader.GetDateTime(npgsqlDataReader.GetOrdinal("dtm1")).ToString();
            }
            contCard.Loc1 = npgsqlDataReader.GetString(npgsqlDataReader.GetOrdinal("loc1"));
            if (npgsqlDataReader["dtm2"] != DBNull.Value)
            {
                contCard.Dtm2 = npgsqlDataReader.GetDateTime(npgsqlDataReader.GetOrdinal("dtm2")).ToString();
            }
            contCard.Loc2 = npgsqlDataReader.GetString(npgsqlDataReader.GetOrdinal("loc2"));
            contCard.Remark = npgsqlDataReader.GetString(npgsqlDataReader.GetOrdinal("remark"));
            if (npgsqlDataReader["dtm3"] != DBNull.Value)
            {
                contCard.Dtm3 = npgsqlDataReader.GetDateTime(npgsqlDataReader.GetOrdinal("dtm3")).ToString();
            }
            contCard.ContInOutID = npgsqlDataReader.GetInt64(npgsqlDataReader.GetOrdinal("continoutid"));
            contCard.UserID3 = npgsqlDataReader.GetString(npgsqlDataReader.GetOrdinal("userid3"));
            contCard.Seal1 = npgsqlDataReader.GetString(npgsqlDataReader.GetOrdinal("seal1"));
            contCard.Seal2 = npgsqlDataReader.GetString(npgsqlDataReader.GetOrdinal("seal2"));
            contCard.Seal3 = npgsqlDataReader.GetString(npgsqlDataReader.GetOrdinal("seal3"));
            contCard.Seal4 = npgsqlDataReader.GetString(npgsqlDataReader.GetOrdinal("seal4"));
            contCard.NoMobilOut = npgsqlDataReader.GetString(npgsqlDataReader.GetOrdinal("nomobilout"));
            contCard.AngkutanOut = npgsqlDataReader.GetString(npgsqlDataReader.GetOrdinal("angkutanout"));
            contCard.Token = npgsqlDataReader.GetString(npgsqlDataReader.GetOrdinal("token"));
            contCard.IsCombo = npgsqlDataReader.GetBoolean(npgsqlDataReader.GetOrdinal("iscombo"));
        }

        public static string CheckKendaraan(string contCardID)
        {
            string result = string.Empty;
            try
            {
                using (NpgsqlConnection npgsqlConnection = AppConfig.GetConnection())
                {
                    if (npgsqlConnection.State == ConnectionState.Closed)
                    {
                        npgsqlConnection.Open();
                    }
                    string query = string.Format("SELECT {0} FROM {1} WHERE contcardid=@ContCardId ",
                                        string.Format(DEFAULT_COLUMN, string.Empty),
                                        DEFAULT_TABLE);
                    using (NpgsqlCommand npgsqlCommand = new NpgsqlCommand(query, npgsqlConnection))
                    {
                        npgsqlCommand.Parameters.AddWithValue("@ContCardId", contCardID);
                        using (NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader())
                        {
                            if (npgsqlDataReader.Read())
                            {
                                result = "ID ContCard: " + npgsqlDataReader.GetString(npgsqlDataReader.GetOrdinal("contcardid")) + " ,";
                                result = result + "Card Mode :" + npgsqlDataReader.GetString(npgsqlDataReader.GetOrdinal("cardmode"));
                                result = result + "Ref Mode :" + npgsqlDataReader.GetInt64(npgsqlDataReader.GetOrdinal("refid"));
                                result = result + "Cont Count :" + npgsqlDataReader.GetString(npgsqlDataReader.GetOrdinal("cont"));
                                result = result + "Cont Size :" + npgsqlDataReader.GetString(npgsqlDataReader.GetOrdinal("size"));
                                result = result + "Cont Type :" + npgsqlDataReader.GetString(npgsqlDataReader.GetOrdinal("type"));
                                if (npgsqlDataReader["dtm1"] != DBNull.Value)
                                {
                                    result = result + "dtm1 :" + npgsqlDataReader.GetDateTime(npgsqlDataReader.GetOrdinal("dtm1")).ToString();
                                }
                                result = result + "Loc1 :" + npgsqlDataReader.GetString(npgsqlDataReader.GetOrdinal("loc1"));
                                if (npgsqlDataReader["dtm2"] != DBNull.Value)
                                {
                                    result = result + "dtm2 :" + npgsqlDataReader.GetDateTime(npgsqlDataReader.GetOrdinal("dtm2")).ToString();
                                }
                                result = result + "Loc2 :" + npgsqlDataReader.GetString(npgsqlDataReader.GetOrdinal("loc2"));
                                result = result + "Remark :" + npgsqlDataReader.GetString(npgsqlDataReader.GetOrdinal("remark"));
                                if (npgsqlDataReader["dtm3"] != DBNull.Value)
                                {
                                    result = result + "dtm3 :" + npgsqlDataReader.GetDateTime(npgsqlDataReader.GetOrdinal("dtm3")).ToString();
                                }
                                result = result + "contInOutID :" + npgsqlDataReader.GetInt64(npgsqlDataReader.GetOrdinal("continoutid")).ToString();
                                result = result + "userid3 :" + npgsqlDataReader.GetString(npgsqlDataReader.GetOrdinal("userid3"));
                                result = result + "seal1 :" + npgsqlDataReader.GetString(npgsqlDataReader.GetOrdinal("seal1"));
                                result = result + "seal2 :" + npgsqlDataReader.GetString(npgsqlDataReader.GetOrdinal("seal2"));
                                result = result + "seal3 :" + npgsqlDataReader.GetString(npgsqlDataReader.GetOrdinal("seal3"));
                                result = result + "seal4 :" + npgsqlDataReader.GetString(npgsqlDataReader.GetOrdinal("seal4"));
                                result = result + "nomobilout :" + npgsqlDataReader.GetString(npgsqlDataReader.GetOrdinal("nomobilout"));
                                result = result + "angkutanout :" + npgsqlDataReader.GetString(npgsqlDataReader.GetOrdinal("angkutanout")).ToString();
                                result = result + "token :" + npgsqlDataReader.GetString(npgsqlDataReader.GetOrdinal("token")).ToString();
                                result = result + "IsCombo :" + npgsqlDataReader.GetBoolean(npgsqlDataReader.GetOrdinal("iscombo")).ToString();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
        public static ContCard CheckKendaraan(long contCardID)
        {
            ContCard contCard = null;
            try
            {
                using (NpgsqlConnection npgsqlConnection = AppConfig.GetConnection())
                {
                    if (npgsqlConnection.State == ConnectionState.Closed)
                    {
                        npgsqlConnection.Open();
                    }
                    string query = string.Format("SELECT {0} FROM {1} WHERE contcardid=@ContCardId ",
                                        string.Format(DEFAULT_COLUMN, string.Empty),
                                        DEFAULT_TABLE);
                    using (NpgsqlCommand npgsqlCommand = new NpgsqlCommand(query, npgsqlConnection))
                    {
                        npgsqlCommand.Parameters.AddWithValue("@ContCardId", contCardID);
                        using (NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader())
                        {
                            if (npgsqlDataReader.Read())
                            {
                                contCard = new ContCard();
                                MappingDataReaderToContCard(npgsqlDataReader, contCard);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return contCard;
        }

        public static bool UpdateContCardGateIn(long contCardID, string location)
        {
            bool result = false;

            int rowAffected = 0;
            try
            {
                using (NpgsqlConnection npgsqlConnection = AppConfig.GetConnection())
                {
                    if (npgsqlConnection.State == ConnectionState.Closed)
                    {
                        npgsqlConnection.Open();
                    }
                    string query = "    UPDATE contcard " +
                                    "   SET  dtm1=@Dtm1, loc1=@Loc1  " +
                                    "   WHERE contcardid=@ContCardID ";
                    using (NpgsqlCommand npgsqlCommand = new NpgsqlCommand(query, npgsqlConnection))
                    {

                        npgsqlCommand.Parameters.AddWithValue("@ContCardID", contCardID);
                        npgsqlCommand.Parameters.AddWithValue("@Dtm1", DateTime.Now);
                        npgsqlCommand.Parameters.AddWithValue("@Loc1", location);
                        rowAffected = npgsqlCommand.ExecuteNonQuery();
                        if (rowAffected >= 1)
                            result = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
    }
}
