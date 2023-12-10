using DomainLayer.DTO;
using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Repository
{
    public interface IPatientRepo
    {

        List<PatientDto> GetAll();
        PatientWithRequestDto GetById(string id);
        int NumsOfPatient();

    }
}
