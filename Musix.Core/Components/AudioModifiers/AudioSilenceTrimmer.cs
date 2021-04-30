//using System;
//using System.IO;
//using Musix.Core.API;
//using Musix.Core.Models;
//using NAudio.Wave;

//namespace Musix.Core.Components.AudioModifiers
//{
//    public class AudioSilenceTrimmer : AudioEffect
//    {
//        public float MaxTrimVolume = 0f;

//        public override AudioEffectResult ApplyEffect(ref AudioFileReader Reader)
//        {
//            if (File.Exists(Output)) File.Delete(Output);
//            using (Mp3FileReader Reader = new Mp3FileReader(Input))
//            using (FileStream Writer = new FileStream(Output, FileMode.OpenOrCreate, FileAccess.Write))
//            {
//                Mp3Frame NextFrame = Reader.ReadNextFrame();
//                while (NextFrame != null)
//                {
//                    bool WriteFrame = true;
//                    bool EndFrames = false;
//                    if (StartT.HasValue)
//                    {
//                        if (Reader.CurrentTime < StartT.Value) WriteFrame = false;
//                    }
//                    if (EndT.HasValue)
//                    {
//                        if (Reader.CurrentTime > EndT.Value)
//                        {
//                            WriteFrame = false;
//                            EndFrames = true;
//                        }
//                    }
//                    if (WriteFrame)
//                    {
//                        Writer.Write(NextFrame.RawData, 0, NextFrame.FrameLength);
//                    }
//                    if (EndFrames)
//                    {
//                        NextFrame = null;
//                        continue;
//                    }
//                    NextFrame = Reader.ReadNextFrame();
//                }
//                return Writer.Length != 0;
//            }
//        }

//        public bool TrimAudio(string Input, string Output, TimeSpan? StartT, TimeSpan? EndT)
//        {
//        }
//    }
//}