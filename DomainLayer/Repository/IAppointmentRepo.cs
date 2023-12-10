using DomainLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Repository
{
    public interface IAppointmentRepo
    {
        void add(AppointmentDTO item);
        void Update(int id,TimeSpan item);
        void Delete(int appointmentId, TimeSpan timeToDelete);


    }
}
