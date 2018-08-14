using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeSide.BusinessLogic.Construct;
using BeSide.Common.Entities;
using BeSide.DataAccess.Construct;

namespace BeSide.BusinessLogic.BusinessComponents
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IUnitOfWork uow;

        public FeedbackService(IUnitOfWork uow)
        {
            this.uow = uow;
        }

        public void Add(Feedback feedback)
        {
            uow.Feedbacks.Create(feedback);
            uow.Save();
        }

        public void Delete(int id)
        {
            uow.Feedbacks.Delete(id);
        }

        public IEnumerable<Feedback> Find(Func<Feedback, bool> predicate)
        {
            var result = uow.Feedbacks.Find(predicate);
            return result;
        }

        public IEnumerable<Feedback> GetAll()
        {
            var result = uow.Feedbacks.GetAll();
            return result;
        }

        public Feedback GetById(int id)
        {
            Feedback feedback = uow.Feedbacks.GetById(id);
            return feedback;
        }

        public IEnumerable<Feedback> GetProviderFeedbacks(string userId)
        {
            var result = uow.Feedbacks.Find(m => m.ProviderProfileId == userId);
            return result;
        }

        public IEnumerable<Feedback> GetUserOrdersFeedbacks(string userId)
        {
            var ordersUser = uow.Orders.Find(m => m.ClientProfileId == userId);
            var result = new List<Feedback>();

            foreach (Order order in ordersUser)
            {
                result.AddRange(order.Feedbacks);
            }

            return result;
        }

        public void Update(Feedback feedback)
        {
            uow.Feedbacks.Update(feedback);
            uow.Save();
        }
    }
}
