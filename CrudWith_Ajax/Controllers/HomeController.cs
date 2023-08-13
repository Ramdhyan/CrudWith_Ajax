using CrudWith_Ajax.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;

namespace CrudWith_Ajax.Controllers
{
    public class HomeController : Controller
    {
        SqlConnection con = new SqlConnection(@"Data Source=RAM\SQLEXPRESS;initial catalog=AjaxDb;integrated security=true");
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Index(AllCLass all)
        {
            SqlCommand cmd = new SqlCommand("sp_insert",con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name", all.Name);
            cmd.Parameters.AddWithValue("@Email", all.Email);
            con.Open();
           int i= cmd.ExecuteNonQuery();

            return Json("",JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Update(AllCLass all)
        {
            SqlCommand cmd = new SqlCommand("sp_update", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", all.Id);
            cmd.Parameters.AddWithValue("@Name", all.Name);
            cmd.Parameters.AddWithValue("@Email", all.Email);
            con.Open();
            int i = cmd.ExecuteNonQuery();

            return Json(i, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetById(int Id)
        {
            AllCLass allCLass = new AllCLass();
            SqlCommand cmd = new SqlCommand("sp_GetSingle", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", Id);
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                allCLass.Id = Convert.ToInt32(dr["Id"]);
                allCLass.Name = dr["Name"].ToString();
                allCLass.Email = dr["Email"].ToString();
            }

            return Json(allCLass,JsonRequestBehavior.AllowGet);
        }

        public JsonResult Display()
        {
            List<AllCLass> lst = new List<AllCLass>();
            SqlCommand cmd = new SqlCommand("sp_display", con);
            cmd.CommandType = CommandType.StoredProcedure;
            con.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                AllCLass allCLass = new AllCLass();
                allCLass.Id = Convert.ToInt32(dr["Id"]);
                allCLass.Name = dr["Name"].ToString();
                allCLass.Email = dr["Email"].ToString();
                lst.Add(allCLass);
            }
            return Json(lst,JsonRequestBehavior.AllowGet);
        }
        public JsonResult Delete(int Id)
        {
            SqlCommand cmd = new SqlCommand("sp_delete", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Id", Id);
            con.Open();
            int i = cmd.ExecuteNonQuery();
            return Json("", JsonRequestBehavior.AllowGet);
        }
    }
}