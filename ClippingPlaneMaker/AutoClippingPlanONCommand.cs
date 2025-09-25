using System;
using Rhino;
using Rhino.Commands;

namespace ClippingPlaneMaker
{
    public class AutoClippingPlanONCommand : Command
    {
        public AutoClippingPlanONCommand()
        {
            Instance = this;
        }

        ///<summary>The only instance of this command.</summary>
        public static AutoClippingPlanONCommand Instance { get; private set; }

        public override string EnglishName => "AutoClippingPlan_ON";

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            CrossSectionMouseListener.Instance.Enabled = true;
            return Result.Success;
        }
    }
}