using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Involys.Poc.Api.Common.Security.Extensions
{
    public static class HideSensitivePropertiesExtensions
    {
        private static string replacement = "*****";
        private static readonly IEnumerable<Clazz> clazzs = new Clazz[]
       {
            new Clazz { Name = "Commande", Fields = new List<Field>(){ new Field { Name = "Objet" } } },
            new Clazz { Name = "Fournisseur", Fields = new List<Field>(){ new Field { Name = "Type" } } },
       };
    
        public static TData HideSensitivePropertiesForItem<TData>(this TData item)
            where TData : class
        {
            Type itemType = item.GetType();
            string itemTypeName = NormalizeName(itemType.Name);
            foreach (var clazz in clazzs)
            {
                if(clazz.Name == itemTypeName)
                {
                    foreach (var field in clazz.Fields)
                    {
                        PropertyInfo prop = itemType.GetProperty(field.Name);
                        prop.SetValue(item, replacement);
                    }
                }

            }
            return item;
        }

        private static string NormalizeName(string name)
        {
            return name.Replace("Response", "").Replace("Query","").Replace("Create","")
                .Replace("Update", "").Replace("DataModel","");
        }
        public static IQueryable<TData> HideSensitiveProperties<TData>(this IQueryable<TData> query)
            where TData : class
        {
            return query.HideSensitiveProperties().AsQueryable();
        }

        public static IEnumerable<TData> HideSensitiveProperties<TData>(this IEnumerable<TData> query)
            where TData : class
        {
            foreach (var item in query)
                yield return item.HideSensitivePropertiesForItem();
        }

        public static TData UnHideSensitivePropertiesForItem<TData, TDest>(this TData item, TDest dest)
            where TData : class
        {
            Type itemType = item.GetType();
            Type destType = dest.GetType();
            String itemTypeName = NormalizeName(itemType.Name);
            foreach (var clazz in clazzs)
            {
                if (clazz.Name == itemTypeName)
                {
                    foreach (var field in clazz.Fields)
                    {
                        PropertyInfo propItem = itemType.GetProperty(field.Name);
                        PropertyInfo propDest = destType.GetProperty(field.Name);
                        propDest.SetValue(dest, propItem.GetValue(item));
                    }
                }

            }
            return item;
        }
    }
}
