//
// Copyright (c) .NET Foundation and Contributors
// See LICENSE file in the project root for full license information.
//

using System.Collections.Generic;

namespace System.Device.Adc.Provider
{
    public interface IAdcProvider
    {
        /// <summary>
        /// Gets the ADC controllers available on the system.
        /// </summary>
        /// <returns>
        /// When this method completes it returns a list of all the available controllers on the system.
        /// </returns>
        IReadOnlyList<IAdcControllerProvider> GetControllers()
    }
}
