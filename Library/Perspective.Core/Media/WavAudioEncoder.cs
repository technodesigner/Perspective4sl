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

namespace Perspective.Core.Media
{
    /// <summary>
    /// Encodes audio raw data to a PCM WAV format stream.
    /// </summary>
    public class WavAudioEncoder
    {
        /// <summary>
        /// Initializes an new instance of WavAudioEncoder.
        /// </summary>
        public WavAudioEncoder()
        {
            Channels = 2;
            SamplesPerSecond = 44100;
            BitsPerSample = 16;
        }

        /// <summary>
        /// Gets or sets the number of channels (1 = mono, 2 = stereo, etc.).
        /// </summary>
        public int Channels { get; set; }

        /// <summary>
        /// Gets or Sets the number of samples per second (i.e. 44100 Hz).
        /// </summary>
        public int SamplesPerSecond { get; set; }

        /// <summary>
        /// Gets or Sets the number of bits per sample (i.e. 16 bits).
        /// </summary>
        public int BitsPerSample { get; set; }
        
        /// <summary>
        /// Gets or sets the audio raw data stream.
        /// </summary>
        public Stream RawData { get; set; }

        /// <summary>
        /// Encodes audio raw data to a specified WAV format stream.
        /// </summary>
        /// <param name="stream">Identifies the file stream that raw data are encoded to.</param>
        public void Save(Stream stream)
        {
            if (RawData != null)
            {
                // WAV file specifications : 
                // https://ccrma.stanford.edu/courses/422/projects/WaveFormat/
                // http://www-mmsp.ece.mcgill.ca/Documents/AudioFormats/WAVE/WAVE.html
                using (BinaryWriter writer = new BinaryWriter(stream))
                {
                    // RIFF chunck
                    writer.Write('R');
                    writer.Write('I');
                    writer.Write('F');
                    writer.Write('F');
                    writer.Write((uint)(36 + RawData.Length));  // chunck size
                    writer.Write('W');
                    writer.Write('A');
                    writer.Write('V');
                    writer.Write('E');

                    // Format subchunck
                    writer.Write('f');
                    writer.Write('m');
                    writer.Write('t');
                    writer.Write(' ');
                    writer.Write((uint)16); // subchunck size
                    writer.Write((ushort)1);  // Audio format : PCM
                    writer.Write((ushort)Channels);
                    writer.Write((uint)SamplesPerSecond);
                    writer.Write((uint)(SamplesPerSecond * Channels * BitsPerSample / 8));  // Byte rate
                    writer.Write((ushort)(Channels * BitsPerSample / 8));   // Data block size
                    writer.Write((ushort)BitsPerSample);

                    // Data subchunck
                    writer.Write('d');
                    writer.Write('a');
                    writer.Write('t');
                    writer.Write('a');
                    writer.Write((uint)(RawData.Length));
                    long position = RawData.Position;
                    try
                    {
                        RawData.Position = 0;
                        RawData.CopyTo(stream);
                    }
                    finally
                    {
                        RawData.Position = position;
                    }
                }
            }
        }
    }
}
