using System.ComponentModel;
using System.Reflection;
using System;

namespace Me.JieChen.Lens.Api.Utility.Extentions;

static class EnumExtensions
{
    /// <summary>
    /// Get a enum type's detailed description
    /// If there is no Description attribute, return enum type string itself
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static string ToDescription(this Enum value)
    {
        FieldInfo? field = value.GetType()?.GetField(value.ToString());
        if (field == null)
        {
            return value.ToString();
        }

        DescriptionAttribute? attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;

        // Return Description, or if there is no DescriptionAttribute decorated for the Subcategory then return Subcategory name string
        return attribute == null ? value.ToString() : attribute.Description;
    }
}
