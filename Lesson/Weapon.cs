using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;
namespace Lesson
{
    //    0 - İnformasiya almaq üçün
    //1 - Shoot metodu üçün
    //2 - Fire metodu üçün
    //3 - GetRemainBulletCount metodu üçün
    //4 - Reload metodu üçün
    //5 - ChangeFireMode metodu üçün
    //6 - Proqramdan dayandırmaq üçün
    //qısayoldur.
    //--------------------------------
    //yeni qısayol əlavə edin :
    //--------------------------------
    //7 - Edit :
    //  8 - Tutumu dəyişsin  // evvelden 30 idise indi 40 olsun
    //  9 - Güllə sayı deyissin
    public class Weapon
    {
        //Sounds Locations
        string AutoAk47Audio = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "machin-gun-auto.mp3");
        string SingleAk47Audio = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "machin-gun-single.mp3");

        string ak47 = @"                                                                                                    
                                                                                              ""     
                                         """"""""""""""""""""""I-Jc{-||c>>>>>>>>>>>>>""                  {O-    
                             jaMaaaaaaaaaaaaac||||||jjcc|||{|j>>>>>-----{cJccccJJJ|""        I---    
                           |jj|||||||j|||{|cc{{{{{{{|c{-j->>>>>-------{{-|cjjjjjcOOJJj|jjjjjOOOdJ>  
           ""I>-{{{>I>-{{-->j|||{{jdcjj||{{{{j{|j||{{{{{jj>II>>>>---{{{||jOc|||jjjJJcjjjjjjjjjcJ-""   
  -jccjjjjjjjj|||{{||||{{{-||||||||||||||{{|j{|ccJJJJJJJOj|||>I""""                                   
  |Jccccjjjjjj||||||||jjjjj{{dJcccca{|I""""{|{{{{{{|-""                                                
  -cccccccjjjjjjjjjccj-""     -|->-cc""""I  >jI{{{|{|{>                                                
  ""JJcccccccccc|-I""         ""{{--jI       "" I|{||||-I                                               
   jJJJcJc{I""               -|->j-           -{|||{j-I                                              
   I|-I                    Ij--j{            ""{{||||j{>                                             
                           ""|j|c""              {||j||||{>                                           
                                                >j|jjj|jj|>                                         
                                                 ""{||jjjjj|""                                        
                                                   ""jj||j{                                          
                                                     ""{|>                                           
                                                                                                    
                                                                                                    ";
        string info = "0 - Getting Information\r\n1 - Shoot \r\n2 - Fire \r\n3 - " +
            "Get Remain Bullet Count \r\n4 - Reload \r\n5 - Change Fire Mode\r\n" +
            "6 - Quit\r\n--------------------------------" + "\n-1. Clear" +
            "\r\n--------------------------------\r\n7 - Change Capacity or Change Current Ammo\n------------------";
        int _bulletCapacity;
        int _bulletCount;
        public int BulletCapacity
        {
            get => _bulletCapacity;
            set
            {
                if (value > 0 && value >= BulletCount) { _bulletCapacity = value; }               
            }
        }
        public int BulletCount
        {
            get => _bulletCount;
            set
            {
                if (value >= 0 && value <= BulletCapacity)
                {
                    _bulletCount = value;
                }
                if(value > BulletCapacity)
                {
                    throw new NotCorrectBulletCountInputException("You cant enter bullet count greater than its capacity");
                }
            }
        }
        public enum FireMode { Single, Auto, Burst }
        public FireMode fmode;
        public Weapon(int bulletCapacity, int bulletCount, FireMode fireMode)
        {
            BulletCapacity = bulletCapacity;
            BulletCount = bulletCount;
            fmode = fireMode;
        }
        public void Shoot()
        {
           Console.Clear();
            Console.WriteLine(ak47);
            
            if (BulletCount > 0)
            {
                if (fmode == FireMode.Single)
                {
                    BulletCount--;
                    PlayAudio(SingleAk47Audio);
                }
                else if (fmode == FireMode.Auto)
                {
                    BulletCount = 0;
                    PlayAudio(AutoAk47Audio);

                }
                else if (fmode == FireMode.Burst && BulletCount > 2)
                {
                    BulletCount -= 3;
                    PlayAudio(AutoAk47Audio);

                }
                else if (fmode == FireMode.Burst && BulletCount < 3)
                {
                    throw new NotEnougBulletException("You dont have enough bullet");
                }
            }
            else
            {
                throw new NotEnougBulletException("Your bullet is zero!");
            }
            GoToMainMenu(1);
        }
        public void Fire()
        {
            Console.Clear();
            Console.WriteLine(ak47);

            if (BulletCount > 0)
            {
                BulletCount = 0;
                PlayAudio(AutoAk47Audio);
            }
            else
            {
                throw new NotEnougBulletException("Your ammo is zero.");
            }
            GoToMainMenu(1);
+
        }
        public int GetRemainBulletCount()
        {
            return BulletCount;

        }
        public void Reload()
        {
            BulletCount = BulletCapacity;
            Console.WriteLine($"Was reloaded your ammo:{BulletCount}/{BulletCapacity}");
        }
        public void ChangeFireMode()
        {

         bool changed = false;
            Console.Clear();
            Console.WriteLine($"Select Fire Mode(Current is {fmode}):");
            Console.WriteLine("1.Single");
            Console.WriteLine("2.Burst");
            Console.WriteLine("3.Auto");

            do
            {

                try
                {
                    int input = int.Parse(Console.ReadLine());
                    switch (input)
                    {
                        case 1:
                            fmode = FireMode.Single;
                            Console.Clear();
                            Console.WriteLine($"was selected successfully:{fmode}");
                            changed = true;
                            break;
                        case 2:
                            fmode = FireMode.Burst;
                            Console.Clear();
                            Console.WriteLine($"was selected successfully:{fmode}");
                            changed = true;
                            break;
                        case 3:
                            fmode = FireMode.Auto;
                            Console.Clear();
                            Console.WriteLine($"was selected successfully:{fmode}");
                            changed = true;
                            break;

                        default:
                            throw new InvalidOptionSelectionException("You can select only 1,2 or 3. ");
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
                }
                catch (NullReferenceException e)
                {
                    Console.WriteLine("Do not enter null or empty input");
                }
                catch (FormatException e)
                {
                    Console.WriteLine("You can only select 1,2,3");
                }
            } while (!changed);


            GoToMainMenu(1.4d);

        }
        void PlayAudio(string audioFilePath)
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
        void GoToMainMenu(double time)
        {
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
