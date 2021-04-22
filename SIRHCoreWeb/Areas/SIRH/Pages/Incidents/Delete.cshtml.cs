
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SIRHCoreData;
using SIRHCoreDomain;
using SIRHCoreService;

namespace SIRHCoreWeb.Areas.SIRH.Pages.Incidents
{
    public class DeleteModel : PageModel
         
    {//injection du context
        
        private SIRHcontext db = new SIRHcontext();

        private readonly IIncidentService incidentService;
        [BindProperty]
        public Incident Incident { get; set; }
        public DeleteModel(SIRHcontext context, IncidentService incservice)

        {
            incidentService = incservice;
            db = context;
        }
       
          
        public void OnGet()
        {
        }
    }
}
