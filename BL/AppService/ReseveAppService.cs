using BL.Bases;
using BL.DTOs;
using BL.Interfaces;
using DAL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.AppService
{
   public  class ReseveAppService : BaseAppService
    {
        public ReseveAppService(IUnitOfWork theUnitOfWork) : base(theUnitOfWork)
        {

        }
        public List<ReserveDto> GetAllReservs()
        {
            return mapper.Map<List<ReserveDto>>(TheUnitOfWork.Reseve.GetAllreserves());
        }
        public ReserveDto GetReserve(int id)
        {
            if (id < 0)
                throw new ArgumentNullException();
            return mapper.Map<ReserveDto>(TheUnitOfWork.Reseve.GetreserveById(id));
        }
        public ReserveDto GetReserveByUserID(string userid)
        {
           
            return mapper.Map<ReserveDto>(TheUnitOfWork.Reseve.GetreserveByUserId(userid));
        }
        public bool CreateReserve(ReserveDto ReserveDto)
        {
            if (ReserveDto == null)

                throw new ArgumentNullException();



            bool result = false;
            var reserve = mapper.Map<reserve>(ReserveDto);
            if (TheUnitOfWork.Reseve.Insertreserve(reserve))
            {
                result = TheUnitOfWork.Commit() > new int();
            }
            return result;
        }
        public bool DeleteReserve(int id)
        {
            if (id < 0)
                throw new ArgumentNullException();
            bool result = false;
            TheUnitOfWork.Reseve.Deletereserve(id);
            result = TheUnitOfWork.Commit() > new int();
            return result;
        }

        public bool CheckReserveExists(int reserveId)
        {
            var result = TheUnitOfWork.Reseve.CheckreserveExists(reserveId);

            if (result)
            {
                return true;
            }
            return false;
        }
        public bool UpdateReserve(ReserveDto ReserveDto, int id)
        {
            var reseve = TheUnitOfWork.Reseve.GetreserveById(id);
           reseve = mapper.Map<reserve>(ReserveDto);
            TheUnitOfWork.Reseve.Updatereserve(reseve);
            TheUnitOfWork.Commit();

            return true;
        }


    }
}

