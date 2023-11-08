using Business.Business;
using Entities.Entities;
using Utilities;
using Newtonsoft.Json;
using AutoMapper;
using Facade.Mappers;
using Facade.DTOs;

namespace Facade.Logic
{
    public class LoggerFacade
    {
        private LoggerBusiness _logger;
        private IMapper _mapper;
        public LoggerFacade(SettingsHelper settings)
        {
            _logger = new LoggerBusiness(settings);
            _mapper = Mapping.configMapper();
        }

        public void Log(Entidades entidad, Acciones acc, params string[] data)
        {
            Log(entidad, acc, 1, data);
        }

        public void Log(Entidades entidad, Acciones acc, int userId = 1, params string[] data)
        {
            try
            {
                var dateTime = DateTime.Now;
                string prevData = string.Empty;
                string postData = string.Empty;
                if (data.Any())
                {
                    switch (acc)
                    {
                        case Acciones.Create:
                        case Acciones.Update:
                            prevData = data[0];
                            postData = data[1];
                            break;
                        case Acciones.Delete:
                        case Acciones.Login:
                            prevData = data[0];

                            break;
                        case Acciones.ReadAll:
                        case Acciones.ReadOne:
                            prevData = data[0];
                            postData = data[0];
                            break;


                    }
                }
                _logger.Add(new Logger()
                {
                    Accion = acc.ToString(),
                    Entidad = entidad.ToString(),
                    Fecha = DateOnly.FromDateTime(dateTime),
                    Hora = TimeOnly.FromDateTime(dateTime),
                    PrevData = prevData,
                    PostData = postData,
                    UserId = (uint)userId
                });
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IEnumerable<LoggerDTO> GetLogs(DateTime date)
        {
            try
            {
                var logs = _logger.GetLogs(DateOnly.FromDateTime(date));
                Log(Entidades.Region, Acciones.ReadAll, JsonConvert.SerializeObject(logs));
                return _mapper.Map<IEnumerable<Logger>, IEnumerable<LoggerDTO>>(logs);
            }
            catch (Exception)
            {

                throw;
            }
        } 
    }
}
