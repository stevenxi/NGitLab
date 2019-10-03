﻿using System;
using System.Collections.Generic;
using System.Linq;
using NGitLab.Models;

namespace NGitLab.Mock.Clients
{
    internal sealed class PipelineClient : ClientBase, IPipelineClient
    {
        private int _projectId;
        private IJobClient _jobClient;

        public PipelineClient(ClientContext context, IJobClient jobClient, int projectId)
            : base(context)
        {
            _jobClient = jobClient;
            _projectId = projectId;
        }

        public Models.Pipeline this[int id]
        {
            get
            {
                var project = GetProject(_projectId, ProjectPermission.View);
                var pipeline = project.Pipelines.GetById(id);
                if (pipeline == null)
                    throw new GitLabNotFoundException();

                return pipeline.ToPipelineClient();
            }
        }

        public IEnumerable<PipelineBasic> All 
        {
            get
            {
                var project = GetProject(_projectId, ProjectPermission.View);
                return project.Pipelines.Select(p => p.ToPipelineBasicClient());
            }
        }

        public IEnumerable<Models.Job> AllJobs
        {
            get 
            {
                return _jobClient.GetJobs(JobScopeMask.All);
            }
        }

        public Models.Pipeline Create(string @ref)
        {
            var project = GetProject(_projectId, ProjectPermission.View);
            var pipeline = project.Pipelines.Add(@ref, JobStatus.Running, Context.User);
            return pipeline.ToPipelineClient();
        }

        public Models.Pipeline CreatePipelineWithTrigger(string token, string @ref, Dictionary<string, string> variables)
        {
            throw new NotImplementedException();
        }

        public void Delete(int pipelineId)
        {
            var project = GetProject(_projectId, ProjectPermission.View);
            var pipeline = project.Pipelines.GetById(pipelineId);
            project.Pipelines.Remove(pipeline);
        }

        public Models.Job[] GetJobs(int pipelineId)
        {
            return _jobClient.GetJobs(JobScopeMask.All).Where(p => p.Pipeline.Id == pipelineId).ToArray();
        }

        [Obsolete("Use JobClient.GetJobs() instead")]
        public IEnumerable<Models.Job> GetJobsInProject(JobScope scope)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PipelineBasic> Search(PipelineQuery query)
        {
            throw new NotImplementedException();
        }
    }
}
