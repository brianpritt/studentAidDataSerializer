using System;
using System.Reflection;
using System.Globalization;

namespace StudentAidData
{
    public class BaseModel
    {
        public static void SetProperty(object currentObject, Type objectType ,string key, string value)
        {
            string format = "MM/dd/yyyy";
            CultureInfo provider = CultureInfo.InvariantCulture;
            PropertyInfo? propertyInfo = objectType.GetProperty(String.Concat(key.Where(c => !Char.IsWhiteSpace(c))), BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            if (propertyInfo != null && propertyInfo.CanWrite)
            {
                if (propertyInfo.PropertyType == typeof(DateTime) || propertyInfo.PropertyType == typeof(DateTime?))
                {
                    if(value != ""){
                        propertyInfo.SetValue(currentObject, DateTime.ParseExact(value, format, provider));
                    } 
                    else
                    {
                        propertyInfo.SetValue(currentObject, null);
                    }
                }
                else if(propertyInfo.PropertyType == typeof(decimal) || propertyInfo.PropertyType == typeof(decimal?))
                {
                    bool containsPercent = value.Contains('%');
                    bool containsDollar = value.Contains('$');
                    if (containsDollar || containsPercent)
                    {
                        propertyInfo.SetValue(currentObject, Decimal.Parse(value.Trim(containsDollar ? '$' :'%')));
                    }
                }
                else if(propertyInfo.PropertyType == typeof(int))
                {
                    propertyInfo.SetValue(currentObject, int.Parse(value));
                }
                else 
                {
                    propertyInfo.SetValue(currentObject, value);
                }
            }
        }
    }
}