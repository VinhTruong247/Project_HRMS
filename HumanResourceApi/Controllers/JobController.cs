using AutoMapper;
using HumanResourceApi.DTO.Experience;
using HumanResourceApi.DTO.Job;
using HumanResourceApi.Models;
using HumanResourceApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HumanResourceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly JobRepo _job;

        public JobController(IMapper mapper, JobRepo job)
        {
            _mapper = mapper;
            _job = job;
        }


        [Authorize]
        [HttpGet("jobs")]
        public IActionResult GetJobs()
        {
            try
            {
                //var jobList = _mapper.Map<List<JobDto>>(_job.GetAll());

                var jobList = _mapper.Map<List<JobDto>>(_job.GetAll());

                return Ok(jobList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpGet("get/job/{jobId}")]
        public IActionResult GetJob(string jobId)
        {
            var job = _mapper.Map<JobDto>(_job.GetAll().Where(j => j.JobId == jobId).FirstOrDefault());
            if (job == null)
            {
                return BadRequest();
            }
            return Ok(job);
        }

        [Authorize]
        [HttpPost("create")]
        public IActionResult Create([FromBody] JobDto job)
        {
            bool validJob = _job.GetAll().Any(j => j.JobId == job.JobId);
            if (validJob)
            {
                return BadRequest();
            }
            var temp = _mapper.Map<Job>(job);
            _job.Add(temp);
            return Ok(temp);
        }

        [Authorize]
        [HttpPut("update/job/{jobId}")]
        public IActionResult UpdateJob(string jobId, [FromBody] UpdateJobDto job)
        {
            if (job == null)
            {
                return BadRequest();
            }
            var validJob = _job.GetAll().Where(j => j.JobId == jobId).FirstOrDefault();
            if (validJob == null)
            {
                return BadRequest();
            }
            _mapper.Map(job, validJob);
            validJob.JobId = jobId;

            _job.Update(validJob);
            return Ok(validJob);
        }

        //[Authorize]
        //[HttpPost("delete")]
        //public IActionResult DeleteJob([FromQuery] string id)
        //{
        //    var job = _mapper.Map<JobDto>(_job.GetAll().Where(j => j.JobId == id).FirstOrDefault());
        //    if (job == null)
        //    {
        //        return BadRequest();
        //    }
        //    var validJob = _job.GetAll().Where(j => j.JobId == id).FirstOrDefault();
        //    _mapper.Map(job, validJob);
        //    validJob.JobId = id;
        //    validJob.Status = "Disable";

        //    _job.Update(validJob);
        //    return Ok(validJob);
        //}
    }
}

