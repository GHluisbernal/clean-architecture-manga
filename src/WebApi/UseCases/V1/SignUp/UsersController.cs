namespace WebApi.UseCases.V1.SignUp
{
    using System;
    using System.IdentityModel.Tokens.Jwt;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Security.Claims;
    using System.Text;
    using System.Threading.Tasks;
    using Application.UseCases.SignUp;
    using Domain.Security;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.IdentityModel.Tokens;
    using Modules.Common;
    using ViewModels;

    /// <summary>
    ///     Customers
    ///     <see href="https://github.com/ivanpaulovich/clean-architecture-manga/wiki/Design-Patterns#controller">
    ///         Controller Design Pattern
    ///     </see>
    ///     .
    /// </summary>
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public sealed class UsersController : ControllerBase, IOutputPort
    {
        private IActionResult? _viewModel;

        void IOutputPort.UserAlreadyExists(User user) =>
            this._viewModel = this.Ok(new SignUpCustomerResponse(new UserModel(user)));

        void IOutputPort.Ok(User user) => this._viewModel = this.Ok(new SignUpCustomerResponse(new UserModel(user)));

        /// <summary>
        ///     Sign-up the current user.
        /// </summary>
        /// <response code="200">User already exists.</response>
        /// <response code="201">The user was created successfully.</response>
        /// <response code="400">Bad request.</response>
        /// <param name="userDetails"></param>
        /// <returns>The user.</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SignUpCustomerResponse))]
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(SignUpCustomerResponse))]
        [ApiConventionMethod(typeof(CustomApiConventions), nameof(CustomApiConventions.Post))]
        public async Task<IActionResult> Post(UserDetails userDetails)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,"https://www.googleapis.com/oauth2/v2/userinfo");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", userDetails.Token);

            using HttpClient client = new HttpClient();
            using HttpResponseMessage response = await client.SendAsync(request)
                .ConfigureAwait(false);
            using HttpContent content = response.Content;
            var json = await content.ReadAsStringAsync()
                .ConfigureAwait(false);
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("81536155-7e6e-420f-a7f0-404fbe2a8d93"));
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userDetails.Name)
                }),
                Expires = DateTime.UtcNow.AddYears(2),
                Issuer = "MyWebsite.com",
                Audience = "MyWebsite.com",
                SigningCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var accessToken = tokenHandler.WriteToken(token);

            // Returns the 'access_token' and the type in lower case
            return this.Ok(new { accessToken, token_type="bearer" });
        }
    }
}
