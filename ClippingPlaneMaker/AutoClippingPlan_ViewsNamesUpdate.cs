using Rhino;
using Rhino.Commands;
using Rhino.Display;
using Rhino.Input;
using Rhino.Input.Custom;
using System.Collections.Generic;

namespace ClippingPlaneMaker
{
    public class AutoClippingPlan_ViewsNamesUpdate : Command
    {
        public AutoClippingPlan_ViewsNamesUpdate() => Instance = this;

        ///<summary>The only instance of this command.</summary>
        public static AutoClippingPlan_ViewsNamesUpdate Instance { get; private set; }

        public override string EnglishName => "AutoClippingPlan_ViewsNamesUpdate";

       

        protected override Result RunCommand(RhinoDoc doc, RunMode mode)
        {
            // --- 1) Choose which logical alias to update (command line) ---
            var gTarget = new GetOption();
            gTarget.SetCommandPrompt("Select which logical view name to update");
            int optTop = gTarget.AddOption(ViewNames.Top);
            int optSide = gTarget.AddOption(ViewNames.Side);
            int optPerspective = gTarget.AddOption(ViewNames.Perspective);

            var r1 = gTarget.Get();
            if (r1 == GetResult.Cancel) return Result.Cancel;
            if (r1 != GetResult.Option) return Result.Nothing;

            int chosen = gTarget.Option().Index;

            // --- 2) Ask user to type the viewport title ---
            var gs = new GetString();
            gs.SetCommandPrompt("Type the name of the viewport (e.g. \"Top\", \"Perspective\")");
            var res2 = gs.Get();
            if (res2 == GetResult.Cancel) return Result.Cancel;
            if (gs.CommandResult() != Result.Success) return gs.CommandResult();

            string selectedName = gs.StringResult();
            if (string.IsNullOrWhiteSpace(selectedName))
            {
                RhinoApp.WriteLine("No viewport name provided.");
                return Result.Nothing;
            }

            // Validate the viewport exists
            var targetView = doc.Views.Find(selectedName, true);
            if (targetView == null)
            {
                RhinoApp.WriteLine($"No viewport titled \"{selectedName}\" was found. Aborting.");
                return Result.Failure;
            }


            // --- 3) Update runtime-configurable aliases ---
            // (Assumes ViewNames.UpdateTop/Side/Perspective(string) exist)
            if (chosen == optTop)
                ViewNames.UpdateTop(selectedName);
            else if (chosen == optSide)
                ViewNames.UpdateSide(selectedName);
            else if (chosen == optPerspective)
                ViewNames.UpdatePerspective(selectedName);

            // --- 4) Activate the chosen viewport (scripted; version-safe) ---
          
            doc.Views.Redraw();

            //  feedback message using the logical alias we mapped
            string logical = (chosen == optTop) ? ViewNames.Top
                           : (chosen == optSide) ? ViewNames.Side
                           : ViewNames.Perspective;

            RhinoApp.WriteLine($"Mapped {logical} -> \"{selectedName}\".");
            return Result.Success;
        }
    }
}
