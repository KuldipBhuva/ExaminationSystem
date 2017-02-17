using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERP_Models.ViewModel;
using ERP_Services;

namespace ERP_System.Controllers
{
    public class ExaminationFormController : Controller
    {
        //
        // GET: /ExaminationForm/

        public ActionResult Index()
        {
            StudentModel objModel = new StudentModel();

            int did = 0;
            int acid = 0;
            if (Session["DID"] != null)
            {
                did = Convert.ToInt32(Session["DID"].ToString());
                acid = Convert.ToInt32(Session["ACID"].ToString());
            }

            CommonService objCService = new CommonService();
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

            return View(objModel);
        }
        [HttpPost]
        public ActionResult Index(StudentModel model)
        {
            StudentModel objModel=new StudentModel();
            List<StudentModel> lstStud = new List<StudentModel>();
            StudentService objService=new StudentService();
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

            return View("Print", objModel);
        }
        public ActionResult Print(StudentModel model)
        {

            return View();
        }
    }
}
