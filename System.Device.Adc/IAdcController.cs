//
// Copyright (c) .NET Foundation and Contributors
// See LICENSE file in the project root for full license information.
//

namespace System.Device.Adc
{
    internal interface IAdcController
    {
        bool IsChannelModeSupported(AdcChannelMode channelMode);
        AdcChannel OpenChannel(int channelNumber);

        int ChannelCount { get; }
        AdcChannelMode ChannelMode { get; set; }
        int MaxValue { get; }
        int MinValue { get; }
        int ResolutionInBits { get; }
    }
}
