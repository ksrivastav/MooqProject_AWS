using Amazon.CognitoIdentityProvider;
using Amazon.Extensions.CognitoAuthentication;
using IdentityServerCognito.ConfigData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Amazon;

using Amazon.CognitoIdentityProvider.Model;
using System.Reflection;
using Amazon.Runtime.Internal;
using Microsoft.AspNetCore.Http.HttpResults;
namespace IdentityServerCognito.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : Controller
    {
        private readonly AppConfig _cloudConfig;
        private readonly AmazonCognitoIdentityProviderClient _provider;
        private readonly CognitoUserPool _userPool;
      //  private readonly UserContextManager _userManager;
        private readonly HttpContext _httpContext;

        public UserController(IOptions<AppConfig> appConfig,  IHttpContextAccessor httpContextAccessor)  //UserContextManager userManager
        {
            _cloudConfig = appConfig.Value;
            _provider = new AmazonCognitoIdentityProviderClient(
               _cloudConfig.AccessKeyId, _cloudConfig.AccessSecretKey, RegionEndpoint.GetBySystemName(_cloudConfig.Region));
            _userPool = new CognitoUserPool(_cloudConfig.UserPoolId, _cloudConfig.AppClientId, _provider);
        //    _userManager = userManager;
            _httpContext = httpContextAccessor.HttpContext;
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}


        [HttpPost]
        public async Task<IActionResult> NewUser(string email, string password)
        {
            SignUpRequest newUser = new SignUpRequest
            {
                ClientId = _cloudConfig.AppClientId,
                Password = password,
                Username = email
            };
            //newUser.UserAttributes.Add(
            //    new AttributeType { Name = "Email", Value = email }
            //    );
            newUser.UserAttributes.Add(new AttributeType { Name= "given_name", Value= email });
            newUser.UserAttributes.Add(new AttributeType { Name = "email", Value = email });
            newUser.UserAttributes.Add(new AttributeType { Name = "gender", Value = "Male" });
            newUser.UserAttributes.Add(new AttributeType { Name = "phone_number", Value = "+918448800770" });
            var result = await _provider.SignUpAsync(newUser);
            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> ConfirmNewUser(string email, string password, string confirmationCode)
        {
            ConfirmSignUpRequest confirmUserRequest = new ConfirmSignUpRequest
            {
                ClientId = _cloudConfig.AppClientId,
                ConfirmationCode = confirmationCode,
                Username = email

            };
            var response = await _provider.ConfirmSignUpAsync(confirmUserRequest);
            return Ok(response);

        }

        [HttpGet]
        public async Task<IActionResult> AuthenticateUserAsync(string email, string password)
        {
            CognitoUser user = new CognitoUser(email, _cloudConfig.AppClientId, _userPool, _provider);
            InitiateSrpAuthRequest userrequest = new InitiateSrpAuthRequest()
            {
                Password = password,
            };

            AuthFlowResponse authFlowResponse = await user.StartWithSrpAuthAsync(userrequest);
            var result = authFlowResponse.AuthenticationResult;
            return Ok(new Tuple<CognitoUser, AuthenticationResultType>(user, result));
        }

        [HttpGet]
        public async Task<IActionResult> UserLogin(string email, string password)
        {
            var result = await AuthenticateUserAsync(email, password);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> UserLogOut(string accessToken)
        {
            var request = new GlobalSignOutRequest { AccessToken = accessToken};
            var response = await _provider.GlobalSignOutAsync(request);

            //await _userManager.SignOut(_httpContext);
            return Ok("User Signed Out");// new    UserSignOutResponse { UserId = model.UserId, Message = "User Signed Out" };
        }


    }
}
