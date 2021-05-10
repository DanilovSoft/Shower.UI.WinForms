using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowerUI
{
    [DebuggerDisplay("{ExternalTemp} => {InternalTemp}")]
    public struct Step
    {
        public int InternalTemp { get; set; }
        public int ExternalTemp { get; set; }
    }

    public class SetupModel
    {
        public const int LOWER_BOUND = 15;
        public const int UPPER_BOUND = 40;
        public const int STEP_COUNT = (UPPER_BOUND - LOWER_BOUND);

        public Step[] Steps { get; }

        public SetupModel()
        {
            Steps = new Step[STEP_COUNT];
        }

        public void ParseTemp(byte[] data)
        {
            for (int i = 0; i < STEP_COUNT; i++)
            {
                int index = STEP_COUNT - i - 1;
                Steps[index].ExternalTemp = i + LOWER_BOUND;
                Steps[index].InternalTemp = data[i];
            }
        }

        public Step this[int index]
        {
            get
            {
                return Steps[index];
            }
            set
            {
                Steps[index] = value;
            }
        }
    }
}
