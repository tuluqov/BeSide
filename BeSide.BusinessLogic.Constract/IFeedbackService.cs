using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeSide.Common.Entities;

namespace BeSide.BusinessLogic.Construct
{
    public interface IFeedbackService
    {
        Feedback GetById(int id);
        void Add(Feedback feedback);
        void Delete(int id);
        void Update(Feedback feedback);
        IEnumerable<Feedback> GetAll();
        IEnumerable<Feedback> Find(Func<Feedback, bool> predicate);
    }
}
