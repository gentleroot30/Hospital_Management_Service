using HospitalMgmtService.Database;
using HospitalMgmtService.Model;
using HospitalMgmtService.RequestResponseModel;
using HospitalMgmtService.RequestResponseModel.RequestModel;
using HospitalMgmtService.RequestResponseModel.ResponseModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static HospitalMgmtService.RequestResponseModel.Constants.Errors;

namespace HospitalMgmtService.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class FeatureController : ControllerBase
    {
        private readonly DBContext _context;
        private SuccessResponse successResponse = new SuccessResponse();
        private FailureResponse failureResponse = new FailureResponse();
        private ErrorResponse errorResponse = new ErrorResponse();

        public FeatureController(DBContext context)
        {
            _context = context;
        }
        [HttpPost("GetAllFeatures")]
        public async Task<IActionResult> GetAllFeatures([FromHeader(Name = "userId")] int userId)
        {
            try
            {
                List<GetAllFeaturesResponse> features = await _context.Features.
                    Select(q => new GetAllFeaturesResponse
                    {
                        FeatureId = q.FeatureId,
                        FeatureName = q.FeatureName,
                    }).ToListAsync();

                if (features != null)
                {
                    var successResponse = new SuccessResponse();
                    successResponse.status = true;
                    successResponse.data = features;
                    return Ok(successResponse);
                }

                else
                {
                    errorResponse = new ErrorResponse();
                    errorResponse.message = Messages.FAILED_TO_FETCH_FEATURE_DATA_MESSAGE;
                    errorResponse.code = Codes.FAILED_TO_FETCH_FEATURE_DATA_ERROR_CODE;
                    failureResponse = new FailureResponse();
                    failureResponse.status = false;
                    failureResponse.error = errorResponse;
                    return BadRequest(failureResponse);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}