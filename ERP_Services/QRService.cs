using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ERP_Models;
using ERP_Models.ViewModel;

namespace ERP_Services
{
    public class QRService
    {
        ERPEntities Dbcontext = new ERPEntities();
        public List<QRModel> getQR()
        {
            try
            {
                Mapper.CreateMap<QRCodeMaster, QRModel>();
                List<QRCodeMaster> objCityMaster = Dbcontext.QRCodeMasters.ToList();
                List<QRModel> objCityItem = Mapper.Map<List<QRModel>>(objCityMaster);
                return objCityItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        
        public int Insert(QRModel model)
        {
            try
            {
                Mapper.CreateMap<QRModel, QRCodeMaster>();
                QRCodeMaster objUser = Mapper.Map<QRCodeMaster>(model);
                Dbcontext.QRCodeMasters.Add(objUser);
                return Dbcontext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public ExamModel getByID(int id)
        {
            try
            {
                Mapper.CreateMap<ExamMaster, ExamModel>();
                ExamMaster objCityMaster = Dbcontext.ExamMasters.Where(m => m.EID == id).SingleOrDefault();
                ExamModel objCityItem = Mapper.Map<ExamModel>(objCityMaster);
                return objCityItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ExamTranModel> getExamShedule(int eid)
        {
            try
            {
                Mapper.CreateMap<ExamTran, ExamTranModel>();
                List<ExamTran> objCityMaster = Dbcontext.ExamTrans.Where(m=>m.EID==eid).ToList();
                List<ExamTranModel> objCityItem = Mapper.Map<List<ExamTranModel>>(objCityMaster);
                return objCityItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<QRModel> getQRBySChoolNStd(int sid,int std)
        {
            try
            {
                //Mapper.CreateMap<QRCodeMaster, QRModel>();
                //List<QRCodeMaster> objCityMaster = Dbcontext.QRCodeMasters.ToList();
                //List<QRModel> objCityItem = Mapper.Map<List<QRModel>>(objCityMaster);
                //return objCityItem;

                var data = (from qr in Dbcontext.QRCodeMasters
                            join em in Dbcontext.ExamMasters on qr.EID equals em.EID
                            join sm in Dbcontext.StudentMasters on qr.StudID equals sm.StudID
                            join sub in Dbcontext.SubjectMasters on qr.SubID equals sub.SubID
                            join sc in Dbcontext.SchoolMasters on qr.SchoolID equals sc.SchoolID
                            join st in Dbcontext.StandardMasters on sm.StdID equals st.StdID
                            where qr.SchoolID==sid && sm.StdID==std
                            select new QRModel()
                            {
                                QRID = qr.QRID,
                                ETID = qr.ETID,
                                EID = qr.EID,
                                StudID = qr.StudID,
                                SubID = qr.SubID,
                                SchoolID = qr.SchoolID,
                                QRCodeImage = qr.QRCodeImage,
                                QRCode = qr.QRCode,
                                ExamDetail = new ExamModel()
                                {
                                    Name = em.Name,
                                    Session=em.Session
                                },
                                studDetails = new StudentModel()
                                {
                                    RollNo = sm.RollNo,
                                    StudID = sm.StudID,
                                    Prefix = sm.Prefix,
                                    FirstName = sm.FirstName,
                                    LastName = sm.LastName,
                                    FatherName = sm.FatherName,
                                    Photo=sm.Photo,
                                    Sign=sm.Sign,
                                    Thumb=sm.Thumb
                                },
                                SubDetail = new SubjectModel()
                                {
                                    Subject = sub.Subject
                                },
                                StdDetail = new StandardModel()
                                {
                                    Standard = st.Standard
                                },
                                SchoolDetail = new SchoolModel()
                                {
                                    Name=sc.Name
                                }

                            }).ToList();
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
