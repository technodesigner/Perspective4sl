//------------------------------------------------------------------
//
//  For licensing information and to get the latest version go to:
//  http://www.codeplex.com/perspective4sl
//
//  THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY
//  OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
//  LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR
//  FITNESS FOR A PARTICULAR PURPOSE.
//
//------------------------------------------------------------------
using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.IO;

namespace Perspective.Core.Wpf.Media
{
    /// <summary>
    /// Captures raw audio data in a stream.
    /// </summary>
    public class StreamAudioSink : AudioSink
    {
        /// <summary>
        /// Initializes an new instance of StreamAudioSink.
        /// </summary>
        /// <param name="stream">The stream to write to.</param>
        public StreamAudioSink(Stream stream):base()
        {
            BaseStream = stream;
        }

        /// <summary>
        /// Gets or sets the audio raw data stream.
        /// </summary>
        public Stream BaseStream { get; private set; }

        /// <summary>
        /// Invoked when an audio device starts capturing audio data.
        /// </summary>
        protected override void OnCaptureStarted()
        {
        }

        /// <summary>
        /// Invoked when an audio device stops capturing audio data.
        /// </summary>
        protected override void OnCaptureStopped()
        {
        }

        /// <summary>
        /// Invoked when an audio device reports an audio format change.
        /// </summary>
        /// <param name="audioFormat">The new audio format.</param>
        protected override void OnFormatChange(AudioFormat audioFormat)
        {
        }

        /// <summary>
        /// Invoked when an audio device captures a complete audio sample.
        /// </summary>
        /// <param name="sampleTimeInHundredNanoseconds">The time, in 100-nanosecond units, when the sample was captured.</param>
        /// <param name="sampleDurationInHundredNanoseconds">The duration of the sample, in 100-nanosecond units.</param>
        /// <param name="sampleData">A byte stream that contains audio data, to be interpreted per the relevant audio format information.</param>
        protected override void OnSamples(
            long sampleTimeInHundredNanoseconds, 
            long sampleDurationInHundredNanoseconds, 
            byte[] sampleData)
        {
            BaseStream.Write(sampleData, 0, sampleData.Length);
        }
    }
}
