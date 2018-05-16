using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using SCI_lab5.Models;
using System.Collections.Generic;
using System;
using System.Linq;
using DbDataLibrary.Models;

namespace SCI_lab5.Helpers
{
    public static class TableHelper
    {
        public static HtmlString CreateClientsList(this IHtmlHelper html, IEnumerable<Client> clients)
        {
            string result = "";
            foreach (Client @client in clients)
            {
                result += "<tr>";
                result += $"<td>{client.Name}</td>";
                result += $"<td>{client.Birthday.ToShortDateString()}</td>";
                result += $"<td>{ @client.Phone}</td>";
                result += $"<td>{ @client.Address}</td>";
                result += $"<td class=\"act\"><form action=\"/Client/Delete/ "+ @client.Id + 
                          "\" method=\"post\"><a class=\"btn btn-sm btn-primary\" href=\"/Client/Edit/" + client.Id + 
                          "\">Изменить</a><button type = \"submit\" class=\"btn btn-sm btn-danger\">Удалить</button></form></td>";
                result += "</tr>";
            }
            return new HtmlString(result);
        }
    }
}
