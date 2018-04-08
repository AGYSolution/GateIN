using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.GateIn.Console.DAL
{
    public class ContCardPICDal
    {
        /// <summary>
        /// create new table contcardpic
        /// </summary>
        /// <param name="npgsqlDataReader"></param>
        /// <param name="contCardPIC"></param>
        private void MappingDataReaderToContCardpic(NpgsqlDataReader npgsqlDataReader, ContCardPic contCardPIC)
        {
            contCardPIC.ContCardPicID = npgsqlDataReader.GetInt64(npgsqlDataReader.GetOrdinal("ContCardPicID"));
            contCardPIC.ContCardID = npgsqlDataReader.GetInt64(npgsqlDataReader.GetOrdinal("ContCardID"));
            contCardPIC.PicDtm = npgsqlDataReader.GetDateTime(npgsqlDataReader.GetOrdinal("PicDtm"));
            contCardPIC.PicName = npgsqlDataReader.GetString(npgsqlDataReader.GetOrdinal("PicName"));
            contCardPIC.PicData = (byte[])npgsqlDataReader[4];
        }
        public bool InsertContCardPIC(ContCardPic contCardPic)
        {
            bool result = false;
            try
            {
                using (NpgsqlConnection npgsqlConnection = AppConfig.GetConnection())
                {
                    if (npgsqlConnection.State == ConnectionState.Closed)
                    {
                        npgsqlConnection.Open();
                    }
                    contCardPic.ContCardPicID = CtsCounter.NextValCtsCounter("CONTCARDPIC_CONTCARDPICID_SEQ");
                    contCardPic.PicDtm = DateTime.Now;
                    string query = "INSERT INTO contcardpic(contcardpicid,contcardid,picdtm,picname,picdata) VALUES(@contcardpicid,@contcardid,@picdtm,@picname,@picdata) ";
                    using (NpgsqlCommand npgsqlCommand = new NpgsqlCommand(query, npgsqlConnection))
                    {
                        npgsqlCommand.Parameters.AddWithValue("@contcardpicid", contCardPic.ContCardPicID);
                        npgsqlCommand.Parameters.AddWithValue("@contcardid", contCardPic.ContCardID);
                        npgsqlCommand.Parameters.AddWithValue("@picdtm", contCardPic.PicDtm);
                        npgsqlCommand.Parameters.AddWithValue("@picname", contCardPic.PicName);
                        npgsqlCommand.Parameters.AddWithValue("@picdata", contCardPic.PicData);
                        npgsqlCommand.ExecuteNonQuery();
                        result = true;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }
        public bool UpdateContCardPIC(ContCardPic contCardPic)
        {
            bool result = false;
            try
            {
                using (NpgsqlConnection npgsqlConnection = AppConfig.GetConnection())
                {
                    if (npgsqlConnection.State == ConnectionState.Closed)
                    {
                        npgsqlConnection.Open();
                    }
                    string query = "UPDATE contcardpic SET contcardid=@contcardid, picdtm=@picdtm, picname=@picname, picdata=@picdata WHERE contcardpicid=@contcardpicid ";
                    using (NpgsqlCommand npgsqlCommand = new NpgsqlCommand(query, npgsqlConnection))
                    {
                        npgsqlCommand.Parameters.AddWithValue("@contcardpicid", contCardPic.ContCardPicID);
                        npgsqlCommand.Parameters.AddWithValue("@contcardid", contCardPic.ContCardID);
                        npgsqlCommand.Parameters.AddWithValue("@picdtm", contCardPic.PicDtm);
                        npgsqlCommand.Parameters.AddWithValue("@picname", contCardPic.PicName);
                        npgsqlCommand.Parameters.AddWithValue("@picdata", contCardPic.PicData);
                        npgsqlCommand.ExecuteNonQuery();
                        result = true;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }
        public ContCardPic GetContCardPICByID(long contCardPICID)
        {
            ContCardPic contCardPic = new ContCardPic();
            try
            {
                using (NpgsqlConnection npgsqlConnection = AppConfig.GetConnection())
                {
                    if (npgsqlConnection.State == ConnectionState.Closed)
                    {
                        npgsqlConnection.Open();
                    }
                    string query = "SELECT contcardpicid,contcardid,picdtm,picname,picdata FROM contcardpic WHERE contcardpicid=@id ";
                    using (NpgsqlCommand npgsqlCommand = new NpgsqlCommand(query, npgsqlConnection))
                    {
                        npgsqlCommand.Parameters.AddWithValue("@id", contCardPICID);
                        using (NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader())
                        {
                            if (npgsqlDataReader.Read())
                            {
                                MappingDataReaderToContCardpic(npgsqlDataReader, contCardPic);
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return contCardPic;
        }
    }
}
