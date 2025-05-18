using Cosmos.Core;
using Cosmos.HAL;
using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.VFS;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace Waltzman_OS
{
    public class Filesystem
    {
        public static string current_directory = @"0:\";
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
            if(input.StartsWith("del "))
            {
                try
                {
                    File.Delete(current_directory + input.Substring(4));
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Deletion failed: " + ex.Message);
                }
            }
            if(input.StartsWith("deldir "))
            {
                Console.Write("Are you sure you want to delete this directory? ");
                String confirm_dir = Console.ReadLine();
                String confirm_dir_lower = confirm_dir.ToLower();

                if (confirm_dir_lower == "y" || confirm_dir_lower == "yes")
                {
                    try
                    {
                        Directory.Delete(current_directory + input.Substring(7));
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Deletion failed: " + e.Message);
                    }
                }
                else
                {
                    Console.WriteLine("Deletion aborted.");
                }
            }
        }
    }
}
