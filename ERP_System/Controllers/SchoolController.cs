using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERP_Models;
using ERP_Models.ViewModel;
using ERP_Services;

namespace ERP_System.Controllers
{
    public class SchoolController : Controller
    {
        //
        // GET: /School/

        ERPEntities db = new ERPEntities();
        public ActionResult Index()
        {
            SchoolModel objModel = new SchoolModel();
            SchoolService objService = new SchoolService();
            List<SchoolModel> lstSchool = new List<SchoolModel>();

            int uid = 0;
            int rid = 0;
            int did = 0;
            if (Session["UID"] != null)
            {
                uid = Convert.ToInt32(Session["UID"].ToString());
                rid = Convert.ToInt32(Session["RoleID"].ToString());
                did = Convert.ToInt32(Session["DID"].ToString());
            }

            lstSchool = objService.getSchool(did);
            objModel.ListSchool = new List<SchoolModel>();
            objModel.ListSchool.AddRange(lstSchool);

            CommonService objCService = new CommonService();
            List<CityModel> lstCity = new List<CityModel>();
            lstCity = objCService.getActiveCity(did);
            objModel.ListCity = new List<CityModel>();
            objModel.ListCity.AddRange(lstCity);

            List<ZoneModel> lstZone = new List<ZoneModel>();
            lstZone = objCService.getActiveZone(did);
            objModel.ListZone = new List<ZoneModel>();
            objModel.ListZone.AddRange(lstZone);

            return View(objModel);
        }
        [HttpPost]
        public ActionResult Index(SchoolModel model, string cmd)
        {
            SchoolService objService = new SchoolService();
            HttpPostedFileBase logo = Request.Files["fileLogo"];
            HttpPostedFileBase sign = Request.Files["fileSign"];
            int uid = 0;
            if (Session["UID"] != null)
            {
                uid = Convert.ToInt32(Session["UID"].ToString());
                if (logo != null)
                {
                    var logoPhoto = Path.GetFileName(logo.FileName);

                    if (logoPhoto != "")
                    {
                        var path = Path.Combine(Server.MapPath("/PhotoUpload/Logo"), logoPhoto);
                        logo.SaveAs(path);
                        model.Logo = logoPhoto;
                    }
                }
                if (sign != null)
                {
                    var signFile = Path.GetFileName(sign.FileName);
                    if (signFile != "")
                    {
                        var path = Path.Combine(Server.MapPath("/PhotoUpload/Sign"), signFile);
                        sign.SaveAs(path);
                        model.Sign = signFile;
                    }
                }
                if (cmd == "Save")
                {
                    try
                    {
                        int rid = 0;
                        int did = 0;
                        if (Session["RoleID"] != null)
                        {
                            rid = Convert.ToInt32(Session["RoleID"].ToString());
                            did = Convert.ToInt32(Session["DID"].ToString());
                        }
                        model.DID = did;
                        model.Prefix = "SCH";
                        model.Status = true;
                        model.CreatedBy = uid;
                        model.CreatedDate = System.DateTime.Now;
                        objService.Insert(model);
                        TempData["AMsg"] = "Saved Successfully!";
                    }
                    catch (Exception ex)
                    {
                        TempData["AMsg"] = "Error Occured, " + ex;
                    }
                }
                else
                {
                    try
                    {
                        model.UpdatedBy = uid;
                        model.UpdatedDate = System.DateTime.Now;
                        objService.Update(model);
                        TempData["AMsg"] = "Updated Successfully!";
                    }
                    catch (Exception ex)
                    {
                        TempData["AMsg"] = "Error Occured," + ex;
                    }
                }


            }
            else
            {
                Response.Redirect("/Login/Index");
            }
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            SchoolModel objModel = new SchoolModel();
            SchoolService objService = new SchoolService();
            List<SchoolModel> lstComp = new List<SchoolModel>();
            int uid = 0;
            int rid = 0;
            int did = 0;
            if (Session["UID"] != null)
            {
                uid = Convert.ToInt32(Session["UID"].ToString());
                rid = Convert.ToInt32(Session["RoleID"].ToString());
                did = Convert.ToInt32(Session["DID"].ToString());
            }
            objModel = objService.getByID(id);


            lstComp = objService.getSchool(did);
            objModel.ListSchool = new List<SchoolModel>();
            objModel.ListSchool.AddRange(lstComp);

            CommonService objCService = new CommonService();
            List<CityModel> lstCity = new List<CityModel>();
            lstCity = objCService.getActiveCity(did);
            objModel.ListCity = new List<CityModel>();
            objModel.ListCity.AddRange(lstCity);

            List<ZoneModel> lstZone = new List<ZoneModel>();
            lstZone = objCService.getActiveZone(did);
            objModel.ListZone = new List<ZoneModel>();
            objModel.ListZone.AddRange(lstZone);
            

            //List<AccountingYearModel> lstAcYr = new List<AccountingYearModel>();
            //lstAcYr = objService.getAcYr();
            //objModel.ListAcYr = new List<AccountingYearModel>();
            //objModel.ListAcYr.AddRange(lstAcYr);

            ViewBag.Action = "Edit";
            return View("Index", objModel);
        }

    }
}
