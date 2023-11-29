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
            const int CommandSpellSummonMinion = 0;
            const int CommandSpellAttackMinion = 1;
            const int CommandSpellFault = 2;

            Random random = new Random();
            float userMaxHealth = 1000;
            float userCurrentHealth = userMaxHealth;
            float percentOfHealthToHeal = 0.8f;
            float bossHealth = 5000;
            float minHealthTreshold = userMaxHealth * percentOfHealthToHeal;
            int damageSpellAttackMinion = 100;
            int healSpellFault = 250;
            int minionMaxTimeLeave = 3;
            int minionCurrentTimeLeave = 0;
            int countSpells = 3;
            int currentCastSpell;
            int bossDamage = 200;
            int moveCount = 0;
            string userSpellName1 = "Рашамон";
            string userSpellName2 = "Хуганзакура";
            string userSpellName3 = "Разлом";
            string healthBar;
            string frame;
            char delimiter = '-';
            bool isTheUserImmuneToDamage = false;
            bool isOpen = true;

            while (isOpen)
            {
                ++moveCount;
                Console.WriteLine($"\nТекущий ход {moveCount}.");
                healthBar = $"Здоровье игрока: {userCurrentHealth}\tЗдоровье босса: {bossHealth}";
                frame = new string(delimiter, healthBar.Length);
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

                if (minionCurrentTimeLeave == 0 && minHealthTreshold < userCurrentHealth)
                {
                    currentCastSpell = CommandSpellSummonMinion;
                }
                else if (minionCurrentTimeLeave != 0 && minHealthTreshold < userCurrentHealth)
                {
                    currentCastSpell = CommandSpellAttackMinion;
                }
                else if (minionCurrentTimeLeave != 0 && minHealthTreshold > userCurrentHealth)
                {
                    currentCastSpell = random.Next(CommandSpellAttackMinion, countSpells);
                }
                else
                {
                    currentCastSpell = random.Next(CommandSpellAttackMinion, countSpells);

                    if (currentCastSpell == CommandSpellAttackMinion)
                    {
                        --currentCastSpell;
                    }
                }

                switch (currentCastSpell)
                {
                    case CommandSpellSummonMinion:
                        minionCurrentTimeLeave = minionMaxTimeLeave;
                        Console.WriteLine($"Вы использует заклинанние {userSpellName1}. Вы призвали миньона");
                        break;

                    case CommandSpellAttackMinion:
                        bossHealth -= damageSpellAttackMinion;
                        Console.WriteLine($"Вы использует заклинанние {userSpellName2} и наносите по боссу {damageSpellAttackMinion} ед. урона.");
                        break;

                    case CommandSpellFault:
                        isTheUserImmuneToDamage = true;
                        userCurrentHealth += healSpellFault;
                        userCurrentHealth = userMaxHealth < userCurrentHealth ? userMaxHealth : userCurrentHealth;
                        Console.WriteLine($"Вы использует заклинанние {userSpellName3}, вы исчезаете на 1 раунд " +
                                          $"и восстанавливаете {healSpellFault} ед. здоровья.");
                        break;
                }

                if (isTheUserImmuneToDamage == false)
                {
                    userCurrentHealth -= bossDamage;
                    Console.WriteLine($"Босс наносит вам {bossDamage} ед. урона.");
                }
                else
                {
                    Console.WriteLine("Босс неможет найти вс чтобы атаковать.");
                }

                if (minionCurrentTimeLeave != 0)
                {
                    --minionCurrentTimeLeave;

                    if (minionCurrentTimeLeave == 0)
                        Console.WriteLine("Время призыва миньона закончилось.");
                    else
                        Console.WriteLine($"Миньон исчезнет через {minionCurrentTimeLeave} ход(а).");
                }
                isTheUserImmuneToDamage = false;
            }
        }
    }
}
