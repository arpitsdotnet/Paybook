using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Paybook.ServiceLayer.Extensions
{
    public static class DataTableExtension
    {
        public static DataTable ToDataTable<T>(this List<T> items)
        {
            var tb = new DataTable(typeof(T).Name);
            try
            {
                PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

                tb.Columns.Add("Index", typeof(int));
                foreach (var prop in props)
                {
                    tb.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                }

                int iIndex = 1;
                foreach (var item in items)
                {
                    var values = new object[props.Length + 1];
                    values[0] = iIndex;
                    for (var i = 0; i < props.Length; i++)
                    {
                        values[i + 1] = props[i].GetValue(item, null);
                    }
                    tb.Rows.Add(values);
                    iIndex++;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex.InnerException);
            }
            return tb;
        }
    }
}
