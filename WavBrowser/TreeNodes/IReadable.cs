﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WavBrowser.TreeNodes
{
	public interface IReadable
	{
		Stream GetStream();
	}
}
