//
// Copyright (c) .NET Foundation and Contributors
// See LICENSE file in the project root for full license information.
//

using System.Runtime.CompilerServices;

namespace System.Device.Adc
{
    /// <summary>
    /// Represents a single ADC channel.
    /// </summary>
    public class AdcChannel : AdcChannelBase, IDisposable
    {
        // this is used as the lock object 
        // a lock is required because multiple threads can access the channel
        [Diagnostics.DebuggerBrowsable(Diagnostics.DebuggerBrowsableState.Never)]
        private readonly object _syncLock;

        [Diagnostics.DebuggerBrowsable(Diagnostics.DebuggerBrowsableState.Never)]
        private readonly int _channelNumber;

        [Diagnostics.DebuggerBrowsable(Diagnostics.DebuggerBrowsableState.Never)]
        private AdcController _adcController;

        [Diagnostics.DebuggerBrowsable(Diagnostics.DebuggerBrowsableState.Never)]
        private bool _disposed;

        internal AdcChannel(AdcController controller, int channelNumber)
        {
            _adcController = controller;
            _channelNumber = channelNumber;

            _syncLock = new object();
        }

        /// <inheritdoc/>
        public AdcController Controller
        {
            get
            {
                return _adcController;
            }
        }

        /// <inheritdoc/>
        public override int ReadValue()
        {
            lock (_syncLock)
            {
                // check if pin has been disposed
                if (_disposed) 
                { 
                    throw new ObjectDisposedException(); 
                }

                return NativeReadValue();
            }
        }

        #region IDisposable Support

        private void Dispose(bool disposing)
        {
            if (_adcController != null)
            {
                if (disposing)
                {
                    NativeDisposeChannel();
                    _adcController = null;

                }

                _disposed = true;
            }
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            lock (_syncLock)
            {
                if (!_disposed)
                {
                    Dispose(true);
                    GC.SuppressFinalize(this);
                }
            }
        }

#pragma warning disable 1591
        ~AdcChannel()
        {
            Dispose(false);
        }
#pragma warning restore 1591

        #endregion

        #region Native Calls

        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern int NativeReadValue();

        [MethodImpl(MethodImplOptions.InternalCall)]
        private extern void NativeDisposeChannel();

        #endregion

    }
}
