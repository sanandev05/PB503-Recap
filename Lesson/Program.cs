namespace Lesson
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Weapon weapon = new Weapon(33, 22, Weapon.FireMode.Single);
            Console.WriteLine("0 - İnformasiya almaq üçün\r\n1 - Shoot metodu üçün\r\n2 - Fire metodu üçün\r\n3 - GetRemainBulletCount metodu üçün\r\n4 - Reload metodu üçün\r\n5 - ChangeFireMode metodu üçün\r\n6 - Proqramdan dayandırmaq üçün\r\nqısayoldur.");
            int input = 1;
            do
            {
                 input = int.Parse(Console.ReadLine());


                switch (input)
                {
                    case 0:
                        Console.WriteLine($"{weapon.BulletCapacity} {weapon.BulletCount} {weapon.fmode}\n");
                        break;
                    case 1:
                        weapon.Shoot();
                        Console.WriteLine("Shooted and current:"+weapon.BulletCount);
                        break;
                    case 2:
                        weapon.Fire();
                        Console.WriteLine("Fired and current:" + weapon.BulletCount);
                        break;
                    case 3:
                        Console.WriteLine("Remain Buller Count" + weapon.GetRemainBulletCount());                        
                        break;
                    case 4:
                        weapon.Reload();
                        Console.WriteLine("Was Reloaded "+weapon.BulletCount);
                        break;
                }
            } while (input != -1);

        }

    }
}
