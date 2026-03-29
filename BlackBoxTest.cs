using Xunit;

namespace GameSkillProblem.Tests
{
    public class BlackBoxTest
    {
        #region === CHARACTER - IsAlive() ===

        [Fact]
        public void C002_IsAlive_WithZeroHP_ReturnsFalse()
        {
            var character = new Character("Hero", 0, 100);
            Assert.False(character.IsAlive());
        }

        [Fact]
        public void C003_IsAlive_WithOneHP_ReturnsTrue()
        {
            var character = new Character("Hero", 1, 100);
            Assert.True(character.IsAlive());
        }

        [Fact]
        public void C004_IsAlive_WithFiftyHP_ReturnsTrue()
        {
            var character = new Character("Hero", 50, 100);
            Assert.True(character.IsAlive());
        }

        [Fact]
        public void C005_IsAlive_WithNinetyNineHP_ReturnsTrue()
        {
            var character = new Character("Hero", 99, 100);
            Assert.True(character.IsAlive());
        }

        [Fact]
        public void C006_IsAlive_WithMaxHP_ReturnsTrue()
        {
            var character = new Character("Hero", 100, 100);
            Assert.True(character.IsAlive());
        }

        #endregion

        #region === CHARACTER - TakeDamage() ===

        [Fact]
        public void C009_TakeDamage_WithZeroDamage_NoChange()
        {
            var character = new Character("Hero", 100, 100);
            character.TakeDamage(0);
            Assert.Equal(100, character.CurrentHP);
        }

        [Fact]
        public void C010_TakeDamage_WithOneDamage_HPDecreasesByOne()
        {
            var character = new Character("Hero", 100, 100);
            character.TakeDamage(1);
            Assert.Equal(99, character.CurrentHP);
        }

        [Fact]
        public void C011_TakeDamage_WithFiftyDamage_HPDecreasesByFifty()
        {
            var character = new Character("Hero", 100, 100);
            character.TakeDamage(50);
            Assert.Equal(50, character.CurrentHP);
        }

        [Fact]
        public void C012_TakeDamage_WithNinetyNineDamage_HPBecomesOne()
        {
            var character = new Character("Hero", 100, 100);
            character.TakeDamage(99);
            Assert.Equal(1, character.CurrentHP);
        }

        [Fact]
        public void C013_TakeDamage_WithMaxDamage_HPBecomesZero()
        {
            var character = new Character("Hero", 100, 100);
            character.TakeDamage(100);
            Assert.Equal(0, character.CurrentHP);
        }

        [Fact]
        public void C014_TakeDamage_WithMoreThanMaxDamage_HPBecomesZeroNotNegative()
        {
            var character = new Character("Hero", 100, 100);
            character.TakeDamage(101);
            Assert.Equal(0, character.CurrentHP);
        }

        [Fact]
        public void C014b_TakeDamage_WithIntMaxValue_HPBecomesZero()
        {
            var character = new Character("Hero", 100, 100);
            character.TakeDamage(int.MaxValue);
            Assert.Equal(0, character.CurrentHP);
        }

        #endregion

        #region === CHARACTER - Heal() ===

        [Fact]
        public void C016_Heal_WithZeroAmount_NoChange()
        {
            var character = new Character("Hero", 50, 100);
            character.Heal(0);
            Assert.Equal(50, character.CurrentHP);
        }

        [Fact]
        public void C017_Heal_WithOneAmount_HPIncreasesByOne()
        {
            var character = new Character("Hero", 100, 100);
            character.TakeDamage(50);
            Assert.Equal(50, character.CurrentHP);
            character.Heal(1);
            Assert.Equal(51, character.CurrentHP);
        }

        [Fact]
        public void C018_Heal_WithTwentyFiveAmount_HPIncreasesByTwentyFive()
        {
            var character = new Character("Hero", 100, 100);
            character.TakeDamage(50);
            Assert.Equal(50, character.CurrentHP);
            character.Heal(25);
            Assert.Equal(75, character.CurrentHP);
        }

        [Fact]
        public void C019_Heal_WithFortyNineAmount_HPBecomesNinetyNine()
        {
            var character = new Character("Hero", 100, 100);
            character.TakeDamage(50);
            Assert.Equal(50, character.CurrentHP);
            character.Heal(49);
            Assert.Equal(99, character.CurrentHP);
        }

