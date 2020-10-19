using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace WebMVC.Models
{
    public class EmployeeServices
    {

        public string connect = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;
        private SqlDataAdapter _adapter;
        private DataSet _ds;
        public IList<EmployeeModel> GetEmployeesList()
        {
            IList<EmployeeModel> getEmpList = new List<EmployeeModel>();
            _ds = new DataSet();
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("EmployeeViewOrInsert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", "GetEmpList");
                _adapter = new SqlDataAdapter(cmd);
                _adapter.Fill(_ds);
                if (_ds.Tables.Count > 0)
                {
                    for (int i = 0; i < _ds.Tables[0].Rows.Count; i++)
                    {
                        EmployeeModel obj = new EmployeeModel();
                        obj.Id = Convert.ToInt32(_ds.Tables[0].Rows[i]["id"]);
                        obj.EmpName = Convert.ToString(_ds.Tables[0].Rows[i]["EmpName"]);
                        obj.EmailId = Convert.ToString(_ds.Tables[0].Rows[i]["EmailId"]);
                        obj.MobileNo = Convert.ToString(_ds.Tables[0].Rows[i]["MobileNo"]);
                        getEmpList.Add(obj);
                    }
                }
            }


            return getEmpList;
        }
        //insert
        public void InsertEmployee(EmployeeModel model)
        {
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("EmployeeViewOrInsert");
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", "AddEmployee");
                cmd.Parameters.AddWithValue("@EmpId", model.Id);
                cmd.Parameters.AddWithValue("@EmpName", model.EmpName);
                cmd.Parameters.AddWithValue("@EmpEmailId", model.EmailId);
                cmd.Parameters.AddWithValue("@EmpMobileNO", model.MobileNo);
                cmd.ExecuteNonQuery();
                con.Close();

            }
        }
        //GetByID
        public EmployeeModel GetEditbyId(int ? Id)
        {
            var model = new EmployeeModel();
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("EmployeeViewOrInsert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", "GetEmployeeById");
                cmd.Parameters.AddWithValue("@EmpID", Id);
                _adapter = new SqlDataAdapter(cmd);
                _adapter.Fill(_ds);
                if (_ds.Tables.Count > 0 && _ds.Tables[0].Rows.Count > 0)
                {
                    model.Id = Convert.ToInt32(_ds.Tables[0].Rows[0]["id "]);
                    model.EmpName = Convert.ToString(_ds.Tables[0].Rows[0]["EmpName"]);
                    model.EmailId = Convert.ToString(_ds.Tables[0].Rows[0]["EmailId"]);
                    model.MobileNo = Convert.ToString(_ds.Tables[0].Rows[0]["MobileNo"]);
                }
            }
            return model;
        }
        //Update
        public void UpdateEmp(EmployeeModel model)
        {
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("EmployeeViewOrInsert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", "UpdateEmployee");
                cmd.Parameters.AddWithValue("@EmpName", model.EmpName);
                cmd.Parameters.AddWithValue("@@EmpEmailId", model.EmailId);
                cmd.Parameters.AddWithValue("@EmpMobileNO", model.MobileNo);
                cmd.Parameters.AddWithValue("@EmpId", model.Id);
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        //Delete
        public void DeleteEmp(int ? Id)
        {
            using (SqlConnection con = new SqlConnection(connect))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("EmployeeViewOrInsert", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@mode", "DeleteEmployee");
                cmd.Parameters.AddWithValue("@EmpId", Id);
                cmd.ExecuteNonQuery();
            }
        }

    }
}