using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.IO;

namespace Hive
{
    /// <summary>
    /// 用于TCP服务器端回调参数
    /// </summary>
    class ReadWriteObject
    {
        public TcpClient client;
        public NetworkStream networkStream;
        public byte[] readBytes;
        public byte[] writeBytes;
        public ReadWriteObject(TcpClient client)
        {
            this.client = client;
            networkStream = client.GetStream();
            readBytes = new byte[client.ReceiveBufferSize];
            writeBytes = new byte[client.SendBufferSize];
        }
        public void InitReadArray()
        { readBytes = new byte[client.ReceiveBufferSize]; }
        public void InitWriteArray()
        { writeBytes = new byte[client.SendBufferSize]; }
    }
    /// <summary>
    /// TCP服务器端从硬盘 异步 读取文件并 异步 发送到TCP客户端
    /// </summary>
    class StateReadNSend
    {
        /// <summary>
        /// 文件读取&发送缓冲区。设置得大则发送速度快，但同时占用内存多（本人测试得知）
        /// </summary>
        public byte[] fileBuffer = new byte[102400];
        public NetworkStream networkStream;
        public FileStream fileStream;
        public long FileLength;
        public StateReadNSend(TcpClient client,FileStream fileStream)
        {
            networkStream = client.GetStream();
            this.fileStream = fileStream;
            this.FileLength = fileStream.Length;
        }
    }
    /// <summary>
    /// 用于TCP客户端回调参数(ClientNFileName->ReadObject)
    /// </summary>
    public class ReadObject
    {
        public NetworkStream networkStream;
        public byte[] bytes;
        /// <summary>
        /// 远程文件全路径
        /// </summary>
        public string fileName;
        public FileStream fileStream;
        /// <summary>
        /// 文件流写入时的字节偏移量,构造时置0
        /// </summary>
        public int offset;
        /// <summary>
        /// 从界面传给cnn,再传给readObject
        /// </summary>
        public ShareElement shareElement;
        /// <summary>
        /// 文件的大小（字节数），由服务器传过来
        /// </summary>
        public long FileLength;
        /// <summary>
        /// 本地存储路径，使用FileDialog存储文件到的本地位置（单独赋值）
        /// </summary>
        public string SaveToPath;

        public ReadObject(NetworkStream ns,int bufferSize,FileStream fileStream,string fileName,ShareElement se)
        {
            this.networkStream = ns;
            bytes = new byte[bufferSize];
            this.fileStream = fileStream;
            offset = 0;
            this.fileName = fileName;
            this.shareElement = se;
        }
    }
    /// <summary>
    /// 用于TCP客户端连接服务器时传递参数(ClientNFileName->ReadObject)
    /// </summary>
    struct ClientNFileName
    {
        public TcpClient client;
        /// <summary>
        /// 远程全路径文件名（共享名）
        /// </summary>
        public string FileName;
        /// <summary>
        /// 本地文件流（要存储的位置）
        /// </summary>
        public FileStream fileStream;
        /// <summary>
        /// 从界面传给cnn,再传给readObject
        /// </summary>
        public ShareElement shareElement;
        /// <summary>
        /// 本地存储路径，使用FileDialog存储文件到的本地位置（单独赋值）
        /// </summary>
        public string SaveToPath;
    }
}
