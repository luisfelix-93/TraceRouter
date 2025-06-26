using Microsoft.AspNetCore.Mvc;
using TraceRouter.Core.Interfaces;
using TraceRouter.Core.UseCase;
using TraceRouter.WebService.DTO;

namespace TraceRouter.WebService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TraceRouteController : ControllerBase
    {
        #region attributes
        private readonly StartTraceUseCase _startTraceUseCase;
        #endregion
        #region constructor
        public TraceRouteController(StartTraceUseCase startTraceUseCase)
        {
            _startTraceUseCase = startTraceUseCase;
        }
        #endregion
        #region controllers
        [HttpPost("start")]
        public IActionResult Start([FromBody] TraceRouteRequest request)
        {
            if (string.IsNullOrEmpty(request.Host) || string.IsNullOrEmpty(request.ConnectionId))
            {
                return BadRequest("O host e o connectionId são obrigatórios");
            }

            Task.Run(() => _startTraceUseCase.ExecuteAsync(request.Host, request.ConnectionId));

            return Accepted();
        }
        #endregion
    }
}
