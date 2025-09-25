using Rhino;
using Rhino.PlugIns;
using System;

namespace ClippingPlaneMaker
{
    ///<summary>
    /// <para>Every RhinoCommon .rhp assembly must have one and only one PlugIn-derived
    /// class. DO NOT create instances of this class yourself. It is the
    /// responsibility of Rhino to create an instance of this class.</para>
    /// <para>To complete plug-in information, please also see all PlugInDescription
    /// attributes in AssemblyInfo.cs (you might need to click "Project" ->
    /// "Show All Files" to see it in the "Solution Explorer" window).</para>
    ///</summary>
    public class ClippingPlaneMakerPlugin : Rhino.PlugIns.PlugIn
    {
        public ClippingPlaneMakerPlugin()
        {
            Instance = this;


        }

        ///<summary>Gets the only instance of the ClippingPlaneMakerPlugin plug-in.</summary>
        public static ClippingPlaneMakerPlugin Instance { get; private set; }


        // You can override methods here to change the plug-in behavior on
        // loading and shut down, add options pages to the Rhino _Option command
        // and maintain plug-in wide options in a document.

        // Called once when Rhino loads your plug-in
        protected override LoadReturnCode OnLoad(ref string errorMessage)
        {
            // Turn on the mouse listener
            CrossSectionMouseListener.Instance.Enabled = true;
            RhinoApp.WriteLine("CrossSectionMouseListener enabled.");
            return LoadReturnCode.Success;
        }

        // Called when Rhino is shutting down or unloading your plug-in
        protected override void OnShutdown()
        {
            // Turn it off (good hygiene)
            CrossSectionMouseListener.Instance.Enabled = false;
            RhinoApp.WriteLine("CrossSectionMouseListener disabled.");
            base.OnShutdown();
        }

    }
}