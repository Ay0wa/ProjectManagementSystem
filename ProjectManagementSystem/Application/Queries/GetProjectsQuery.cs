using ProjectManagementSystem.Domain.Entities;
using System.Collections.Generic;

namespace ProjectManagementSystem.Application.Queries
{
    public class GetProjectsQuery : IQuery<List<Project>>
    {
    }

    public class GetProjectsQueryHandler : IQueryHandler<GetProjectsQuery, List<Project>>
    {
        private readonly List<Project> projects;

        public GetProjectsQueryHandler(List<Project> projects)
        {
            this.projects = projects ?? throw new ArgumentNullException(nameof(projects));
        }

        public List<Project> Handle(GetProjectsQuery query)
        {
            return projects;
        }
    }
}