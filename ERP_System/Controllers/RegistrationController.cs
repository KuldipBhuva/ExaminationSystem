using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using ERP_Models;
using ERP_Models.ViewModel;
using ERP_Services;

namespace ERP_System.Controllers
{
    public class RegistrationController : Controller
    {
        //
        // GET: /Registration/
        ERPEntities Dbcontext = new ERPEntities();
        public ActionResult Index()
        {
            StudentModel objModel = new StudentModel();
            List<StudentModel> lstStud = new List<StudentModel>();
            StudentService objService = new StudentService();

            int did = 0;
            int acid = 0;
            if (Session["DID"] != null)
            {
                did = Convert.ToInt32(Session["DID"].ToString());
                acid = Convert.ToInt32(Session["ACID"].ToString());
            }

            lstStud = objService.getStud(did, acid);
            objModel.ListStud = new List<StudentModel>();
            objModel.ListStud.AddRange(lstStud);

            CommonService objCService = new CommonService();
            List<DistrictModel> lstDist = new List<DistrictModel>();
            lstDist = objCService.getActiveDistrict();
            objModel.ListDist = new List<DistrictModel>();
            objModel.ListDist.AddRange(lstDist);

            List<CityModel> lstCity = new List<CityModel>();
            lstCity = objCService.getActiveCity(did);
            objModel.ListCity = new List<CityModel>();
            objModel.ListCity.AddRange(lstCity);

            List<SchoolModel> lstSchool = new List<SchoolModel>();
            lstSchool = objCService.getActiveSchool(did);
            objModel.ListSchool = new List<SchoolModel>();
            objModel.ListSchool.AddRange(lstSchool);

            List<StandardModel> lstStd = new List<StandardModel>();
            lstStd = objCService.getActiveStd();
            objModel.ListStd = new List<StandardModel>();
            objModel.ListStd.AddRange(lstStd);

            List<ZoneModel> lstZone = new List<ZoneModel>();
            lstZone = objCService.getActiveZone(did);
            objModel.ListZone = new List<ZoneModel>();
            objModel.ListZone.AddRange(lstZone);

            List<SubjectModel> lstSub = new List<SubjectModel>();
            lstSub = objCService.getActiveSubject();
            objModel.ListSub = new List<SubjectModel>();
            objModel.ListSub.AddRange(lstSub);
            return View(objModel);
        }
        [HttpPost]
        public ActionResult Index(StudentModel model, SubTranModel model1, string cmd)
        {
            StudentService objService = new StudentService();
            HttpPostedFileBase logo = Request.Files["fileLogo"];
            HttpPostedFileBase sign = Request.Files["fileSign"];
            HttpPostedFileBase thumb = Request.Files["fileThumb"];
            int uid = 0;
            int acid = 0;
            int did = 0;
            if (Session["UID"] != null)
            {
                uid = Convert.ToInt32(Session["UID"].ToString());
                acid = Convert.ToInt32(Session["ACID"].ToString());
                did = Convert.ToInt32(Session["DID"].ToString());
            }
            model.DID = did;

            if (logo != null)
            {
                var logoPhoto = Path.GetFileName(logo.FileName);

                if (logoPhoto != "")
                {
                    var path = Path.Combine(Server.MapPath("/PhotoUpload/Photo"), logoPhoto);
                    logo.SaveAs(path);
                    model.Photo = logoPhoto;
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
            if (thumb != null)
            {
                var thumbPhoto = Path.GetFileName(thumb.FileName);

                if (thumbPhoto != "")
                {
                    var path = Path.Combine(Server.MapPath("/PhotoUpload/Thumb"), thumbPhoto);
                    thumb.SaveAs(path);
                    model.Thumb = thumbPhoto;
                }
            }
            if (cmd == "Save")
            {
                try
                {
                    var lstDist = Dbcontext.DistrictMasters.Where(m => m.DID == model.DID).SingleOrDefault();
                    string name = lstDist.Name.Substring(0, 3);
                    var prefix = Thread.CurrentThread.CurrentCulture.TextInfo.ToUpper(name);
                    model.Prefix = prefix + "/";
                    model.Status = true;
                    model.CreatedBy = uid;
                    model.Verified = false;
                    model.CreatedDate = System.DateTime.Now;
                    model.ACID = acid;
                    int sid = objService.Insert(model);

                    model1.StudID = sid;

                    foreach (var s in model.ListSub)
                    {
                        model1.SubID = s.SubID;
                        model1.IsChecked = s.IsChecked;
                        objService.InsertSub(model1);
                    }
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

                    foreach (var s in model.ListSub)
                    {
                        SubTran sub = Dbcontext.SubTrans.Where(u => u.StudID == model.StudID && u.SubID == s.SubID).SingleOrDefault();
                        if (sub != null)
                        {
                            sub.StudID = model.StudID;
                            sub.SubID = s.SubID;
                            sub.IsChecked = s.IsChecked;
                            Dbcontext.SaveChanges();
                        }
                        else
                        {
                            model1.StudID = model.StudID;
                            model1.SubID = s.SubID;
                            model1.IsChecked = s.IsChecked;
                            objService.InsertSub(model1);
                        }
                    }

                    TempData["AMsg"] = "Updated Successfully!";
                }
                catch (Exception ex)
                {
                    TempData["AMsg"] = "Error Occured," + ex;
                }
            }


            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            StudentModel objModel = new StudentModel();
            StudentService objService = new StudentService();
            List<StudentModel> lstStud = new List<StudentModel>();


            objModel = objService.getByID(id);

            int did = 0;
            int acid = 0;
            if (Session["DID"] != null)
            {
                did = Convert.ToInt32(Session["DID"].ToString());
                acid = Convert.ToInt32(Session["ACID"].ToString());
            }

            lstStud = objService.getStud(did, acid);
            objModel.ListStud = new List<StudentModel>();
            objModel.ListStud.AddRange(lstStud);

            CommonService objCService = new CommonService();
            List<DistrictModel> lstDist = new List<DistrictModel>();
            lstDist = objCService.getActiveDistrict();
            objModel.ListDist = new List<DistrictModel>();
            objModel.ListDist.AddRange(lstDist);

            List<CityModel> lstCity = new List<CityModel>();
            lstCity = objCService.getActiveCity(did);
            objModel.ListCity = new List<CityModel>();
            objModel.ListCity.AddRange(lstCity);

            List<SchoolModel> lstSchool = new List<SchoolModel>();
            lstSchool = objCService.getActiveSchool(did);
            objModel.ListSchool = new List<SchoolModel>();
            objModel.ListSchool.AddRange(lstSchool);

            List<StandardModel> lstStd = new List<StandardModel>();
            lstStd = objCService.getActiveStd();
            objModel.ListStd = new List<StandardModel>();
            objModel.ListStd.AddRange(lstStd);

            List<ZoneModel> lstZone = new List<ZoneModel>();
            lstZone = objCService.getActiveZone(did);
            objModel.ListZone = new List<ZoneModel>();
            objModel.ListZone.AddRange(lstZone);

            List<SubjectModel> lstSub = new List<SubjectModel>();
            lstSub = objCService.getSubjectTran(id);
            objModel.ListSub = new List<SubjectModel>();
            objModel.ListSub.AddRange(lstSub);

            ViewBag.Action = "Edit";
            return View("Index", objModel);
        }
        //[HttpPost]
        //public ActionResult Edit(StudentModel model)
        //{
        //    StudentService objService = new StudentService();
        //    model.UpdatedBy = 1;
        //    model.UpdatedDate = System.DateTime.Now;
        //    objService.Update(model);
        //    return RedirectToAction("Index");
        //}
    }
}
