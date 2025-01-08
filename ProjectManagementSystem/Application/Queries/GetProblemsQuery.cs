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

        public GetProblemsQueryHandler(List<Problem> problems)
        {
            this.problems = problems ?? throw new ArgumentNullException(nameof(problems));
        }

        public List<Problem> Handle(GetProblemsQuery query)
        {
            return problems;
        }
    }
}