using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERP_Models;
using ERP_Models.ViewModel;
using ERP_Services;

namespace ERP_System.Controllers
{
    public class DashboardController : Controller
    {
        //
        // GET: /Dashboard/
        ERPEntities Dbcontext = new ERPEntities();
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ChangePwd(FormCollection All)
        {
            int uid = 0;
            string opwd = All["txtOpwd"];
            string email = All["txtEmail"];
            string npwd = All["txtNpwd"];
            string ncpwd = All["txtNCpwd"];
            if (Session["UID"] != null)
            {
                uid = Convert.ToInt32(Session["UID"].ToString());
            }

            var lst = Dbcontext.UserMasters.Where(m => m.UID == uid).SingleOrDefault();

            if (lst.Password == opwd && lst.Email == email)
            {
                if (npwd == ncpwd)
                {
                    UserMaster um = Dbcontext.UserMasters.Where(m => m.UID == uid).SingleOrDefault();
                    um.Password = ncpwd;
                    Dbcontext.SaveChanges();
                    ////TempData["AMsg"] = "Password changed successfully.";

                    TempData["AMsg"] = "Password changed successfully.";

                    Response.Redirect("/Login/Index");
                    return Content("<script language='javascript' type='text/javascript'>alert('Password changed successfully.');</script>");
                }
                else
                {
                    //TempData["AMsg"] = "Status Not Updated";
                    //return Content("<script language='javascript' type='text/javascript'>alert('New Password and Confirm New Password Not Matched.');</script>");
                    TempData["AMsg"] = "New Password and Confirm New Password Not Matched.";
                    return RedirectToAction("Index");
                }
            }
            else if (lst.Password != opwd)
            {
                //return Content("<script language='javascript' type='text/javascript'>alert('Old Password Not Matched.');</script>");
                TempData["AMsg"] = "Old Password Not Matched.";
                return RedirectToAction("Index");
            }
            else if (lst.Email != email)
            {
                //return Content("<script language='javascript' type='text/javascript'>alert('Email Not Matched.');</script>");
                TempData["AMsg"] = "Email Not Matched.";
                return RedirectToAction("Index");
            }
            else
            {
                //return Content("<script language='javascript' type='text/javascript'>alert('Error occured, Try again!!!');</script>");
                TempData["AMsg"] = "Error occured, Try again!!!";
                return RedirectToAction("Index");
            }

        }
        [HttpPost]
        public ActionResult ChangeAcYr(FormCollection All)
        {
            int uid = 0;
            if (Session["UID"] != null)
            {
                uid = Convert.ToInt32(Session["UID"].ToString());
            }
            int AYID = Convert.ToInt32(All["AcYear"]);
            UserMaster um = Dbcontext.UserMasters.Where(m => m.UID == uid).SingleOrDefault();
            um.AccID = AYID;
            um.UpdatedBy = uid;
            um.UpdatedDate = System.DateTime.Now;
            Dbcontext.SaveChanges();
            TempData["AMsg"] = "Year set successfully.";            
            string currentPath = HttpContext.Request.UrlReferrer.AbsolutePath;

            Response.Redirect(currentPath);
            return Content("<script language='javascript' type='text/javascript'>alert('Year changed successfully.');</script>");
            //return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult ChangeDist(FormCollection All)
        {
            int uid = 0;
            if (Session["UID"] != null)
            {
                uid = Convert.ToInt32(Session["UID"].ToString());
            }
            int did = Convert.ToInt32(All["dist"]);
            UserMaster um = Dbcontext.UserMasters.Where(m => m.UID == uid).SingleOrDefault();
            um.DID = did;
            um.UpdatedBy = uid;
            um.UpdatedDate = System.DateTime.Now;
            Dbcontext.SaveChanges();
            TempData["AMsg"] = "District set successfully.";
            string currentPath = HttpContext.Request.UrlReferrer.AbsolutePath;

            Response.Redirect(currentPath);            
            return Content("<script language='javascript' type='text/javascript'>alert('District changed successfully.');</script>");
            //return RedirectToAction("Index");
        }
        public ActionResult FillCity(int did)
        {          
            List<CityModel> lstCity = new List<CityModel>();
            CityModel objModel = new CityModel();
            CommonService objService = new CommonService();
            lstCity = objService.getCityByDID(did);
            objModel.ListCity = new List<CityModel>();
            objModel.ListCity.AddRange(lstCity);
            return Json(objModel.ListCity, JsonRequestBehavior.AllowGet);
        }
        public ActionResult FillZone(int cid)
        {
            List<ZoneModel> lstZone = new List<ZoneModel>();
            ZoneModel objModel = new ZoneModel();
            CommonService objService = new CommonService();
            lstZone = objService.getZoneByCID(cid);
            objModel.ListZone = new List<ZoneModel>();
            objModel.ListZone.AddRange(lstZone);
            return Json(objModel.ListZone, JsonRequestBehavior.AllowGet);
        }
        public ActionResult FillSchool(int cid,int zid)
        {
            int did = 0;
            if (Session["DID"] != null)
            {
                did = Convert.ToInt32(Session["DID"].ToString());
            }
            List<SchoolModel> lstSchool = new List<SchoolModel>();
            SchoolModel objModel = new SchoolModel();
            CommonService objService = new CommonService();
            lstSchool = objService.getSchoolbyIDs(did,cid,zid);
            objModel.ListSchool = new List<SchoolModel>();
            objModel.ListSchool.AddRange(lstSchool);
            return Json(objModel.ListSchool, JsonRequestBehavior.AllowGet);
        }
    }
}
