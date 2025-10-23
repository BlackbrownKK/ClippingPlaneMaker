using System;

using Rhino.UI;

namespace ClippingPlaneMaker
{
    public class CrossSectionMouseListener : MouseCallback
    {
        /*
    The CrossSectionMouseListener class listens for mouse clicks in a specific Rhino viewport
    (e.g., the "Side View") and triggers the creation of cross-section clipping planes.

    - Inherits from Rhino's MouseCallback to intercept mouse events.
    - Only responds to left-clicks within the active viewport.
    - Verifies that the active viewport matches the Side View (case-insensitive check).
    - Converts the 2D mouse position into a 3D world coordinate using the frustum line.
    - Extracts the X and Z values from the clicked location.
    - Sends those coordinates to the ClippingPlaneMaker to generate clipping planes in:
        • Top view (Z),
        • Forward view (X),
        • Isometric view (both X and Z).

    Usage:
    - Instantiate and enable this mouse listener to allow interactive sectioning based on user clicks in a side view.
*/
        private static readonly Lazy<CrossSectionMouseListener> _instance = new Lazy<CrossSectionMouseListener>(() => new CrossSectionMouseListener()); 
        public static CrossSectionMouseListener Instance => _instance.Value;
        
        protected override void OnMouseDown(MouseCallbackEventArgs e)
        {
            if (e.MouseButton != MouseButton.Left)
                return;

            var view = e.View;
            if (view == null || !view.ActiveViewport.Name.Equals(ViewNames.Side, StringComparison.OrdinalIgnoreCase))
                return;

            // Convert mouse position to 3D point
            if (!e.View.ActiveViewport.GetFrustumLine(e.ViewportPoint.X, e.ViewportPoint.Y, out var line))
                return;

            // Get point in world space (choose Z coordinate from near point)
            double z = line.From.Z;
            double x = line.From.X;
            ClippingPlaneMaker.TopClippingPlaneMaker(z);
            ClippingPlaneMaker.isometricClippingPlaneMaker(x, z); // Pass both X and Z to isometricClippingPlaneMaker
        }
    }
}
