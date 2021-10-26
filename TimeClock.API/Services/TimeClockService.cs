using AutoMapper;
using TimeClock.API.Entities;
using TimeClock.API.Interfaces;
using TimeClock.API.DTOs;
using System.Threading.Tasks;
using System;
using FluentValidation;
using TimeClock.API.Entities.Validator;
using System.Collections.Generic;
using System.Linq;

namespace TimeClock.API.Services
{
    public class TimeClockService : ITimeClockService
    {
        private readonly IBaseRepository<Entities.TimeClock> _repository;
        private readonly IMapper _mapper;

        public TimeClockService(IBaseRepository<Entities.TimeClock> repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task Add(CreateTimeClockDTO timeClockDTO)
        {
            var splitEntryTime = timeClockDTO._entryTime.Split(':').Select(i => int.Parse(i)).ToArray();
            var splitExitTime = timeClockDTO._exitTime.Split(':').Select(i => int.Parse(i)).ToArray();

            timeClockDTO.EntryTime = new TimeSpan(0, splitEntryTime[0], splitEntryTime[1]);
            timeClockDTO.ExitTime = new TimeSpan(0, splitExitTime[0], splitExitTime[1]);

            var timeClock = _mapper.Map<Entities.TimeClock>(timeClockDTO);
            Validate(timeClock, new TimeClockValidator());
            await _repository.InsertAsync(timeClock);
        }
        public async Task Update(UpdateTimeClockDTO timeClockDTO)
        {
            var timeClock = _mapper.Map<Entities.TimeClock>(timeClockDTO);
            Validate(timeClock, new TimeClockValidator());
            await _repository.UpdateAsync(timeClock);
        }

        public async Task Delete(Guid id) =>
            await _repository.DeleteAsync(await _repository.FindOneAsync(p => p.Id == id));

        public async Task<ResponseTimeClockDTO> Get(Guid id) => 
            _mapper.Map<ResponseTimeClockDTO>(await _repository.FindOneAsync(p => p.Id == id));

        public async Task<List<ResponseTimeClockDTO>> Get() =>
            _mapper.Map<List<ResponseTimeClockDTO>>(await _repository.FindAsync(p => true)).Select(i =>
            {
                i._pointDate = i.PointDate.ToString("dd-MM-yyyy");
                return i;
            }).ToList();

        private void Validate(Entities.TimeClock obj, AbstractValidator<Entities.TimeClock> validator)
        {
            if (obj == null)
                throw new Exception("Object not detected");

            validator.ValidateAndThrow(obj);
        }
    }
}