        [Fact]
        public void C020_Heal_WithFiftyAmount_HPBecomesMax()
        {
            var character = new Character("Hero", 100, 100);
            character.TakeDamage(50);
            Assert.Equal(50, character.CurrentHP);
            character.Heal(50);
            Assert.Equal(100, character.CurrentHP);
        }

        [Fact]
        public void C021_Heal_WithMoreThanNeeded_HPDoesNotExceedMax()
        {
            var character = new Character("Hero", 100, 100);
            character.TakeDamage(50);
            Assert.Equal(50, character.CurrentHP);
            character.Heal(51);
            Assert.Equal(100, character.CurrentHP);
        }

        [Fact]
        public void C021b_Heal_WithIntMaxValue_HPDoesNotExceedMax()
        {
            var character = new Character("Hero", 100, 100);
            character.TakeDamage(50);
            Assert.Equal(50, character.CurrentHP);
            character.Heal(int.MaxValue);
            Assert.Equal(100, character.CurrentHP);
        }

        #endregion

        #region === CHARACTER - HasEnoughMana() ===

        [Fact]
        public void C023_HasEnoughMana_WithZeroCost_ReturnsTrue()
        {
            var character = new Character("Hero", 100, 100);
            Assert.True(character.HasEnoughMana(0));
        }

        [Fact]
        public void C024_HasEnoughMana_WithOneCost_ReturnsTrue()
        {
            var character = new Character("Hero", 100, 100);
            Assert.True(character.HasEnoughMana(1));
        }

        [Fact]
        public void C025_HasEnoughMana_WithFiftyCost_ReturnsTrue()
        {
            var character = new Character("Hero", 100, 100);
            Assert.True(character.HasEnoughMana(50));
        }

        [Fact]
        public void C026_HasEnoughMana_WithNinetyNineCost_ReturnsTrue()
        {
            var character = new Character("Hero", 100, 100);
            Assert.True(character.HasEnoughMana(99));
        }

        [Fact]
        public void C027_HasEnoughMana_WithMaxMana_ReturnsTrue()
        {
            var character = new Character("Hero", 100, 100);
            Assert.True(character.HasEnoughMana(100));
        }

        [Fact]
        public void C028_HasEnoughMana_WithMoreThanAvailable_ReturnsFalse()
        {
            var character = new Character("Hero", 100, 100);
            Assert.False(character.HasEnoughMana(101));
        }

        #endregion

        #region === CHARACTER - UseMana() ===

        [Fact]
        public void C030_UseMana_WithZeroCost_NoChange()
        {
            var character = new Character("Hero", 100, 100);
            character.UseMana(0);
            Assert.Equal(100, character.CurrentMana);
        }

        [Fact]
        public void C031_UseMana_WithOneCost_ManaDecreasesByOne()
        {
            var character = new Character("Hero", 100, 100);
            character.UseMana(1);
            Assert.Equal(99, character.CurrentMana);
        }

        [Fact]
        public void C032_UseMana_WithFiftyCost_ManaDecreasesByFifty()
        {
            var character = new Character("Hero", 100, 100);
            character.UseMana(50);
            Assert.Equal(50, character.CurrentMana);
        }

        [Fact]
        public void C033_UseMana_WithNinetyNineCost_ManaBecomesOne()
        {
            var character = new Character("Hero", 100, 100);
            character.UseMana(99);
            Assert.Equal(1, character.CurrentMana);
        }

        [Fact]
        public void C034_UseMana_WithMaxMana_ManaBecomesZero()
        {
            var character = new Character("Hero", 100, 100);
            character.UseMana(100);
            Assert.Equal(0, character.CurrentMana);
        }

        [Fact]
        public void C035_UseMana_WithMoreThanAvailable_NoChange()
        {
            var character = new Character("Hero", 100, 100);
            character.UseMana(101);
            Assert.Equal(100, character.CurrentMana);
        }

        [Fact]
        public void C035b_UseMana_WithIntMaxValue_NoChange()
        {
            var character = new Character("Hero", 100, 100);
            character.UseMana(int.MaxValue);
            Assert.Equal(100, character.CurrentMana);
        }

        #endregion

        #region === CHARACTER - RestoreMana() ===

