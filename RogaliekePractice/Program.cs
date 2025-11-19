using System;

class SimpleRoguelike
{
    static Random random = new Random();

    static void Main()
    {
        Console.WriteLine("I--0-0-0-0-0 ROGALIK RUSSIA 0-0-0-0-0--I");


        int hp = 100;
        int attack = 10;
        int defense = 5;
        int turn = 0;

        while (hp > 0)
        {
            turn++;
            Console.WriteLine($"\nХод {turn} | HP: {hp} | Атака: {attack} | Защита: {defense}");

            if (turn % 10 == 0)
            {
                Console.WriteLine("ПОЯВИЛСЯ БОСС");
                FightBoss(ref hp, attack, defense);
            }
            else if (random.Next(2) == 0)
            {
                Console.WriteLine("Найден сундук!");
                OpenChest(ref hp, ref attack, ref defense);
            }
            else
            {
                Console.WriteLine("Появился враг!");
                FightEnemy(ref hp, attack, defense);
            }
        }

        Console.WriteLine($"\nИгра окончена. Пройдено ходов: {turn}");
    }

    static void OpenChest(ref int hp, ref int attack, ref int defense)

    {
        int item = random.Next(3);

        if (item == 0)

        {
            hp = 100;
            Console.WriteLine("Зелье лечения! HP восстановлено");
        }

        else if (item == 1)

        {
            int newAttack = random.Next(8, 16);
            Console.WriteLine($"Новое оружие! Атака: {newAttack} (было: {attack})");
            Console.Write("Взять? (д/н): ");

            if (Console.ReadLine() == "д")
            {
                attack = newAttack;
                Console.WriteLine($"Оружие экипировано, новая атака {attack}");
            }
            else if (Console.ReadLine() == "н")
            {
                Console.WriteLine($"Лут (оружие) пропущено, осталась старая атака {attack}");
            }
        }

        else

        {
            int newDefense = random.Next(4, 11);
            Console.WriteLine($"Новая броня! Защита: {newDefense} (было: {defense})");
            Console.Write("Взять? (д/н): ");
            if (Console.ReadLine() == "д")
            {
                defense = newDefense;
                Console.WriteLine($"Броня экипирована, новый параметр брони: {defense}");
            }
            else if (Console.ReadLine() == "н")
            {
                Console.WriteLine($"Лут (броня) пропущена, остался старый параметр брони: {defense}");
            }

        }
    }

    static void FightEnemy(ref int hp, int attack, int defense)
    {
        string[] enemies = { "Гоблин", "Скелет", "Маг" };
        string enemy = enemies[random.Next(3)];
        int enemyHP = random.Next(20, 35);
        int enemyAttack = random.Next(6, 12);

        Console.WriteLine($"{enemy} (HP: {enemyHP}, Атака: {enemyAttack})");
        Fight(ref hp, attack, defense, enemy, enemyHP, enemyAttack);
    }

    static void FightBoss(ref int hp, int attack, int defense)
    {
        string[] bosses = { "ВВГ", "Ковальский", "Архимаг C++", "Пестов С--" };
        string boss = bosses[random.Next(4)];

        int bossHP, bossAttack;

        switch (boss)
        {
            case "ВВГ":
                bossHP = 80;
                bossAttack = 18;
                Console.WriteLine($"{boss} (HP: {bossHP}, Атака: {bossAttack}) \n Особенность: Крит +10%");
                break;
            case "Ковальский":
                bossHP = 100;
                bossAttack = 16;
                Console.WriteLine($"{boss} (HP: {bossHP}, Атака: {bossAttack}) \n Особенность: Игнорирвание брони");
                break;
            case "Архимаг C++":
                bossHP = 70;
                bossAttack = 19;
                Console.WriteLine($"{boss} (HP: {bossHP}, Атака: {bossAttack}) \n Особенность: Заморозка +10% (пропуск хода)");
                break;
            default:
                bossHP = 50;
                bossAttack = 22;
                Console.WriteLine($"{boss} (HP: {bossHP}, Атака: {bossAttack}) \n Особенность: Игнорирование брони + заморозка (пропуск хода)");
                break;
        }

        Fight(ref hp, attack, defense, boss, bossHP, bossAttack);
    }

    static void Fight(ref int hp, int attack, int defense, string enemy, int enemyHP, int enemyAttack)
    {
        bool frozen = false;

        while (enemyHP > 0 && hp > 0)
        {
            if (frozen)
            {
                Console.WriteLine("Заморожен! Пропускаете ход");
                frozen = false;
            }
            else
            {
                Console.Write("Атака - А или Защита - Б? ");
                string choice = Console.ReadLine();

                if (choice == "А")
                {
                    enemyHP -= attack;
                    Console.WriteLine($"Вы нанесли {attack} урона");
                }
                else
                {
                    if (random.Next(100) < 40)
                    {
                        Console.WriteLine("Уклонение! Враг промахнулся");
                        continue;
                    }
                    else
                    {
                        int block = defense * random.Next(70, 101) / 100;
                        Console.WriteLine($"Блокировано {block} урона");
                    }
                }
            }

            if (enemyHP <= 0)
            {
                Console.WriteLine($"{enemy} побежден!");
                break;
            }

            int damage = enemyAttack;

            if (enemy == "Гоблин")
            {
                if (random.Next(100) < 20)
                {
                    damage *= 2;
                    Console.WriteLine("Критический урон!");
                }
            }
            else if (enemy == "ВВГ")
            {
                if (random.Next(100) < 30)
                {
                    damage *= 2;
                    Console.WriteLine("Критический урон!");
                }
            }
            else if (enemy == "Скелет" || enemy == "Ковальский" || enemy == "Пестов С--")
            {
                Console.WriteLine("Враг игнорирует вашу защиту!");
            }
            else
            {
                damage = 1;
            }

            if ((enemy == "Маг" && random.Next(100) < 25) ||
                (enemy == "Архимаг C++" && random.Next(100) < 35) ||
                (enemy == "Пестов С--" && random.Next(100) < 40))
            {
                frozen = true;
                Console.WriteLine("Враг заморозил вас!");
            }

            hp = hp - damage;
            Console.WriteLine($"{enemy} нанес {damage} урона");

            if (hp <= 0) break;
        }
    }
}