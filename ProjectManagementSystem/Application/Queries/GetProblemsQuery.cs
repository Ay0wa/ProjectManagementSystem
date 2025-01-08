using ProjectManagementSystem.Domain.Entities;
using System.Collections.Generic;

namespace ProjectManagementSystem.Application.Queries
{
    public class GetProblemsQuery : IQuery<List<Problem>>
    {
    }

    public class GetProblemsQueryHandler : IQueryHandler<GetProblemsQuery, List<Problem>>
    {
        private readonly List<Problem> problems;

        public GetProblemsQueryHandler(List<Problem> Problems)
        {
            this.problems = Problems ?? throw new ArgumentNullException(nameof(Problems));
        }

        public List<Problem> Handle(GetProblemsQuery query)
        {
            return problems;
        }
    }
}