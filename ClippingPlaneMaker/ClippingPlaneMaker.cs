using System;
using System.Collections.Generic; 
using Rhino;
using Rhino.Display;
using Rhino.DocObjects;
using Rhino.Geometry;

namespace ClippingPlaneMaker
{
    public static class ClippingPlaneMaker
    {
        /*
    The ClippingPlaneMaker class provides utilities for dynamically creating and managing
    cross-sectional clipping planes in different Rhino viewports.


 TopClippingPlaneMaker(double z)
        - Targets the "Top" viewport.
        - Creates a horizontal clipping plane at a specific Z-coordinate.
        - Clears previous clipping planes in the top view before placement.


    Key Details:
    - Uses Rhino's `AddClippingPlane` method to create section planes.
    - Clipping planes are sized large (10,000 x 10,000) to ensure full coverage.
    - After adding, the view is zoomed and redrawn to update visuals.
    - The class depends on `CustomViewportLayoutCommand` constants for view name matching.
    - Works in coordination with mouse interaction classes (like CrossSectionMouseListener) to enable interactive sectioning.

    Usage:
    - Call these methods from UI or mouse event handlers to generate section views on demand.
*/
        const double ULength = 1;
        const double VLength = 1;
        


        public static void TopClippingPlaneMaker(double z)
        {
            var doc = RhinoDoc.ActiveDoc;
            if (doc == null)
                return;

            RhinoView topView = null;
            foreach (var v in doc.Views.GetViewList(true, false))
            {
                if (v.ActiveViewport.Name.Equals(ViewNames.Top, StringComparison.OrdinalIgnoreCase))
                {
                    topView = v;
                    break;
                }
            }

            if (topView == null)
            {
                RhinoApp.WriteLine($"Top view not found.");
                return;
            }

            doc.Views.ActiveView = topView;
            

            // Define the plane at Z height
            Plane plane = new Plane(new Point3d(0, 0, z), new Vector3d(0, 0, -1));
            
            // Delete existing clipping planes
            var existing = doc.Objects.FindClippingPlanesForViewport(topView.ActiveViewport);
            foreach (var cp in existing)
                doc.Objects.Delete(cp, true);

            // Add new clipping plane
            Guid cpId = doc.Objects.AddClippingPlane(plane, ULength, VLength,
                new List<Guid> { topView.ActiveViewportID });

            if (cpId != Guid.Empty)
            {
                topView.Redraw();
            }
        }


        public static void isometricClippingPlaneMaker(double x, double z)
        {
            

            var doc = RhinoDoc.ActiveDoc;
            if (doc == null)
                return;

            // Find isometric view
            RhinoView isoView = null;
            foreach (var v in doc.Views.GetViewList(true, false))
            {
                if (v.ActiveViewport.Name.ToLower().Contains(ViewNames.Perspective.ToLower()))
                {
                    isoView = v;
                    break;
                }
            }

            if (isoView == null)
            {
                RhinoApp.WriteLine($"Perspective view not found.");
                return;
            }

            doc.Views.ActiveView = isoView;

            // Define the plane at Z height
            Plane plane = new Plane(new Point3d(0, 0, z), new Vector3d(0, 0, -100));
            double uLength = 10000;
            double vLength = 10000;

            // Delete existing clipping planes
            var existing = doc.Objects.FindClippingPlanesForViewport(isoView.ActiveViewport);
            foreach (var cp in existing)
                doc.Objects.Delete(cp, true);

            // Add new clipping plane
            Guid cpId = doc.Objects.AddClippingPlane(plane, uLength, vLength, new List<Guid> { isoView.ActiveViewportID });

            if (cpId != Guid.Empty)
            {
                isoView.ActiveViewport.ZoomExtents();
                isoView.Redraw();
                doc.Views.Redraw();
                RhinoApp.WriteLine($"Clipping plane created at X = {x:0.00}, Z = {z:0.00}");
            }

        }

    }
}