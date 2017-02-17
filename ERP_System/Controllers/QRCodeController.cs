using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ERP_Models;
using ERP_Models.ViewModel;
using ERP_Services;
using QRCoder;

namespace ERP_System.Controllers
{
    public class QRCodeController : Controller
    {
        //
        // GET: /QRCode/
        ERPEntities Dbcontext = new ERPEntities();
        public ActionResult Index()
        {
            QRService objService = new QRService();
            QRModel objModel = new QRModel();
            List<QRModel> lstQR = new List<QRModel>();
            CommonService objCService = new CommonService();

            int did = 0;
            int acid = 0;
            if (Session["DID"] != null)
            {
                did = Convert.ToInt32(Session["DID"].ToString());
                acid = Convert.ToInt32(Session["ACID"].ToString());
            }

            lstQR = objService.getQR();
            objModel.ListQR = new List<QRModel>();
            objModel.ListQR.AddRange(lstQR);

            List<SchoolModel> lstSchool = new List<SchoolModel>();
            lstSchool = objCService.getActiveSchool(did);
            objModel.ListSchool = new List<SchoolModel>();
            objModel.ListSchool.AddRange(lstSchool);

            List<StandardModel> lstStd = new List<StandardModel>();
            lstStd = objCService.getActiveStd();
            objModel.ListStd = new List<StandardModel>();
            objModel.ListStd.AddRange(lstStd);

            List<ExamModel> lstExam = new List<ExamModel>();
            lstExam = objCService.getActiveExam();
            objModel.ListExam = new List<ExamModel>();
            objModel.ListExam.AddRange(lstExam);
            return View(objModel);
        }    
        [HttpPost]
        public ActionResult Index(QRModel model)
        {
            QRService objService = new QRService();
            QRModel objModel = new QRModel();

            List<ExamTranModel> lstShedule = new List<ExamTranModel>();
            lstShedule = objService.getExamShedule(Convert.ToInt32(model.EID));
            if (lstShedule.Count() >0)
            {
                int stdID = Convert.ToInt32(lstShedule[0].StdID);
                var lstStud = Dbcontext.StudentMasters.Where(m => m.StdID == stdID).ToList();


                foreach (var i in lstShedule)
                {
                    foreach (var st in lstStud)
                    {
                        var std = Dbcontext.StandardMasters.Where(m => m.StdID == st.StdID).SingleOrDefault();
                        var sub = Dbcontext.SubjectMasters.Where(m => m.SubID == i.SubID).SingleOrDefault();

                        string code = "Registration# : " + st.Prefix + "" + st.StudID + ",Roll No. : " + st.RollNo + ",Name : " + st.FirstName + ' ' + st.MiddleName + ' ' + st.LastName + ",Subject : " + sub.Subject + ",Standard : " + std.Standard;
                        QRCodeGenerator qrGenerator = new QRCodeGenerator();
                        QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.Q);
                        System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
                        imgBarCode.Height = 150;
                        imgBarCode.Width = 150;

                        using (Bitmap bitMap = qrCode.GetGraphic(20))
                        {
                            using (MemoryStream ms = new MemoryStream())
                            {
                                bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                                byte[] byteImage = ms.ToArray();
                                imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                                model.QRCodeImage = byteImage;
                            }
                            //plBarCode.Controls.Add(imgBarCode);

                            model.QRCode = Convert.ToString(imgBarCode.ImageUrl);
                            model.StudID = st.StudID;
                            model.SubID = i.SubID;
                            model.ETID = i.ETID;
                            model.SchoolID = i.SchoolID;
                            model.EID = i.EID;
                            objService.Insert(model);
                        }
                    }
                }
                return RedirectToAction("Print", new { @SchoolID = model.SchoolID, @StdID = model.StdID });
            }
            else
            {
                TempData["AMsg"] = "QR Code not Generated! First you should to arrange shedule.";
                return RedirectToAction("Index");
            }

            
        }
        public ActionResult getExamDetail(int eid)
        {
            int uid = 0;
            int did = 0;
            int rid = 0;
            if (Session["UserId"] != null)
            {
                uid = Convert.ToInt32(Session["UserId"].ToString());
                did = Convert.ToInt32(Session["DID"].ToString());
                rid = Convert.ToInt32(Session["RoleID"].ToString());
            }
            ExamModel objModel = new ExamModel();
            QRService objService = new QRService();
            objModel = objService.getByID(eid);
            
            List<ExamTranModel> lstShedule = new List<ExamTranModel>();
            lstShedule = objService.getExamShedule(eid);
            objModel.ListSchedule = new List<ExamTranModel>();
            objModel.ListSchedule.AddRange(lstShedule);
            //return Json(objModel, JsonRequestBehavior.AllowGet);
            return PartialView("_ExamDetail", objModel);
        }
        public ActionResult Print(int SchoolID, int StdID)
        {
            QRService objService = new QRService();
            List<QRModel> lstQR=new List<QRModel>();
            QRModel objModel=new QRModel();

            lstQR = objService.getQRBySChoolNStd(SchoolID, StdID);
            objModel.ListQR = new List<QRModel>();
            objModel.ListQR.AddRange(lstQR);
            return View(objModel);
        }
    }
}
