namespace GameSkillProblem
{
    public class GameDemo
    {
        private Character _player;
        private Character _enemy;
        private List<Skill> _skills;
        private string[] _sentences;
        private Random _random;
        private int _score;
        private int _timeLimit;

        public GameDemo()
        {
            _player = new Character("Hero", 100, 100);
            _enemy = new Character("Enemy", 80, 60);
            _skills = new List<Skill>
            {
                new Skill(0, 25, 30, "enemy", "Fireball"),
                new Skill(0, 20, 0, "enemy", "Heal")
            };
            _random = new Random();
            _score = 0;
            _timeLimit = 7;
            
            _sentences = new string[]
            {
                "fireball burns enemies",
                "healing light shines bright",
                "flames consume all",
                "magic powers awaken",
                "darkness falls around",
                "light breaks through",
                "energy surges forth",
                "power grows within",
                "flames rise up high",
                "magic flows inside"
            };
        }

        public void Run()
        {
            DisplayIntro();
            
            while (true)
            {
                _player = new Character("Hero", 100, 100);
                _enemy = new Character("Enemy", 80, 60);
                _score = 0;
                
                while (_player.IsAlive() && _enemy.IsAlive())
                {
                    if (!PlayTurn())
                    {
                        break;
                    }
                }
                
                DisplayEndGame();
                
                Console.WriteLine();
                Console.Write("[1] Play Again  [0] Exit: ");
                string? choice = Console.ReadLine();
                
                if (choice != "1")
                {
                    Console.WriteLine("Thanks for playing!");
                    break;
                }
            }
        }

        private void DisplayIntro()
        {
            Console.WriteLine();
            Console.WriteLine("========================================");
            Console.WriteLine("       TYPING BATTLE GAME");
            Console.WriteLine("========================================");
            Console.WriteLine();
            Console.WriteLine("How to play:");
            Console.WriteLine("  - Select a skill (Fireball or Heal)");
            Console.WriteLine("  - Type the sentence to cast the skill");
            Console.WriteLine("  - Wrong char: -5 mana penalty");
            Console.WriteLine("  - Fireball: 30 damage, costs 25 mana");
            Console.WriteLine("  - Heal: 0 damage, costs 20 mana");
            Console.WriteLine();
            Console.WriteLine($"Time limit: {_timeLimit} seconds per turn");
            Console.WriteLine();
        }

        private void DisplayStats()
        {
            Console.WriteLine();
            Console.WriteLine($"HERO  HP: {DrawBar(_player.CurrentHP, _player.MaxHP, 20)} {_player.CurrentHP}/{_player.MaxHP}");
            Console.WriteLine($"      MP: {DrawBar(_player.CurrentMana, _player.MaxMana, 20)} {_player.CurrentMana}/{_player.MaxMana}");
            Console.WriteLine($"ENEMY HP: {DrawBar(_enemy.CurrentHP, _enemy.MaxHP, 20)} {_enemy.CurrentHP}/{_enemy.MaxHP}");
            Console.WriteLine($"      MP: {DrawBar(_enemy.CurrentMana, _enemy.MaxMana, 20)} {_enemy.CurrentMana}/{_enemy.MaxMana}");
            Console.WriteLine();
        }

        private string DrawBar(int current, int max, int length)
        {
            if (max <= 0) return new string(' ', length);
            
            int filled = (int)((double)current / max * length);
            filled = Math.Max(0, Math.Min(filled, length));
            
            return new string('#', filled) + new string('-', length - filled);
        }

        private void DisplaySkills()
        {
            Console.WriteLine("--- SKILLS ---");
            Console.WriteLine("1. Fireball - 30 damage, 25 mana");
            Console.WriteLine("2. Heal     - 0 damage, 20 mana");
            Console.WriteLine();
        }

