using AutoMapper;
using BL.Configration;
using BL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Bases
{
    public class BaseAppService : IDisposable
    {
        protected IUnitOfWork TheUnitOfWork { get; set; }
        protected readonly IMapper mapper = AutoMapperProfile.mapper;
        public BaseAppService(IUnitOfWork theUnitOfWork)
        {
            TheUnitOfWork = theUnitOfWork;
        }
        public void Dispose()
        {
            TheUnitOfWork.Dispose();
        }
    }
}
