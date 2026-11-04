using System.ComponentModel;
using System.Data;
using System.Reflection;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Lib.Core.Extensions.Enums
{
    public static class EnumExtension
    {
        public static List<T> ToListObject<T>(DataTable dataTable) where T : new()
        {
            var dataList = new List<T>();

            //Define what attributes to be read from the class
            const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;

            //Read Attribute Names and Types
            var objFieldNames = typeof(T).GetProperties(flags).Cast<PropertyInfo>().
                Select(item => new
                {
                    Name = item.Name,
                    Type = Nullable.GetUnderlyingType(item.PropertyType) ?? item.PropertyType
                }).ToList();

            //Read Datatable column names and types
            var dtlFieldNames = dataTable.Columns.Cast<DataColumn>().
                Select(item => new {
                    Name = item.ColumnName,
                    Type = item.DataType
                }).ToList();

            foreach (DataRow dataRow in dataTable.AsEnumerable().ToList())
            {
                var classObj = new T();

                foreach (var dtField in dtlFieldNames)
                {
                    PropertyInfo? propertyInfos = classObj?.GetType()?.GetProperty(dtField?.Name!);

                    var field = objFieldNames.Find(x => x.Name == dtField?.Name);

                    if (field != null)
                    {

                        if (propertyInfos?.PropertyType == typeof(DateTime))
                        {
                            propertyInfos.SetValue
                            (classObj, convertToDateTime(dataRow[dtField?.Name!]), null);
                        }
                        else if (propertyInfos?.PropertyType == typeof(int))
                        {
                            propertyInfos.SetValue
                            (classObj, ConvertToInt(dataRow[dtField?.Name!]), null);
                        }
                        else if (propertyInfos?.PropertyType == typeof(long))
                        {
                            propertyInfos.SetValue
                            (classObj, ConvertToLong(dataRow[dtField?.Name!]), null);
                        }
                        else if (propertyInfos?.PropertyType == typeof(decimal))
                        {
                            propertyInfos.SetValue
                            (classObj, ConvertToDecimal(dataRow[dtField?.Name!]), null);
                        }
                        else if (propertyInfos?.PropertyType == typeof(string))
                        {
                            if (dataRow[dtField?.Name!].GetType() == typeof(DateTime))
                            {
                                propertyInfos.SetValue
                                (classObj, ConvertToDateString(dataRow[dtField?.Name!]), null);
                            }
                            else
                            {
                                propertyInfos.SetValue
                                (classObj, ConvertToString(dataRow[dtField?.Name!]), null);
                            }
                        }
                    }
                }
                dataList.Add(classObj);
            }
            return dataList;
        }

        private static string ConvertToDateString(object date)
        {
            if (date == null)
                return string.Empty;

            return (Convert.ToDateTime(date)).ToShortDateString();
        }

        private static string ConvertToString(object value)
        {
            if (value == null)
                return string.Empty;

            return value?.ToString()!;
        }

        private static int ConvertToInt(object value)
        {
            return Convert.ToInt32(value);
        }

        private static long ConvertToLong(object value)
        {
            return Convert.ToInt64(value);
        }

        private static decimal ConvertToDecimal(object value)
        {
            return Convert.ToDecimal(value);
        }

        private static DateTime convertToDateTime(object date)
        {
            return (Convert.ToDateTime(date));
        }


        public static string GetEnumDescription(this Enum enumValue)
        {
            System.Reflection.FieldInfo fieldInfo = enumValue.GetType().GetField(enumValue.ToString())!;

            DescriptionAttribute[] descriptionAttributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return descriptionAttributes.Length > 0 ? descriptionAttributes[0].Description : enumValue.ToString();
        }

        public static TEnum ToEnum<TEnum>(this int val)
        {
            return (TEnum)System.Enum.ToObject(typeof(TEnum), val);
        }

        public static TEnum ToEnum<TEnum>(this long val)
        {
            return (TEnum)System.Enum.ToObject(typeof(TEnum), val);
        }

        public static TEnum ToEnum<TEnum>(this string val)
        {
            return (TEnum)System.Enum.Parse(typeof(TEnum), val);
        }
    }

    public static class DictionaryExtension
    {
        public static void AddOrUpdate(this IDictionary<string, string> dict, string key, string value)
        {
            if (!dict.ContainsKey(key))
                dict.Add(key, value);
            //else
            //    dict[key] = dict.Where(a => a.Key == key).FirstOrDefault().Value + "," + value;
        }
    }
}