using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CDMIS.Models;
using System.Web.Mvc;

namespace CDMIS.ViewModels
{
    public class Home
    {
    }

    public class PatientList
    {
        public List<PatientExport> list { get; set; }
        public PatientList()
        {
            list = new List<PatientExport>();
        }
    }


    public class ClinicDataExport
    {
        public string HealthCoachSelected { get; set; }
        public string HealthCoachName { get; set; }
        public string ModuleSelected { get; set; }
        public string TableSelected { get; set; }
        public List<PatientExport> list { get; set; }

        public List<SelectListItem> getTableList()
        {
            return CommonVariables.getTableList();
        }

        public List<SelectListItem> getHealthCoachList()
        {
            return CommonVariables.getHealthCoachList();
        }

        public List<SelectListItem> getModuleList()
        {
            return CommonVariables.GetModuleList();
            
        }
        public ClinicDataExport()
        {
            list = new List<PatientExport>();

        }
    }
}