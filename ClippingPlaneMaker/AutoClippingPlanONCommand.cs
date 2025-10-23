using Rhino;
using Rhino.Commands;
using Rhino.Input;
using Rhino.Input.Custom;
using System;

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
            var go = new GetOption();
            go.SetCommandPrompt("Set Perspective view port availble for clipping plan");
            var tog = new OptionToggle(!CrossSectionMouseListener.perspectiveClippingPlaneOn, "Off", "On");
            int idxPersp = go.AddOptionToggle("PerspectiveClipping", ref tog);
            while (true)
            {
                var res = go.Get();
                if (res == GetResult.Option)
                {
                    if (go.OptionIndex() == idxPersp)
                    {
                        CrossSectionMouseListener.perspectiveClippingPlaneOn = !tog.CurrentValue ? true : false;
                    }
                    continue;
                }
                if (res == GetResult.Nothing || res == GetResult.Cancel)
                    break;
            }
            RhinoApp.WriteLine($"Perspective clipping planes: {(CrossSectionMouseListener.perspectiveClippingPlaneOn ? "OFF" : "ON")}");
            return Result.Success;
        }
    }
}