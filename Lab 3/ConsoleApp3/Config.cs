using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;

namespace ConsoleApp3
{
    class Config
    {
        public char filetype;
        public FileInfo configFile;
        public DirectoryInfo source;
        public DirectoryInfo target;
        public bool archive;
        public bool encrypt;

        public void ToBeArchived(object obj, FileSystemEventArgs args)
        {
            Console.WriteLine("Archiving", args.FullPath);
            FileInfo fileToCompress = new FileInfo(args.FullPath);
            FileInfo compressedFile = new FileInfo(target.FullName + "//" + fileToCompress.Name + ".gz");
            using (FileStream originalFileStream = fileToCompress.OpenRead())
            {
                using (FileStream compressedFileStream = File.OpenWrite(compressedFile.FullName))
                {
                    using (GZipStream compressionStream = new GZipStream(compressedFileStream, CompressionMode.Compress))
                    {
                        originalFileStream.CopyTo(compressionStream);
                    }
                }
            }
        }

        public void ToBeEncrypted(object obj, FileSystemEventArgs args)
        {
            Console.WriteLine("Encrypting ", args.FullPath);
            FileInfo fileToEncrypt = new FileInfo(args.FullPath);
            FileInfo encryptedFile = new FileInfo(target.FullName + "//" + fileToEncrypt.Name + ".gz");
            using (FileStream originalFileStream = fileToEncrypt.OpenRead())
            {
                using (FileStream compressedFileStream = File.OpenWrite(encryptedFile.FullName))
                {
                    using (CryptoStream compressionStream = new CryptoStream(compressedFileStream, Rijndael.Create().CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        originalFileStream.CopyTo(compressionStream);
                    }
                }
            }
        }

    }
}
