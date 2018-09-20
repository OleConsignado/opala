using System.Collections.Generic;

namespace Opala.Infra.NotificationAdapter.Exceptions
{
    public class CoreErrorDto
    {
        public string Key { get; set; }
        public string Message { get; set; }
    }

    public class CoreExceptionDto
    {
        public IEnumerable<CoreErrorDto> Errors { get; set; } = new List<CoreErrorDto>();
        public string Message { get; set; }
        public string TypeName { get; set; }
    }
}
