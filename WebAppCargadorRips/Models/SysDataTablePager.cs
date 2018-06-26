using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//https://qawithexperts.com/article/asp.net/jquery-datatable-server-side-processing-with-web-api-in-aspn/72
//clase maestra para el listado en datatablejs
namespace WebAppCargadorRips.Models
{
    public class SysDataTablePager<T>
    {
        public SysDataTablePager(IEnumerable<T> items,
            int totalRecords,
            int totalDisplayRecords,
            string sEcho)
        {
            aaData = items;
            iTotalRecords = totalRecords;
            iTotalDisplayRecords = totalDisplayRecords;
            this.sEcho = sEcho;
        }



        public IEnumerable<T> aaData { get; set; }

        public int iTotalRecords { get; set; }

        public int iTotalDisplayRecords { get; set; }

        public string sEcho { get; set; }
    }
}