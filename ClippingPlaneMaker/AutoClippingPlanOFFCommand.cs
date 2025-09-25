using System;
using Rhino;
using Rhino.Commands;

namespace ClippingPlaneMaker
{
    public class AutoClippingPlanCommand : Command
    {
        public AutoClippingPlanCommand()
        {
            Instance = this;
        }

        ///<summary>The only instance of this command.</summary>
        public static AutoClippingPlanCommand Instance { get; private set; }

        public override string EnglishName => "AutoClippingPlan_OFF";

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            CrossSectionMouseListener.Instance.Enabled = false;
            return Result.Success;
        }
    }
}