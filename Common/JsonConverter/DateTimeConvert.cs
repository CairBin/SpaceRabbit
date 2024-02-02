using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Commons.JsonConverter
{
    public class DateTimeConvert:JsonConverter<DateTime>
    {
        private readonly string _fmtString;

        public DateTimeConvert(string fmtString)
        {
            this._fmtString = fmtString;
        }

        public DateTimeConvert()
        {
            this._fmtString = "yyyy-MM-dd HH:mm:ss";
        }

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            string? str = reader.GetString();
            if (str == null)
                return default(DateTime);

            return DateTime.Parse(str);
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(this._fmtString));
        }

    }
}
