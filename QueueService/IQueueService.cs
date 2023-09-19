using Microsoft.AspNetCore.Mvc;

namespace TiendaOrdenadoresWebApi.QueueService
{
    public interface IQueueService
    {
        Task<IActionResult?> ConsumeMessage();
    }
}