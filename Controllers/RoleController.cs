using HospitalMgmtService.Database;
using HospitalMgmtService.Model;
using HospitalMgmtService.RequestResponseModel;
using HospitalMgmtService.RequestResponseModel.RequestModel;
using HospitalMgmtService.RequestResponseModel.ResponseModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using static HospitalMgmtService.RequestResponseModel.Constants.Errors;

namespace HospitalMgmtService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {

        private readonly DBContext _context;
        private SuccessResponse successResponse = new SuccessResponse();
        private FailureResponse failureResponse = new FailureResponse();
        private ErrorResponse errorResponse = new ErrorResponse();
        private readonly ILogger<RoleController> _logger;


        public RoleController(DBContext context, ILogger<RoleController> logger)
        {
            _context = context;
            logger = _logger;
            
        }

       
        [HttpPost("GetRoles")]
        public async Task<IActionResult> GetRoles([FromBody]GetRolesRequest getRoles, [FromHeader(Name = "userId")] int userId)
        {
            try { 
              var roleQuery = _context.Roles.Select(x => new GetAllRolesResponse
              {
                  roleId = x.RoleId,
                roleName =  x.RoleName,
                description = x.Description,
                createdAt= x.CreatedAt,
                createdBy =x.CreatedBy
              }).AsQueryable();


            if (getRoles.searchByType == 1)
            {
                roleQuery = roleQuery.Where(role => role.roleName.Contains(getRoles.searchByValue));
            }
                else
                {
                    errorResponse = new ErrorResponse();
                    errorResponse.message = Constants.Errors.Messages.INVALID_SEARCH_TYPE_MESSAGE;
                    errorResponse.code = Constants.Errors.Codes.INVALID_SEARCHTYPE_ERROR_CODE;
                    failureResponse = new FailureResponse();
                    failureResponse.status = false;
                    failureResponse.error = errorResponse;
                    return BadRequest(failureResponse);
                }
            var roles= await roleQuery.ToListAsync();

                    var successResponse = new SuccessResponse();
                    successResponse.status = true;
                    successResponse.data = roles;
                    return Ok(successResponse);
                
            }
            catch (Exception ex)
            {
                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
                return BadRequest(ex.Message);
            }
        }



        [HttpPost("GetRoleById")]
        public async Task<IActionResult> GetRoleById([FromBody]GetRoleByIdRequest getRoleByIdRequest, [FromHeader(Name = "userId")] int userId)

