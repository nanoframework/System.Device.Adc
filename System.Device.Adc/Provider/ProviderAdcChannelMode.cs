//
// Copyright (c) .NET Foundation and Contributors
// See LICENSE file in the project root for full license information.
//

namespace System.Device.Adc.Provider
{
    /// <summary>
    /// Determines how the pin value is represented. Implementation of specifics are decided by the provider, so differential may be fully or pseudo differential.
    /// </summary>
    public enum ProviderAdcChannelMode
    {
        /// <summary>
        /// Difference between two pins.
        /// </summary>
        Differential,
        /// <summary>
        /// Simple value of a particular pin.
        /// </summary>
        SingleEnded
    }
}
