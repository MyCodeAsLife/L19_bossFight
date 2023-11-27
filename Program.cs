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
            string userSpellName1 = "Рашамон";      // Вызов миньона
            string userSpellName2 = "Хуганзакура";  // Скилл\атака миньона
            string userSpellName3 = "Разлом";       // Лечение
            float userMaxHealth = 1000;
            float userCurrentHealth = userMaxHealth;
            int userDamageSpell2 = 100;
            int userHealSpell3 = 250;
            int minionMaxTimeLeave = 3;
            int minionCurrentTimeLeave = 0;
            float percentOfHealthToHeal = 0.8f;
            int countSpells = 3;
            int castSpell;
            float bossHealth = 5000;
            int bossDamage = 200;
            int stringLenght;
            string healthBar = $"Здоровье игрока: {userCurrentHealth}\tЗдоровье босса: {bossHealth}";
            char delimiter = '-';
            bool userImmunityToDamage = false;
            bool isOpen = true;
            // Вписать текст нанесения ударов
            while (isOpen)
            {
                stringLenght = healthBar.Length;
                //Console.Clear();
                Console.WriteLine(healthBar + '\n' + new string(delimiter, stringLenght));

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
                        break;

                    case CommandSpell2:
                        bossHealth -= userDamageSpell2;
                        break;

                    case CommandSpell3:
                        userImmunityToDamage = true;
                        userCurrentHealth += userHealSpell3;
                        userCurrentHealth = userMaxHealth < userCurrentHealth ? userMaxHealth : userCurrentHealth;
                        break;
                }
                if (!userImmunityToDamage)
                {
                    userCurrentHealth -= bossDamage;
                    if (userCurrentHealth < 0)
                        userCurrentHealth = 0;
                }

                if (minionCurrentTimeLeave != 0)
                    --minionCurrentTimeLeave;
                userImmunityToDamage = false;
            }
        }
    }
}
