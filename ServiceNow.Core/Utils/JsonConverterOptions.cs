using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SNow.Core.Utils
{
    public static class JsonConverterOptions
    {
        private static JsonSerializerOptions _options = new JsonSerializerOptions()
        {
            //Allows to map Id from id
            PropertyNameCaseInsensitive = true,
            //Allows to map  "25" to 25
            NumberHandling = JsonNumberHandling.AllowReadingFromString,
            //Allows float numbers ??,            
        };


        /// <summary>
        /// Generic Enum Converter
        /// </summary>
        /// <typeparam name="T">A Custom Enum</typeparam>
        public class CustomEnumConverter<T> : JsonConverter<T> where T : Enum
        {
            public override T Read(ref Utf8JsonReader reader, Type typeToConvert,
                JsonSerializerOptions options)
            {
                return (T)Enum.Parse(typeof(T), reader.GetString());
            }

            public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
            {
                Enum test = (Enum)Enum.Parse(typeof(T), value.ToString());
                writer.WriteNumberValue(Convert.ToInt32(test));
            }
        }

        public class NullableIntOption : JsonConverter<int?>
        {
            public override int? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                var value = reader.GetString();
                if (String.IsNullOrEmpty(value))
                    return null;

                int.TryParse(value, out var result);
                return result;
            }

            public override void Write(Utf8JsonWriter writer, int? value, JsonSerializerOptions options)
            {
                writer.WriteStringValue(value.ToString());
            }
        }

        public class NullableFloatOption : JsonConverter<float?>
        {
            public override float? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                var value = reader.GetString();
                if (String.IsNullOrEmpty(value))
                    return null;

                float.TryParse(value, out var result);
                return result;
            }

            public override void Write(Utf8JsonWriter writer, float? value, JsonSerializerOptions options)
            {
                writer.WriteStringValue(value.ToString());
            }
        }

        public class NullableGuidOption : JsonConverter<Guid?>
        {
            /// <summary>
            /// Handle ServiceNow Guid serialization, 
            /// if it is an object like
            /// <code>
            /// business_service": {
            ///    "link": "https://dev.service-now.com/api/now/table/cmdb_ci_service/ce02b8461b88f01030cb635bbc4bcb6d",
            ///    "value": "ce02b8461b88f01030cb635bbc4bcb6d"
            ///  }
            /// </code>
            /// it will read until it reaches "ce02b8461b88f01030cb635bbc4bcb6d"
            /// if it is an string 
            /// it will read the value
            /// </summary>
            /// <param name="reader"></param>
            /// <param name="typeToConvert"></param>
            /// <param name="options"></param>
            /// <returns></returns>
            public NullableGuidOption()
            {

            }

            public override Guid? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                string id = "not found";
                if (reader.TokenType == JsonTokenType.StartObject)
                {
                    //PropertyName link key
                    reader.Read();

                    //String link
                    reader.Read();

                    //PropertyName value key
                    reader.Read();

                    //String value
                    reader.Read();

                    id = reader.GetString();

                    //to reach enObject
                    reader.Read();

                    if (string.IsNullOrEmpty(id))
                        return Guid.Empty;

                    return new Guid(id);

                }

                var value = reader.GetString();

                Guid.TryParse(value, out var result);

                return result;
            }

            public override void Write(Utf8JsonWriter writer, Guid? value, JsonSerializerOptions options)
            {
                if (value == null)
                    writer.WriteStringValue("");
                else
                    writer.WriteStringValue(value?.ToString("N"));
            }
        }

        public class GuidOption : JsonConverter<Guid>
        {
            /// <summary>
            /// Handle ServiceNow Guid serialization, 
            /// if it is an object like
            /// <code>
            /// business_service": {
            ///    "link": "https://dev.service-now.com/api/now/table/cmdb_ci_service/ce02b8461b88f01030cb635bbc4bcb6d",
            ///    "value": "ce02b8461b88f01030cb635bbc4bcb6d"
            ///  }
            /// </code>
            /// it will read until it reaches "ce02b8461b88f01030cb635bbc4bcb6d"
            /// if it is an string 
            /// it will read the value
            /// </summary>
            /// <param name="reader"></param>
            /// <param name="typeToConvert"></param>
            /// <param name="options"></param>
            /// <returns></returns>
            public GuidOption()
            {

            }

            public override Guid Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                string id = "not found";
                if (reader.TokenType == JsonTokenType.StartObject)
                {
                    //PropertyName link key
                    reader.Read();

                    //String link
                    reader.Read();

                    //PropertyName value key
                    reader.Read();

                    //String value
                    reader.Read();

                    id = reader.GetString();

                    //to reach enObject
                    reader.Read();

                    if (string.IsNullOrEmpty(id))
                        return Guid.Empty;

                    return new Guid(id);

                }

                var value = reader.GetString();

                Guid.TryParse(value, out var result);

                return result;
            }

            public override void Write(Utf8JsonWriter writer, Guid value, JsonSerializerOptions options)
            {
                if (value == null)
                    writer.WriteStringValue("");
                else
                    writer.WriteStringValue(value.ToString("N"));
            }
        }

        public class NullableDateOption : JsonConverter<DateTime?>
        {
            public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                var value = reader.GetString();
                if(String.IsNullOrEmpty(value))
                    return null;

                return DateTime.ParseExact(value, "yyyy-MM-dd HH:mm:ss", null)
;
            }

            public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
            {
                writer.WriteStringValue(value?.ToString("yyyy-MM-dd HH:mm:ss"));
            }
        }

        public class DateOption : JsonConverter<DateTime>
        {
            public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                var value = reader.GetString();
                if (String.IsNullOrEmpty(value))
                    return new DateTime(); 

                return DateTime.ParseExact(value, "yyyy-MM-dd HH:mm:ss", null);;
            }

            public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
            {
                writer.WriteStringValue(value.ToString("yyyy-MM-dd HH:mm:ss"));
            }
        }

        public enum BooleanFormat
        {
            Boolean,
            Text,
            Number
        }

        /// <summary>
        /// Handle Boolean Conversion for:
        /// true, yes and 1 values as TRUE
        /// false, no and 0 values as FALSE
        /// </summary>
        public class CustomBooleanConverter : JsonConverter<bool>
        {
            BooleanFormat _format = BooleanFormat.Text;

            /// <summary>
            /// Handle how boolean are (des)serialized.
            /// </summary>
            /// <param name="format">Specifies how the boolean should be formatted, default is text</param>
            public CustomBooleanConverter(BooleanFormat format = BooleanFormat.Text)
            {
                _format = format;
            }
            public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            {
                switch (reader.TokenType)
                {
                    case JsonTokenType.String:
                        string value = reader.GetString();
                        string chkValue = value.ToLower();
                        if (chkValue.Equals("true") || chkValue.Equals("yes") || chkValue.Equals("1"))
                        {
                            return true;
                        }
                        if (value.ToLower().Equals("false") || chkValue.Equals("no") || chkValue.Equals("0"))
                        {
                            return false;
                        }
                        break;
                    case JsonTokenType.Number:
                        if (reader.GetInt32() == 1) return true;
                        return false;
                    case JsonTokenType.True:
                        return true;
                    case JsonTokenType.False:
                        return false;
                    default:
                        throw new InvalidCastException($"Trying to convert JsonTokenType {reader.TokenType} to Boolean!");
                }
                return false;
            }

            public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options)
            {
                switch (value)
                {
                    case true:
                        switch (_format)
                        {
                            case BooleanFormat.Boolean:
                                writer.WriteBooleanValue(true);
                                break;
                            case BooleanFormat.Text:
                                writer.WriteStringValue("true");
                                break;
                            case BooleanFormat.Number:
                                writer.WriteNumberValue(1);
                                break;
                            default:
                                break;
                        }

                        break;
                    case false:
                        switch (_format)
                        {
                            case BooleanFormat.Boolean:
                                writer.WriteBooleanValue(false);
                                break;
                            case BooleanFormat.Text:
                                writer.WriteStringValue("false");
                                break;
                            case BooleanFormat.Number:
                                writer.WriteNumberValue(0);
                                break;
                            default:
                                break;
                        }
                        break;
                }
            }
        }

        public static void ConfigureCustomSerializers(JsonConverter[] customConverters)
        {
            _options.Converters.Add(new GuidOption());
            _options.Converters.Add(new NullableGuidOption());

            _options.Converters.Add(new NullableIntOption());
            _options.Converters.Add(new NullableFloatOption());

            _options.Converters.Add(new DateOption());
            _options.Converters.Add(new NullableDateOption());

            _options.Converters.Add(new CustomBooleanConverter());
            foreach (JsonConverter converter in customConverters)
            {
                _options.Converters.Add(converter);
            }            
        }

        public static JsonSerializerOptions CustomSerializationOptions
        {
            get
            {
                //there should be at least Guid and Date converters
                if (_options.Converters.Count == 0)
                    ConfigureCustomSerializers(new JsonConverter[] { });

                return _options;
            }
        }
    }
}
