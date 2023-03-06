using Newtonsoft.Json.Converters;

namespace AdministradorWeb.Models
{
    public class JsonDateFormatConverter : IsoDateTimeConverter
    {
        public JsonDateFormatConverter(string format)
        {
            DateTimeFormat = format;
        }
    }
}
