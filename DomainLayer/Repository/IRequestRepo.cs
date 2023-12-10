using DomainLayer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Repository
{
    public interface IRequestRepo
    {
        bool HasRequests(string doctorId);
       RequestCountDto NumOfRequests();
        List<RequestPatientDTO> Bookingofboctor( int pageSize, int pageNumber);
        List<RequestDto> BookingofPatient();
        void CancelBooking(int id);

        void ADDRequest(AddRequestDTO item);
        void confirmcheckup(int id);

    }
}
