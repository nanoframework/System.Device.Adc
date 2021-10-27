//
// Copyright (c) .NET Foundation and Contributors
// See LICENSE file in the project root for full license information.
//

using System.Runtime.CompilerServices;

namespace System.Device.Adc
{
    /// <summary>
    /// Represents an <see cref="AdcController"/> on the system
    /// </summary>
    public sealed class AdcController : IAdcController
    {
        // this is used as the lock object 
        // a lock is required because multiple threads can access the AdcController
        [Diagnostics.DebuggerBrowsable(Diagnostics.DebuggerBrowsableState.Never)]
        private object _syncLock;

        [Diagnostics.DebuggerBrowsable(Diagnostics.DebuggerBrowsableState.Never)]
        private AdcChannelMode _channelMode = AdcChannelMode.SingleEnded;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdcController"/> class. 
        /// </summary>
        /// <returns>
        /// The <see cref="AdcController"/> for the system.
        /// </returns>
        /// <exception cref="InvalidOperationException">If the <see cref="AdcController"/> has already been instantiated.</exception>
        public AdcController()
        {
            // check if this device is already opened
            if (_syncLock == null)
            {
                // call native init to allow HAL/PAL inits related with ADC hardware
                // this is also used to check if the requested ADC actually exists
                NativeInit();

                _syncLock = new object();
            }
            else
            {
                // this controller already exists: throw an exception
                throw new InvalidOperationException();
            }
        }

        /// <summary>
        /// The number of channels available on the <see cref="AdcController"/>.
        /// </summary>
        /// <value>
        /// Number of channels.
        /// </value>
        public int ChannelCount {
            get
            {
                return NativeGetChannelCount();
            }
        }

        /// <summary>
        /// Gets or sets the channel mode for the <see cref="AdcController"/>.
        /// </summary>
        /// <value>
        /// The mode for the <see cref="AdcChannel"/>.
        /// </value>
        public AdcChannelMode ChannelMode {
            get
            {
                return _channelMode;
            }
            set
            {
                _channelMode = value;
            }
        }

        /// <summary>
        /// Gets the maximum value that the <see cref="AdcController"/> can report.
        /// </summary>
        /// <value>
        /// The maximum value.
        /// </value>
        public int MaxValue {
            get
            {
                return NativeGetMaxValue();
            }
            
        }

        /// <summary>
        /// The minimum value the <see cref="AdcController"/> can report.
        /// </summary>
        /// <value>
        /// The minimum value.
        /// </value>
        public int MinValue {
            get
            {
                return NativeGetMinValue();
            }
        }

        /// <summary>
        /// Gets the resolution of the controller as number of bits it has. For example, if we have a 10-bit ADC, that means it can detect 1024 (2^10) discrete levels.
        /// </summary>
        /// <value>
        /// The number of bits the <see cref="AdcController"/> has.
        /// </value>
        public int ResolutionInBits {
            get
            {
                return NativeGetResolutionInBits();
            }
        }


        /// <summary>
        /// Verifies that the specified channel mode is supported by the controller.
        /// </summary>
        /// <param name="channelMode">
        /// The channel mode.
        /// </param>
        /// <returns>
        /// True if the specified channel mode is supported, otherwise false.
        /// </returns>
        public bool IsChannelModeSupported(AdcChannelMode channelMode)
        {
            return NativeIsChannelModeSupported((int)channelMode);
        }

        /// <summary>
        /// Opens a connection to the specified ADC channel.
        /// </summary>
        /// <param name="channelNumber">
        /// The channel to connect to.
        /// </param>
        /// <returns>
        /// The ADC channel.
        /// </returns>
        public AdcChannel OpenChannel(Int32 channelNumber)
        {
            NativeOpenChannel(channelNumber);

            return new AdcChannel(this, channelNumber);
        }

        #region Native Calls

        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern void NativeOpenChannel(Int32 channelNumber);

        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern int NativeGetChannelCount();

        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern int NativeGetMaxValue();

        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern int NativeGetMinValue();

        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern bool NativeIsChannelModeSupported(int mode);

        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern int NativeGetResolutionInBits();

        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern void NativeInit();

        #endregion
    }
}
