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
    public class ExamService
    {
        ERPEntities Dbcontext = new ERPEntities();
        public List<ExamModel> getExam(int did, int acid)
        {
            try
            {
                //Mapper.CreateMap<ExamMaster, ExamModel>();
                //List<ExamMaster> objCityMaster = Dbcontext.ExamMasters.Where(m => m.DID == did && m.ACID == acid).ToList();
                //List<ExamModel> objCityItem = Mapper.Map<List<ExamModel>>(objCityMaster);
                //return objCityItem;

                var data = (from em in Dbcontext.ExamMasters
                            join sm in Dbcontext.SchoolMasters on em.SchoolID equals sm.SchoolID
                            join dm in Dbcontext.DistrictMasters on sm.DID equals dm.DID
                            join std in Dbcontext.StandardMasters on em.StdID equals std.StdID
                            where em.ACID == acid && sm.DID == did
                            select new ExamModel()
                            {
                                EID = em.EID,
                                StdID = em.StdID,
                                ACID = em.ACID,
                                SchoolID = em.SchoolID,
                                Session = em.Session,
                                Name = em.Name,
                                Status = em.Status,
                                SchoolDetail = new SchoolModel()
                                {
                                    Name = sm.Name
                                },
                                StandardDetail = new StandardModel()
                                {
                                    Standard = std.Standard
                                }

                            }).ToList();
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int Insert(ExamModel model)
        {
            try
            {
                Mapper.CreateMap<ExamModel, ExamMaster>();
                ExamMaster objUser = Mapper.Map<ExamMaster>(model);
                Dbcontext.ExamMasters.Add(objUser);
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
        public int Update(ExamModel model)
        {
            Mapper.CreateMap<ExamModel, ExamMaster>();
            ExamMaster objUser = Dbcontext.ExamMasters.SingleOrDefault(m => m.EID == model.EID);
            objUser = Mapper.Map(model, objUser);
            return Dbcontext.SaveChanges();
        }
        public List<SubjectModel> getSubjectByStud(int StudID)
        {
            try
            {
                var data = (from st in Dbcontext.SubTrans
                            join sub in Dbcontext.SubjectMasters on st.SubID equals sub.SubID
                            where st.StudID == StudID && st.IsChecked == true
                            select new SubjectModel()
                            {
                                SubID = st.SubID,
                                Subject = sub.Subject,


                            }).ToList();
                return data;
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
        public List<StudentModel> getStud(int did, int acid, int StdID, int SchoolID)
        {
            try
            {
                //Mapper.CreateMap<StudentMaster, StudentModel>();
                //List<StudentMaster> objCityMaster = Dbcontext.StudentMasters.Where(m=>m.DID==did && m.ACID==acid).ToList();
                //List<StudentModel> objCityItem = Mapper.Map<List<StudentModel>>(objCityMaster);
                //return objCityItem;

                var data = (from sm in Dbcontext.StudentMasters
                            join std in Dbcontext.StandardMasters on sm.StdID equals std.StdID
                            join dm in Dbcontext.DistrictMasters on sm.DID equals dm.DID
                            join zm in Dbcontext.ZoneMasters on sm.ZID equals zm.ZID
                            join schl in Dbcontext.SchoolMasters on sm.SchoolID equals schl.SchoolID

                            where sm.ACID == acid && sm.DID == did && sm.SchoolID == SchoolID && sm.StdID == StdID
                            select new StudentModel()
                            {
                                RollNo = sm.RollNo,
                                StudID = sm.StudID,
                                Prefix = sm.Prefix,
                                FirstName = sm.FirstName,
                                MiddleName = sm.MiddleName,
                                LastName = sm.LastName,
                                FatherName = sm.FatherName,
                                MotherName = sm.MotherName,
                                Gender = sm.Gender,
                                DOB = sm.DOB,
                                Photo = sm.Photo,
                                Sign = sm.Sign,
                                Thumb = sm.Thumb,
                                DID = sm.DID,
                                CID = sm.CID,
                                ZID = sm.ZID,
                                SchoolID = sm.SchoolID,
                                StdID = sm.StdID,
                                ACID = sm.ACID,
                                Verified = sm.Verified,
                                Status = sm.Status,
                                CreatedBy = sm.CreatedBy,
                                CreatedDate = sm.CreatedDate,
                                StdDetail = new StandardModel()
                                {
                                    Standard = std.Standard
                                },
                                DistDetail = new DistrictModel()
                                {
                                    Name = dm.Name
                                },
                                ZoneDetail = new ZoneModel()
                                {
                                    Name = zm.Name
                                }
                            }).ToList();
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<QRModel> getQRBySChoolNStd(int sid, int std)
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
                            join center in Dbcontext.SchoolMasters on em.CenterID equals center.SchoolID into cc
                            from c in cc.DefaultIfEmpty()
                            join st in Dbcontext.StandardMasters on sm.StdID equals st.StdID
                            where qr.SchoolID == sid && sm.StdID == std
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
                                    Session = em.Session
                                },
                                studDetails = new StudentModel()
                                {
                                    RollNo = sm.RollNo,
                                    StudID = sm.StudID,
                                    Prefix = sm.Prefix,
                                    FirstName = sm.FirstName,
                                    LastName = sm.LastName,
                                    FatherName = sm.FatherName,
                                    Photo = sm.Photo,
                                    Sign = sm.Sign,
                                    Thumb = sm.Thumb
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
                                    Name = sc.Name
                                },
                                CenterDetail = new SchoolModel()
                                {
                                    Name = (c == null ? string.Empty : c.Name)
                                }

                            }).ToList();
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int InsertResult(ExamModel model)
        {
            try
            {
                Mapper.CreateMap<ExamModel, ResultMaster>();
                ResultMaster objUser = Mapper.Map<ResultMaster>(model);
                Dbcontext.ResultMasters.Add(objUser);
                return Dbcontext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public List<ResultModel> getResultOfStud(int sid, int eid)
        {
            var data = (from rm in Dbcontext.ResultMasters
                        join em in Dbcontext.ExamMasters on rm.EID equals em.EID
                        join sm in Dbcontext.StudentMasters on rm.StudID equals sm.StudID
                        join sub in Dbcontext.SubjectMasters on rm.SubID equals sub.SubID
                        join school in Dbcontext.SchoolMasters on sm.SchoolID equals school.SchoolID
                        join std in Dbcontext.StandardMasters on sm.StdID equals std.StdID
                        join zm in Dbcontext.ZoneMasters on sm.ZID equals zm.ZID
                        join dm in Dbcontext.DistrictMasters on sm.DID equals dm.DID
                        join et in Dbcontext.ExamTrans on rm.SubID equals et.SubID
                        where rm.StudID == (sid == 0 ? rm.StudID : sid) && rm.EID == eid
                        select new ResultModel()
                        {
                            RID = rm.RID,
                            EID = rm.EID,
                            StudID = rm.StudID,
                            SubID = rm.SubID,
                            Marks = rm.Marks,
                            StudDetail = new StudentModel()
                            {
                                FirstName = sm.FirstName,
                                LastName = sm.LastName,
                                MiddleName = sm.MiddleName,
                                FatherName = sm.FatherName,
                                MotherName = sm.MotherName,
                                Prefix = sm.Prefix,
                                StudID = sm.StudID,
                                RollNo = sm.RollNo,
                            },
                            SchoolDetail = new SchoolModel()
                            {
                                Name = school.Name
                            },
                            StdDetail = new StandardModel()
                            {
                                StdID = std.StdID,
                                Standard = std.Standard
                            },
                            ZoneDetail = new ZoneModel()
                            {
                                Name = zm.Name
                            },
                            DistDetail = new DistrictModel()
                            {
                                Name = dm.Name
                            },
                            SubDetail = new SubjectModel()
                            {
                                Subject = sub.Subject
                            },
                            ExamSheduleDetail = new ExamTranModel()
                            {
                                ETID=et.ETID,
                                EID=et.EID,
                                SchoolID=et.SchoolID,
                                SubID=et.SubID,
                                StdID=et.StdID,
                                ACID=et.ACID,
                                Date=et.Date,
                                Timing=et.Timing,
                                TotalMarks=et.TotalMarks,
                                PassingMarks=et.PassingMarks
                            }
                        }).ToList();
            return data;
        }
        public List<StudentModel> getStudentByStd(int schoolID, int StdID)
        {
            try
            {
                Mapper.CreateMap<StudentMaster, StudentModel>();
                List<StudentMaster> objCityMaster = Dbcontext.StudentMasters.Where(m => m.Status == true && m.SchoolID == schoolID && m.StdID == StdID).ToList();
                List<StudentModel> objCityItem = Mapper.Map<List<StudentModel>>(objCityMaster);
                return objCityItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ExamModel> getExamByStd(int schoolID, int StdID)
        {
            try
            {
                Mapper.CreateMap<ExamMaster, ExamModel>();
                List<ExamMaster> objCityMaster = Dbcontext.ExamMasters.Where(m => m.Status == true && m.SchoolID == schoolID && m.StdID == StdID).ToList();
                List<ExamModel> objCityItem = Mapper.Map<List<ExamModel>>(objCityMaster);
                return objCityItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<SubjectModel> getSubByStdID(int? id)
        {
            Mapper.CreateMap<SubjectMaster, SubjectModel>();
            List<SubjectMaster> objCityMaster = Dbcontext.SubjectMasters.Where(m => m.Status == true && m.StdID == id).ToList();
            List<SubjectModel> objCityItem = Mapper.Map<List<SubjectModel>>(objCityMaster);
            return objCityItem;
        }
        public List<SubjectModel> getShedule(int id,int? schoolID,int? StdID)
        {
            //Mapper.CreateMap<ExamTran, SubjectModel>();
            //List<ExamTran> objCityMaster = Dbcontext.ExamTrans.Where(m => m.EID == id && m.SchoolID==schoolID && m.StdID == StdID).ToList();
            //List<SubjectModel> objCityItem = Mapper.Map<List<SubjectModel>>(objCityMaster);
            //return objCityItem;

            var data = (from et in Dbcontext.ExamTrans
                        join sm in Dbcontext.SubjectMasters on et.SubID equals sm.SubID
                        where et.EID == id && et.SchoolID == schoolID && et.StdID == StdID
                        select new SubjectModel()
                        {
                            ETID=et.ETID,
                            Subject=sm.Subject,
                            Date=et.Date,
                            Timing=et.Timing,
                            TotalMarks=et.TotalMarks,
                            PassingMarks=et.PassingMarks,

                        }).ToList();
            return data;
        }
        public int InsertSchedule(ExamTranModel model)
        {
            try
            {
                Mapper.CreateMap<ExamTranModel, ExamTran>();
                ExamTran objUser = Mapper.Map<ExamTran>(model);
                Dbcontext.ExamTrans.Add(objUser);
                return Dbcontext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