        private bool PlayTurn()
        {
            DisplayStats();
            DisplaySkills();
            
            // Select skill
            int skillChoice = 0;
            while (skillChoice < 1 || skillChoice > 2)
            {
                Console.Write("Select skill (1-2): ");
                string? input = Console.ReadLine();
                if (int.TryParse(input, out int c) && c >= 1 && c <= 2)
                {
                    skillChoice = c;
                }
            }
            
            Skill selectedSkill = _skills[skillChoice - 1];
            
            // Check if can cast
            if (!_player.HasEnoughMana(selectedSkill.Mana))
            {
                Console.WriteLine();
                Console.WriteLine($"Not enough mana! Need {selectedSkill.Mana}, have {_player.CurrentMana}");
                
                // Lose condition: no mana
                Console.WriteLine();
                Console.WriteLine("You ran out of mana!");
                Console.WriteLine("GAME OVER!");
                return false;
            }
            
            // Typing challenge with timer
            Console.WriteLine();
            Console.WriteLine($"Cast {selectedSkill.Animation} by typing:");
            string targetSentence = _sentences[_random.Next(_sentences.Length)];
            Console.WriteLine($"  \"{targetSentence}\"");
            Console.WriteLine();
            
            Console.Write("Type here: ");
            string typed = "";
            DateTime startTime = DateTime.Now;
            bool timeout = false;
            int wrongChars = 0;
            
            while (typed != targetSentence)
            {
                // Check timeout
                TimeSpan elapsed = DateTime.Now - startTime;
                if (elapsed.TotalSeconds >= _timeLimit)
                {
                    timeout = true;
                    break;
                }
                
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    
                    if (key.Key == ConsoleKey.Backspace && typed.Length > 0)
                    {
                        typed = typed.Substring(0, typed.Length - 1);
                        Console.Write("\b \b");
                    }
                    else if (key.KeyChar != '\0')
                    {
                        char c = key.KeyChar;
                        int idx = typed.Length;
                        
                        if (idx < targetSentence.Length)
                        {
                            if (c == targetSentence[idx])
                            {
                                typed += c;
                                Console.Write(c);
                            }
                            else
                            {
                                // Wrong character - lose 5 mana
                                wrongChars++;
                                Console.WriteLine();
                                Console.WriteLine($"WRONG! -5 mana (total: {wrongChars * 5})");
                                Console.WriteLine($"  \"{targetSentence}\"");
                                Console.Write("Continue typing: " + typed);
                            }
                        }
                    }
                }
                
                Thread.Sleep(50);
            }
            
            Console.WriteLine();
            
            // Check timeout
            if (timeout)
            {
                Console.WriteLine();
                Console.WriteLine("TIME'S UP!");
                
                // Lose condition: timeout while enemy alive
                if (_enemy.IsAlive())
                {
                    Console.WriteLine("You took too long!");
                    Console.WriteLine("GAME OVER!");
                    return false;
                }
            }
            
            // Apply mana penalty for wrong chars
            if (wrongChars > 0)
            {
                _player.UseMana(wrongChars * 5);
                Console.WriteLine($"Mana penalty: -{wrongChars * 5}");
            }
            
            // Cast skill
            bool success = selectedSkill.Cast(_player, _enemy);
            
            if (success)
            {
                Console.WriteLine($"CAST SUCCESS! {selectedSkill.Animation}");
                
                if (selectedSkill.Damage > 0)
                {
                    Console.WriteLine($"Enemy takes {selectedSkill.Damage} damage!");
                }
                else
                {
                    Console.WriteLine("(Heal does 0 damage - placebo)");
                }
                
                _score++;
            }
            else
            {
                Console.WriteLine("CAST FAILED!");
            }
            
            Console.WriteLine();
            
            return true;
        }

        private void DisplayEndGame()
        {
            if (!_enemy.IsAlive())
            {
                Console.WriteLine("========================================");
                Console.WriteLine("VICTORY! You defeated the enemy!");
                Console.WriteLine($"Score: {_score}");
                Console.WriteLine("========================================");
            }
            else
            {
                Console.WriteLine("========================================");
                Console.WriteLine("GAME OVER! You were defeated!");
                Console.WriteLine($"Score: {_score}");
                Console.WriteLine("========================================");
            }
        }
    }
}
