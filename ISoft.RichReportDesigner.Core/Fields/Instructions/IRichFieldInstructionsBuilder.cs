using ISoft.RichReportDesigner.Core.Fields.Argumentes;
using ISoft.RichReportDesigner.Core.Fields.Codes;
using ISoft.RichReportDesigner.Core.Fields.Switches;

namespace ISoft.RichReportDesigner.Core.Fields.Instructions
{
    public interface IRichFieldInstructionsBuilder
    {
        void BuildInstructions(IRichFieldCode fieldCode, object options, InstructionsBuilderTargets targets);

        void BuildInstructions(IRichFieldCode fieldCode, object options);

        void BuildFieldArguments(IRichFieldArgumentCollection arguments, object options);

        void BuildFieldSwitches(IRichFieldSwitchCollection switches, object options);

        string GetFieldSwitchesBuildString(object options);

        string GetFieldArgumentsBuildString(object options);
    }
}
