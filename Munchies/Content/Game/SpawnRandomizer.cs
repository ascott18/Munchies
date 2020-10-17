using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Munchies
{
	public static class SpawnRandomizer
	{
		private static readonly Random random = new Random();

		/// <summary>
		///     Given an array of integers where each integer represents the proportion to the sum of the array that integer's index
		///     should be chosen, pick an index and return it.
		/// </summary>
		/// <param name="chances">The integer array that contains the proportions at which each index should be chosen.</param>
		/// <returns>The index of the array that has been chosen.</returns>
		public static int PickSpawn(int[] chances)
		{
			int rnd = random.Next(chances.Sum());

			int runningSum = 0;
			for (int index = 0; index < chances.Length; index++)
			{
				runningSum += chances[index];
				if (runningSum > rnd)
					return index;
			}
			return 0;
		}
	}
}
