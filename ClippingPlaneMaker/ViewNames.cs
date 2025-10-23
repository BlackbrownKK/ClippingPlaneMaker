using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClippingPlaneMaker
{
    public static class ViewNames
    {
        public static string Top { get; private set; } = "Top";
        public static string Side { get; private set; } = "Side";
        public static string Perspective { get; private set; } = "Perspective";
        public static void UpdateTop(string newName) =>
            Top = string.IsNullOrWhiteSpace(newName) ? Top : newName;
        public static void UpdateSide(string newName) =>
            Side = string.IsNullOrWhiteSpace(newName) ? Side : newName;
        public static void UpdatePerspective(string newName) =>
            Perspective = string.IsNullOrWhiteSpace(newName) ? Perspective : newName;
    }
}
