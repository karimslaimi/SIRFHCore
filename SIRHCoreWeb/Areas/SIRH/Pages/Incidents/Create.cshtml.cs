using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SIRHCoreData;
using SIRHCoreDomain;
using SIRHCoreService;

namespace SIRHCoreWeb.Areas.SIRH.Pages.Incidents
{
    public class CreateModel : PageModel
    {
        //injection du context

        private SIRHcontext db = new SIRHcontext();
        private readonly IIncidentService incidentService;
        [BindProperty]
        public Incident Incident { get; set; }
        public CreateModel(SIRHcontext context, IncidentService incservice)

        {
            incidentService = incservice;
            db = context;
        }
        public void OnGet()
        {
        }
    }
}
