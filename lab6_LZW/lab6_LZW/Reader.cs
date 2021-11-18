using System;
using System.IO;
using System.Collections;

namespace lab6_LZW
{
    public class MyBinaryReader : BinaryReader
    {
        private bool[] curByte = new bool[8];
        private byte curBitIndx = 0;
        private BitArray ba;
        private bool EOS = false;

        public MyBinaryReader(Stream s) : base(s)
        {
            ba = new BitArray(new byte[] { base.ReadByte() });
            ba.CopyTo(curByte, 0);
            ba = null;
        }

        public override bool ReadBoolean()
        {
            if (curBitIndx == 8)
            {
                ba = new BitArray(new byte[] { base.ReadByte() });
                ba.CopyTo(curByte, 0);
                ba = null;
                this.curBitIndx = 0;
            }

            bool b = curByte[curBitIndx];
            curBitIndx++;
            return b;
        }

        public int ReadNBits(int n)
        {
            if (EOS)
                return -1;

            BitArray bitVal = new BitArray(32);
            for (int i = 0; i < n; i++)
            {
                try
                {
                    bitVal[i] = ReadBoolean();
                }
                catch (System.IO.EndOfStreamException)
                {
                    EOS = true;
                    break;
                    //return -1;
                }
            }

            int[] array = new int[1];
            bitVal.CopyTo(array, 0);

            return array[0];
        }

        /*
        public override byte ReadByte()
        {
            bool[] bar = new bool[8];
            byte i;
            for (i = 0; i < 8; i++)
            {
                bar[i] = this.ReadBoolean();
            }

            byte b = 0;
            byte bitIndex = 0;
            for (i = 0; i < 8; i++)
            {
                if (bar[i])
                {
                    b |= (byte)(((byte)1) << bitIndex);
                }
                bitIndex++;
            }
            return b;
        }

        
        public override byte[] ReadBytes(int count)
        {
            byte[] bytes = new byte[count];
            for (int i = 0; i < count; i++)
            {
                bytes[i] = this.ReadByte();
            }
            return bytes;
        }

        public override ushort ReadUInt16()
        {
            byte[] bytes = ReadBytes(2);
            return BitConverter.ToUInt16(bytes, 0);
        }

        public override uint ReadUInt32()
        {
            byte[] bytes = ReadBytes(4);
            return BitConverter.ToUInt32(bytes, 0);
        }

        public override ulong ReadUInt64()
        {
            byte[] bytes = ReadBytes(8);
            return BitConverter.ToUInt64(bytes, 0);
        }*/
    }
}