        [Fact]
        public void C037_RestoreMana_WithZeroAmount_NoChange()
        {
            var character = new Character("Hero", 100, 50);
            character.RestoreMana(0);
            Assert.Equal(50, character.CurrentMana);
        }

        [Fact]
        public void C038_RestoreMana_WithOneAmount_ManaIncreasesByOne()
        {
            var character = new Character("Hero", 100, 100);
            character.UseMana(50);
            Assert.Equal(50, character.CurrentMana);
            character.RestoreMana(1);
            Assert.Equal(51, character.CurrentMana);
        }

        [Fact]
        public void C039_RestoreMana_WithTwentyFiveAmount_ManaIncreasesByTwentyFive()
        {
            var character = new Character("Hero", 100, 100);
            character.UseMana(50);
            Assert.Equal(50, character.CurrentMana);
            character.RestoreMana(25);
            Assert.Equal(75, character.CurrentMana);
        }

        [Fact]
        public void C040_RestoreMana_WithFortyNineAmount_ManaBecomesNinetyNine()
        {
            var character = new Character("Hero", 100, 100);
            character.UseMana(50);
            Assert.Equal(50, character.CurrentMana);
            character.RestoreMana(49);
            Assert.Equal(99, character.CurrentMana);
        }

        [Fact]
        public void C041_RestoreMana_WithFiftyAmount_ManaBecomesMax()
        {
            var character = new Character("Hero", 100, 100);
            character.UseMana(50);
            Assert.Equal(50, character.CurrentMana);
            character.RestoreMana(50);
            Assert.Equal(100, character.CurrentMana);
        }

        [Fact]
        public void C042_RestoreMana_WithMoreThanNeeded_ManaDoesNotExceedMax()
        {
            var character = new Character("Hero", 100, 100);
            character.UseMana(50);
            Assert.Equal(50, character.CurrentMana);
            character.RestoreMana(51);
            Assert.Equal(100, character.CurrentMana);
        }

        [Fact]
        public void C042b_RestoreMana_WithIntMaxValue_ManaDoesNotExceedMax()
        {
            var character = new Character("Hero", 100, 100);
            character.UseMana(50);
            Assert.Equal(50, character.CurrentMana);
            character.RestoreMana(int.MaxValue);
            Assert.Equal(100, character.CurrentMana);
        }

        #endregion

        #region === SKILL - Cooldown Property ===

