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
    public class ExamController : Controller
    {
        //
        // GET: /Exam/
        ERPEntities Dbcontext = new ERPEntities();
        public ActionResult Index()
        {

            ExamModel objModel = new ExamModel();
            List<ExamModel> lstExam = new List<ExamModel>();
            ExamService objService = new ExamService();


            int did = 0;
            int acid = 0;
            if (Session["DID"] != null)
            {
                did = Convert.ToInt32(Session["DID"].ToString());
                acid = Convert.ToInt32(Session["ACID"].ToString());
            }

            lstExam = objService.getExam(did, acid);
            objModel.ListExam = new List<ExamModel>();
            objModel.ListExam.AddRange(lstExam);

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
        public ActionResult Index(ExamModel model, string cmd)
        {
            ExamService objService = new ExamService();

            int uid = 0;
            int acid = 0;
            int did = 0;
            if (Session["UID"] != null)
            {
                uid = Convert.ToInt32(Session["UID"].ToString());
                acid = Convert.ToInt32(Session["ACID"].ToString());
                did = Convert.ToInt32(Session["DID"].ToString());
            }

            if (model.CenterID == null)
            {
                model.CenterID = model.SchoolID;
            }
            if (cmd == "Save")
            {
                try
                {

                    model.Status = true;
                    model.CreatedBy = uid;
                    model.ACID = acid;
                    model.CreatedDate = System.DateTime.Now;
                    int sid = objService.Insert(model);
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
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            ExamModel objModel = new ExamModel();
            ExamService objService = new ExamService();
            objModel = objService.getByID(id);
            int did = 0;
            int acid = 0;
            if (Session["DID"] != null)
            {
                did = Convert.ToInt32(Session["DID"].ToString());
                acid = Convert.ToInt32(Session["ACID"].ToString());
            }
            List<ExamModel> lstExam = new List<ExamModel>();
            lstExam = objService.getExam(did, acid);
            objModel.ListExam = new List<ExamModel>();
            objModel.ListExam.AddRange(lstExam);

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

            ViewBag.Action = "Edit";

            return View("Index", objModel);
        }
        public ActionResult QRCode(int id, QRModel model)
        {
            ExamService objService = new ExamService();
            ExamModel objModel = new ExamModel();

            int did = 0;
            int acid = 0;
            if (Session["DID"] != null)
            {
                did = Convert.ToInt32(Session["DID"].ToString());
                acid = Convert.ToInt32(Session["ACID"].ToString());
            }

            var lstExam = Dbcontext.ExamMasters.Where(m => m.EID == id).SingleOrDefault();
            var lstQRCode = Dbcontext.QRCodeMasters.Where(m => m.EID == id).ToList();
            if (lstQRCode.Count > 0)
            {
                return RedirectToAction("PrintQRCode", new { @SchoolID = lstExam.SchoolID, @StdID = lstExam.StdID });
            }
            else
            {
                if (lstExam != null)
                {
                    var lstStud = Dbcontext.StudentMasters.Where(m => m.StdID == lstExam.StdID && m.DID == did && m.ACID == acid && m.SchoolID == lstExam.SchoolID).ToList();

                    foreach (var st in lstStud)
                    {
                        var std = Dbcontext.StandardMasters.Where(m => m.StdID == st.StdID).SingleOrDefault();
                        List<SubjectModel> lstSub = new List<SubjectModel>();
                        lstSub = objService.getSubjectByStud(st.StudID);

                        //var sub = Dbcontext.SubjectMasters.Where(m => m.SubID == i.SubID).SingleOrDefault();
                        foreach (var sub in lstSub)
                        {
                            string code = Convert.ToString(st.StudID + "-" + lstExam.EID + "-" + sub.SubID);
                            //string code = "Registration# : " + st.Prefix + "" + st.StudID + ",Roll No. : " + st.RollNo + ",Name : " + st.FirstName + ' ' + st.MiddleName + ' ' + st.LastName + ",Subject : " + sub.Subject + ",Standard : " + std.Standard;
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
                                model.SubID = sub.SubID;
                                model.SchoolID = st.SchoolID;
                                model.EID = id;
                                model.StdID = st.StdID;
                                objService.Insert(model);
                            }
                        }
                    }
                    return RedirectToAction("PrintQRCode", new { @SchoolID = lstExam.SchoolID, @StdID = lstExam.StdID });
                }
                else
                {
                    TempData["AMsg"] = "QR Code not Generated!";
                    return RedirectToAction("Index");
                }
            }
            //return RedirectToAction("Print", new { @SchoolID = model.SchoolID, @StdID = model.StdID });                                                    
        }
        public ActionResult PrintQRCode(int SchoolID, int StdID)
        {
            ExamService objService = new ExamService();
            List<QRModel> lstQR = new List<QRModel>();
            QRModel objModel = new QRModel();

            lstQR = objService.getQRBySChoolNStd(SchoolID, StdID);
            objModel.ListQR = new List<QRModel>();
            objModel.ListQR.AddRange(lstQR);
            return View(objModel);
        }
        public ActionResult BarCode(int id, QRModel model)
        {
            ExamService objService = new ExamService();
            ExamModel objModel = new ExamModel();

            int did = 0;
            int acid = 0;
            if (Session["DID"] != null)
            {
                did = Convert.ToInt32(Session["DID"].ToString());
                acid = Convert.ToInt32(Session["ACID"].ToString());
            }

            var lstExam = Dbcontext.ExamMasters.Where(m => m.EID == id).SingleOrDefault();
            var lstQRCode = Dbcontext.QRCodeMasters.Where(m => m.EID == id).ToList();
            if (lstQRCode.Count > 0)
            {
                return RedirectToAction("PrintQRCode", new { @SchoolID = lstExam.SchoolID, @StdID = lstExam.StdID });
            }
            else
            {
                if (lstExam != null)
                {
                    var lstStud = Dbcontext.StudentMasters.Where(m => m.StdID == lstExam.StdID && m.DID == did && m.ACID == acid && m.SchoolID==lstExam.SchoolID).ToList();

                    foreach (var st in lstStud)
                    {
                        var std = Dbcontext.StandardMasters.Where(m => m.StdID == st.StdID).SingleOrDefault();
                        List<SubjectModel> lstSub = new List<SubjectModel>();
                        lstSub = objService.getSubjectByStud(st.StudID);





                        //var sub = Dbcontext.SubjectMasters.Where(m => m.SubID == i.SubID).SingleOrDefault();
                        foreach (var sub in lstSub)
                        {
                            string barCode = Convert.ToString(st.StudID + "-" + lstExam.EID + "-" + sub.SubID);
                            System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
                            using (Bitmap bitMap = new Bitmap(barCode.Length * 40, 80))
                            {
                                using (Graphics graphics = Graphics.FromImage(bitMap))
                                {
                                    Font oFont = new Font("IDAutomationHC39M", 16);
                                    PointF point = new PointF(2f, 2f);
                                    SolidBrush blackBrush = new SolidBrush(Color.Black);
                                    SolidBrush whiteBrush = new SolidBrush(Color.White);
                                    graphics.FillRectangle(whiteBrush, 0, 0, bitMap.Width, bitMap.Height);
                                    graphics.DrawString("*" + barCode + "*", oFont, blackBrush, point);
                                }
                                using (MemoryStream ms = new MemoryStream())
                                {
                                    bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                                    byte[] byteImage = ms.ToArray();

                                    Convert.ToBase64String(byteImage);
                                    imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                                    model.QRCodeImage = byteImage;
                                }
                                //plBarCode.Controls.Add(imgBarCode);
                                model.QRCode = Convert.ToString(imgBarCode.ImageUrl);
                                model.StudID = st.StudID;
                                model.SubID = sub.SubID;
                                model.SchoolID = st.SchoolID;
                                model.EID = id;
                                model.StdID = st.StdID;
                                objService.Insert(model);
                            }





                            //string code = Convert.ToString(st.StudID + "-" + lstExam.EID + "-" + sub.SubID);
                            ////string code = "Registration# : " + st.Prefix + "" + st.StudID + ",Roll No. : " + st.RollNo + ",Name : " + st.FirstName + ' ' + st.MiddleName + ' ' + st.LastName + ",Subject : " + sub.Subject + ",Standard : " + std.Standard;
                            //QRCodeGenerator qrGenerator = new QRCodeGenerator();
                            //QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(code, QRCodeGenerator.ECCLevel.Q);
                            //System.Web.UI.WebControls.Image imgBarCode = new System.Web.UI.WebControls.Image();
                            //imgBarCode.Height = 150;
                            //imgBarCode.Width = 150;

                            //using (Bitmap bitMap = qrCode.GetGraphic(20))
                            //{
                            //    using (MemoryStream ms = new MemoryStream())
                            //    {
                            //        bitMap.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                            //        byte[] byteImage = ms.ToArray();
                            //        imgBarCode.ImageUrl = "data:image/png;base64," + Convert.ToBase64String(byteImage);
                            //        model.QRCodeImage = byteImage;
                            //    }
                            //    //plBarCode.Controls.Add(imgBarCode);

                            //    model.QRCode = Convert.ToString(imgBarCode.ImageUrl);
                            //    model.StudID = st.StudID;
                            //    model.SubID = sub.SubID;
                            //    model.SchoolID = st.SchoolID;
                            //    model.EID = id;
                            //    model.StdID = st.StdID;
                            //    objService.Insert(model);
                            //}
                        }



                    }
                    return RedirectToAction("PrintQRCode", new { @SchoolID = lstExam.SchoolID, @StdID = lstExam.StdID });
                }
                else
                {
                    TempData["AMsg"] = "Bar Code not Generated!";
                    return RedirectToAction("Index");
                }
            }
            //return RedirectToAction("Print", new { @SchoolID = model.SchoolID, @StdID = model.StdID });                                                    
        }

        public ActionResult ExamForm(int id)
        {
            ExamService objService = new ExamService();
            StudentModel objModel = new StudentModel();

            int did = 0;
            int acid = 0;
            if (Session["DID"] != null)
            {
                did = Convert.ToInt32(Session["DID"].ToString());
                acid = Convert.ToInt32(Session["ACID"].ToString());
            }

            var lstExam = Dbcontext.ExamMasters.Where(m => m.EID == id).SingleOrDefault();
            int std = Convert.ToInt32(lstExam.StdID);
            int school = Convert.ToInt32(lstExam.SchoolID);
            objModel.ExamDetail = lstExam;
            objModel.SchoolDetail = Dbcontext.SchoolMasters.Where(m => m.SchoolID == school).SingleOrDefault();
            List<StudentModel> lstStud = new List<StudentModel>();

            lstStud = objService.getStud(did, acid, std, school);
            objModel.ListStud = new List<StudentModel>();
            objModel.ListStud.AddRange(lstStud);

            return View("PrintExaminationForm", objModel);
        }
        public ActionResult PrintExaminationForm(StudentModel model)
        {
            return View();
        }
        public ActionResult AdmitCard(int id)
        {
            ExamService objService = new ExamService();
            StudentModel objModel = new StudentModel();

            int did = 0;
            int acid = 0;
            if (Session["DID"] != null)
            {
                did = Convert.ToInt32(Session["DID"].ToString());
                acid = Convert.ToInt32(Session["ACID"].ToString());
            }

            var lstExam = Dbcontext.ExamMasters.Where(m => m.EID == id).SingleOrDefault();
            int std = Convert.ToInt32(lstExam.StdID);
            int school = Convert.ToInt32(lstExam.SchoolID);
            objModel.ExamDetail = lstExam;
            objModel.SchoolDetail = Dbcontext.SchoolMasters.Where(m => m.SchoolID == school).SingleOrDefault();
            objModel.AcYearDetail = Dbcontext.AccountingYears.Where(m => m.AYID == acid).SingleOrDefault();
            List<StudentModel> lstStud = new List<StudentModel>();

            lstStud = objService.getStud(did, acid, std, school);
            objModel.ListStud = new List<StudentModel>();
            objModel.ListStud.AddRange(lstStud);

            return View("PrintAdmitCard", objModel);
        }
        public ActionResult PrintAdmitCard()
        {
            return View();
        }
        public ActionResult AtteSheet(int id)
        {
            ExamService objService = new ExamService();
            StudentModel objModel = new StudentModel();

            int did = 0;
            int acid = 0;
            if (Session["DID"] != null)
            {
                did = Convert.ToInt32(Session["DID"].ToString());
                acid = Convert.ToInt32(Session["ACID"].ToString());
            }

            var lstExam = Dbcontext.ExamMasters.Where(m => m.EID == id).SingleOrDefault();
            
            int std = Convert.ToInt32(lstExam.StdID);
            int school = Convert.ToInt32(lstExam.SchoolID);
            objModel.ExamDetail = lstExam;
            objModel.SchoolDetail = Dbcontext.SchoolMasters.Where(m => m.SchoolID == school).SingleOrDefault();
            objModel.AcYearDetail = Dbcontext.AccountingYears.Where(m => m.AYID == acid).SingleOrDefault();
            List<StudentModel> lstStud = new List<StudentModel>();

            lstStud = objService.getStud(did, acid, std, school);
            objModel.ListStud = new List<StudentModel>();
            objModel.ListStud.AddRange(lstStud);

            return View("PrintAtteSheet", objModel);
        }
        public ActionResult PrintAtteSheet()
        {
            return View();
        }
        public ActionResult Result()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Result(ExamModel model)
        {
            ExamService objService = new ExamService();
            int uid = 0;
            if (Session["UID"] != null)
            {
                uid = Convert.ToInt32(Session["UID"].ToString());
            }
            var lstResult = Dbcontext.ResultMasters.Where(m => m.EID == model.EID && m.StudID == model.StudID && m.SubID == model.SubID).SingleOrDefault();

            if (lstResult != null)
            {
                ResultMaster res = Dbcontext.ResultMasters.Where(r => r.EID == model.EID && r.StudID == model.StudID && r.SubID == model.SubID).SingleOrDefault();
                res.Marks = model.Marks;
                res.UpdatedBy = uid;
                res.UpdatedDate = System.DateTime.Now;
                Dbcontext.SaveChanges();
            }
            else
            {
                model.CreatedBy = uid;
                model.CreatedDate = System.DateTime.Now;
                objService.InsertResult(model);
            }
            return RedirectToAction("Result");
        }
        public ActionResult getSub(string code)
        {
            ExamService objService = new ExamService();
            ExamModel objModel = new ExamModel();

            string qr = code;
            int StudID = 0;
            int EID = 0;
            int SubID = 0;

            string[] str = qr.Split('-');
            if (str.Length == 3 && code != "")
            {
                StudID = Convert.ToInt32(str[0]);
                EID = Convert.ToInt32(str[1]);
                SubID = Convert.ToInt32(str[2]);
                var lstSub = Dbcontext.SubjectMasters.Where(m => m.SubID == SubID).SingleOrDefault();
                objModel.ResultDetail = Dbcontext.ResultMasters.Where(m => m.EID == EID && m.StudID == StudID && m.SubID == SubID).SingleOrDefault();
                if (objModel.ResultDetail != null)
                {
                    objModel.Marks = objModel.ResultDetail.Marks;
                }
                objModel.SubDetail = lstSub;
                objModel.EID = EID;
                objModel.StudID = StudID;
                objModel.SubID = SubID;
                objModel.StudDetail = Dbcontext.StudentMasters.Where(m => m.StudID == StudID).SingleOrDefault();
                int stdID = Convert.ToInt32(objModel.StudDetail.StdID);
                objModel.StdDetail = Dbcontext.StandardMasters.Where(m => m.StdID == stdID).SingleOrDefault();

                return PartialView("_Result", objModel);
            }
            else
            {
                TempData["AMsg"] = "QR Code not valid,Try Again.!!";
                return RedirectToAction("Result");
            }

        }
        public ActionResult GenerateResult()
        {
            ExamModel objModel = new ExamModel();
            List<ExamModel> lstExam = new List<ExamModel>();
            ExamService objService = new ExamService();


            int did = 0;
            int acid = 0;
            if (Session["DID"] != null)
            {
                did = Convert.ToInt32(Session["DID"].ToString());
                acid = Convert.ToInt32(Session["ACID"].ToString());
            }

            lstExam = objService.getExam(did, acid);
            objModel.ListExam = new List<ExamModel>();
            objModel.ListExam.AddRange(lstExam);

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

            StudentService objStudSer = new StudentService();
            List<StudentModel> lstStud = new List<StudentModel>();
            lstStud = objStudSer.getStud(did, acid);
            objModel.ListStud = new List<StudentModel>();
            objModel.ListStud.AddRange(lstStud);
            return View(objModel);
        }
        [HttpPost]
        public ActionResult GenerateResult(ExamModel model)
        {
            ExamService objService = new ExamService();
            ExamModel objModel = new ExamModel();
            int StudID = 0;
            int EID = 0;
            StudID = Convert.ToInt32(model.StudID);
            EID = Convert.ToInt32(model.EID);

            List<ResultModel> lstRes = new List<ResultModel>();
            lstRes = objService.getResultOfStud(StudID, EID);
            objModel.ListResult = new List<ResultModel>();
            objModel.ListResult.AddRange(lstRes);
            return View("PrintResult", objModel);
        }
        public ActionResult PrintResult(ExamModel model)
        {

            return View();
        }
        public ActionResult FillStud(int SID, int StdID)
        {
            int did = 0;
            if (Session["DID"] != null)
            {
                did = Convert.ToInt32(Session["DID"].ToString());
            }
            List<StudentModel> lstStud = new List<StudentModel>();
            StudentModel objModel = new StudentModel();
            ExamService objService = new ExamService();
            lstStud = objService.getStudentByStd(SID, StdID);
            objModel.ListStud = new List<StudentModel>();
            objModel.ListStud.AddRange(lstStud);
            return Json(objModel.ListStud, JsonRequestBehavior.AllowGet);
        }
        public ActionResult FillExam(int SID, int StdID)
        {
            int did = 0;
            if (Session["DID"] != null)
            {
                did = Convert.ToInt32(Session["DID"].ToString());
            }
            List<ExamModel> lstStud = new List<ExamModel>();
            ExamModel objModel = new ExamModel();
            ExamService objService = new ExamService();
            lstStud = objService.getExamByStd(SID, StdID);
            objModel.ListExam = new List<ExamModel>();
            objModel.ListExam.AddRange(lstStud);
            return Json(objModel.ListExam, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Schedule(int id)
        {
            List<ExamModel> lstStud = new List<ExamModel>();
            ExamModel objModel = new ExamModel();
            ExamService objService = new ExamService();

            var lstExam = Dbcontext.ExamMasters.Where(m => m.EID == id).SingleOrDefault();
            
            int? stdID = lstExam.StdID;
            int? schoolID=lstExam.SchoolID;
            int eid=lstExam.EID;
            var lstSchool = Dbcontext.SchoolMasters.Where(m => m.SchoolID == schoolID).SingleOrDefault();
            var lstStd = Dbcontext.StandardMasters.Where(m => m.StdID == stdID).SingleOrDefault();
            List<SubjectModel> lstSub = new List<SubjectModel>();
            var lstSchedule = Dbcontext.ExamTrans.Where(m => m.EID == eid && m.SchoolID == schoolID && m.StdID == stdID).ToList();
            if (lstSchedule.Count > 0)
            {
                lstSub = objService.getShedule(id, schoolID, stdID);
                objModel.ListSub = new List<SubjectModel>();
                objModel.ListSub.AddRange(lstSub);
            }
            else
            {
                lstSub = objService.getSubByStdID(stdID);
                objModel.ListSub = new List<SubjectModel>();
                objModel.ListSub.AddRange(lstSub);
            }

            objModel.ExamDetails = lstExam;
            objModel.StdDetail = lstStd;
            objModel.SchoolMastDetail = lstSchool;
            return View(objModel);
        }
        [HttpPost]
        public ActionResult Schedule(ExamModel model,ExamTranModel model1)
        {
            ExamService objService = new ExamService();
              int did = 0;
            int acid = 0;
            if (Session["DID"] != null)
            {
                did = Convert.ToInt32(Session["DID"].ToString());
                acid = Convert.ToInt32(Session["ACID"].ToString());
            }
            int eid = model.ExamDetails.EID;
            int? schoolID = model.ExamDetails.SchoolID;
            int? stdID=model.ExamDetails.StdID;
            var lstSchedule = Dbcontext.ExamTrans.Where(m => m.EID == eid && m.SchoolID == schoolID && m.StdID == stdID).ToList();

            if (lstSchedule.Count > 0)
            {
                foreach (var s in model.ListSub)
                {
                    ExamTran et = Dbcontext.ExamTrans.Where(u => u.ETID == s.ETID).SingleOrDefault();
                    et.Date = s.Date;
                    et.Timing = s.Timing;
                    et.TotalMarks = s.TotalMarks;
                    et.PassingMarks = s.PassingMarks;
                    Dbcontext.SaveChanges();
                }
            }
            else
            {
                model1.EID = eid;
                model1.SchoolID = schoolID;
                model1.StdID = stdID;
                model1.ACID = acid;

                foreach (var i in model.ListSub)
                {
                    model1.SubID = i.SubID;
                    model1.Date = i.Date;
                    model1.Timing = i.Timing;
                    model1.TotalMarks = i.TotalMarks;
                    model1.PassingMarks = i.PassingMarks;
                    objService.InsertSchedule(model1);
                }
            }
            return RedirectToAction("Index");
        }
    }
}
