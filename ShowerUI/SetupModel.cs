﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShowerUI
{
    public class SetupModel
    {
        public const int LOWER_BOUND = 15;
        public const int UPPER_BOUND = 40;
        public const int STEP_COUNT = UPPER_BOUND - LOWER_BOUND;

        public SetupModel()
        {
            Steps = new TemperatureStep[STEP_COUNT];
        }

        public TemperatureStep[] Steps { get; }

        public void ParseTemp(byte[] data)
        {
            for (int i = 0; i < STEP_COUNT; i++)
            {
                int index = STEP_COUNT - i - 1;
                Steps[index].ExternalTemp = i + LOWER_BOUND;
                Steps[index].InternalTemp = data[i];
            }
        }

        public TemperatureStep this[int index]
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