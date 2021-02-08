using Refit;
using SI_Master.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SI_Master.REST
{
    public interface IAuthApi
    {

        [Post("/pos_nav/register")]
        Task<Response<List<object>>> Register([Query] RegisterRequest request);

        [Post("/pos_nav/verify")]
        Task<Response<VerifyResponse>> Verify([Query] VerifyRequest request);
    }
}
