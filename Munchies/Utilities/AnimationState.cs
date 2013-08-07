using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Munchies
{
    static class AnimationState
    {
        /* This is code for instantiating this class, which I don't want to do
        private int numStates;
        public int NumberOfStates
        { 
            set
            {
                numStates = value;
            }
            get
            {
                return numStates;
            }
        }

        private int duration;
        public int StateDurationMilliseconds
        {
            set
            {
                duration = value;
            }
            get
            {
                return duration;
            }
        }

        public int GetState(double GameTime)
        {
            return GetState(GameTime, numStates, duration);
        } */

        /// <summary>
        /// Gets an zero-based integer representing the current state of a looping animation.
        /// </summary>
        /// <param name="time">The time (in seconds) that will be used to determine the state.</param>
        /// <param name="numStates">The total number of states in the animation.</param>
        /// <param name="duration">The duration of each state in the animation in milliseconds.</param>
        /// <returns>The zero-based state of the animation at the current time.</returns>
        public static int GetState(double time, int numStates, int duration)
        {
            return GetState(0, time, numStates, duration);
        }

        /// <summary>
        /// Gets an zero-based integer representing the current state of a looping animation.
        /// </summary>
        /// <param name="startTime">The time (in seconds) at which the animation should start, relative to the gameTime parameter.</param>
        /// <param name="gameTime">The time (in seconds) that will be used to determine the state.</param>
        /// <param name="numStates">The total number of states in the animation.</param>
        /// <param name="duration">The duration of each state in the animation in milliseconds.</param>
        /// <returns>The zero-based state of the animation at the current time.</returns>
        public static int GetState(double startTime, double gameTime, int numStates, int duration)
        {
            double gameTimeMilliseconds = (gameTime - startTime) * 1000;

            // The number of milliseonds that a complete loop will take
            double millisecondsPerLoop = duration * numStates;

            double millisecondsElapsedInLoop = gameTimeMilliseconds % millisecondsPerLoop;

            int state = (int)(millisecondsElapsedInLoop / millisecondsPerLoop * numStates);

            return state;
        }

    }
}
