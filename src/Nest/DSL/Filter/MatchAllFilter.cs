﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Nest
{
	public class MatchAllFilter : FilterBase
	{
		internal override bool IsConditionless
		{
			get
			{
				return false;
			}

		}
	}
}
