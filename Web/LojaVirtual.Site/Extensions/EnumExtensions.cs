using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace LojaVirtual.Site.Extensions
{
    public static class EnumExtensions
    {
        public static string DisplayName(this Enum value)
        {
            var attribute = value.GetType().GetField(value.ToString())?.GetCustomAttribute<DisplayAttribute>()?.GetName();
            return attribute ?? value.ToString();
        }
    }

}
