using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeClock.API.DTOs;

namespace TimeClock.API.Interfaces
{
    public interface ITimeClockService
    {
        Task Add(CreateTimeClockDTO obj);
        Task Update(UpdateTimeClockDTO obj);
        Task Delete(Guid id);
        Task<ResponseTimeClockDTO> Get(Guid id);
        Task<List<ResponseTimeClockDTO>> Get();
    }
}
