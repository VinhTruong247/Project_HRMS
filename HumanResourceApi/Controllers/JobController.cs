using AutoMapper;
using HumanResourceApi.DTO.Experience;
using HumanResourceApi.DTO.Job;
using HumanResourceApi.DTO.Project;
using HumanResourceApi.Helper;
using HumanResourceApi.Models;
using HumanResourceApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace HumanResourceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        public readonly IMapper _mapper;
        public readonly JobRepo _job;
        public Regex jobIdRegex = new Regex(@"^JB\d{6}");

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

                var jobList = _mapper.Map<List<ResponseJobDto>>(_job.GetAll());

                return Ok(jobList);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }

        [Authorize]
        [HttpGet("get/job/{jobId}")]
        public IActionResult GetJob(string jobId)
        {
            try
            {
                if (!jobIdRegex.IsMatch(jobId))
                {
                    return BadRequest("Wrong jobId Format.");
                }
                var job = _mapper.Map<ResponseJobDto>(_job.GetAll().Where(j => j.JobId == jobId).FirstOrDefault());
                if (job == null)
                {
                    return BadRequest("Job ID = " + jobId + " doesn't seem to be found.");
                }
                return Ok(job);
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }

        [Authorize]
        [HttpPost("create")]
        public IActionResult Create([FromBody] JobDto job)
        {
            try
            {
                if (job == null)
                {
                    return BadRequest("Some input information is null");
                }
                int count = _job.GetAll().Count() + 1;
                var jobId = "RP" + count.ToString().PadLeft(6, '0');
                var temp = _mapper.Map<Job>(job);
                temp.JobId = jobId;
                _job.Add(temp);
                return Ok(_mapper.Map<ResponseJobDto>(temp));
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }

        [Authorize]
        [HttpPut("update/job/{jobId}")]
        public IActionResult UpdateJob(string jobId, [FromBody] UpdateJobDto job)
        {
            try
            {
                if (job == null)
                {
                    return BadRequest("Some input information is null");
                }
                if (!jobIdRegex.IsMatch(jobId))
                {
                    return BadRequest("Wrong jobId Format.");
                }
                var validJob = _job.GetAll().Where(j => j.JobId == jobId).FirstOrDefault();
                if (validJob == null)
                {
                    return BadRequest();
                }
                _mapper.Map(job, validJob);
                validJob.JobId = jobId;

                _job.Update(validJob);
                return Ok(_mapper.Map<ResponseJobDto>(validJob));
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }

        [HttpGet("jobs/search/{keyword}")]
        public IActionResult FindProject(string keyword)
        {
            try
            {
                var resultList = _mapper.Map<List<ResponseJobDto>>(_job.GetAll().Where(j => RemoveVietnameseSign.RemoveSign(j.JobTitle).ToLower().Contains(keyword.ToLower())));
                if (resultList == null)
                {
                    return BadRequest("No active job(s) found");
                }
                return Ok(resultList.OrderBy(j => j.JobId));
            }
            catch (Exception ex)
            {
                return BadRequest("Something went wrong: " + ex.Message);
            }
        }
    }
}

