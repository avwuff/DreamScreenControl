using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DreamScreenControl
{
    class DreamScreen
    {
        private static byte[] uartComm_crc8_table = new byte[] { unchecked((byte)0), unchecked((byte)7), unchecked((byte)14), unchecked((byte)9), unchecked((byte)28), unchecked((byte)27), unchecked((byte)18), unchecked((byte)21), unchecked((byte)56), unchecked((byte)63), unchecked((byte)54), unchecked((byte)49),
            unchecked((byte)36), unchecked((byte)35), unchecked((byte)42), unchecked((byte)45), unchecked((byte)112), unchecked((byte)119), unchecked((byte)126), unchecked((byte)121), unchecked((byte)108), unchecked((byte)107), unchecked((byte)98), unchecked((byte)101), unchecked((byte)72), unchecked((byte)79),
            unchecked((byte)70), unchecked((byte)65), unchecked((byte)84), unchecked((byte)83), unchecked((byte)90), unchecked((byte)93), unchecked((byte)-32), unchecked((byte)-25), unchecked((byte)-18), unchecked((byte)-23), unchecked((byte)-4), unchecked((byte)-5), unchecked((byte)-14), unchecked((byte)-11),
            unchecked((byte)-40), unchecked((byte)-33), unchecked((byte)-42), unchecked((byte)-47), unchecked((byte)-60), unchecked((byte)-61), unchecked((byte)-54), unchecked((byte)-51), unchecked((byte)-112), unchecked((byte)-105), unchecked((byte)-98), unchecked((byte)-103), unchecked((byte)-116), unchecked((byte)-117),
            unchecked((byte)-126), unchecked((byte)-123), unchecked((byte)-88), unchecked((byte)-81), unchecked((byte)-90), unchecked((byte)-95), unchecked((byte)-76), unchecked((byte)-77), unchecked((byte)-70), unchecked((byte)-67), unchecked((byte)-57), unchecked((byte)-64), unchecked((byte)-55), unchecked((byte)-50),
            unchecked((byte)-37), unchecked((byte)-36), unchecked((byte)-43), unchecked((byte)-46), unchecked((byte)-1), unchecked((byte)-8), unchecked((byte)-15), unchecked((byte)-10), unchecked((byte)-29), unchecked((byte)-28), unchecked((byte)-19), unchecked((byte)-22), unchecked((byte)-73), unchecked((byte)-80),
            unchecked((byte)-71), unchecked((byte)-66), unchecked((byte)-85), unchecked((byte)-84), unchecked((byte)-91), unchecked((byte)-94), unchecked((byte)-113), unchecked((byte)-120), unchecked((byte)-127), unchecked((byte)-122), unchecked((byte)-109), unchecked((byte)-108), unchecked((byte)-99), unchecked((byte)-102),
            unchecked((byte)39), unchecked((byte)32), unchecked((byte)41), unchecked((byte)46), unchecked((byte)59), unchecked((byte)60), unchecked((byte)53), unchecked((byte)50), unchecked((byte)31), unchecked((byte)24), unchecked((byte)17), unchecked((byte)22), unchecked((byte)3), unchecked((byte)4), unchecked((byte)13),
            unchecked((byte)10), unchecked((byte)87), unchecked((byte)80), unchecked((byte)89), unchecked((byte)94), unchecked((byte)75), unchecked((byte)76), unchecked((byte)69), unchecked((byte)66), unchecked((byte)111), unchecked((byte)104), unchecked((byte)97), unchecked((byte)102), unchecked((byte)115), unchecked((byte)116),
            unchecked((byte)125), unchecked((byte)122), unchecked((byte)-119), unchecked((byte)-114), unchecked((byte)-121), 0, unchecked((byte)-107), unchecked((byte)-110), unchecked((byte)-101), unchecked((byte)-100), unchecked((byte)-79), unchecked((byte)-74), unchecked((byte)-65), unchecked((byte)-72), unchecked((byte)-83),
            unchecked((byte)-86), unchecked((byte)-93), unchecked((byte)-92), unchecked((byte)-7), unchecked((byte)-2), unchecked((byte)-9), unchecked((byte)-16), unchecked((byte)-27), unchecked((byte)-30), unchecked((byte)-21), unchecked((byte)-20), unchecked((byte)-63), unchecked((byte)-58), unchecked((byte)-49), unchecked((byte)-56),
            unchecked((byte)-35), unchecked((byte)-38), unchecked((byte)-45), unchecked((byte)-44), unchecked((byte)105), unchecked((byte)110), unchecked((byte)103), unchecked((byte)96), unchecked((byte)117), unchecked((byte)114), unchecked((byte)123), unchecked((byte)124), unchecked((byte)81), unchecked((byte)86), unchecked((byte)95),
            unchecked((byte)88), unchecked((byte)77), unchecked((byte)74), unchecked((byte)67), unchecked((byte)68), unchecked((byte)25), unchecked((byte)30), unchecked((byte)23), unchecked((byte)16), unchecked((byte)5), unchecked((byte)2), unchecked((byte)11), unchecked((byte)12), unchecked((byte)33), unchecked((byte)38),
            unchecked((byte)47), unchecked((byte)40), unchecked((byte)61), unchecked((byte)58), unchecked((byte)51), unchecked((byte)52), unchecked((byte)78), unchecked((byte)73), unchecked((byte)64), unchecked((byte)71), unchecked((byte)82), unchecked((byte)85), unchecked((byte)92), unchecked((byte)91), unchecked((byte)118),
            unchecked((byte)113), unchecked((byte)120), (byte)255, unchecked((byte)106), unchecked((byte)109), unchecked((byte)100), unchecked((byte)99), unchecked((byte)62), unchecked((byte)57), unchecked((byte)48), unchecked((byte)55), unchecked((byte)34), unchecked((byte)37), unchecked((byte)44), unchecked((byte)43),
            unchecked((byte)6), unchecked((byte)1), unchecked((byte)8), unchecked((byte)15), unchecked((byte)26), unchecked((byte)29), unchecked((byte)20), unchecked((byte)19), unchecked((byte)-82), unchecked((byte)-87), unchecked((byte)-96), unchecked((byte)-89), unchecked((byte)-78), unchecked((byte)-75), unchecked((byte)-68),
            unchecked((byte)-69), unchecked((byte)-106), unchecked((byte)-111), unchecked((byte)-104), unchecked((byte)-97), unchecked((byte)-118), unchecked((byte)-115), unchecked((byte)-124), unchecked((byte)-125), unchecked((byte)-34), unchecked((byte)-39), unchecked((byte)-48), unchecked((byte)-41), unchecked((byte)-62),
            unchecked((byte)-59), unchecked((byte)-52), unchecked((byte)-53), unchecked((byte)-26), unchecked((byte)-31), unchecked((byte)-24), unchecked((byte)-17), unchecked((byte)-6), unchecked((byte)-3), unchecked((byte)-12), unchecked((byte)-13) };

        private IPAddress int_floorIP;
        private Socket floorComm = new Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Dgram, System.Net.Sockets.ProtocolType.Udp);
        private int Port = 8888;
        private IPEndPoint endPoint;


        private byte groupNumber = 0;

        public DreamScreen(string remoteIP)
        {

            int_floorIP = IPAddress.Parse(remoteIP);
            endPoint = new IPEndPoint(int_floorIP, Port);

        }

        public void setMode(int mode)
        {
            sendUDPWrite((byte)3, (byte)1, new byte[] { (byte)mode });
        }


        void sendUDPWrite(byte command1, byte command2, byte[] payload)
        {

            using (MemoryStream stream = new MemoryStream())
            {
                using (BinaryWriter response = new BinaryWriter(stream))
                {
                    response.Write((byte)252);
                    response.Write((byte)(payload.Length + 5));
                    response.Write((byte)this.groupNumber);
                    /*
                    if (broadcastingToGroup)
                    {
                        response.write(33);
                    }
                    else
                    {*/
                        response.Write((byte)17);
                    //}
                    response.Write(command1);
                    response.Write(command2);
                    foreach (byte b in payload)
                    {
                        response.Write(b);
                    }

                    var byteSend = stream.ToArray();
                    response.Write(uartComm_calculate_crc8(byteSend));

                    Console.Write("Sending " + stream.Length + " bytes.");
                    sendUDPUnicast(stream.ToArray());
                }

            }


        }

        private byte uartComm_calculate_crc8(byte[] data)
        {
            byte size = (byte)(data[1] + 1);
            byte crc = (byte)0;
            for (byte cntr = (byte)0; cntr < size; cntr = (byte)(cntr + 1))
            {
                crc = uartComm_crc8_table[((byte)(data[cntr] ^ crc)) & 255];
            }
            return crc;
        }

        private void sendUDPUnicast(byte[] data)
        {
            floorComm.SendTo(data, endPoint);
        }
    }
}
