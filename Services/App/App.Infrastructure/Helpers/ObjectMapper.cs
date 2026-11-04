using Newtonsoft.Json;
using System.Reflection;

namespace App.Infrastructure.Helpers
{
    public static class ObjectMapper
    {
        public static void CopyPropertiesTo(this object fromObject, object toObject)
        {
            try
            {
                PropertyInfo[] toObjectProperties = toObject.GetType().GetProperties();
                foreach (PropertyInfo propTo in toObjectProperties)
                {
                    try
                    {
                        PropertyInfo? propFrom = fromObject.GetType().GetProperty(propTo.Name);
                        if (propFrom != null && propFrom.CanWrite)
                            propTo.SetValue(toObject, propFrom.GetValue(fromObject, null), null);
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        //public static void ConvertDecimalPropertiesToPoints(this object fromObject, string points)
        //{
        //    try
        //    {
        //        PropertyInfo[] fromObjectProperties = fromObject.GetType().GetProperties();
        //        foreach (PropertyInfo propTo in fromObjectProperties)
        //        {
        //            if (propTo.PropertyType.Name == "Decimal" || propTo.PropertyType.Name == "Decimal?")
        //            {
        //                PropertyInfo? propFrom = fromObject.GetType().GetProperty(propTo.Name);
        //                propTo.SetValue(fromObject, Convert.ToDecimal(propFrom.GetValue(fromObject, null)).FixDecimalToPoints(points == "" || points == null ? "3" : points), null);

        //            }
        //        }
        //    }
        //    catch (Exception)
        //    {

        //    }
        //}

        public static async Task<string> ImageToBase64(string imagePath)
        {
            try
            {
                byte[] imageBytes;
                using (var httpClient = new HttpClient())
                {
                    imageBytes = await httpClient.GetByteArrayAsync(imagePath);
                }

                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {JsonConvert.SerializeObject(ex)}");
                return null;
            }
        }
    }
}