        [Fact]
        public void S001_Cooldown_SetMinusOne_ThrowsArgumentOutOfRangeException()
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => new Skill(-1, 50, 100));
            Assert.Contains("Cooldown must be between 0 and 60 seconds", exception.Message);
        }

        [Fact]
        public void S002_Cooldown_SetZero_Succeeds()
        {
            var skill = new Skill(0, 50, 100);
            Assert.Equal(0, skill.Cooldown);
        }

        [Fact]
        public void S003_Cooldown_SetOne_Succeeds()
        {
            var skill = new Skill(1, 50, 100);
            Assert.Equal(1, skill.Cooldown);
        }

        [Fact]
        public void S004_Cooldown_SetThirty_Succeeds()
        {
            var skill = new Skill(30, 50, 100);
            Assert.Equal(30, skill.Cooldown);
        }

        [Fact]
        public void S005_Cooldown_SetFiftyNine_Succeeds()
        {
            var skill = new Skill(59, 50, 100);
            Assert.Equal(59, skill.Cooldown);
        }

        [Fact]
        public void S006_Cooldown_SetSixty_Succeeds()
        {
            var skill = new Skill(60, 50, 100);
            Assert.Equal(60, skill.Cooldown);
        }

        [Fact]
        public void S007_Cooldown_SetSixtyOne_ThrowsArgumentOutOfRangeException()
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => new Skill(61, 50, 100));
            Assert.Contains("Cooldown must be between 0 and 60 seconds", exception.Message);
        }

        #endregion

        #region === SKILL - Mana Property ===

        [Fact]
        public void S008_Mana_SetMinusOne_ThrowsArgumentOutOfRangeException()
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => new Skill(10, -1, 100));
            Assert.Contains("Mana must be between 0 and 100", exception.Message);
        }

        [Fact]
        public void S009_Mana_SetZero_Succeeds()
        {
            var skill = new Skill(10, 0, 100);
            Assert.Equal(0, skill.Mana);
        }

        [Fact]
        public void S010_Mana_SetOne_Succeeds()
        {
            var skill = new Skill(10, 1, 100);
            Assert.Equal(1, skill.Mana);
        }

        [Fact]
        public void S011_Mana_SetFifty_Succeeds()
        {
            var skill = new Skill(10, 50, 100);
            Assert.Equal(50, skill.Mana);
        }

        [Fact]
        public void S012_Mana_SetNinetyNine_Succeeds()
        {
            var skill = new Skill(10, 99, 100);
            Assert.Equal(99, skill.Mana);
        }

        [Fact]
        public void S013_Mana_SetOneHundred_Succeeds()
        {
            var skill = new Skill(10, 100, 100);
            Assert.Equal(100, skill.Mana);
        }

        [Fact]
        public void S014_Mana_SetOneHundredOne_ThrowsArgumentOutOfRangeException()
        {
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => new Skill(10, 101, 100));
            Assert.Contains("Mana must be between 0 and 100", exception.Message);
        }

        #endregion

        #region === SKILL - ReduceCooldown() ===

        [Fact]
        public void S015_ReduceCooldown_WhenZero_NoChange()
        {
            var skill = new Skill(0, 50, 100);
            skill.SetCurrentCooldown(0);
            skill.ReduceCooldown();
            Assert.Equal(0, skill.CurrentCooldown);
        }

        [Fact]
        public void S016_ReduceCooldown_WhenOne_DecreasesToZero()
        {
            var skill = new Skill(10, 50, 100);
            skill.SetCurrentCooldown(1);
            skill.ReduceCooldown();
            Assert.Equal(0, skill.CurrentCooldown);
        }

        [Fact]
        public void S017_ReduceCooldown_WhenThirty_DecreasesToTwentyNine()
        {
            var skill = new Skill(30, 50, 100);
            skill.SetCurrentCooldown(30);
            skill.ReduceCooldown();
            Assert.Equal(29, skill.CurrentCooldown);
        }

        [Fact]
        public void S018_ReduceCooldown_WhenSixty_DecreasesToFiftyNine()
        {
            var skill = new Skill(60, 50, 100);
            skill.SetCurrentCooldown(60);
            skill.ReduceCooldown();
            Assert.Equal(59, skill.CurrentCooldown);
        }

        #endregion

        #region === SKILL - SetCurrentCooldown() ===

        [Fact]
        public void S019_SetCurrentCooldown_WithNegativeOne_SetsToNegativeOne()
        {
            var skill = new Skill(30, 50, 100);
            skill.SetCurrentCooldown(-1);
            Assert.Equal(-1, skill.CurrentCooldown);
        }

        [Fact]
        public void S020_SetCurrentCooldown_WithZero_SetsToZero()
        {
            var skill = new Skill(30, 50, 100);
            skill.SetCurrentCooldown(0);
            Assert.Equal(0, skill.CurrentCooldown);
        }

        [Fact]
        public void S021_SetCurrentCooldown_WithOne_SetsToOne()
        {
            var skill = new Skill(30, 50, 100);
            skill.SetCurrentCooldown(1);
            Assert.Equal(1, skill.CurrentCooldown);
        }

        [Fact]
        public void S022_SetCurrentCooldown_WithIntMaxMinusOne_SetsToIntMaxMinusOne()
        {
            var skill = new Skill(30, 50, 100);
            skill.SetCurrentCooldown(int.MaxValue - 1);
            Assert.Equal(int.MaxValue - 1, skill.CurrentCooldown);
        }

        [Fact]
        public void S023_SetCurrentCooldown_WithIntMaxValue_SetsToIntMaxValue()
        {
            var skill = new Skill(30, 50, 100);
            skill.SetCurrentCooldown(int.MaxValue);
            Assert.Equal(int.MaxValue, skill.CurrentCooldown);
        }

        #endregion

        #region === SKILL - Cast() Decision Table ===

        [Fact]
        public void S024_Cast_WithEnoughMana_ZeroCooldown_ValidTarget_ReturnsTrue()
        {
            var skill = new Skill(10, 50, 100, "Fireball");
            skill.Target = "Enemy";
            var caster = new Character("Hero", 100, 100);
            var target = new Character("Enemy", 100, 50);

            var result = skill.Cast(caster, target);

            Assert.True(result);
        }

        [Fact]
        public void S025_Cast_WithEnoughMana_ZeroCooldown_NoTarget_ReturnsFalse()
        {
            var skill = new Skill(10, 50, 100, "Fireball");
            skill.Target = "Enemy";
            var caster = new Character("Hero", 100, 100);

            var result = skill.Cast(caster, null);

            Assert.False(result);
        }

        [Fact]
        public void S026_Cast_WithEnoughMana_CooldownNotZero_ValidTarget_ReturnsFalse()
        {
            var skill = new Skill(10, 50, 100, "Fireball");
            skill.Target = "Enemy";
            skill.SetCurrentCooldown(5);
            var caster = new Character("Hero", 100, 100);
            var target = new Character("Enemy", 100, 50);

            var result = skill.Cast(caster, target);

            Assert.False(result);
        }

        [Fact]
        public void S027_Cast_WithEnoughMana_CooldownNotZero_NoTarget_ReturnsFalse()
        {
            var skill = new Skill(10, 50, 100, "Fireball");
            skill.Target = "Enemy";
            skill.SetCurrentCooldown(5);
            var caster = new Character("Hero", 100, 100);

            var result = skill.Cast(caster, null);

            Assert.False(result);
        }

        [Fact]
        public void S028_Cast_NotEnoughMana_ZeroCooldown_ValidTarget_ReturnsFalse()
        {
            var skill = new Skill(10, 50, 100, "Fireball");
            skill.Target = "Enemy";
            var caster = new Character("Hero", 100, 30);
            var target = new Character("Enemy", 100, 50);

            var result = skill.Cast(caster, target);

            Assert.False(result);
        }

        [Fact]
        public void S029_Cast_NotEnoughMana_ZeroCooldown_NoTarget_ReturnsFalse()
        {
            var skill = new Skill(10, 50, 100, "Fireball");
            skill.Target = "Enemy";
            var caster = new Character("Hero", 100, 30);

            var result = skill.Cast(caster, null);

            Assert.False(result);
        }

        [Fact]
        public void S030_Cast_NotEnoughMana_CooldownNotZero_ValidTarget_ReturnsFalse()
        {
            var skill = new Skill(10, 50, 100, "Fireball");
            skill.Target = "Enemy";
            skill.SetCurrentCooldown(5);
            var caster = new Character("Hero", 100, 30);
            var target = new Character("Enemy", 100, 50);

            var result = skill.Cast(caster, target);

            Assert.False(result);
        }

        [Fact]
        public void S031_Cast_NotEnoughMana_CooldownNotZero_NoTarget_ReturnsFalse()
        {
            var skill = new Skill(10, 50, 100, "Fireball");
            skill.Target = "Enemy";
            skill.SetCurrentCooldown(5);
            var caster = new Character("Hero", 100, 30);

            var result = skill.Cast(caster, null);

            Assert.False(result);
        }

        #endregion

        #region === SKILL - Cast() Decision Table (HP <= 0) ===

        [Fact]
        public void S032_Cast_Dead_WithEnoughMana_ZeroCooldown_ValidTarget_ReturnsFalse()
        {
            var skill = new Skill(0, 50, 10, "Fireball");
            skill.Target = "Enemy";
            var caster = new Character("Caster", 0, 100);
            var target = new Character("Target", 100, 100);

            var result = skill.Cast(caster, target);

            Assert.False(result);
        }

        [Fact]
        public void S033_Cast_Dead_WithEnoughMana_ZeroCooldown_NoTarget_ReturnsFalse()
        {
            var skill = new Skill(0, 50, 10, "Fireball");
            var caster = new Character("Caster", 0, 100);

            var result = skill.Cast(caster, null);

            Assert.False(result);
        }

        [Fact]
        public void S034_Cast_Dead_WithEnoughMana_NonZeroCooldown_ValidTarget_ReturnsFalse()
        {
            var skill = new Skill(10, 50, 10, "Fireball");
            skill.Target = "Enemy";
            skill.SetCurrentCooldown(5);
            var caster = new Character("Caster", 0, 100);
            var target = new Character("Target", 100, 100);

            var result = skill.Cast(caster, target);

            Assert.False(result);
        }

        [Fact]
        public void S035_Cast_Dead_WithEnoughMana_NonZeroCooldown_NoTarget_ReturnsFalse()
        {
            var skill = new Skill(10, 50, 10, "Fireball");
            skill.SetCurrentCooldown(5);
            var caster = new Character("Caster", 0, 100);

            var result = skill.Cast(caster, null);

            Assert.False(result);
        }

        [Fact]
        public void S036_Cast_Dead_NotEnoughMana_ZeroCooldown_ValidTarget_ReturnsFalse()
        {
            var skill = new Skill(0, 50, 10, "Fireball");
            skill.Target = "Enemy";
            var caster = new Character("Caster", 0, 0);
            var target = new Character("Target", 100, 100);

            var result = skill.Cast(caster, target);

            Assert.False(result);
        }

        [Fact]
        public void S037_Cast_Dead_NotEnoughMana_ZeroCooldown_NoTarget_ReturnsFalse()
        {
            var skill = new Skill(0, 50, 10, "Fireball");
            var caster = new Character("Caster", 0, 0);

            var result = skill.Cast(caster, null);

            Assert.False(result);
        }

        [Fact]
        public void S038_Cast_Dead_NotEnoughMana_NonZeroCooldown_ValidTarget_ReturnsFalse()
        {
            var skill = new Skill(10, 50, 10, "Fireball");
            skill.Target = "Enemy";
            skill.SetCurrentCooldown(5);
            var caster = new Character("Caster", 0, 0);
            var target = new Character("Target", 100, 100);

            var result = skill.Cast(caster, target);

            Assert.False(result);
        }

        [Fact]
        public void S039_Cast_Dead_NotEnoughMana_NonZeroCooldown_NoTarget_ReturnsFalse()
        {
            var skill = new Skill(10, 50, 10, "Fireball");
            skill.SetCurrentCooldown(5);
            var caster = new Character("Caster", 0, 0);

            var result = skill.Cast(caster, null);

            Assert.False(result);
        }

        #endregion

        #region === SKILL - Cast() Additional Tests ===

        [Fact]
        public void S040_Cast_WithZeroHP_ReturnsFalse()
        {
            var skill = new Skill(10, 50, 100, "Fireball");
            skill.Target = "Enemy";
            var caster = new Character("Hero", 0, 100);
            var target = new Character("Enemy", 100, 50);

            var result = skill.Cast(caster, target);

            Assert.False(result);
        }

        [Fact]
        public void S041_Cast_WithExactMana_Succeeds()
        {
            var skill = new Skill(10, 50, 100, "Fireball");
            skill.Target = "Enemy";
            var caster = new Character("Hero", 100, 50);
            var target = new Character("Enemy", 100, 50);

            var result = skill.Cast(caster, target);

            Assert.True(result);
        }

        [Fact]
        public void S042_Cast_Success_SetsCooldown()
        {
            var skill = new Skill(10, 50, 100, "Fireball");
            skill.Target = "Enemy";
            var caster = new Character("Hero", 100, 100);
            var target = new Character("Enemy", 100, 50);

            skill.Cast(caster, target);

            Assert.Equal(10, skill.CurrentCooldown);
        }

        [Fact]
        public void S043_Cast_NoTargetRequired_Succeeds()
        {
            var skill = new Skill(10, 50, 100, "Heal");
            skill.Target = null;
            var caster = new Character("Hero", 100, 100);

            var result = skill.Cast(caster, null);

            Assert.True(result);
        }

        [Fact]
        public void S044_Cast_TargetDead_ReturnsFalse()
        {
            var skill = new Skill(10, 50, 100, "Fireball");
            skill.Target = "Enemy";
            var caster = new Character("Hero", 100, 100);
            var target = new Character("Enemy", 0, 50);

            var result = skill.Cast(caster, target);

            Assert.False(result);
        }

        [Fact]
        public void S045_Cast_WithDamage_ReducesTargetHP()
        {
            var skill = new Skill(10, 50, 30, "Enemy");
            var caster = new Character("Hero", 100, 100);
            var target = new Character("Enemy", 100, 50);

            skill.Cast(caster, target);

            Assert.Equal(70, target.CurrentHP);
        }

        #endregion
    }
}
