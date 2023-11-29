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
            string nameSpellSummonMinion = "Рашамон";
            string nameSpellAttackMinion = "Хуганзакура";
            string nameSpellFault = "Разлом";
            string healthBar;
            string frame = null;
            char delimiter = '-';
            bool isUserImmuneToDamage = false;
            bool isOpen = true;

            while (isOpen)
            {
                ++moveCount;
                Console.WriteLine($"\nТекущий ход {moveCount}.");
                healthBar = $"Здоровье игрока: {userCurrentHealth}\tЗдоровье босса: {bossHealth}";
                frame = new string(delimiter, healthBar.Length);
                Console.WriteLine($"{frame}\n{healthBar}\n{frame}");

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
                        Console.WriteLine($"Вы использует заклинанние {nameSpellSummonMinion}. Вы призвали миньона");
                        break;

                    case CommandSpellAttackMinion:
                        bossHealth -= damageSpellAttackMinion;
                        Console.WriteLine($"Вы использует заклинанние {nameSpellAttackMinion} и наносите по боссу {damageSpellAttackMinion} ед. урона.");
                        break;

                    case CommandSpellFault:
                        isUserImmuneToDamage = true;
                        userCurrentHealth += healSpellFault;
                        userCurrentHealth = userMaxHealth < userCurrentHealth ? userMaxHealth : userCurrentHealth;
                        Console.WriteLine($"Вы использует заклинанние {nameSpellFault}, вы исчезаете на 1 раунд " +
                                          $"и восстанавливаете {healSpellFault} ед. здоровья.");
                        break;
                }

                if (isUserImmuneToDamage == false)
                {
                    userCurrentHealth -= bossDamage;
                    Console.WriteLine($"Босс наносит вам {bossDamage} ед. урона.");
                }
                else
                {
                    Console.WriteLine("Босс неможет найти вас чтобы атаковать.");
                }

                if (minionCurrentTimeLeave != 0)
                {
                    --minionCurrentTimeLeave;

                    if (minionCurrentTimeLeave == 0)
                    {
                        Console.WriteLine("Время призыва миньона закончилось.");
                    }
                    else
                    {
                        Console.WriteLine($"Миньон исчезнет через {minionCurrentTimeLeave} ход(а).");
                    }
                }

                isUserImmuneToDamage = false;

                if (userCurrentHealth <= 0 || bossHealth <= 0)
                {
                    isOpen = false;
                }
            }

            Console.WriteLine(frame);

            if (userCurrentHealth <= 0 && bossHealth <= 0)
            {
                Console.WriteLine("Ничья! Игрок и босс пали одновременно.");
            }
            else if (bossHealth <= 0)
            {
                Console.WriteLine("Здоровья босса упало до 0.\nИгрок победил!");
            }
            else
            {
                Console.WriteLine("Здоровье игрока упало до 0.\nБосс победил!");
            }
        }
    }
}
