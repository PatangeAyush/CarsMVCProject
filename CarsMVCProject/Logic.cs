using CarsMVCProject.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Helpers;

namespace CarsMVCProject
{
    public class Logic
    {
        string connection = ConfigurationManager.ConnectionStrings["MyConnectionString"].ToString();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["MyConnectionString"].ToString());

        #region  ---------- Get Car Logic ------------
        public List<GetCarModel> Get()
        {
            SqlCommand cmd = new SqlCommand("SpGetcars", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            adapter.Fill(dt);
            List<GetCarModel> CarList = new List<GetCarModel>();

            foreach (DataRow dr in dt.Rows)
            {
                CarList.Add(new GetCarModel
                {
                    ID = Convert.ToInt32(dr["ID"]),
                    CarName = dr["CarName"].ToString(),
                    Carprice = Convert.ToDecimal(dr["Carprice"]),
                    CusName = dr["CusName"].ToString(),
                    Email = dr["Email"].ToString(),
                    ManufacturingDate = Convert.ToDateTime(dr["ManufacturingDate"]),
                    CID = Convert.ToInt32(dr["CID"]),
                    UploadImage = dr["UploadImage"].ToString()
                });
            }
            con.Close();
            return CarList;
        }
        #endregion

        #region ------- Insert Car Logic -----------

        public bool AddCar(InsertCarModel add)
        {
            using (SqlConnection con = new SqlConnection(connection))
            {
                using (SqlCommand cmd = new SqlCommand("SpInsertCars", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CarName", add.CarName);
                    //cmd.Parameters.AddWithValue("@CarColour", add.CarColour);
                    cmd.Parameters.AddWithValue("@Carprice", add.Carprice);
                    cmd.Parameters.AddWithValue("@CusName", add.CusName);
                    cmd.Parameters.AddWithValue("@Email", add.Email);
                    cmd.Parameters.AddWithValue("@CID", add.CID);
                    cmd.Parameters.AddWithValue("@UploadImage", add.UploadImage);
                   
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();

                    if (i > 0) { return true; }
                    else { return false; }
                }
            }            
        }
        #endregion

        #region  -------- Update Car Logic ---------

        public bool UpdateCar(UpdateCarModel update)
        {
            SqlCommand cmd = new SqlCommand("SpUpdateCars", con);
            cmd.CommandType= CommandType.StoredProcedure;

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);

            cmd.Parameters.AddWithValue("@ID", update.ID);
            cmd.Parameters.AddWithValue("@CarName", update.CarName);
            //cmd.Parameters.AddWithValue("@CarColour", update.CarColour);
            cmd.Parameters.AddWithValue("@Carprice", update.Carprice);
            cmd.Parameters.AddWithValue("@CusName", update.CusName);
            cmd.Parameters.AddWithValue("@Email", update.Email);
            cmd.Parameters.AddWithValue("@CID",update.CID);

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i > 0) { return true; }
            else { return false; }
        }


        public List<UpdateCarModel>  updateGetcar()
        {
            SqlCommand cmd = new SqlCommand("SpGetcars", con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            con.Open();
            adapter.Fill(dt);
            List<UpdateCarModel> CarList = new List<UpdateCarModel>();

            foreach (DataRow dr in dt.Rows)
            {
                CarList.Add(new UpdateCarModel
                {
                    ID = Convert.ToInt32(dr["ID"]),
                    CarName = dr["CarName"].ToString(),
                    
                    Carprice = Convert.ToDecimal(dr["Carprice"]),
                    CusName = dr["CusName"].ToString(),
                    Email = dr["Email"].ToString(),
                    CID = Convert.ToInt32( dr["CID"])
                });
            }
            con.Close();
            return CarList;
        }
        #endregion

        #region  ----- Delete Car Logic --------

        public bool DeleteCar(int ID)
        {
            SqlCommand cmd = new SqlCommand("SpDeleteCars", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@ID",ID);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();

            if (i > 0) { return true; }
            else { return false; }
        }
        #endregion

        #region ---- Drop Down List Logic ------

        public List<ColoursModel> GetColours() 
        {
            using (SqlConnection con = new SqlConnection(connection))
            {
                using (SqlCommand cmd = new SqlCommand("select * from Colours", con))
                {
                    cmd.CommandType = CommandType.Text;
                    SqlDataAdapter adp = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    con.Open();
                    adp.Fill(dt);
                    List<ColoursModel> colourList = new List<ColoursModel>();
                    foreach (DataRow dr in dt.Rows)
                    {
                        colourList.Add(new ColoursModel
                        {
                            CID = Convert.ToInt32(dr["CID"]),
                            Colours = dr["Colours"].ToString()

                        });
                    }
                    con.Close();
                    return colourList;
                }
                
            }
            
        }
        #endregion

        #region ---- Sending Email Logic ----

        public void SendEMAIL(string address, string subject, string body)
        {
            using (MailMessage mm = new MailMessage())
            {
                mm.From = new MailAddress("corporatehuntofficial@gmail.com");
                mm.To.Add(address);
                mm.Subject = subject;
                mm.Body = body;
                mm.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;

                    NetworkCredential nc = new NetworkCredential("corporatehuntofficial@gmail.com", "tsjs nnlw kpim noqo");
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = nc;
                    smtp.Port = 587;

                    smtp.Send(mm);
                }

            }

        }
        #endregion

        #region ---- Email Validation ----

        public bool EMAIL(string Email, int ID, string Type)
        {
            SqlCommand cmd = new SqlCommand("CheckMail", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@EMAIL", Email);
            cmd.Parameters.AddWithValue("@ID", ID);
            cmd.Parameters.AddWithValue("@Type", Type);
            //SqlCommand cmd = new SqlCommand("SELECT * FROM BOOKS where Email='" + EmailId + "' and ID <> "+id, con);
            con.Open();

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sqlDataAdapter.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        #endregion
    }

}
        