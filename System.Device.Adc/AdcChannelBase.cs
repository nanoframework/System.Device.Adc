//
// Copyright (c) .NET Foundation and Contributors
// See LICENSE file in the project root for full license information.
//

namespace System.Device.Adc
{
    /// <summary>
    /// Base class for <see cref="AdcChannel"/>.
    /// </summary>
    public abstract class AdcChannelBase
    {
        /// <summary>
        /// Reads the digital representation of the analog value from the ADC.
        /// </summary>
        /// <returns>
        /// The digital value.
        /// </returns>
        public abstract int ReadValue();

        /// <summary>
        /// Reads the value as a percentage of the max value possible for this controller.
        /// </summary>
        /// <returns>
        /// The value as percentage of the max value.
        /// </returns>
        public abstract double ReadRatio();

        /// <summary>
        /// Gets the AdcController for this channel.
        /// </summary>
        /// <value>
        /// The <see cref="AdcController"/>.
        /// </value>
        public AdcController Controller { get; }
    }
}
