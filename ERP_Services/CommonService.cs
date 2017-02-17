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
    public class CommonService
    {
        ERPEntities Dbcontext = new ERPEntities();
        public List<SchoolModel> getActiveSchool(int did)
        {
            try
            {
                Mapper.CreateMap<SchoolMaster, SchoolModel>();
                List<SchoolMaster> objCityMaster = Dbcontext.SchoolMasters.Where(m => m.Status == true && m.DID==did).ToList();
                List<SchoolModel> objCityItem = Mapper.Map<List<SchoolModel>>(objCityMaster);
                return objCityItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<SchoolModel> getSchoolbyIDs(int did,int cid, int zid)
        {
            try
            {
                Mapper.CreateMap<SchoolMaster, SchoolModel>();
                List<SchoolMaster> objCityMaster = Dbcontext.SchoolMasters.Where(m => m.Status == true && m.DID == did && m.CID==cid && m.ZID==zid).ToList();
                List<SchoolModel> objCityItem = Mapper.Map<List<SchoolModel>>(objCityMaster);
                return objCityItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<StandardModel> getActiveStd()
        {
            try
            {
                Mapper.CreateMap<StandardMaster, StandardModel>();
                List<StandardMaster> objCityMaster = Dbcontext.StandardMasters.Where(m => m.Status == true).ToList();
                List<StandardModel> objCityItem = Mapper.Map<List<StandardModel>>(objCityMaster);
                return objCityItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ExamModel> getActiveExam()
        {
            try
            {
                Mapper.CreateMap<ExamMaster, ExamModel>();
                List<ExamMaster> objCityMaster = Dbcontext.ExamMasters.Where(m => m.Status == true).ToList();
                List<ExamModel> objCityItem = Mapper.Map<List<ExamModel>>(objCityMaster);
                return objCityItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<DistrictModel> getActiveDistrict()
        {
            try
            {
                Mapper.CreateMap<DistrictMaster, DistrictModel>();
                List<DistrictMaster> objCityMaster = Dbcontext.DistrictMasters.Where(m=>m.Status==true).ToList();
                List<DistrictModel> objCityItem = Mapper.Map<List<DistrictModel>>(objCityMaster);
                return objCityItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CityModel> getActiveCity(int did)
        {
            try
            {
                Mapper.CreateMap<CityMaster, CityModel>();
                List<CityMaster> objCityMaster = Dbcontext.CityMasters.Where(m => m.Status == true && m.DID==did).ToList();
                List<CityModel> objCityItem = Mapper.Map<List<CityModel>>(objCityMaster);
                return objCityItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CityModel> getCityByDID(int did)
        {
            try
            {
                Mapper.CreateMap<CityMaster, CityModel>();
                List<CityMaster> objCityMaster = Dbcontext.CityMasters.Where(m => m.Status == true && m.DID==did).ToList();
                List<CityModel> objCityItem = Mapper.Map<List<CityModel>>(objCityMaster);
                return objCityItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ZoneModel> getActiveZone(int did)
        {
            try
            {
                Mapper.CreateMap<ZoneMaster, ZoneModel>();
                List<ZoneMaster> objCityMaster = Dbcontext.ZoneMasters.Where(m => m.Status == true && m.DID==did).ToList();
                List<ZoneModel> objCityItem = Mapper.Map<List<ZoneModel>>(objCityMaster);
                return objCityItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<ZoneModel> getZoneByCID(int cid)
        {
            try
            {
                Mapper.CreateMap<ZoneMaster, ZoneModel>();
                List<ZoneMaster> objCityMaster = Dbcontext.ZoneMasters.Where(m => m.Status == true && m.CID == cid).ToList();
                List<ZoneModel> objCityItem = Mapper.Map<List<ZoneModel>>(objCityMaster);
                return objCityItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<SubjectModel> getActiveSubject()
        {
            try
            {
                Mapper.CreateMap<SubjectMaster, SubjectModel>();
                List<SubjectMaster> objCityMaster = Dbcontext.SubjectMasters.Where(m => m.Status == true).ToList();
                List<SubjectModel> objCityItem = Mapper.Map<List<SubjectModel>>(objCityMaster);
                return objCityItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<SubjectModel> getSubjectTran(int StudID)
        {
            try
            {
                //Mapper.CreateMap<SubTran, SubTranModel>();
                //List<SubTran> objCityMaster = Dbcontext.SubTrans.Where(m => m.StudID==StudID).ToList();
                //List<SubjectModel> objCityItem = Mapper.Map<List<SubjectModel>>(objCityMaster);
                //return objCityItem;

                var data=(from st in Dbcontext.SubTrans
                              join sm in Dbcontext.SubjectMasters on st.SubID equals sm.SubID
                              where st.StudID==StudID
                              select new SubjectModel()
                              {
                                  Subject=sm.Subject, 
                                  IsChecked=st.IsChecked,
                                  SubID=st.SubID
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
