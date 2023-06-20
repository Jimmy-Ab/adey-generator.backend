using System;
using Microsoft.AspNetCore.Mvc;
using PrivacyPolicyGeneratorBackend.Application.Features.TelebirrIntegration;
using PrivacyPolicyGeneratorBackend.SharedKernel.Wrapper;

namespace PrivacyPolicyGeneratorBackend.WebApi.Controllers.v1
{
    public class TelebirrPaymentController : BaseApiController<TelebirrPaymentController>
    {
        [HttpPost(nameof(MakeTelebirrPayment))]
        public async Task<ActionResult<Result<TelebirrResponseDto>>> MakeTelebirrPayment(TelebirrPaymentCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

    }
}

