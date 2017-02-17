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
    public class StudentService
    {
        ERPEntities Dbcontext = new ERPEntities();
        public List<StudentModel> getStud(int did,int acid)
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

                            where sm.ACID == acid && sm.DID == did
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
                                },
                                SchlDetail = new SchoolModel()
                                {
                                    Name=schl.Name
                                }
                            }).ToList();
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<StudentModel> getStudForResult(int did, int acid,int StdID)
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

                            where sm.ACID == acid && sm.DID == did && sm.StdID == StdID
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
                                },
                                SchlDetail = new SchoolModel()
                                {
                                    Name=schl.Name
                                }
                            }).ToList();
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int Insert(StudentModel model)
        {
            try
            {
                Mapper.CreateMap<StudentModel, StudentMaster>();
                StudentMaster objUser = Mapper.Map<StudentMaster>(model);
                Dbcontext.StudentMasters.Add(objUser);
                Dbcontext.SaveChanges();
                int id = Dbcontext.StudentMasters.Max(m => m.StudID);
                return id;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public int InsertSub(SubTranModel model)
        {
            try
            {
                Mapper.CreateMap<SubTranModel, SubTran>();                
                SubTran objUser = Mapper.Map<SubTran>(model);
                Dbcontext.SubTrans.Add(objUser);
                return Dbcontext.SaveChanges();                
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public StudentModel getByID(int id)
        {
            try
            {
                Mapper.CreateMap<StudentMaster, StudentModel>();
                StudentMaster objCityMaster = Dbcontext.StudentMasters.Where(m => m.StudID == id).SingleOrDefault();
                StudentModel objCityItem = Mapper.Map<StudentModel>(objCityMaster);
                return objCityItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int Update(StudentModel model)
        {
            Mapper.CreateMap<StudentModel, StudentMaster>();
            StudentMaster objUser = Dbcontext.StudentMasters.SingleOrDefault(m => m.StudID == model.StudID);
            objUser = Mapper.Map(model, objUser);
            return Dbcontext.SaveChanges();
        }

        public bool DeleteByID(int id)
        {
            try
            {
                Mapper.CreateMap<StudentMaster, StudentModel>();
                StudentMaster objCityMaster = Dbcontext.StudentMasters.Where(m => m.StudID == id).SingleOrDefault();
                Dbcontext.StudentMasters.Remove(objCityMaster);
                Dbcontext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
