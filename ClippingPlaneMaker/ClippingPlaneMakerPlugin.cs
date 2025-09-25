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

    }
}