        {
            try
            {
                List<GetRoleByIdResponse> roles = await _context.Roles.Where(p => p.RoleId == getRoleByIdRequest.RoleId)
                .Include(p => p.RoleFeatures)
                .Select(p => new GetRoleByIdResponse
                {
              roleId = p.RoleId,
              roleName = p.RoleName,
              description = p.Description,
               createdAt =p.CreatedAt,
                    

              createdBy = p.CreatedBy,
              features = p.RoleFeatures.Select(rf => new RequestResponseModel.ResponseModel.FeaturesDTO
              {
                  featureId = rf.Feature.FeatureId,
                  featureName = rf.Feature.FeatureName,
                  add = rf.AddPerm,
                  edit = rf.EditPerm,
                  view = rf.ViewPerm,
                  delete = rf.DeletePerm
              }).ToList()
          })
      .ToListAsync();
                if (roles != null)
                {

                    var successResponse = new SuccessResponse();
                    successResponse.status = true;
                    successResponse.data = roles;
                    return Ok(successResponse);
                }
                else
                {
                    errorResponse = new RequestResponseModel.ErrorResponse();
                    errorResponse.message = Constants.Errors.Messages.ROLE_ID_DOES_NOT_EXISTS_MESSAGE;
                    errorResponse.code = Constants.Errors.Codes.ROLE_ID_DOES_NOT_EXISTS_ERROR_CODE;
                    failureResponse = new RequestResponseModel.FailureResponse();
                    failureResponse.status = false;
                    failureResponse.error = errorResponse;
                    return NotFound(failureResponse);
                }
            }catch(Exception ex)
            {

                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("GetRoleFeatures")]
        public async Task<IActionResult> GetRoleFeatures( [FromHeader(Name = "userId")] int userId)

        {
            try
            {
                var user = _context.Users.Where(x => x.UserId == userId).FirstOrDefault();
                List<RoleFeature> roles = await _context.RoleFeatures.Where(p => p.RoleIdFk == user.RoleIdFk).Include(p => p.Feature)
            .ToListAsync();

                var response = new GetRoleFeaturesResponse
                {
                    features = roles.Select(p => new FeaturesDTO
                    {
                        featureId = p.Feature.FeatureId,
                        featureName = p.Feature.FeatureName,
                        add = p.AddPerm,
                        edit = p.EditPerm,
                        view = p.ViewPerm,
                        delete = p.DeletePerm
                    }).ToList()
                };
                if (response != null)
                {

                    var successResponse = new SuccessResponse();
                    successResponse.status = true;
                    successResponse.data = response;
                    return Ok(successResponse);
                }
                else
                {
                    errorResponse = new RequestResponseModel.ErrorResponse();
                    errorResponse.message = Constants.Errors.Messages.ROLE_ID_DOES_NOT_EXISTS_MESSAGE;
                    errorResponse.code = Constants.Errors.Codes.ROLE_ID_DOES_NOT_EXISTS_ERROR_CODE;
                    failureResponse = new RequestResponseModel.FailureResponse();
                    failureResponse.status = false;
                    failureResponse.error = errorResponse;
                    return NotFound(failureResponse);
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
                return BadRequest(ex.Message);
            }
        }


        [HttpPost("AddRole")]
        public async Task<IActionResult> AddRole([FromBody] AddRoleRequest addRoleRequest, [FromHeader(Name = "userId")] int userId)
        {
            try

            {
                Role role = _context.Roles.Where(r => r.RoleName == addRoleRequest.roleName).FirstOrDefault();
                if (role != null)
                {
                    errorResponse = new RequestResponseModel.ErrorResponse();
                    errorResponse.message = Constants.Errors.Messages.ROLE_EXIST_MESSAGE;
                    errorResponse.code = Constants.Errors.Codes.ROLE_EXIST_ERROR_ERROR_CODE;
                    failureResponse = new RequestResponseModel.FailureResponse();
                    failureResponse.status = false;
                    failureResponse.error = errorResponse;
                    return Conflict(failureResponse);

                }
                if(role == null)
                {

                    Role newRole = new Role();
                    newRole.RoleName = addRoleRequest.roleName;
                    newRole.Description = addRoleRequest.description;
                    newRole.CreatedBy = userId;
                    if (string.IsNullOrWhiteSpace(addRoleRequest.roleName))
                    {
                        errorResponse = new ErrorResponse();
                        errorResponse.message = Messages.ROLE_NAME_CAN_NOT_BE_BLANK;
                        errorResponse.code = Codes.ROLE_NAME_CAN_NOT_BE_BLANK_ERROR_CODE;
                        failureResponse = new FailureResponse();
                        failureResponse.status = false;
                        failureResponse.error = errorResponse;
                        return BadRequest(failureResponse);
                    }
                    _context.Roles.Add(newRole);

                    _context.SaveChanges();

                    role = _context.Roles.Find(newRole.RoleId);

                    foreach (var featureDto in addRoleRequest.features)
                    {
                        Feature feature = _context.Features.Find(featureDto.featureId);

                        if (feature == null)
                          {

                                errorResponse = new RequestResponseModel.ErrorResponse();
                                errorResponse.message = Constants.Errors.Messages.FEATURE_ID_DOES_NOT_EXISTS;
                                errorResponse.code = Constants.Errors.Codes.FEATURE_ID_DOES_NOT_EXISTS_ERROR_CODE;
                                failureResponse = new RequestResponseModel.FailureResponse();
                                failureResponse.status = false;
                                failureResponse.error = errorResponse;
                                return NotFound(failureResponse);
                        }

                        RoleFeature roleFeature = new RoleFeature();
                        roleFeature.Role = role;
                        roleFeature.Feature = feature;
                        roleFeature.AddPerm = featureDto.add;
                        roleFeature.EditPerm = featureDto.edit;
                        roleFeature.ViewPerm = featureDto.view;
                        roleFeature.DeletePerm = featureDto.delete;
                        roleFeature.CreatedBy = userId;
                        _context.RoleFeatures.Add(roleFeature);
                        _context.SaveChanges();
                    }

                    successResponse.status = true;
                    successResponse.data = new
                    {
                        id = newRole.RoleId,
                        message = Constants.SuccessMessages.ROLE_SAVED_MESSAGE,

                    };
                    return Ok(successResponse);
                }
            
                else
                {
                    errorResponse = new RequestResponseModel.ErrorResponse();
                    errorResponse.message = Constants.Errors.Messages.FAILED_TO_SAVE_ROLE_MESSAGE;
                    errorResponse.code = Constants.Errors.Codes.FAILED_TO_SAVE_ROLE_ERROR_CODE;
                    failureResponse = new RequestResponseModel.FailureResponse();
                    failureResponse.status = false;
                    failureResponse.error = errorResponse;
                    return BadRequest(failureResponse);

                }
            }
            catch (Exception ex)
            {
                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
                return BadRequest(ex.Message);

            }
            finally { }

        }

        [HttpPost("UpdateRole")]
        public async Task<IActionResult> UpdateRole([FromBody] UpdateRoleRequest updateRoleRequest, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                var existingRole = _context.Roles.FirstOrDefault(r => r.RoleId == updateRoleRequest.roleId);
                if (existingRole != null)
                {
                    existingRole.RoleName = updateRoleRequest.roleName;
                    existingRole.Description = updateRoleRequest.description;
                    existingRole.UpdatedBy = userId;
                    var existingRoleFeatures = _context.RoleFeatures.Where(rf => rf.RoleIdFk == existingRole.RoleId).ToList();
                    _context.RoleFeatures.RemoveRange(existingRoleFeatures);

                    foreach (var featureDto in updateRoleRequest.features)
                    {
                        Feature feature = _context.Features.Find(featureDto.featureId);
                        if (feature == null)
                        {

                            errorResponse = new RequestResponseModel.ErrorResponse();
                            errorResponse.message = Constants.Errors.Messages.FEATURE_ID_DOES_NOT_EXISTS;
                            errorResponse.code = Constants.Errors.Codes.FEATURE_ID_DOES_NOT_EXISTS_ERROR_CODE;
                            failureResponse = new RequestResponseModel.FailureResponse();
                            failureResponse.status = false;
                            failureResponse.error = errorResponse;
                            return NotFound(failureResponse);
                        }
                        RoleFeature roleFeature = new RoleFeature();
                        roleFeature.Role = existingRole;
                        roleFeature.Feature = feature;
                        roleFeature.AddPerm = featureDto.add;
                        roleFeature.EditPerm = featureDto.edit;
                        roleFeature.ViewPerm = featureDto.view;
                        roleFeature.DeletePerm = featureDto.delete;
                        roleFeature.CreatedBy = userId;

                        _context.RoleFeatures.Attach(roleFeature);
                        _context.SaveChanges();


                        var successResponse = new SuccessResponse();
                        successResponse.data = Constants.SuccessMessages.ROLE_UPDATED_MESSAGE;
                        successResponse.status = true;

                        return Ok(successResponse);
                    }

                }
                    errorResponse = new ErrorResponse();
                    errorResponse.message = Constants.Errors.Messages.FAILED_TO_FETCH_ROLES_DATA_MESSAGE;
                    errorResponse.code = Constants.Errors.Codes.FAILED_TO_FETCH_ROLE_DATA_ERROR_CODE;
                    failureResponse = new RequestResponseModel.FailureResponse();
                    failureResponse.status = false;
                    failureResponse.error = errorResponse;
                    return NotFound(failureResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("DeleteRole")]

        public async Task<IActionResult> DeleteRole([FromBody] DeleteRoleRequest deleteRoleRequest, [FromHeader(Name = "userId")] int userId)
        {
            try
            {
                var deleteRole = _context.Roles.Where(r => r.RoleId == deleteRoleRequest.roleId).FirstOrDefault();

                if (deleteRole == null)
                {

                    errorResponse = new RequestResponseModel.ErrorResponse();

                    errorResponse.message = Constants.Errors.Messages.FAILED_TO_DELETE_ROLE_MESSAGE;
                    errorResponse.code = Constants.Errors.Codes.FAILED_TO_DELETE_ROLE_ERROR_CODE;
                    failureResponse = new RequestResponseModel.FailureResponse();
                    failureResponse.status = false;
                    failureResponse.error = errorResponse;
                    return NotFound(failureResponse);
                }

                _context.Roles.Remove(deleteRole);
                var roleFeaturesToDelete = _context.RoleFeatures.Where(rf => rf.RoleIdFk == deleteRole.RoleId);
                _context.RoleFeatures.RemoveRange(roleFeaturesToDelete);
                _context.SaveChanges();

                var successResponse = new SuccessResponse();
                successResponse.data = Constants.SuccessMessages.ROLE_DELETED_MESSAGE;
                successResponse.status = true;

                return Ok(successResponse);

            }catch(Exception ex)
            {
                _logger.LogError($" Exception Message: {ex.Message}");
                _logger.LogError($" Exception Stack Trace: {ex.StackTrace}");
                return BadRequest(ex.Message);

            }
        }

    }
}


