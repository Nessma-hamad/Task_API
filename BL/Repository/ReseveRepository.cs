using BL.Bases;
using DAL.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Repository
{
    public class ReseveRepository : BaseRepository<reserve>
    {
        private DbContext EC_DbContext;
        public ReseveRepository(DbContext EC_DbContext) : base(EC_DbContext)
        {
            this.EC_DbContext = EC_DbContext;
        }
        public List<reserve> GetAllreserves()
        {
            return GetAll().ToList();
        }

        public bool Insertreserve(reserve reserve)
        {
            return Insert(reserve);
        }
        public void Updatereserve(reserve reserve)
        {
            Update(reserve);
        }
        public void Deletereserve(int id)
        {
            Delete(id);
        }
        public bool CheckreserveExists(int id)
        {
            return GetAny(b => b.ID == id);
        }
        public reserve GetreserveById(int id)
        {
            return GetFirstOrDefault(b => b.ID == id);
        }
        public reserve GetreserveByUserId(string userid)
        {
            return GetFirstOrDefault(b => b.UserID == userid);
        }
    }
}


