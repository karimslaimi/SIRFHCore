using Microsoft.AspNetCore.Mvc;
using SIRHCoreData.Repositories;
using SIRHCoreDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIRHCoreService
{
   public  interface IIncidentService : IIncidentRepository
    {
        void createIncident(Incident i);
        Incident GetIncident(int id);

        IEnumerable<Incident> GetIncident();
        IEnumerable<Incident> GetUserIncident(string username);
        IEnumerable<Incident> GetAffectedIncident(string username);



        void UpdateIncident(Incident i);
        void DeleteIncident(Incident i);
        void traiterIncident([Bind("id")] Incident i);
        //void attribuerIncident(Incident i);
        //IEnumerable<Incident> GetIncidentByDateCreation(DateTime datecreation);
        //IEnumerable<Incident> GetIncidentByDatereglage(DateTime datereglage);
        //IEnumerable<Incident> GetIncidentBycreepar(string creepar);

    }
}
