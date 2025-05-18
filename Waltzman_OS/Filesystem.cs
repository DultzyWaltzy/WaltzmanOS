using Cosmos.Core;
using Cosmos.HAL;
using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.VFS;
using System.ComponentModel.DataAnnotations;

namespace Waltzman_OS
{
    public static class Filesystem
    {
        public static CosmosVFS vfs;

        public static void Init_FS()
        {
            vfs = new CosmosVFS();
            VFSManager.RegisterVFS(vfs);
        }
        public static void List_Files_FS()
        {
            var files = vfs.GetDirectoryListing(@"0:\");
            foreach (var file in files)
            {
                Console.WriteLine(file.mName);
            }
        }
        public static void Handle_FS_Command(String input)
        {
            if (input == "list")
            {
                List_Files_FS();
            }
        }
    }
}
