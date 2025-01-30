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
    public class AdcController : AdcControllerBase
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

        /// <inheritdoc/>
        public override int ChannelCount
        {
            get
            {
                return NativeGetChannelCount();
            }
        }

        /// <inheritdoc/>
        public override AdcChannelMode ChannelMode 
        {
            get
            {
                return _channelMode;
            }

            set
            {
                _channelMode = value;
            }
        }

        /// <inheritdoc/>
        public override int MaxValue
        {
            get
            {
                return NativeGetMaxValue();
            }
        }

        /// <inheritdoc/>
        public override int MinValue
        {
            get
            {
                return NativeGetMinValue();
            }
        }

        /// <inheritdoc/>
        public override int ResolutionInBits 
        {
            get
            {
                return NativeGetResolutionInBits();
            }
        }

        /// <inheritdoc/>
        public override bool IsChannelModeSupported(AdcChannelMode channelMode)
        {
            return NativeIsChannelModeSupported((int)channelMode);
        }

        /// <inheritdoc/>
        public override AdcChannel OpenChannel(int channelNumber)
        {
            NativeOpenChannel(channelNumber);

            return new AdcChannel(this, channelNumber);
        }

        #region Native Calls

        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern void NativeOpenChannel(int channelNumber);

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
