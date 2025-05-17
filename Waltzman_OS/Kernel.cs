using Cosmos.Core;
using Cosmos.HAL;
using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;

namespace Waltzman_OS
{
    public class Kernel : Sys.Kernel
    {

        protected override void BeforeRun()
        {
            Console.Clear();
            Console.WriteLine("WaltzmanOS booted. Press Enter to Continue.");
            Console.ReadLine();
            Console.Clear();
        }

        protected override void Run()
        {
            Console.Write("-> ");
            String user_input = Console.ReadLine();
            String input_lower = user_input.ToLower();

            if (input_lower == "beep")
            {
                Console.Beep(); //make beep noise (very needed feature)
            }

            else if (input_lower == "clear" || input_lower == "cls")
            {
                Console.Clear();
            }

            else if (input_lower == "reboot")
            {
                Console.Write("Are you sure you want to reboot the OS? ");
                String confirm = Console.ReadLine();
                String confirm_lower = confirm.ToLower();

                if (confirm_lower == "y" || confirm_lower == "yes")
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
                Console.Write("Are you sure you want to shutdown your computer? ");
                String confirm_shutdown = Console.ReadLine();
                String confirm_shutdown_lower = confirm_shutdown.ToLower();

                if (confirm_shutdown_lower == "y" || confirm_shutdown_lower == "yes")
                {
                    try
                    {
                        Console.WriteLine("Shutting down...");
                        Cosmos.System.Power.Shutdown();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Shutdown failed: " + e.Message); //print error message
                        Console.WriteLine("Shutdown through the terminal may not be supported on your hardware.");
                        Console.WriteLine("You may need to shutdown your computer manually.");
                    }
                }
                else
                {
                    Console.WriteLine("Shutdown aborted.");
                }
            }
            else if(input_lower == "ram")
            {
                Console.WriteLine("Available RAM: " + Cosmos.Core.GCImplementation.GetAvailableRAM());
                Console.WriteLine("Total RAM: " + Cosmos.Core.CPU.GetAmountOfRAM);
                Console.WriteLine("CPU Vendor: " + Cosmos.Core.CPU.GetCPUVendorName);

            }
        }
    }
}
