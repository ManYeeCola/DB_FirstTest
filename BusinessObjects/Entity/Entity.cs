using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BusinessObjects.Entity
{
    public class Entity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
    }

    [Owned]
    public class KeyValue
    {
        public Enum Id { get; set; }
        [NotMapped]
        public string Name { get; set; }
    }

    public static class EnumExtension
    {
        /// <summary>
        /// 获取枚举项上设置的显示文字
        /// </summary>
        /// <param name="value">被扩展对象</param>
        public static string EnumMetadataDisplay(this Enum value)
        {
            string name = Enum.GetName(value.GetType(), value);
            if (string.IsNullOrEmpty(name))
                return value.ToString();
            var attribute = value.GetType().GetField(name).GetCustomAttributes(
                 typeof(DisplayAttribute), false)
                 .Cast<DisplayAttribute>()
                 .FirstOrDefault();
            if (attribute != null)
                return attribute.Name;

            return value.ToString();
        }
    }

}
