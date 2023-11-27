using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace L19_bossFight
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int CommandSpell1 = 0;
            const int CommandSpell2 = 1;
            const int CommandSpell3 = 2;

            Random random = new Random();
            float userMaxHealth = 1000;
            float userCurrentHealth = userMaxHealth;
            float percentOfHealthToHeal = 0.8f;
            float bossHealth = 5000;
            int userDamageSpell2 = 100;
            int userHealSpell3 = 250;
            int minionMaxTimeLeave = 3;
            int minionCurrentTimeLeave = 0;
            int countSpells = 3;
            int castSpell;
            int bossDamage = 200;
            int stringLenght;
            int moveCount = 0;
            string userSpellName1 = "Рашамон";
            string userSpellName2 = "Хуганзакура";
            string userSpellName3 = "Разлом";
            string healthBar;
            string frame;
            char delimiter = '-';
            bool userImmunityToDamage = false;
            bool isOpen = true;

            while (isOpen)
            {
                ++moveCount;
                Console.WriteLine($"\nТекущий ход {moveCount}.");
                healthBar = $"Здоровье игрока: {userCurrentHealth}\tЗдоровье босса: {bossHealth}";
                stringLenght = healthBar.Length;
                frame = new string(delimiter, stringLenght);
                Console.WriteLine($"{frame}\n{healthBar}\n{frame}");

                if (userCurrentHealth <= 0 || bossHealth <= 0)
                {
                    if (userCurrentHealth <= 0 && bossHealth <= 0)
                    {
                        userCurrentHealth = 0;
                        bossHealth = 0;
                        Console.WriteLine("Ничья! Игрок и босс пали одновременно.");
                    }
                    else if (bossHealth <= 0)
                    {
                        bossHealth = 0;
                        Console.WriteLine("Игрок победил!");
                    }
                    else
                    {
                        userCurrentHealth = 0;
                        Console.WriteLine("Босс победил!");
                    }
                    isOpen = false;
                    continue;
                }
                if (minionCurrentTimeLeave == 0 && (userMaxHealth * percentOfHealthToHeal) < userCurrentHealth)
                    castSpell = CommandSpell1;
                else if (minionCurrentTimeLeave != 0 && (userMaxHealth * percentOfHealthToHeal) < userCurrentHealth)
                    castSpell = CommandSpell2;
                else if (minionCurrentTimeLeave != 0 && (userMaxHealth * percentOfHealthToHeal) > userCurrentHealth)
                    castSpell = random.Next(CommandSpell2, countSpells);
                else
                {
                    castSpell = random.Next(CommandSpell2, countSpells);

                    if (castSpell == CommandSpell2)
                        --castSpell;
                }
                switch (castSpell)
                {
                    case CommandSpell1:
                        minionCurrentTimeLeave = minionMaxTimeLeave;
                        Console.WriteLine($"Вы использует заклинанние {userSpellName1}. Вы призвали миньона");
                        break;

                    case CommandSpell2:
                        bossHealth -= userDamageSpell2;
                        Console.WriteLine($"Вы использует заклинанние {userSpellName2} и наносите по боссу {userDamageSpell2} ед. урона.");
                        break;

                    case CommandSpell3:
                        userImmunityToDamage = true;
                        userCurrentHealth += userHealSpell3;
                        userCurrentHealth = userMaxHealth < userCurrentHealth ? userMaxHealth : userCurrentHealth;
                        Console.WriteLine($"Вы использует заклинанние {userSpellName3}, вы исчезаете на 1 раунд " +
                                          $"и восстанавливаете {userHealSpell3} ед. здоровья.");

                        break;
                }
                if (!userImmunityToDamage)
                {
                    userCurrentHealth -= bossDamage;
                    Console.WriteLine($"Босс наносит вам {bossDamage} ед. урона.");
                }
                else
                    Console.WriteLine("Босс неможет найти вс чтобы атаковать.");

                if (minionCurrentTimeLeave != 0)
                {
                    --minionCurrentTimeLeave;

                    if (minionCurrentTimeLeave == 0)
                        Console.WriteLine("Время призыва миньона закончилось.");
                    else
                        Console.WriteLine($"Миньон исчезнет через {minionCurrentTimeLeave} ход(а).");
                }
                userImmunityToDamage = false;
            }
        }
    }
}
