using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.GateIn.Console.DAL
{
    public class SecureGateLogDAL
    {

        public void MappingDataReaderToSecureGateLog(NpgsqlDataReader npgsqlDataReader, SecureGateLog secureGateLog)
        {
            secureGateLog.SecureGateLogID = npgsqlDataReader.GetInt64(npgsqlDataReader.GetOrdinal("SecureGateLogID"));
            if (npgsqlDataReader["dtm1"] != DBNull.Value)
            {
                secureGateLog.Dtm1 = npgsqlDataReader.GetDateTime(npgsqlDataReader.GetOrdinal("dtm1"));
            }
            secureGateLog.Loc1 = npgsqlDataReader.GetString(npgsqlDataReader.GetOrdinal("Loc1"));

            secureGateLog.LogCat = npgsqlDataReader.GetString(npgsqlDataReader.GetOrdinal("LogCat"));
            secureGateLog.LogCat = npgsqlDataReader.GetString(npgsqlDataReader.GetOrdinal("LogRemark"));

            secureGateLog.RefID = npgsqlDataReader.GetInt64(npgsqlDataReader.GetOrdinal("RefID"));
        }
        public bool InsertSecureGateLog(SecureGateLog secureGateLog)
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
                    string query = "INSERT INTO securegatelog(securegatelogid,dtm1,loc1,logcat,logremark,refid) VALUES(@SecureGateLogID,@Dtm1,@Loc1,@LogCat,@LogRemark,@RefID)";
                    secureGateLog.SecureGateLogID = CtsCounter.NextValCtsCounter("SecureGateLog_SecureGateLogID_SEQ");
                    secureGateLog.Dtm1 = DateTime.Now;
                    using (NpgsqlCommand npgsqlCommand = new NpgsqlCommand(query, npgsqlConnection))
                    {
                        npgsqlCommand.Parameters.AddWithValue("@SecureGateLogID", secureGateLog.SecureGateLogID);

                        npgsqlCommand.Parameters.AddWithValue("@Dtm1", secureGateLog.Dtm1);
                        npgsqlCommand.Parameters.AddWithValue("@Loc1", secureGateLog.Loc1);
                        npgsqlCommand.Parameters.AddWithValue("@LogCat", secureGateLog.LogCat);
                        npgsqlCommand.Parameters.AddWithValue("@LogRemark", secureGateLog.LogRemark);
                        npgsqlCommand.Parameters.AddWithValue("@RefID", secureGateLog.RefID);


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
        public bool UpdateSecureGateLog(SecureGateLog secureGateLog)
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
                    string query = "UPDATE securegatelog SET  dtm1=@Dtm1, loc1=@Loc1, logcat=@LogCat, logremark=@LogRemark, refid=@RefID WHERE securegatelogid=@SecureGateLogID ";
                    //secureGateLog.SecureGateLogID = CtsCounter.NextValCtsCounter("SecureGateLog_SecureGateLogID_SEQ");
                    //secureGateLog.Dtm1 = DateTime.Now;
                    using (NpgsqlCommand npgsqlCommand = new NpgsqlCommand(query, npgsqlConnection))
                    {
                        npgsqlCommand.Parameters.AddWithValue("@SecureGateLogID", secureGateLog.SecureGateLogID);

                        npgsqlCommand.Parameters.AddWithValue("@Dtm1", secureGateLog.Dtm1);
                        npgsqlCommand.Parameters.AddWithValue("@Loc1", secureGateLog.Loc1);
                        npgsqlCommand.Parameters.AddWithValue("@LogCat", secureGateLog.LogCat);
                        npgsqlCommand.Parameters.AddWithValue("@LogRemark", secureGateLog.LogRemark);
                        npgsqlCommand.Parameters.AddWithValue("@RefID", secureGateLog.RefID);


                        npgsqlCommand.ExecuteNonQuery();
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
        public SecureGateLog FillSecureGateLogByID(long ID)
        {
            SecureGateLog secureGateLog = new SecureGateLog();
            try
            {
                using (NpgsqlConnection npgsqlConnection = AppConfig.GetConnection())
                {
                    if (npgsqlConnection.State == ConnectionState.Closed)
                    {
                        npgsqlConnection.Open();
                    }
                    string query = "SELECT securegatelogid,dtm1,loc1,logcat,logremark,refid FROM securegatelog WHERE securegatelogid=@id ";
                    using (NpgsqlCommand npgsqlCommand = new NpgsqlCommand(query, npgsqlConnection))
                    {
                        npgsqlCommand.Parameters.AddWithValue("@id", ID);
                        using (NpgsqlDataReader npgsqlDataReader = npgsqlCommand.ExecuteReader())
                        {
                            if (npgsqlDataReader.Read())
                            {
                                MappingDataReaderToSecureGateLog(npgsqlDataReader, secureGateLog);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return secureGateLog;
        }
    }
}
