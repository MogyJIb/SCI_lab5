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
                result +=
                    $"<td style=\"padding-right:10px\"><a href=\"/Client/Edit/{client.Id}\">Edit</a></td>";
                result +=
                    $"<td style=\"padding-right:10px\"><a href=\"/Client/Delete/{client.Id}\">Delete</a></td>";           
                result += $"<td style=\"padding-right:10px\"><a href=\"/Client/More/{client.Id}\">Detail</a></td>";                   
                result += "</tr>";
            }
            return new HtmlString(result);
        }


        public static HtmlString CreateTourKindsList(this IHtmlHelper html, IEnumerable<TourKind> tourKinds)
        {
            string result = "";
            foreach (TourKind @tourKind in tourKinds)
            {
                result += "<tr>";
                result += $"<td>{tourKind.Name}</td>";
                result += $"<td>{ tourKind.Description}</td>";
                result += $"<td>{ tourKind.Constraints}</td>";
                result +=
                    $"<td style=\"padding-right:10px\"><a href=\"/TourKind/Edit/{tourKind.Id}\">Edit</a></td>";
                result +=
                    $"<td style=\"padding-right:10px\"><a href=\"/TourKind/Delete/{tourKind.Id}\">Delete</a></td>";
                result += $"<td style=\"padding-right:10px\"><a href=\"/TourKind/More/{tourKind.Id}\">Detail</a></td>";
                result += "</tr>";
            }
            return new HtmlString(result);
        }

        public static HtmlString CreateToursList(this IHtmlHelper html, IEnumerable<Tour> tours)
        {
            string result = "";
            foreach (Tour @tour in tours)
            {
                result += "<tr>";
                result += $"<td>{tour.Name}</td>";
                result += $"<td>{tour.Price}</td>";
                result += $"<td>{tour.StartDate.ToShortDateString()}</td>";
                result += $"<td>{tour.EndDate.ToShortDateString()}</td>";
                result += $"<td>{tour.TourKind.Name }</td>";
                result += $"<td>{tour.Client.Name}</td>";
                result +=
                    $"<td style=\"padding-right:10px\"><a href=\"/Tour/Edit/{tour.Id}\">Edit</a></td>";
                result +=
                    $"<td style=\"padding-right:10px\"><a href=\"/Tour/Delete/{tour.Id}\">Delete</a></td>";
                result += $"<td style=\"padding-right:10px\"><a href=\"/Tour/More/{tour.Id}\">Detail</a></td>";
                result += "</tr>";
            }
            return new HtmlString(result);
        }
    }
}
