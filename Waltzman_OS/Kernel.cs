using Cosmos.Core;
using Cosmos.HAL;
using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using Cosmos.System.FileSystem;
using Cosmos.System.FileSystem.VFS;

namespace Waltzman_OS
{
    public class Kernel : Sys.Kernel
    {
        public bool confirmation(string confirm_text)
        {
            Console.Write(confirm_text);
            String confirmation = Console.ReadLine();
            String confirmation_lower = confirmation.ToLower();

            if (confirmation_lower == "y" || confirmation_lower == "yes")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected override void BeforeRun()
        {

            Console.Clear();
            Console.WriteLine("Booting...");
            Filesystem.Init_FS();
            Console.WriteLine("WaltzmanOS booted. Press Enter to Continue.");
            Console.ReadLine();
            Console.Clear();
        }

        protected override void Run()
        {
            Console.Write(Filesystem.current_directory + " $ ");
            String user_input = Console.ReadLine();
            String input_lower = user_input.ToLower();

            if (input_lower == "beep")
            {
                Console.Beep(); // Make beep noise (very needed feature)
            }

            else if (input_lower == "clear" || input_lower == "cls")
            {
                Console.Clear();
            }

            else if (input_lower == "reboot")
            {
                bool confirm = confirmation("Are you sure you want to reboot the OS? ");

                if (confirm)
                {
                    Console.WriteLine("Rebooting...");
                    CPU.Reboot();
                }
                else
                {
                    Console.WriteLine("Reboot aborted.");
                }
            }

            else if (input_lower == "shutdown")
            {
                bool shutdown_check = confirmation("Are you sure you want to shutdown your computer? ");

                if (shutdown_check)
                {
                    try
                    {
                        Console.WriteLine("Shutting down...");
                        Cosmos.System.Power.Shutdown();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Shutdown failed: " + e.Message); // Print error message
                        Console.WriteLine("Shutdown through the terminal may not be supported on your hardware.");
                        Console.WriteLine("You may need to shutdown your computer manually.");
                    }
                }
                else
                {
                    Console.WriteLine("Shutdown aborted.");
                }
            }

            else if (input_lower == "ram")
            {
                Console.WriteLine("Available RAM: " + Cosmos.Core.GCImplementation.GetAvailableRAM() + "KB");
            }

            // Final check, do NOT put anything under this else
            else
            {
                Filesystem.Handle_FS_Command(user_input); // Handle potential Filesystem input (outside func used to avoid even more else ifs)
            }
        }
    }
}
