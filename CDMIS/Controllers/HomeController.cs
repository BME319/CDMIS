using System.Collections.Generic;
using System.Web.Mvc;
using CDMIS.Models;
using CDMIS.CommonLibrary;
using CDMIS.ViewModels;
using System.Data;
using CDMIS.ServiceReference;
using System;
using System.IO;

namespace CDMIS.Controllers
{
    [StatisticsTracker]
    public class HomeController : Controller
    {
        public static ServicesSoapClient _ServicesSoapClient = new ServicesSoapClient();

        public ActionResult Index()
        {
            ClinicDataExport model = new ClinicDataExport();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(ClinicDataExport model)
        {
            model.list = getPatientList(model.HealthCoachSelected, model.ModuleSelected);
            return View(model);
        }

      

        public List<PatientExport> getPatientList(string DoctorId, string CategoryCode)
        {
            DataSet docList = _ServicesSoapClient.GetPatientsMatchByDoctorId(DoctorId, CategoryCode);
            List<PatientExport> list = new List<PatientExport>();
            if (docList.Tables.Count > 0)
            {
                foreach (DataRow dr in docList.Tables[0].Rows)
                {
                    PatientExport item = new PatientExport();
                    item.HealthCoachId = dr["DoctorId"].ToString();
                    item.HealthCoachName = dr["DoctorName"].ToString();
                    item.PatientId = dr["PatientId"].ToString();
                    item.PatientName = dr["PatientName"].ToString();
                    item.HUserId = dr["HUserId"].ToString();
                    item.HospitalCode = dr["HospitalCode"].ToString();
                    item.HospitalName = dr["HospitalName"].ToString();
                    item.Description = dr["Description"].ToString();
                    list.Add(item);
                }
            }
            return list;
        }

        public JsonResult ExportPList(string json, string HCId)
        {
            var res = new JsonResult();

            bool flag = true;
            string FileN = HCId + DateTime.Now.ToString("yyyyMMdd");

            FileStream fs1 = new FileStream("D:\\"+FileN+".txt", FileMode.Create, FileAccess.Write);//创建写入文件 
            StreamWriter sw = new StreamWriter(fs1);
            sw.Write(json);//开始写入值

            sw.Close();
            fs1.Close();

            if (flag)
            {
                res.Data = true;
            }
            else
            {
                res.Data = false;
            }
            res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
            return res;
        }

    }
}