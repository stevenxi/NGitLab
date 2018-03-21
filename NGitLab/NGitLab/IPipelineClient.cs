﻿using System.Collections.Generic;
using NGitLab.Models;

namespace NGitLab
{
    public interface IPipelineClient
    {
        /// <summary>
        /// All the pipelines of the project.
        /// </summary>
        IEnumerable<PipelineBasic> All { get; }
        
        /// <summary>
        /// Returns the detail of a single pipeline.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Pipeline this[int id] { get; }

        /// <summary>
        /// Get all jobs in a project
        /// </summary>
        IEnumerable<Job> AllJobs { get; }

        /// <summary>
        /// Get jobs in a project meeting the scope
        /// </summary>
        /// <param name="scope"></param>
        [System.Obsolete("Use JobClient.GetJobs() instead")]
        IEnumerable<Job> GetJobsInProject(JobScope scope);

        /// <summary>
        /// Returns the jobs of a pipeline.
        /// </summary>
        Job[] GetJobs(int pipelineId);

        /// <summary>
        /// Create a new pipeline.
        /// </summary>
        /// <param name="ref">Reference to commit</param>
        Pipeline Create(string @ref);
    }
}