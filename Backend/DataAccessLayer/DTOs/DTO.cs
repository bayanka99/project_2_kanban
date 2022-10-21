using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer.DTOs
{
    internal abstract class DTO
    {
        protected DalController dc;
        public long Id { get; set; } = -1;
        protected DTO(DalController controller)
        {
            dc = controller;
        }


    }
}
