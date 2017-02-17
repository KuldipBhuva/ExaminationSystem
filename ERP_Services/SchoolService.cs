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
    public class SchoolService
    {
        ERPEntities Dbcontext = new ERPEntities();
        public List<SchoolModel> getSchool(int did)
        {
            try
            {
                Mapper.CreateMap<SchoolMaster, SchoolModel>();
                List<SchoolMaster> objCityMaster = Dbcontext.SchoolMasters.Where(m=>m.DID==did).ToList();
                List<SchoolModel> objCityItem = Mapper.Map<List<SchoolModel>>(objCityMaster);
                return objCityItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //public List<SchoolModel> getPComp()
        //{
        //    try
        //    {
        //        Mapper.CreateMap<SchoolMaster, SchoolModel>();
        //        List<SchoolMaster> objCityMaster = Dbcontext.SchoolMasters.Where(m => m.Status == true && m.PCompID == null).ToList();
        //        List<SchoolModel> objCityItem = Mapper.Map<List<SchoolModel>>(objCityMaster);
        //        return objCityItem;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public int Insert(SchoolModel model)
        {
            try
            {
                Mapper.CreateMap<SchoolModel, SchoolMaster>();
                SchoolMaster objUser = Mapper.Map<SchoolMaster>(model);
                Dbcontext.SchoolMasters.Add(objUser);
                return Dbcontext.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public SchoolModel getByID(int id)
        {
            try
            {
                Mapper.CreateMap<SchoolMaster, SchoolModel>();
                SchoolMaster objCityMaster = Dbcontext.SchoolMasters.Where(m => m.SchoolID == id).SingleOrDefault();
                SchoolModel objCityItem = Mapper.Map<SchoolModel>(objCityMaster);
                return objCityItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int Update(SchoolModel model)
        {
            Mapper.CreateMap<SchoolModel, SchoolMaster>();
            SchoolMaster objUser = Dbcontext.SchoolMasters.SingleOrDefault(m => m.SchoolID == model.SchoolID);
            objUser = Mapper.Map(model, objUser);
            return Dbcontext.SaveChanges();
        }

        public bool DeleteByID(int id)
        {
            try
            {
                Mapper.CreateMap<SchoolMaster, SchoolModel>();
                SchoolMaster objCityMaster = Dbcontext.SchoolMasters.Where(m => m.SchoolID == id).SingleOrDefault();
                Dbcontext.SchoolMasters.Remove(objCityMaster);
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
