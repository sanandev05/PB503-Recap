using System.Threading.Channels;
using NAudio.Wave;
namespace Lesson
{
    public class Program
    {
        
        static void Main(string[] args)
        {

            string Author = @"┌────────────────────────────────────────────────────────────────────────────────────────────┐
│  _             ____                            _   _                                       │
│ | |__  _   _  / ___|  __ _ _ __   __ _ _ __   | | | |_   _ ___  ___ _   _ _ __   _____   __│
│ | '_ \| | | | \___ \ / _` | '_ \ / _` | '_ \  | |_| | | | / __|/ _ \ | | | '_ \ / _ \ \ / /│
│ | |_) | |_| |  ___) | (_| | | | | (_| | | | | |  _  | |_| \__ \  __/ |_| | | | | (_) \ V / │
│ |_.__/ \__, | |____/ \__,_|_| |_|\__,_|_| |_| |_| |_|\__,_|___/\___|\__, |_| |_|\___/ \_/  │
│        |___/                                                        |___/                  │
└────────────────────────────────────────────────────────────────────────────────────────────┘";
            Console.WriteLine(Author);
            string info = "0 - Getting Information\r\n1 - Shoot \r\n2 - Fire \r\n3 - " +
            "Get Remain Bullet Count \r\n4 - Reload \r\n5 - Change Fire Mode\r\n" +
            "6 - Quit\r\n--------------------------------" + "\n-1. Clear" +
            "\r\n--------------------------------\r\n7 - Change Capacity or Change Current Ammo\n------------------";
            Weapon weapon = new Weapon(33, 22, Weapon.FireMode.Single);
            Console.WriteLine(info);
            Console.WriteLine("");
            string MenuSelectionAudioFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "gta-menu.wav");

            int input = 1;
            do
            {
                try
                {
                    input = int.Parse(Console.ReadLine());
                    switch (input)
                    {
                        case 0:

                            Console.WriteLine($"{weapon.BulletCount}/{weapon.BulletCapacity} {weapon.fmode}\n");
                            break;
                        case -1:
                            Console.Clear();
                            Console.WriteLine(info);

                            PlayAudio(MenuSelectionAudioFilePath);
                            break;
                        case 1:
                            weapon.Shoot();
                            Console.WriteLine("Shooted and current:" + weapon.GetRemainBulletCount() + "/" + weapon.BulletCapacity);
                            PlayAudio(MenuSelectionAudioFilePath);
                            break;
                        case 2:
                            weapon.Fire();
                            Console.WriteLine("Fired and current:" + weapon.GetRemainBulletCount() + "/" + weapon.BulletCapacity);
                            PlayAudio(MenuSelectionAudioFilePath);
                            break;
                        case 3:
                            Console.WriteLine("Remain Buller Count:" + weapon.GetRemainBulletCount() + "/" + weapon.BulletCapacity);
                            PlayAudio(MenuSelectionAudioFilePath);
                            break;
                        case 4:
                            weapon.Reload();
                            PlayAudio(MenuSelectionAudioFilePath);
                            break;
                        case 5:
                            Console.WriteLine("Changing Fire Mode:");
                            weapon.ChangeFireMode();
                            PlayAudio(MenuSelectionAudioFilePath);
                            break;
                        case 6:
                            PlayAudio(MenuSelectionAudioFilePath);
                            break;
                        case 7:
                            Console.Clear();
                            // Console.WriteLine("Edit Mode:\r\n1 - Change Capacity\r\n2 - Change Current Ammo");
                            int mode = 0;
                            bool changed = false;
                            PlayAudio(MenuSelectionAudioFilePath);
                            do
                            {
                                try
                                {
                                    Console.WriteLine("Edit Mode:\r\n1 - Change Capacity\r\n2 - Change Current Ammo");
                                    mode = int.Parse(Console.ReadLine());

                                    switch (mode)
                                    {
                                        case 1:
                                            int capacity;
                                            Console.Write($"Enter Capacity:");
                                            capacity = int.Parse(Console.ReadLine());
                                            while (capacity <= 0)
                                            {
                                                Console.Write($"Capacity can not be below 0!");
                                                capacity = int.Parse(Console.ReadLine());
                                            }
                                            weapon.BulletCapacity = capacity;
                                            Console.WriteLine("Bullet Capacity successfully changed");
                                            changed = true;
                                            break;
                                        case 2:
                                            int count;
                                            Console.Write($"Enter Count:");
                                            count = int.Parse(Console.ReadLine());
                                            while (count <= 0)
                                            {
                                                Console.Write($"Count can not be below 0!");
                                                count = int.Parse(Console.ReadLine());
                                            }
                                            weapon.BulletCount = count;
                                            Console.WriteLine("Bullet Count successfully changed");
                                            changed = true;

                                            break;

                                    }
                                }
                                catch (NullReferenceException e) { Console.WriteLine("\n" + e.Message + "\n"); }
                                catch (FormatException e) { Console.WriteLine("\n" + e.Message + "\n"); }
                                catch (NotCorrectBulletCountInputException e) { Console.WriteLine("\n" + e.Message + "\n"); }
                            } while (!changed);
                            Console.Clear();
                            Console.WriteLine(info);
                            break;
                        default:
                            Console.WriteLine("You can select only -1 0 1 2 3 4 5 6 7");
                            break;

                    }
                }
                catch (InvalidOptionSelectionException e)
                {
                    Console.WriteLine(e.Message);
                }
                catch (NotEnougBulletException e)
                {
                    Console.WriteLine(e.Message);
                    GoToMainMenu(1.4d);
                }
                catch (NullReferenceException e)
                {
                    Console.WriteLine("Do not enter null or empty input");
                }
                catch (FormatException e)
                {
                    Console.WriteLine("You can only select 0,1,2,3,4,5,6,7,8,9");
                }

            } while (input != 6);


        }

        static void PlayAudio(string audioFilePath)
        {
            try
            {
                using (var audioFile = new AudioFileReader(audioFilePath))
                using (var outputDevice = new WaveOutEvent())
                {
                    outputDevice.Init(audioFile);
                    outputDevice.Play();
                    while (outputDevice.PlaybackState == PlaybackState.Playing)
                    {
                        System.Threading.Thread.Sleep(0); // Keep console running
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error playing audio: {ex.Message}");
            }
        }
        static void GoToMainMenu(double time)
        {
            string info = "0 - Getting Information\r\n1 - Shoot \r\n2 - Fire \r\n3 - " +
            "Get Remain Bullet Count \r\n4 - Reload \r\n5 - Change Fire Mode\r\n" +
            "6 - Quit\r\n--------------------------------" + "\n-1. Clear" +
            "\r\n--------------------------------\r\n7 - Change Capacity or Change Current Ammo\n------------------";
            int start = int.Parse(DateTime.Now.Second.ToString());
            double difference = 0;
            Console.WriteLine("Loading  ...");
            do
            {
                difference = Math.Abs(double.Parse(DateTime.Now.Second.ToString()) - start);
            } while (difference <= time);
            Console.Clear();
            Console.WriteLine(info);
        }

    }
}
